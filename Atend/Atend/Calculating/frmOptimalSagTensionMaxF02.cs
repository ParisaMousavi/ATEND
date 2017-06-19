using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.Data.SqlClient;

namespace Atend.Calculating
{
    public partial class frmOptimalSagTensionMaxF02 : Form
    {

        ArrayList SectionCode = new ArrayList();
        Atend.Global.Calculation.Mechanical.CalcOptimalSagTensionmaxF calcOptimalSagTension;
        ArrayList SelectedList = new ArrayList();
        System.Data.DataTable dtSectionNumber = new System.Data.DataTable();
        System.Data.DataColumn dcColumn = new System.Data.DataColumn("Name");
        System.Data.DataColumn dcColumn1 = new System.Data.DataColumn("Code");
        Atend.Base.Calculating.CNetWorkCross NetCross = new Atend.Base.Calculating.CNetWorkCross();

        System.Data.DataTable dtConductorDay1 = new System.Data.DataTable();


        System.Data.DataTable dtSagTension = new System.Data.DataTable();
        System.Data.DataTable dtConductorDay = new System.Data.DataTable();
        System.Data.DataTable dtPole = new System.Data.DataTable();

        double se;

        Atend.Global.Calculation.Section section = new Atend.Global.Calculation.Section();
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        double volt = 400;

        System.Data.DataTable dtConsol = new System.Data.DataTable();
        System.Data.DataTable dtCalamp = new System.Data.DataTable();
        System.Data.DataTable dtHMax = new System.Data.DataTable();

        System.Data.DataTable dtBranch = new System.Data.DataTable();
        System.Data.DataTable dtGlobal = new System.Data.DataTable();
        bool ForceToClose = false;
        public frmOptimalSagTensionMaxF02()
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

            //dtRow.Columns.Add("ProjectName");
            //dtRow.Columns.Add("NumSection");
            //dtRow.Columns.Add("FirstPole");
            //dtRow.Columns.Add("LastPole");
            //dtRow.Columns.Add("UTS");
            //dtRow.Columns.Add("SE");
            //dtRow.Columns.Add("SpanCount");
            //dtRow.Columns.Add("SpanLenght");
            //dtRow.Columns.Add("CondName");


        }


        public void selectArea()
        {
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

        private void frmOptimalSagTensionVTS_Load(object sender, EventArgs e)
        {

            if (ForceToClose)
                this.Close();


            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SelectedList.Clear();
            SectionCode.Clear();

            selectArea();
            //while (SelectedList.Count == 0)
            //{
            //    MessageBox.Show("لطفا یک سکشن را جهت انجام محاسبه انتخاب کنید", "خطا");
            //    selectArea();
            //}



            dtSectionNumber = new System.Data.DataTable();
            System.Data.DataColumn dcColumn = new System.Data.DataColumn("SectionNumber");
            System.Data.DataColumn dcColumn1 = new System.Data.DataColumn("SectionCode");
            System.Data.DataColumn dcStart = new System.Data.DataColumn("StartPole");
            System.Data.DataColumn dcEnd = new System.Data.DataColumn("EndPole");
            System.Data.DataColumn dcNetCross = new System.Data.DataColumn("NetCrossCode", System.Type.GetType("System.Int32"));
            System.Data.DataColumn TrustBorder = new System.Data.DataColumn("TrustBorder");
            System.Data.DataColumn IsCheck = new System.Data.DataColumn("IsCheck", System.Type.GetType("System.Boolean"));
            System.Data.DataColumn Consol = new System.Data.DataColumn("Consol", System.Type.GetType("System.Int32"));
            System.Data.DataColumn Calamp = new System.Data.DataColumn("Calamp", System.Type.GetType("System.Int32"));



            dtSectionNumber.Columns.Add(dcColumn);
            dtSectionNumber.Columns.Add(dcColumn1);
            dtSectionNumber.Columns.Add(dcStart);
            dtSectionNumber.Columns.Add(dcEnd);
            dtSectionNumber.Columns.Add(dcNetCross);
            dtSectionNumber.Columns.Add(TrustBorder);
            dtSectionNumber.Columns.Add(IsCheck);
            dtSectionNumber.Columns.Add(Consol);
            dtSectionNumber.Columns.Add(Calamp);







            //System.Data.DataColumn dcNetCross = new System.Data.DataColumn("NetCrossCode", System.Type.GetType("System.Int32"));
            //dtSectionNumber.Columns.Add(dcNetCross);

            //System.Data.DataColumn dcUTS = new System.Data.DataColumn("UTS");
            //dtSectionNumber.Columns.Add(dcUTS);

            AddNetCrossCodeColumn();
            AddCalampColumn();
            AddConsolColumn();


            Boolean chk = true;
            //ed.WriteMessage("SelectedList.Count= " + SelectedList.Count.ToString() + "\n");


            System.Data.DataTable dt = Atend.Base.Design.DPoleSection.AccessSelectAll();
            if (SelectedList.Count != 0)
            {
                if (dt.Rows.Count > 0)
                {
                    BindDataTodtSectionNumber();
                    ////    for (int i = 0; i < SelectedList.Count; i++)
                    ////    {
                    ////        chk = true;
                    ////        Atend.Global.Calculation.General.General.AutocadSelectedList cadInfo = (Atend.Global.Calculation.General.General.AutocadSelectedList)SelectedList[i];
                    ////        //ed.WriteMessage("Guid= "+cadInfo.ProductGuid.ToString()+"ProductType = "+cadInfo.ProductType.ToString()+"\n");

                    ////        Atend.Base.Design.DPoleSection poleSection = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(cadInfo.ProductGuid, cadInfo.ProductType);
                    ////        //ed.WriteMessage("PoleSection.SectionCode Is= "+poleSection.SectionCode.ToString()+"\n");
                    ////        if (poleSection.SectionCode != Guid.Empty)
                    ////        {
                    ////            if (SectionCode.Count != 0)
                    ////            {
                    ////                for (int j = 0; j < SectionCode.Count; j++)
                    ////                {

                    ////                    if ((Guid)SectionCode[j] == poleSection.SectionCode)
                    ////                    {
                    ////                        chk = false;
                    ////                    }


                    ////                }
                    ////                if (chk)
                    ////                {
                    ////                    //    ed.WriteMessage("Add To SectionCode,SectionCode Is={0}\n", poleSection.SectionCode);
                    ////                    SectionCode.Add(poleSection.SectionCode);
                    ////                }
                    ////            }
                    ////            else
                    ////            {
                    ////                //  ed.WriteMessage("Add To SectionCode Else,SectionCode Is={0}\n", poleSection.SectionCode);
                    ////                SectionCode.Add(poleSection.SectionCode);
                    ////            }
                    ////        }
                    ////        //ed.WriteMessage("SectionCodeList.Count= "+SectionCode.Count.ToString()+"\n");
                    ////    }
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


                    if (dtSectionNumber.Rows.Count > 0)
                    {
                        chkSelection.Checked = true;
                        BindDataToGridSection();
                        ChangeColor();
                    }
                }
            }
            //}
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Validation()
        {
            //btnSelect.Focus();
            //if (SelectedList.Count == 0)
            //{
            //    MessageBox.Show("لطفا یک محدوده را جهت محاسبات انتخاب کنید\n", "خطا");
            //    btnSelect.Focus();
            //    return false;
            //}
            //if (cboConsol.Items.Count > 0)
            //{
            //    if (string.IsNullOrEmpty(cboConsol.Text))
            //    {
            //        MessageBox.Show("لطفا نام کنسول را انتخاب کنید\n", "خطا");
            //        cboConsol.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(cboVolt.Text))
            //    {
            //        MessageBox.Show("لطفا ولتاژ را مشخص نمایید\n", "خطا");
            //        cboVolt.Focus();
            //        return false;

            //    }
            //}

            //if (CboCalampNAme.Items.Count > 0)
            //{
            //    if (string.IsNullOrEmpty(CboCalampNAme.Text))
            //    {
            //        MessageBox.Show("لطفا نام کلمپ را انتخاب کنید\n", "خطا");
            //        CboCalampNAme.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(cboVolCalamp.Text))
            //    {
            //        MessageBox.Show("لطفا ولتاژ کلمپ را مشخص نمایید\n", "خطا");
            //        cboVolCalamp.Focus();
            //        return false;

            //    }
            //}


            //if (!Atend.Control.NumericValidation.DoubleConverter(cboVolt.Text))
            //{
            //    MessageBox.Show("لطفا ولتاژ را با فرمت مناسب وارد نمایید", "خطا");
            //    cboVolt.Focus();
            //    return false;
            //}

            //if (string.IsNullOrEmpty(txtEtminan.Text))
            //{
            //    MessageBox.Show("لطفا حاشیه اطمینان را مشخص نمایید\n", "خطا");
            //    txtEtminan.Focus();
            //    return false;

            //}
            //if (!Atend.Control.NumericValidation.DoubleConverter(txtEtminan.Text))
            //{
            //    MessageBox.Show("لطفا حاشیه اطمینان را با فرمت مناسب وارد نمایید", "خطا");
            //    txtEtminan.Focus();
            //    return false;
            //}

            //for (int i = 0; i < gvNetCross.Rows.Count; i++)
            //{
            //    //ed.WriteMessage("gvNetCross.Rows[i].Cells[0].Value= " + gvNetCross.Rows[i].Cells[0].Value.ToString() + " cboSelectSection.SelectedText= " + cboSelectSection.Text+"\n");
            //    if (Convert.ToInt32(gvNetCross.Rows[i].Cells[0].Value) == Convert.ToInt32(gvSection.SelectedRows[0].Cells[1].Value.ToString()))
            //    {
            //        //ed.WriteMessage("Read NetCross\n");
            //        DataGridViewComboBoxCell cboNetCross = (DataGridViewComboBoxCell)gvNetCross.Rows[i].Cells["Column2"];
            //        if (cboNetCross.Value == null)
            //        {
            //            MessageBox.Show("لطفا محل عبور شبکه را مشخص نمایید\n", "خطا");
            //            return false;
            //        }


            //    }
            //}

            if (Atend.Control.Common.Demo)
            {
                //MessageBox.Show("", "");
                return false;
            }

            if (string.IsNullOrEmpty(txtStart.Text))
            {
                MessageBox.Show("لطفا شروع دما را مشخص نمایید\n", "خطا");
                txtStart.Focus();
                return false;

            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtStart.Text))
            {
                MessageBox.Show("لطفا شروع دما را با فرمت مناسب وارد نمایید", "خطا");
                txtStart.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtEnd.Text))
            {
                MessageBox.Show("لطفا پایان دما را مشخص نمایید\n", "خطا");
                txtEnd.Focus();
                return false;

            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtEnd.Text))
            {
                MessageBox.Show("لطفا پایان دما را با فرمت مناسب وارد نمایید", "خطا");
                txtEnd.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDistance.Text))
            {
                MessageBox.Show("لطفا فاصله دما را مشخص نمایید\n", "خطا");
                txtDistance.Focus();
                return false;

            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtDistance.Text))
            {
                MessageBox.Show("لطفا پایان فاصله دما را با فرمت مناسب وارد نمایید", "خطا");
                txtDistance.Focus();
                return false;
            }


            System.Data.DataTable dt = Atend.Base.Design.DWeatherType.AccessSelectAll();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("لطفاابتدا شرایط اب وهوایی را تنظیم کنید", "خطا");
                return false;
            }


            return true;
        }

        private void پروندهToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void BindDataToResultGrid(Guid sectionCode)
        {
            ArrayList arTemp = new ArrayList();
            //try
            //{
            dtSagTension.Rows.Clear();
            dtPole.Rows.Clear();
            dtConductorDay1.Columns.Clear();
            dtConductorDay1.Rows.Clear();

            bool chkAdd = true;

            System.Data.DataTable dtSagTension01 = Atend.Base.Calculating.CSagAndTension.AccessSelect(sectionCode, false);
            dtSagTension01.Columns.Add("SE");
            System.Data.DataTable dtPole01 = Atend.Base.Calculating.CWindOnPole.AccessSelectBySectionCode(sectionCode, false);
            System.Data.DataTable dtTemp = Atend.Base.Calculating.CConductorDay.AccessSelectBySectionCode(sectionCode, false);
            System.Data.DataTable dtCond = Atend.Base.Calculating.CConductorDay.AccessSelect(sectionCode, false);
            Atend.Base.Calculating.CDefaultMec DefMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(sectionCode, false);
            foreach(DataRow dr in dtSagTension01.Rows)
            {
                dr["SE"] =Math.Round(Convert.ToDouble(DefMec.SE),2);
            }
            if (dtTemp.Rows.Count != 0)
            {
                arTemp.Add(dtTemp.Rows[0]["Temp"].ToString());

                foreach (DataRow dr in dtTemp.Rows)
                {
                    chkAdd = true;
                    for (int i = 0; i < arTemp.Count; i++)
                    {
                        if (arTemp[i].ToString() == dr["Temp"].ToString())
                        {
                            chkAdd = false;
                        }
                    }
                    if (chkAdd)
                    {
                        arTemp.Add(dr["Temp"].ToString());
                    }
                }

                dtConductorDay1.Columns.Add("From");
                dtConductorDay1.Columns.Add("TO");
                dtConductorDay1.Columns.Add("SpanLenght");
                dtConductorDay1.Columns.Add("ConductorName");
            }
            for (int i = 0; i < arTemp.Count; i++)
            {
                dtConductorDay1.Columns.Add(arTemp[i].ToString());
                dtConductorDay1.Columns.Add(arTemp[i].ToString() + "F");
            }

            foreach (DataRow dr in dtCond.Rows)
            {
                DataRow dr1 = dtConductorDay1.NewRow();
                dr1["From"] = dr["From"].ToString();
                dr1["To"] = dr["TO"].ToString();
                dr1["Spanlenght"] = dr["SpanLenght"].ToString();
                dr1["ConductorName"] = dr["ConductorName"].ToString();
                for (int i = 0; i < arTemp.Count; i++)
                {
                    DataRow[] drTemp = dtTemp.Select(string.Format(" Temp={0} AND ConductorDayCode={1}", arTemp[i].ToString(), dr["Code"].ToString()));
                    dr1[arTemp[i].ToString()] = drTemp[0]["Tension"].ToString();
                    dr1[arTemp[i].ToString() + "F"] = drTemp[0]["Sag"].ToString();
                }
                dtConductorDay1.Rows.Add(dr1);
            }

            if (dtConductorDay1.Rows.Count != 0)
                gvConductorDay.DataSource = dtConductorDay1;

            if (dtSagTension01.Rows.Count != 0)
            {
                gvSagAndTension.AutoGenerateColumns = false;
                gvSagAndTension.DataSource = dtSagTension01;
                CheckSag(dtSagTension01);
            }

            if (dtPole01.Rows.Count != 0)
            {
                gvPole.AutoGenerateColumns = false;
                gvPole.DataSource = dtPole01;
                //btnContinue.Enabled = true;
            }

        }
        private void Save(int RowIndex)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Base.Calculating.CDefaultMec defMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), false);
            DataRow[] drDefault = dtSectionNumber.Select(string.Format(" SectionCode='{0}'", gvSection.Rows[RowIndex].Cells[0].Value.ToString()));

            Atend.Global.Calculation.Mechanical.SagTensionSave sagTension = new Atend.Global.Calculation.Mechanical.SagTensionSave();
            sagTension.DefMec = defMec;
            sagTension.Distance = Convert.ToInt32(txtDistance.Text);
            sagTension.End = Convert.ToInt32(txtEnd.Text);
            sagTension.Start = Convert.ToInt32(txtStart.Text);
            sagTension.DrDefault = drDefault;
            sagTension.DtConductorDay = dtConductorDay;
            sagTension.DtHMAX = dtHMax;
            sagTension.DtPole = dtPole;
            sagTension.DtsagTension = dtSagTension;
            sagTension.IsUTS = false;
            sagTension.Se = se;
            sagTension.SectionCode = new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString());
            sagTension.Volt = volt;
            sagTension.TrustBorder = Convert.ToDouble(gvSection.Rows[RowIndex].Cells["TrustBorder"].Value.ToString());
            if (!sagTension.SaveData())
            {
                ed.WriteMessage("اطلاعات با موفقیت ثبت نشد\n");
            }



            //ed.WriteMessage("#########################SE={0}\n", se.ToString());
            //if (defMec.Code != 0)
            //{
            //    //ed.WriteMessage("UTS={0}\n", drDefault[0]["UTS"].ToString());
            //    defMec.TrustBorder = Convert.ToDouble(gvSection.Rows[RowIndex].Cells["TrustBorder"].Value.ToString());
            //    defMec.NetCross = Convert.ToInt32(drDefault[0]["NetCrossCode"].ToString());
            //    defMec.SE = se;
            //    defMec.AccessUpdate();
            //}
            //else
            //{
            //    //ed.WriteMessage("NETCROSSCODE={0}\n", drDefault[0]["NetCrossCode"].ToString());
            //    defMec.NetCross = Convert.ToInt32(drDefault[0]["NetCrossCode"].ToString());
            //    defMec.Uts = 0;
            //    defMec.IsUTS = false;
            //    defMec.SectionCode = new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString());
            //    defMec.TrustBorder = Convert.ToDouble(gvSection.Rows[RowIndex].Cells["TrustBorder"].Value.ToString());
            //    defMec.Vol = Convert.ToDouble(volt);
            //    defMec.SE = se;
            //    defMec.AccessInsert();
            //}


            //System.Data.DataTable dtSag = Atend.Base.Calculating.CSagAndTension.AccessSelectBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), false);

            //if (dtSag.Rows.Count != 0)
            //{
            //    Atend.Base.Calculating.CSagAndTension.AccessDeleteBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), false);
            //}

            //System.Data.DataTable dtWind = Atend.Base.Calculating.CWindOnPole.AccessSelectBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), false);

            //if (dtWind.Rows.Count != 0)
            //{
            //    Atend.Base.Calculating.CWindOnPole.AccessDeleteBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), false);
            //}

            //System.Data.DataTable dtCondDay = Atend.Base.Calculating.CConductorDay.AccessSelectBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), false);

            //if (dtCondDay.Rows.Count != 0)
            //{
            //    Atend.Base.Calculating.CConductorDay.AccessDeleteBySectionCode(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), false);
            //}



            //foreach (DataRow dr in dtSagTension.Rows)
            //{
            //    Atend.Base.Calculating.CSagAndTension cSagTension = new Atend.Base.Calculating.CSagAndTension();
            //    cSagTension.ConductorName = dr["ConductorName"].ToString();
            //    cSagTension.IceF = Convert.ToDouble(dr["IceF"].ToString());
            //    cSagTension.IceH = Convert.ToDouble(dr["IceH"].ToString());
            //    cSagTension.MaxTempF = Convert.ToDouble(dr["MaxTempF"].ToString());
            //    cSagTension.MaxTempH = Convert.ToDouble(dr["MaxTempH"].ToString());
            //    cSagTension.MinTempF = Convert.ToDouble(dr["MinTempF"].ToString());
            //    cSagTension.MinTempH = Convert.ToDouble(dr["MinTempH"].ToString());
            //    cSagTension.NormF = Convert.ToDouble(dr["NormF"].ToString());
            //    cSagTension.NormH = Convert.ToDouble(dr["NormH"].ToString());
            //    cSagTension.SectionCode = new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString());
            //    cSagTension.WindAndIceF = Convert.ToDouble(dr["WindAndIceF"].ToString());
            //    cSagTension.WindAndIceH = Convert.ToDouble(dr["WindAndIceH"].ToString());
            //    cSagTension.WindF = Convert.ToDouble(dr["WindF"].ToString());
            //    cSagTension.WindH = Convert.ToDouble(dr["WindH"].ToString());
            //    cSagTension.IsUTS = false;
            //    cSagTension.MaxF = Convert.ToDouble(dr["MaxF"].ToString());
            //    if (!cSagTension.AccessInsert())
            //    {
            //        ed.WriteMessage("Failed SagTension INSERT\n");
            //    }

            //}

            //foreach (DataRow dr in dtConductorDay.Rows)
            //{
            //    Atend.Base.Calculating.CConductorDay condDay = new Atend.Base.Calculating.CConductorDay();
            //    condDay.SectionCode = new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString());
            //    condDay.From = dr["From"].ToString();
            //    condDay.To = dr["To"].ToString();
            //    condDay.SpanLenght = Convert.ToDouble(dr["SpanLenght"].ToString());
            //    condDay.ConductorName = dr["ConductorName"].ToString();
            //    condDay.IsUTS = false;
            //    if (!condDay.AccessInsert())
            //    {
            //        ed.WriteMessage(" Failed CondDay.Access INSERT\n");
            //    }

            //    for (int i = Convert.ToInt32(txtEnd.Text); i > Convert.ToInt32(txtStart.Text); i = i - Convert.ToInt32(txtDistance.Text))
            //    {
            //        Atend.Base.Calculating.CTemp cTemp = new Atend.Base.Calculating.CTemp();
            //        //ed.WriteMessage("condday.Code={0}\n", condDay.Code);
            //        cTemp.ConductorDayCode = condDay.Code;
            //        cTemp.Temp = i;
            //        cTemp.Sag = Convert.ToDouble(dr[i.ToString() + "F"].ToString());
            //        cTemp.Tension = Convert.ToDouble(dr[i.ToString()].ToString());
            //        if (!cTemp.AccessInsert())
            //        {
            //            ed.WriteMessage("Failed Ctemp.AccessInsert\n");
            //        }
            //    }
            //}


            //foreach (DataRow dr in dtPole.Rows)
            //{
            //    Atend.Base.Calculating.CWindOnPole windPole = new Atend.Base.Calculating.CWindOnPole();
            //    windPole.DcIceHeavy = Convert.ToDouble(dr["DcIceHeavy"].ToString());
            //    windPole.DcIsUTS = false;
            //    windPole.DcMaxTemp = Convert.ToDouble(dr["DcMaxTemp"].ToString());
            //    windPole.DcMinTemp = Convert.ToDouble(dr["DcMinTemp"].ToString());
            //    windPole.DcNorm = Convert.ToDouble(dr["DcNorm"].ToString());
            //    windPole.DcPole = dr["DcPole"].ToString();
            //    windPole.DcwindIce = Convert.ToDouble(dr["DcWindIce"].ToString());
            //    windPole.DcWindSpeed = Convert.ToDouble(dr["DcWindSpeed"].ToString());
            //    windPole.SectionCode = new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString());
            //    windPole.DcPoleGuid = new Guid(dr["DcPoleGuid"].ToString());
            //    windPole.DcAngle = Convert.ToDouble(dr["DcAngle"].ToString());
            //    if (!windPole.AccessInsert())
            //    {
            //        ed.WriteMessage("Failed WindOnPole INSERT\n");
            //    }

            //}
        }

        private void txtVTS_TextChanged(object sender, EventArgs e)
        {

        }

        //public void BindDataToCboConsol()
        //{
        //    //System.Data.DataTable dtConsol = new System.Data.DataTable();


        //    //dtConsol.Clear();
        //    ed.WriteMessage("I am In BindDataToConsol\n");
        //    System.Data.DataTable dtPoleSection = new System.Data.DataTable();
        //    dtPoleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //    System.Data.DataTable dtPoleTip = new System.Data.DataTable();
        //    dtPoleTip = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
        //    ed.WriteMessage("dtPoleTip.Rows.count={0}\n", dtPoleSection.Rows.Count);
        //    foreach (DataRow dr in dtPoleTip.Rows)
        //    {
        //        DataRow drPole = dtPoleSection.NewRow();
        //        drPole["ProductType"] = dr["ProductType"].ToString();
        //        drPole["ProductCode"] = dr["ProductCode"].ToString();
        //        drPole["SectionCode"] = dr["SectionCode"].ToString();
        //        dtPoleSection.Rows.Add(drPole);
        //    }
        //    System.Data.DataTable DtConsol = new System.Data.DataTable();
        //    dtConsol.Rows.Clear();
        //    foreach (DataRow dr in dtPoleSection.Rows)
        //    {
        //        //Atend.Base.Design.DNode MyNode = Atend.Base.Design.DNode.SelectByCode(new Guid(dr["PoleGuid"].ToString()));
        //        //ed.WriteMessage("dr[PoleGuid]= " + dr["ProductCode"].ToString() + "\n");

        //        DtConsol = Atend.Base.Design.DConsol.AccessSelectByParentCode(new Guid(dr["ProductCode"].ToString()));
        //        //ed.WriteMessage("DtConsol.Count= "+DtConsol.Rows.Count.ToString()+"\n");
        //        foreach (DataRow drConsol in DtConsol.Rows)
        //        {
        //            Atend.Base.Equipment.EConsol MyConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(drConsol["ProductCode"].ToString()));
        //            //ed.WriteMessage("MyConsol.Name={0}\n",MyConsol.Name);
        //            DataRow drNew = dtConsol.NewRow();
        //            drNew["Name"] = MyConsol.Name;
        //            drNew["Code"] = drConsol["Code"].ToString();
        //            dtConsol.Rows.Add(drNew);
        //            ed.WriteMessage("Add OneRow\n");
        //        }
        //    }

        //    cboConsol.DisplayMember = "Name";
        //    cboConsol.ValueMember = "Code";
        //    cboConsol.DataSource = dtConsol;
        //    cboConsol.Refresh();
        //    ed.WriteMessage("*****************dtConsol.Count= " + dtConsol.Rows.Count.ToString() + "\n");
        //}

        //public void BindDataToCboCalamp()
        //{

        //    System.Data.DataTable dtPoleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));

        //    //ed.WriteMessage("dtPoleSection.Count= "+dtPoleSection.Rows.Count.ToString()+"\n");
        //    foreach (DataRow dr in dtPoleSection.Rows)
        //    {
        //        //Atend.Base.Design.DNode MyNode = Atend.Base.Design.DNode.SelectByCode(new Guid(dr["PoleGuid"].ToString()));
        //        ed.WriteMessage("dr[PoleGuid]= " + dr["ProductCode"].ToString() + "\n");
        //        Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dr["ProductCode"].ToString()));
        //        System.Data.DataTable DtCalamp = new System.Data.DataTable();
        //        DtCalamp = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));
        //        ed.WriteMessage("Dtcalamp.Count= " + dtCalamp.Rows.Count.ToString() + "\n");
        //        foreach (DataRow drKalamp in DtCalamp.Rows)
        //        {
        //            Atend.Base.Equipment.EClamp MyCalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(drKalamp["ProductCode"].ToString()));
        //            ed.WriteMessage("MyCalamp.Name={0}\n", MyCalamp.Name);
        //            DataRow drNew = dtCalamp.NewRow();
        //            drNew["Name"] = MyCalamp.Name;
        //            drNew["Code"] = drKalamp["Code"].ToString();
        //            dtCalamp.Rows.Add(drNew);
        //            ed.WriteMessage("Add OneRow\n");
        //        }
        //    }

        //    CboCalampNAme.DisplayMember = "Name";
        //    CboCalampNAme.ValueMember = "Code";
        //    CboCalampNAme.DataSource = dtCalamp;

        //    //ed.WriteMessage("dtConsol.Count= " + dtConsol.Rows.Count.ToString() + "\n");


        //}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cboSelectSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ed.WriteMessage("Change Index In CboSelectSection\n");
            //BindDataToCboConsol();
        }

        //private void cboConsol_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Atend.Base.Design.DConsol consol = Atend.Base.Design.DConsol.AccessSelectByCode(new Guid(cboConsol.SelectedValue.ToString()));
        //    Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(consol.ProductCode);
        //    if (eConsol.VoltageLevel == 400)
        //        cboVolt.Text = "400";

        //    if (eConsol.VoltageLevel == 1100)
        //        cboVolt.Text = "1100";

        //    if (eConsol.VoltageLevel == 20000)
        //        cboVolt.Text = "20000";

        //    if (eConsol.VoltageLevel == 33000)
        //        cboVolt.Text = "33000";

        //    if (eConsol.Type == 0)
        //    {
        //        txtType.Text = "عمودی";
        //    }
        //    if (eConsol.Type == 1)
        //    {
        //        txtType.Text = "مثلثی";
        //    }
        //    if (eConsol.Type == 2)
        //    {
        //        txtType.Text = "افقی";
        //    }
        //    txtDistancePhase.Text = eConsol.DistancePhase.ToString();

        //}

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void تنظیمشرایطابوهواییToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void انتقالبهفایلExelToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        //private void CboCalampNAme_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Atend.Base.Design.DPackage MyPackage = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(CboCalampNAme.SelectedValue.ToString()));
        //    Atend.Base.Equipment.EClamp calamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(MyPackage.ProductCode);
        //    if (calamp.VoltageLevel == 400)
        //        cboVolCalamp.Text = "400";

        //    if (calamp.VoltageLevel == 1100)
        //        cboVolCalamp.Text = "1100";

        //    if (calamp.VoltageLevel == 20000)
        //        cboVolCalamp.Text = "20000";

        //    if (calamp.VoltageLevel == 33000)
        //        cboVolCalamp.Text = "33000";

        //}

        public void AddNetCrossCodeColumn()
        {
            System.Data.DataTable dt2 = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dt2;
            c.DisplayMember = "Name";
            c.ValueMember = "Code";
            c.DataPropertyName = "NetCrossCode";
            c.HeaderText = "محل عبور شبکه";
            c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            gvSection.Columns.Insert(gvSection.Columns.Count - 2, c);


        }

        public void AddConsolColumn()
        {
            dtConsol = Atend.Base.Equipment.EConsol.SelectAllAndMergeOburi();//Oburi

            System.Data.DataColumn dcRow = new System.Data.DataColumn("RowNO", System.Type.GetType("System.Int32"));
            dtConsol.Columns.Add(dcRow);
            int i = 1;
            foreach (DataRow dr in dtConsol.Rows)
            {
                dr["RowNO"] = i;
                i++;
            }
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dtConsol;
            c.DisplayMember = "Name";
            c.ValueMember = "RowNO";
            c.DataPropertyName = "Consol";
            c.HeaderText = "کنسول";
            //c.FillWeight = 300;
            c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            gvSection.Columns.Insert(gvSection.Columns.Count - 2, c);
        }

        public void AddCalampColumn()
        {
            dtCalamp = Atend.Base.Equipment.EClamp.SelectAllAndMergeOburi();//Oburi

            System.Data.DataColumn dcRow = new System.Data.DataColumn("RowNO", System.Type.GetType("System.Int32"));
            dtCalamp.Columns.Add(dcRow);
            int i = 1;
            foreach (DataRow dr in dtCalamp.Rows)
            {
                dr["RowNO"] = i;
                i++;
            }

            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dtCalamp;
            c.DisplayMember = "Name";
            c.ValueMember = "RowNo";
            c.DataPropertyName = "Calamp";
            c.HeaderText = "کلمپ";
            c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            gvSection.Columns.Insert(gvSection.Columns.Count - 2, c);
        }


        //public int FindConsolOburi()
        //{
        //    //System.Data.DataTable dtConsol = new System.Data.DataTable();


        //    //dtConsol.Clear();
        //    ed.WriteMessage("I am In BindDataToConsol\n");
        //    System.Data.DataTable dtPoleSection = new System.Data.DataTable();
        //    dtPoleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //    System.Data.DataTable dtPoleTip = new System.Data.DataTable();
        //    dtPoleTip = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
        //    ed.WriteMessage("dtPoleTip.Rows.count={0}\n", dtPoleSection.Rows.Count);
        //    foreach (DataRow dr in dtPoleTip.Rows)
        //    {
        //        DataRow drPole = dtPoleSection.NewRow();
        //        drPole["ProductType"] = dr["ProductType"].ToString();
        //        drPole["ProductCode"] = dr["ProductCode"].ToString();
        //        drPole["SectionCode"] = dr["SectionCode"].ToString();
        //        dtPoleSection.Rows.Add(drPole);
        //    }
        //    //System.Data.DataTable DtConsol = new System.Data.DataTable();
        //    //dtConsol.Rows.Clear();
        //    foreach (DataRow dr in dtPoleSection.Rows)
        //    {
        //        //Atend.Base.Design.DNode MyNode = Atend.Base.Design.DNode.SelectByCode(new Guid(dr["PoleGuid"].ToString()));
        //        //ed.WriteMessage("dr[PoleGuid]= " + dr["ProductCode"].ToString() + "\n");

        //        System.Data.DataTable dtConsol1 = Atend.Base.Design.DConsol.AccessSelectByParentCode(new Guid(dr["ProductCode"].ToString()));

        //        foreach (DataRow dr1 in dtConsol1.Rows)
        //        {
        //            Atend.Base.Equipment.EConsol econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dr1["ProductCode"].ToString()));
        //            if (econsol.ConsolType == 2)
        //            {
        //                //return econsol.Code;
        //                int consolCode = FindConsolOburi();
        //                if (consolCode != 0)
        //                {
        //                    DataRow[] dr1 = dtConsol.Select(" Code=" + consolCode.ToString());
        //                    dr["Consol"] = Convert.ToInt32(dr1[0]["RowNo"].ToString());
        //                }
        //                else
        //                {
        //                    dr["Consol"] = Convert.ToInt32(dtConsol.Rows[0]["RowNo"].ToString());
        //                }

        //            }
        //        }
        //    }






        //    return 0;
        //    ed.WriteMessage("*****************dtConsol.Count= " + dtConsol.Rows.Count.ToString() + "\n");
        //}


        private void ChangeColor()
        {
            for (int i = 0; i < gvSection.Rows.Count; i++)
            {
                System.Data.DataTable cSag = Atend.Base.Calculating.CSagAndTension.AccessSelectBySectionCode(new Guid(gvSection.Rows[0].Cells[0].Value.ToString()), false);
                if (cSag.Rows.Count != 0)
                {
                    gvSection.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        private void CheckSag(System.Data.DataTable dtsagtension)
        {
            try
            {
                for (int i = 0; i < gvSagAndTension.Rows.Count; i++)
                {
                    gvSagAndTension.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                double fmax = Convert.ToDouble(gvSagAndTension.Rows[0].Cells["MaxF"].Value.ToString());
                for (int i = 0; i < gvSagAndTension.Rows.Count; i++)
                {
                    if (Convert.ToDouble(gvSagAndTension.Rows[i].Cells["NormF"].Value.ToString()) > fmax)
                    {
                        gvSagAndTension.Rows[i].Cells["NormF"].Style.BackColor = Color.Red;
                    }

                    if (Convert.ToDouble(gvSagAndTension.Rows[i].Cells["IceF"].Value.ToString()) > fmax)
                    {
                        gvSagAndTension.Rows[i].Cells["IceF"].Style.BackColor = Color.Red;
                    }
                    if (Convert.ToDouble(gvSagAndTension.Rows[i].Cells["WindF"].Value.ToString()) > fmax)
                    {
                        gvSagAndTension.Rows[i].Cells["WindF"].Style.BackColor = Color.Red;
                    }
                    if (Convert.ToDouble(gvSagAndTension.Rows[i].Cells["MaxTempF"].Value.ToString()) > fmax)
                    {
                        gvSagAndTension.Rows[i].Cells["MaxTempF"].Style.BackColor = Color.Red;
                    }
                    if (Convert.ToDouble(gvSagAndTension.Rows[i].Cells["MinTempF"].Value.ToString()) > fmax)
                    {
                        gvSagAndTension.Rows[i].Cells["MinTempF"].Style.BackColor = Color.Red;
                    }
                    if (Convert.ToDouble(gvSagAndTension.Rows[i].Cells["WindAndIceF"].Value.ToString()) > fmax)
                    {
                        gvSagAndTension.Rows[i].Cells["WindAndIceF"].Style.BackColor = Color.Red;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR In CHKSag={0}\n", ex.Message);
            }


            int j = 0;
            foreach (DataRow dr in dtsagtension.Rows)
            {
                //m ed.WriteMessage("DtNormH={0},gvNormH={1}\n", dr["NormH"].ToString(), gvSagAndTension.Rows[j].Cells["NormH"].Value.ToString());
                if (Convert.ToDouble(dr["NormH"].ToString()) > Convert.ToDouble(dr["MaxNormH"].ToString()))
                {
                    gvSagAndTension.Rows[j].Cells["NormH"].Style.BackColor = Color.Red;
                }
                // ed.WriteMessage("DtIceH={0},gvIceH={1}\n", dr["IceH"].ToString(), gvSagAndTension.Rows[j].Cells["IceH"].Value.ToString());

                if (Convert.ToDouble(dr["IceH"].ToString()) > Convert.ToDouble(dr["MaxIceH"].ToString()))
                {
                    gvSagAndTension.Rows[j].Cells["IceH"].Style.BackColor = Color.Red;
                }
                // ed.WriteMessage("DtWindH={0},gvWindH={1}\n", dr["WindH"].ToString(), gvSagAndTension.Rows[j].Cells["WindH"].Value.ToString());

                if (Convert.ToDouble(dr["WindH"].ToString()) > Convert.ToDouble(dr["MaxWindH"].ToString()))
                {
                    gvSagAndTension.Rows[j].Cells["WindH"].Style.BackColor = Color.Red;
                }
                //ed.WriteMessage("DtMaxTempH={0},gvMaxTempH={1}\n", dr["MaxTempH"].ToString(), gvSagAndTension.Rows[j].Cells["MaxTempH"].Value.ToString());

                if (Convert.ToDouble(dr["MaxTempH"].ToString()) > Convert.ToDouble(dr["MaxMaxTempH"].ToString()))
                {
                    gvSagAndTension.Rows[j].Cells["MaxTempH"].Style.BackColor = Color.Red;
                }
                //ed.WriteMessage("DtMinTempH={0},gvMinTempH={1}\n", dr["MinTempH"].ToString(), gvSagAndTension.Rows[j].Cells["MinTempH"].Value.ToString());

                if (Convert.ToDouble(dr["MinTempH"].ToString()) > Convert.ToDouble(dr["MaxMinTempH"].ToString()))
                {

                    gvSagAndTension.Rows[j].Cells["MinTempH"].Style.BackColor = Color.Red;
                }

                if (Convert.ToDouble(dr["WindAndIceH"].ToString()) > Convert.ToDouble(dr["MaxWindIceH"].ToString()))
                {
                    gvSagAndTension.Rows[j].Cells["WindAndIceH"].Style.BackColor = Color.Red;
                }
                j++;
            }
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
            Atend.Base.Calculating.CPowerWithHalter.AccessDelete();
            Atend.Base.Calculating.CPowerWithOutHalter.AccessDelete();
            Atend.Base.Calculating.CRudSurface.AccessDelete();


            dtSagTension.Rows.Clear();
            dtPole.Rows.Clear();
            dtConductorDay.Rows.Clear();
            dtConductorDay1.Rows.Clear();
        }

        //private void bindDataToEnterInformation()
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("START bindDataToEnterInformation\n");
        //    Atend.Base.Calculating.CSetDefaultMec cSetDefaultmec = Atend.Base.Calculating.CSetDefaultMec.AccessSelect();
        //    System.Data.DataTable dtNetDefault = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
        //    foreach (DataRow dr in dtSectionNumber.Rows)
        //    {

        //        Atend.Base.Calculating.CDefaultMec defaultMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), true);
        //        if (defaultMec.Code != 0)
        //        {
        //            //dr["UTS"] = defaultMec.Uts;
        //            dr["NetCrossCode"] = defaultMec.NetCross;
        //        }
        //        else
        //        {
        //            //dr["UTS"] = cSetDefaultmec.Uts;
        //            dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());
        //        }
        //    }

        //    txtDistance.Text = cSetDefaultmec.Distance.ToString();
        //    txtEnd.Text = cSetDefaultmec.End.ToString();
        //    txtStart.Text = cSetDefaultmec.Start.ToString();
        //    txtEtminan.Text = cSetDefaultmec.TrustBorder.ToString();
        //    ed.WriteMessage("FINISH bindDataToEnterInformation\n");
        //}

        private void BindDataToGridSection()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("BindDataToGridSection\n");
            Atend.Base.Calculating.CSetDefaultMec cSetDefaultmec = Atend.Base.Calculating.CSetDefaultMec.AccessSelect();
            System.Data.DataTable dtNetDefault = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();

            bool hasConsol = false;
            bool hasCalamp = false;

            foreach (DataRow dr in dtSectionNumber.Rows)
            {
                //ed.WriteMessage("TrustBorder={0}\n", cSetDefaultmec.TrustBorder);
                Atend.Base.Calculating.CDefaultMec defaultMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), false);
                if (defaultMec.Code != 0)
                {
                    dr["TrustBorder"] = defaultMec.TrustBorder;
                    dr["NetCrossCode"] = defaultMec.NetCross;
                }
                else
                {
                    dr["TrustBorder"] = cSetDefaultmec.TrustBorder;
                    //dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());
                    dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());

                }


                txtDistance.Text = cSetDefaultmec.Distance.ToString();
                txtEnd.Text = cSetDefaultmec.End.ToString();
                txtStart.Text = cSetDefaultmec.Start.ToString();
                //txtEtminan.Text = cSetDefaultmec.TrustBorder.ToString();



                //ed.WriteMessage("SectionCode={0}\n", new Guid(dr["SectionCode"].ToString()));
                Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                //ed.WriteMessage("*****Start={0},End={1}\n", startEnd.StartPole, startEnd.EndPole);
                Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);

                Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
                //DataRow drSection = dtSection.NewRow();
                //dr["SectionNumber"] = dr["SectionNumber"].ToString();
                //dr["SectionCode"] = dr["SectionCode"].ToString();

                int consolCode = FindConsol(new Guid(dr["SectionCode"].ToString()));
                int calampCode = FindCalamp(new Guid(dr["SectionCode"].ToString()));

                DataRow[] drConsol = dtConsol.Select("Code ='" + consolCode.ToString() + "'");
                DataRow[] drCalamp = dtCalamp.Select("Code='" + calampCode.ToString() + "'");


                if (drConsol.Length > 0)
                {
                    dr["Consol"] = Convert.ToInt32(drConsol[0]["RowNo"].ToString());
                }
                else
                {
                    dr["Consol"] = Convert.ToInt32(dtConsol.Rows[0]["RowNo"].ToString());
                }


                if (drCalamp.Length > 0)
                {
                    dr["Calamp"] = Convert.ToInt32(drCalamp[0]["RowNo"].ToString());
                }
                else
                {
                    dr["Calamp"] = Convert.ToInt32(dtCalamp.Rows[0]["RowNo"].ToString());
                }

                //ed.WriteMessage("&&&\n");
                dr["StartPole"] = dPackStart.Number;
                dr["EndPole"] = dPackEnd.Number;

                System.Data.DataTable dtCond = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(dr["SectionCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                System.Data.DataTable dtSelfKeeper = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(dr["SectionCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                ed.WriteMessage("dtSelfKeeper.Rows.count={0},SectionCode={1}\n", dtSelfKeeper.Rows.Count, dr["SectionCode"].ToString());
                if (dtCond.Rows.Count > 0)
                {
                    hasConsol = true;
                }


                if (dtSelfKeeper.Rows.Count > 0)
                {
                    hasCalamp = true;
                }
                //dtSection.Rows.Add(drSection);
            }
            ed.WriteMessage("HasCalamp={0}\n", hasCalamp);
            if (hasCalamp)
            {
                gvSection.Columns[5].Visible = true;
            }
            else
            {
                gvSection.Columns[5].Visible = false;
            }


            if (hasConsol)
            {
                gvSection.Columns[6].Visible = true;
            }
            else
            {
                gvSection.Columns[6].Visible = false;
            }

            gvSection.AutoGenerateColumns = false;
            gvSection.DataSource = dtSectionNumber;


        }

        private int FindConsol(Guid SectionCode)
        {
            System.Data.DataTable dtPoleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
            System.Data.DataTable dtPoleTip = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

            //ed.WriteMessage("dtPoleSection2={0}\n", calcOptimalSagTension.DtPoleSection.Rows.Count);
            foreach (DataRow dr in dtPoleTip.Rows)
            {
                DataRow drPole = dtPoleSection.NewRow();
                drPole["ProductType"] = dr["ProductType"].ToString();
                drPole["ProductCode"] = dr["ProductCode"].ToString();
                drPole["SectionCode"] = dr["SectionCode"].ToString();
                dtPoleSection.Rows.Add(drPole);
            }

            foreach (DataRow dr in dtPoleSection.Rows)
            {
                Atend.Base.Design.DPackage dNode = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dr["ProductCode"].ToString()));
                System.Data.DataTable dtConsol = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dNode.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
                foreach (DataRow drConsol in dtConsol.Rows)
                {
                    Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(drConsol["ProductCode"].ToString()));
                    if (consol.ConsolType == 2 || consol.ConsolType==3)
                    {
                        return consol.Code;
                    }
                }
            }
            return 0;


        }

        private int FindCalamp(Guid SectionCode)
        {
            System.Data.DataTable dtPoleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
            System.Data.DataTable dtPoleTip = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

            //ed.WriteMessage("dtPoleSection2={0}\n", calcOptimalSagTension.DtPoleSection.Rows.Count);
            foreach (DataRow dr in dtPoleTip.Rows)
            {
                DataRow drPole = dtPoleSection.NewRow();
                drPole["ProductType"] = dr["ProductType"].ToString();
                drPole["ProductCode"] = dr["ProductCode"].ToString();
                drPole["SectionCode"] = dr["SectionCode"].ToString();
                dtPoleSection.Rows.Add(drPole);
            }

            foreach (DataRow dr in dtPoleSection.Rows)
            {
                Atend.Base.Design.DPackage dNode = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dr["ProductCode"].ToString()));
                System.Data.DataTable dtCalamp = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dNode.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));
                foreach (DataRow drCalamp in dtCalamp.Rows)
                {
                    Atend.Base.Equipment.EClamp calamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(drCalamp["ProductCode"].ToString()));
                    if (calamp.Type == 5)
                    {
                        return calamp.Code;
                    }
                }
            }
            return 0;


        }

        private void btnSection_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Delete();

            section = new Atend.Global.Calculation.Section();
            section.DetermineSection();

            dtSectionNumber.Rows.Clear();
            //dtSection.Rows.Clear();
            SectionCode.Clear();
            Boolean chk = true;

            for (int i = 0; i < SelectedList.Count; i++)
            {
                chk = true;
                Atend.Global.Calculation.General.General.AutocadSelectedList cadInfo = (Atend.Global.Calculation.General.General.AutocadSelectedList)SelectedList[i];
                //ed.WriteMessage("Guid= "+cadInfo.ProductGuid.ToString()+"ProductType = "+cadInfo.ProductType.ToString()+"\n");

                Atend.Base.Design.DPoleSection poleSection = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(cadInfo.ProductGuid, cadInfo.ProductType);
                //ed.WriteMessage("PoleSection.SectionCode Is= "+poleSection.SectionCode.ToString()+"\n");
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
                        //    ed.WriteMessage("Add To SectionCode,SectionCode Is={0}\n", poleSection.SectionCode);
                        SectionCode.Add(poleSection.SectionCode);
                    }
                }
                else
                {
                    //  ed.WriteMessage("Add To SectionCode Else,SectionCode Is={0}\n", poleSection.SectionCode);
                    SectionCode.Add(poleSection.SectionCode);
                }
                //ed.WriteMessage("SectionCodeList.Count= "+SectionCode.Count.ToString()+"\n");
            }


            for (int i = 0; i < SectionCode.Count; i++)
            {
                //ed.WriteMessage("**Sec.Code={0}\n", SectionCode[i].ToString());

                Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
                System.Data.DataRow dr = dtSectionNumber.NewRow();
                ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
                dr["SectionNumber"] = sectionNUmber.Number.ToString();
                dr["SectionCode"] = sectionNUmber.Code.ToString();
                dtSectionNumber.Rows.Add(dr);
            }

            //System.Data.DataTable dtNetDefault = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            //foreach (DataRow dr in dtSectionNumber.Rows)
            //{
            //    dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());
            //    dr["UTS"] = 10;

            //}
            //bindDataToEnterInformation();

            gvSection.AutoGenerateColumns = false;
            gvSection.DataSource = dtSectionNumber;

            //Bind Data To ComboBoxSelect Section

            //cboSelectSection.DisplayMember = "SectionNumber";
            //cboSelectSection.ValueMember = "SectionCode";
            //cboSelectSection.DataSource = dtSectionNumber;

            BindDataToGridSection();
            //gvSection.Rows[0].Selected = true;
            //BindDataToCboConsol();
            //BindDataToCboCalamp();
        }

        private void gvSection_Click(object sender, EventArgs e)
        {
            //ArrayList arTemp = new ArrayList();
            //try
            //{
            //    dtSagTension.Rows.Clear();
            //    dtPole.Rows.Clear();
            //    dtConductorDay1.Columns.Clear();
            //    dtConductorDay1.Rows.Clear();

            //    bool chkAdd = true;

            //    dtSagTension = Atend.Base.Calculating.CSagAndTension.AccessSelectBySectionCode(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()), false);
            //    dtPole = Atend.Base.Calculating.CWindOnPole.AccessSelectBySectionCode(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()), false);
            //    System.Data.DataTable dtTemp = Atend.Base.Calculating.CConductorDay.AccessSelectBySectionCode(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()), false);
            //    System.Data.DataTable dtCond = Atend.Base.Calculating.CConductorDay.AccessSelect(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()), false);

            //    if (dtTemp.Rows.Count != 0)
            //    {
            //        arTemp.Add(dtTemp.Rows[0]["Temp"].ToString());

            //        foreach (DataRow dr in dtTemp.Rows)
            //        {
            //            chkAdd = true;
            //            for (int i = 0; i < arTemp.Count; i++)
            //            {
            //                if (arTemp[i].ToString() == dr["Temp"].ToString())
            //                {
            //                    chkAdd = false;
            //                }
            //            }
            //            if (chkAdd)
            //            {
            //                arTemp.Add(dr["Temp"].ToString());
            //            }
            //        }

            //        dtConductorDay1.Columns.Add("From");
            //        dtConductorDay1.Columns.Add("TO");
            //        dtConductorDay1.Columns.Add("SpanLenght");
            //        dtConductorDay1.Columns.Add("ConductorName");
            //    }
            //    for (int i = 0; i < arTemp.Count; i++)
            //    {
            //        dtConductorDay1.Columns.Add(arTemp[i].ToString());
            //        dtConductorDay1.Columns.Add(arTemp[i].ToString() + "F");
            //    }

            //    foreach (DataRow dr in dtCond.Rows)
            //    {
            //        DataRow dr1 = dtConductorDay1.NewRow();
            //        dr1["From"] = dr["From"].ToString();
            //        dr1["To"] = dr["TO"].ToString();
            //        dr1["Spanlenght"] = dr["SpanLenght"].ToString();
            //        dr1["ConductorName"] = dr["ConductorName"].ToString();
            //        for (int i = 0; i < arTemp.Count; i++)
            //        {
            //            DataRow[] drTemp = dtTemp.Select(string.Format(" Temp={0} AND ConductorDayCode={1}", arTemp[i].ToString(), dr["Code"].ToString()));
            //            dr1[arTemp[i].ToString()] = drTemp[0]["Tension"].ToString();
            //            dr1[arTemp[i].ToString() + "F"] = drTemp[0]["Sag"].ToString();
            //        }
            //        dtConductorDay1.Rows.Add(dr1);
            //    }

            //    if (dtConductorDay1.Rows.Count != 0)
            //        gvConductorDay.DataSource = dtConductorDay1;

            //    if (dtSagTension.Rows.Count != 0)
            //    {
            //        gvSagAndTension.AutoGenerateColumns = false;
            //        gvSagAndTension.DataSource = dtSagTension;
            //        CheckSag();
            //    }

            //    if (dtPole.Rows.Count != 0)
            //    {
            //        gvPole.AutoGenerateColumns = false;
            //        gvPole.DataSource = dtPole;
            //        btnContinue.Enabled = true;
            //    }

            //}
            //catch (System.Exception ex)
            //{
            //    ed.WriteMessage("ERRor ={0}\n", ex.Message);
            //}
            if (gvSection.Rows.Count > 0)
            {
                BindDataToResultGrid(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()));
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {

        }

        private void btnRotate_Click(object sender, EventArgs e)
        {

        }

        private void سکشنبندیToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void ReadVoltage(int RowIndex)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("ReadVoltage\n");
            System.Data.DataTable dtsec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
            //ed.WriteMessage("ReadVoltage\n");
            if (dtsec.Rows.Count == 0)
                dtsec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[RowIndex].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

            //ed.WriteMessage("@@\n");
            Atend.Base.Design.DPackage dParent = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dtsec.Rows[0]["ProductCode"].ToString()));
            System.Data.DataTable dt = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dParent.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
            if (dt.Rows.Count == 0)
                dt = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(new Guid(dtsec.Rows[0]["ProductCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

            //ed.WriteMessage("BeforConsol Count={0}\n", dt.Rows.Count);
            if (Convert.ToInt32(dt.Rows[0]["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
            {
                //ed.WriteMessage("*******\n");
                Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dt.Rows[0]["ProductCode"].ToString()));
                if (eConsol.VoltageLevel == 400)
                    volt = 400;

                if (eConsol.VoltageLevel == 1100)
                    volt = 1100;

                if (eConsol.VoltageLevel == 20000)
                    volt = 20000;

                if (eConsol.VoltageLevel == 33000)
                    volt = 33000;
            }

            if (Convert.ToInt32(dt.Rows[0]["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
            {
                Atend.Base.Equipment.EClamp eKalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(dt.Rows[0]["ProductCode"].ToString()));
                if (eKalamp.VoltageLevel == 400)
                    volt = 400;

                if (eKalamp.VoltageLevel == 1100)
                    volt = 1100;

                if (eKalamp.VoltageLevel == 20000)
                    volt = 20000;

                if (eKalamp.VoltageLevel == 33000)
                    volt = 33000;
            }
            //ed.WriteMessage("FINISH++\n");


        }

        private void chkSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelection.Checked)
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectArea();
            //if (SelectedList.Count!=0)
            //{
            dtSectionNumber.Rows.Clear();
            //}
            BindDataTodtSectionNumber();


            if (dtSectionNumber.Rows.Count > 0)
            {
                chkSelection.Checked = true;
                BindDataToGridSection();
                ChangeColor();
            }
        }

        private void BindDataTodtSectionNumber()
        {
            Boolean chk = true;

            for (int i = 0; i < SelectedList.Count; i++)
            {
                chk = true;
                Atend.Global.Calculation.General.General.AutocadSelectedList cadInfo = (Atend.Global.Calculation.General.General.AutocadSelectedList)SelectedList[i];
                //ed.WriteMessage("Guid= "+cadInfo.ProductGuid.ToString()+"ProductType = "+cadInfo.ProductType.ToString()+"\n");

                Atend.Base.Design.DPoleSection poleSection = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(cadInfo.ProductGuid, cadInfo.ProductType);
                //ed.WriteMessage("PoleSection.SectionCode Is= "+poleSection.SectionCode.ToString()+"\n");
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
                        //    ed.WriteMessage("Add To SectionCode,SectionCode Is={0}\n", poleSection.SectionCode);
                        SectionCode.Add(poleSection.SectionCode);
                    }
                }
                else
                {
                    //  ed.WriteMessage("Add To SectionCode Else,SectionCode Is={0}\n", poleSection.SectionCode);
                    SectionCode.Add(poleSection.SectionCode);
                }
                //ed.WriteMessage("SectionCodeList.Count= "+SectionCode.Count.ToString()+"\n");
            }


            for (int i = 0; i < SectionCode.Count; i++)
            {
                //ed.WriteMessage("**Sec.Code={0}\n", SectionCode[i].ToString());

                Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
                if (sectionNUmber.Code != Guid.Empty)
                {
                    System.Data.DataRow dr = dtSectionNumber.NewRow();
                    //ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
                    dr["SectionNumber"] = sectionNUmber.Number.ToString();
                    dr["SectionCode"] = sectionNUmber.Code.ToString();
                    dtSectionNumber.Rows.Add(dr);
                }
            }
        }

        private void Weather()
        {
            Atend.Design.frmWeather weather = new Atend.Design.frmWeather();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(weather);
        }

        private void CreateSection()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            Delete();

            section = new Atend.Global.Calculation.Section();
            section.DetermineSection();

            dtSectionNumber.Rows.Clear();
            //dtSection.Rows.Clear();
            SectionCode.Clear();
            Boolean chk = true;
            if (SelectedList.Count != 0)
            {
                BindDataTodtSectionNumber();



                if (dtSectionNumber.Rows.Count > 0)
                {
                    BindDataToGridSection();
                }
                else
                {
                    ed.WriteMessage("سکشن یافت نشد\n");
                }
                //gvSection.Rows[0].Selected = true;
                //BindDataToCboConsol();
                //BindDataToCboCalamp();
            }
        }

        private void Calculation()
        {
            txtDistance.Focus();
            Guid lastSectionCode = Guid.Empty;
            //try
            //{

            if (Validation())
            {

                this.Cursor = Cursors.WaitCursor;
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                dtGlobal = Atend.Base.Design.DGlobal.AccessSelectAll();
                dtBranch = Atend.Global.Acad.UAcad.FillBranchList();
                for (int gCounter = 0; gCounter < gvSection.Rows.Count; gCounter++)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvSection.Rows[gCounter].Cells[1];
                    if (chk.Value.ToString() == "True")
                    {
                        calcOptimalSagTension = new Atend.Global.Calculation.Mechanical.CalcOptimalSagTensionmaxF();
                        calcOptimalSagTension.DtPoleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                        System.Data.DataTable dtPoleTip = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

                        //ed.WriteMessage("dtPoleSection2={0}\n", calcOptimalSagTension.DtPoleSection.Rows.Count);
                        foreach (DataRow dr in dtPoleTip.Rows)
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
                        calcOptimalSagTension.Volt = volt;


                        calcOptimalSagTension.DtPoleCond = dtGlobal;
                        calcOptimalSagTension.dtBranchList = dtBranch;



                        DataGridViewComboBoxCell cboNetCross = (DataGridViewComboBoxCell)gvSection.Rows[gCounter].Cells[4];
                        //ed.WriteMessage("NetCrossCode={0}\n", cboNetCross.Value.ToString());
                        NetCross = Atend.Base.Calculating.CNetWorkCross.AccessSelectByCode(Convert.ToInt32(cboNetCross.Value.ToString()));



                        if (volt == 400)
                        {
                            calcOptimalSagTension.Clearance = NetCross.V380;
                        }
                        if (volt == 11000)
                        {
                            calcOptimalSagTension.Clearance = NetCross.KV11;
                        }
                        if (volt == 20000)
                        {
                            calcOptimalSagTension.Clearance = NetCross.KV20;
                        }
                        if (volt == 33000)
                        {
                            calcOptimalSagTension.Clearance = NetCross.KV32;
                        }
                        //ed.WriteMessage("Create DtconductorSectionConsol={0}\n", gvSection.SelectedRows[0].Cells[0].Value.ToString());
                        calcOptimalSagTension.Volt = volt;

                        calcOptimalSagTension.DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

                        DataRow[] drs = dtConsol.Select("ROWNO=" + gvSection.Rows[gCounter].Cells[6].Value.ToString());
                        //ed.WriteMessage("RowNo={0}\n", gvSection.Rows[gCounter].Cells[6].Value.ToString());

                        if (Convert.ToBoolean(drs[0]["IsSql"].ToString()))
                        {
                            Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(drs[0]["XCode"].ToString()));
                            calcOptimalSagTension.Consol = consol;
                        }
                        else
                        {
                            Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(drs[0]["Code"].ToString()));
                            calcOptimalSagTension.Consol = consol;
                        }
                        if (calcOptimalSagTension.DtConductorSection.Rows.Count == 0)
                        {
                            //ed.WriteMessage("Create DtconductorSection={0}\n", cboSelectSection.SelectedValue.ToString());
                            calcOptimalSagTension.DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                            DataRow[] drsCalamp = dtCalamp.Select("ROWNO=" + gvSection.Rows[gCounter].Cells[5].Value.ToString());

                            if (Convert.ToBoolean(drsCalamp[0]["IsSql"].ToString()))
                            {
                                Atend.Base.Equipment.EClamp Calamp = Atend.Base.Equipment.EClamp.SelectByXCode(new Guid(drsCalamp[0]["XCode"].ToString()));
                                calcOptimalSagTension.Calamp = Calamp;
                            }
                            else
                            {
                                Atend.Base.Equipment.EClamp calamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(drsCalamp[0]["Code"].ToString()));
                                calcOptimalSagTension.Calamp = calamp;
                            }


                            //ed.WriteMessage("Count={0}\n", calcOptimalSagTension.DtConductorSection.Rows.Count);

                        }

                        System.Data.DataColumn dc1 = new System.Data.DataColumn("Angle");
                        calcOptimalSagTension.DtConductorSection.Columns.Add(dc1);

                        calcOptimalSagTension.Start = Convert.ToInt32(txtStart.Text);
                        calcOptimalSagTension.End = Convert.ToInt32(txtEnd.Text);
                        calcOptimalSagTension.Distance = Convert.ToInt32(txtDistance.Text);
                        calcOptimalSagTension.ZaribEtminan = Convert.ToDouble(gvSection.Rows[gCounter].Cells["TrustBorder"].Value.ToString());
                        calcOptimalSagTension.SectionCode = new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString());


                        se = calcOptimalSagTension.ComputeSE02();
                        int SpanCount = calcOptimalSagTension.DtConductorSection.Rows.Count;

                        dtSagTension.Rows.Clear();
                        dtPole.Rows.Clear();
                        dtConductorDay.Rows.Clear();

                        //ed.WriteMessage("Go To Select Branch\n");
                        Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(calcOptimalSagTension.DtConductorSection.Rows[0]["ProductCode"].ToString()));
                        calcOptimalSagTension.dtStTable.Rows.Clear();

                        dtSagTension = calcOptimalSagTension.CalSagTension02(MyBranch).Copy();
                        
                        dtHMax = calcOptimalSagTension.FindHMax02();

                        //ed.WriteMessage("*****dtSagTension.rows.count={0}\n", dtSagTension.Rows.Count);
                        //gvSagAndTension.AutoGenerateColumns = false;
                        //gvSagAndTension.DataSource = dtSagTension;

                        dtConductorDay = calcOptimalSagTension.CalcTempTable02();
                        //gvPole.AutoGenerateColumns = false;
                        //gvConductorDay.DataSource = dtConductorDay;

                        dtPole = calcOptimalSagTension.WindOnPole02();
                        //gvPole.DataSource = dtPole;
                        //if (dtPole.Rows.Count > 0)
                        //{
                        //    btnContinue.Enabled = true;
                        //}
                        calcOptimalSagTension.CloseConnection();
                        Save(gCounter);

                        lastSectionCode = new Guid(gvSection.Rows[gCounter].Cells[0].Value.ToString());

                    }
                }


                //DataRow dr1 = dtRow.NewRow();
                //dr1["ProjectName"] = "";
                //dr1["NumSection"] = gvSection.SelectedRows[0].Cells[1].Value.ToString();
                //dr1["FirstPole"] = "";
                //dr1["LastPole"] = "";
                //dr1["UTS"] = "";
                //dr1["SE"] = se.ToString();
                //dr1["SpanCount"] = SpanCount;
                //dr1["SpanLenght"] = "";
                //dr1["CondName"] = gvSagAndTension.Rows[0].Cells[0].Value;
                //dtRow.Rows.Add(dr1);
                //calcOptimalSagTension.IsSagOk();
                //ed.WriteMessage("********GoTO CAlc\n");

            }
            if (lastSectionCode != Guid.Empty)
            {
                BindDataToResultGrid(lastSectionCode);
            }
            //if (gvPole.Rows.Count > 0)
            //{
            //    btnContinue.Enabled = true;
            //}
            this.Cursor = Cursors.Default;
            //}
            //catch (System.Exception ex)
            //{
            //    ed.WriteMessage("ERROR IN CALC={0}\n", ex.Message);
            //}
        }

        private void TransferToEXCEL()
        {
            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(1);
            System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            int day = calender.GetDayOfMonth(DateTime.Today);
            int Month = calender.GetMonth(DateTime.Today);
            int Year = calender.GetYear(DateTime.Today);

            string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
            string NameFlash = "نتیجه محاسبات مکانیکی به روش MAXF" + date + ".xls";


            Atend.Global.Utility.UReport.CreateExcelFinalMechanical(NameFlash, false);
        }

        private void OfferPolePower()
        {
            if (gvPole.Rows.Count>0)
            {
            System.Data.DataTable dtpole02 = (System.Data.DataTable)gvPole.DataSource;

            if (dtpole02.Rows.Count > 0)
            {
                frmChoiceForce frm = new frmChoiceForce(dtpole02, false, new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()));
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);
            }
            }
            else
            {
                MessageBox.Show("لطفا ابتدا سکشن مورد نظر را انتخاب نمایید","خطا");
            }
        }

        private void RotatePole()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            System.Data.DataTable dtPole02 = (System.Data.DataTable)gvPole.DataSource;

            foreach (DataRow dr in dtPole02.Rows)
            {
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dr["dcPoleGuid"].ToString()));
                int shape = 0;
                int type = 0;
                if (dpack.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.Pole))
                {
                    //ed.WriteMessage("***Pole\n");
                    Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dpack.ProductCode);
                    shape = Pole.Shape;
                    type = Pole.Type;
                }
                if (dpack.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip))
                {
                    //ed.WriteMessage("***PoleTip\n");
                    Atend.Base.Equipment.EPoleTip PoleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(dpack.ProductCode);
                    Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.AccessSelectByCode(PoleTip.PoleCode);
                    shape = pole.Shape;
                    type = pole.Type;
                }
                //ed.WriteMessage("Type={0},Shape={1}", type, shape);
                //ed.WriteMessage("Angle={0},dcPoleGuid={1}", dr["dcAngle"].ToString(), dr["dcPoleGuid"].ToString());
                if (shape == 0)
                {

                    Atend.Global.Acad.DrawEquips.AcDrawCirclePole.RotatePoleCircle(Convert.ToDouble(dr["dcAngle"].ToString()), new Guid(dr["dcPoleGuid"].ToString()));
                }
                if (shape == 1)
                {
                    if (type == 2)
                    {
                        Atend.Global.Acad.DrawEquips.AcDrawPolygonPole.RotatePolePolygon(Convert.ToDouble(dr["dcAngle"].ToString()), new Guid(dr["dcPoleGuid"].ToString()));

                    }
                    else
                    {
                        Atend.Global.Acad.DrawEquips.AcDrawPole.RotatePole(Convert.ToDouble(dr["dcAngle"].ToString()), new Guid(dr["dcPoleGuid"].ToString()));
                    }


                }

            }
        }
        private void شرایطآبوهواییToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Weather();
        }

        private void انتقالبهفایلEXCELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransferToEXCEL();
        }

        private void خروجToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void سکشنبندیToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateSection();
        }

        private void محاسبهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculation();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Weather();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CreateSection();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Calculation();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            TransferToEXCEL();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void پیشنهادقدرتپایهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OfferPolePower();
        }

        private void چرخشپایهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotatePole();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            OfferPolePower();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            RotatePole();
        }

    }
}