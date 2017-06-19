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
    public class DPoleSection
    {
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        //private int designCode;

        //public int DesignCode
        //{
        //    get { return designCode; }
        //    set { designCode = value; }
        //}

        private int productType;
        public int ProductType
        {
            get { return productType; }
            set { productType = value; }
        }

        private Guid productCode;
        public Guid ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private Guid sectionCode;
        public Guid SectionCode
        {
            get { return sectionCode; }
            set { sectionCode = value; }
        }

        private double angle;
        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        private string poleNumber;
        public string PoleNumber
        {
            get { return poleNumber; }
            set { poleNumber = value; }
        }

        //public bool insert(SqlTransaction transaction,SqlConnection _connection)
        //{
        //    //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlConnection connection = _connection;
        //    SqlCommand command = new SqlCommand("D_PoleSection_Insert", connection);
        //    command.Transaction = transaction;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    command.Parameters.Add(new SqlParameter("iproductCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iProductType", ProductType));
        //    command.Parameters.Add(new SqlParameter("iSectioncode",  SectionCode));

        //    try
        //    {
        //        //connection.Open();
        //        command.ExecuteNonQuery();
        //        //connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format("Error DPoleSection.Insert : {0} \n", ex1.Message));
        //        //connection.Close();
        //        return false;
        //    }

        //}

        //public static bool Delete(int DesignCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("D_PoleSection_Delete", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR DPoleSection.Delete {0}\n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }
        //}

        //public static DataTable SelectBySectionCodeDesignCodeProductType(Guid SectionCode, int DesignCode, int ProductType)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_PoleSection_SelectBySectionCodeDesignCodeProductType", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iSectionCode",  SectionCode));
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductType", ProductType));

        //    DataSet dsPoleSection = new DataSet();
        //    adapter.Fill(dsPoleSection);
        //    return dsPoleSection.Tables[0];
        //}

        //public static DPoleSection SelectByDesignCodeProductCodeProductType(Guid ProductCode, int DesignCode, int ProductType)
        //{

        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("D_PoleSection_SelectByDesignCodeProductCodeProductType", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iProductType", ProductType));
        //    command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    DPoleSection dPoleSection = new DPoleSection ();
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            dPoleSection.Code=Convert.ToInt32(reader["Code"]);
        //            dPoleSection.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //            dPoleSection.ProductCode = new Guid(reader["ProductCode"].ToString());
        //            dPoleSection.ProductType = Convert.ToInt32(reader["ProductType"]);
        //            dPoleSection.SectionCode = new Guid(reader["SectionCode"].ToString());


        //        }

        //        reader.Close();
        //        connection.Close();

        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR D_PoleSection_SelectByDesignCodeProductCodeProductType {0}\n", ex1.Message));
        //        connection.Close();
        //    }
        //    return dPoleSection;
        //    //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    //SqlDataAdapter adapter = new SqlDataAdapter("D_PoleSection_SelectByDesignCodeProductCodeProductType", connection);
        //    //adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    //adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductType", ProductType));
        //    //adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    //adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductCode", ProductType));

        //    //DataSet dsPoleSection = new DataSet();
        //    //adapter.Fill(dsPoleSection);
        //    //return dsPoleSection.Tables[0];
        //}

        //public static DataTable SelectBySectionCodeDesignCode(Guid SectionCode, int DesignCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_PoleSection_SelectBySectionCodeDesignCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iSectionCode", SectionCode));
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    DataSet dsPoleSection = new DataSet();
        //    adapter.Fill(dsPoleSection);
        //    return dsPoleSection.Tables[0];
        //}

        //~~~~~~~~~~~~~~~~~~AccessPart~~~~~~~~~~~~~~~

        public bool Accessinsert(OleDbTransaction transaction, OleDbConnection _connection)
        {
            //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_PoleSection_Insert", connection);
            command.Transaction = transaction;
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            command.Parameters.Add(new OleDbParameter("iProductType", ProductType));
            command.Parameters.Add(new OleDbParameter("iproductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iSectioncode", SectionCode));

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
                ed.WriteMessage(string.Format("#Error DPoleSection.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

        }

        public static bool AccessDelete()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_PoleSection_Delete", connection);
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
                ed.WriteMessage(string.Format(" ERROR DPoleSection.Delete {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool AccessDelete(OleDbConnection aConnection)
        {
            OleDbConnection connection = aConnection;
            OleDbCommand command = new OleDbCommand("D_PoleSection_Delete", connection);
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
                ed.WriteMessage(string.Format(" ERROR DPoleSection.Delete {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }
        public static DataTable AccessSelectBySectionCodeProductType(Guid SectionCode, int ProductType)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_PoleSection_SelectBySectionCodeProductType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductType", ProductType));

            DataSet dsPoleSection = new DataSet();
            adapter.Fill(dsPoleSection);
            return dsPoleSection.Tables[0];
        }


        //CALCULATION HATAMI
        public static DataTable AccessSelectBySectionCodeProductType(Guid SectionCode, int ProductType,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_PoleSection_SelectBySectionCodeProductType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductType", ProductType));

            DataSet dsPoleSection = new DataSet();
            adapter.Fill(dsPoleSection);
            return dsPoleSection.Tables[0];
        }
        public static DPoleSection AccessSelectByProductCodeProductType(Guid ProductCode, int ProductType)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_PoleSection_SelectByProductCodeProductType", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));

            command.Parameters.Add(new OleDbParameter("iProductType", ProductType));
            //command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            DPoleSection dPoleSection = new DPoleSection();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dPoleSection.Code = Convert.ToInt32(reader["Code"]);
                    //dPoleSection.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                    dPoleSection.ProductCode = new Guid(reader["ProductCode"].ToString());
                    dPoleSection.ProductType = Convert.ToInt32(reader["ProductType"]);
                    dPoleSection.SectionCode = new Guid(reader["SectionCode"].ToString());


                }
                else
                {
                    dPoleSection.SectionCode = Guid.Empty;
                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR D_PoleSection_SelectByDesignCodeProductCodeProductType {0}\n", ex1.Message));
                connection.Close();
            }
            return dPoleSection;
            //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            //SqlDataAdapter adapter = new SqlDataAdapter("D_PoleSection_SelectByDesignCodeProductCodeProductType", connection);
            //adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductType", ProductType));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductCode", ProductType));

            //DataSet dsPoleSection = new DataSet();
            //adapter.Fill(dsPoleSection);
            //return dsPoleSection.Tables[0];
        }

        public static DataTable AccessSelectBySectionCode(Guid SectionCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_PoleSection_SelectBySectionCodeDesignCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            DataSet dsPoleSection = new DataSet();
            adapter.Fill(dsPoleSection);
            return dsPoleSection.Tables[0];
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_PoleSection_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsPoleSection = new DataSet();
            adapter.Fill(dsPoleSection);
            return dsPoleSection.Tables[0];
        }


        //CALCULATION HATAMI
        public static DataTable AccessSelectAll(OleDbConnection _Connection)
        {
            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_PoleSection_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsPoleSection = new DataSet();
            adapter.Fill(dsPoleSection);
            return dsPoleSection.Tables[0];
        }

        //*******************Access To Memory For Calc
        public static DPoleSection AccessSelectByProductCodeProductType(DataTable dtPoleSection, Guid ProductCode, int ProductType)
        {
            DataRow[] dr = dtPoleSection.Select(string.Format("ProductCode='{0}' AND ProductType={1}", ProductCode, ProductType));
            DPoleSection dPoleSection = new DPoleSection();

            if (dr.Length != 0)
            {
                dPoleSection.Code = Convert.ToInt32(dr[0]["Code"]);
                //dPoleSection.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                dPoleSection.ProductCode = new Guid(dr[0]["ProductCode"].ToString());
                dPoleSection.ProductType = Convert.ToInt32(dr[0]["ProductType"]);
                dPoleSection.SectionCode = new Guid(dr[0]["SectionCode"].ToString());


            }
            else
            {
                dPoleSection.SectionCode = Guid.Empty;
            }

            return dPoleSection;

        }



        public static DataTable AccessSelectBySectionCodeProductType(DataTable dtPoleSection,Guid SectionCode, int ProductType)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_PoleSection_SelectBySectionCodeProductType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductType", ProductType));

            DataSet dsPoleSection = new DataSet();
            adapter.Fill(dsPoleSection);
            return dsPoleSection.Tables[0];
        }
    }
}
