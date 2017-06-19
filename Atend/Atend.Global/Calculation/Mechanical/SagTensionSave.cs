using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.EditorInput;
using System.Data;
using System.Data.OleDb;


namespace Atend.Global.Calculation.Mechanical
{
    public class SagTensionSave
    {

        private Atend.Base.Calculating.CDefaultMec defMec;

        public Atend.Base.Calculating.CDefaultMec DefMec
        {
            get { return defMec; }
            set { defMec = value; }
        }

        Guid sectionCode;

        public Guid SectionCode
        {
            get { return sectionCode; }
            set { sectionCode = value; }
        }

        bool isUTS;

        public bool IsUTS
        {
            get { return isUTS; }
            set { isUTS = value; }
        }

        System.Data.DataTable dtsagTension;

        public System.Data.DataTable DtsagTension
        {
            get { return dtsagTension; }
            set { dtsagTension = value; }
        }

        System.Data.DataTable dtConductorDay;

        public System.Data.DataTable DtConductorDay
        {
            get { return dtConductorDay; }
            set { dtConductorDay = value; }
        }

        System.Data.DataTable dtPole;

        public System.Data.DataTable DtPole
        {
            get { return dtPole; }
            set { dtPole = value; }
        }


        System.Data.DataTable dtHMAX;

        public System.Data.DataTable DtHMAX
        {
            get { return dtHMAX; }
            set { dtHMAX = value; }
        }


        int end;

        public int End
        {
            get { return end; }
            set { end = value; }
        }

        int start;

        public int Start
        {
            get { return start; }
            set { start = value; }
        }

        int distance;

        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        DataRow[] drDefault;

        public DataRow[] DrDefault
        {
            get { return drDefault; }
            set { drDefault = value; }
        }

        double volt;

        public double Volt
        {
            get { return volt; }
            set { volt = value; }
        }

        double se;

        public double Se
        {
            get { return se; }
            set { se = value; }
        }


        double trustBorder;

        public double TrustBorder
        {
            get { return trustBorder; }
            set { trustBorder = value; }
        }

        public bool SaveData()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction aTransaction;
            try
            {
                aConnection.Open();
                aTransaction = aConnection.BeginTransaction();
                try
                {
                    if (IsUTS)
                    {
                        if (defMec.Code != 0)
                        {
                            //ed.WriteMessage("UTS={0}\n", drDefault[0]["UTS"].ToString());
                            defMec.Uts = Convert.ToDouble(DrDefault[0]["UTS"].ToString());
                            defMec.NetCross = Convert.ToInt32(DrDefault[0]["NetCrossCode"].ToString());
                            defMec.SE = Se;
                            if (!defMec.AccessUpdate(aConnection, aTransaction))
                            {
                                throw new System.Exception("failed defMec.AccessUpdate");
                            }
                        }
                        else
                        {
                            //ed.WriteMessage("NETCROSSCODE={0}\n", drDefault[0]["NetCrossCode"].ToString());
                            defMec.NetCross = Convert.ToInt32(DrDefault[0]["NetCrossCode"].ToString());
                            defMec.Uts = Convert.ToDouble(DrDefault[0]["UTS"].ToString());
                            defMec.IsUTS = true;
                            defMec.SectionCode = SectionCode;
                            defMec.TrustBorder = 0;
                            defMec.Vol = Convert.ToDouble(volt);
                            defMec.SE = Se;
                            if (!defMec.AccessInsert(aConnection, aTransaction))
                            {
                                throw new System.Exception("failed defMec.AccessInsert");
                            }
                        }
                    }
                    else
                    {
                        if (defMec.Code != 0)
                        {
                            defMec.TrustBorder = TrustBorder;
                            defMec.NetCross = Convert.ToInt32(drDefault[0]["NetCrossCode"].ToString());
                            defMec.SE = se;
                            defMec.AccessUpdate();
                        }
                        else
                        {
                            //ed.WriteMessage("NETCROSSCODE={0}\n", drDefault[0]["NetCrossCode"].ToString());
                            defMec.NetCross = Convert.ToInt32(drDefault[0]["NetCrossCode"].ToString());
                            defMec.Uts = 0;
                            defMec.IsUTS = false;
                            defMec.SectionCode = SectionCode;
                            defMec.TrustBorder = TrustBorder;
                            defMec.Vol = Convert.ToDouble(volt);
                            defMec.SE = se;
                            defMec.AccessInsert();
                        }
                    }


                    DataTable dtsag = Atend.Base.Calculating.CSagAndTension.AccessSelectBySectionCode(SectionCode, IsUTS, aConnection, aTransaction);
                    //ed.WriteMessage("1\n");
                    if (dtsag.Rows.Count != 0)
                    {
                        if (!Atend.Base.Calculating.CSagAndTension.AccessDeleteBySectionCode(SectionCode, IsUTS, aConnection, aTransaction))
                        {
                            throw new System.Exception("Failed CSagAndTension.AccessDeleteBySectionCode");
                        }
                    }
                    //ed.WriteMessage("2\n");

                    foreach (DataRow dr in dtsag.Rows)
                    {
                        if (!Atend.Base.Calculating.CMaxTension.AccessDeleteBysagTensionCode(Convert.ToInt32(dr["Code"].ToString()),aTransaction,aConnection))
                        {
                            throw new System.Exception("Failed cMaxTension.AccessDelete");
                        }
                    }
                    //ed.WriteMessage("3\n");

                    System.Data.DataTable dtWind = Atend.Base.Calculating.CWindOnPole.AccessSelectBySectionCode(SectionCode, IsUTS,aConnection,aTransaction);

                    if (dtWind.Rows.Count != 0)
                    {
                        if (!Atend.Base.Calculating.CWindOnPole.AccessDeleteBySectionCode(SectionCode, IsUTS, aConnection, aTransaction))
                        {
                            throw new System.Exception("Failed WindOnPole.AccessDeleteBySectionCode");
                        }
                    }
                   //ed.WriteMessage("4\n");

                    System.Data.DataTable dtCondDay = Atend.Base.Calculating.CConductorDay.AccessSelectBySectionCode(SectionCode, IsUTS,aConnection,aTransaction);

                    if (dtCondDay.Rows.Count != 0)
                    {
                        if (!Atend.Base.Calculating.CConductorDay.AccessDeleteBySectionCode(SectionCode, IsUTS, aConnection, aTransaction))
                        {
                            throw new System.Exception("Failed ConductorDay.AccessDeleteBySectionCode");
                        }
                    }



                   // ed.WriteMessage("5\n");

                    int j = 0;
                    foreach (DataRow dr in DtsagTension.Rows)
                    {
                        Atend.Base.Calculating.CSagAndTension cSagTension = new Atend.Base.Calculating.CSagAndTension();
                        cSagTension.ConductorName = dr["ConductorName"].ToString();
                        cSagTension.IceF = Convert.ToDouble(dr["IceF"].ToString());
                        cSagTension.IceH = Convert.ToDouble(dr["IceH"].ToString());
                        cSagTension.MaxTempF = Convert.ToDouble(dr["MaxTempF"].ToString());
                        cSagTension.MaxTempH = Convert.ToDouble(dr["MaxTempH"].ToString());
                        cSagTension.MinTempF = Convert.ToDouble(dr["MinTempF"].ToString());
                        cSagTension.MinTempH = Convert.ToDouble(dr["MinTempH"].ToString());
                        cSagTension.NormF = Convert.ToDouble(dr["NormF"].ToString());
                        cSagTension.NormH = Convert.ToDouble(dr["NormH"].ToString());
                        cSagTension.SectionCode =SectionCode;
                        cSagTension.WindAndIceF = Convert.ToDouble(dr["WindAndIceF"].ToString());
                        cSagTension.WindAndIceH = Convert.ToDouble(dr["WindAndIceH"].ToString());
                        cSagTension.WindF = Convert.ToDouble(dr["WindF"].ToString());
                        cSagTension.WindH = Convert.ToDouble(dr["WindH"].ToString());
                        cSagTension.IsUTS = IsUTS;
                        cSagTension.MaxF = Convert.ToDouble(dr["MaxF"].ToString());
                        if (cSagTension.AccessInsert(aConnection, aTransaction))
                        {
                            ed.WriteMessage("J={0},cSagTension.Code={1}\n",j,cSagTension.Code);
                            Atend.Base.Calculating.CMaxTension MaxTension = new Atend.Base.Calculating.CMaxTension();
                            MaxTension.SagTensionCode = cSagTension.Code;
                            MaxTension.MaxNormH = Convert.ToDouble(dtHMAX.Rows[j]["NormH"].ToString());
                            MaxTension.MaxIceH = Convert.ToDouble(dtHMAX.Rows[j]["IceH"].ToString());
                            MaxTension.MaxWindH = Convert.ToDouble(dtHMAX.Rows[j]["WindH"].ToString());
                            MaxTension.MaxMaxTempH = Convert.ToDouble(dtHMAX.Rows[j]["MaxTempH"].ToString());
                            MaxTension.MaxMinTempH = Convert.ToDouble(dtHMAX.Rows[j]["MinTempH"].ToString());
                            MaxTension.MaxWindH = Convert.ToDouble(dtHMAX.Rows[j]["WindH"].ToString());
                            MaxTension.MaxWindAndIceH = Convert.ToDouble(DtHMAX.Rows[j]["WindIceH"].ToString());
                                if (!MaxTension.AccessInsert(aConnection,aTransaction))
                                {
                                    throw new System.Exception("failed MaxTension.AccessInsert");
                                }
                                j++;
                        }
                        else
                        {
                            throw new System.Exception("failed cSagTension.AccessInsert");

                        }
                    }
                    //ed.WriteMessage("6\n");

                    foreach (DataRow dr in DtConductorDay.Rows)
                    {
                        Atend.Base.Calculating.CConductorDay condDay = new Atend.Base.Calculating.CConductorDay();
                        condDay.SectionCode = SectionCode;
                        condDay.From = dr["From"].ToString();
                        condDay.To = dr["To"].ToString();
                        condDay.SpanLenght = Convert.ToDouble(dr["SpanLenght"].ToString());
                        condDay.ConductorName = dr["ConductorName"].ToString();
                        condDay.IsUTS = IsUTS;
                        if (!condDay.AccessInsert(aConnection,aTransaction))
                        {
                            throw new System.Exception("Failed condDay.AccessInsert"); ;
                        }

                        for (int i = End; i > Start; i = i - Distance)
                        {
                            Atend.Base.Calculating.CTemp cTemp = new Atend.Base.Calculating.CTemp();
                            //ed.WriteMessage("condday.Code={0}\n", condDay.Code);
                            cTemp.ConductorDayCode = condDay.Code;
                            cTemp.Temp = i;
                            cTemp.Sag = Convert.ToDouble(dr[i.ToString() + "F"].ToString());
                            cTemp.Tension = Convert.ToDouble(dr[i.ToString()].ToString());
                            if (!cTemp.AccessInsert(aConnection,aTransaction))
                            {
                                throw new System.Exception("Failed cTemp.AccessInsert");
                            }
                        }
                    }

                    ed.WriteMessage("7\n");

                    foreach (DataRow dr in DtPole.Rows)
                    {
                        Atend.Base.Calculating.CWindOnPole windPole = new Atend.Base.Calculating.CWindOnPole();
                        windPole.DcIceHeavy = Convert.ToDouble(dr["DcIceHeavy"].ToString());
                        windPole.DcIsUTS = IsUTS;
                        windPole.DcMaxTemp = Convert.ToDouble(dr["DcMaxTemp"].ToString());
                        windPole.DcMinTemp = Convert.ToDouble(dr["DcMinTemp"].ToString());
                        windPole.DcNorm = Convert.ToDouble(dr["DcNorm"].ToString());
                        windPole.DcPole = dr["DcPole"].ToString();
                        windPole.DcwindIce = Convert.ToDouble(dr["DcWindIce"].ToString());
                        windPole.DcWindSpeed = Convert.ToDouble(dr["DcWindSpeed"].ToString());
                        windPole.SectionCode = SectionCode;
                        windPole.DcPoleGuid = new Guid(dr["dcPoleGuid"].ToString());
                        windPole.DcAngle = Convert.ToDouble(dr["dcAngle"].ToString());
                        if (!windPole.AccessInsert(aConnection,aTransaction))
                        {
                            throw new System.Exception("Failed windPole.AccessInsert");
                        }

                    }


                }
                catch(System.Exception ex1)
                {
                     ed.WriteMessage("ERROR SavesagTensionData: 02 {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;
                }
            }
            catch(System.Exception ex1)
            {
                ed.WriteMessage("ERROR SaveSagTensionData: 01 {0} \n", ex1.Message);
                aConnection.Close();
                return false;
            }
            ed.WriteMessage("###SAVE Mechanical DATA\n");
            aTransaction.Commit();
            aConnection.Close();
            return true;
        }

    }
}
