using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Calculating
{
    public partial class frmRudSurface : Form
    {
        bool ForceToClose = false;
        public frmRudSurface()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\nchecking.....\n");
            if (!Atend.Global.Acad.DrawEquips.Dicision.IsHere())
            {
                if (!Atend.Global.Acad.DrawEquips.Dicision.IsThere())
                {
                    //System.Diagnostics.Process[] prs = System.Diagnostics.Process.GetProcesses();
                    //foreach (System.Diagnostics.Process pr in prs)
                    //{
                    //    if (pr.ProcessName == "acad")
                    //    {
                    //        pr.CloseMainWindow();
                    //    }
                    //}
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "شناسایی قفل";
                    notification.Msg = "لطفا وضعیت قفل را بررسی نمایید ";
                    notification.infoCenterBalloon();

                    ForceToClose = true;

                }
            }

            InitializeComponent();

            dtSection.Columns.Add("SectionCode");
            dtSection.Columns.Add("SectionNumber");
            dtSection.Columns.Add("StartPole");
            dtSection.Columns.Add("EndPole");
        }

        ArrayList PolesHeight = new ArrayList();

        ArrayList SelectedList = new ArrayList();

        ArrayList SectionCode = new ArrayList();

        System.Data.DataTable dtSectionNumber = new System.Data.DataTable();

        public System.Data.DataTable dtSection = new System.Data.DataTable();

        Atend.Global.Calculation.Section section;

        System.Data.DataTable dtResult = new System.Data.DataTable();
        double Volt = 0;

        Atend.Base.Calculating.CNetWorkCross NetCross = new Atend.Base.Calculating.CNetWorkCross();

        Atend.Global.Calculation.Mechanical.CalcOptimalSagTension calcOptimalSagTension;

        public void SelectArea()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SelectedList.Clear();
            SectionCode.Clear();

            #region Select
            this.Hide();

            // ~~~~~~~~~~~~ Start Select Entities 


            int PoleCounter = 0;

            int ConductorCounter = 0;

            PromptSelectionOptions pso = new PromptSelectionOptions();

            pso.MessageForAdding = "Select area where you want: \n";

            PromptSelectionResult psr = ed.GetSelection(pso);

            if (psr.Status == PromptStatus.OK)
            {

                SelectionSet ss = psr.Value;

                ObjectId[] SelectedObjectID = ss.GetObjectIds();

                foreach (ObjectId so in SelectedObjectID)
                {

                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(so);
                    if (at_info.ParentCode != "NONE" && (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Pole || at_info.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
                    {
                        Atend.Global.Calculation.General.General.AutocadSelectedList Item =
                            new Atend.Global.Calculation.General.General.AutocadSelectedList();

                        Item.ProductGuid = new Guid(at_info.NodeCode);
                        Item.ProductType = at_info.NodeType;
                        Item.ConductorAngle = 0;

                        string PoleNumber = "";
                        Atend.Base.Acad.AT_SUB poleSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(so);
                        foreach (ObjectId oi in poleSub.SubIdCollection)
                        {

                            Atend.Base.Acad.AT_INFO subinfo =
                                Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (subinfo.ParentCode != "NONE" && subinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                            {

                                MText dbt = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as MText;
                                if (dbt != null)
                                {
                                    PoleNumber = dbt.Contents;
                                }
                            }

                        }

                        Item.PoleNumber = PoleNumber;
                        SelectedList.Add(Item);
                        PoleCounter++;
                    }
                    else if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                    {
                        Atend.Global.Calculation.General.General.AutocadSelectedList Item =
                            new Atend.Global.Calculation.General.General.AutocadSelectedList();

                        Item.ProductGuid = new Guid(at_info.NodeCode);
                        Item.ProductType = (int)Atend.Control.Enum.ProductType.Conductor;
                        Item.ConductorAngle = 0;
                        Item.PoleNumber = "";
                        SelectedList.Add(Item);
                        ConductorCounter++;
                    }
                    else if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                    {
                        Atend.Global.Calculation.General.General.AutocadSelectedList Item =
                            new Atend.Global.Calculation.General.General.AutocadSelectedList();

                        Item.ProductGuid = new Guid(at_info.NodeCode);
                        Item.ProductType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
                        Item.ConductorAngle = 0;
                        Item.PoleNumber = "";
                        SelectedList.Add(Item);
                        ConductorCounter++;
                    }


                }

                //lblPole.Text = PoleCounter.ToString();
                //lblConductor.Text = ConductorCounter.ToString();
                ed.WriteMessage("Finish\n");


            }
            else
            {
                //lblPole.Text = "0";
                //lblConductor.Text = "0";
            }

            // ~~~~~~~~~~~~ End Select Entities


            this.Show();


            #endregion
        }
        private void frmRudSurface_Load(object sender, EventArgs e)
        {

            if (ForceToClose)
                this.Close();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SelectedList.Clear();
            SectionCode.Clear();
            //int gvCount = gvNetCross.Rows.Count - 1;
            //for (int i = gvCount; i >= 0; i--)
            //{
            //    gvNetCross.Rows.RemoveAt(i);
            //}
            //gvNetCross.Refresh();


            //cboVolt.SelectedIndex = 0;

            SelectArea();


            //while (SelectedList.Count == 0)
            //{
            //    MessageBox.Show("لطفا جهت انجام محاسبه یک سکشن را انتخاب کنید", "خطا");
            //    SelectArea();
            //}

            dtSectionNumber = new System.Data.DataTable();
            System.Data.DataColumn dcColumn = new System.Data.DataColumn("SectionNumber");
            System.Data.DataColumn dcColumn1 = new System.Data.DataColumn("SectionCode");
            System.Data.DataColumn dcStartPole = new System.Data.DataColumn("StartPole");
            System.Data.DataColumn dcEndPole = new System.Data.DataColumn("EndPole");
            System.Data.DataColumn dcIsCheck = new System.Data.DataColumn("IsCheck");
            dtSectionNumber.Columns.Add(dcColumn);
            dtSectionNumber.Columns.Add(dcColumn1);
            dtSectionNumber.Columns.Add(dcStartPole);
            dtSectionNumber.Columns.Add(dcEndPole);
            dtSectionNumber.Columns.Add(dcIsCheck);

            System.Data.DataColumn dcNetCross = new System.Data.DataColumn("NetCrossCode", System.Type.GetType("System.Int32"));
            dtSectionNumber.Columns.Add(dcNetCross);

            System.Data.DataColumn dcUTS = new System.Data.DataColumn("UTS");
            dtSectionNumber.Columns.Add(dcUTS);


            AddNetCrossCodeColumn();


            Boolean chk = true;


            System.Data.DataTable dt = Atend.Base.Design.DPoleSection.AccessSelectAll();
            if (SelectedList.Count != 0)
            {
                if (dt.Rows.Count > 0)
                {
                    BindDateTodtSectionNumber();
                    //for (int i = 0; i < SelectedList.Count; i++)
                    //{
                    //    chk = true;
                    //    Atend.Global.Calculation.General.General.AutocadSelectedList cadInfo = (Atend.Global.Calculation.General.General.AutocadSelectedList)SelectedList[i];

                    //    Atend.Base.Design.DPoleSection poleSection = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(cadInfo.ProductGuid, cadInfo.ProductType);
                    //    if (SectionCode.Count != 0)
                    //    {
                    //        for (int j = 0; j < SectionCode.Count; j++)
                    //        {

                    //            if ((Guid)SectionCode[j] == poleSection.SectionCode)
                    //            {
                    //                chk = false;
                    //            }


                    //        }
                    //        if (chk)
                    //        {
                    //            SectionCode.Add(poleSection.SectionCode);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        SectionCode.Add(poleSection.SectionCode);
                    //    }
                    //}


                    //for (int i = 0; i < SectionCode.Count; i++)
                    //{

                    //    Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
                    //    System.Data.DataRow dr = dtSectionNumber.NewRow();
                    //    ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
                    //    dr["SectionNumber"] = sectionNUmber.Number.ToString();
                    //    dr["SectionCode"] = sectionNUmber.Code.ToString();
                    //    dtSectionNumber.Rows.Add(dr);
                    //}



                    if (dtSectionNumber.Rows.Count > 0)
                    {
                        BindDataToGridSection();
                        //ChangeColor();
                        chkSelectSection.Checked = true;
                        //ReadVoltage();
                    }
                }
            }
        }

        private void BindDateTodtSectionNumber()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Boolean chk = true;
            SectionCode.Clear();
            for (int i = 0; i < SelectedList.Count; i++)
            {
                chk = true;
                Atend.Global.Calculation.General.General.AutocadSelectedList cadInfo = (Atend.Global.Calculation.General.General.AutocadSelectedList)SelectedList[i];

                Atend.Base.Design.DPoleSection poleSection = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(cadInfo.ProductGuid, cadInfo.ProductType);
                if (poleSection.SectionCode != Guid.Empty)
                {
                    if (SectionCode.Count != 0)
                    {
                        for (int j = 0; j < SectionCode.Count; j++)
                        {

                            if ((Guid)SectionCode[j] == poleSection.SectionCode)
                            {
                                chk = false;
                            }


                        }
                        if (chk)
                        {
                            SectionCode.Add(poleSection.SectionCode);
                        }
                    }
                    else
                    {
                        SectionCode.Add(poleSection.SectionCode);
                    }
                }
            }


            for (int i = 0; i < SectionCode.Count; i++)
            {

                Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
                System.Data.DataRow dr = dtSectionNumber.NewRow();
                ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
                dr["SectionNumber"] = sectionNUmber.Number.ToString();
                dr["SectionCode"] = sectionNUmber.Code.ToString();
                dtSectionNumber.Rows.Add(dr);
            }


        }
        private void BindDataToGridSection()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            foreach (DataRow dr in dtSectionNumber.Rows)
            {
                ed.WriteMessage("SectionCode={0},SectionNumber={1}\n", new Guid(dr["SectionCode"].ToString()), dr["SectionNumber"].ToString());
                Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                ed.WriteMessage("*****Start={0},End={1}\n", startEnd.StartPole, startEnd.EndPole);
                Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);

                Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);

                dr["StartPole"] = dPackStart.Number;
                dr["EndPole"] = dPackEnd.Number;


                Atend.Base.Calculating.CSetDefaultMec cSetDefaultmec = Atend.Base.Calculating.CSetDefaultMec.AccessSelect();
                System.Data.DataTable dtNetDefault = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();

                Atend.Base.Calculating.CDefaultRudSurface defaultRudSurface = Atend.Base.Calculating.CDefaultRudSurface.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                if (defaultRudSurface.Code != 0)
                {
                    dr["UTS"] = defaultRudSurface.UTS;
                    dr["NetCrossCode"] = defaultRudSurface.NetCross;
                }
                else
                {
                    dr["UTS"] = cSetDefaultmec.Uts;
                    dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());
                }



            }
            ed.WriteMessage(" EXIT BindDataToGridSection\n");
            gvSection.AutoGenerateColumns = false;
            gvSection.DataSource = dtSectionNumber;
        }

        private void ReadVoltage(int RowIndex)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("ReadVoltage\n");
            System.Data.DataTable dtsec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
            ed.WriteMessage("ReadVoltage\n");
            if (dtsec.Rows.Count == 0)
                dtsec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

            ed.WriteMessage("@@\n");
            Atend.Base.Design.DPackage dParent = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dtsec.Rows[0]["ProductCode"].ToString()));
            System.Data.DataTable dt = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dParent.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
            if (dt.Rows.Count == 0)
                dt = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dParent.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

            ed.WriteMessage("BeforConsol Count={0}\n", dt.Rows.Count);
            if (Convert.ToInt32(dt.Rows[0]["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
            {
                ed.WriteMessage("*******\n");
                Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dt.Rows[0]["ProductCode"].ToString()));
                if (eConsol.VoltageLevel == 400)
                    Volt = 400;

                if (eConsol.VoltageLevel == 1100)
                    Volt = 1100;

                if (eConsol.VoltageLevel == 20000)
                    Volt = 20000;

                if (eConsol.VoltageLevel == 33000)
                    Volt = 33000;
            }

            if (Convert.ToInt32(dt.Rows[0]["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
            {
                Atend.Base.Equipment.EClamp eKalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(dt.Rows[0]["ProductCode"].ToString()));
                if (eKalamp.VoltageLevel == 400)
                    Volt = 400;

                if (eKalamp.VoltageLevel == 1100)
                    Volt = 1100;

                if (eKalamp.VoltageLevel == 20000)
                    Volt = 20000;

                if (eKalamp.VoltageLevel == 33000)
                    Volt = 33000;
            }
            ed.WriteMessage("FINISH++\n");


        }

        //private void bindDataToEnterInformation()
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("START bindDataToEnterInformation\n");
        //    Atend.Base.Calculating.CSetDefaultMec cSetDefaultmec = Atend.Base.Calculating.CSetDefaultMec.AccessSelect();
        //    System.Data.DataTable dtNetDefault = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
        //    foreach (DataRow dr in dtSectionNumber.Rows)
        //    {

        //        Atend.Base.Calculating.CDefaultRudSurface defaultRudSurface = Atend.Base.Calculating.CDefaultRudSurface.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
        //        if (defaultRudSurface.Code != 0)
        //        {
        //            dr["UTS"] = defaultRudSurface.UTS;
        //            dr["NetCrossCode"] = defaultRudSurface.NetCross;
        //        }
        //        else
        //        {
        //            dr["UTS"] = cSetDefaultmec.Uts;
        //            dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());
        //        }
        //    }

        //    //txtDistance.Text = cSetDefaultmec.Distance.ToString();
        //    //txtEnd.Text = cSetDefaultmec.End.ToString();
        //    //txtStart.Text = cSetDefaultmec.Start.ToString();
        //    ed.WriteMessage("FINISH bindDataToEnterInformation\n");
        //}

        private void سکشنبندیToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void AddNetCrossCodeColumn()
        {
            System.Data.DataTable dt2 = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dt2;
            c.DisplayMember = "Name";
            c.ValueMember = "Code";
            c.DataPropertyName = "NetCrossCode";
            c.HeaderText = "محل عبور شبکه";
            gvSection.Columns.Insert(gvSection.Columns.Count - 2, c);

        }

        private void Delete()
        {
            Atend.Base.Design.DGlobal.AccessDelete();
            Atend.Base.Design.DPoleSection.AccessDelete();
            Atend.Base.Design.DSection.AccessDelete();
            Atend.Base.Calculating.CStartEnd.AccessDelete();
            Atend.Base.Calculating.CSagAndTension.AccessDelete();
            Atend.Base.Calculating.CWindOnPole.AccessDelete();
            Atend.Base.Calculating.CConductorDay.AccessDelete();
            Atend.Base.Calculating.CTemp.AccessDelete();
            Atend.Base.Calculating.CRudSurface.AccessDelete();
            Atend.Base.Calculating.CDefaultRudSurface.AccessDelete();


        }

        private void پروندهToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private bool Validation()
        {
            if (Atend.Control.Common.Demo)
            {
                // MessageBox.Show("", "");
                return false;
            }

            return true;
        }

        private void Save(int RowIndex)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Base.Calculating.CDefaultRudSurface defRudSurface = Atend.Base.Calculating.CDefaultRudSurface.AccessSelectBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()));
            DataRow[] drDefault = dtSectionNumber.Select(string.Format(" SectionCode='{0}'", gvSection.Rows[RowIndex].Cells[0].Value.ToString()));

            if (defRudSurface.Code != 0)
            {
                ed.WriteMessage("UTS={0}\n", drDefault[0]["UTS"].ToString());
                defRudSurface.UTS = Convert.ToDouble(drDefault[0]["UTS"].ToString());
                defRudSurface.NetCross = Convert.ToInt32(drDefault[0]["NetCrossCode"].ToString());
                defRudSurface.SectionCode = new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString());
                //defMec.SE = se;
                defRudSurface.AccessUpdate();
            }
            else
            {
                ed.WriteMessage("UTS={0}\n", drDefault[0]["UTS"].ToString());
                defRudSurface.NetCross = Convert.ToInt32(drDefault[0]["NetCrossCode"].ToString());
                defRudSurface.UTS = Convert.ToDouble(drDefault[0]["UTS"].ToString());
                defRudSurface.SectionCode = new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString());

                defRudSurface.AccessInsert();
            }


            System.Data.DataTable dtResultRudSurface = Atend.Base.Calculating.CRudSurface.AccessSelectBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()));

            if (dtResultRudSurface.Rows.Count != 0)
            {
                Atend.Base.Calculating.CRudSurface.AccessDeleteBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()));
            }


            foreach (DataRow dr in dtResult.Rows)
            {
                Atend.Base.Calculating.CRudSurface surface = new Atend.Base.Calculating.CRudSurface();
                surface.DcIceHeavy = Convert.ToDouble(dr["DcIceHeavy"].ToString());
                surface.DcMaxTemp = Convert.ToDouble(dr["DcMaxTemp"].ToString());
                surface.DcMinTemp = Convert.ToDouble(dr["DcMinTemp"].ToString());
                surface.DcNorm = Convert.ToDouble(dr["DcNorm"].ToString());
                surface.DcPole = dr["DcPole"].ToString();
                surface.DcwindIce = Convert.ToDouble(dr["DcWindIce"].ToString());
                surface.DcWindSpeed = Convert.ToDouble(dr["DcWindSpeed"].ToString());
                surface.SectionCode = new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString());
                if (!surface.AccessInsert())
                {
                    ed.WriteMessage("Failed surface INSERT\n");
                }

            }

        }

        private void gvSection_Click(object sender, EventArgs e)
        {
            //ArrayList arTemp = new ArrayList();

            dtResult.Rows.Clear();


            bool chkAdd = true;

            dtResult = Atend.Base.Calculating.CRudSurface.AccessSelectBySectionCode(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()));


            if (dtResult.Rows.Count != 0)
            {
                gvResult.AutoGenerateColumns = false;
                gvResult.DataSource = dtResult;
            }

        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void شرایطابوهواییToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FindHeightOfPoles()
        {
            Atend.Base.Calculating.CStartEnd startEnd = new Atend.Base.Calculating.CStartEnd();
            System.Data.DataTable poleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));

            System.Data.DataTable PoleTipSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

            foreach (DataRow dr in PoleTipSection.Rows)
            {
                DataRow drPole = poleSection.NewRow();
                drPole["ProductType"] = dr["ProductType"].ToString();
                drPole["ProductCode"] = dr["ProductCode"].ToString();
                drPole["SectionCode"] = dr["SectionCode"].ToString();
                poleSection.Rows.Add(drPole);
            }

        }

        private void انتقالبهفایلExelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chkSelectSection_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectSection.Checked)
            {
                foreach (DataRow dr in dtSectionNumber.Rows)
                {
                    dr["IsCheck"] = true;
                }
            }
            else
            {
                foreach (DataRow dr in dtSectionNumber.Rows)
                {
                    dr["IsCheck"] = false;
                }
            }
        }

        private void Weather()
        {
            Atend.Design.frmWeather weather = new Atend.Design.frmWeather();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(weather);
        }

        private void شرایطآبوهواییToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Weather();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Weather();
        }

        private void TransferToEXCEL()
        {
            System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            int day = calender.GetDayOfMonth(DateTime.Today);
            int Month = calender.GetMonth(DateTime.Today);
            int Year = calender.GetYear(DateTime.Today);

            string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
            string NameFlash = "نتیجه محاسبات مکانیکی سطوح نا هموار" + date + ".xls";

            Atend.Global.Utility.UReport.CreateExcelRudSurface(NameFlash);
        }

        private void انتقالبهفایلEXCELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransferToEXCEL();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            TransferToEXCEL();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Calculation()
        {
            chkSelectSection.Focus();

            if (Validation())
            {
                this.Cursor = Cursors.WaitCursor;
                System.Data.DataTable dtGlobal = Atend.Base.Design.DGlobal.AccessSelectAll();
                System.Data.DataTable dtBranch = Atend.Global.Acad.UAcad.FillBranchList();
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                for (int gCounter = 0; gCounter < gvSection.Rows.Count; gCounter++)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvSection.Rows[gCounter].Cells[1];
                    if (chk.Value.ToString() == "True")
                    {
                        calcOptimalSagTension = new Atend.Global.Calculation.Mechanical.CalcOptimalSagTension();
                        calcOptimalSagTension.DtPoleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));

                        System.Data.DataTable dtPole1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                        foreach (DataRow dr in dtPole1.Rows)
                        {
                            DataRow drPole = calcOptimalSagTension.DtPoleSection.NewRow();
                            drPole["ProductType"] = dr["ProductType"].ToString();
                            drPole["ProductCode"] = dr["ProductCode"].ToString();
                            drPole["SectionCode"] = dr["SectionCode"].ToString();
                            calcOptimalSagTension.DtPoleSection.Rows.Add(drPole);
                        }


                        System.Data.DataColumn dc = new System.Data.DataColumn("PoleNumber");
                        calcOptimalSagTension.DtPoleSection.Columns.Add(dc);
                        foreach (DataRow dr in calcOptimalSagTension.DtPoleSection.Rows)
                        {
                            Atend.Base.Design.DPackage node = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dr["ProductCode"].ToString()));
                            dr["PoleNumber"] = node.Number.ToString();

                        }
                        ReadVoltage(gCounter);
                        calcOptimalSagTension.Volt = Volt;

                        calcOptimalSagTension.DtPoleCond = dtGlobal;
                        calcOptimalSagTension.dtBranchList = dtBranch;

                        DataGridViewComboBoxCell cboNetCross = (DataGridViewComboBoxCell)gvSection.Rows[gCounter].Cells[4];
                        //ed.WriteMessage("NetCrossCode={0}\n", cboNetCross.Value.ToString());
                        NetCross = Atend.Base.Calculating.CNetWorkCross.AccessSelectByCode(Convert.ToInt32(cboNetCross.Value.ToString()));

                        if (Volt == 400)
                        {
                            calcOptimalSagTension.Clearance = NetCross.V380;
                        }
                        if (Volt == 11000)
                        {
                            calcOptimalSagTension.Clearance = NetCross.KV11;
                        }
                        if (Volt == 20000)
                        {
                            calcOptimalSagTension.Clearance = NetCross.KV20;
                        }
                        if (Volt == 33000)
                        {
                            calcOptimalSagTension.Clearance = NetCross.KV32;
                        }

                        calcOptimalSagTension.DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                        if (calcOptimalSagTension.DtconductorSection.Rows.Count == 0)
                        {

                            calcOptimalSagTension.DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                            //ed.WriteMessage("Count={0}\n", calcOptimalSagTension.DtconductorSection.Rows.Count);
                        }

                        calcOptimalSagTension.VTS = Convert.ToDouble(gvSection.Rows[gCounter].Cells["UTS"].Value.ToString());
                        //System.Data.DataColumn dc1 = new System.Data.DataColumn("Angle");
                        //calcOptimalSagTension.DtconductorSection.Columns.Add(dc1);
                        calcOptimalSagTension.SectionCode = new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString());
                        //calcOptimalSagTension.Start = Convert.ToInt32(txtStart.Text);
                        //calcOptimalSagTension.End = Convert.ToInt32(txtEnd.Text);
                        //calcOptimalSagTension.Distance = Convert.ToInt32(txtDistance.Text);
                        Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(calcOptimalSagTension.DtconductorSection.Rows[0]["ProductCode"].ToString()));
                        //ed.WriteMessage("OOO\n");
                        dtResult.Rows.Clear();

                        //se = calcOptimalSagTension.ComputeSE();
                        //ed.WriteMessage("&&&&&&&&&&&&&&&&&&&SE={0}\n", se.ToString());
                        //SpanCount = calcOptimalSagTension.DtconductorSection.Rows.Count;
                        calcOptimalSagTension.dtStTable.Rows.Clear();

                        ed.WriteMessage("GOTOSurface\n");
                        dtResult = calcOptimalSagTension.calcRudSurface02().Copy();
                        gvResult.AutoGenerateColumns = false;
                        calcOptimalSagTension.CloseConnection();
                        gvResult.DataSource = dtResult;
                        Save(gCounter);

                    }
                }
                this.Cursor = Cursors.Default;
                //DataRow dr1 = dtRow.NewRow();
                //dr1["ProjectName"] = "";
                //dr1["NumSection"] = gvSection.SelectedRows[0].Cells[1].Value.ToString();
                //dr1["FirstPole"] = "";
                //dr1["LastPole"] = "";
                //dr1["UTS"] = calcOptimalSagTension.VTS;
                //dr1["SE"] = se.ToString();
                //dr1["SpanCount"] = SpanCount;
                //dr1["SpanLenght"] = "";
                //dr1["CondName"] = gvSagAndTension.Rows[0].Cells[0].Value;
                //dtRow.Rows.Add(dr1);
                //ChangeColor();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Calculation();
        }

        private void CreateSection()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Delete();

            section = new Atend.Global.Calculation.Section();
            section.DetermineSection();

            dtSectionNumber.Rows.Clear();
            dtSection.Rows.Clear();
            SectionCode.Clear();
            Boolean chk = true;

            ////for (int i = 0; i < SelectedList.Count; i++)
            ////{
            ////    chk = true;
            ////    Atend.Global.Calculation.General.General.AutocadSelectedList cadInfo = (Atend.Global.Calculation.General.General.AutocadSelectedList)SelectedList[i];
            ////    //ed.WriteMessage("Guid= "+cadInfo.ProductGuid.ToString()+"ProductType = "+cadInfo.ProductType.ToString()+"\n");

            ////    Atend.Base.Design.DPoleSection poleSection = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(cadInfo.ProductGuid, cadInfo.ProductType);
            ////    //ed.WriteMessage("PoleSection.SectionCode Is= "+poleSection.SectionCode.ToString()+"\n");
            ////    if (SectionCode.Count != 0)
            ////    {
            ////        for (int j = 0; j < SectionCode.Count; j++)
            ////        {

            ////            if ((Guid)SectionCode[j] == poleSection.SectionCode)
            ////            {
            ////                chk = false;
            ////            }


            ////        }
            ////        if (chk)
            ////        {
            ////            //    ed.WriteMessage("Add To SectionCode,SectionCode Is={0}\n", poleSection.SectionCode);
            ////            SectionCode.Add(poleSection.SectionCode);
            ////        }
            ////    }
            ////    else
            ////    {
            ////        //  ed.WriteMessage("Add To SectionCode Else,SectionCode Is={0}\n", poleSection.SectionCode);
            ////        SectionCode.Add(poleSection.SectionCode);
            ////    }
            ////    //ed.WriteMessage("SectionCodeList.Count= "+SectionCode.Count.ToString()+"\n");
            ////}


            ////for (int i = 0; i < SectionCode.Count; i++)
            ////{
            ////    //ed.WriteMessage("**Sec.Code={0}\n", SectionCode[i].ToString());

            ////    Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
            ////    System.Data.DataRow dr = dtSectionNumber.NewRow();
            ////    ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
            ////    dr["SectionNumber"] = sectionNUmber.Number.ToString();
            ////    dr["SectionCode"] = sectionNUmber.Code.ToString();
            ////    dtSectionNumber.Rows.Add(dr);
            ////}
            if (SelectedList.Count != 0)
            {
                BindDateTodtSectionNumber();
                ed.WriteMessage("dtSectionNumber.Rows.count={0}\n",dtSectionNumber.Rows.Count);
                if (dtSectionNumber.Rows.Count > 0)
                {
                    BindDataToGridSection();

                    chkSelectSection.Checked = true;
                    // ReadVoltage();
                }
                else
                {
                    ed.WriteMessage("سکشن یافت نشد\n");

                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CreateSection();
        }

        private void سکشنبندیToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateSection();
        }

        private void محاسبهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculation();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectArea();

            if (SelectedList.Count != 0)
            {
                dtSectionNumber.Rows.Clear();
            }
            BindDateTodtSectionNumber();


            if (dtSectionNumber.Rows.Count > 0)
            {
                chkSelectSection.Checked = true;
                BindDataToGridSection();
                //ChangeColor();
            }
        }
    }
}