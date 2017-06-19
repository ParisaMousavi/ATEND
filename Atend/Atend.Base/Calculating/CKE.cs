using System;
using System.Collections.Generic;
using System.Text;
//
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections;
using System.Data.OleDb;

namespace Atend.Base.Calculating
{
   public class CKE
    {
        private double crossSectionArea;

        public double CrossSectionArea
        {
            get { return crossSectionArea; }
            set { crossSectionArea = value; }
        }
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private double vertical;

        public double Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }
        private double horizental;

        public double Horizental
        {
            get { return horizental; }
            set { horizental = value; }
        }

        private double triangle;

        public double Triangle
        {
            get { return triangle; }
            set { triangle = value; }
        }

       //public static CKE SelectByCrossSectionArea(double CrossSectionArea)
       //{
       //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //    SqlCommand command = new SqlCommand("C_KE_SelectByCrossSectionArea", connection);
       //    command.CommandType = CommandType.StoredProcedure;
       //    command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
       //    connection.Open();
       //    SqlDataReader reader = command.ExecuteReader();
       //    CKE ke = new CKE();
       //    if (reader.Read())
       //    {
       //        ke.Code = Convert.ToInt32(reader["Code"]);
       //        ke.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"]);
       //        ke.Horizental = Convert.ToSingle(reader["Horizantal"]);
       //        ke.Triangle = Convert.ToSingle(reader["Triangle"]);
       //        ke.Vertical = Convert.ToSingle(reader["Vertical"]);
       //    }
       //    reader.Close();
       //    connection.Close();
       //    return ke;
       //}

       //public static DataTable SelectAll()
       //{
       //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //    SqlDataAdapter adapter = new SqlDataAdapter("C_KE_SelectByCode", connection);
       //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
       //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
       //    DataSet dsKE = new DataSet();
       //    adapter.Fill(dsKE);
       //    return dsKE.Tables[0];
       //}
       //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access PArt~~~~~~~~~~~
       public static CKE AccessSelectByCrossSectionArea(double CrossSectionArea)
       {
           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbCommand command = new OleDbCommand("C_KE_SelectByCrossSectionArea", connection);
           command.CommandType = CommandType.StoredProcedure;
           command.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
           connection.Open();
           OleDbDataReader reader = command.ExecuteReader();
           CKE ke = new CKE();
           if (reader.Read())
           {
               ke.Code = Convert.ToInt32(reader["Code"]);
               ke.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"]);
               ke.Horizental = Convert.ToSingle(reader["Horizantal"]);
               ke.Triangle = Convert.ToSingle(reader["Triangle"]);
               ke.Vertical = Convert.ToSingle(reader["Vertical"]);
           }
           reader.Close();
           connection.Close();
           return ke;
       }

       //CALCULATION HATAMI 
       public static CKE AccessSelectByCrossSectionArea(double CrossSectionArea,OleDbConnection _Connection)
       {
           OleDbConnection connection = _Connection;
           OleDbCommand command = new OleDbCommand("C_KE_SelectByCrossSectionArea", connection);
           command.CommandType = CommandType.StoredProcedure;
           command.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
           //connection.Open();
           OleDbDataReader reader = command.ExecuteReader();
           CKE ke = new CKE();
           if (reader.Read())
           {
               ke.Code = Convert.ToInt32(reader["Code"]);
               ke.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"]);
               ke.Horizental = Convert.ToSingle(reader["Horizantal"]);
               ke.Triangle = Convert.ToSingle(reader["Triangle"]);
               ke.Vertical = Convert.ToSingle(reader["Vertical"]);
           }
           reader.Close();
           //connection.Close();
           return ke;
       }

       public static DataTable AccessSelectAll()
       {
           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbDataAdapter adapter = new OleDbDataAdapter("C_KE_SelectByCode", connection);
           adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
           DataSet dsKE = new DataSet();
           adapter.Fill(dsKE);
           return dsKE.Tables[0];
       }
    }
}
