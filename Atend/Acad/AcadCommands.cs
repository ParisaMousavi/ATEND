using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.BoundaryRepresentation;
using Autodesk.AutoCAD.Runtime;


[assembly: CommandClass(typeof(Atend.Acad.AcadCommands))]

namespace Atend.Acad
{
    public class AcadCommands
    {

        [CommandMethod("SendACommandToAutoCAD")]
        public static void SendACommandToAutoCAD()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            // Draws a circle and zooms to the extents or 
            // limits of the drawing
            acDoc.SendStringToExecute("._circle 2,2,0 4 ", true, false, false);
            acDoc.SendStringToExecute("._zoom _all ", true, false, false);
        }



        [CommandMethod("ATEND")]
        public static void ATEND()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("444:\n");
            if (true)
            {
                string strNames = "";
                ed.WriteMessage("444:\n");
                using (Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Autodesk\AutoCAD\R20.1\ACAD-F001:409\ATEND", false))
                {
                    if (Key != null)
                    {
                        strNames = Key.GetValue("LOADER").ToString();
                        ed.WriteMessage("strNames:{0}\n", strNames);
                    }
                }
                Atend.Control.Common.fullPath = strNames.Replace(@"\Atend.dll", "");
                ed.WriteMessage("fullpath:{0}\n", Atend.Control.Common.fullPath);
                try
                {

                    Atend.Acad.Ribbon.SettingRibbon();
                    ed.WriteMessage("Ribbon is called. \n");
                }
                catch(Autodesk.AutoCAD.BoundaryRepresentation.Exception  ex)
                {
                    Atend.Control.Common.IsClassicView = true;
                    ed.WriteMessage("Error:{0}\n", ex.Message);
                }
            }
            else
            {
                System.Diagnostics.Process[] prs = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process pr in prs)
                {
                    if (pr.ProcessName == "acad")
                    {
                        pr.CloseMainWindow();
                    }
                }
            }

            //RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\PDFlib\PDFlib\7.0.4", true);
            //if (key == null)
            //{
            //    key = Registry.LocalMachine.CreateSubKey(@"Software\PDFlib\PDFlib\7.0.4");
            //    key = Registry.LocalMachine.OpenSubKey(@"Software\PDFlib\PDFlib\7.0.4", true);
            //    key.SetValue("license", "w700602-009100-731090-Y6WPH2-5SE4A2", RegistryValueKind.String);
            //}


        }



        [CommandMethod("sSs")]
        static public void sss()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;



            PromptStringOptions pso = new PromptStringOptions("OI:");
            PromptResult pr = ed.GetString(pso);
            Atend.Global.Acad.UAcad.GetNodeInfoByObjectId02(Convert.ToInt32(pr.StringResult));

            //Atend.Global.Acad.DrawEquips.AcDrawGIS.DrawNewBlock(new Point3d(10,10,0));


            //--------------------------------------------------

            //Atend.Global.Acad.DrawEquips.AcDrawFrame _AcDrawFrame = new Atend.Global.Acad.DrawEquips.AcDrawFrame();
            //_AcDrawFrame.UpdateFrame();

            //--------------------------------------------------

            //List<Entity> ents = Atend.Global.Acad.DrawEquips.AcDrawPolygonPole.LegendEntity(new Point3d(0, 0, 0),"مشخصات پایه پرتیک");
            //foreach (Entity en in ents)
            //{
            //    Atend.Global.Acad.UAcad.DrawEntityOnScreen(en, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
            //}


            //PromptPointOptions ppo = new PromptPointOptions("select opoint");
            //PromptPointResult ppr = ed.GetPoint(ppo);
            //ed.WriteMessage("ANS : {0} \n", Atend.Global.Acad.DrawEquips.AcDrawForbidenArea.PointWasInForbidenArea(ppr.Value));


            //Atend.Global.Acad.Global.FindAllEquips();

            //////PromptEntityOptions peo = new PromptEntityOptions("select e:");
            //////PromptEntityResult per = ed.GetEntity(peo);
            //////ObjectId goi = Atend.Global.Acad.UAcad.GetEntityGroup(per.ObjectId);
            //////if (goi != ObjectId.Null)
            //////{
            //////    Point3d p3 = Atend.Global.Acad.UAcad.CenterOfGroup(goi);
            //////    ed.WriteMessage("p3:{0} \n", p3);
            //////}

            //Atend.Calculating.frmTest _frmTest = new Atend.Calculating.frmTest();
            //_frmTest.dataGridView5.DataSource = dt;
            //_frmTest.ShowDialog();

            //Atend.Commands cm = new Commands();
            //cm.CreateWhiteBack(new ObjectId(int.Parse(pr.StringResult)));

            //System.Data.DataColumn c1 = new System.Data.DataColumn("MiddleJackPanelXCode");
            //System.Data.DataColumn c2 = new System.Data.DataColumn("CellXCode");

            //////System.Data.DataTable MiddleJackCells;
            //////PromptEntityOptions peo = new PromptEntityOptions("select enti: \n");
            //////PromptEntityResult per = ed.GetEntity(peo);

            //////ObjectId GOI = Atend.Global.Acad.UAcad.GetEntityGroup(per.ObjectId);
            //////if (GOI != ObjectId.Null)
            //////{
            //////    //now get middlejackpanel info
            //////    Atend.Base.Acad.AT_INFO MJPInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(GOI);
            //////    if (MJPInfo.ParentCode != "NONE" && MJPInfo.NodeType == (int)Atend.Control.Enum.ProductType.MiddleJackPanel)
            //////    {
            //////        MiddleJackCells = Atend.Base.Design.DCellStatus.AccessSelectByJackPanelCode(new Guid(MJPInfo.NodeCode));
            //////        System.Data.DataColumn c1 = new System.Data.DataColumn("HeaderOI");
            //////        System.Data.DataColumn c2 = new System.Data.DataColumn("IsEnterance");
            //////        System.Data.DataColumn c3 = new System.Data.DataColumn("Order");
            //////        MiddleJackCells.Columns.Add(c1);
            //////        MiddleJackCells.Columns.Add(c2);
            //////        MiddleJackCells.Columns.Add(c3);
            //////        if (MiddleJackCells.Rows.Count > 0)
            //////        {
            //////            //bring all sub of middle jackpanel entities
            //////            ObjectIdCollection OIC = Atend.Global.Acad.UAcad.GetGroupSubEntities(GOI);
            //////            int CellCounter = 1;
            //////            foreach (ObjectId oi in OIC)
            //////            {
            //////                Atend.Base.Acad.AT_INFO CellInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
            //////                if (CellInfo.ParentCode != "NONE" && CellInfo.NodeType == (int)Atend.Control.Enum.ProductType.Cell)
            //////                {
            //////                    System.Data.DataRow[] mycell = MiddleJackCells.Select(string.Format("CellCode = '{0}'", CellInfo.NodeCode));
            //////                    if (mycell.Length > 0)
            //////                    {
            //////                        ObjectId hoi = ObjectId.Null;
            //////                        mycell[0]["Order"] = CellCounter;
            //////                        CellCounter++;
            //////                        foreach (ObjectId oii in OIC)
            //////                        {
            //////                            Atend.Base.Acad.AT_INFO hinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);

            //////                            if (hinfo.ParentCode != "NONE" && hinfo.NodeCode == CellInfo.NodeCode && hinfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
            //////                            {
            //////                                hoi = oii;
            //////                            }
            //////                        }
            //////                        if (hoi != ObjectId.Null)
            //////                        {
            //////                            mycell[0]["HeaderOI"] = hoi.ToString().Substring(1, hoi.ToString().Length - 2);
            //////                            if (hoi == per.ObjectId)
            //////                            {
            //////                                mycell[0]["IsEnterance"] = 1;
            //////                            }
            //////                            else
            //////                            {
            //////                                mycell[0]["IsEnterance"] = 0;
            //////                            }
            //////                        }
            //////                        else
            //////                        {
            //////                            mycell[0]["HeaderOI"] = -1;
            //////                            mycell[0]["IsEnterance"] = 0;
            //////                        }
            //////                    }
            //////                }
            //////            }
            //////        }
            //////    }
            //////}//
        }


        [CommandMethod("SHOWPALETTE")]
        static public void ShowPalette()
        {
            Atend.Control.Common.ps.Visible = true;
        }

        [CommandMethod("FORBIDENAREA")]
        public void DrawForbidenArea()
        {
            Atend.Global.Acad.DrawEquips.AcDrawForbidenArea.DrawForbidenArea();
        }

        [CommandMethod("FINALDESIGN")]
        public void DrawFinaldesign()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Global.Acad.DrawEquips.AcDrawGroundPost.DrawShield();
                Atend.Global.Acad.DrawEquips.AcDrawPole.DrawShield();
                Atend.Global.Acad.DrawEquips.AcDrawPole.DrawShieldTip();
                Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel.DrawShield();
                Atend.Global.Acad.DrawEquips.AcDrawAirPost.DrawShield();
                Atend.Global.Acad.DrawEquips.AcDrawDB.DrawShield();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("EEROR COMMAND : {0} \n", ex.Message);
            }

        }

        [CommandMethod("TERMINAL")]
        public void DrawTerminal()
        {
            Atend.Global.Acad.DrawEquips.AcDrawTerminal _AcDrawTerminal = new Atend.Global.Acad.DrawEquips.AcDrawTerminal();

            _AcDrawTerminal.UseAccess = Atend.Base.Acad.AcadGlobal.TerminalData.UseAccess;
            _AcDrawTerminal.Existance = Atend.Base.Acad.AcadGlobal.TerminalData.Existance;
            _AcDrawTerminal.ProjectCode = Atend.Base.Acad.AcadGlobal.TerminalData.ProjectCode;

            _AcDrawTerminal.eConductorTip = Atend.Base.Acad.AcadGlobal.TerminalData.eConductorTip;
            _AcDrawTerminal.eConductors = Atend.Base.Acad.AcadGlobal.TerminalData.eConductors;

            _AcDrawTerminal.DrawTerminal();
        }

        //MOUSAVI
        [CommandMethod("POLE")]
        public void DrawPole()
        {
            if (Atend.Control.Common.Demo)
            {
                if (Atend.Base.Design.DNode.AccessSelectAll().Rows.Count > 15)
                {
                    return;
                }
            }
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(Atend.Control.ConnectionString.AccessCnString + "\n");
            Atend.Global.Acad.DrawEquips.AcDrawPole ACP = new Atend.Global.Acad.DrawEquips.AcDrawPole();
            ACP.dPoleInfo = Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo;
            ACP.UseAccess = Atend.Base.Acad.AcadGlobal.PoleData.UseAccess;
            ACP.ePole = Atend.Base.Acad.AcadGlobal.PoleData.ePole;
            ACP.Height = Atend.Base.Acad.AcadGlobal.PoleData.Height;
            //projectcode
            ACP.ProjectCode = Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode;
            ACP.Existance = Atend.Base.Acad.AcadGlobal.PoleData.Existance;
            ACP.eConsols = Atend.Base.Acad.AcadGlobal.PoleData.eConsols;
            ACP.eConsolUseAccess = Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess;
            ACP.eConsolExistance = Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance;
            ACP.eConsolCount = Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount;
            ACP.HalterExist = Atend.Base.Acad.AcadGlobal.PoleData.HalterExistance;
            ACP.HalterProjectCode = Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode;
            ACP.eHalter = Atend.Base.Acad.AcadGlobal.PoleData.eHalter;
            ACP.eHalterCount = Atend.Base.Acad.AcadGlobal.PoleData.eHalterCount;
            //ProjectCode
            ACP.eConsolProjectCode = Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode;
            //for halter ??????????????
            ACP.DrawPole();
            //ACP.DrawPole(Point3d.Origin, 120);
        }

        //MOUSAVI
        [CommandMethod("POLETip")]
        public void DrawPoleTip()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            if (Atend.Control.Common.Demo)
            {
                if (Atend.Base.Design.DNode.AccessSelectAll().Rows.Count > 15)
                {
                    return;
                }
            }
            //ed.WriteMessage(Atend.Control.ConnectionString.AccessCnString + "\n");
            Atend.Global.Acad.DrawEquips.AcDrawPole ACP = new Atend.Global.Acad.DrawEquips.AcDrawPole();
            ACP.dPoleInfo = Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo;
            ACP.UseAccess = Atend.Base.Acad.AcadGlobal.PoleData.UseAccess;
            ACP.ePole = Atend.Base.Acad.AcadGlobal.PoleData.ePole;
            ACP.eConsols = Atend.Base.Acad.AcadGlobal.PoleData.eConsols;
            ACP.eConsolUseAccess = Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess;
            ACP.eConsolExistance = Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance;
            ACP.eConsolCount = Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount;
            ACP.ePoleTip = Atend.Base.Acad.AcadGlobal.PoleData.ePoleTip;

            //projectcode
            ACP.ProjectCode = Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode;
            ACP.Existance = Atend.Base.Acad.AcadGlobal.PoleData.Existance;
            ACP.eConsolProjectCode = Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode;

            ACP.eHalter = Atend.Base.Acad.AcadGlobal.PoleData.eHalter;
            ACP.HalterExist = Atend.Base.Acad.AcadGlobal.PoleData.HalterExistance;
            ACP.HalterProjectCode = Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode;
            ACP.eHalterCount = Atend.Base.Acad.AcadGlobal.PoleData.eHalterCount;

            ACP.Height = Atend.Base.Acad.AcadGlobal.PoleData.Height;
            ACP.DrawPolTip();
        }

        //MOUSAVI
        [CommandMethod("Kalamp")]
        public void DrawKalamp()
        {

            Atend.Global.Acad.DrawEquips.AcDrawKalamp _AcDrawKalamp = new Atend.Global.Acad.DrawEquips.AcDrawKalamp();
            _AcDrawKalamp.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
            _AcDrawKalamp.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
            _AcDrawKalamp.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;

            //projectcode
            _AcDrawKalamp.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
            _AcDrawKalamp.DrawKalamp();
        }

        //MOUSAVi
        [CommandMethod("HeaderCable")]
        public void DrawHeaderCable()
        {

            Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel _headerCable = new Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel();
            _headerCable.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
            _headerCable.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
            _headerCable.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
            _headerCable.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;
            _headerCable.DrawHeaderCabel();
        }

        [CommandMethod("Frame")]
        public void DrawFrame()
        {

            Atend.Global.Acad.DrawEquips.AcDrawFrame _AcDrawFrame = new Atend.Global.Acad.DrawEquips.AcDrawFrame();
            _AcDrawFrame.FrameHeigh = Atend.Base.Acad.AcadGlobal.FrameData.Height;
            _AcDrawFrame.FrameWidth = Atend.Base.Acad.AcadGlobal.FrameData.Width;
            _AcDrawFrame.Products = Atend.Base.Acad.AcadGlobal.FrameData.Products;
            _AcDrawFrame.HaveDescription = Atend.Base.Acad.AcadGlobal.FrameData.HaveDescription;
            _AcDrawFrame.HaveInformation = Atend.Base.Acad.AcadGlobal.FrameData.HaveInformation;
            _AcDrawFrame.HaveSign = Atend.Base.Acad.AcadGlobal.FrameData.HaveSign;
            _AcDrawFrame.DrawFrame();
        }

        //MOUSAVI
        [CommandMethod("Kablsho")]
        public void DrawKablsho()
        {

            Atend.Global.Acad.DrawEquips.AcDrawKablsho _AcDrawKablsho = new Atend.Global.Acad.DrawEquips.AcDrawKablsho();
            _AcDrawKablsho.eKablsho = Atend.Base.Acad.AcadGlobal.KablshoData.eKablsho;
            _AcDrawKablsho.Existance = Atend.Base.Acad.AcadGlobal.KablshoData.Existance;
            _AcDrawKablsho.UseAccess = Atend.Base.Acad.AcadGlobal.KablshoData.UseAccess;
            _AcDrawKablsho.ProjectCode = Atend.Base.Acad.AcadGlobal.KablshoData.ProjectCode;
            _AcDrawKablsho.DrawKablsho();
        }

        #region table code
        //private static ObjectId CreatePoleTable(Point3d TablePosition)
        //{

        //    Document doc = Application.DocumentManager.MdiActiveDocument;
        //    Database db = doc.Database;
        //    Editor ed = doc.Editor;
        //    ObjectId TableId;




        //    Table tb = new Table();
        //    tb.TableStyle = db.Tablestyle;
        //    tb.NumRows = 8;
        //    tb.NumColumns = 6;
        //    tb.SetColumnWidth(85);
        //    tb.SetRowHeight(28);
        //    tb.Position = TablePosition;


        //    //write data in table
        //    //0
        //    tb.SetTextHeight(0, 0, 15);
        //    tb.SetTextString(0, 0, "شرکت توزیع نیروی برق نواحی تهران");
        //    tb.SetAlignment(0, 0, CellAlignment.MiddleCenter);

        //    //1
        //    tb.SetTextHeight(1, 5, 12);
        //    tb.SetTextString(1, 5, "عنوان");
        //    tb.SetAlignment(1, 5, CellAlignment.MiddleLeft);

        //    //2
        //    tb.SetTextHeight(2, 5, 12);
        //    tb.SetTextString(2, 5, "نشانی");
        //    tb.SetAlignment(2, 5, CellAlignment.MiddleLeft);

        //    //3
        //    tb.SetTextHeight(3, 1, 12);
        //    tb.SetTextString(3, 1, "طراح");
        //    tb.SetAlignment(3, 1, CellAlignment.MiddleLeft);

        //    tb.SetTextHeight(3, 5, 12);
        //    tb.SetTextString(3, 5, "شماره");
        //    tb.SetAlignment(3, 5, CellAlignment.MiddleLeft);


        //    //4
        //    tb.SetTextHeight(4, 1, 12);
        //    tb.SetTextString(4, 1, "کنترل");
        //    tb.SetAlignment(4, 1, CellAlignment.MiddleLeft);

        //    tb.SetTextHeight(4, 3, 12);
        //    tb.SetTextString(4, 3, "نقشه بردار");
        //    tb.SetAlignment(4, 3, CellAlignment.MiddleLeft);

        //    tb.SetTextHeight(4, 5, 12);
        //    tb.SetTextString(4, 5, "ویرایش");
        //    tb.SetAlignment(4, 5, CellAlignment.MiddleLeft);

        //    //5
        //    tb.SetTextHeight(5, 1, 12);
        //    tb.SetTextString(5, 1, "تایید");
        //    tb.SetAlignment(5, 1, CellAlignment.MiddleLeft);

        //    tb.SetTextHeight(5, 3, 12);
        //    tb.SetTextString(5, 3, "ترسیم");
        //    tb.SetAlignment(5, 3, CellAlignment.MiddleLeft);

        //    tb.SetTextHeight(5, 5, 12);
        //    tb.SetTextString(5, 5, "شماره ثبت");
        //    tb.SetAlignment(5, 5, CellAlignment.MiddleLeft);

        //    //6
        //    tb.SetTextHeight(6, 1, 12);
        //    tb.SetTextString(6, 1, "تصویب");
        //    tb.SetAlignment(6, 1, CellAlignment.MiddleLeft);

        //    tb.SetTextHeight(6, 3, 12);
        //    tb.SetTextString(6, 3, "تاریخ");
        //    tb.SetAlignment(6, 3, CellAlignment.MiddleLeft);

        //    tb.SetTextHeight(6, 5, 12);
        //    tb.SetTextString(6, 5, "مقیاس");
        //    tb.SetAlignment(6, 5, CellAlignment.MiddleLeft);

        //    //7
        //    tb.SetTextHeight(7, 1, 12);
        //    tb.SetTextString(7, 1, "تهیه کننده");
        //    tb.SetAlignment(7, 1, CellAlignment.MiddleLeft);

        //    tb.SetTextHeight(7, 3, 12);
        //    tb.SetTextString(7, 3, "مدت اعتبار");
        //    tb.SetAlignment(7, 3, CellAlignment.MiddleLeft);


        //    tb.GenerateLayout();



        //    Transaction tr = doc.TransactionManager.StartTransaction();

        //    using (tr)
        //    {

        //        BlockTable bt = (BlockTable)tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);

        //        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

        //        TableId = btr.AppendEntity(tb);

        //        tr.AddNewlyCreatedDBObject(tb, true);

        //        tr.Commit();

        //    }


        //    return TableId;

        //}
        #endregion

        //MOUSAVI
        [CommandMethod("POLECircle")]
        public void DrawPoleCircle()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //ed.WriteMessage(Atend.Control.ConnectionString.AccessCnString + "\n");

            if (Atend.Control.Common.Demo)
            {
                if (Atend.Base.Design.DNode.AccessSelectAll().Rows.Count > 15)
                {
                    return;
                }
            }
            Atend.Global.Acad.DrawEquips.AcDrawCirclePole ADCP = new Atend.Global.Acad.DrawEquips.AcDrawCirclePole();
            ADCP.dPoleInfo = Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo;
            ADCP.UseAccess = Atend.Base.Acad.AcadGlobal.CirclePoleData.UseAccess;
            ADCP.ePole = Atend.Base.Acad.AcadGlobal.CirclePoleData.ePole;
            ADCP.Height = Atend.Base.Acad.AcadGlobal.CirclePoleData.Height;

            ADCP.eConsols = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols;
            ADCP.eConsolUseAccess = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess;
            ADCP.eConsolExistance = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance;
            ADCP.eConsolCount = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount;
            ADCP.Existance = Atend.Base.Acad.AcadGlobal.CirclePoleData.Existance;

            ADCP.HalterExistance = Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterExistance;
            ADCP.HalterProjectCode = Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterProjectCode;
            ADCP.eHalter = Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalter;
            ADCP.eHalterCount = Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalterCount;

            //projectcode
            ADCP.ProjectCode = Atend.Base.Acad.AcadGlobal.CirclePoleData.ProjectCode;
            ADCP.eConsolProjectCode = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode;


            ADCP.Height = Atend.Base.Acad.AcadGlobal.CirclePoleData.Height;
            ed.WriteMessage("%%%%Height={0}\n", ADCP.Height);
            ADCP.DrawPoleCircle();
            //ADCP.DrawPoleCircle(Point3d.Origin, 185);

        }

        //MOUSAVI
        [CommandMethod("POLECircleTip")]
        public void DrawPoleCircleTip()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //ed.WriteMessage(Atend.Control.ConnectionString.AccessCnString + "\n");

            if (Atend.Control.Common.Demo)
            {
                if (Atend.Base.Design.DNode.AccessSelectAll().Rows.Count > 15)
                {
                    return;
                }
            }
            Atend.Global.Acad.DrawEquips.AcDrawCirclePole ADCP = new Atend.Global.Acad.DrawEquips.AcDrawCirclePole();
            ADCP.dPoleInfo = Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo;
            ADCP.UseAccess = Atend.Base.Acad.AcadGlobal.CirclePoleData.UseAccess;
            ADCP.ePole = Atend.Base.Acad.AcadGlobal.CirclePoleData.ePole;
            ADCP.eConsols = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols;
            ADCP.eConsolUseAccess = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess;
            ADCP.eConsolExistance = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance;
            ADCP.eConsolCount = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount;
            ADCP.ePoleTip = Atend.Base.Acad.AcadGlobal.CirclePoleData.ePoleTip;

            //projectcode
            ADCP.ProjectCode = Atend.Base.Acad.AcadGlobal.CirclePoleData.ProjectCode;
            ADCP.Existance = Atend.Base.Acad.AcadGlobal.CirclePoleData.Existance;
            ADCP.eConsolProjectCode = Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode;

            ADCP.eHalter = Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalter;
            ADCP.HalterExistance = Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterExistance;
            ADCP.HalterProjectCode = Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterProjectCode;
            ADCP.eHalterCount = Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalterCount;

            ADCP.Height = Atend.Base.Acad.AcadGlobal.CirclePoleData.Height;
            ed.WriteMessage("@@@@@@@@@@@@Height={0}\n", Atend.Base.Acad.AcadGlobal.PoleData.Height);


            ADCP.DrawPoleCircleTip();

        }

        //MOUSAVI
        [CommandMethod("PolePolygon")]
        public void DrawPolePolygon()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(Atend.Control.ConnectionString.AccessCnString + "\n");
            if (Atend.Control.Common.Demo)
            {
                if (Atend.Base.Design.DNode.AccessSelectAll().Rows.Count > 15)
                {
                    return;
                }
            }
            Atend.Global.Acad.DrawEquips.AcDrawPolygonPole ADPP = new Atend.Global.Acad.DrawEquips.AcDrawPolygonPole();
            ADPP.dPoleInfo = Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo;
            ADPP.UseAccess = Atend.Base.Acad.AcadGlobal.PolygonPoleData.UseAccess;
            ADPP.ePole = Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePole;
            ADPP.Height = Atend.Base.Acad.AcadGlobal.PolygonPoleData.Height;


            ADPP.eConsols = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols;
            ADPP.eConsolUseAccess = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess;
            ADPP.eConsolExistance = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance;
            ADPP.eConsolCount = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount;
            ADPP.HalterExistance = Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterExistance;
            ADPP.HalterProjectCode = Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterProjectCode;
            ADPP.eHalter = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalter;

            //projectcode
            ADPP.ProjectCode = Atend.Base.Acad.AcadGlobal.PolygonPoleData.ProjectCode;
            ADPP.Existance = Atend.Base.Acad.AcadGlobal.PolygonPoleData.Existance;
            ADPP.eConsolProjectCode = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode;

            ADPP.eHalter = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalter;
            ADPP.HalterExistance = Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterExistance;
            ADPP.HalterProjectCode = Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterProjectCode;
            ADPP.eHalterCount = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalterCount;

            ADPP.DrawPolePolygon();
            //ADPP.DrawPolePolygon(Point3d.Origin, 185);

        }

        [CommandMethod("PolePolygonTip")]
        public void DrawPolePolygonTip()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //ed.WriteMessage(Atend.Control.ConnectionString.AccessCnString + "\n");

            if (Atend.Control.Common.Demo)
            {
                if (Atend.Base.Design.DNode.AccessSelectAll().Rows.Count > 15)
                {
                    return;
                }
            }
            Atend.Global.Acad.DrawEquips.AcDrawPolygonPole ADPP = new Atend.Global.Acad.DrawEquips.AcDrawPolygonPole();
            ADPP.dPoleInfo = Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo;
            ADPP.UseAccess = Atend.Base.Acad.AcadGlobal.PolygonPoleData.UseAccess;
            ADPP.ePole = Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePole;
            ADPP.eConsols = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols;
            ADPP.eConsolUseAccess = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess;
            ADPP.eConsolExistance = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance;
            ADPP.eConsolCount = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount;
            ADPP.ePoleTip = Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePoleTip;

            //projectcode
            ADPP.ProjectCode = Atend.Base.Acad.AcadGlobal.PolygonPoleData.ProjectCode;
            ADPP.Existance = Atend.Base.Acad.AcadGlobal.PolygonPoleData.Existance;
            ADPP.eConsolProjectCode = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode;

            ADPP.eHalter = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalter;
            ADPP.HalterExistance = Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterExistance;
            ADPP.HalterProjectCode = Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterProjectCode;
            ADPP.eHalterCount = Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalterCount;

            ADPP.Height = Atend.Base.Acad.AcadGlobal.PolygonPoleData.Height;

            ADPP.DrawPolePolygonTip();

        }


        //[CommandMethod("Transformer")]
        //public void DrawTransformer(Entity PostContainerEntity, int ProductCode)
        //{
        //    //Atend.Global.Acad.DrawEquips.AcDrawTransformer ADT = new Atend.Global.Acad.DrawEquips.AcDrawTransformer();
        //    //ADT.DrawTransformer();
        //}






        //MOUSAVi
        [CommandMethod("CONDUCTOR")]
        public void DrawConductor()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            Atend.Global.Acad.DrawEquips.AcDrawConductor ADC = new Atend.Global.Acad.DrawEquips.AcDrawConductor();
            ADC.Existance = Atend.Base.Acad.AcadGlobal.ConductorData.Existance;
            ADC.UseAccess = Atend.Base.Acad.AcadGlobal.ConductorData.UseAccess;
            ADC.eConductorTip = Atend.Base.Acad.AcadGlobal.ConductorData.eConductorTip;
            ADC.eConductors = Atend.Base.Acad.AcadGlobal.ConductorData.eConductors;

            //projectcode
            ADC.ProjectCode = Atend.Base.Acad.AcadGlobal.ConductorData.ProjectCode;

            ADC.DrawConductor();
        }

        //MOUSAVI
        [CommandMethod("GroundPost")]
        public void DrawGroundPost()
        {

            if (Atend.Control.Common.Demo)
            {
                if (Atend.Base.Design.DPost.AccessSelectAll().Rows.Count > 1)
                {
                    return;
                }
            }
            Atend.Global.Acad.DrawEquips.AcDrawGroundPost02 ADGP = new Atend.Global.Acad.DrawEquips.AcDrawGroundPost02();

            ADGP.UseAccess = Atend.Base.Acad.AcadGlobal.GroundPostData.UseAccess;

            ADGP.eGroundPost = Atend.Base.Acad.AcadGlobal.GroundPostData.eGroundPost;

            ADGP.eJackPanelMiddles = Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles;

            ADGP.eJackPanelWeeks = Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks;

            ADGP.eTransformers = Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers;

            ADGP.Existance = Atend.Base.Acad.AcadGlobal.GroundPostData.Existance;

            ADGP.eJackPanelMiddleExistance = Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance;

            ADGP.eJackPanelWeekExistance = Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance;

            ADGP.eTransformerExistance = Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance;


            //projectcode
            ADGP.ProjectCode = Atend.Base.Acad.AcadGlobal.GroundPostData.ProjectCode;
            ADGP.eJackPanelMiddleProjectCode = Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleProjectCode;
            ADGP.eJackPanelWeekProjectCode = Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekProjectCode;
            ADGP.eTransformerProjectCode = Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerProjectCode;


            ADGP.DrawGroundPost();

        }

        [CommandMethod("Jumper")]
        public void DrawJumper()
        {
            Atend.Global.Acad.DrawEquips.AcDrawJumper ADJ = new Atend.Global.Acad.DrawEquips.AcDrawJumper();
            ADJ.DrawJumper();
        }

        [CommandMethod("GroundPostUpdate")]
        public void DrawGroundPostUpdate()
        {

            //Atend.Global.Acad.DrawEquips.AcDrawGroundPost _AcDrawGroundPost = Atend.Base.Acad.AcadGlobal.GroundPostData._AcDrawGroundPost;
            //if (_AcDrawGroundPost.UpdateGroundPostData(new Guid(at_Info.NodeCode)))
            //{
            //    _AcDrawGroundPost.DrawGroundPost();
            //}
            //else
            //{
            //    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
            //    notification.Title = "خطا";
            //    notification.Msg = "خطا در ویرایش پست زمینی";
            //    notification.ShowStatusBarBalloon();
            //}


        }

        //MOUSAVI
        [CommandMethod("DB")]
        public void DrawDB()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawDB _AcDrawDB = new Atend.Global.Acad.DrawEquips.AcDrawDB();
            _AcDrawDB.UseAccess = Atend.Base.Acad.AcadGlobal.DBData.UseAccess;
            _AcDrawDB.ProjectCode = Atend.Base.Acad.AcadGlobal.DBData.ProjectCode;
            _AcDrawDB.Existance = Atend.Base.Acad.AcadGlobal.DBData.Existance;
            _AcDrawDB.eDBPhuse = Atend.Base.Acad.AcadGlobal.DBData.eDBPhuse;
            _AcDrawDB.EDB = Atend.Base.Acad.AcadGlobal.DBData.eDB;
            //ed.WriteMessage("Start draw db from out side \n");
            _AcDrawDB.DrawDB();
            //ed.WriteMessage("finish draw db from out side \n");

        }

        [CommandMethod("ConnectionPoint")]
        public void DrawConnectionPoint()
        {
            //Atend.Global.Acad.DrawEquips.AcDrawConnectionPoint ADCP = new Atend.Global.Acad.DrawEquips.AcDrawConnectionPoint();
            //ADCP.DrawConnectionPoint();
        }

        //[CommandMethod("MiddleJackPanel")]
        //public void DrawMiddleJackPanel(Entity PostContainerEntity, int MiddleJackPanelProductCode)
        //{
        //    //Atend.Global.Acad.DrawEquips.AcDrawMiddleJackPanel ADMJ = new Atend.Global.Acad.DrawEquips.AcDrawMiddleJackPanel();
        //    //ADMJ.DrawMiddleJackPanel();
        //}




        //[CommandMethod("WeekJackPanel")]
        //public void DrawWeekJackPanel(Entity PostContainerEntity, int CellCount, int ProductCode)
        //{
        //    //Atend.Global.Acad.DrawEquips.AcDrawWeekJackPanel ADWJ = new Atend.Global.Acad.DrawEquips.AcDrawWeekJackPanel();
        //    //ADWJ.DrawWeekJackPanel();
        //}


        //MOUSAVI


        [CommandMethod("LoadBackGround")]
        public void AttachXref()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            string ReferenzName = System.IO.Path.GetFileNameWithoutExtension(Atend.Control.Common.DesignBackGroundName);
            //ed.WriteMessage("~~~~~~{0}\n", ReferenzName);
            string ReferenzPath = Atend.Control.Common.DesignBackGroundName;  //Atend.Control.Common.DesignFullAddress + @"\grid.dwg";


            //ed.WriteMessage("~~~~~~{0}\n", ReferenzPath);
            Autodesk.AutoCAD.ApplicationServices.Document dwg = Application.DocumentManager.MdiActiveDocument;
            Database db = dwg.Database;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = dwg.TransactionManager;
            Transaction bTrans = tm.StartTransaction();
            try
            {
                ObjectId myX = db.AttachXref(ReferenzPath, ReferenzName);
                Autodesk.AutoCAD.DatabaseServices.BlockReference myBl = new
                BlockReference(new Autodesk.AutoCAD.Geometry.Point3d(0, 0, 0), myX);
                BlockTable bt = (BlockTable)bTrans.GetObject(db.BlockTableId, OpenMode.ForRead, false);
                BlockTableRecord mSpace = (BlockTableRecord)bTrans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite, false);
                mSpace.AppendEntity(myBl);
                bTrans.AddNewlyCreatedDBObject(myBl, true);
                bTrans.Commit();

                // XATTACH
                //object obj = Application.GetSystemVariable("XATTACH");
                //ed.WriteMessage("XATTACH:{0} \n",obj);

            }
            catch (System.Exception e)
            {
                string tmp = e.Message;
                //ed.WriteMessage(e + "\n");
                bTrans.Abort();
            }
        }


        [CommandMethod("SelfKeeper")]
        public void DrawSelfKeeper()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper ADSK = new Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper();
            ADSK.Existance = Atend.Base.Acad.AcadGlobal.SelfKeeperData.Existance;
            ADSK.UseAccess = Atend.Base.Acad.AcadGlobal.SelfKeeperData.UseAccess;
            ADSK.eSelfKeeperTip = Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeeperTip;
            ADSK.eSelfKeepers = Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeepers;

            //projectcode
            ADSK.ProjectCode = Atend.Base.Acad.AcadGlobal.SelfKeeperData.ProjectCode;

            ADSK.DrawSelfKeeper02();

        }


        [CommandMethod("DrawingSaved")]
        public static void DrawingSaved()
        {
            object obj = Application.GetSystemVariable("DBMOD");
            if (System.Convert.ToInt16(obj) != 0)
            {
                if (System.Windows.Forms.MessageBox.Show("Do you wish to save this drawing?",
                "Save Drawing",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.Yes)
                {
                    Document acDoc = Application.DocumentManager.MdiActiveDocument;
                    acDoc.Database.SaveAs(acDoc.Name, true, DwgVersion.Current,
                    acDoc.Database.SecurityParameters);
                }
            }
        }


        [CommandMethod("GroundCabel")]
        public void DrawGroundCable()
        {
            Atend.Global.Acad.DrawEquips.AcDrawGroundCabel ADGC = new Atend.Global.Acad.DrawEquips.AcDrawGroundCabel();

            ADGC.UseAccess = Atend.Base.Acad.AcadGlobal.GroundCableData.UseAccess;
            ADGC.ProjectCode = Atend.Base.Acad.AcadGlobal.GroundCableData.ProjectCode;
            ADGC.Existance = Atend.Base.Acad.AcadGlobal.GroundCableData.Existance;
            ADGC.eGroundCabelTip = Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabelTip;
            ADGC.eGroundCabels = Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabels;

            ADGC.DrawGroundCable02();
        }


        [CommandMethod("CONSOL")]
        public void DrawConsol()
        {
            Atend.Global.Acad.DrawEquips.AcDrawConsol ADC = new Atend.Global.Acad.DrawEquips.AcDrawConsol();
            ADC.UseAccess = Atend.Base.Acad.AcadGlobal.ConsolData.UseAccess;
            ADC.ProjectCode = Atend.Base.Acad.AcadGlobal.ConsolData.ProjectCode;
            ADC.Existance = Atend.Base.Acad.AcadGlobal.ConsolData.Existance;
            ADC.eConsol = Atend.Base.Acad.AcadGlobal.ConsolData.eConsol;
            ADC.ConsolConut = 1;
            ADC.DrawConsol();
        }


        [CommandMethod("GROUND")]
        public void DrawGround()
        {
            Atend.Global.Acad.DrawEquips.AcDrawGround ADG = new Atend.Global.Acad.DrawEquips.AcDrawGround();
            ADG.UseAccess = Atend.Base.Acad.AcadGlobal.GroundData.UseAccess;
            ADG.ProjectCode = Atend.Base.Acad.AcadGlobal.GroundData.ProjectCode;
            ADG.Existance = Atend.Base.Acad.AcadGlobal.GroundData.Existance;
            ADG.eGround = Atend.Base.Acad.AcadGlobal.GroundData.eGround;
            ADG.DrawGround02();

        }

        //MOUSAVI
        [CommandMethod("AirPost")]
        public void DrawAirPost()
        {

            if (Atend.Control.Common.Demo)
            {
                if (Atend.Base.Design.DPost.AccessSelectAll().Rows.Count > 1)
                {
                    return;
                }
            }
            Atend.Global.Acad.DrawEquips.AcDrawAirPost ADAP = new Atend.Global.Acad.DrawEquips.AcDrawAirPost();

            ADAP.eAirPost = Atend.Base.Acad.AcadGlobal.AirPostData.eAirPost;
            ADAP.eJackPanelWeekExistance = Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance;
            ADAP.eJackPanelWeeks = Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks;
            ADAP.eTransformerExistance = Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance;
            ADAP.eTransformers = Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers;
            ADAP.Existance = Atend.Base.Acad.AcadGlobal.AirPostData.Existance;
            ADAP.UseAccess = Atend.Base.Acad.AcadGlobal.AirPostData.UseAccess;

            //projectcode
            ADAP.ProjectCode = Atend.Base.Acad.AcadGlobal.AirPostData.ProjectCode;
            ADAP.eJackPanelWeekProjectCode = Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode;
            ADAP.eTransformerProjectCode = Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerProjectCode;



            ADAP.DrawAirPost();

        }


        [CommandMethod("Khazan")]
        public void DrawKhazan()
        {
            Atend.Global.Acad.DrawEquips.AcDrawKhazan DKhazan = new Atend.Global.Acad.DrawEquips.AcDrawKhazan();
            DKhazan.UseAccess = Atend.Base.Acad.AcadGlobal.KhazanData.UseAccess;
            DKhazan.Existance = Atend.Base.Acad.AcadGlobal.KhazanData.Existance;
            DKhazan.eKhazanTip = Atend.Base.Acad.AcadGlobal.KhazanData.eKhazanTip;
            DKhazan.ProjectCode = Atend.Base.Acad.AcadGlobal.KhazanData.ProjectCode;
            DKhazan.DrawKhazan();
        }


        [CommandMethod("ROD")]
        public void DrawRod()
        {
            Atend.Global.Acad.DrawEquips.AcDrawRod DROD = new Atend.Global.Acad.DrawEquips.AcDrawRod();
            DROD.ERod = Atend.Base.Acad.AcadGlobal.RodData.eRod;
            DROD.Existance = Atend.Base.Acad.AcadGlobal.RodData.Existance;
            DROD.UseAccess = Atend.Base.Acad.AcadGlobal.RodData.UseAccess;
            DROD.ProjectCode = Atend.Base.Acad.AcadGlobal.RodData.ProjectCode;
            DROD.DrawRod();
        }


        [CommandMethod("LIGHT")]
        public void DrawLight()
        {
            Atend.Global.Acad.DrawEquips.AcDrawLight ADL = new Atend.Global.Acad.DrawEquips.AcDrawLight();
            ADL.eLight = Atend.Base.Acad.AcadGlobal.LightData.eLight;
            ADL.Existance = Atend.Base.Acad.AcadGlobal.LightData.Existance;
            ADL.UseAccess = Atend.Base.Acad.AcadGlobal.LightData.UseAccess;
            ADL.ProjectCode = Atend.Base.Acad.AcadGlobal.LightData.ProjectCode;
            ADL.DrawLigth();
        }


        [CommandMethod("BREAKER")]
        public void DrawBreaker()
        {
            Atend.Global.Acad.DrawEquips.AcDrawBreaker DBREAKER = new Atend.Global.Acad.DrawEquips.AcDrawBreaker();
            DBREAKER.eBreaker = Atend.Base.Acad.AcadGlobal.BreakerData.eBreaker;
            DBREAKER.Existance = Atend.Base.Acad.AcadGlobal.BreakerData.Existance;
            DBREAKER.UseAccess = Atend.Base.Acad.AcadGlobal.BreakerData.UseAccess;
            DBREAKER.ProjectCode = Atend.Base.Acad.AcadGlobal.BreakerData.ProjectCode;
            DBREAKER.DrawBreaker();
        }


        [CommandMethod("CATOUT")]
        public void DrawCatOut()
        {
            Atend.Global.Acad.DrawEquips.AcDrawCatOut ADCO = new Atend.Global.Acad.DrawEquips.AcDrawCatOut();
            ADCO.ECatOut = Atend.Base.Acad.AcadGlobal.CatOutData.eCatOut;
            ADCO.Existance = Atend.Base.Acad.AcadGlobal.CatOutData.Existance;
            ADCO.UseAccess = Atend.Base.Acad.AcadGlobal.CatOutData.UseAccess;
            ADCO.ProjectCode = Atend.Base.Acad.AcadGlobal.CatOutData.ProjectCode;
            ADCO.DrawCatout();
        }


        [CommandMethod("DISCONNECTOR")]
        public void DrawDisConnector()
        {
            Atend.Global.Acad.DrawEquips.AcDrawDisConnector ADDC = new Atend.Global.Acad.DrawEquips.AcDrawDisConnector();
            ADDC.eDisConnector = Atend.Base.Acad.AcadGlobal.DisConnectorData.eDisConnector;
            ADDC.Existance = Atend.Base.Acad.AcadGlobal.DisConnectorData.Existance;
            ADDC.UseAccess = Atend.Base.Acad.AcadGlobal.DisConnectorData.UseAccess;
            ADDC.ProjectCode = Atend.Base.Acad.AcadGlobal.DisConnectorData.ProjectCode;
            ADDC.DrawDisconnector();
        }


        [CommandMethod("STREETBOX")]
        public void DrawStreetBox()
        {
            Atend.Global.Acad.DrawEquips.AcDrawStreetBox ADSB = new Atend.Global.Acad.DrawEquips.AcDrawStreetBox();

            ADSB.UseAccess = Atend.Base.Acad.AcadGlobal.StreetBoxData.UseAccess;
            ADSB.Existance = Atend.Base.Acad.AcadGlobal.StreetBoxData.Existance;
            ADSB.eStreetBox = Atend.Base.Acad.AcadGlobal.StreetBoxData.eStreetBox;
            ADSB.eStreetBoxPhuse = Atend.Base.Acad.AcadGlobal.StreetBoxData.eStreetBoxPhuse;
            ADSB.ProjectCode = Atend.Base.Acad.AcadGlobal.StreetBoxData.ProjectCode;
            ADSB.DrawStreetBox();
        }


        [CommandMethod("MeasuredJackPanel")]
        public void DrawMeasuredJackPanel()
        {
            Atend.Global.Acad.DrawEquips.AcDrawMeasuredJackPanel ADMJ = new Atend.Global.Acad.DrawEquips.AcDrawMeasuredJackPanel();

            ADMJ.UseAccess = Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.UseAccess;
            ADMJ.Existance = Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.Existance;
            ADMJ.eMeasuredJackPanel = Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.eMeasuredJackPanel;
            ADMJ.ProjectCode = Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.ProjectCode;
            ADMJ.DrawMeasuredJackPanel();
        }


        [CommandMethod("Mafsal")]
        public void DrawMafsal()
        {

            Atend.Global.Acad.DrawEquips.AcDrawMafsal DM = new Atend.Global.Acad.DrawEquips.AcDrawMafsal();
            DM.MafsalInfo = Atend.Base.Acad.AcadGlobal.MafsalData.MafsalInfo;
            DM.DrawMafsal();


        }

        //[CommandMethod("ContiLine")]
        //public void DrawContiLine()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    bool Conti = true;
        //    Atend.Global.Acad.AcadJigs.DrawContinousLineJig CLJ = new Atend.Global.Acad.AcadJigs.DrawContinousLineJig();
        //    PromptResult pr;

        //    while (Conti)
        //    {
        //        Polyline pl = CLJ.GetEntity() as Polyline;
        //        if (pl != null && pl.NumberOfVertices == 0)
        //        {
        //            pr = ed.Drag(CLJ);
        //            if (pr.Status == PromptStatus.OK)
        //            {
        //                CLJ.SetStartPoint(CLJ.CentrePoint);
        //                CLJ.AddVertex();

        //            }
        //            else
        //            {
        //                Conti = false;
        //            }
        //        }
        //        else if (pl != null && pl.NumberOfVertices != 1)
        //        {
        //            pr = ed.Drag(CLJ);
        //            if (pr.Status == PromptStatus.OK)
        //            {
        //                Point3d LastPoint = CLJ.CentrePoint;
        //                CLJ.SetEndPoint(LastPoint);

        //                #region Save one  line here

        //                Polyline DrawnLine = CLJ.GetEntity() as Polyline;
        //                if (DrawnLine != null)
        //                {
        //                    Line l = new Line(DrawnLine.GetPoint3dAt(0), DrawnLine.GetPoint3dAt(1));
        //                    Atend.Global.Acad.Global.DrawEntityOnScreen(l, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
        //                }

        //                #endregion


        //                CLJ = new Atend.Global.Acad.AcadJigs.DrawContinousLineJig();
        //                CLJ.SetStartPoint(LastPoint);
        //                CLJ.AddVertex();
        //            }
        //            else
        //            {
        //                Conti = false;
        //            }

        //        }
        //        else
        //        {
        //            Conti = false;
        //        }

        //    }


        //}


        ////////[CommandMethod("ContiLine001")]
        ////////public void MyPolyJig()
        ////////{
        ////////    Document doc = Application.DocumentManager.MdiActiveDocument;

        ////////    Editor ed = doc.Editor;
        ////////    Matrix3d ucs = ed.CurrentUserCoordinateSystem;
        ////////    //ed.WriteMessage("UCS " + ucs.ToString() + "\n");
        ////////    Atend.Global.Acad.AcadJigs.DrawGroundCableJig jig = new Atend.Global.Acad.AcadJigs.DrawGroundCableJig(ucs);
        ////////    ObjectIdCollection HeaderOis = new ObjectIdCollection();
        ////////    bool bSuccess = true, bComplete = false;
        ////////    do
        ////////    {
        ////////        PromptResult res = ed.Drag(jig);
        ////////        bSuccess = (res.Status == PromptStatus.OK);
        ////////        if (bSuccess)
        ////////        {
        ////////            Polyline pl = jig.GetEntity() as Polyline;
        ////////            if (pl != null)
        ////////            {

        ////////                if (pl.NumberOfVertices == 1)
        ////////                {
        ////////                    Point3d CurrentPoint = new Point3d(pl.GetPoint3dAt(pl.NumberOfVertices - 1).X, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Y, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Z);
        ////////                    //ed.WriteMessage("~~~~CurrentPoint:{0}\n", CurrentPoint);
        ////////                    System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CurrentPoint);
        ////////                    System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.GroundCabel);
        ////////                    bool PointContainerWasFound = false;
        ////////                    Point3d AcceptedCenterPoint = Point3d.Origin;
        ////////                    foreach (System.Data.DataRow dr in PointContainerList.Rows)
        ////////                    {
        ////////                        DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        ////////                        if (drs.Length != 0)
        ////////                        {
        ////////                            PointContainerWasFound = true;
        ////////                            HeaderOis.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        ////////                            string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        ////////                            string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        ////////                            string y = StrTemp[1];
        ////////                            string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        ////////                            AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        ////////                            //ed.WriteMessage("~~Accepted point:{0}\n", AcceptedCenterPoint);
        ////////                        }
        ////////                    }
        ////////                    if (PointContainerWasFound)
        ////////                    {
        ////////                        //Set first Point
        ////////                        ed.WriteMessage("Set first Point");
        ////////                        pl.SetPointAt(0, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));

        ////////                    }
        ////////                    else
        ////////                    {
        ////////                        //Draw header cable
        ////////                        //ed.WriteMessage("Draw header cable");
        ////////                        //ed.WriteMessage("~~Hedaer point:{0}\n", CurrentPoint);
        ////////                        Entity Header = DrawHeader(CurrentPoint);
        ////////                        ObjectId HeaderOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Header, Atend.Control.Enum.AutoCadLayerName.MID_GROUND.ToString());
        ////////                        HeaderOis.Add(HeaderOI);
        ////////                        Atend.Base.Acad.AT_INFO HeaderInfo = new Atend.Base.Acad.AT_INFO(HeaderOI);
        ////////                        HeaderInfo.ParentCode = "";
        ////////                        HeaderInfo.NodeCode = "";
        ////////                        HeaderInfo.ProductCode = 0;
        ////////                        HeaderInfo.NodeType = (int)Atend.Control.Enum.ProductType.HeaderCabel;
        ////////                        HeaderInfo.Insert();

        ////////                    }
        ////////                    //ed.WriteMessage("Go to add vertex : {0}\n", pl.NumberOfVertices);
        ////////                    jig.AddLatestVertex();

        ////////                }
        ////////                else
        ////////                {
        ////////                    //ed.WriteMessage("Go to add vertex : {0}\n", pl.NumberOfVertices);
        ////////                    jig.AddLatestVertex();

        ////////                }

        ////////            }
        ////////        }
        ////////        bComplete = (res.Status == PromptStatus.None);
        ////////        if (bComplete)
        ////////        {
        ////////            Polyline pl = jig.GetEntity() as Polyline;
        ////////            if (pl != null)
        ////////            {
        ////////                //ed.WriteMessage("Go to add vertex : {0}\n", pl.NumberOfVertices);
        ////////                jig.RemoveLastVertex();

        ////////                //------------------
        ////////                Point3d CurrentPoint = new Point3d(pl.GetPoint3dAt(pl.NumberOfVertices - 1).X, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Y, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Z);
        ////////                //ed.WriteMessage("~~~~CurrentPoint:{0}\n", CurrentPoint);
        ////////                System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CurrentPoint);
        ////////                System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.GroundCabel);
        ////////                bool PointContainerWasFound = false;
        ////////                Point3d AcceptedCenterPoint = Point3d.Origin;
        ////////                foreach (System.Data.DataRow dr in PointContainerList.Rows)
        ////////                {
        ////////                    DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        ////////                    if (drs.Length != 0)
        ////////                    {
        ////////                        PointContainerWasFound = true;
        ////////                        HeaderOis.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        ////////                        string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        ////////                        string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        ////////                        string y = StrTemp[1];
        ////////                        string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        ////////                        AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        ////////                        //ed.WriteMessage("~~Accepted point:{0}\n", AcceptedCenterPoint);
        ////////                    }
        ////////                }
        ////////                if (PointContainerWasFound)
        ////////                {
        ////////                    //Set first Point
        ////////                    ed.WriteMessage("Set first Point");
        ////////                    pl.SetPointAt(pl.NumberOfVertices - 1, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));

        ////////                }
        ////////                else
        ////////                {
        ////////                    //Draw header cable
        ////////                    ed.WriteMessage("Draw header cable");
        ////////                    //ed.WriteMessage("~~Hedaer point:{0}\n", CurrentPoint);
        ////////                    Entity Header = DrawHeader(CurrentPoint);
        ////////                    ObjectId HeaderOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Header, Atend.Control.Enum.AutoCadLayerName.MID_GROUND.ToString());
        ////////                    HeaderOis.Add(HeaderOI);
        ////////                    Atend.Base.Acad.AT_INFO HeaderInfo = new Atend.Base.Acad.AT_INFO(HeaderOI);
        ////////                    HeaderInfo.ParentCode = "";
        ////////                    HeaderInfo.NodeCode = "";
        ////////                    HeaderInfo.ProductCode = 0;
        ////////                    HeaderInfo.NodeType = (int)Atend.Control.Enum.ProductType.HeaderCabel;
        ////////                    HeaderInfo.Insert();

        ////////                }
        ////////                //------------------


        ////////            }
        ////////        }
        ////////    } while (bSuccess && !bComplete);
        ////////    if (bComplete)
        ////////    {
        ////////        Database db = doc.Database;
        ////////        Transaction tr = db.TransactionManager.StartTransaction();
        ////////        using (tr)
        ////////        {
        ////////            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead, false);
        ////////            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite, false);
        ////////            btr.AppendEntity(jig.GetEntity());
        ////////            tr.AddNewlyCreatedDBObject(jig.GetEntity(), true);
        ////////            tr.Commit();
        ////////        }
        ////////    }
        ////////}


        //~~~~~~~~~~~~~~~~~~~~ Acess Part ~~~~~~~~~~~~~~~~~~~~~~//


        #region Access Draw Pole

        [CommandMethod("ACSPOLE")]
        //public void AccessDrawPole()
        //{
        //    Document doc = Application.DocumentManager.MdiActiveDocument;
        //    Editor ed = doc.Editor;
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    ObjectId NewPoleObjectId = ObjectId.Null;
        //    ObjectIdCollection NewConsolObjectIds = new ObjectIdCollection();


        //    using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
        //    {
        //        bool conti = true;
        //        int i = 0;
        //        Atend.Global.Acad.AcadJigs.DrawPoleJig drawPoleJig = new Atend.Global.Acad.AcadJigs.DrawPoleJig(Atend.Base.Acad.AcadGlobal.PoleData.dConsolCount);

        //        //ed.WriteMessage("ConsolCount was sent {0} \n", Atend.Base.Acad.AcadGlobal.dConsolCount);

        //        while (conti)
        //        {

        //            PromptResult pr;

        //            pr = ed.Drag(drawPoleJig);

        //            if (pr.Status == PromptStatus.OK && !drawPoleJig.GetAngle)
        //            {

        //                drawPoleJig.GetPoint = false;
        //                drawPoleJig.GetAngle = true;

        //            }
        //            else if (pr.Status == PromptStatus.OK && drawPoleJig.GetAngle)
        //            {

        //                conti = false;
        //                List<Entity> entities = drawPoleJig.GetEntities();

        //                #region Save data here
        //                foreach (Entity ent in entities)
        //                {
        //                    object productType = null;
        //                    Entity newEntity = ent;
        //                    Atend.Global.Acad.AcadJigs.DrawPoleJig.MyPolyline myPoly = ent as Atend.Global.Acad.AcadJigs.DrawPoleJig.MyPolyline;
        //                    if (myPoly.AdditionalDictionary.ContainsKey("ProductType"))
        //                    {
        //                        myPoly.AdditionalDictionary.TryGetValue("ProductType", out productType);
        //                    }
        //                    else
        //                    {
        //                        return;
        //                    }
        //                    //ed.WriteMessage("ProductType= " + productType.ToString() + "\n");

        //                    if (Convert.ToInt32(productType) == (int)Atend.Control.Enum.ProductType.Pole)
        //                    {

        //                        // add extention data
        //                        NewPoleObjectId = Atend.Global.Acad.Global.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
        //                        //ed.WriteMessage("Pole Objectid is {0} \n", NewPoleObjectId);
        //                        AccessSavePoleData();

        //                        Atend.Base.Acad.AT_INFO at_info = new Atend.Base.Acad.AT_INFO();

        //                        at_info.ParentCode = "";
        //                        at_info.NodeCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.Code.ToString();
        //                        at_info.NodeType = Convert.ToInt32(productType);
        //                        at_info.ProductCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.ProductCode.ToString();
        //                        at_info.SelectedObjectId = ent.ObjectId;
        //                        at_info.Insert();


        //                        // ed.WriteMessage("Extension was done \n");
        //                    }
        //                    else if (Convert.ToInt32(productType) == (int)Atend.Control.Enum.ProductType.Consol)
        //                    {
        //                        // add extention data
        //                        //ed.WriteMessage("The Entity Is Consol\n");
        //                        Atend.Base.Design.DPackage package = (Atend.Base.Design.DPackage)Atend.Base.Acad.AcadGlobal.PoleData.dPackages[i];
        //                        bool IsWeek = false;
        //                        Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(package.ProductCode);


        //                        switch (eConsol.VoltageLevel)
        //                        {
        //                            case 20000:
        //                                IsWeek = false;
        //                                break;
        //                            case 11000:
        //                                IsWeek = false;
        //                                break;
        //                            case 33000:
        //                                IsWeek = false;
        //                                break;
        //                            case 400:
        //                                IsWeek = true;
        //                                break;
        //                        }
        //                        string LayerName;
        //                        if (IsWeek)
        //                        {
        //                            LayerName = Atend.Control.Enum.AutoCadLayerName.WEEK_AIR.ToString();
        //                        }
        //                        else
        //                        {
        //                            LayerName = Atend.Control.Enum.AutoCadLayerName.MID_AIR.ToString();
        //                        }

        //                        //determine type of consol and change its entity color

        //                        Atend.Base.Equipment.EConsol currentConsol =
        //                            Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(package.ProductCode);

        //                        switch (currentConsol.ConsolType)
        //                        {
        //                            case 0:
        //                                ent.ColorIndex = 30;
        //                                break;
        //                            case 1:
        //                                ent.ColorIndex = 180;
        //                                break;
        //                            case 2:
        //                                ent.ColorIndex = 44;
        //                                break;
        //                            case 3:
        //                                ent.ColorIndex = 220;
        //                                break;
        //                        }

        //                        //end of determining conso, type

        //                        ObjectId NewConsolObjectID = Atend.Global.Acad.Global.DrawEntityOnScreen(ent, LayerName);
        //                        //ed.WriteMessage("CONSOL Objectid is {0} \n", NewConsolObjectID);
        //                        NewConsolObjectIds.Add(NewConsolObjectID);
        //                        Atend.Base.Design.DConsol consol = new Atend.Base.Design.DConsol();
        //                        ed.WriteMessage("ConsolCount= " + Atend.Base.Acad.AcadGlobal.dConsolCode.Count.ToString() + "\n");
        //                        consol.Code = (Guid)Atend.Base.Acad.AcadGlobal.dConsolCode[i];


        //                        //Atend.Base.Design.DPackage package = (Atend.Base.Design.DPackage)Atend.Base.Acad.AcadGlobal.dPackages[i];
        //                        consol.LoadCode = 0;
        //                        consol.IsExistance = package.IsExistance;
        //                        consol.ProductCode = package.ProductCode;
        //                        consol.ParentCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                        consol.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //                        //ed.WriteMessage("ConsolCode= " + consol.Code.ToString() + "\n");
        //                        //ed.WriteMessage("ParentCode= " + Atend.Base.Acad.AcadGlobal.dNode.Code.ToString() + "\n");
        //                        consol.AccessInsert();
        //                        Atend.Base.Acad.AT_INFO at_info = new Atend.Base.Acad.AT_INFO();

        //                        at_info.ParentCode = Atend.Base.Acad.AcadGlobal.dNode.Code.ToString();
        //                        at_info.NodeCode = package.Code.ToString();
        //                        at_info.NodeType = Convert.ToInt32(productType);
        //                        at_info.ProductCode = package.ProductCode.ToString();
        //                        at_info.SelectedObjectId = ent.ObjectId;
        //                        at_info.Insert();

        //                        //insert pole as a sub for each one

        //                        Atend.Base.Acad.AT_SUB at_sub = new Atend.Base.Acad.AT_SUB();
        //                        at_sub.SelectedObjectId = NewConsolObjectID;
        //                        at_sub.SubIdCollection.Add(NewPoleObjectId);
        //                        at_sub.Insert();


        //                        i++;
        //                        // ed.WriteMessage("Extension was done \n");

        //                    }//End of DRaw consol

        //                }// Draw Finished

        //                //insert consols as a sub for pole

        //                Atend.Base.Acad.AT_SUB at_sub1 = new Atend.Base.Acad.AT_SUB();
        //                at_sub1.SelectedObjectId = NewPoleObjectId;
        //                at_sub1.SubIdCollection = NewConsolObjectIds;
        //                at_sub1.Insert();


        //                #endregion

        //            }
        //            else
        //            {
        //                conti = false;
        //            }
        //        }


        //    }

        //}

        ////private bool AccessSavePoleData()
        ////{

        ////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        ////    Atend.Base.Design.NodeTransaction nodeTran = new Atend.Base.Design.NodeTransaction();

        ////    Database db = HostApplicationServices.WorkingDatabase;

        ////    //using (Transaction tr = db.TransactionManager.StartTransaction())
        ////    //{
        ////    //ed.WriteMessage("\n~~~~~~~~~~~~~~ Start Pole Process ~~~~~~~~~~~~~~\n");
        ////    Atend.Base.Acad.AcadGlobal.PoleData.dNode.LoadCode = 0;

        ////    //EXTRA
        ////    //if (nodeTran.AccessInsertPole())
        ////    //{

        ////    //    ed.WriteMessage("Save Pole Information Done. \n");

        ////    //}
        ////    //else
        ////    //{

        ////    //    ed.WriteMessage("Save Pole Information Failed. \n");
        ////    //    return false;

        ////    //}

        ////    //ed.WriteMessage("\n~~~~~~~~~~~~~~ End Pole Process ~~~~~~~~~~~~~~\n");
        ////    return true;

        ////    //}


        ////}

        #endregion


        #region Access Draw Pole-Circle

        //////[CommandMethod("ACSPOLECircle")]
        //////public void AccessDrawPoleCircle()
        //////{

        //////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //////    bool conti = true;

        //////    ObjectIdCollection NewConsolObjectIds = new ObjectIdCollection();

        //////    ObjectId NewPoleDrawn = ObjectId.Null;

        //////    using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
        //////    {


        //////        Atend.Global.Acad.AcadJigs.DrawPoleCircleJig poleCircle = new Atend.Global.Acad.AcadJigs.DrawPoleCircleJig(
        //////            Atend.Base.Acad.AcadGlobal.PoleData.dConsolCount);

        //////        while (conti)
        //////        {

        //////            PromptResult pr = ed.Drag(poleCircle);

        //////            if (pr.Status == PromptStatus.OK)
        //////            {
        //////                conti = false;
        //////                //ed.WriteMessage("Draw finished. \n");
        //////                #region Save Data Here

        //////                List<Entity> Entities = poleCircle.GetEntities();
        //////                //ed.WriteMessage("Number of entities : {0} \n", Entities.Count);
        //////                bool PoleSaved = false;

        //////                int i = 0;
        //////                foreach (Entity ent in Entities)
        //////                {

        //////                    if (!PoleSaved)
        //////                    {
        //////                        ed.WriteMessage("Pole was not saved . \n");
        //////                        //Save Pole here
        //////                        Atend.Global.Acad.AcadJigs.DrawPoleCircleJig.MyCircle MyCirclePole = ent as Atend.Global.Acad.AcadJigs.DrawPoleCircleJig.MyCircle;
        //////                        if (MyCirclePole != null)
        //////                        {
        //////                            //ed.WriteMessage("pole object found \n");
        //////                            object productType = null;
        //////                            if (MyCirclePole.AdditionalDictionary.ContainsKey("ProductType"))
        //////                            {
        //////                                MyCirclePole.AdditionalDictionary.TryGetValue("ProductType", out productType);
        //////                                if (Convert.ToInt32(productType) == (int)Atend.Control.Enum.ProductType.Pole)
        //////                                {
        //////                                    //ed.WriteMessage("pole code found \n");
        //////                                    #region SavePoleData

        //////                                    if (AccessSavePoleData())
        //////                                    {
        //////                                        //ed.WriteMessage("Save Pole data done \n");
        //////                                        NewPoleDrawn = Atend.Global.Acad.Global.DrawEntityOnScreen(ent,
        //////                                            Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

        //////                                        Atend.Base.Acad.AT_INFO pole_info = new Atend.Base.Acad.AT_INFO(NewPoleDrawn);
        //////                                        pole_info.ParentCode = "";
        //////                                        pole_info.NodeCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.Code.ToString();
        //////                                        pole_info.NodeType = (int)Atend.Control.Enum.ProductType.Pole;
        //////                                        pole_info.ProductCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.ProductCode.ToString();
        //////                                        pole_info.Insert();

        //////                                        PoleSaved = true;

        //////                                    }
        //////                                    else
        //////                                    {
        //////                                        return;
        //////                                    }

        //////                                    #endregion

        //////                                }
        //////                                else
        //////                                {
        //////                                    return;
        //////                                }

        //////                            }
        //////                            else
        //////                            {
        //////                                return;
        //////                            }
        //////                        }
        //////                    }
        //////                    else
        //////                    {
        //////                        //ed.WriteMessage("Pole was saved . \n");
        //////                        //Save Consol Here

        //////                        Atend.Global.Acad.AcadJigs.DrawPoleCircleJig.MyPolyLine MyConsol = ent as Atend.Global.Acad.AcadJigs.DrawPoleCircleJig.MyPolyLine;
        //////                        if (MyConsol != null)
        //////                        {
        //////                            object productType = null;
        //////                            if (MyConsol.AdditionalDictionary.ContainsKey("ProductType"))
        //////                            {
        //////                                MyConsol.AdditionalDictionary.TryGetValue("ProductType", out productType);
        //////                                if (Convert.ToInt32(productType) == (int)Atend.Control.Enum.ProductType.Consol)
        //////                                {

        //////                                    //ed.WriteMessage("The Entity Is Consol\n");
        //////                                    Atend.Base.Design.DPackage package = (Atend.Base.Design.DPackage)Atend.Base.Acad.AcadGlobal.PoleData.dPackages[i];
        //////                                    bool IsWeek = false;
        //////                                    Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(package.ProductCode);


        //////                                    switch (eConsol.VoltageLevel)
        //////                                    {
        //////                                        case 20000:
        //////                                            IsWeek = false;
        //////                                            break;
        //////                                        case 11000:
        //////                                            IsWeek = false;
        //////                                            break;
        //////                                        case 33000:
        //////                                            IsWeek = false;
        //////                                            break;
        //////                                        case 400:
        //////                                            IsWeek = true;
        //////                                            break;
        //////                                    }
        //////                                    string LayerName;
        //////                                    if (IsWeek)
        //////                                    {
        //////                                        LayerName = Atend.Control.Enum.AutoCadLayerName.WEEK_AIR.ToString();
        //////                                    }
        //////                                    else
        //////                                    {
        //////                                        LayerName = Atend.Control.Enum.AutoCadLayerName.MID_AIR.ToString();
        //////                                    }

        //////                                    //determine type of consol and change its entity color

        //////                                    Atend.Base.Equipment.EConsol currentConsol =
        //////                                        Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(package.ProductCode);

        //////                                    switch (currentConsol.ConsolType)
        //////                                    {
        //////                                        case 0:
        //////                                            ent.ColorIndex = 30;
        //////                                            break;
        //////                                        case 1:
        //////                                            ent.ColorIndex = 180;
        //////                                            break;
        //////                                        case 2:
        //////                                            ent.ColorIndex = 44;
        //////                                            break;
        //////                                        case 3:
        //////                                            ent.ColorIndex = 220;
        //////                                            break;
        //////                                    }

        //////                                    //end of determining conso, type

        //////                                    ObjectId NewConsolObjectID = Atend.Global.Acad.Global.DrawEntityOnScreen(ent, LayerName);
        //////                                    //ed.WriteMessage("CONSOL Objectid is {0} \n", NewConsolObjectID);
        //////                                    NewConsolObjectIds.Add(NewConsolObjectID);
        //////                                    Atend.Base.Design.DConsol consol = new Atend.Base.Design.DConsol();
        //////                                    //ed.WriteMessage("ConsolCount= " + Atend.Base.Acad.AcadGlobal.dConsolCode.Count.ToString() + "\n");
        //////                                    consol.Code = (Guid)Atend.Base.Acad.AcadGlobal.PoleData.dConsolCode[i];


        //////                                    //Atend.Base.Design.DPackage package = (Atend.Base.Design.DPackage)Atend.Base.Acad.AcadGlobal.dPackages[i];

        //////                                    consol.IsExistance = Convert.ToByte(package.IsExistance);
        //////                                    consol.ProductCode = package.ProductCode;
        //////                                    consol.ParentCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.Code;
        //////                                    consol.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //////                                    //ed.WriteMessage("ConsolCode= " + consol.Code.ToString() + "\n");
        //////                                    //ed.WriteMessage("ParentCode= " + Atend.Base.Acad.AcadGlobal.dNode.Code.ToString() + "\n");
        //////                                    consol.AccessInsert();
        //////                                    Atend.Base.Acad.AT_INFO at_info = new Atend.Base.Acad.AT_INFO();

        //////                                    at_info.ParentCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.Code.ToString();
        //////                                    at_info.NodeCode = package.Code.ToString();
        //////                                    at_info.NodeType = Convert.ToInt32(productType);
        //////                                    at_info.ProductCode = package.ProductCode;
        //////                                    at_info.SelectedObjectId = NewConsolObjectID;
        //////                                    at_info.Insert();

        //////                                    //insert pole as a sub for each one

        //////                                    Atend.Base.Acad.AT_SUB at_sub = new Atend.Base.Acad.AT_SUB();
        //////                                    at_sub.SelectedObjectId = NewConsolObjectID;
        //////                                    at_sub.SubIdCollection.Add(NewPoleDrawn);
        //////                                    at_sub.Insert();


        //////                                    i++;

        //////                                }
        //////                            }

        //////                        }

        //////                    }

        //////                }// end of foreach

        //////                if (PoleSaved)
        //////                {
        //////                    Atend.Base.Acad.AT_SUB polesub = new Atend.Base.Acad.AT_SUB(NewPoleDrawn);
        //////                    foreach (ObjectId oi in NewConsolObjectIds)
        //////                    {

        //////                        polesub.SubIdCollection.Add(oi);
        //////                    }
        //////                    polesub.Insert();
        //////                }

        //////                #endregion

        //////            }
        //////            else
        //////            {
        //////                conti = false;
        //////            }


        //////        }
        //////    }
        //////}

        #endregion


        #region Access Draw Pole-Polygon

        ////////[CommandMethod("ACSPolePolygon")]

        ////////public void AccessDrawPolePolygon()
        ////////{

        ////////    bool conti = true, PoleSaved = false;
        ////////    ObjectId NewPoleObjectId = ObjectId.Null;
        ////////    ObjectIdCollection ids = new ObjectIdCollection();
        ////////    int i = 0;


        ////////    using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
        ////////    {


        ////////        Atend.Global.Acad.AcadJigs.DrawPolePolygonJig polePolygon = new Atend.Global.Acad.AcadJigs.DrawPolePolygonJig(
        ////////            Atend.Base.Acad.AcadGlobal.PoleData.dConsolCount);

        ////////        while (conti)
        ////////        {

        ////////            PromptResult pr = ed.Drag(polePolygon);

        ////////            if (pr.Status == PromptStatus.OK)
        ////////            {

        ////////                conti = false;

        ////////                #region save data here

        ////////                List<Entity> Entities = polePolygon.GetEntities();

        ////////                foreach (Entity ent in Entities)
        ////////                {
        ////////                    object productType = null;
        ////////                    Entity newEntity = ent;
        ////////                    Atend.Global.Acad.AcadJigs.DrawPolePolygonJig.MyPolyLine myPoly = ent as Atend.Global.Acad.AcadJigs.DrawPolePolygonJig.MyPolyLine;
        ////////                    if (myPoly.AdditionalDictionary.ContainsKey("ProductType"))
        ////////                    {
        ////////                        myPoly.AdditionalDictionary.TryGetValue("ProductType", out productType);
        ////////                    }
        ////////                    else
        ////////                    {
        ////////                        return;
        ////////                    }
        ////////                    //ed.WriteMessage("ProductType= " + productType.ToString() + "\n");

        ////////                    if (Convert.ToInt32(productType) == (int)Atend.Control.Enum.ProductType.Pole)
        ////////                    {

        ////////                        // add extention data
        ////////                        NewPoleObjectId = Atend.Global.Acad.Global.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
        ////////                        //ed.WriteMessage("Pole Objectid is {0} \n", NewPoleObjectId);
        ////////                        if (AccessSavePoleData())
        ////////                        {

        ////////                            Atend.Base.Acad.AT_INFO at_info = new Atend.Base.Acad.AT_INFO();

        ////////                            at_info.ParentCode = "";
        ////////                            at_info.NodeCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.Code.ToString();
        ////////                            at_info.NodeType = Convert.ToInt32(productType);
        ////////                            at_info.ProductCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.ProductCode.ToString();
        ////////                            at_info.SelectedObjectId = ent.ObjectId;
        ////////                            at_info.Insert();

        ////////                            PoleSaved = true;

        ////////                        }
        ////////                        // ed.WriteMessage("Extension was done \n");
        ////////                    }
        ////////                    else if (Convert.ToInt32(productType) == (int)Atend.Control.Enum.ProductType.Consol)
        ////////                    {
        ////////                        // add extention data
        ////////                        //ed.WriteMessage("The Entity Is Consol\n");
        ////////                        Atend.Base.Design.DPackage package = (Atend.Base.Design.DPackage)Atend.Base.Acad.AcadGlobal.PoleData.dPackages[i];
        ////////                        bool IsWeek = false;
        ////////                        Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(package.ProductCode);


        ////////                        switch (eConsol.VoltageLevel)
        ////////                        {
        ////////                            case 20000:
        ////////                                IsWeek = false;
        ////////                                break;
        ////////                            case 11000:
        ////////                                IsWeek = false;
        ////////                                break;
        ////////                            case 33000:
        ////////                                IsWeek = false;
        ////////                                break;
        ////////                            case 400:
        ////////                                IsWeek = true;
        ////////                                break;
        ////////                        }
        ////////                        string LayerName;
        ////////                        if (IsWeek)
        ////////                        {
        ////////                            LayerName = Atend.Control.Enum.AutoCadLayerName.WEEK_AIR.ToString();
        ////////                        }
        ////////                        else
        ////////                        {
        ////////                            LayerName = Atend.Control.Enum.AutoCadLayerName.MID_AIR.ToString();
        ////////                        }

        ////////                        //determine type of consol and change its entity color

        ////////                        Atend.Base.Equipment.EConsol currentConsol =
        ////////                            Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(package.ProductCode);

        ////////                        switch (currentConsol.ConsolType)
        ////////                        {
        ////////                            case 0:
        ////////                                ent.ColorIndex = 30;
        ////////                                break;
        ////////                            case 1:
        ////////                                ent.ColorIndex = 180;
        ////////                                break;
        ////////                            case 2:
        ////////                                ent.ColorIndex = 44;
        ////////                                break;
        ////////                            case 3:
        ////////                                ent.ColorIndex = 220;
        ////////                                break;
        ////////                        }

        ////////                        //end of determining conso, type

        ////////                        ObjectId NewConsolObjectID = Atend.Global.Acad.Global.DrawEntityOnScreen(ent, LayerName);
        ////////                        //ed.WriteMessage("CONSOL Objectid is {0} \n", NewConsolObjectID);
        ////////                        ids.Add(NewConsolObjectID);
        ////////                        Atend.Base.Design.DConsol consol = new Atend.Base.Design.DConsol();
        ////////                        //ed.WriteMessage("ConsolCount= " + Atend.Base.Acad.AcadGlobal.dConsolCode.Count.ToString() + "\n");
        ////////                        consol.Code = (Guid)Atend.Base.Acad.AcadGlobal.PoleData.dConsolCode[i];


        ////////                        //Atend.Base.Design.DPackage package = (Atend.Base.Design.DPackage)Atend.Base.Acad.AcadGlobal.dPackages[i];

        ////////                        consol.IsExistance = Convert.ToByte(package.IsExistance);
        ////////                        consol.ProductCode = package.ProductCode;
        ////////                        consol.ParentCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.Code;
        ////////                        consol.DesignCode = Atend.Control.Common.SelectedDesignCode;
        ////////                        //ed.WriteMessage("ConsolCode= " + consol.Code.ToString() + "\n");
        ////////                        //ed.WriteMessage("ParentCode= " + Atend.Base.Acad.AcadGlobal.dNode.Code.ToString() + "\n");
        ////////                        consol.AccessInsert();
        ////////                        Atend.Base.Acad.AT_INFO at_info = new Atend.Base.Acad.AT_INFO();

        ////////                        at_info.ParentCode = Atend.Base.Acad.AcadGlobal.PoleData.dNode.Code.ToString();
        ////////                        at_info.NodeCode = package.Code.ToString();
        ////////                        at_info.NodeType = Convert.ToInt32(productType);
        ////////                        at_info.ProductCode = package.ProductCode;
        ////////                        at_info.SelectedObjectId = NewConsolObjectID;
        ////////                        at_info.Insert();

        ////////                        //insert pole as a sub for each one

        ////////                        Atend.Base.Acad.AT_SUB at_sub = new Atend.Base.Acad.AT_SUB();
        ////////                        at_sub.SelectedObjectId = NewConsolObjectID;
        ////////                        at_sub.SubIdCollection.Add(NewPoleObjectId);
        ////////                        at_sub.Insert();


        ////////                        i++;
        ////////                        // ed.WriteMessage("Extension was done \n");

        ////////                    }//End of DRaw consol

        ////////                }// Draw Finished


        ////////                if (PoleSaved)
        ////////                {
        ////////                    Atend.Base.Acad.AT_SUB poleSub = new Atend.Base.Acad.AT_SUB(NewPoleObjectId);
        ////////                    foreach (ObjectId oi in ids)
        ////////                    {
        ////////                        poleSub.SubIdCollection.Add(oi);
        ////////                    }
        ////////                    poleSub.Insert();
        ////////                }

        ////////                #endregion


        ////////            }


        ////////        }

        ////////    }

        ////////}

        #endregion


        #region Access Draw Transformer

        ////////[CommandMethod("Transformer")]
        //////public void AccessDrawTransformer(Entity PostContainerEntity, int ProductCode)
        //////{

        //////    bool conti = true;
        //////    Atend.Global.Acad.AcadJigs.DrawTransformerJig transformerJig = new Atend.Global.Acad.AcadJigs.DrawTransformerJig(PostContainerEntity);
        //////    PromptResult pr;

        //////    while (conti)
        //////    {
        //////        pr = ed.Drag(transformerJig);
        //////        if (pr.Status == PromptStatus.OK)
        //////        {
        //////            conti = false;
        //////            //~~~~~~~~~~~~~~~~
        //////            #region Save data here


        //////            List<Entity> Entities = transformerJig.GetEntities();

        //////            object TransformerCode = null;
        //////            object TransformerParent = null;
        //////            Atend.Base.Acad.AcadGlobal.PostEquips ItemForDelet = null;
        //////            foreach (Atend.Base.Acad.AcadGlobal.PostEquips p in Atend.Base.Acad.AcadGlobal.PostEquipInserted)
        //////            {
        //////                if (p.ProductCode == ProductCode && p.ProductType == (int)Atend.Control.Enum.ProductType.Transformer)
        //////                {

        //////                    TransformerCode = p.CodeGuid.ToString();
        //////                    TransformerParent = p.ParentCode.ToString();
        //////                    ItemForDelet = p;

        //////                }

        //////            }
        //////            //ed.WriteMessage("Item for delete code:{0}\n", ItemForDelet.CodeGuid);

        //////            Atend.Base.Acad.AcadGlobal.PostEquipInserted.Remove(ItemForDelet);

        //////            //foreach (Atend.Base.Acad.AcadGlobal.PostEquips p in Atend.Base.Acad.AcadGlobal.PostEquipInserted)
        //////            //{

        //////            //ed.WriteMessage("parent:{0} , code:{1} , type:{2} \n", p.ParentCode, p.CodeGuid, p.ProductType);
        //////            //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        //////            //}


        //////            if (TransformerCode != null && TransformerParent != null)
        //////            {

        //////                ObjectIdCollection OIC = new ObjectIdCollection();

        //////                foreach (Entity ent in Entities)
        //////                {
        //////                    ObjectId NewEntOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.MID_GROUND.ToString());

        //////                    Atend.Base.Acad.AT_INFO EntInfo = new Atend.Base.Acad.AT_INFO(NewEntOI);
        //////                    EntInfo.ParentCode = TransformerParent.ToString();
        //////                    EntInfo.NodeCode = "";



        //////                    Atend.Global.Acad.AcadJigs.MyPolyLine poly = ent as Atend.Global.Acad.AcadJigs.MyPolyLine;
        //////                    object ProductType = null;
        //////                    if (poly != null)
        //////                    {
        //////                        if (poly.AdditionalDictionary.ContainsKey("ProductType"))
        //////                        {
        //////                            poly.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
        //////                        }

        //////                    }
        //////                    else
        //////                    {
        //////                        Atend.Global.Acad.AcadJigs.MyLine lin = ent as Atend.Global.Acad.AcadJigs.MyLine;
        //////                        if (lin != null)
        //////                        {

        //////                            if (lin.AdditionalDictionary.ContainsKey("ProductType"))
        //////                            {
        //////                                lin.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
        //////                            }


        //////                        }
        //////                        else
        //////                        {
        //////                            Atend.Global.Acad.AcadJigs.MyCircle cir = ent as Atend.Global.Acad.AcadJigs.MyCircle;
        //////                            if (cir != null)
        //////                            {

        //////                                if (cir.AdditionalDictionary.ContainsKey("ProductType"))
        //////                                {
        //////                                    cir.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
        //////                                }


        //////                            }
        //////                        }
        //////                    }




        //////                    if (ProductType != null)
        //////                    {
        //////                        EntInfo.NodeType = Convert.ToInt32(ProductType);
        //////                    }
        //////                    else
        //////                    {
        //////                        EntInfo.NodeType = 0;
        //////                    }
        //////                    EntInfo.ProductCode = 0;
        //////                    EntInfo.Insert();

        //////                    OIC.Add(NewEntOI);

        //////                }

        //////                ObjectId TransformerGroupOI = Atend.Global.Acad.Global.MakeGroup(TransformerCode.ToString(), OIC);

        //////                Atend.Base.Acad.AT_INFO GroupInfo = new Atend.Base.Acad.AT_INFO(TransformerGroupOI);
        //////                GroupInfo.ParentCode = TransformerParent.ToString();
        //////                GroupInfo.NodeCode = TransformerCode.ToString();
        //////                GroupInfo.NodeType = (int)Atend.Control.Enum.ProductType.Transformer;
        //////                GroupInfo.ProductCode = ProductCode;
        //////                GroupInfo.Insert();


        //////                Atend.Base.Acad.AT_SUB.AddToAT_SUB(TransformerGroupOI, PostContainerEntity.ObjectId);


        //////            }


        //////            #endregion
        //////            //~~~~~~~~~~~~~~~~
        //////        }
        //////        else
        //////        {
        //////            conti = false;
        //////        }
        //////    }


        //////}


        #endregion


        #region Access Draw Conductor
        // ~~~~~~~~~~~~ Start Draw Conductor Code Part ~~~~~~~~~~~~~~~~~~

        //public Guid NodeGuid;

        ////////[CommandMethod("ACSCONDUCTOR")]
        ////////public void AccessDrawConductor()
        ////////{

        ////////    Point3dCollection p3Collection = new Point3dCollection();
        ////////    bool FirstPoint = true;
        ////////    bool conti = true;
        ////////    Point3d StartPoint = Point3d.Origin, sp;
        ////////    Database db = HostApplicationServices.WorkingDatabase;
        ////////    Document doc = Application.DocumentManager.MdiActiveDocument;
        ////////    bool NeedArc1 = false, NeedArc2 = false, IsWeek1 = false, IsWeek2 = false;
        ////////    //double ConsolVoltage = 0;
        ////////    Atend.Base.Equipment.EConsol eConsol;
        ////////    ObjectId newArc1 = ObjectId.Null, newArc2 = ObjectId.Null;

        ////////    ObjectId FirstConsol = ObjectId.Null, SecondConsol = ObjectId.Null;


        ////////    //Acad.AcadJigs.DrawConductorJig drawConductor = new AcadJigs.DrawConductorJig();

        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        ////////    PromptEntityOptions peo = new PromptEntityOptions("");

        ////////    while (conti)
        ////////    {
        ////////        if (FirstPoint)
        ////////        {

        ////////            peo.Message = "\n Select Start Container of the Conductor: ";

        ////////            PromptEntityResult per = ed.GetEntity(peo);

        ////////            if (per.Status == PromptStatus.OK)
        ////////            {
        ////////                Atend.Base.Acad.AT_INFO at_info =
        ////////                Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
        ////////                FirstConsol = per.ObjectId;

        ////////                if (at_info.NodeType != (int)Atend.Control.Enum.ProductType.Consol)
        ////////                {
        ////////                    return;
        ////////                }
        ////////                else
        ////////                {

        ////////                    Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.LeftNodeCode = new Guid(at_info.ParentCode);
        ////////                    eConsol = Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(new Guid(at_info.ProductCode.ToString()));

        ////////                }

        ////////                //ed.WriteMessage("\nConsol One Volateg : {0}\n", eConsol.VoltageLevel);

        ////////                switch (eConsol.VoltageLevel)
        ////////                {
        ////////                    //case is >400:
        ////////                    //    IsWeek1 = false;
        ////////                    //    break;
        ////////                    //case 11:
        ////////                    //    IsWeek1 = false;
        ////////                    //    break;
        ////////                    //case 33:
        ////////                    //    IsWeek1 = false;
        ////////                    //    break;
        ////////                    case 400:
        ////////                        IsWeek1 = true;
        ////////                        break;
        ////////                    default:
        ////////                        IsWeek1 = false;
        ////////                        break;
        ////////                }

        ////////                //ed.WriteMessage("\nConsol One Tension : {0}\n", eConsol.ConsolType);
        ////////                switch (eConsol.ConsolType)
        ////////                {
        ////////                    case 0:
        ////////                        NeedArc1 = true;
        ////////                        break;
        ////////                    case 1:
        ////////                        NeedArc1 = true;
        ////////                        break;
        ////////                    case 2:
        ////////                        NeedArc1 = false;
        ////////                        break;
        ////////                    case 3:
        ////////                        NeedArc1 = false;
        ////////                        break;
        ////////                }

        ////////                //Polyline pline = (Polyline)GetEntityByObjectID(per.ObjectId);
        ////////                Polyline pline = GetEntityByObjectID(per.ObjectId) as Polyline;

        ////////                if (pline != null)
        ////////                {

        ////////                    StartPoint = Atend.Global.Acad.UAcad.CenterOfEntity(pline);

        ////////                    Atend.Base.Acad.AcadGlobal.dConductorInformation.Point1 = StartPoint;

        ////////                    //ed.WriteMessage("\nStart Point {0} \n", StartPoint);

        ////////                    FirstPoint = false;

        ////////                }

        ////////            }
        ////////            else
        ////////            {
        ////////                conti = false;
        ////////            }
        ////////        }
        ////////        else if (!FirstPoint)
        ////////        {
        ////////            peo.Message = "\n Select End Container of the Conductor: ";

        ////////            PromptEntityResult per = ed.GetEntity(peo);

        ////////            if (per.Status == PromptStatus.OK)
        ////////            {

        ////////                Atend.Base.Acad.AT_INFO at_info =
        ////////                Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
        ////////                SecondConsol = per.ObjectId;

        ////////                if (at_info.NodeType != (int)Atend.Control.Enum.ProductType.Consol)
        ////////                {
        ////////                    return;
        ////////                }
        ////////                else
        ////////                {
        ////////                    Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.RightNodeCode = new Guid(at_info.ParentCode);
        ////////                    eConsol = Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(new Guid(at_info.ProductCode.ToString()));
        ////////                }


        ////////                //ed.WriteMessage("\nConsol Two Volateg : {0}\n", eConsol.VoltageLevel);

        ////////                switch (eConsol.VoltageLevel)
        ////////                {
        ////////                    //case 20:
        ////////                    //    IsWeek2 = false;
        ////////                    //    break;
        ////////                    //case 11:
        ////////                    //    IsWeek2 = false;
        ////////                    //    break;
        ////////                    //case 33:
        ////////                    //    IsWeek2 = false;
        ////////                    //    break;
        ////////                    case 400:
        ////////                        IsWeek2 = true;
        ////////                        break;
        ////////                    default:
        ////////                        IsWeek2 = false;
        ////////                        break;
        ////////                }

        ////////                //ed.WriteMessage("\nConsol Two tension : {0}\n", eConsol.ConsolType);

        ////////                switch (eConsol.ConsolType)
        ////////                {
        ////////                    case 0:
        ////////                        NeedArc2 = true;
        ////////                        break;
        ////////                    case 1:
        ////////                        NeedArc2 = true;
        ////////                        break;
        ////////                    case 2:
        ////////                        NeedArc2 = false;
        ////////                        break;
        ////////                    case 3:
        ////////                        NeedArc2 = false;
        ////////                        break;
        ////////                }
        ////////                Polyline pline = GetEntityByObjectID(per.ObjectId) as Polyline;

        ////////                if (pline != null)
        ////////                {

        ////////                    sp = Atend.Global.Acad.UAcad.CenterOfEntity(pline);

        ////////                    Atend.Base.Acad.AcadGlobal.dConductorInformation.Point2 = sp;

        ////////                    Line LineEntity = new Line(StartPoint, sp);

        ////////                    if (IsWeek1 == IsWeek2)
        ////////                    {
        ////////                        Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.IsWeek = IsWeek2;
        ////////                        Atend.Design.frmDrawBranch drawBranch = new Atend.Design.frmDrawBranch();
        ////////                        drawBranch.Length = LineEntity.Length;
        ////////                        drawBranch.ShowDialog();
        ////////                        Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.Lenght = drawBranch.Length;


        ////////                        Atend.Base.Equipment.EConductorTip ConductorTip =
        ////////                            Atend.Base.Equipment.EConductorTip.SelectByXCode(
        ////////                            Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.ProductCode);


        ////////                        Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.Number = ConductorTip.Description;
        ////////                        Atend.Base.Acad.AcadGlobal.dConductorInformation.Point1 = LineEntity.StartPoint;
        ////////                        Atend.Base.Acad.AcadGlobal.dConductorInformation.Point2 = LineEntity.EndPoint;


        ////////                        #region save data here
        ////////                        if (AccessSaveConductorData())
        ////////                        {
        ////////                            if (LineEntity != null)
        ////////                            {

        ////////                                string LayerName;
        ////////                                if (IsWeek1)
        ////////                                {
        ////////                                    LayerName = Atend.Control.Enum.AutoCadLayerName.WEEK_AIR.ToString();
        ////////                                }
        ////////                                else
        ////////                                {
        ////////                                    LayerName = Atend.Control.Enum.AutoCadLayerName.MID_AIR.ToString();
        ////////                                }


        ////////                                ObjectId DrawnLine = Atend.Global.Acad.Global.DrawEntityOnScreen(LineEntity, LayerName);

        ////////                                Atend.Base.Acad.AT_INFO at_infoForConsol = new Atend.Base.Acad.AT_INFO();
        ////////                                at_infoForConsol.SelectedObjectId = DrawnLine;
        ////////                                at_infoForConsol.ParentCode = "";
        ////////                                at_infoForConsol.NodeCode = Atend.Base.Acad.AcadGlobal.dBranch.Code.ToString();
        ////////                                at_infoForConsol.NodeType = (int)Atend.Control.Enum.ProductType.Conductor;
        ////////                                at_infoForConsol.ProductCode = Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.ProductCode.ToString();
        ////////                                at_infoForConsol.Insert();

        ////////                                Entity EText = WriteNote(Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.Number, LineEntity.StartPoint, LineEntity.EndPoint);

        ////////                                ObjectId DrawnText = Atend.Global.Acad.Global.DrawEntityOnScreen(EText, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

        ////////                                Atend.Base.Acad.AT_SUB at_sub = new Atend.Base.Acad.AT_SUB();
        ////////                                at_sub.SelectedObjectId = DrawnLine;
        ////////                                at_sub.SubIdCollection.Add(DrawnText);
        ////////                                at_sub.SubIdCollection.Add(FirstConsol);
        ////////                                at_sub.SubIdCollection.Add(SecondConsol);
        ////////                                at_sub.Insert();


        ////////                                Atend.Base.Acad.AT_INFO TextInfo = new Atend.Base.Acad.AT_INFO(DrawnText);
        ////////                                TextInfo.ParentCode = Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.Code.ToString();
        ////////                                TextInfo.NodeCode = "";
        ////////                                TextInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
        ////////                                TextInfo.ProductCode = 0;
        ////////                                TextInfo.Insert();



        ////////                                Atend.Base.Acad.AT_SUB.AddToAT_SUB(DrawnLine, FirstConsol);

        ////////                                Atend.Base.Acad.AT_SUB.AddToAT_SUB(DrawnLine, SecondConsol);





        ////////                                if (NeedArc1)
        ////////                                {
        ////////                                    Arc a = (Arc)CreateArcForSpecialConsol(FirstConsol);



        ////////                                    newArc1 = Atend.Global.Acad.Global.DrawEntityOnScreen(a, LayerName);
        ////////                                    //ed.WriteMessage("1 \n");
        ////////                                    if (newArc1 != ObjectId.Null)
        ////////                                    {
        ////////                                        //ed.WriteMessage("2 \n");
        ////////                                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(newArc1, FirstConsol);

        ////////                                        Atend.Base.Acad.AT_INFO Parentconsol = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(FirstConsol);

        ////////                                        Atend.Base.Acad.AT_INFO at_info1 = new Atend.Base.Acad.AT_INFO();
        ////////                                        at_info1.SelectedObjectId = newArc1;
        ////////                                        at_info1.ParentCode = Parentconsol.NodeCode;
        ////////                                        at_info1.NodeCode = "";
        ////////                                        at_info1.NodeType = (int)Atend.Control.Enum.ProductType.TensionArc;
        ////////                                        at_info1.ProductCode = 0;
        ////////                                        at_info1.Insert();


        ////////                                    }


        ////////                                }

        ////////                                if (NeedArc2)
        ////////                                {
        ////////                                    Arc a = (Arc)CreateArcForSpecialConsol(SecondConsol);

        ////////                                    newArc2 = Atend.Global.Acad.Global.DrawEntityOnScreen(a, LayerName);
        ////////                                    //ed.WriteMessage("1 \n");
        ////////                                    if (newArc2 != ObjectId.Null)
        ////////                                    {
        ////////                                        //ed.WriteMessage("2 \n");
        ////////                                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(newArc2, SecondConsol);

        ////////                                        Atend.Base.Acad.AT_INFO Parentconsol = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(SecondConsol);

        ////////                                        Atend.Base.Acad.AT_INFO at_info1 = new Atend.Base.Acad.AT_INFO();
        ////////                                        at_info1.SelectedObjectId = newArc2;
        ////////                                        at_info1.ParentCode = Parentconsol.NodeCode;
        ////////                                        at_info1.NodeCode = "";
        ////////                                        at_info1.NodeType = (int)Atend.Control.Enum.ProductType.TensionArc;
        ////////                                        at_info1.ProductCode = 0;
        ////////                                        at_info1.Insert();

        ////////                                    }

        ////////                                }

        ////////                                conti = false;

        ////////                            }

        ////////                            Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.LeftNodeCode = NodeGuid;

        ////////                        }
        ////////                        #endregion
        ////////                    }
        ////////                    else
        ////////                    {
        ////////                        //BOTH consol was not the same

        ////////                        //ed.WriteMessage("Both consol was not Correct. \n");
        ////////                    }
        ////////                }

        ////////            }
        ////////            else
        ////////            {
        ////////                conti = false;
        ////////            }
        ////////        }//end if for FirstPoint

        ////////    }

        ////////}

        ////////private bool AccessSaveConductorData()
        ////////{

        ////////    Atend.Base.Design.CoductorTransaction conductorTran = new Atend.Base.Design.CoductorTransaction();

        ////////    if (conductorTran.AccessInsert())
        ////////    {
        ////////        return true;
        ////////    }
        ////////    else
        ////////    {
        ////////        return false;
        ////////    }
        ////////}

        // ~~~~~~~~~~~~ End Draw Conductor Code Part ~~~~~~~~~~~~~~~~~~
        #endregion


        ////////////#region Access Draw GroundPost

        //////////////[CommandMethod("GroundPost")]
        ////////////public Entity AccessDrawGroundPost(int GroundPostProductCode)
        ////////////{

        ////////////    double w, h, s = 0.9;
        ////////////    Entity PostEntity = null;

        ////////////    //PromptDoubleOptions pdo = new PromptDoubleOptions("Enter Ground Post Width : ");
        ////////////    //PromptDoubleResult pdr = ed.GetDouble(pdo);

        ////////////    //if (pdr.Status == PromptStatus.OK)
        ////////////    //{
        ////////////    //    w = pdr.Value;
        ////////////    //    pdo.Message = "Enter Ground Post Heigth : ";
        ////////////    //    pdr = ed.GetDouble(pdo);

        ////////////    //    if (pdr.Status == PromptStatus.OK)
        ////////////    //    {

        ////////////    //h = pdr.Value;


        ////////////    Atend.Design.frmPostSize FPS = new Atend.Design.frmPostSize();
        ////////////    if (FPS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        ////////////    {
        ////////////        h = FPS.Tol;
        ////////////        w = FPS.Arz;
        ////////////        //________________________________________________
        ////////////        // All data was getting well
        ////////////        Atend.Global.Acad.AcadJigs.DrawGroundPostJig drawGroundPostJig = new Atend.Global.Acad.AcadJigs.DrawGroundPostJig(w, h, s);
        ////////////        bool Conti = true;

        ////////////        while (Conti)
        ////////////        {
        ////////////            PromptResult pr =
        ////////////            ed.Drag(drawGroundPostJig);

        ////////////            if (pr.Status == PromptStatus.OK)
        ////////////            {
        ////////////                //Conti = false;
        ////////////                drawGroundPostJig.GetAngle = true;
        ////////////                pr = ed.Drag(drawGroundPostJig);

        ////////////                if (pr.Status == PromptStatus.OK)
        ////////////                {
        ////////////                    Conti = false;
        ////////////                    //ed.WriteMessage("Time to save data \n ");

        ////////////                    #region SaveDataHere

        ////////////                    if (AccessSaveGroundPostData(GroundPostProductCode))
        ////////////                    {

        ////////////                        ObjectId oi = Atend.Global.Acad.Global.DrawEntityOnScreen(drawGroundPostJig.GetEntity(), Atend.Control.Enum.AutoCadLayerName.POST.ToString());
        ////////////                        PostEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);

        ////////////                        Guid CurrentPostCodinDPost = new Guid();
        ////////////                        //EXTRA
        ////////////                        //int ProductCode = 0;
        ////////////                        Guid ProductCode = Guid.Empty;
        ////////////                        foreach (Atend.Base.Acad.AcadGlobal.GroundPostData.PostEquips p in Atend.Base.Acad.AcadGlobal.GroundPostData.PostEquipInserted)
        ////////////                        {
        ////////////                            if (p.ProductType == (int)Atend.Control.Enum.ProductType.GroundPost)
        ////////////                            {
        ////////////                                CurrentPostCodinDPost = p.CodeGuid;
        ////////////                                ProductCode = p.ProductCode;
        ////////////                            }
        ////////////                        }

        ////////////                        Atend.Base.Acad.AT_INFO postInfo = new Atend.Base.Acad.AT_INFO(oi);
        ////////////                        postInfo.ParentCode = "";
        ////////////                        postInfo.NodeCode = CurrentPostCodinDPost.ToString();
        ////////////                        postInfo.NodeType = (int)Atend.Control.Enum.ProductType.GroundPost;
        ////////////                        postInfo.ProductCode = ProductCode;
        ////////////                        postInfo.Insert();

        ////////////                        ObjectId DrawnText = Atend.Global.Acad.Global.DrawEntityOnScreen(Atend.Global.Acad.Global.WriteNoteMText("PostInformation \r\n\rSecond Information",
        ////////////                            Atend.Global.Acad.UAcad.CenterOfEntity(drawGroundPostJig.GetEntity())), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
        ////////////                        //ed.WriteMessage("text drawn \n");
        ////////////                        Atend.Base.Acad.AT_INFO textInfo = new Atend.Base.Acad.AT_INFO(DrawnText);
        ////////////                        textInfo.ParentCode = CurrentPostCodinDPost.ToString();
        ////////////                        textInfo.NodeCode = "";
        ////////////                        textInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
        ////////////                        textInfo.ProductCode = 0;
        ////////////                        textInfo.Insert();

        ////////////                        //ed.WriteMessage("information saved for text \n");
        ////////////                        Atend.Base.Acad.AT_SUB PostSub = new Atend.Base.Acad.AT_SUB(oi);
        ////////////                        PostSub.SubIdCollection.Add(DrawnText);
        ////////////                        PostSub.Insert();
        ////////////                        //ed.WriteMessage("information saved for post \n");
        ////////////                    }
        ////////////                    #endregion
        ////////////                    //ed.WriteMessage("Entity Drawn on the screen . \n");

        ////////////                }
        ////////////                //else
        ////////////                //{
        ////////////                //    Conti = false;
        ////////////                //    ed.WriteMessage("Drawing failed \n");
        ////////////                //}
        ////////////            }

        ////////////        }
        ////////////        //_________________________________________
        ////////////    }
        ////////////    //    }
        ////////////    //    else
        ////////////    //    {
        ////////////    //        ed.WriteMessage("\nError while getting data from user.\n");
        ////////////    //    }
        ////////////    //}
        ////////////    //else
        ////////////    //{
        ////////////    //    ed.WriteMessage("\nError while getting data from user.\n");
        ////////////    //}
        ////////////    return PostEntity;
        ////////////}

        ////////////private bool AccessSaveGroundPostData(int GroundPostProductCode)
        ////////////{
        ////////////    //if (Atend.Base.Design.NodeTransaction.InsertGroundPost(GroundPostProductCode))
        ////////////    //{
        ////////////    if (Atend.Base.Design.NodeTransaction.InsertGroundPost(GroundPostProductCode))
        ////////////    {
        ////////////        return true;
        ////////////    }
        ////////////    else
        ////////////    {
        ////////////        return false;
        ////////////    }
        ////////////    //}
        ////////////    //else
        ////////////    //{
        ////////////    //    return false;
        ////////////    //}

        ////////////}

        ////////////#endregion


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        #region Additional Method
        //~~~~~~~~~~~~~~ Start Additional Method Code Part ~~~~~~~~~~~~~~~~~~~~

        public Entity GetEntityByObjectID(ObjectId SelectedObjectId)
        {
            Entity ent;

            //ed.WriteMessage(" go to GetEntityByObjectID \n");

            //Database db = HostApplicationServices.WorkingDatabase;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {

                ent = (Entity)tr.GetObject(SelectedObjectId, OpenMode.ForRead);

            }

            return ent;
        }

        //public Point3d CenterOfRectangle(Polyline Rectangle)
        //{
        //    LineSegment3d LS1 = Rectangle.GetLineSegmentAt(0);

        //    LineSegment3d LS2 = Rectangle.GetLineSegmentAt(1);

        //    LineSegment3d ls3 = new LineSegment3d(LS1.StartPoint, LS2.EndPoint);

        //    Point3d CenterPoint = ls3.MidPoint;

        //    return CenterPoint;

        //}

        //private ObjectId DrawEntityOnScreen(Entity entity)
        //{

        //    ObjectId oi;


        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("I aM in DrawEntityOnScreen\n");
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
        //    {


        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {

        //            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);

        //            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

        //            oi = btr.AppendEntity(entity);

        //            tr.AddNewlyCreatedDBObject(entity, true);

        //            tr.Commit();


        //        }
        //    }
        //    ed.WriteMessage("End Of DrawEntityOnScreen\n");

        //    return oi;

        //}

        //private ObjectId DrawEntityOnScreen(Entity entity, string LayerName)
        //{

        //    ObjectId oi;

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("I aM in DrawEntityOnScreen\n");
        //    Database db = HostApplicationServices.WorkingDatabase;
        //    using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
        //    {


        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {

        //            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);

        //            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);


        //            entity.LayerId = Atend.Global.Acad.AcadLayer.GetLayerById(LayerName);

        //            oi = btr.AppendEntity(entity);

        //            tr.AddNewlyCreatedDBObject(entity, true);

        //            tr.Commit();


        //        }
        //    }
        //    ed.WriteMessage("End Of DrawEntityOnScreen\n");

        //    return oi;

        //}


        //public bool IsPost(ObjectId SelectedObjectID)
        //{
        //    Database db = HostApplicationServices.WorkingDatabase;

        //    using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
        //    {

        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {

        //            DBObject ent = (DBObject)tr.GetObject(SlectedObjectId, OpenMode.ForRead);

        //            DBDictionary ExtDict = (DBDictionary)tr.GetObject(ent.ExtensionDictionary, OpenMode.ForRead);

        //            if (ExtDict.Contains(InformationTable))
        //            {

        //                //ed.WriteMessage("EXIST \n");
        //                Xrecord xrec = (Xrecord)tr.GetObject(ExtDict.GetAt(InformationTable), OpenMode.ForRead);

        //                foreach (TypedValue tv in xrec.Data)
        //                {
        //                    //ed.WriteMessage("Value : {0} , Type : {1} \n",tv.Value , tv.TypeCode);
        //                    if (tv.TypeCode == (byte)DxfCode.Int32)
        //                    {
        //                        if (Convert.ToInt32(tv.Value) == (int)Atend.Control.Enum.ProductType.Post)
        //                        {

        //                            foreach (TypedValue tv1 in xrec.Data)
        //                            {
        //                                //ed.WriteMessage("Value : {0} , Type : {1} \n",tv.Value , tv.TypeCode);
        //                                if (tv1.TypeCode == (byte)DxfCode.Text)
        //                                {

        //                                    NodeGuid = new Guid(Convert.ToString(tv1.Value));

        //                                }
        //                            }

        //                            return true;
        //                        }
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                ed.WriteMessage("AT_INFO : NOT EXIST \n");
        //            }
        //            return false;
        //        }
        //    }



        //}

        /// <summary>
        /// Iterate all extension dictionary and show its values
        /// </summary>
        [CommandMethod("RE")]
        public void ReadExtension()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            Database db = Application.DocumentManager.MdiActiveDocument.Database;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {

                PromptEntityOptions peo = new PromptEntityOptions("Select an entity : \n");

                PromptEntityResult per = ed.GetEntity(peo);

                if (per.Status == PromptStatus.OK)
                {

                    ObjectId selectedObjectId = per.ObjectId;

                    DBObject selectedObject = (DBObject)
                        tr.GetObject(selectedObjectId, OpenMode.ForRead);

                    DBDictionary dbdic = (DBDictionary)
                        tr.GetObject(selectedObject.ExtensionDictionary, OpenMode.ForRead);
                    if (dbdic != null)
                    {
                        ed.WriteMessage("CurrentOI:{0}\n", per.ObjectId);
                        foreach (DBDictionaryEntry de in dbdic)
                        {
                            ed.WriteMessage("dictionary name : {0} \n", de.Key);

                            Xrecord xrec = (Xrecord)tr.GetObject(dbdic.GetAt(de.Key), OpenMode.ForRead);
                            foreach (TypedValue tv in xrec.Data)
                            {
                                ed.WriteMessage("Value : {0} , Type : {1} \n", tv.Value, tv.TypeCode);
                            }
                        }
                    }
                    else
                    {
                        ed.WriteMessage("selected object do not have dictionary. \n");
                    }
                }
                else
                {
                    ed.WriteMessage("Error while selection \n");
                }
            }
        }

        [CommandMethod("RE2")]
        public void ReadExtension2()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptEntityOptions peo = new PromptEntityOptions("Select an entity : \n");
            PromptEntityResult per = ed.GetEntity(peo);
            if (per.Status == PromptStatus.OK)
            {
                ObjectId selectedObjectId = per.ObjectId;
                Atend.Base.Acad.AT_INFO information = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjectId);
                ed.WriteMessage("SelectedObjectId:{0} \nParentCode:{1} \nNodeCode:{2} \nNodeType:{3} \nProductCode:{4} \nAngle:{5} \n"
                    , information.SelectedObjectId, information.ParentCode, information.NodeCode, ((Atend.Control.Enum.ProductType)information.NodeType).ToString(), information.ProductCode, information.Angle);

                ed.WriteMessage("------------Start subs-----------------\n");
                Atend.Base.Acad.AT_SUB subs = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(selectedObjectId);
                foreach (ObjectId oi in subs.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO information1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                    ed.WriteMessage("SelectedObjectId:{0} \nParentCode:{1} \nNodeCode:{2} \nNodeType:{3} \nProductCode:{4} \nAngle:{5} \n"
                        , information1.SelectedObjectId, information1.ParentCode, information1.NodeCode, ((Atend.Control.Enum.ProductType)information1.NodeType).ToString(), information1.ProductCode, information1.Angle);

                    ed.WriteMessage("-----------------------------\n");

                }

            }
            else
            {
                ed.WriteMessage("Error while selection \n");
            }
        }


        //~~~~~~~~~~~~~~ End Additional Method Code Part ~~~~~~~~~~~~~~~~~~~~
        #endregion


        #region Iterate Tree
        //~~~~~~~~~~~~~~~ Start Iterate Tree Code Part ~~~~~~~~~~~~~~~~~~~~~~~

        public class NodeListInformation
        {
            private int designCode;

            public int DesignCode
            {
                get { return designCode; }
                set { designCode = value; }
            }

            private Guid nodeCode;

            public Guid NodeCode
            {
                get { return nodeCode; }
                set { nodeCode = value; }
            }

            private string no;

            public string No
            {
                get { return no; }
                set { no = value; }
            }



        }

        public List<NodeListInformation> NodeList = new List<NodeListInformation>();


        /// <summary>
        /// Find a way from startPole to endPole
        /// </summary>
        /// <param name="StartPole"></param>
        /// <param name="EndPole"></param>
        /// <returns></returns>
        public bool Iteration(Guid StartPole, Guid EndPole)
        {

            NodeListInformation nodelist = new NodeListInformation();
            //nodelist.DesignCode = Atend.Control.Common.SelectedDesignCode;
            nodelist.NodeCode = StartPole;
            nodelist.No = "";

            NodeList.Add(nodelist);


            if (IterateLeftNodeInBranch(StartPole, EndPole))
            {
                //ed.WriteMessage("Iteration way found with left . \n");
                return true;
            }
            else
            {

                if (IterateRightNodeInBranch(StartPole, EndPole))
                {
                    //ed.WriteMessage("Iteration way found with right . \n");
                    return true;

                }
                else
                {
                    NodeList.Clear();
                    return false;
                }

            }
        }

        private bool IterateLeftNodeInBranch(Guid StartPole, Guid EndPole)
        {
            System.Data.DataTable List =
            Atend.Base.Design.DBranch.AccessSelectByLeftNodeCode(
                StartPole
                );

            foreach (DataRow dr in List.Rows)
            {

                if (new Guid(dr["RightNodeCode"].ToString()) == EndPole)
                {
                    NodeListInformation nodelist = new NodeListInformation();
                    //nodelist.DesignCode = Atend.Control.Common.SelectedDesignCode;
                    nodelist.NodeCode = new Guid(dr["RightNodeCode"].ToString());
                    nodelist.No = "";

                    NodeList.Add(nodelist);
                    return true;

                }
                else
                {

                    if (IterateLeftNodeInBranch(new Guid(dr["RightNodeCode"].ToString()), EndPole))
                    {

                        NodeListInformation nodelist = new NodeListInformation();
                        //nodelist.DesignCode = Atend.Control.Common.SelectedDesignCode;
                        nodelist.NodeCode = new Guid(dr["RightNodeCode"].ToString());
                        nodelist.No = "";

                        NodeList.Add(nodelist);
                        return true;


                    }
                    else if (IterateRightNodeInBranch(new Guid(dr["RightNodeCode"].ToString()), EndPole))
                    {

                        NodeListInformation nodelist = new NodeListInformation();
                        //nodelist.DesignCode = Atend.Control.Common.SelectedDesignCode;
                        nodelist.NodeCode = new Guid(dr["RightNodeCode"].ToString());
                        nodelist.No = "";

                        NodeList.Add(nodelist);
                        return true;


                    }

                }

            }

            return false;
        }

        private bool IterateRightNodeInBranch(Guid StartPole, Guid EndPole)
        {

            System.Data.DataTable List =
                Atend.Base.Design.DBranch.AccessSelectByRigthNodeCode(
                StartPole
               );

            foreach (DataRow dr in List.Rows)
            {

                if (new Guid(dr["LeftNodeCode"].ToString()) == EndPole)
                {
                    NodeListInformation nodelist = new NodeListInformation();
                    //nodelist.DesignCode = Atend.Control.Common.SelectedDesignCode;
                    nodelist.NodeCode = new Guid(dr["LeftNodeCode"].ToString());
                    nodelist.No = "";

                    NodeList.Add(nodelist);
                    return true;


                }
                else
                {

                    if (IterateRightNodeInBranch(new Guid(dr["LeftNodeCode"].ToString()), EndPole))
                    {

                        NodeListInformation nodelist = new NodeListInformation();
                        //nodelist.DesignCode = Atend.Control.Common.SelectedDesignCode;
                        nodelist.NodeCode = new Guid(dr["LeftNodeCode"].ToString());
                        nodelist.No = "";

                        NodeList.Add(nodelist);
                        return true;


                    }
                    else if (IterateLeftNodeInBranch(new Guid(dr["LeftNodeCode"].ToString()), EndPole))
                    {

                        NodeListInformation nodelist = new NodeListInformation();
                        //nodelist.DesignCode = Atend.Control.Common.SelectedDesignCode;
                        nodelist.NodeCode = new Guid(dr["LeftNodeCode"].ToString());
                        nodelist.No = "";

                        NodeList.Add(nodelist);
                        return true;


                    }

                }

            }

            return false;


        }

        public bool DetermineInnerPoint()
        {
            //Point2d centerpt, selectedpt ;
            //double tool, arz, angle;
            //Point2d pt1, pt2, pt3, pt4, pttmp;
            //pt1.X = centerpt.X + tool / 2;
            //pt2.X = centerpt.X + tool / 2;
            //pt3.X = centerpt.X - tool / 2;
            //pt4.X = centerpt.X - tool / 2;
            //pt1.Y = centerpt.Y + arz / 2;
            //pt2.Y = centerpt.Y + arz / 2;
            //pt3.Y = centerpt.Y - arz / 2;
            //pt4.Y = centerpt.Y - arz / 2;
            ////Now we ar going to rotage rectangle by given angle
            //pttmp = pt1;
            //pt1.X = pttmp.GetAsVector().Length * Math.Cos(angle);
            //pt1.Y = pttmp.GetAsVector().Length * Math.Sin(angle);
            //pttmp = pt2;
            //pt2.X = pttmp.GetAsVector().Length * Math.Cos(angle);
            //pt2.Y = pttmp.GetAsVector().Length * Math.Sin(angle);
            //pttmp = pt3;
            //pt3.X = pttmp.GetAsVector().Length * Math.Cos(angle);
            //pt3.Y = pttmp.GetAsVector().Length * Math.Sin(angle);
            //pttmp = pt4;
            //pt4.X = pttmp.GetAsVector().Length * Math.Cos(angle);
            //pt4.Y = pttmp.GetAsVector().Length * Math.Sin(angle);
            ////No we are going to check innerpoint



            return true;
        }

        //~~~~~~~~~~~~~~~ End Iterate Tree Code Part ~~~~~~~~~~~~~~~~~~~~~~~
        #endregion


        //#region Inside Point
        ////~~~~~~~~~~~~~~~ Start Inside Point Code ~~~~~~~~~~~~~~~~~~~~~~~

        //const int INSIDE = 0;
        //const int OUTSIDE = 1;

        //private double MIN(double x, double y)
        //{
        //    if (x < y)
        //        return x;
        //    else
        //        return y;
        //}

        //private double MAX(double x, double y)
        //{

        //    if (x > y)
        //        return x;
        //    else
        //        return y;

        //}

        //private int InsidePolygon(Point3dCollection P3C, Point3d point)
        //{
        //    //ed.WriteMessage("I Am In InsidePolygon\n");
        //    int counter = 0;
        //    int N;
        //    double xinters;
        //    Point3d p1, p2;

        //    p1 = P3C[0];
        //    N = P3C.Count - 1;
        //    //ed.WriteMessage("N= "+N.ToString()+"\n");

        //    for (int i = 1; i <= N; i++)
        //    {
        //        //ed.WriteMessage("i= "+i.ToString()+"\n");
        //        p2 = P3C[i - ((int)(i / N) * i)];
        //        //ed.WriteMessage("{0} REMAIL", (N - ((int)(i / N) * i)));
        //        //ed.WriteMessage("p2= "+p2+"\n");
        //        if (point.Y > MIN(p1.Y, p2.Y))
        //        {
        //            //ed.WriteMessage("I AM IN The First IF\n");
        //            //ed.WriteMessage("Point.y= "+point.Y.ToString()+"\n");
        //            //ed.WriteMessage("p1.Y= "+p1.Y.ToString()+"\n");
        //            //ed.WriteMessage("p2.Y= "+p2.Y.ToString()+"\n");
        //            if (point.Y <= MAX(p1.Y, p2.Y))
        //            {
        //                //ed.WriteMessage("I AM IN The Second IF\n");
        //                //ed.WriteMessage("point.X= "+point.X.ToString()+"\n");
        //                //ed.WriteMessage("p1.X= "+p1.X.ToString()+"\n");
        //                //ed.WriteMessage("p2.X= "+p2.X.ToString()+"\n");
        //                if (point.X <= MAX(p1.X, p2.X))
        //                {
        //                    //ed.WriteMessage("I Am In The Third If\n");
        //                    //ed.WriteMessage("p1.Y= " + p1.Y.ToString() + "\n");
        //                    //ed.WriteMessage("p2.Y= "+p2.Y.ToString()+"\n");

        //                    if (p1.Y != p2.Y)
        //                    {
        //                        //ed.WriteMessage("I Am In The Forth IF\n");

        //                        xinters = (point.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;
        //                        //ed.WriteMessage("Xinters= "+xinters.ToString()+"\n");
        //                        if (p1.X == p2.X || point.X <= xinters)
        //                        {
        //                            //ed.WriteMessage("I Am In The Fifth If\n");
        //                            counter++;
        //                            //ed.WriteMessage("Counter= "+counter.ToString()+"\n");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        p1 = p2;
        //    }
        //    if (counter - (counter * (int)(counter / 2)) == 0)
        //        return OUTSIDE;
        //    else
        //        return INSIDE;

        //}

        ////~~~~~~~~~~~~~~~ End Inside Point Code ~~~~~~~~~~~~~~~~~~~~~~~
        //#endregion



    }
}
