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
    public class DSection
    {
        private Guid code;
        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }

        //private  int designCode;

        // public int DesignCode
        // {
        //     get { return designCode; }
        //     set { designCode = value; }
        // }

        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public ArrayList PoleSection = new ArrayList();
        public ArrayList GlobalSection = new ArrayList();
   
        public bool AccessInsert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbTransaction transaction;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Section_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = Guid.NewGuid();
            //Guid InsertedCode;
            //ed.WriteMessage("Code= "+Code.ToString()+"\n");
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {

                    command.ExecuteNonQuery();


                    bool canCommitTransaction = true;
                    int counter = 0;

                    ed.WriteMessage("count of subequip " + PoleSection.Count.ToString() + " \n");
                    while (canCommitTransaction && counter < PoleSection.Count)
                    {
                        //Atend.Base.Design.DPoleSection  _Polesection;
                        //_Polesection = ((Atend.Base.Design.DPoleSection)PoleSection[counter]);
                        //_Polesection.SectionCode code;
                        Atend.Base.Design.DPoleSection _PoleSection;
                        _PoleSection = ((Atend.Base.Design.DPoleSection)PoleSection[counter]);
                        _PoleSection.SectionCode = Code;

                        //ed.WriteMessage("ProductType= "+_PoleSection.ProductType.ToString());
                        if (_PoleSection.Accessinsert(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.writeMessage("Error For Insert \n");
                        }
                        counter++;
                    }

                    counter = 0;
                    while (canCommitTransaction && counter<GlobalSection.Count)
                    {
                        Atend.Base.Design.DGlobal _Global;
                        _Global = ((Atend.Base.Design.DGlobal)GlobalSection[counter]);
                        if (_Global.Accessinsert(transaction, connection))
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                        counter++;
                    }

                    if (canCommitTransaction)
                    {
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        connection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error Occured During ESection Insertion 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DSection.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error DeSection.Insert : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}

        }


  
        //Extra
        public static DataTable AccessSelectAll()
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Section_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            DataSet dsSection = new DataSet();
            adapter.Fill(dsSection);
            return dsSection.Tables[0];

        }


        public static DSection AccessSelectByCode(Guid Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Section_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader Reader = command.ExecuteReader();
            DSection section = new DSection();
            if (Reader.Read())
            {
                section.Code = new Guid(Reader["Code"].ToString());
                //section.DesignCode = Convert.ToInt32(Reader["DesignCode"]);
                section.Number = Convert.ToInt32(Reader["Number"]);
            }
            else
            {
                section.Code = Guid.Empty;
            }
            Reader.Close();
            connection.Close();
            return section;
        }


        public static bool AccessDelete()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Section_Delete", connection);
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
                ed.WriteMessage(string.Format(" ERROR DSection.Delete {0}\n", ex1.Message));
                connection.Close();
                return false;
            }
        }
        public static bool AccessDelete(OleDbConnection aConnection)
        {
            OleDbConnection connection = aConnection;
            OleDbCommand command = new OleDbCommand("D_Section_Delete", connection);
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
                ed.WriteMessage(string.Format(" ERROR DSection.Delete {0}\n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

    }
}
