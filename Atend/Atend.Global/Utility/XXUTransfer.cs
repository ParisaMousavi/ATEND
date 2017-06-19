using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Autodesk.AutoCAD.ApplicationServices;
using System.IO;
using Autodesk.AutoCAD.EditorInput;
namespace Atend.Global.Utility
{
    public class XXUTransfer
    {
        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //public static bool AirPostTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtAirPost = Atend.Base.Equipment.EAirPost.SelectAll();
        //    ed.WriteMessage("Access In AirPost= "+Atend.Control.Common.AccessPath);
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtAirPost.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_AirPost_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add("iCode", OleDbType.Integer).Value = dr["Code"].ToString();
        //        insertCommand.Parameters.Add("iName", OleDbType.VarChar).Value = dr["Name"].ToString();
        //        insertCommand.Parameters.Add("iProductCode", OleDbType.Integer).Value = dr["ProductCode"].ToString();
        //        insertCommand.Parameters.Add("iCapacity", OleDbType.Integer).Value = dr["Capacity"].ToString();
        //        insertCommand.Parameters.Add("iComment", OleDbType.VarChar).Value = dr["Comment"].ToString();
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //            //con.Close();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer AirPost: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool Autokey_3pTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtAutoKey3p = Atend.Base.Equipment.EAutoKey_3p.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtAutoKey3p.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_AutoKey_3p_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add("iCode", OleDbType.Integer).Value = dr["Code"].ToString();
        //        insertCommand.Parameters.Add("iName", OleDbType.VarChar).Value = dr["Name"].ToString();
        //        insertCommand.Parameters.Add("iComment", OleDbType.VarChar).Value = dr["Comment"].ToString();
        //        insertCommand.Parameters.Add("iAmper", OleDbType.Single).Value = dr["Amper"].ToString();
        //        insertCommand.Parameters.Add("iProductCode", OleDbType.Integer).Value = dr["ProductCode"].ToString();
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //            //con.Close();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer AutoKey3p: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //        //string queryString = "Insert into E_AirPost ()values()";

        //    }
        //    return true;

        //}

        //public static bool BreakerTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtBreaker = Atend.Base.Equipment.EBreaker.SelectAll();
        //    foreach (DataRow dr in dtBreaker.Rows)
        //    {
        //        OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //        OleDbCommand insertCommand = new OleDbCommand("E_Breaker_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iAmper", Convert.ToSingle(dr["Amper"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt16(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //            //con.Close();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Breaker: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool BusTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtBus = Atend.Base.Equipment.EBus.SelectAll();
        //    foreach (DataRow dr in dtBus.Rows)
        //    {
        //        OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //        OleDbCommand insertCommand = new OleDbCommand("E_Bus_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add("iCode", OleDbType.Integer).Value = dr["Code"].ToString();
        //        insertCommand.Parameters.Add("iProductCode", OleDbType.Integer).Value = dr["ProductCode"].ToString();
        //        insertCommand.Parameters.Add("iSize", OleDbType.Single).Value = dr["Size"].ToString();
        //        insertCommand.Parameters.Add("iType", OleDbType.SmallInt).Value = dr["Type"].ToString();
        //        insertCommand.Parameters.Add("iComment", OleDbType.VarChar).Value = dr["Comment"].ToString();
        //        insertCommand.Parameters.Add("iName", OleDbType.VarChar).Value = dr["Name"].ToString();
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //            //con.Close();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Bus: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool BreakerTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtBreakerType = Atend.Base.Equipment.EBreakerType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtBreakerType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_BreakerType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer BreakerType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //        //string queryString = "Insert into E_AirPost ()values()";

        //    }
        //    return true;

        //}

        //public static bool BusMaterialTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtBusMaterialType = Atend.Base.Equipment.EBusMaterialType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtBusMaterialType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_BusMaterialType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer BusMaterialType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }
        //    }
        //    return true;

        //}

        //public static bool BusTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtBusType = Atend.Base.Equipment.EBusType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtBusType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_BusType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer BusType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool CabelTypeTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtCabelType = Atend.Base.Equipment.ECabelType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtCabelType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_CabelType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer CabelType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool CatOutTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtCatOut = Atend.Base.Equipment.ECatOut.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtCatOut.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_CatOut_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add("iCode", OleDbType.Integer).Value = dr["Code"].ToString();
        //        insertCommand.Parameters.Add("iProductCode", OleDbType.Integer).Value = dr["ProductCode"].ToString();
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt16(dr["Type"].ToString())));
        //        insertCommand.Parameters.Add("iAmper", OleDbType.Single).Value = dr["Amper"].ToString();
        //        insertCommand.Parameters.Add("iVol", OleDbType.Single).Value = dr["Vol"].ToString();
        //        insertCommand.Parameters.Add("iComment", OleDbType.VarChar).Value = dr["Comment"].ToString();

        //        insertCommand.Parameters.Add("iName", OleDbType.VarChar).Value = dr["Name"].ToString();

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer CatOut: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool CatOutTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtCatOutType = Atend.Base.Equipment.ECatOutType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtCatOutType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_CatOutType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer CatOut: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool ConductorTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtConductor = Atend.Base.Equipment.EConductor.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtConductor.Rows)
        //    {


        //        OleDbCommand insertCommand = new OleDbCommand("E_Conductor_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;

        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iMaterialCode", Convert.ToInt32(dr["MaterialCode"])));



        //        insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", Convert.ToSingle(dr["crossSectionArea"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iDiagonal", Convert.ToSingle(dr["Diagonal"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iAlasticity", Convert.ToInt32(dr["Alasticity"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iWeight", Convert.ToInt32(dr["Weight"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iAlpha", Convert.ToSingle(dr["Alpha"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iUTS", Convert.ToInt32(dr["UTS"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iMaxCurrent", Convert.ToSingle(dr["MaxCurrent"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iResistance", Convert.ToSingle(dr["Resistance"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iReactance", Convert.ToSingle(dr["Reactance"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iDamperType", Convert.ToInt32(dr["DamperType"])));


        //        insertCommand.Parameters.Add(new OleDbParameter("iIsCabel", Convert.ToBoolean(dr["IsCabel"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iCabelTypeCode", Convert.ToInt32(dr["CabelTypeCode"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iGMR", Convert.ToSingle(dr["GMR"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));

        //        insertCommand.Parameters.Add(new OleDbParameter("iWC", Convert.ToSingle(dr["WC"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Conductor: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool ConductorDamperTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtconductorDamperType = Atend.Base.Equipment.EConductorDamperType.SelectAll();

        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtconductorDamperType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_ConductorDamperType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //            //con.Close();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer ConductorDamperType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool ConductorMaterialTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtconductorMaterialType = Atend.Base.Equipment.EConductorMaterialType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtconductorMaterialType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_ConductorMaterialType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer ConductorMaterialType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool ConsolTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtConsol = Atend.Base.Equipment.EConsol.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    Byte[] imageContent = new byte[0];
        //    ed.WriteMessage("Enter Consol\n");
        //    foreach (DataRow dr in dtConsol.Rows)
        //    {
        //        ed.WriteMessage("For Each\n");
        //        imageContent = (Byte[])(dr["Image"]);
        //        OleDbCommand insertCommand = new OleDbCommand("E_Consol_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["productCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iLenght", Convert.ToSingle(dr["Length"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add("iImage", OleDbType.Binary).Value = imageContent;
        //        insertCommand.Parameters.Add(new OleDbParameter("iDistancePhase", Convert.ToSingle(dr["DistancePhase"])));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Consol: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}


        //public static bool ContainerPackageTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtcontainerPackage = Atend.Base.Equipment.EContainerPackage.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtcontainerPackage.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_ContainerPackage_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iContainerCode", Convert.ToInt32(dr["ContainerCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (OleDbException ex)
        //        {
        //            ed.WriteMessage("Error In Transfer ContainerPackage: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool CountorTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtCountor = Atend.Base.Equipment.ECountor.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtCountor.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_Countor_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iAmper", Convert.ToSingle(dr["Amper"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("PhaseCount", Convert.ToInt32(dr["PhaseCount"].ToString())));
        //        insertCommand.Parameters.Add(new OleDbParameter("Comment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("Name", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (OleDbException ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Countor: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool CTTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtCT = Atend.Base.Equipment.ECT.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtCT.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_CT_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCT_Vol", Convert.ToSingle(dr["CT_Vol"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCT_Convert", Convert.ToInt32(dr["CT_Convert"].ToString())));
        //        insertCommand.Parameters.Add(new OleDbParameter("Comment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("Name", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (OleDbException ex)
        //        {
        //            ed.WriteMessage("Error In Transfer CT: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool DBTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtDB = Atend.Base.Equipment.EDB.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtDB.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_DB_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCountorCount", Convert.ToInt32(dr["CountorCount"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("Comment", dr["Comment"].ToString()));

        //        insertCommand.Parameters.Add(new OleDbParameter("Name", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (OleDbException ex)
        //        {
        //            ed.WriteMessage("Error In Transfer DB: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool DisconnectorTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtDisconnector = Atend.Base.Equipment.EDisconnector.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtDisconnector.Rows)
        //    {
        //        ed.WriteMessage("Name= " + dr["Name"].ToString() + "Coment =" + dr["Comment"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_disconnector_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iAmper", Convert.ToSingle(dr["Amper"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));

        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Disconnector: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool DisconnectorTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtDisconnectorType = Atend.Base.Equipment.EDisconnectorType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtDisconnectorType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_disconnectorType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer DisconnectorType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;
        //}


        //public static bool GroundPostTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtGroundPost = Atend.Base.Equipment.EGroundPost.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("befor ForEaech\n");
        //    foreach (DataRow dr in dtGroundPost.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_GroundPost_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iGroundType", Convert.ToByte(dr["GroundType"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToByte(dr["Type"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iAdvanceType", Convert.ToByte(dr["AdvanceType"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Convert.ToSingle(dr["Capacity"].ToString())));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCellCount", Convert.ToSByte(dr["CellCount"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        //insertCommand.Parameters.Add(new OleDbParameter("iOperationCode",Convert.ToInt32(dr["OperationCode"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer GroundPost: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool GroundPostCellTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtGroundPostCell = Atend.Base.Equipment.EGroundPostCell.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("befor ForEaech\n");
        //    foreach (DataRow dr in dtGroundPostCell.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_GroundPostCell_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iNum", Convert.ToInt32(dr["Num"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductPackageCode", Convert.ToInt32(dr["ProductPackageCode"])));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer GroundPostCell: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool HeaderCabelTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtHeaderCabel = Atend.Base.Equipment.EHeaderCabel.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("befor ForEaech\n");
        //    foreach (DataRow dr in dtHeaderCabel.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_HeaderCabel_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iproductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", Convert.ToSingle(dr["CrossSectionArea"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", Convert.ToSingle(dr["CrossSectionArea"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iVol", Convert.ToSingle(dr["Vol"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer HeaderCabel: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool HeaderCabelMaterialTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtHeaderCabelMaterial = Atend.Base.Equipment.EHeaderCabelMaterial.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtHeaderCabelMaterial.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_HeaderCabelMaterial_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer HeaderCabelMaterial: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool HeaderCabelTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtHeaderCabelType = Atend.Base.Equipment.EHeaderCabelType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtHeaderCabelType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_HeaderCabelType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer HeaderCabelType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool InsulatorTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtInsulator = Atend.Base.Equipment.EInsulator.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("bE");
        //    foreach (DataRow dr in dtInsulator.Rows)
        //    {
        //        ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_Insulator_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iMaterialCode", Convert.ToInt32(dr["MaterialCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iVolt", Convert.ToSingle(dr["Volt"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iLenghtInsulatorChain", Convert.ToSingle(dr["LenghtInsulatorChain"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }

        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In TransferInsulator: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool InsulatorMaterialTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtInsulatorMaterial = Atend.Base.Equipment.EInsulatorType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("bE");
        //    foreach (DataRow dr in dtInsulatorMaterial.Rows)
        //    {
        //        ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_InsulatorMaterial_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }

        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In TransferInsulatorMaterial: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool JackPanelTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtJackPanel = Atend.Base.Equipment.EJAckPanel.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("bE");
        //    foreach (DataRow dr in dtJackPanel.Rows)
        //    {
        //        ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_JackPanel_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        insertCommand.Parameters.Add(new OleDbParameter("iMasterProductCode", Convert.ToInt32(dr["MasterProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iMasterProductType", dr["MasterProductType"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCellcount", Convert.ToInt32(dr["CellCount"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iIs20kv", Convert.ToByte(dr["Is20kv"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("Comment", dr["Comment"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }

        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In TransferInsulator: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool JackPanelCellTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtJackPanelCell = Atend.Base.Equipment.EJackPanelCell.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("bE");
        //    foreach (DataRow dr in dtJackPanelCell.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_JackPanelCell_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iJackPanelCode", Convert.ToInt32(dr["JackPanelCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCellNum", Convert.ToByte(dr["CellNum"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductType", Convert.ToInt32(dr["ProductType"])));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }

        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In TransferJackPanelCell: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool JumperTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtJumper = Atend.Base.Equipment.EJumper.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("bE");
        //    foreach (DataRow dr in dtJumper.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_Jumper_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        insertCommand.Parameters.Add(new OleDbParameter("iMaterialCode", Convert.ToInt32(dr["MaterialCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", Convert.ToSingle(dr["CrossSectionArea"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["ProductCode"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }

        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Jumper: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool KhazanTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtKhazan = Atend.Base.Equipment.EKhazan.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("bE");
        //    foreach (DataRow dr in dtKhazan.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_Khazan_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iMaker", dr["Maker"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Convert.ToInt32(dr["Capacity"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iVol", Convert.ToSingle(dr["Vol"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }

        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Khazan: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool MafsalTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtMafsal = Atend.Base.Equipment.EMafsal.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("bE");
        //    foreach (DataRow dr in dtMafsal.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_Mafsal_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", Convert.ToSingle(dr["CrossSectionArea"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }

        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Mafsal: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool MafsalTypeTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtMafsalType = Atend.Base.Equipment.EMafsalType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtMafsalType.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_MafsalType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer MafsalType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool MiddleGroundPostTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtMiddleGroundCabel = Atend.Base.Equipment.EMiddleGroundCabel.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtMiddleGroundCabel.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_MiddleGroundCabel_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iNumString", Convert.ToInt32(dr["NumString"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", Convert.ToSingle(dr["CrossSectionArea"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iVol", Convert.ToSingle(dr["Vol"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", Convert.ToInt32(dr["Code"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer MiddleGroundCabel: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool OperationTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtOperation = Atend.Base.Equipment.EOperation.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtOperation.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_Operation_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", dr["Code"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductID", Convert.ToInt32(dr["ProductID"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Operation: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool PackageTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtPackage = Atend.Base.Equipment.EPackage.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtPackage.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_Package_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", dr["Code"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCount", Convert.ToInt32(dr["Count"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iContainerCode", Convert.ToInt32(dr["ContainerCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iIsContainer", Convert.ToInt32(dr["IsContainer"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Package: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool PackageAllTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtPackageAll = Atend.Base.Equipment.EPackageAll.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtPackageAll.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_PackageAll_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", dr["Code"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iContainerCode", Convert.ToInt32(dr["ContainerCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iContainerType", Convert.ToInt32(dr["ContainerType"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductID", Convert.ToInt32(dr["ProductID"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductType", Convert.ToInt32(dr["ProductType"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCount", Convert.ToInt32(dr["Count"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iNodeCode", dr["NodeCode"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iIsContainer", Convert.ToInt32(dr["IsContainer"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Package: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool PhotoCellTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtPhotoCell = Atend.Base.Equipment.EPhotoCell.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtPhotoCell.Rows)
        //    {
        //        OleDbCommand insertCommand = new OleDbCommand("E_PhotoCell_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", dr["Code"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer PhotoCell: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool PhuseTransfer()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtPhuse = Atend.Base.Equipment.EPhuse.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    foreach (DataRow dr in dtPhuse.Rows)
        //    {
        //        ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_Phuse_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iAmper", Convert.ToSingle(dr["Amper"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iPhusepoleCode", Convert.ToInt32(dr["PhusePoleCode"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Phuse: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool PhuseKeyTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("Enter In PhuseKeyTransfer\n");
        //    DataTable dtPhuseKey = Atend.Base.Equipment.EPhuseKey.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("Count= " + dtPhuseKey.Rows.Count.ToString() + "\n");
        //    foreach (DataRow dr in dtPhuseKey.Rows)
        //    {
        //        ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_PhuseKey_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iAmper", Convert.ToSingle(dr["Amper"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iMark", dr["Mark"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iIsPhuseKey", Convert.ToBoolean(dr["IsPhuseKey"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer PhuseKey: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool PhusePoleTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("Enter In PhuseKeyTransfer\n");
        //    DataTable dtPhusePole = Atend.Base.Equipment.EPhusePole.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("Count= " + dtPhusePole.Rows.Count.ToString() + "\n");
        //    foreach (DataRow dr in dtPhusePole.Rows)
        //    {
        //        ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_PhusePole_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));

        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iAmper", Convert.ToSingle(dr["Amper"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iMark", dr["Mark"].ToString()));


        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer PhusePole: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool PhuseTypeTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtPhuseType = Atend.Base.Equipment.EPhuseType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    ed.WriteMessage("Count= " + dtPhuseType.Rows.Count.ToString() + "\n");
        //    foreach (DataRow dr in dtPhuseType.Rows)
        //    {
        //        ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_PhuseType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer PhuseType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool PoleTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtPole = Atend.Base.Equipment.EPole.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtPole.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_Pole_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iHeight", Convert.ToSingle(dr["Height"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iPower", Convert.ToSingle(dr["Power"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iTopCrossSectionArea", Convert.ToSingle(dr["TopCrossSectionArea"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iBottomCrossSectionArea", Convert.ToSingle(dr["BottomCrossSectionArea"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Pole: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool PostTransfer()
        //{

        //    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //DataTable dtPost = Atend.Base.Equipment.EPost.SelectAll();
        //    //OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    //foreach (DataRow dr in dtPost.Rows)
        //    //{
        //    //    //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //    //    OleDbCommand insertCommand = new OleDbCommand("E_Post_Insert", con);
        //    //    insertCommand.CommandType = CommandType.StoredProcedure;
        //    //    insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //    //    insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //    //    insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //    //    insertCommand.Parameters.Add(new OleDbParameter("iCount", Convert.ToInt32(dr["Count"])));
        //    //    insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Convert.ToSingle(dr["Capacity"])));
        //    //    insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));

        //    //    insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));

        //    //    try
        //    //    {
        //    //        if (con.State == ConnectionState.Closed)
        //    //        {
        //    //            con.Open();
        //    //        }
        //    //        insertCommand.ExecuteNonQuery();
        //    //    }
        //    //    catch (System.Exception ex)
        //    //    {
        //    //        ed.WriteMessage("Error In Transfer Post: " + ex.Message + "\n");
        //    //        con.Close();
        //    //        return false;
        //    //    }

        //    //}
        //    return true;
        //}

        //public static bool ProductPackageTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtProductPackage = Atend.Base.Equipment.EProductPackage.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtProductPackage.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_ProductPackage_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iContainerPackageCode", Convert.ToInt32(dr["ContainerPackageCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCount", Convert.ToInt32(dr["Count"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iTableType", Convert.ToInt32(dr["TableType"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer ProductPackage: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool PTTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtPT = Atend.Base.Equipment.EPT.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtPT.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_PT_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iPT_Vol", Convert.ToSingle(dr["PT_Vol"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iPT_Convert", Convert.ToSingle(dr["PT_Convert"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer PT: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool ReCloserTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtReCloser = Atend.Base.Equipment.EReCloser.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtReCloser.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_ReCloser_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iAmper", Convert.ToSingle(dr["Amper"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer ReCloser: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool ReCloserTypeTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtReCloserType = Atend.Base.Equipment.EReCloserType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtReCloserType.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_ReCloserType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer ReCloserType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool RodTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtRod = Atend.Base.Equipment.ERod.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtRod.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_Rod_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iVol", Convert.ToSingle(dr["Vol"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iAmper", Convert.ToSingle(dr["Amper"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToInt32(dr["Type"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer ReCloser: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool SectionLizerTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtSectionLizer = Atend.Base.Equipment.ESectionLizer.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtSectionLizer.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_SectionLizer_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iAmper", Convert.ToSingle(dr["Amper"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iVolt", Convert.ToSingle(dr["Volt"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer SectionLizer: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

        //public static bool StreetBoxTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtStreetBox = Atend.Base.Equipment.EStreetBox.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtStreetBox.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_StreetBox_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iInputCount", Convert.ToInt32(dr["InputCount"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer StreetBox: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool TowerTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtTower = Atend.Base.Equipment.ETower.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtTower.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_Tower_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iHeight", Convert.ToSingle(dr["Height"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Tower: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool TransformerTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtTransformer = Atend.Base.Equipment.ETransformer.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtTransformer.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_Transformer_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Convert.ToSingle(dr["Capacity"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iVolt", Convert.ToInt32(dr["Volt"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
        //        insertCommand.Parameters.Add(new OleDbParameter("iTypeCode", Convert.ToSingle(dr["TypeCode"])));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer Transformer: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}
        //public static bool TransformerTypeTransfer()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable dtTransformerType = Atend.Base.Equipment.ETransformerType.SelectAll();
        //    OleDbConnection con = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    foreach (DataRow dr in dtTransformerType.Rows)
        //    {
        //        //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
        //        OleDbCommand insertCommand = new OleDbCommand("E_TransformerType_Insert", con);
        //        insertCommand.CommandType = CommandType.StoredProcedure;
        //        insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
        //        insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));

        //        try
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("Error In Transfer TransformerType: " + ex.Message + "\n");
        //            con.Close();
        //            return false;
        //        }

        //    }
        //    return true;

        //}

    }
}
