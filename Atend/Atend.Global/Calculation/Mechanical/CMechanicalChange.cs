using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Global.Calculation.Mechanical
{
    public class CMechanicalChange
    {
        private List<Atend.Base.Design.DPackage> dPackage;

        public List<Atend.Base.Design.DPackage> DPackage
        {
            get { return dPackage; }
            set { dPackage = value; }
        }

        private List<Atend.Base.Equipment.EPole> pole;

        public List<Atend.Base.Equipment.EPole> Pole
        {
            get { return pole; }
            set { pole = value; }
        }


        private List<Atend.Base.Design.DPoleInfo> poleInfo;

        public List<Atend.Base.Design.DPoleInfo> PoleInfo
        {
            get { return poleInfo; }
            set { poleInfo = value; }
        }


        private List<int> count;

        public List<int> Count
        {
            get { return count; }
            set { count = value; }
        }



        private List<Atend.Base.Equipment.EHalter> halter;

        public List<Atend.Base.Equipment.EHalter> Halter
        {
            get { return halter; }
            set { halter = value; }
        }

        private List<int> halterCount;

        public List<int> HalterCount
        {
            get { return halterCount; }
            set { halterCount = value; }
        }

        public CMechanicalChange()
        {
            DPackage = new List<Atend.Base.Design.DPackage>();
            Pole = new List<Atend.Base.Equipment.EPole>();
            PoleInfo = new List<Atend.Base.Design.DPoleInfo>();
            Count = new List<int>();
            Halter = new List<Atend.Base.Equipment.EHalter>();
            HalterCount = new List<int>();
        }

        public bool ChangePoleInfo()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                try
                {
                    int poleCounter = 0;
                    foreach (Atend.Base.Design.DPackage dpack in dPackage)
                    {

                        ObjectId obj = Atend.Global.Acad.UAcad.GetPoleByGuid(dpack.NodeCode);
                        Atend.Base.Acad.AT_INFO atInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);


                        if (!Pole[poleCounter].AccessInsert(transaction, connection, true, true))
                        {
                            throw new System.Exception("Insert Pole FAiled");
                        }

                        ed.WriteMessage("COUNT={0}\n",Count[poleCounter].ToString());
                        PoleInfo[poleCounter].Factor = Convert.ToByte(Count[poleCounter]);
                        if (!PoleInfo[poleCounter].AccessUpdate(transaction,connection))
                        {
                            throw new System.Exception("Insert PoleInfo Failed");
                        }

                        dpack.ProductCode = Pole[poleCounter].Code;
                        if (!dpack.AccessUpdate(transaction,connection))
                        {
                            throw new System.Exception("Insert Package Failed");
                        }
                        poleCounter++;
                    }
                }
                catch(System.Exception ex)
                {
                    ed.WriteMessage(string.Format("MechanicalChange Has Error={0}\n",ex.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch(System.Exception ex)
            {
                ed.WriteMessage(string.Format("error in Mechanical Change01={0}",ex.Message));
                return false;
            }

            transaction.Commit();
            connection.Close();
            return true;
        }


        public bool ChangePoleInfoWithHalter()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                try
                {
                    int poleCounter = 0;
                    foreach (Atend.Base.Design.DPackage dpack in dPackage)
                    {

                        ObjectId obj = Atend.Global.Acad.UAcad.GetPoleByGuid(dpack.NodeCode);
                        Atend.Base.Acad.AT_INFO atInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);


                        if (!Pole[poleCounter].AccessInsert(transaction, connection, true, true))
                        {
                            throw new System.Exception("Insert Pole FAiled");
                        }

                        if (!Halter[poleCounter].AccessInsert(transaction,connection,true,true))
                        {
                            throw new System.Exception("Insert Halter Failed");
                        }

                        ed.WriteMessage("COUNT={0}\n", Count[poleCounter].ToString());
                        PoleInfo[poleCounter].Factor = Convert.ToByte(Count[poleCounter]);
                        PoleInfo[poleCounter].HalterCount = HalterCount[poleCounter];
                        PoleInfo[poleCounter].HalterType = Halter[poleCounter].Code;

                        if (!PoleInfo[poleCounter].AccessUpdate(transaction, connection))
                        {
                            throw new System.Exception("Insert PoleInfo Failed");
                        }

                        dpack.ProductCode = Pole[poleCounter].Code;
                        if (!dpack.AccessUpdate(transaction, connection))
                        {
                            throw new System.Exception("Insert Package Failed");
                        }
                        poleCounter++;
                    }
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage(string.Format("MechanicalChange Has Error={0}\n", ex.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("error in Mechanical Change01={0}", ex.Message));
                return false;
            }

            transaction.Commit();
            connection.Close();
            return true;
        }
    }
}
