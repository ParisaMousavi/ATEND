using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;


using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Design
{
    public class DDesignProfile
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int designId;
        public int DesignId
        {
            get { return designId; }
            set { designId = value; }
        }

        private string designName;
        public string DesignName
        {
            get { return designName; }
            set { designName = value; }
        }

        private string designCode;
        public string DesignCode
        {
            get { return designCode; }
            set { designCode = value; }
        }

        private DateTime designDate;
        public DateTime DesignDate
        {
            get { return designDate; }
            set { designDate = value; }
        }

        private float scale;
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private int zone;
        public int Zone
        {
            get { return zone; }
            set { zone = value; }
        }

        private string validate;
        public string Validate
        {
            get { return validate; }
            set { validate = value; }
        }

        private string employer;
        public string Employer
        {
            get { return employer; }
            set { employer = value; }
        }

        private string adviser;
        public string Adviser
        {
            get { return adviser; }
            set { adviser = value; }
        }

        private string designer;
        public string Designer
        {
            get { return designer; }
            set { designer = value; }
        }

        private string controller;
        public string Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        private string supporter;
        public string Supporter
        {
            get { return supporter; }
            set { supporter = value; }
        }

        private string approval;
        public string Approval
        {
            get { return approval; }
            set { approval = value; }
        }

        private string planner;
        public string Planner
        {
            get { return planner; }
            set { planner = value; }
        }

        private string edition;
        public string Edition
        {
            get { return edition; }
            set { edition = value; }
        }

        //private int userCode;
        //public int UserCode
        //{
        //    get { return userCode; }
        //    set { userCode = value; }
        //}


        //private int additionalCode;
        //public int AdditionalCode
        //{
        //    get { return additionalCode; }
        //    set { additionalCode = value; }
        //}


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("B_DesignProfile_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
            Command.Parameters.Add(new SqlParameter("iDesignName", DesignName));
            Command.Parameters.Add(new SqlParameter("iDesignDate", DesignDate));
            Command.Parameters.Add(new SqlParameter("iScale", Scale));
            Command.Parameters.Add(new SqlParameter("iAddress", Address));
            Command.Parameters.Add(new SqlParameter("iZone", Zone));
            Command.Parameters.Add(new SqlParameter("iValidate", Validate));
            Command.Parameters.Add(new SqlParameter("iEmployer", Employer));
            Command.Parameters.Add(new SqlParameter("iAdviser", Adviser));
            Command.Parameters.Add(new SqlParameter("iDesigner", Designer));
            Command.Parameters.Add(new SqlParameter("iController", Controller));
            Command.Parameters.Add(new SqlParameter("iSupporter", Supporter));
            Command.Parameters.Add(new SqlParameter("iApproval", Approval));
            Command.Parameters.Add(new SqlParameter("iPlanner", Planner));
            
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR BDesignProfile.Insert {0}\n", ex1.Message));
                Connection.Close();
                return false;
            }
            Connection.Close();
            return true;
        }

        //frmSaveDesignServer
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;
            SqlCommand Command = new SqlCommand("B_DesignProfile_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
            Command.Parameters.Add(new SqlParameter("iDesignName", DesignName));
            Command.Parameters.Add(new SqlParameter("iDesignDate", DesignDate));
            Command.Parameters.Add(new SqlParameter("iScale", Scale));
            Command.Parameters.Add(new SqlParameter("iAddress", Address));
            Command.Parameters.Add(new SqlParameter("iZone", Zone));
            //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Validate:{0}\n", Validate);
            Command.Parameters.Add(new SqlParameter("iValidate", Validate));
            Command.Parameters.Add(new SqlParameter("iEmployer", Employer));
            Command.Parameters.Add(new SqlParameter("iAdviser", Adviser));
            Command.Parameters.Add(new SqlParameter("iDesigner", Designer));
            Command.Parameters.Add(new SqlParameter("iController", Controller));
            Command.Parameters.Add(new SqlParameter("iSupporter", Supporter));
            Command.Parameters.Add(new SqlParameter("iApproval", Approval));
            Command.Parameters.Add(new SqlParameter("iPlanner", Planner));

            try
            {
                Command.ExecuteNonQuery();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR BDesignProfile.Insert {0}\n", ex1.Message));
                return false;
            }
            return true;
        }


        public bool Update()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("B_DesignProfile_Update", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iId", Id));
            Command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
            Command.Parameters.Add(new SqlParameter("iDesignName", DesignName));
            Command.Parameters.Add(new SqlParameter("iDesignDate", DesignDate));
            Command.Parameters.Add(new SqlParameter("iScale", Scale));
            Command.Parameters.Add(new SqlParameter("iAddress", Address));
            Command.Parameters.Add(new SqlParameter("iZone", Zone));
            Command.Parameters.Add(new SqlParameter("iValidate", Validate));
            Command.Parameters.Add(new SqlParameter("iEmployer", Employer));
            Command.Parameters.Add(new SqlParameter("iAdviser", Adviser));
            Command.Parameters.Add(new SqlParameter("iDesigner", Designer));
            Command.Parameters.Add(new SqlParameter("iController", Controller));
            Command.Parameters.Add(new SqlParameter("iSupporter", Supporter));
            Command.Parameters.Add(new SqlParameter("iApproval", Approval));
            Command.Parameters.Add(new SqlParameter("iPlanner", Planner));

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();

            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BDesignProfile.Update {0}\n", ex1.Message));
                Connection.Close();
                return false;
            }
            Connection.Close();
            return true;
        }

       //public bool Update(SqlTransaction _transaction, SqlConnection _connection)
       // {
       //     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
       //     SqlConnection Connection = _connection;
       //     SqlCommand Command = new SqlCommand("B_DesignProfile_Update", Connection);
       //     Command.CommandType = CommandType.StoredProcedure;
       //     Command.Transaction = _transaction;

       //     //ed.WriteMessage("Id is {0} \n", Id);
       //     Command.Parameters.Add(new SqlParameter("iId", Id));
       //     Command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
       //     Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
       //     Command.Parameters.Add(new SqlParameter("iDesignName", DesignName));
       //     Command.Parameters.Add(new SqlParameter("iDesignDate", DesignDate));
       //     Command.Parameters.Add(new SqlParameter("iScale", Scale));
       //     Command.Parameters.Add(new SqlParameter("iAddress", Address));
       //     Command.Parameters.Add(new SqlParameter("iZone", Zone));
       //     Command.Parameters.Add(new SqlParameter("iValidate", Validate));
       //     Command.Parameters.Add(new SqlParameter("iEmployer", Employer));
       //     Command.Parameters.Add(new SqlParameter("iAdviser", Adviser));
       //     Command.Parameters.Add(new SqlParameter("iDesigner", Designer));
       //     Command.Parameters.Add(new SqlParameter("iController", Controller));
       //     Command.Parameters.Add(new SqlParameter("iSupporter", Supporter));
       //     Command.Parameters.Add(new SqlParameter("iApproval", Approval));
       //     Command.Parameters.Add(new SqlParameter("iPlanner", Planner));
       //     try
       //     {
       //         Command.ExecuteNonQuery();
       //     }
       //     catch (System.Exception ex1)
       //     {
       //         ed.WriteMessage(string.Format(" ERROR BDesignProfile.Update {0}\n", ex1.Message));
       //         return false;
       //     }
       //     return true;
       // }

        //frmSaveDesignServer
        public bool UpdateByDesignId(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;
            SqlCommand Command = new SqlCommand("B_DesignProfile_UpdateByDesignId", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            //ed.WriteMessage("Id is {0} \n", Id);
            //Command.Parameters.Add(new SqlParameter("iId", Id));
            Command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
            Command.Parameters.Add(new SqlParameter("iDesignName", DesignName));
            Command.Parameters.Add(new SqlParameter("iDesignDate", DesignDate));
            Command.Parameters.Add(new SqlParameter("iScale", Scale));
            Command.Parameters.Add(new SqlParameter("iAddress", Address));
            Command.Parameters.Add(new SqlParameter("iZone", Zone));
            Command.Parameters.Add(new SqlParameter("iValidate", Validate));
            Command.Parameters.Add(new SqlParameter("iEmployer", Employer));
            Command.Parameters.Add(new SqlParameter("iAdviser", Adviser));
            Command.Parameters.Add(new SqlParameter("iDesigner", Designer));
            Command.Parameters.Add(new SqlParameter("iController", Controller));
            Command.Parameters.Add(new SqlParameter("iSupporter", Supporter));
            Command.Parameters.Add(new SqlParameter("iApproval", Approval));
            Command.Parameters.Add(new SqlParameter("iPlanner", Planner));


            try
            {
                Command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR BDesignProfile.Update {0}\n", ex1.Message));
                return false;
            }
            return true;
        }


        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_DesignProfile_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsDesignProfile = new DataSet();
            adapter.Fill(dsDesignProfile);

            return dsDesignProfile.Tables[0];
        }

        public static DDesignProfile SelectByID(int ID)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("B_DesignProfile_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", ID));
            DDesignProfile DP = new DDesignProfile();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DP.Id = Convert.ToInt32(reader["Id"].ToString());
                    DP.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                    DP.DesignName = reader["DesignName"].ToString();
                    DP.DesignCode = Convert.ToString(reader["DesignCode"]);
                    DP.Scale = Convert.ToSingle(reader["Scale"].ToString());
                    DP.DesignDate = Convert.ToDateTime(reader["DesignDate"].ToString());
                    DP.Address = reader["Address"].ToString();
                    DP.Zone = Convert.ToInt32(reader["Zone"].ToString());
                    DP.Validate = reader["Validate"].ToString();
                    DP.Employer = reader["Employer"].ToString();
                    DP.Adviser = reader["Adviser"].ToString();
                    DP.Designer = reader["Designer"].ToString();
                    DP.Controller = reader["Controller"].ToString();
                    DP.Supporter = reader["Supporter"].ToString();
                    DP.Approval = reader["Approval"].ToString();
                    DP.Planner = reader["Planner"].ToString();

                }
                else
                {
                    DP.id = -1;
                    DP.DesignCode = "";
                    DP.DesignName = "NONE";
                    DP.Scale = 0;
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for DesignCode : \n", ""));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error DDesignProfile.Select : {0} \n", ex1.Message));
                connection.Close();
            }
            return DP;
        }

        //OpenFromServer
        public static DDesignProfile SelectByDesignID(int DesignID)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("B_DesignProfile_SelectByDesignId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iDesignId", DesignID));
            DDesignProfile DP = new DDesignProfile();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DP.Id = Convert.ToInt32(reader["Id"].ToString());
                    DP.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                    DP.DesignName = reader["DesignName"].ToString();
                    DP.DesignCode = Convert.ToString(reader["DesignCode"]);
                    DP.Scale = Convert.ToSingle(reader["Scale"].ToString());
                    DP.DesignDate = Convert.ToDateTime(reader["DesignDate"].ToString());
                    DP.Address = reader["Address"].ToString();
                    DP.Zone = Convert.ToInt32(reader["Zone"].ToString());
                    DP.Validate = reader["Validate"].ToString();
                    DP.Employer = reader["Employer"].ToString();
                    DP.Adviser = reader["Adviser"].ToString();
                    DP.Designer = reader["Designer"].ToString();
                    DP.Controller = reader["Controller"].ToString();
                    DP.Supporter = reader["Supporter"].ToString();
                    DP.Approval = reader["Approval"].ToString();
                    DP.Planner = reader["Planner"].ToString();

                }
                else
                {
                    DP.id = -1;
                    DP.DesignCode = "";
                    DP.DesignName = "NONE";
                    DP.Scale = 0;

                    DP.DesignDate = DateTime.Now;
                    DP.Address = "";
                    DP.Zone = 0;
                    DP.Validate = "";
                    DP.Employer = "";
                    DP.Adviser = "";
                    DP.Designer = "";
                    DP.Controller = "";
                    DP.Supporter = "";
                    DP.Approval = "";
                    DP.Planner = "";

                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for DesignCode : \n", ""));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error DDesignProfile.Select : {0} \n", ex1.Message));
                connection.Close();
            }
            return DP;
        }

        //frmSaveDesignServer
        public static DDesignProfile SelectByDesignID(SqlTransaction _transaction  , SqlConnection _connection , int DesignID)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("B_DesignProfile_SelectByDesignId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iDesignId", DesignID));
            command.Transaction = _transaction;

            DDesignProfile DP = new DDesignProfile();
            try
            {
                //connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DP.Id = Convert.ToInt32(reader["Id"].ToString());
                    DP.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                    DP.DesignName = reader["DesignName"].ToString();
                    DP.DesignCode = Convert.ToString(reader["DesignCode"]);
                    DP.Scale = Convert.ToSingle(reader["Scale"].ToString());
                    DP.DesignDate = Convert.ToDateTime(reader["DesignDate"].ToString());
                    DP.Address = reader["Address"].ToString();
                    DP.Zone = Convert.ToInt32(reader["Zone"].ToString());
                    DP.Validate = reader["Validate"].ToString();
                    DP.Employer = reader["Employer"].ToString();
                    DP.Adviser = reader["Adviser"].ToString();
                    DP.Designer = reader["Designer"].ToString();
                    DP.Controller = reader["Controller"].ToString();
                    DP.Supporter = reader["Supporter"].ToString();
                    DP.Approval = reader["Approval"].ToString();
                    DP.Planner = reader["Planner"].ToString();

                }
                else
                {
                    DP.Id = -1;
                    DP.DesignCode = "";
                    DP.DesignName = "NONE";
                    DP.Scale = 0;

                    DP.DesignDate = DateTime.Now;
                    DP.Address = "";
                    DP.Zone = 0;
                    DP.Validate = "";
                    DP.Employer = "";
                    DP.Adviser = "";
                    DP.Designer = "";
                    DP.Controller = "";
                    DP.Supporter = "";
                    DP.Approval = "";
                    DP.Planner = "";

                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for DesignCode : \n", ""));

                }
                reader.Close();
                //connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error DDesignProfile.Select : {0} \n", ex1.Message));
                //connection.Close();
            }
            return DP;
        }



        //~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        //public bool Insert()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.AccessCnString);
        //    SqlCommand Command = new SqlCommand("B_DesignProfile_Insert", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;

        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iDesignerName", DesignerName));
        //    Command.Parameters.Add(new SqlParameter("iDesignDate", DesignDate));
        //    Command.Parameters.Add(new SqlParameter("iScale", Scale));

        //    try
        //    {
        //        Connection.Open();
        //        Command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR BDesignProfile.Insert {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }

        //}

        //frmDesignProfile
        public bool AccessInsert()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_DesignProfile_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new OleDbParameter("iDesignId", DesignId));
            Command.Parameters.Add(new OleDbParameter("iDesignName", DesignName));
            Command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            Command.Parameters.Add(new OleDbParameter("iDesignDate", DesignDate));
            Command.Parameters.Add(new OleDbParameter("iScale", Scale));
            Command.Parameters.Add(new OleDbParameter("iAddress", Address));
            Command.Parameters.Add(new OleDbParameter("iZone", Zone));
            Command.Parameters.Add(new OleDbParameter("iValidate", Validate));
            Command.Parameters.Add(new OleDbParameter("iEmployer", Employer));
            Command.Parameters.Add(new OleDbParameter("iAdviser", Adviser));
            Command.Parameters.Add(new OleDbParameter("iDesigner", Designer));
            Command.Parameters.Add(new OleDbParameter("iController", Controller));
            Command.Parameters.Add(new OleDbParameter("iSupporter", Supporter));
            Command.Parameters.Add(new OleDbParameter("iApproval", Approval));
            Command.Parameters.Add(new OleDbParameter("iPlanner", Planner));
            Command.Parameters.Add(new OleDbParameter("iEdition", Edition));
            //Command.Parameters.Add(new OleDbParameter("iAdditionalCode", AdditionalCode));

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BDesignProfile.AccessInsert {0}\n", ex1.Message));

                Connection.Close();
                return false;
            }

        }

        //frmDesignProfile //openFromServer
        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_DesignProfile_Update", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("Id:{0} \n",Id);
            Command.Parameters.Add(new OleDbParameter("iId", Id));
            //ed.WriteMessage("DdesignId:{0}\n", DesignId);
            Command.Parameters.Add(new OleDbParameter("iDesignId", DesignId));
            //ed.WriteMessage("DesignName:{0}\n", DesignName);
            Command.Parameters.Add(new OleDbParameter("iDesignName", DesignName));
            //ed.WriteMessage("DesignCode:{0}\n", DesignCode);
            Command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            //ed.WriteMessage("DesignDate:{0}\n", DesignDate);
            Command.Parameters.Add(new OleDbParameter("iDesignDate", DesignDate));
            //ed.WriteMessage("Scale:{0}\n", Scale);
            Command.Parameters.Add(new OleDbParameter("iScale", Scale));
            //ed.WriteMessage("Address:{0}\n", Address);
            Command.Parameters.Add(new OleDbParameter("iAddress", Address));
            ed.WriteMessage("Zone:{0}\n", Zone);
            Command.Parameters.Add(new OleDbParameter("iZone", Zone));
            //ed.WriteMessage("Validate:{0}\n", Validate);
            Command.Parameters.Add(new OleDbParameter("iValidate", Validate));
            //ed.WriteMessage("Employer:{0}\n", Employer);
            Command.Parameters.Add(new OleDbParameter("iEmployer", Employer));
            //ed.WriteMessage("Adviser:{0}\n", Adviser);
            Command.Parameters.Add(new OleDbParameter("iAdviser", Adviser));
            //ed.WriteMessage("Designer:{0}\n", Designer);
            Command.Parameters.Add(new OleDbParameter("iDesigner", Designer));
            //ed.WriteMessage("Controller:{0}\n", Controller);
            Command.Parameters.Add(new OleDbParameter("iController", Controller));
            //ed.WriteMessage("Supporter:{0}\n", Supporter);
            Command.Parameters.Add(new OleDbParameter("iSupporter", Supporter));
            //ed.WriteMessage("Approval:{0}\n", Approval);
            Command.Parameters.Add(new OleDbParameter("iApproval", Approval));
            //ed.WriteMessage("Planner:{0}\n", Planner);
            Command.Parameters.Add(new OleDbParameter("iPlanner", Planner));
            //Command.Parameters.Add(new OleDbParameter("iAdditionalCode", AdditionalCode));
            Command.Parameters.Add(new OleDbParameter("iEdition", Edition));
            try
            {
                Connection.Open();
                try
                {
                    Command.ExecuteNonQuery();
                }
                catch (System.Data.OleDb.OleDbException ex2)
                {
                    ed.WriteMessage("EEEEEEEERor:{0} \n", ex2.Message);
                }
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR BDesignProfile.AccessUpdate {0}\n", ex1.Message));
                Connection.Close();
                return false;
            }

        }

        //frmSaveDesignServer
        public bool AccessUpdate(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = _connection;
            OleDbCommand Command = new OleDbCommand("D_DesignProfile_Update", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;
            //ed.WriteMessage("Id:{0} \n",Id);
            Command.Parameters.Add(new OleDbParameter("iId", Id));
            //ed.WriteMessage("DdesignId:{0}\n", DesignId);
            Command.Parameters.Add(new OleDbParameter("iDesignId", DesignId));
            //ed.WriteMessage("DesignName:{0}\n", DesignName);
            Command.Parameters.Add(new OleDbParameter("iDesignName", DesignName));
            //ed.WriteMessage("DesignCode:{0}\n", DesignCode);
            Command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            //ed.WriteMessage("DesignDate:{0}\n", DesignDate);
            Command.Parameters.Add(new OleDbParameter("iDesignDate", DesignDate));
            //ed.WriteMessage("Scale:{0}\n", Scale);
            Command.Parameters.Add(new OleDbParameter("iScale", Scale));
            //ed.WriteMessage("Address:{0}\n", Address);
            Command.Parameters.Add(new OleDbParameter("iAddress", Address));
            //ed.WriteMessage("Zone:{0}\n", Zone);
            Command.Parameters.Add(new OleDbParameter("iZone", Zone));
            //ed.WriteMessage("Validate:{0}\n", Validate);
            Command.Parameters.Add(new OleDbParameter("iValidate", Validate));
            //ed.WriteMessage("Employer:{0}\n", Employer);
            Command.Parameters.Add(new OleDbParameter("iEmployer", Employer));
            //ed.WriteMessage("Adviser:{0}\n", Adviser);
            Command.Parameters.Add(new OleDbParameter("iAdviser", Adviser));
            //ed.WriteMessage("Designer:{0}\n", Designer);
            Command.Parameters.Add(new OleDbParameter("iDesigner", Designer));
            //ed.WriteMessage("Controller:{0}\n", Controller);
            Command.Parameters.Add(new OleDbParameter("iController", Controller));
            //ed.WriteMessage("Supporter:{0}\n", Supporter);
            Command.Parameters.Add(new OleDbParameter("iSupporter", Supporter));
            //ed.WriteMessage("Approval:{0}\n", Approval);
            Command.Parameters.Add(new OleDbParameter("iApproval", Approval));
            //ed.WriteMessage("Planner:{0}\n", Planner);
            Command.Parameters.Add(new OleDbParameter("iPlanner", Planner));
            //Command.Parameters.Add(new OleDbParameter("iAdditionalCode", AdditionalCode));
            Command.Parameters.Add(new OleDbParameter("iEdition", Edition));
            try
            {
                try
                {
                    Command.ExecuteNonQuery();
                }
                catch (System.Data.OleDb.OleDbException ex2)
                {
                    ed.WriteMessage("EEEEEEEERor:{0} \n", ex2.Message);
                }
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR BDesignProfile.AccessUpdate {0}\n", ex1.Message));
                return false;
            }

        }

        //MOUSAVI -> frmDesignProfile //AcDrawFrame
        public static DDesignProfile AccessSelect()
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("a1\n");
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_DesignProfile_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            DDesignProfile DP = new DDesignProfile();
            //ed.WriteMessage("a2\n");
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("a3\n");
                if (reader.Read())
                {
                    DP.Id = Convert.ToInt32(reader["Id"].ToString());
                    DP.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                    DP.DesignName = reader["DesignName"].ToString();
                    DP.DesignCode = Convert.ToString(reader["DesignCode"]);
                    DP.Scale = Convert.ToSingle(reader["Scale"].ToString());
                    try
                    {
                        DP.DesignDate = Convert.ToDateTime(reader["DesignDate"].ToString());
                    }
                    catch
                    {
                        DP.DesignDate = DateTime.Now;
                    }
                    DP.Address = reader["Address"].ToString();
                    if (reader["Zone"].ToString() == "")
                        DP.Zone = 0;
                    else
                        DP.Zone = Convert.ToInt32(reader["Zone"].ToString());
                    DP.Validate = reader["Validate"].ToString();
                    DP.Employer = reader["Employer"].ToString();
                    DP.Adviser = reader["Adviser"].ToString();
                    DP.Designer = reader["Designer"].ToString();
                    DP.Controller = reader["Controller"].ToString();
                    DP.Supporter = reader["Supporter"].ToString();
                    DP.Approval = reader["Approval"].ToString();
                    DP.Planner = reader["Planner"].ToString();
                    DP.Edition = reader["Edition"].ToString();
                    //DP.AdditionalCode = Convert.ToInt32( reader["AdditionalCode"].ToString());
                    ed.WriteMessage("Record found \n");
                }
                else
                {
                    //ed.WriteMessage("a5\n");
                    DP.Id = 0;
                    DP.DesignId = -1;
                    DP.DesignCode = "";
                    DP.DesignName = "NONE";
                    DP.Scale = 0;
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" >>> >>> No Record found for DesignCode  \n"));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DDesignProfile.AccessSelect : {0} \n", ex1.Message));
                connection.Close();
            }
            return DP;






        }

        //frmDesignSaveServer
        public static DDesignProfile AccessSelect(OleDbTransaction _transaction, OleDbConnection _connection)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_DesignProfile_Select", connection);
            command.Transaction = _transaction;

            command.CommandType = CommandType.StoredProcedure;
            DDesignProfile DP = new DDesignProfile();
            //ed.WriteMessage("a2\n");
            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("a3\n");
                if (reader.Read())
                {
                    //ed.WriteMessage("a4\n");
                    DP.Id = Convert.ToInt32(reader["Id"].ToString());
                    DP.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                    DP.DesignName = reader["DesignName"].ToString();
                    DP.DesignCode = Convert.ToString(reader["DesignCode"]);
                    DP.Scale = Convert.ToSingle(reader["Scale"].ToString());
                    try
                    {
                        DP.DesignDate = Convert.ToDateTime(reader["DesignDate"].ToString());
                    }
                    catch
                    {
                        DP.DesignDate = DateTime.Now;
                    }
                    DP.Address = reader["Address"].ToString();
                    DP.Zone = Convert.ToInt32(reader["Zone"].ToString());
                    DP.Validate = reader["Validate"].ToString();
                    ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Validate:{0}\n", DP.Validate);
                    DP.Employer = reader["Employer"].ToString();
                    DP.Adviser = reader["Adviser"].ToString();
                    DP.Designer = reader["Designer"].ToString();
                    DP.Controller = reader["Controller"].ToString();
                    DP.Supporter = reader["Supporter"].ToString();
                    DP.Approval = reader["Approval"].ToString();
                    DP.Edition = reader["Edition"].ToString();
                    DP.Planner = reader["Planner"].ToString();
                    //DP.AdditionalCode = Convert.ToInt32( reader["AdditionalCode"].ToString());
                    ed.WriteMessage("Record found \n");
                }
                else
                {
                    ed.WriteMessage("a5\n");
                    DP.Id = 0;
                    DP.DesignId = -1;
                    DP.DesignCode = "";
                    DP.DesignName = "NONE";
                    DP.Scale = 0;
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for DesignCode  \n"));

                }
                reader.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DDesignProfile.AccessSelect : {0} \n", ex1.Message));
            }
            return DP;

        }




        //public static BDesignProfile AccessSelect_ByDesignCode(int Code)
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbDataAdapter adapter = new OleDbDataAdapter("B_DesignProfile_SelectByDesignCode", Connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", Code));
        //    DataSet dsDP = new DataSet();
        //    adapter.Fill(dsDP);
        //    BDesignProfile DP = new BDesignProfile();
        //    if (dsDP.Tables[0].Rows.Count > 0)
        //    {
        //        DP.Code = Convert.ToInt16(dsDP.Tables[0].Rows[0]["Code"].ToString());
        //        DP.DesignCode = Convert.ToInt32(dsDP.Tables[0].Rows[0]["DesignCode"].ToString());
        //        DP.DesignDate = Convert.ToDateTime(dsDP.Tables[0].Rows[0]["DesignDate"].ToString());
        //        DP.DesignerName = dsDP.Tables[0].Rows[0]["DesignerName"].ToString();
        //        DP.Scale = Convert.ToDouble(dsDP.Tables[0].Rows[0]["Scale"].ToString());
        //    }
        //    else
        //    {
        //        DP.Code = 0;
        //        DP.DesignCode = 0;
        //        DP.DesignDate = Convert.ToDateTime("2009/1/1");
        //        DP.DesignerName = "NONE";
        //        DP.Scale = 0;
        //    }
        //    return DP;
        //}
    }
}
