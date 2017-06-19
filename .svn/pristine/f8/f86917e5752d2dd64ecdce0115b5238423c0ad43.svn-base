using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;


//get from tehran 7/15
namespace Atend.Global.Acad
{
    public class AcadRemove
    {

        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //public static bool DeleteConsol(ObjectId PoleOI, String ConsolCode)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //ed.writeMessage("~~~~~~~~~~~Start Graphic Delete Consol ~~~~~~~~~~~~~~~\n");
        //    try
        //    {
        //        Atend.Base.Acad.AT_SUB poleSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(PoleOI);
        //        foreach (ObjectId oi in poleSub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO ConsolInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //            if (ConsolInfo.ParentCode != "NONE" && ConsolInfo.NodeType == (int)Atend.Control.Enum.ProductType.Consol && ConsolInfo.NodeCode == ConsolCode)
        //            {
        //                Atend.Base.Acad.AT_SUB ConsolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oii in ConsolSub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO ConsolSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);
        //                    if (ConsolSubInfo.ParentCode != "NONE" && ConsolSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //                    {
        //                        ObjectId NextConsolOI = UAcad.GetNextConsol(oi, new Guid(ConsolSubInfo.NodeCode));
        //                        //ed.writeMessage("Graphic Next Consol OI:{0}\n", NextConsolOI);

        //                        Atend.Base.Acad.AT_SUB NextConsolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(NextConsolOI);
        //                        foreach (ObjectId NextConsolSubOI in NextConsolSub.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO NextConsolSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(NextConsolSubOI);
        //                            if (NextConsolSubInfo.ParentCode != "NONE" && NextConsolSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.TensionArc)
        //                            {
        //                                //ed.writeMessage("Graphic YES IT HAS ARC \n");
        //                                if (!DeleteEntityByObjectId(NextConsolSubOI))
        //                                {
        //                                    throw new System.Exception("While delete arc from next consol \n");
        //                                }
        //                                else
        //                                {
        //                                    Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(NextConsolSubOI, NextConsolOI);
        //                                }
        //                            }
        //                        }
        //                    }

        //                }
        //                foreach (ObjectId oii in ConsolSub.SubIdCollection)
        //                {

        //                    Atend.Base.Acad.AT_INFO ConsolSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);
        //                    if (ConsolSubInfo.ParentCode != "NONE" && ConsolSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //                    {
        //                        DeleteConductor(oii);
        //                    }
        //                    if (ConsolSubInfo.ParentCode != "NONE" && ConsolSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.TensionArc)
        //                    {
        //                        if (!DeleteConductor(oii))
        //                        {
        //                            throw new System.Exception("While delete tension arc\n");
        //                        }
        //                    }

        //                }
        //                if (!DeleteEntityByObjectId(oi))
        //                {
        //                    throw new System.Exception("while delete consol \n");
        //                }
        //                else
        //                {
        //                    //ed.writeMessage("**************AT_SUB.RemoveFromAT_SUB\n");
        //                    Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, PoleOI);
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //ed.WriteMessage("~~~~~~~~~~~End Graphic Delete Consol~~~~~~~~~~~~~~~\n");
        //        ed.WriteMessage("Graphic ERROR DeLeteConsol :" + ex.Message + "\n");
        //        return false;
        //    }
        //    //ed.writeMessage("~~~~~~~~~~~End Graphic Delete Consol~~~~~~~~~~~~~~~\n");
        //    return true;
        //}

        //private static bool DeleteConductor(ObjectId ConductorOI)
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //ed.WriteMessage("~~~~~~~~~~~Start Counductor From Graphic~~~~~~~~~~~~~~~\n");
        //    try
        //    {
        //        Atend.Base.Acad.AT_SUB CounductorSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(ConductorOI);
        //        foreach (ObjectId CounductorSubOI in CounductorSub.SubIdCollection)
        //        {
        //            //ed.WriteMessage("GRA delete conductor sub\n");
        //            Atend.Base.Acad.AT_INFO CounductorSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(CounductorSubOI);
        //            if (CounductorSubInfo.ParentCode != "NONE" && CounductorSubInfo.NodeType != (int)Atend.Control.Enum.ProductType.Consol)
        //            {
        //                if (!DeleteEntityByObjectId(CounductorSubOI))
        //                {
        //                    throw new System.Exception("GRA while delete conductor sub\n");
        //                }
        //            }
        //        }
        //        //ed.WriteMessage("GRA delete conductor\n");
        //        if (!DeleteEntityByObjectId(ConductorOI))
        //        {
        //            throw new System.Exception("GRA while delete conductor \n");
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("GRA ERROR CONDUCTOR : {0} \n", ex.Message);
        //        return false;
        //    }
        //    return true;
        //}

        public static bool DeleteEntityByObjectId(ObjectId oi)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //Database db = HostApplicationServices.WorkingDatabase;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            //ed.WriteMessage("Start Delete {0}\n", oi);
            try
            {
                using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {

                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        //ed.WriteMessage("****12\n");
                        Entity ent = tr.GetObject(oi, OpenMode.ForWrite, true) as Entity;
                        if (ent != null)
                        {
                            //ed.WriteMessage("****13\n");

                            ////////////////Atend.Base.Acad.AT_SUB entitySub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(ent.ObjectId);

                            ////////////////foreach (ObjectId oii in entitySub.SubIdCollection)
                            ////////////////{
                            ////////////////    Atend.Base.Acad.AT_INFO SubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);
                            ////////////////    if (SubInfo.ParentCode != "NONE" && SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                            ////////////////    {
                            ////////////////        Entity ent1 = tr.GetObject(oii, OpenMode.ForWrite, true) as Entity;
                            ////////////////        if (ent1 != null)
                            ////////////////        {
                            ////////////////            ent1.Erase(true);
                            ////////////////        }
                            ////////////////    }
                            ////////////////}

                            ent.Erase(true);
                        }
                        tr.Commit();
                        //ed.WriteMessage("****14\n");
                    }
                }
            }
            catch(System.Exception ex)
            {
                ed.WriteMessage("End Delete : {0}\n",ex.Message);
                return false;
            }
            
            return true;
        }

        public static bool DeleteGroundPost(ObjectId GroundPostOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            bool AllObjectsDeleted = true;
            Atend.Base.Acad.AT_SUB GroundPostSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(GroundPostOI);
            try
            {
                foreach (ObjectId oi in GroundPostSub.SubIdCollection)
                {


                    ObjectIdCollection GroupSubs = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi);
                    if (GroupSubs.Count != 0)
                    {
                        foreach (ObjectId oii in GroupSubs)
                        {
                            if (!DeleteEntityByObjectId(oii))
                                AllObjectsDeleted = false;
                        }
                        if (!DeleteEntityByObjectId(oi))
                            AllObjectsDeleted = false;
                    }
                    else
                    {
                        if (!DeleteEntityByObjectId(oi))
                            AllObjectsDeleted = false;

                    }


                }
                if (!DeleteEntityByObjectId(GroundPostOI))
                    AllObjectsDeleted = false;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error GroundPost Graphical Delete:{0}\n", ex.Message);
            }
            return AllObjectsDeleted;
        }

        public static bool DeleteCollection(ObjectId[] Collection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ObjectIdCollection newCollection = new ObjectIdCollection();
            foreach (ObjectId c in Collection)
            {
                switch ((Atend.Control.Enum.ProductType)Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(c).NodeType)
                {
                    case Atend.Control.Enum.ProductType.Pole:
                        newCollection.Add(c);
                        break;
                    case Atend.Control.Enum.ProductType.GroundPost:
                        newCollection.Add(c);
                        break;
                    case Atend.Control.Enum.ProductType.AirPost:
                        newCollection.Add(c);
                        break;
                }
            }
            foreach (ObjectId c in Collection)
            {
                bool sw = true;
                foreach (ObjectId cSub in newCollection)
                {
                    if (cSub == c)
                        sw = false;
                }
                if (sw)
                    newCollection.Add(c);
            }

            foreach (ObjectId collect in newCollection)
            {
                Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                if (atinfo.ParentCode != "NONE")
                {
                    try
                    {
                        switch ((Atend.Control.Enum.ProductType)atinfo.NodeType)
                        {
                            case Atend.Control.Enum.ProductType.Comment:
                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
                                {
                                    throw new System.Exception("Error In Delete Comment\n");
                                }
                                break;
                            case Atend.Control.Enum.ProductType.Pole:
                                if (Atend.Global.Acad.DrawEquips.AcDrawPole.DeletePoleData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawPole.DeletePole(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawPole\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataPole\n");
                                }
                                break;
                            case Atend.Control.Enum.ProductType.PoleTip:
                                if (Atend.Global.Acad.DrawEquips.AcDrawPole.DeletePoleData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawPole.DeletePole(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawPoleTip\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataPoleTip\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Conductor:
                                if (Atend.Global.Acad.DrawEquips.AcDrawConductor.DeleteConductorData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawConductor.DeleteConductor(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawConductor\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataConductor\n");
                                }
                                break;
                            case Atend.Control.Enum.ProductType.GroundCabel:
                                if (Atend.Global.Acad.DrawEquips.AcDrawGroundCabel.DeleteGroundCabelData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawGroundCabel.DeleteGroundCabel(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawGroundCabel\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataGroundCabel\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.SelfKeeper:
                                if (Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper.DeleteSelfKeeperData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper.DeleteSelfKeeper(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawSelfKeeper\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataSelfKeeper\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Disconnector:
                                if (Atend.Global.Acad.DrawEquips.AcDrawDisConnector.DeleteDisconnectorData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawDisConnector.DeleteDisconnector(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawDisconnector\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataDisconnector\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Breaker:
                                if (Atend.Global.Acad.DrawEquips.AcDrawBreaker.DeleteBreakerData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawBreaker.DeleteBreaker(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawBreaker\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataBreaker\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.CatOut:
                                if (Atend.Global.Acad.DrawEquips.AcDrawCatOut.DeleteCatOutData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawCatOut.DeleteCatOut(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawCatOut\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataCatOut\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Jumper:
                                if (Atend.Global.Acad.DrawEquips.AcDrawJumper.DeleteJumperData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawJumper.DeleteJumper(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawJumper\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataJumper\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.ConsolElse:  //for Khazan And Rod
                                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(collect);
                                ObjectIdCollection _Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
                                Atend.Base.Acad.AT_INFO atinfo2 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_Collection[1]);
                                if (atinfo2.ParentCode != "NONE" && atinfo2.NodeType == (int)Atend.Control.Enum.ProductType.BankKhazan)
                                {
                                    if (Atend.Global.Acad.DrawEquips.AcDrawKhazan.DeleteKhazanData(collect))
                                    {
                                        if (!Atend.Global.Acad.DrawEquips.AcDrawKhazan.DeleteKhazan(collect))
                                        {
                                            ed.WriteMessage("Error in delete drawConsolElse\n");
                                        }
                                    }
                                    else
                                    {
                                        ed.WriteMessage("Error in delete dataConsolElse\n");
                                    }
                                }
                                else
                                {
                                    if (Atend.Global.Acad.DrawEquips.AcDrawRod.DeleteRodData(collect))
                                    {
                                        if (!Atend.Global.Acad.DrawEquips.AcDrawRod.DeleteRod(collect))
                                        {
                                            ed.WriteMessage("Error in delete drawConsolElse_2\n");
                                        }
                                    }
                                    else
                                    {
                                        ed.WriteMessage("Error in delete dataConsolElse_2\n");
                                    }
                                }
                                break;

                            case Atend.Control.Enum.ProductType.GroundPost:
                                if (Atend.Global.Acad.DrawEquips.AcDrawGroundPost.DeleteGroundPostData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawGroundPost.DeleteGroundPost(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawGroundPost\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataGroundPost\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.AirPost:
                                if (Atend.Global.Acad.DrawEquips.AcDrawAirPost.DeleteAirPostData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawAirPost.DeleteAirPost(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawAirPost\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataAirPost\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.HeaderCabel:
                                if (Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel.DeleteHeaderCabelData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel.DeleteHeaderCabel(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawHeaderCabel\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataHeaderCabel\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Kalamp:
                                if (Atend.Global.Acad.DrawEquips.AcDrawKalamp.DeleteKalampData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawKalamp.DeleteKalamp(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawKalamp\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataKalamp\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.KablSho:
                                if (Atend.Global.Acad.DrawEquips.AcDrawKablsho.DeleteKablshoData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawKablsho.DeleteKablsho(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawKablSho\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataKablSho\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.StreetBox:
                                if (Atend.Global.Acad.DrawEquips.AcDrawStreetBox.DeleteStreetBoxData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawStreetBox.DeleteStreetBox(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawStreetBox\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataStreetBox\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.DB:
                                if (Atend.Global.Acad.DrawEquips.AcDrawDB.DeleteDBData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawDB.DeleteDB(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawDB\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataDB\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Ground:
                                if (Atend.Global.Acad.DrawEquips.AcDrawGround.DeleteGroundData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawGround.DeleteGround(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawGround\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataGround\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Light:
                                if (Atend.Global.Acad.DrawEquips.AcDrawLight.DeleteLightData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawLight.DeleteLight(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawLight\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataLight\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Consol:
                                if (Atend.Global.Acad.DrawEquips.AcDrawConsol.DeleteConsolData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawConsol.DeleteConsol(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawConsol\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataConsol\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.MeasuredJackPanel:
                                if (Atend.Global.Acad.DrawEquips.AcDrawMeasuredJackPanel.DeleteMeasuredJackPanelData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawMeasuredJackPanel.DeleteMeasuredJackPanel(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawMeasuredJackPanel\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataMeasuredJackPanel\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Terminal:
                                if (Atend.Global.Acad.DrawEquips.AcDrawTerminal.DeleteTerminalData(collect))
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawTerminal.DeleteTerminal(collect))
                                    {
                                        ed.WriteMessage("Error in delete drawTerminal\n");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Error in delete dataTerminal\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.ForbiddenArea:
                                if (!Atend.Global.Acad.DrawEquips.AcDrawForbidenArea.DeleteForbiddenArea(collect))
                                {
                                    ed.WriteMessage("Error in delete drawForbiddenArea\n");
                                }
                                break;

                            case Atend.Control.Enum.ProductType.FrameInformation:
                                ObjectId idgroup = Atend.Global.Acad.UAcad.GetEntityGroup(collect);
                                ObjectIdCollection _CollectionFrame = Atend.Global.Acad.UAcad.GetGroupSubEntities(idgroup);
                                foreach (ObjectId frameoid in _CollectionFrame)
                                {
                                    if (!Atend.Global.Acad.DrawEquips.AcDrawFrame.DeleteFrame(frameoid))
                                    {
                                        ed.WriteMessage("Error in delete drawFramegroup\n");
                                    }
                                }

                                //Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(idgroup);
                                //foreach (ObjectId h in sub2.SubIdCollection)
                                //{
                                //    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
                                //    ed.WriteMessage("@@@@@@@@ :{0}\n", at_info20.NodeType);
                                //}

                                if (!Atend.Global.Acad.DrawEquips.AcDrawFrame.DeleteFrame(collect))
                                {
                                    ed.WriteMessage("Error in delete drawFrame\n");
                                }
                                break;


                        }
                    }
                    catch (System.Exception ex)
                    {
                        //ed.WriteMessage("error :{0}\n", ex.Message);
                    }
                }

            }
            return true;
        }

        

    }
}
