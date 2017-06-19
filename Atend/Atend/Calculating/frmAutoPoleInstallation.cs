using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Windows.Media.Imaging;


namespace Atend.Calculating
{
    public partial class frmAutoPoleInstallation : Form
    {


        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        DataTable dtMergeConsol = new DataTable();
        DataTable dtMergeClamp = new DataTable();

        DataTable BranchesData = new DataTable();
        DataColumn dc1 = new DataColumn("BranchCode", System.Type.GetType("System.String"));
        DataColumn dc2 = new DataColumn("BranchType", System.Type.GetType("System.Int32"));
        DataColumn dc3 = new DataColumn("BranchOI", System.Type.GetType("System.String"));
        DataColumn dc5 = new DataColumn("Type", System.Type.GetType("System.String"));
        DataColumn dc6 = new DataColumn("Voltage", System.Type.GetType("System.Int32"));
        DataColumn dc7 = new DataColumn("PoleCode", System.Type.GetType("System.String"));

        DataColumn dc8 = new DataColumn("SPoleCode", System.Type.GetType("System.Int32"));
        DataColumn dc10 = new DataColumn("SPoleName", System.Type.GetType("System.String"));

        DataColumn dc9 = new DataColumn("EPoleCode", System.Type.GetType("System.Int32"));
        DataColumn dc11 = new DataColumn("EPoleName", System.Type.GetType("System.String"));


        Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection ConsolsOIs = new Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection();
        Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection ClampsOIs = new Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection();

        bool ForceToClose = false;
        //Guid StartPoleCode;
        //double PoleAngle = 0;
        //Autodesk.AutoCAD.DatabaseServices.ObjectId BranchOi = Autodesk.AutoCAD.DatabaseServices.ObjectId.Null;

        List<Autodesk.AutoCAD.DatabaseServices.Entity> SelectdEntities = new List<Autodesk.AutoCAD.DatabaseServices.Entity>();
        //bool IsConductor = false;


        public frmAutoPoleInstallation()
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

            BranchesData.Columns.Add(dc1);
            BranchesData.Columns.Add(dc2);
            BranchesData.Columns.Add(dc3);
            //BranchesData.Columns.Add(dc4);
            BranchesData.Columns.Add(dc5);
            BranchesData.Columns.Add(dc6);
            BranchesData.Columns.Add(dc7);

            BranchesData.Columns.Add(dc8);
            BranchesData.Columns.Add(dc10);
            BranchesData.Columns.Add(dc9);
            BranchesData.Columns.Add(dc11);

            dtMergeConsol = Atend.Base.Equipment.EConsol.SelectAllAndMerge();
            dtMergeConsol.Columns.Add("ROWNO", System.Type.GetType("System.Int32"));
            int i = 1;
            foreach (DataRow dr in dtMergeConsol.Rows)
            {
                dr["ROWNO"] = i;
                i++;
            }
            dtMergeClamp = Atend.Base.Equipment.EClamp.SelectAllAndMerge();
            dtMergeClamp.Columns.Add("ROWNO", System.Type.GetType("System.Int32"));
            i = 1;
            foreach (DataRow dr in dtMergeClamp.Rows)
            {
                dr["ROWNO"] = i;
                i++;
            }
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void شرایطابوهواییToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Atend.Design.frmWeather weather = new Atend.Design.frmWeather();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(weather);

        }

        //private void cboConsolType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindToGvConsolTip();
        //}

        private void BindToCboNetCross()
        {
            cboNetCross.DisplayMember = "Name";
            cboNetCross.ValueMember = "Code";
            cboNetCross.DataSource = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
        }

        //private void BindToGvConsolTip()
        //{
        //    DataView dv = new DataView();
        //    //MessageBox.Show("consol counr: " + dtMergeConsol.Rows.Count.ToString());
        //    dv.Table = dtMergeConsol;
        //    dv.RowFilter = " Type = " + Convert.ToInt16(cboConsolCross.SelectedIndex);
        //    gvConsolsTip.AutoGenerateColumns = false;
        //    gvConsolsTip.DataSource = dv;
        //    ChangeColor();
        //    //ed.WriteMessage("BindToGvConsolTip finished \n");
        //}

        //private void BindToGvClamp()
        //{
        //    DataView dv = new DataView();
        //    // MessageBox.Show(dtMergeClamp.Rows.Count.ToString());
        //    dv.Table = dtMergeClamp;
        //    if (Atend.Control.NumericValidation.Int32Converter(cboVoltage.Text))
        //    {
        //        dv.RowFilter = " VoltageLevel = " + Convert.ToInt32(cboVoltage.Text);
        //    }
        //    else
        //    {
        //        dv.RowFilter = " VoltageLevel >0 ";
        //    }
        //    gvClamp.AutoGenerateColumns = false;
        //    gvClamp.DataSource = dv;
        //    ChangeColor();
        //    //ed.WriteMessage("BindToGvClamp finished \n");

        //}

        //public void ChangeColor()
        //{
        //    for (int i = 0; i < gvConsolsTip.Rows.Count; i++)
        //    {
        //        if (Convert.ToBoolean(gvConsolsTip.Rows[i].Cells["Is"].Value) == false)
        //        {
        //            gvConsolsTip.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
        //        }
        //    }

        //    for (int i = 0; i < gvClamp.Rows.Count; i++)
        //    {
        //        if (Convert.ToBoolean(gvClamp.Rows[i].Cells[3].Value) == false)
        //        {
        //            gvClamp.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
        //        }
        //    }

        //}

        private void BindDataToConsol()
        {
            cboConsolCross.DisplayMember = "Name";
            cboConsolCross.ValueMember = "ROWNO";
            DataView dv = new DataView(dtMergeConsol);
            dv.RowFilter = "ConsolType<>0 AND ConsolType<>1";
            cboConsolCross.DataSource = dv;

            DataTable dtMergeConsol1 = dtMergeConsol.Copy();
            cboConsolTension.DisplayMember = "Name";
            cboConsolTension.ValueMember = "ROWNO";
            DataView dv1 = new DataView(dtMergeConsol1);
            dv1.RowFilter = "ConsolType=0 OR ConsolType=1";
            cboConsolTension.DataSource = dv1;

        }

        private void BindDataToClamp()
        {
            cboClampCross.DisplayMember = "Name";
            cboClampCross.ValueMember = "ROWNO";

            DataView dv = new DataView(dtMergeClamp);
            dv.RowFilter = "Type=5";
            cboClampCross.DataSource = dv;


            DataTable dtMergeClamp1 = dtMergeClamp.Copy();
            DataView dv1 = new DataView(dtMergeClamp1);
            dv1.RowFilter = "Type<>5";
            cboClampTension.DisplayMember = "Name";
            cboClampTension.ValueMember = "ROWNO";
            cboClampTension.DataSource = dv1;

        }

        private void frmAutoPoleInstallation_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            gvBranches.AutoGenerateColumns = false;
            BindDataToClamp();
            BindDataToClamp();
            BindDataToConsol();
            BindToCboNetCross();


            if (cboNetCross.Items.Count > 0)
                cboNetCross.SelectedIndex = cboNetCross.Items.Count - 1;

            if (cboBranchType.Items.Count > 0)
                cboBranchType.SelectedIndex = cboBranchType.Items.Count - 1;

            if (cboConsolCross.Items.Count > 0)
                cboConsolCross.SelectedIndex = cboConsolCross.Items.Count - 1;

            if (cboConsolTension.Items.Count > 0)
                cboConsolTension.SelectedIndex = cboConsolTension.Items.Count - 1;

            if (cboClampCross.Items.Count > 0)
                cboClampCross.SelectedIndex = cboClampCross.Items.Count - 1;

            if (cboClampTension.Items.Count > 0)
                cboClampTension.SelectedIndex = cboClampTension.Items.Count - 1;


        }

        private bool Validation()
        {

            if (Atend.Control.Common.Demo)
            {
                //MessageBox.Show("", "");
                return false;
            }
            return true;
        }

        private void poleInsertionMenu_Click(object sender, EventArgs e)
        {

            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (Validation() && gvBranches.Rows.Count > 0)
                {

                    //foreach (Autodesk.AutoCAD.DatabaseServices.Entity ent in SelectdEntities)
                    for (int i = 0; i < gvBranches.Rows.Count; i++)
                    {
                        //SelectdEntities.Clear();
                        Atend.Global.Calculation.Mechanical.CAutoPoleInstallation _AutoPoleInstallation = new Atend.Global.Calculation.Mechanical.CAutoPoleInstallation();
                        //ed.WriteMessage("--$$--1\n");
                        _AutoPoleInstallation.Se = Convert.ToDouble(txtSe.Text);
                        //ed.WriteMessage("--$$--2\n");
                        _AutoPoleInstallation.ChangePercent = Convert.ToDouble(txtTolerance.Text);
                        //ed.WriteMessage("--$$--3\n");
                        _AutoPoleInstallation.UTS = Convert.ToDouble(txtUTS.Text);
                        //ed.WriteMessage("--$$--4\n");
                        _AutoPoleInstallation.NetCrossCode = Convert.ToInt32(cboConsolCross.SelectedValue);
                        //ed.WriteMessage("--$$--5\n");
                        _AutoPoleInstallation.Relibility = Convert.ToDouble(txtRelibility.Text);
                        //ed.WriteMessage("--$$--6\n");
                        _AutoPoleInstallation.MaxSectionLength = Convert.ToDouble(txtMaxSectionLength.Text);
                        //ed.WriteMessage("--$$--7\n");
                        _AutoPoleInstallation.SelectedBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(gvBranches.Rows[i].Cells["BranchCode"].Value.ToString()));
                        //ed.WriteMessage("--$$--8\n");



                        Atend.Base.Design.DPackage PolePackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(gvBranches.Rows[i].Cells["PoleCode"].Value.ToString()));
                        if (PolePackage.Type == (int)Atend.Control.Enum.ProductType.Pole)
                        {
                            _AutoPoleInstallation.SelectedPole = Atend.Base.Equipment.EPole.AccessSelectByCode(PolePackage.ProductCode);
                        }
                        else if (PolePackage.Type == (int)Atend.Control.Enum.ProductType.PoleTip)
                        {
                            Atend.Base.Equipment.EPoleTip PoleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(PolePackage.ProductCode);
                            if (PoleTip.Code != -1)
                            {
                                _AutoPoleInstallation.SelectedPole = Atend.Base.Equipment.EPole.AccessSelectByCode(PoleTip.PoleCode);
                            }
                        }



                        //ed.WriteMessage("--$$--9\n");


                        if (cboBranchType.SelectedIndex == 0)
                        {
                            DataRow[] drs = dtMergeConsol.Select("ROWNO=" + cboConsolCross.SelectedValue.ToString());
                            if (drs.Length > 0 && Convert.ToBoolean(drs[0]["IsSql"]) == true)
                            {
                                _AutoPoleInstallation.SelectedConsol = Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(new Guid(drs[0]["XCode"].ToString()));
                            }
                            else
                            {
                                _AutoPoleInstallation.SelectedConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(drs[0]["Code"]));
                            }

                            drs = dtMergeConsol.Select("ROWNO=" + cboConsolTension.SelectedValue.ToString());
                            if (drs.Length > 0 && Convert.ToBoolean(drs[0]["IsSql"]) == true)
                            {
                                _AutoPoleInstallation.SelectedConsolTension = Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(new Guid(drs[0]["XCode"].ToString()));
                            }
                            else
                            {
                                _AutoPoleInstallation.SelectedConsolTension = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(drs[0]["Code"]));
                            }

                            _AutoPoleInstallation.SelectedClamp = null;
                            _AutoPoleInstallation.SelectedClampTension = null;

                        }
                        else
                        {
                            _AutoPoleInstallation.SelectedConsol = null;
                            _AutoPoleInstallation.SelectedConsolTension = null;

                            DataRow[] drs = dtMergeClamp.Select("ROWNO=" + cboClampCross.SelectedValue.ToString());
                            if (drs.Length > 0 && Convert.ToBoolean(drs[0]["IsSql"]) == true)
                            {
                                _AutoPoleInstallation.SelectedClamp = Atend.Base.Equipment.EClamp.SelectByXCodeForDesign(new Guid(drs[0]["XCode"].ToString()));
                            }
                            else
                            {
                                _AutoPoleInstallation.SelectedClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(drs[0]["Code"]));
                            }


                            drs = dtMergeClamp.Select("ROWNO=" + cboClampTension.SelectedValue.ToString());
                            if (drs.Length > 0 && Convert.ToBoolean(drs[0]["IsSql"]) == true)
                            {
                                _AutoPoleInstallation.SelectedClampTension = Atend.Base.Equipment.EClamp.SelectByXCodeForDesign(new Guid(drs[0]["XCode"].ToString()));
                            }
                            else
                            {
                                _AutoPoleInstallation.SelectedClampTension = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(drs[0]["Code"]));
                            }
                        }
                        //ed.WriteMessage("CLAMP CROSS : {0} \n", _AutoPoleInstallation.SelectedClamp.Name);
                        //ed.WriteMessage("CLAMP CROSS TENSION : {0} \n", _AutoPoleInstallation.SelectedClampTension.Name);


                        _AutoPoleInstallation.BranchEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(new Autodesk.AutoCAD.DatabaseServices.ObjectId(new IntPtr(Convert.ToInt32(gvBranches.Rows[i].Cells["BranchOI"].Value)))) as Autodesk.AutoCAD.DatabaseServices.Line;
                        //_AutoPoleInstallation.BranchEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(BranchOi) as Autodesk.AutoCAD.DatabaseServices.Line;
                        if (_AutoPoleInstallation.PoleInstallationWithoutForbiddenArea(new Guid(gvBranches.Rows[i].Cells["PoleCode"].Value.ToString())))

                        //ed.WriteMessage("--$$--11\n");
                        //double answer = 0;
                        //int t = _AutoPoleInstallation.SpanCalculation(out answer);
                        //MessageBox.Show("answer:" + answer + "t:" + t);
                        {
                            DataGridViewImageCell _IC = gvBranches.Rows[i].Cells["StatusImage"] as DataGridViewImageCell;
                            if (_IC != null)
                            {
                                _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\button_ok.png");
                                ed.WriteMessage("image assigned \n");
                                gvBranches.Refresh();
                            }
                        }
                        else
                        {
                            MessageBox.Show("خطا در زمان انجام پایه گذاری اتوماتیک");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Guid StartPoleCode = Guid.Empty;
            Autodesk.AutoCAD.DatabaseServices.ObjectId _BranchOi = Autodesk.AutoCAD.DatabaseServices.ObjectId.Null;
            int _vol = 0;
            //gClamp.Visible = false;
            //gConsol.Visible = false;
            BranchesData.Clear();

            this.Hide();

            PromptSelectionOptions pso = new PromptSelectionOptions();
            pso.MessageForAdding = "Select Branches Please";

            //PromptEntityOptions PEO = new PromptEntityOptions(": \n");
            //PromptEntityResult PER = ed.GetEntity(PEO);
            PromptSelectionResult PSR = ed.GetSelection(pso);
            SelectdEntities.Clear();
            if (PSR.Status == PromptStatus.OK)
            {
                SelectionSet ss = PSR.Value;
                ////////if (ss.Count > 0)
                ////////{
                ////////    Atend.Base.Acad.AT_INFO SelectedBranchInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ss[0].ObjectId);
                ////////    if (SelectedBranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                ////////    {
                ////////        IsConductor = true;
                ////////    }
                ////////    else
                ////////    {
                ////////        IsConductor = false;
                ////////    }
                ////////}
                //MessageBox.Show("NUmber of selected entity : " + ss.Count.ToString());
                foreach (SelectedObject so in ss)
                {
                    ConsolsOIs.Clear();
                    ClampsOIs.Clear();
                    Atend.Base.Acad.AT_INFO BranchInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(so.ObjectId);
                    if (BranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                    {
                        //MessageBox.Show("it was conductor");
                        //gConsol.Visible = true;
                        Atend.Base.Acad.AT_SUB BranchSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(so.ObjectId);
                        foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId oi in BranchSub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO ConsolInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (ConsolInfo.NodeType == (int)Atend.Control.Enum.ProductType.Consol)
                            {
                                Atend.Base.Equipment.EConsol ConsolData = Atend.Base.Equipment.EConsol.AccessSelectByCode(ConsolInfo.ProductCode);
                                if (ConsolData.ConsolType == 0 || ConsolData.ConsolType == 1)
                                {
                                    ConsolsOIs.Add(oi);
                                    Autodesk.AutoCAD.DatabaseServices.Line BranchEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(so.ObjectId) as Autodesk.AutoCAD.DatabaseServices.Line;
                                    if (BranchEntity != null)
                                    {
                                        if (Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi)) == BranchEntity.StartPoint)
                                        {
                                            StartPoleCode = new Guid(ConsolInfo.ParentCode);
                                            _BranchOi = so.ObjectId;
                                            _vol = ConsolData.VoltageLevel;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (BranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                    {
                        //MessageBox.Show("it was self keeper");
                        //gClamp.Visible = true;
                        Atend.Base.Acad.AT_SUB BranchSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(so.ObjectId);
                        foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId oi in BranchSub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO ClampInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (ClampInfo.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
                            {
                                Atend.Base.Equipment.EClamp ClampData = Atend.Base.Equipment.EClamp.AccessSelectByCode(ClampInfo.ProductCode);
                                if (ClampData.Type != 5)
                                {
                                    ClampsOIs.Add(oi);
                                    Autodesk.AutoCAD.DatabaseServices.Line BranchEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(so.ObjectId) as Autodesk.AutoCAD.DatabaseServices.Line;
                                    if (BranchEntity != null)
                                    {
                                        if (Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi)) == BranchEntity.StartPoint)
                                        {
                                            StartPoleCode = new Guid(ClampInfo.ParentCode);
                                            _BranchOi = so.ObjectId;
                                            _vol = ClampData.VoltageLevel;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (ConsolsOIs.Count == 2 || ClampsOIs.Count == 2)
                    {
                        //if (IsConductor)
                        //{
                        //    if (BranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                        //    {
                        SelectdEntities.Add(Atend.Global.Acad.UAcad.GetEntityByObjectID(so.ObjectId));


                        DataRow NewDataRow = BranchesData.NewRow();
                        NewDataRow["BranchCode"] = BranchInfo.NodeCode;
                        NewDataRow["BranchType"] = Convert.ToInt32(BranchInfo.NodeType);
                        NewDataRow["BranchOI"] = _BranchOi.ToString().Substring(1, _BranchOi.ToString().Length - 2);
                        //NewDataRow["IsSelected"] = true;
                        NewDataRow["Type"] = (Convert.ToInt32(BranchInfo.NodeType) == (int)Atend.Control.Enum.ProductType.Conductor ? "سیم" : "کابل خودنگهدار");
                        NewDataRow["Voltage"] = _vol;
                        NewDataRow["PoleCode"] = StartPoleCode;
                        BranchesData.Rows.Add(NewDataRow);

                        //int NewRowIndex = gvBranches.Rows.Add();
                        //gvBranches.Rows[NewRowIndex].Cells["BranchCode"].Value = BranchInfo.ProductCode;
                        //gvBranches.Rows[NewRowIndex].Cells["BranchType"].Value = Convert.ToInt32(BranchInfo.NodeType);
                        //gvBranches.Rows[NewRowIndex].Cells["BranchOI"].Value = _BranchOi.ToString().Substring(1, _BranchOi.ToString().Length - 2);
                        //gvBranches.Rows[NewRowIndex].Cells["IsSelected"].Value = true;
                        //gvBranches.Rows[NewRowIndex].Cells["Type"].Value = (Convert.ToInt32(BranchInfo.NodeType) == (int)Atend.Control.Enum.ProductType.Conductor ? "سیم" : "کابل خودنگهدار");
                        //gvBranches.Rows[NewRowIndex].Cells["Voltage"].Value = _vol;


                        //    }
                        //    gConsol.Visible = true;
                        //    gClamp.Visible = false;
                        //}
                        //else
                        //{
                        //    if (BranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                        //    {
                        //        SelectdEntities.Add(Atend.Global.Acad.UAcad.GetEntityByObjectID(so.ObjectId));
                        //    }
                        //    gConsol.Visible = false;
                        //    gClamp.Visible = true;
                        //}
                    }

                }//end for

            }// end if (PSR.Status == PromptStatus.OK)
            this.Show();
            DataView dv = new DataView(BranchesData);
            dv.RowFilter = "BranchType=" + (cboBranchType.SelectedIndex == 0 ? (int)Atend.Control.Enum.ProductType.Conductor : (int)Atend.Control.Enum.ProductType.SelfKeeper);
            gvBranches.DataSource = dv;
            //MessageBox.Show("count:" + SelectdEntities.Count.ToString());

        }

        private void gClamp_Enter(object sender, EventArgs e)
        {

        }

        private void cboBranchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(BranchesData);
            dv.RowFilter = "BranchType=" + (cboBranchType.SelectedIndex == 0 ? (int)Atend.Control.Enum.ProductType.Conductor : (int)Atend.Control.Enum.ProductType.SelfKeeper);
            gvBranches.DataSource = dv;

            if (cboBranchType.SelectedIndex == 0)
            {
                gConsol.Visible = true;
                gClamp.Visible = false;
            }
            else
            {
                gConsol.Visible = false;
                gClamp.Visible = true;

            }


        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void cboClampCross_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboClampCross.Text != string.Empty)
            {
                //MessageBox.Show("1");
                DataRow[] drs = dtMergeClamp.Select(string.Format("ROWNO={0}", cboClampCross.SelectedValue));
                if (drs.Length > 0)
                {
                    //MessageBox.Show("2");
                    if (Convert.ToBoolean(drs[0]["IsSql"]) == true)
                    {
                        //MessageBox.Show("SQL");
                        txtClampCross.Text = Atend.Base.Equipment.EClamp.SelectByXCode(new Guid(drs[0]["XCode"].ToString())).VoltageLevel.ToString();
                    }
                    else
                    {
                        //MessageBox.Show("Access");
                        txtClampCross.Text = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(drs[0]["Code"])).VoltageLevel.ToString();
                    }
                }
            }
        }

        private void cboClampTension_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboClampCross.Text != string.Empty)
            {
                //MessageBox.Show("1");
                DataRow[] drs = dtMergeClamp.Select(string.Format("ROWNO={0}", cboClampCross.SelectedValue));
                if (drs.Length > 0)
                {
                    //MessageBox.Show("2");
                    if (Convert.ToBoolean(drs[0]["IsSql"]) == true)
                    {
                        //MessageBox.Show("SQL");
                        txtClampTension.Text = Atend.Base.Equipment.EClamp.SelectByXCode(new Guid(drs[0]["XCode"].ToString())).VoltageLevel.ToString();
                    }
                    else
                    {
                        //MessageBox.Show("Access");
                        txtClampTension.Text = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(drs[0]["Code"])).VoltageLevel.ToString();
                    }
                }
            }
        }

        private void cboConsolCross_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboConsolCross.Text != string.Empty)
            {
                //MessageBox.Show("1");
                DataRow[] drs = dtMergeConsol.Select(string.Format("ROWNO={0}", cboConsolCross.SelectedValue));
                if (drs.Length > 0)
                {
                    //MessageBox.Show("2");
                    if (Convert.ToBoolean(drs[0]["IsSql"]) == true)
                    {
                        //MessageBox.Show("SQL");
                        txtConsolCross.Text = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(drs[0]["XCode"].ToString())).VoltageLevel.ToString();
                    }
                    else
                    {
                        //MessageBox.Show("Access");
                        txtConsolCross.Text = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(drs[0]["Code"])).VoltageLevel.ToString();
                    }
                }
            }
        }

        private void cboConsolTension_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboConsolTension.Text != string.Empty)
            {
                //MessageBox.Show("1");
                DataRow[] drs = dtMergeConsol.Select(string.Format("ROWNO={0}", cboConsolTension.SelectedValue));
                if (drs.Length > 0)
                {
                    //MessageBox.Show("2");
                    if (Convert.ToBoolean(drs[0]["IsSql"]) == true)
                    {
                        //MessageBox.Show("SQL");
                        txtConsolTension.Text = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(drs[0]["XCode"].ToString())).VoltageLevel.ToString();
                    }
                    else
                    {
                        //MessageBox.Show("Access");
                        txtConsolTension.Text = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(drs[0]["Code"])).VoltageLevel.ToString();
                    }
                }
            }
        }

        //private void cboVoltage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindToGvClamp();
        //}

        //private void btnConsolAdd_Click(object sender, EventArgs e)
        //{
        //    if (gvConsolsTip.SelectedRows.Count > 0)
        //    {
        //        gvSelectedConsol.Rows.Add();
        //        //MessageBox.Show(gvConsolsTip.Rows[gvConsolsTip.CurrentRow.Index].Cells[2].Value.ToString());
        //        gvSelectedConsol.Rows[gvSelectedConsol.Rows.Count - 1].Cells[0].Value = gvConsolsTip.Rows[gvConsolsTip.CurrentRow.Index].Cells[0].Value.ToString();
        //        gvSelectedConsol.Rows[gvSelectedConsol.Rows.Count - 1].Cells[1].Value = gvConsolsTip.Rows[gvConsolsTip.CurrentRow.Index].Cells[1].Value.ToString();
        //        gvSelectedConsol.Rows[gvSelectedConsol.Rows.Count - 1].Cells[2].Value = gvConsolsTip.Rows[gvConsolsTip.CurrentRow.Index].Cells[2].Value.ToString();
        //        gvSelectedConsol.Rows[gvSelectedConsol.Rows.Count - 1].Cells[3].Value = gvConsolsTip.Rows[gvConsolsTip.CurrentRow.Index].Cells[3].Value.ToString();
        //        gvSelectedConsol.Rows[gvSelectedConsol.Rows.Count - 1].Cells[4].Value = gvConsolsTip.Rows[gvConsolsTip.CurrentRow.Index].Cells[4].Value.ToString();
        //    }
        //}

        //private void btnClampAdd_Click(object sender, EventArgs e)
        //{
        //    if (gvClamp.SelectedRows.Count > 0)
        //    {
        //        int i = gvSelectedClamp.Rows.Add();
        //        //MessageBox.Show(gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[2].Value.ToString());
        //        gvSelectedClamp.Rows[i].Cells[0].Value = gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[0].Value.ToString();
        //        gvSelectedClamp.Rows[i].Cells[1].Value = gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[1].Value.ToString();
        //        gvSelectedClamp.Rows[i].Cells[2].Value = gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[2].Value.ToString();
        //    }
        //}

    }
}