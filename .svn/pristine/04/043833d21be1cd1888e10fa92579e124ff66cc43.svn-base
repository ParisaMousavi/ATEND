using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Global.Calculation.Electrical
{
    public class CElectricalCrossSectionChane
    {
        List<Atend.Base.Design.DBranch> dBranch;

        public List<Atend.Base.Design.DBranch> DBranch
        {
            get { return dBranch; }
            set { dBranch = value; }
        }



        List<Atend.Base.Equipment.EConductorTip> condTip;

        public List<Atend.Base.Equipment.EConductorTip> CondTip
        {
            get { return condTip; }
            set { condTip = value; }
        }


        List<Atend.Base.Equipment.ESelfKeeperTip> selfTip;

        public List<Atend.Base.Equipment.ESelfKeeperTip> SelfTip
        {
            get { return selfTip; }
            set { selfTip = value; }
        }

        List<Atend.Base.Equipment.EGroundCabelTip> groundTip;

        public List<Atend.Base.Equipment.EGroundCabelTip> GroundTip
        {
            get { return groundTip; }
            set { groundTip = value; }
        }

        public CElectricalCrossSectionChane()
        {
            DBranch = new List<Atend.Base.Design.DBranch>();
            CondTip = new List<Atend.Base.Equipment.EConductorTip>();
            SelfTip = new List<Atend.Base.Equipment.ESelfKeeperTip>();
            GroundTip = new List<Atend.Base.Equipment.EGroundCabelTip>();
        }

        public bool ChangeBranchInfo()
        {
            OleDbConnection conection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction transaction;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                conection.Open();
                transaction = conection.BeginTransaction();
                try
                {
                    int ConductorCounter = 0;
                    int SelfkeeperCounter = 0;
                    int GroundCabelCounter = 0;

                    foreach (Atend.Base.Design.DBranch branch in dBranch)
                    {

                        ObjectId obj = Atend.Global.Acad.UAcad.GetBranchByGuid(branch.Code);
                        Atend.Base.Acad.AT_INFO atInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);

                        #region Conductor
                        if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                        {
                            Atend.Base.Equipment.EConductor CondPhase = Atend.Base.Equipment.EConductor.SelectByXCode(CondTip[ConductorCounter].PhaseProductXCode);
                            Atend.Base.Equipment.EConductor CondNeutral = Atend.Base.Equipment.EConductor.SelectByXCode(CondTip[ConductorCounter].NeutralProductXCode);
                            Atend.Base.Equipment.EConductor CondNight = Atend.Base.Equipment.EConductor.SelectByXCode(CondTip[ConductorCounter].NightProductXCode);

                            if (!CondPhase.AccessInsert(transaction, conection, true, true))
                            {
                                throw new System.Exception("Insert Failed");
                            }

                            if (!CondNeutral.AccessInsert(transaction, conection, true, true))
                            {
                                throw new System.Exception("Insert Failed");
                            }
                            if (!CondNight.AccessInsert(transaction, conection, true, true))
                            {
                            }
                            CondTip[ConductorCounter].PhaseProductCode = CondPhase.Code;
                            CondTip[ConductorCounter].NeutralProductCode = CondNeutral.Code;
                            CondTip[ConductorCounter].NightProductCode = CondNight.Code;

                            if (!CondTip[ConductorCounter].AccessInsert(transaction, conection, true, true))
                            {
                                throw new System.Exception("Insert Tip Failed");
                            }

                            branch.ProductCode = CondTip[ConductorCounter].Code;
                            branch.Number = CondTip[ConductorCounter].Description;
                            atInfo.ProductCode = condTip[ConductorCounter].Code;
                            if (branch.AccessUpdate(transaction, conection))
                            {
                                Atend.Global.Acad.DrawEquips.AcDrawConductor.ChangeCounductorComment(obj, CondTip[ConductorCounter].Description);
                                atInfo.Insert();
                            }
                            else
                            {
                                throw new System.Exception("Insert Branch Failed");

                            }
                            ConductorCounter++;
                        }
                        #endregion


                        #region SelfKeeper
                        if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
                        {
                            Atend.Base.Equipment.ESelfKeeper SelfPhase = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(SelfTip[SelfkeeperCounter].PhaseProductxCode);
                            Atend.Base.Equipment.ESelfKeeper SelfNeutral = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(SelfTip[SelfkeeperCounter].NeutralProductxCode);
                            Atend.Base.Equipment.ESelfKeeper SelfNight = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(SelfTip[SelfkeeperCounter].NightProductxCode);

                            if (!SelfPhase.AccessInsert(transaction, conection, true, true))
                            {
                                throw new System.Exception("Insert Failed");
                            }

                            if (!SelfNeutral.AccessInsert(transaction, conection, true, true))
                            {
                                throw new System.Exception("Insert Failed");
                            }
                            if (!SelfNight.AccessInsert(transaction, conection, true, true))
                            {
                                throw new System.Exception("Insert Failed");

                            }
                            SelfTip[SelfkeeperCounter].PhaseProductCode = SelfPhase.Code;
                            SelfTip[SelfkeeperCounter].NeutralProductCode = SelfNeutral.Code;
                            SelfTip[SelfkeeperCounter].NightProductCode = SelfNight.Code;

                            if (!SelfTip[SelfkeeperCounter].AccessInsert(transaction, conection))
                            {
                                throw new System.Exception("Insert Tip Failed");
                            }

                            branch.ProductCode = SelfTip[SelfkeeperCounter].Code;
                            branch.Number = SelfTip[SelfkeeperCounter].Description;
                            atInfo.ProductCode = SelfTip[SelfkeeperCounter].Code;

                            if (branch.AccessUpdate(transaction, conection))
                            {
                                Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper.ChangeCabelComment(obj, SelfTip[SelfkeeperCounter].Description);
                                atInfo.Insert();
                            }
                            else
                            {
                                throw new System.Exception("Insert Branch Failed");

                            }

                            SelfkeeperCounter++;
                        }
                        #endregion


                        #region GroundCabel
                        if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel))
                        {
                            Atend.Base.Equipment.EGroundCabel GroundPhase = Atend.Base.Equipment.EGroundCabel.SelectByXCode(GroundTip[GroundCabelCounter].PhaseProductXCode);
                            Atend.Base.Equipment.EGroundCabel GroundNeutral = Atend.Base.Equipment.EGroundCabel.SelectByXCode(GroundTip[GroundCabelCounter].NeutralProductXCode);
                            //Atend.Base.Equipment.EGroundCabel GroundNight = Atend.Base.Equipment.EGroundCabel.SelectByXCode(GroundTip[GroundCabelCounter].NightProductxCode);

                            if (!GroundPhase.AccessInsert(transaction, conection, true, true))
                            {
                                throw new System.Exception("Insert Failed");
                            }

                            if (!GroundNeutral.AccessInsert(transaction, conection, true, true))
                            {
                                throw new System.Exception("Insert Failed");
                            }
                            //if (!GroundNight.AccessInsert(transaction, conection, true, true))
                            //{
                            //    throw new System.Exception("Insert Failed");

                            //}
                            GroundTip[GroundCabelCounter].PhaseProductCode = GroundPhase.Code;
                            GroundTip[GroundCabelCounter].NeutralProductCode = GroundNeutral.Code;
                            //GroundTip[GroundCabelCounter].NightProductCode = GroundNight.Code;

                            if (!GroundTip[GroundCabelCounter].AccessInsert(transaction, conection, true, true))
                            {
                                throw new System.Exception("Insert Tip Failed");
                            }

                            branch.ProductCode = GroundTip[GroundCabelCounter].Code;
                            branch.Number = GroundTip[GroundCabelCounter].Description;
                            atInfo.ProductCode = GroundTip[GroundCabelCounter].Code;
                            if (branch.AccessUpdate(transaction, conection))
                            {
                                Atend.Global.Acad.DrawEquips.AcDrawGroundCabel.ChangeCabelComment(obj, GroundTip[GroundCabelCounter].Description);
                                atInfo.Insert();
                            }
                            else
                            {
                                throw new System.Exception("Insert Branch Failed");
                            }
                            GroundCabelCounter++;
                        }
                        #endregion
                    }
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("Error In 01={0}\n", ex.Message);
                    transaction.Rollback();
                    conection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("Error in 02={0}\n", ex1.Message);
                conection.Close();
                return false;
            }
            transaction.Commit();
            conection.Close();
            return true;
        }
    }
}
