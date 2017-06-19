using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using System.Xml;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using System.IO;

namespace Atend.Design
{
    public partial class frmDrawPole03 : Form
    {
        //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public int shape;
        public int Type;
        bool AllowClose = true;
        DataTable dtMergePole = new DataTable();
        DataTable dtMergeConsol = new DataTable();
        DataTable dtgvPoleConsol = new DataTable();
        //DataTable dttemp = new DataTable();
        bool ForceToClose = false;
        System.Data.DataTable dtWorkOrders;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo = new Atend.Base.Design.DPoleInfo();
            Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.PoleData.eConsols = new List<Atend.Base.Equipment.EConsol>();
            Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.PoleData.eHalter = new Atend.Base.Equipment.EHalter();
            Atend.Base.Acad.AcadGlobal.PoleData.eHalterCount = 0;
            Atend.Base.Acad.AcadGlobal.PoleData.ePole = new Atend.Base.Equipment.EPole();
            Atend.Base.Acad.AcadGlobal.PoleData.ePoleTip = new Atend.Base.Equipment.EPoleTip();
            Atend.Base.Acad.AcadGlobal.PoleData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.PoleData.HalterExistance = 0;
            Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.PoleData.Height = 0;
            Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.PoleData.UseAccess = false;


            Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo = new Atend.Base.Design.DPoleInfo();
            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols = new List<Atend.Base.Equipment.EConsol>();
            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalter = new Atend.Base.Equipment.EHalter();
            Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalterCount = 0;
            Atend.Base.Acad.AcadGlobal.CirclePoleData.ePole = new Atend.Base.Equipment.EPole();
            Atend.Base.Acad.AcadGlobal.CirclePoleData.ePoleTip = new Atend.Base.Equipment.EPoleTip();
            Atend.Base.Acad.AcadGlobal.CirclePoleData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterExistance = 0;
            Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.CirclePoleData.Height = 0;
            Atend.Base.Acad.AcadGlobal.CirclePoleData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.CirclePoleData.UseAccess = false;


            Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo = new Atend.Base.Design.DPoleInfo();
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols = new List<Atend.Base.Equipment.EConsol>();
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess = new System.Collections.ArrayList();
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalter = new Atend.Base.Equipment.EHalter();
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalterCount = 0;
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePole = new Atend.Base.Equipment.EPole();
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePoleTip = new Atend.Base.Equipment.EPoleTip();
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterExistance = 0;
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.Height = 0;
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.PolygonPoleData.UseAccess = false;

        }

        public frmDrawPole03()
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
            dtMergeConsol = Atend.Base.Equipment.EConsol.SelectAllAndMerge();
            dtWorkOrders = Atend.Base.Base.BWorkOrder.SelectChilds();

        }

        private bool Validation()
        {
            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}


            //btnDelete.Focus();
            if (txtHeight.Text == "")
            {
                MessageBox.Show("لطفا پایه مورد نظر را انتخاب نمایید", "خطا");
                tabControl1.TabPages[0].Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.Int32Converter(txtCountPole.Text))
            {
                MessageBox.Show("لطفا  تعداد پایه مورد نظر را انتخاب نمایید", "خطا");
                txtCountPole.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtHeightFromGround.Text))
            {
                MessageBox.Show("لطفا ارتفاع از سطح زمین را مشخص نمایید", "خطا");
                txtHeightFromGround.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtHeightFromGround.Text))
            {
                MessageBox.Show("لطفا  رتفاع از سطح زمین رابا فرمت مناسب وارد نمایید", "خطا");
                txtHeightFromGround.Focus();
                return false;
            }

            //if (gvPoleConsol.Rows.Count == 0)
            //{
            //    MessageBox.Show("لطفاً کنسول های پایه را انتخاب کنید", "خطا");
            //    return false;
            //}

            if (gvPoleConsol.Rows.Count > 4)
            {
                MessageBox.Show("شما مجاز به انتخاب حداکثر 4 کنسول برای این پایه هستید", "خطا");
                return false;
            }

            //for (int i = 0; i < gvPoleConsol.Rows.Count; i++)
            //{
            //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvPoleConsol.Rows[i].Cells[6];

            //    if (c.Value == null)
            //    {
            //        MessageBox.Show("لطفاً کد وضعیت تجهیز کنسول را انتخاب کنید", "خطا");
            //        return false;
            //    }
            //}

            if (string.IsNullOrEmpty(cboIsExist.Text))
            {
                MessageBox.Show("لطفاً موجود یاپیشنهادی پایه را انتخاب کنید", "خطا");
                return false;
            }

            //for (int i = 0; i < gvPoleConsol.Rows.Count; i++)
            //{
            //    if (gvPoleConsol.Rows[i].Cells[5].Value == null)
            //    {
            //        MessageBox.Show("لطفاً تعداد کنسول را انتخاب کنید", "خطا");
            //        return false;
            //    }
            //    if (!Atend.Control.NumericValidation.Int32Converter(gvPoleConsol.Rows[i].Cells[5].Value.ToString()))
            //    {
            //        MessageBox.Show("لطفاً تعداد کنسول را با فرمت مناسب انتخاب کنید", "خطا");
            //        return false;
            //    }
            //}

            //for (int i = 0; i < gvPoleConsol.Rows.Count; i++)
            //{
            //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvPoleConsol.Rows[i].Cells[7];

            //    if (c.Value == null)
            //    {
            //        MessageBox.Show("لطفاً کد دستور کار کنسول را انتخاب کنید", "خطا");
            //        return false;
            //    }
            //}

            if (gvPole.Rows.Count > 0 && gvPole.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("gvPole.Rows.Count:" + gvPole.Rows.Count + "gvPole.SelectedRows.Count:" + gvPole.SelectedRows.Count);
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;
            }


            return true;
        }

        private void BindDataToHalterType()
        {
            cboHalterType.DataSource = Atend.Base.Equipment.EHalter.SelectAllX();
            cboHalterType.DisplayMember = "Name";
            cboHalterType.ValueMember = "XCode";
            if (cboHalterType.Items.Count > 0)
            {
                //cboHalterType.SelectedValue = cboHalterType.Items.Count - 1;
                cboConsolType.SelectedIndex = 0;
            }

            //cboConsolType.SelectedIndex = 0;

        }

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt.Copy();
            //cboMProjectCode.DataSource = dt;
            //cboMProjectCode.DisplayMember = "Name";
            //cboMProjectCode.ValueMember = "Code";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            double height = -1;
            double power = -1;
            int type = -1;
            bool check = false;
            string strFilter = "";
            if (chkHeight.Checked)
            {
                height = Convert.ToDouble(nudHeight.Value);
                strFilter = " height='" + height + "'";
                check = true;
            }
            if (chkPower.Checked)
            {
                power = Convert.ToDouble(nudPower.Value);
                if (strFilter != "")
                    strFilter += " AND Power='" + power + "'";
                else
                {
                    strFilter = " Power='" + power + "'";
                }
                check = true;
            }
            if (chkType.Checked)
            {
                type = Convert.ToInt32(cboType.SelectedValue);
                if (strFilter != "")
                {
                    strFilter += " AND Type='" + type + "'";
                }
                else
                {
                    strFilter = "Type='" + type + "'";
                }
                check = true;
            }
            if (check)
            {
                DataView dv = new DataView();
                dv.Table = dtMergePole;
                dv.RowFilter = strFilter;
                dv.Sort = "XCode,Power";
                gvPole.AutoGenerateColumns = false;
                gvPole.DataSource = dv;

            }
            else
            {
                gvPole.AutoGenerateColumns = false;
                gvPole.DataSource = dtMergePole;
            }

            ChangeColor();
        }

        private void BindDataToConsolTip()
        {
            DataView dv = new DataView();
            dv.Table = dtMergeConsol;
            dv.RowFilter = " Type = '" + Convert.ToInt16(cboConsolType.SelectedIndex) + "'";
            gvConsolsTip.AutoGenerateColumns = false;
            gvConsolsTip.DataSource = dv;
            ChangeColor();
        }

        //Confirmed
        private void SetDefaultValues()
        {
            if (Atend.Control.ConnectionString.StatusDef)
            {
                if (Atend.Control.Common.IsExist != -1)
                    cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
                if (Atend.Control.Common.ProjectCode != -1)
                    cboProjCode.SelectedValue = Atend.Control.Common.ProjectCode;
                if (Atend.Control.Common.ProjectCode != -1)
                    cboMProjectCode.SelectedValue = Atend.Control.Common.ProjectCode;
            }
        }

        //Confirmed
        private void BindDataToGridMain()
        {
            dtMergePole = Atend.Base.Equipment.EPole.SelectAllAndMerge();
            gvPole.AutoGenerateColumns = false;
            gvPole.DataSource = dtMergePole;
            ChangeColor();
        }

        private void BindDataToGridPoleConsol()
        {
            DataColumn dcXCode = new DataColumn("XCode");
            dtgvPoleConsol.Columns.Add(dcXCode);
            DataColumn dcCode = new DataColumn("Code");
            dtgvPoleConsol.Columns.Add(dcCode);
            DataColumn dcName = new DataColumn("Name");
            dtgvPoleConsol.Columns.Add(dcName);
            DataColumn dcConsolType = new DataColumn("ConsolType");
            dtgvPoleConsol.Columns.Add(dcConsolType);
            DataColumn dcIsSql = new DataColumn("IsSql");
            dtgvPoleConsol.Columns.Add(dcIsSql);
            DataColumn dcCount = new DataColumn("Count");
            dtgvPoleConsol.Columns.Add(dcCount);
            DataColumn dcIsExistance = new DataColumn("IsExistance", System.Type.GetType("System.Int32"));
            dtgvPoleConsol.Columns.Add(dcIsExistance);
            DataColumn dcProjectCode = new DataColumn("ProjectCode", System.Type.GetType("System.Int32"));
            dtgvPoleConsol.Columns.Add(dcProjectCode);


            gvPoleConsol.AutoGenerateColumns = false;
            gvPoleConsol.DataSource = dtgvPoleConsol;

            BindDataToCboInGridView();
            AddProjectCodeColumn();


        }

        private void frmDrawPole01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ResetClass();
            BindDataToGridMain();
            BindDataToConsolTip();
            BindDataToGridPoleConsol();
            BindDataToHalterType();
            BindDataToComboBoxIsExist();


            if (cboConsolType.Items.Count > 0)
            {
                cboConsolType.SelectedIndex = 0;
            }

            if (cboType.Items.Count > 0)
            {
                cboType.SelectedIndex = 0;
            }

            SetDefaultValues();
        }

        public void BindDataToCboInGridView()
        {
            DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dt;
            c.DisplayMember = "Name";
            c.ValueMember = "Code";
            c.DataPropertyName = "IsExistance";
            c.HeaderText = " وضعیت تجهیز";
            gvPoleConsol.Columns.Insert(gvPoleConsol.Columns.Count - 1, c);
            ChangeColor();

        }

        public void AddProjectCodeColumn()
        {
            //System.Data.DataTable dt2 = Atend.Base.Base.BWorkOrder.SelectChilds();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dtWorkOrders;
            c.DisplayMember = "Name";
            c.ValueMember = "ACode";
            c.DataPropertyName = "ProjectCode";
            c.HeaderText = "شرح دستورکار";
            gvPoleConsol.Columns.Insert(gvPoleConsol.Columns.Count - 1, c);
        }

        //Confirmed
        public void BindDataToComboBoxIsExist()
        {

            cboMIsExist.DisplayMember = "Name";
            cboMIsExist.ValueMember = "Code";
            cboMIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();

            DataTable dtstatus = Atend.Base.Base.BEquipStatus.SelectAllX();
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
            //MessageBox.Show(dtstatus.Rows.Count.ToString());

            //if (Atend.Control.Common.IsExist == -1)
            //{

            DataRow[] drs = dtstatus.Select("IsDefault=True");
            if (drs.Length > 0)
            {
                cboIsExist.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
                cboMIsExist.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
            }
            //MessageBox.Show("finished");

            //    for (int i = 0; i < dtstatus.Rows.Count; i++)
            //    {
            //        if (Convert.ToBoolean(dtstatus.Rows[i]["IsDefault"].ToString()))
            //        {
            //            //cboIsExist.SelectedIndex = i;
            //            cboIsExist.SelectedValue = dtstatus.Rows[i]["Code"];
            //            cboMIsExist.SelectedValue = dtstatus.Rows[i]["Code"];
            //        }
            //    }
            //}
            //else
            //{
            //    cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
            //}

            //dttemp = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));

            //cboIsExist.DisplayMember = "Name";
            //cboIsExist.ValueMember = "Code";
            //cboIsExist.DataSource = Atend.Control.Common.StatuseCode;


            //cboMIsExist.DisplayMember = "Name";
            //cboMIsExist.ValueMember = "Code";
            //cboMIsExist.DataSource = Atend.Control.Common.StatuseCode.Copy();


            //cboIsExist.SelectedIndex = 4;
            //cboMIsExist.SelectedIndex = 4;

        }

        private void gvPole_Click(object sender, EventArgs e)
        {
            Atend.Base.Equipment.EPole pole;
            if (gvPole.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvPole.Rows[gvPole.CurrentRow.Index].Cells[2].Value))
                {
                    pole = Atend.Base.Equipment.EPole.SelectByXCode(new Guid(
                        gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else
                {
                    pole = Atend.Base.Equipment.EPole.AccessSelectByCode(Convert.ToInt32(
                   gvPole.Rows[gvPole.CurrentRow.Index].Cells[1].Value.ToString()));
                }
                txtBottomCrossSectionArea.Text = Math.Round(pole.ButtomCrossSectionArea, 2).ToString();
                txtHeight.Text = Math.Round(pole.Height, 2).ToString();
                txtPower.Text = Math.Round(pole.Power, 2).ToString();
                txtTopCrossSectionArea.Text = Math.Round(pole.TopCrossSectionArea, 2).ToString();
                if (pole.Type == 0)
                {
                    txtType.Text = "بتونی";
                }
                if (pole.Type == 1)
                {
                    txtType.Text = "فلزی";
                }
                if (pole.Type == 2)
                {
                    txtType.Text = "تلسکوپی";
                }
                if (pole.Type == 3)
                {
                    txtType.Text = "چوبی";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gvConsolsTip.DataSource;
            dt.DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", txtSearch.Text);
        }

        private void gvConsolsTip_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //MessageBox.Show(gvConsolsTip.Rows[gvConsolsTip.CurrentRow.Index].Cells[0].Value.ToString());
            if (gvConsolsTip.Rows.Count > 0)
            {
                tvConsolSubEquipment.Nodes.Clear();
                Guid ContainerXCode;
                int ContainerCode;
                DataTable ProductPackageTable;
                int Type = (int)Atend.Control.Enum.ProductType.Consol;
                bool IsSql = Convert.ToBoolean(gvConsolsTip.SelectedRows[0].Cells[4].Value);
                //ed.WriteMessage("IsSql={0}\n", IsSql);

                if (IsSql == true)
                {

                    ContainerXCode = new Guid(gvConsolsTip.SelectedRows[0].Cells[0].Value.ToString());
                    //ed.WriteMessage("ContainerXCode={0}\n", ContainerXCode);
                    ProductPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerXCodeType(ContainerXCode, Type);
                    Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.SelectByXCode(ContainerXCode);
                    Byte[] byteBLOBData = new Byte[0];
                    byteBLOBData = (Byte[])(Consol.Image);
                    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                    pictureBox1.Image = Image.FromStream(stmBLOBData);
                }
                else
                {
                    ContainerCode = Convert.ToInt32(gvConsolsTip.SelectedRows[0].Cells[1].Value.ToString());
                    ProductPackageTable = Atend.Base.Equipment.EProductPackage.AccessSelectByContainerCodeAndType(ContainerCode, Type);
                    //ed.WriteMessage("ContainerCode={0}\n", ContainerCode);
                    Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(ContainerCode);
                    Byte[] byteBLOBData = new Byte[0];
                    byteBLOBData = (Byte[])(Consol.Image);
                    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                    pictureBox1.Image = Image.FromStream(stmBLOBData);
                }

                //ed.WriteMessage("ProductPackage.Rows.Count={0}\n", ProductPackageTable.Rows.Count);
                foreach (DataRow row in ProductPackageTable.Rows)
                {

                    #region find each row TableType
                    byte TableType = Convert.ToByte(row["TableType"]);
                    #endregion
                    //ed.WriteMessage(string.Format("TableType : {0} \n", TableType));

                    #region search in XML for Table of TableType value
                    string Table = DetermineTableValue(TableType);
                    #endregion

                    //ed.WriteMessage(string.Format("Table : {0} \n", Table));
                    if (IsSql)
                    {
                        if (Table == "Self")
                        {
                            //ed.WriteMessage("Go To Local Self\n");
                            #region Switch Local
                            switch ((Atend.Control.Enum.ProductType)TableType)
                            {


                                case Atend.Control.Enum.ProductType.Insulator:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"])); //SelectByCode(Convert.ToInt32(row["ProductCode"]));       
                                    Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(insulator.Name);

                                    break;

                                case Atend.Control.Enum.ProductType.AirPost:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EAirPost airPost = Atend.Base.Equipment.EAirPost.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(airPost.Name);
                                    break;


                                case Atend.Control.Enum.ProductType.Breaker:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EBreaker breaker = Atend.Base.Equipment.EBreaker.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(breaker.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.GroundCabel:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EGroundCabel cabel = Atend.Base.Equipment.EGroundCabel.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(cabel.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.CatOut:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.ECatOut catout = Atend.Base.Equipment.ECatOut.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(catout.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Conductor:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(conductor.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Consol:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(consol.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Disconnector:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EDisconnector disconnector = Atend.Base.Equipment.EDisconnector.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(disconnector.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.GroundPost:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(groundPost.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.HeaderCabel:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EHeaderCabel headerCabel = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(headerCabel.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.Khazan:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EKhazan khzan = Atend.Base.Equipment.EKhazan.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(khzan.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Mafsal:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EMafsal mafsal = Atend.Base.Equipment.EMafsal.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(mafsal.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.Pole:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(pole.Name);
                                    break;
                                //case Atend.Control.Enum.ProductType.PT:

                                case Atend.Control.Enum.ProductType.Rod:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.ERod rod = Atend.Base.Equipment.ERod.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(rod.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.StreetBox:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EStreetBox streetBox = Atend.Base.Equipment.EStreetBox.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(streetBox.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Transformer:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.ETransformer transform = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(transform.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.InsulatorChain:
                                    Atend.Base.Equipment.EInsulatorChain insulatorChain = Atend.Base.Equipment.EInsulatorChain.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(insulatorChain.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.InsulatorPipe:
                                    Atend.Base.Equipment.EInsulatorPipe InsulatorPipe = Atend.Base.Equipment.EInsulatorPipe.SelectByXCode(new Guid(row["XCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(InsulatorPipe.Name);
                                    break;
                            }
                        }
                        else
                        {
                            ////ed.WriteMessage("second productCode : " + Convert.ToInt32(row["ProductCode"]).ToString());
                            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByXCode(Convert.ToInt32(row["ProductCode"]));
                            tvConsolSubEquipment.Nodes.Add(product.Name);
                        }
                            #endregion
                    }

                    else
                    {
                        if (Table == "Self")
                        {
                            ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));

                            #region Switch Local
                            switch ((Atend.Control.Enum.ProductType)TableType)
                            {

                                case Atend.Control.Enum.ProductType.Insulator:

                                    ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"])); //SelectByCode(Convert.ToInt32(row["ProductCode"]));       
                                    Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(insulator.Name);

                                    break;

                                case Atend.Control.Enum.ProductType.AirPost:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EAirPost airPost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(airPost.Name);
                                    break;


                                case Atend.Control.Enum.ProductType.Breaker:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EBreaker breaker = Atend.Base.Equipment.EBreaker.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(breaker.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.GroundCabel:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EGroundCabel cabel = Atend.Base.Equipment.EGroundCabel.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(cabel.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.CatOut:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.ECatOut catout = Atend.Base.Equipment.ECatOut.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(catout.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Conductor:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(conductor.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Consol:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(consol.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Disconnector:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EDisconnector disconnector = Atend.Base.Equipment.EDisconnector.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(disconnector.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.GroundPost:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(groundPost.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.HeaderCabel:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EHeaderCabel headerCabel = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(headerCabel.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.Khazan:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EKhazan khzan = Atend.Base.Equipment.EKhazan.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(khzan.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Mafsal:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EMafsal mafsal = Atend.Base.Equipment.EMafsal.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(mafsal.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.Pole:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(pole.Name);
                                    break;
                                //case Atend.Control.Enum.ProductType.PT:

                                case Atend.Control.Enum.ProductType.Rod:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.ERod rod = Atend.Base.Equipment.ERod.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(rod.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.StreetBox:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.EStreetBox streetBox = Atend.Base.Equipment.EStreetBox.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(streetBox.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.Transformer:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                    Atend.Base.Equipment.ETransformer transform = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(transform.Name);
                                    break;

                                case Atend.Control.Enum.ProductType.InsulatorChain:
                                    Atend.Base.Equipment.EInsulatorChain insulatorChain = Atend.Base.Equipment.EInsulatorChain.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(insulatorChain.Name);
                                    break;
                                case Atend.Control.Enum.ProductType.InsulatorPipe:
                                    Atend.Base.Equipment.EInsulatorPipe insulatorPipe = Atend.Base.Equipment.EInsulatorPipe.AccessSelectByCode(Convert.ToInt32(row["ProductCode"].ToString()));
                                    tvConsolSubEquipment.Nodes.Add(insulatorPipe.Name);
                                    break;
                            }
                        }
                        //else
                        //{
                        //    ////ed.WriteMessage("second productCode : " + Convert.ToInt32(row["ProductCode"]).ToString());
                        //    Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByXCode(Convert.ToInt32(row["ProductCode"]));
                        //    tvConsolSubEquipment.Nodes.Add(product.Name);
                        //}
                            #endregion


                    }
                }
            }

            /////////////////////////////////////////////////////////////////////



        }

        private void btnNewConsolTip_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmConsol02 FrmConsol = new Atend.Equipment.frmConsol02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmConsol);
            BindDataToConsolTip();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //System.Data.DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX(); //Atend.Control.Common.StatuseCode.Copy();
            //System.Data.DataTable dt2 = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString())); //Atend.Base.Base.BProjectCode.AccessSelectAll();
            if (gvConsolsTip.SelectedRows.Count > 0)
            {
                DataRow drr = dtgvPoleConsol.NewRow();

                drr["XCode"] = gvConsolsTip.SelectedRows[0].Cells[0].Value;
                drr["Code"] = gvConsolsTip.SelectedRows[0].Cells[1].Value;
                drr["Name"] = gvConsolsTip.SelectedRows[0].Cells[2].Value;
                if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "0")
                {
                    drr["ConsolType"] = "کششی";
                }
                if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "1")
                {
                    drr["ConsolType"] = "انتهایی";
                }
                if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "2")
                {
                    drr["ConsolType"] = "عبوری";
                }
                if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "3")
                {
                    drr["ConsolType"] = "DP";
                }
                drr["IsSql"] = gvConsolsTip.SelectedRows[0].Cells[4].Value;
                drr["Count"] = "1";

                drr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                if (cboProjCode.Items.Count > 0)
                {
                    drr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                }
                else
                {
                    //bool isFind = false;
                    //for (int i = 0; i < dtwo.Rows.Count && isFind == false; i++)
                    //{
                    //    if (Convert.ToInt32(dttemp.Rows[i]["ACode"].ToString()) != 0)
                    //    {
                    drr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"].ToString();
                    //isFind = true;
                    //    }
                    //}
                    //drr["ProjectCode"] = 0;
                }

                //drr["IsExistance"] = Convert.ToInt32(dt.Rows[4]["Code"]);
                //drr["ProjectCode"] = Convert.ToInt32(dt2.Rows[0]["Code"]);

                dtgvPoleConsol.Rows.Add(drr);

                //gvPoleConsol.Rows.Add();
                //gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[0].Value = gvConsolsTip.SelectedRows[0].Cells[0].Value;
                //gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[1].Value = gvConsolsTip.SelectedRows[0].Cells[1].Value;
                //gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[2].Value = gvConsolsTip.SelectedRows[0].Cells[2].Value;

                //if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "0")
                //{
                //    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[3].Value = "کششی";
                //}
                //if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "1")
                //{
                //    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[3].Value = "انتهایی";
                //}
                //if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "2")
                //{
                //    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[3].Value = "عبوری";
                //}
                //if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "3")
                //{
                //    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[3].Value = "DP";
                //}
                //gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[4].Value = gvConsolsTip.SelectedRows[0].Cells[4].Value;
                //gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[5].Value = "1";



                ////gvConsolsTip.Rows[gvPoleConsol.Rows.Count - 1].Cells[6].Value = 0;
                ////gvConsolsTip.Rows[gvPoleConsol.Rows.Count - 1].Cells[7].Value = 0;

            }
        }

        private string DetermineTableValue(int Code)
        {
            XmlDocument _xmlDoc = new XmlDocument();
            System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            string fullPath = m.FullyQualifiedName;
            try
            {
                fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
            }
            catch
            {
            }

            string xmlPath = fullPath + "\\EquipmentName.xml";
            _xmlDoc.Load(xmlPath);
            foreach (XmlElement xElement in _xmlDoc.DocumentElement)
            {
                foreach (XmlNode xnode in xElement.ChildNodes)
                {
                    if (xnode.Attributes["Code"].Value == Code.ToString())
                    {
                        return xnode.Attributes["Table"].Value;
                    }
                }
            }
            return "";
        }

        private void cboConsolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AA\n");
            BindDataToConsolTip();
            //gvConsolsTip.Refresh();
            //ed.WriteMessage("bb\n");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnCancel.Focus();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            bool useAccess = false;
            if (Validation())
            {
                dtgvPoleConsol = (System.Data.DataTable)gvPoleConsol.DataSource;
                Atend.Base.Equipment.EConsol eConsol = new Atend.Base.Equipment.EConsol();
                int Existance = 0;
                int projectcode = 0;
                Atend.Base.Equipment.EPole ePole = new Atend.Base.Equipment.EPole();
                if (Convert.ToBoolean(gvPole.Rows[gvPole.CurrentRow.Index].Cells[2].Value) == true)
                {
                    //it was sql selected
                    ePole = Atend.Base.Equipment.EPole.SelectByXCode(new Guid(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value.ToString()));
                    useAccess = false;
                }
                else
                {
                    //it was access selected
                    ePole = Atend.Base.Equipment.EPole.AccessSelectByCode(Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[1].Value.ToString()));
                    useAccess = true;
                }

                //ed.WriteMessage("\n POLE XCOde = {0}\n", ePole.XCode.ToString());
                if (ePole.Code == -1)
                {

                    //ed.WriteMessage("\nERORRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR\n");
                }
                ////ed.WriteMessage("\n77777777\n");

                Atend.Base.Equipment.EHalter Halt = new Atend.Base.Equipment.EHalter();
                if (!string.IsNullOrEmpty(cboHalterType.Text))
                {
                    Halt = Atend.Base.Equipment.EHalter.SelectByXCode(new Guid(cboHalterType.SelectedValue.ToString()));
                }

                if (ePole.Shape == 0)
                {
                    ////ed.WriteMessage("I AM IN Shape=0\n");
                    shape = 0;

                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Clear();
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Clear();
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance.Clear();
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode.Clear();
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess.Clear();


                    Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo.HalterCount = Convert.ToInt32(nudHalter.Value);
                    //Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo.HalterType = Convert.ToInt32(cboHalterType.SelectedValue);
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo.Factor = Convert.ToByte(txtCountPole.Text);
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.UseAccess = useAccess;

                    Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.Existance = status.ACode;
                    if (cboProjCode.Items.Count == 0)
                        Atend.Base.Acad.AcadGlobal.CirclePoleData.ProjectCode = 0;
                    else
                        Atend.Base.Acad.AcadGlobal.CirclePoleData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                    Atend.Base.Acad.AcadGlobal.CirclePoleData.ePole = ePole;
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.Height = Convert.ToDouble(txtHeightFromGround.Text);

                    Atend.Base.Base.BEquipStatus statusHalter = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboMIsExist.SelectedValue));
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterExistance = statusHalter.ACode;
                    if (cboMProjectCode.Items.Count == 0)
                        Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterProjectCode = 0;
                    else
                        Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterProjectCode = Convert.ToInt32(cboMProjectCode.SelectedValue);

                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalterCount = Convert.ToInt32(nudHalter.Value);
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalter = Halt;

                    //for (int i = 0; i < gvPoleConsol.Rows.Count; i++) #
                    //for(int i=0;i<dtgvPoleConsol.Rows.Count;i++)
                    foreach (DataRow dr in dtgvPoleConsol.Rows)
                    {
                        //if (Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value) != 0) #
                        if (Convert.ToInt32(dr["Count"]) != 0)
                        {
                            Atend.Base.Base.BEquipStatus statusConsol = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                            Existance = statusConsol.ACode;

                            if (dr["ProjectCode"].ToString() == "")
                                projectcode = 0;
                            else
                                projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                            //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvPoleConsol.Rows[i].Cells[7];#
                            //if (Convert.ToBoolean(gvPoleConsol.Rows[i].Cells[4].Value) == true)#
                            if (Convert.ToBoolean(dr["IsSql"]) == true)
                            {
                                //eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                //    gvPoleConsol.Rows[i].Cells[0].Value.ToString()));#
                                eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                    dr["XCode"].ToString()));
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Add(eConsol);
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess.Add(false);
                                //Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Add(Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value));#
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance.Add(Existance);

                                //************* PROJECT CODE HERE*******************//
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode.Add(projectcode);
                                //Atend.Base.Acad.AcadGlobal.CirclePoleData.ePole = ePole;
                            }
                            else
                            {
                                //eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                                //   gvPoleConsol.Rows[i].Cells[1].Value.ToString()));#
                                eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                                   dr["Code"].ToString()));
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Add(eConsol);
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess.Add(true);
                                //Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Add(Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value));#
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"]));
                                ////ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance.Add(Existance);


                                //************* PROJECT CODE HERE*******************//
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode.Add(projectcode);
                                //Atend.Base.Acad.AcadGlobal.CirclePoleData.ePole = ePole;

                            }
                        }
                    }
                    ////ed.WriteMessage("Consol.Count={0}\n", Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Count);

                }
                else
                {
                    shape = 1;
                    if (ePole.Type == 2)
                    {
                        //ed.WriteMessage("\n333333\n");

                        Type = 2;
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode.Clear();

                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo.HalterCount = Convert.ToInt32(nudHalter.Value);
                        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo.HalterType = Convert.ToInt32(cboHalterType.SelectedValue);
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo.Factor = Convert.ToByte(txtCountPole.Text);

                        Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.Existance = status.ACode;
                        if (cboProjCode.Items.Count == 0)
                            Atend.Base.Acad.AcadGlobal.PolygonPoleData.ProjectCode = 0;
                        else
                            Atend.Base.Acad.AcadGlobal.PolygonPoleData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.UseAccess = useAccess;
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePole = ePole;
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.Height = Convert.ToDouble(txtHeightFromGround.Text);

                        Atend.Base.Base.BEquipStatus statusHalter = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboMIsExist.SelectedValue));
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterExistance = statusHalter.ACode;
                        if (cboMProjectCode.Items.Count == 0)
                            Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterProjectCode = 0;
                        else
                            Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterProjectCode = Convert.ToInt32(cboMProjectCode.SelectedValue);

                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalterCount = Convert.ToInt32(nudHalter.Value);
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalter = Halt;
                        //ed.WriteMessage("\nHalter XCode ={0}\n", Halt.XCode.ToString());


                        //for (int i = 0; i < gvPoleConsol.Rows.Count; i++)#
                        //for (int i = 0; i < dtgvPoleConsol.Rows.Count; i++)
                        foreach (DataRow dr in dtgvPoleConsol.Rows)
                        {
                            //ed.WriteMessage("Circle Has Consol\n");
                            if (Convert.ToInt32(dr["Count"]) != 0)
                            {
                                Atend.Base.Base.BEquipStatus statusConsol = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                                Existance = statusConsol.ACode;

                                if (dr["ProjectCode"].ToString() == "")
                                    projectcode = 0;
                                else
                                    projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                                //if (Convert.ToBoolean(gvPoleConsol.Rows[i].Cells[4].Value) == true)#
                                if (Convert.ToBoolean(dr["IsSql"]) == true)
                                {
                                    //eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                    //    gvPoleConsol.Rows[i].Cells[0].Value.ToString()));#
                                    eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                        dr["XCode"].ToString()));
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Add(eConsol);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess.Add(false);
                                    //Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount.Add(Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value));#
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"]));
                                    ////ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance.Add(Existance);



                                    //************* PROJECT CODE HERE*******************//
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode.Add(projectcode);
                                    //Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePole = ePole;
                                }
                                else
                                {
                                    //eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                                    //   gvPoleConsol.Rows[i].Cells[1].Value.ToString()));#
                                    eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                                       dr["Code"]));
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Add(eConsol);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess.Add(true);
                                    //Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount.Add(Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value));#
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"]));
                                    ////ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance.Add(Existance);


                                    //************* PROJECT CODE HERE*******************//
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode.Add(projectcode);
                                    //Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePole = ePole;
                                }
                            }
                        }
                        ////ed.WriteMessage("Consol.Count={0}\n", Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Count);

                    }
                    else
                        if (ePole.Type == 3)
                        {
                            //ed.WriteMessage("\n4444444\n");

                            Type = 3;
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Clear();


                            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterCount = Convert.ToInt32(nudHalter.Value);
                            //Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterType = Convert.ToInt32(cboHalterType.SelectedValue);
                            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.Factor = Convert.ToByte(txtCountPole.Text);
                            Atend.Base.Acad.AcadGlobal.PoleData.UseAccess = useAccess;

                            Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                            Atend.Base.Acad.AcadGlobal.PoleData.Existance = status.ACode;
                            if (cboProjCode.Items.Count == 0)
                                Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode = 0;
                            else
                                Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                            Atend.Base.Acad.AcadGlobal.PoleData.ePole = ePole;
                            Atend.Base.Acad.AcadGlobal.PoleData.Height = Convert.ToDouble(txtHeightFromGround.Text);

                            Atend.Base.Base.BEquipStatus statusHalter = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboMIsExist.SelectedValue));
                            Atend.Base.Acad.AcadGlobal.PoleData.HalterExistance = statusHalter.ACode;
                            if (cboMProjectCode.Items.Count == 0)
                                Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode = 0;
                            else
                                Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode = Convert.ToInt32(cboMProjectCode.SelectedValue);

                            Atend.Base.Acad.AcadGlobal.PoleData.eHalterCount = Convert.ToInt32(nudHalter.Value);
                            Atend.Base.Acad.AcadGlobal.PoleData.eHalter = Halt;

                            //ed.WriteMessage("\nHalter XCode ={0}\n", Halt.XCode.ToString());

                            //for (int i = 0; i < gvPoleConsol.Rows.Count; i++)#
                            //for (int i = 0; i < dtgvPoleConsol.Columns.Count; i++)
                            foreach (DataRow dr in dtgvPoleConsol.Rows)
                            {
                                //if (Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value) != 0)#
                                if (Convert.ToInt32(dr["Count"]) != 0)
                                {
                                    Atend.Base.Base.BEquipStatus statusConsol = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                                    Existance = statusConsol.ACode;

                                    if (dr["ProjectCode"].ToString() == "")
                                        projectcode = 0;
                                    else
                                        projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());
                                    //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvPoleConsol.Rows[i].Cells[7];#

                                    //if (Convert.ToBoolean(gvPoleConsol.Rows[i].Cells[4].Value) == true)#
                                    if (Convert.ToBoolean(dr["IsSql"]) == true)
                                    {
                                        //eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                        //    gvPoleConsol.Rows[i].Cells[0].Value.ToString()));#
                                        eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                            dr["XCode"].ToString()));
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Add(eConsol);
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Add(false);
                                        //Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value));#
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"]));
                                        //ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Add(Existance);


                                        //************* PROJECT CODE HERE*******************//
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Add(projectcode);
                                        //Atend.Base.Acad.AcadGlobal.PoleData.ePole = ePole;

                                    }
                                    else
                                    {
                                        //eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                                        //   gvPoleConsol.Rows[i].Cells[1].Value.ToString()));#
                                        eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                                           dr["Code"]));
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Add(eConsol);
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Add(true);
                                        //Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value));#
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"]));
                                        //ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Add(Existance);


                                        //************* PROJECT CODE HERE*******************//
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Add(projectcode);
                                        //Atend.Base.Acad.AcadGlobal.PoleData.ePole = ePole;

                                    }
                                }
                            }
                            //ed.WriteMessage("Consol.Count={0}\n", Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Count);
                        }
                        else
                        {
                            //ed.WriteMessage("\n88888\n");

                            Type = 3;
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Clear();
                            Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Clear();


                            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterCount = Convert.ToInt32(nudHalter.Value);
                            //Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterType = Convert.ToInt32(cboHalterType.SelectedValue);
                            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.Factor = Convert.ToByte(txtCountPole.Text);

                            Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                            Atend.Base.Acad.AcadGlobal.PoleData.Existance = status.ACode;
                            if (cboProjCode.Items.Count == 0)
                                Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode = 0;
                            else
                                Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);
                            //Atend.Base.Acad.AcadGlobal.PoleData.Existance = Convert.ToInt32(cboIsExist.SelectedValue);
                            //Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);
                            Atend.Base.Acad.AcadGlobal.PoleData.UseAccess = useAccess;
                            Atend.Base.Acad.AcadGlobal.PoleData.ePole = ePole;
                            Atend.Base.Acad.AcadGlobal.PoleData.Height = Convert.ToDouble(txtHeightFromGround.Text);

                            Atend.Base.Base.BEquipStatus statusHalter = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboMIsExist.SelectedValue));
                            Atend.Base.Acad.AcadGlobal.PoleData.HalterExistance = statusHalter.ACode;
                            if (cboMProjectCode.Items.Count == 0)
                                Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode = 0;
                            else
                                Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode = Convert.ToInt32(cboMProjectCode.SelectedValue);
                            //Atend.Base.Acad.AcadGlobal.PoleData.HalterExistance = Convert.ToInt32(cboMIsExist.SelectedValue);
                            //Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode = Convert.ToInt32(cboMProjectCode.SelectedValue);
                            Atend.Base.Acad.AcadGlobal.PoleData.eHalterCount = Convert.ToInt32(nudHalter.Value);
                            Atend.Base.Acad.AcadGlobal.PoleData.eHalter = Halt;
                            //ed.WriteMessage("\nHalter XCode ={0}\n", Halt.XCode.ToString());

                            //for (int i = 0; i < gvPoleConsol.Rows.Count; i++)#
                            //for (int i = 0; i < dtgvPoleConsol.Rows.Count; i++)
                            foreach (DataRow dr in dtgvPoleConsol.Rows)
                            {
                                //if (Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value) != 0)#
                                if (Convert.ToInt32(dr["Count"]) != 0)
                                {
                                    Atend.Base.Base.BEquipStatus statusConsol = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                                    Existance = statusConsol.ACode;

                                    if (dr["ProjectCode"].ToString() == "")
                                        projectcode = 0;
                                    else
                                        projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());
                                    //projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                                    //if (Convert.ToBoolean(gvPoleConsol.Rows[i].Cells[4].Value) == true)#
                                    if (Convert.ToBoolean(dr["IsSql"]) == true)
                                    {
                                        //eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                        //    gvPoleConsol.Rows[i].Cells[0].Value.ToString()));#
                                        eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                            dr["XCode"].ToString()));
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Add(eConsol);
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Add(false);
                                        //Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value));#
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"]));
                                        //ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Add(Existance);

                                        //************* PROJECT CODE HERE*******************//
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Add(projectcode);

                                    }
                                    else
                                    {
                                        //eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                                        //   gvPoleConsol.Rows[i].Cells[1].Value.ToString()));#
                                        eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                                           dr["Code"]));
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Add(eConsol);
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Add(true);
                                        //Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(gvPoleConsol.Rows[i].Cells[5].Value));#
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"]));
                                        //ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Add(Existance);

                                        //************* PROJECT CODE HERE*******************//
                                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Add(projectcode);

                                    }
                                }
                            }
                            //ed.WriteMessage("Consol.Count={0}\n", Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Count);
                        }

                }
                //ed.WriteMessage("\nFINISH55555\n");

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                if (cboProjCode.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;

                AllowClose = true;

            }//if (Validation())
            else
            {
                //ed.WriteMessage("Don't Go to _POLE command.\n");
                AllowClose = false;
            }

            //ed.WriteMessage("!!!" + cboIsExist.SelectedValue.ToString() + "\n");
            //ed.WriteMessage("!!!" + cboProjCode.SelectedValue.ToString() + "\n");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void frmDrawPole01_FormClosing(object sender, FormClosingEventArgs e)
        {


            if (!AllowClose)
            {
                e.Cancel = true;
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvPoleConsol.SelectedRows.Count > 0)
            {
                gvPoleConsol.Rows.RemoveAt(gvPoleConsol.SelectedRows[0].Index);
            }

        }

        public void ChangeColor()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            for (int i = 0; i < gvPole.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvPole.Rows[i].Cells["IsSql"].Value) == false)
                {
                    gvPole.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
            for (int i = 0; i < gvConsolsTip.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvConsolsTip.Rows[i].Cells["Sql"].Value) == false)
                {
                    gvConsolsTip.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeColor();
        }

        private void cboProjCode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboProjCode.SelectedValue != null)
            {
                cboMProjectCode.SelectedValue = cboProjCode.SelectedValue;
                if (dtgvPoleConsol.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtgvPoleConsol.Rows)
                    {
                        if (cboProjCode.SelectedValue != null)
                        {
                            dr["ProjectCode"] = Convert.ToInt32(cboProjCode.SelectedValue);
                        }
                        else
                        {
                            dr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"].ToString();
                        }
                    }
                }
            }
        }

        private void cboIsExist_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboIsExist.SelectedValue != null)
            {
                cboMIsExist.SelectedValue = cboIsExist.SelectedValue;
                if (dtgvPoleConsol.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtgvPoleConsol.Rows)
                    {
                        dr["IsExistance"] = Convert.ToInt32(cboIsExist.SelectedValue);
                    }
                }


                DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboMIsExist.SelectedValue.ToString()));
                cboProjCode.DisplayMember = "Name";
                cboProjCode.ValueMember = "ACode";
                cboProjCode.DataSource = dtWorkOrder;

            }
        }

        private void cboMIsExist_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboMIsExist.SelectedValue != null)
            {
                DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboMIsExist.SelectedValue.ToString()));
                cboMProjectCode.DisplayMember = "Name";
                cboMProjectCode.ValueMember = "ACode";
                cboMProjectCode.DataSource = dtWorkOrder;

            }
        }

    }
}