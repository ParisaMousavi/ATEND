using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;
using System.Collections;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmDrawGroundPost03 : Form
    {
        public int transformerCount = 0;
        public int MiddlejackPanelCount = 0;
        public int WeekJackPAnelCount = 0;
        public int GroundPostProductCode = 0;
        public ArrayList arMiddleJAckPAnel = new ArrayList();
        public ArrayList arweekJackPanel = new ArrayList();
        public ArrayList arTransformer = new ArrayList();
        DataTable dtSuEquip = new DataTable();
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        DataTable dtMerge = new DataTable();
        bool AllowToclose = true;
        bool ForceToClose = false;
        System.Data.DataTable dtWorkOrders;


        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.GroundPostData.eGroundPost = new Atend.Base.Equipment.EGroundPost();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance = new ArrayList();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleProjectCode = new ArrayList();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles = new List<Atend.Base.Equipment.EJAckPanel>();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance = new ArrayList();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekProjectCode = new ArrayList();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks = new List<Atend.Base.Equipment.EJackPanelWeek>();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance = new ArrayList();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerProjectCode = new ArrayList();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers = new List<Atend.Base.Equipment.ETransformer>();
            Atend.Base.Acad.AcadGlobal.GroundPostData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.GroundPostData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.GroundPostData.UseAccess = false;
        }

        public frmDrawGroundPost03()
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
            dtWorkOrders = Atend.Base.Base.BWorkOrder.SelectChilds();
        }

        //In Use
        public void ChangeColor()
        {
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[i].Cells[2].Value) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        //In Use
        private bool Validation()
        {
            bool flag = true;
            if (chkCapacity.Checked)
            {
                if (string.IsNullOrEmpty(txtCapacity.Text))
                {
                    MessageBox.Show("لطفا ظرفیت را وارد کنید", "خطا");
                    txtCapacity.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(txtCapacity.Text))
                {
                    MessageBox.Show("لطفا ظرفیت را با فرمت مناسب وارد نمایید", "خطا");
                    txtCapacity.Focus();
                    return false;
                }
            }

            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            if (gvDisconnector.Rows.Count > 0 && gvDisconnector.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;
            }


            //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
            //{
            //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[3];
            //    if (gvSubEquip.Rows[i].Cells[3].Value == null)
            //    {
            //        MessageBox.Show("لطفاً کد صورت وضعیت را انتخاب کنید", "خطا");
            //        return false;
            //    }
            //}

            return true;

        }

        //In Use
        private void btnSearch_Click(object sender, EventArgs e)
        {
            double capacity = -1;
            bool check = false;
            string strFilter = "";

            if (Validation())
            {

                if (chkCapacity.Checked)
                {

                    capacity = Convert.ToDouble(txtCapacity.Text);
                    check = true;
                    strFilter = " Capacity='" + capacity + "'";
                }
            }
            if (check)
            {
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = strFilter;
                gvDisconnector.AutoGenerateColumns = false;
                gvDisconnector.DataSource = dv;
            }
            else
            {
                gvDisconnector.AutoGenerateColumns = false;
                gvDisconnector.DataSource = dtMerge;
            }
            ChangeColor();

        }

        //In Use
        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        //In Use
        private void gvDisconnector_Click(object sender, EventArgs e)
        {
            dtSuEquip.Rows.Clear();

            Atend.Base.Equipment.EGroundPost groundPost;
            DataTable dtExistance = Atend.Base.Base.BEquipStatus.SelectAllX(); //Atend.Control.Common.StatuseCode;
            DataTable dtProjCode = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString())); //Atend.Base.Base.BProjectCode.AccessSelectAll();

            if (gvDisconnector.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == true)
                {
                    //ed.WriteMessage("## SQL\n");
                    groundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                    if (groundPost.Code != -1)
                    {
                        //ed.WriteMessage("Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX.Count : {0}", Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX.Count);
                        for (int i = 0; i < Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX.Count; i++)
                        {
                            int count = Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeCountEPackageX[i].ToString());
                            for (int j = 0; j < count; j++)
                            {
                                DataRow dr = dtSuEquip.NewRow();
                                dr["XCode"] = Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX[i];
                                string name = "";
                                //ed.WriteMessage("~~~~~~~~ TYPE:{0}", Atend.Base.Equipment.EGroundPost.nodeTypeEPackageX[i].ToString());
                                switch ((Atend.Control.Enum.ProductType)(int.Parse(Atend.Base.Equipment.EGroundPost.nodeTypeEPackageX[i].ToString())))
                                {
                                    case Atend.Control.Enum.ProductType.Transformer:
                                        //     {
                                        Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX[i].ToString()));
                                        name = transformer.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                                        dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                        if (cboProjCode.Items.Count > 0)
                                        {
                                            dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                        }
                                        else
                                        {
                                            dr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"];
                                        }
                                        dr["Name"] = name;
                                        dr["IsSql"] = true;
                                        dtSuEquip.Rows.Add(dr);

                                        break;

                                    //       }
                                    case Atend.Control.Enum.ProductType.WeekJackPanel:
                                        //   {
                                        Atend.Base.Equipment.EJackPanelWeek jackPanelweek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX[i].ToString()));
                                        name = jackPanelweek.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                                        dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                        if (cboProjCode.Items.Count > 0)
                                        {
                                            dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                        }
                                        else
                                        {
                                            dr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"];
                                        }
                                        dr["Name"] = name;
                                        dr["IsSql"] = true;
                                        dtSuEquip.Rows.Add(dr);

                                        break;

                                    // }
                                    case Atend.Control.Enum.ProductType.MiddleJackPanel:
                                        //{
                                        Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.SelectByXCode(new Guid(Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX[i].ToString()));
                                        name = jackPanel.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
                                        dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                        if (cboProjCode.Items.Count > 0)
                                        {
                                            dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                        }
                                        else
                                        {
                                            dr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"];
                                        }
                                        dr["Name"] = name;
                                        dr["IsSql"] = true;
                                        dtSuEquip.Rows.Add(dr);

                                        break;

                                    //}
                                }

                                //dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                //if (cboProjCode.Items.Count > 0)
                                //{
                                //    dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                //}
                                //else
                                //{
                                //    dr["ProjectCode"] = 0;
                                //}
                                //dr["Name"] = name;
                                //dr["IsSql"] = true;
                                //dtSuEquip.Rows.Add(dr);
                            }
                        }
                    }

                }
                else
                {
                    //ed.WriteMessage("## Access\n");
                    groundPost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                    for (int i = 0; i < Atend.Base.Equipment.EGroundPost.nodeKeysEPackage.Count; i++)
                    {
                        //ed.WriteMessage("##1\n");
                        int count = Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeCountEPackage[i].ToString());
                        for (int j = 0; j < count; j++)
                        {
                            DataRow dr = dtSuEquip.NewRow();
                            dr["Code"] = Atend.Base.Equipment.EGroundPost.nodeKeysEPackage[i];
                            string name = "";
                            switch ((Atend.Control.Enum.ProductType)(int.Parse(Atend.Base.Equipment.EGroundPost.nodeTypeEPackage[i].ToString())))
                            {
                                case Atend.Control.Enum.ProductType.Transformer:
                                    {
                                        //ed.WriteMessage("##Transformer\n");
                                        Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeKeysEPackage[i].ToString()));
                                        name = transformer.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                                        dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                        if (cboProjCode.Items.Count > 0)
                                        {
                                            dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                        }
                                        else
                                        {
                                            dr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"];
                                        }
                                        dr["Name"] = name;
                                        dr["IsSql"] = false;
                                        dtSuEquip.Rows.Add(dr);
                                        break;
                                    }
                                case Atend.Control.Enum.ProductType.WeekJackPanel:
                                    {
                                        //ed.WriteMessage("##WeekJackPanel\n");
                                        Atend.Base.Equipment.EJackPanelWeek jackPanelweek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeKeysEPackage[i].ToString()));
                                        name = jackPanelweek.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                                        dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                        if (cboProjCode.Items.Count > 0)
                                        {
                                            dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                        }
                                        else
                                        {
                                            dr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"];
                                        }
                                        dr["Name"] = name;
                                        dr["IsSql"] = false;
                                        dtSuEquip.Rows.Add(dr);
                                        break;

                                    }
                                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                                    {
                                        //ed.WriteMessage("##MiddleJackPanel\n");
                                        Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeKeysEPackage[i].ToString()));
                                        name = jackPanel.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
                                        dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                        if (cboProjCode.Items.Count > 0)
                                        {
                                            dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                        }
                                        else
                                        {
                                            dr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"];
                                        }
                                        dr["Name"] = name;
                                        dr["IsSql"] = false;
                                        dtSuEquip.Rows.Add(dr);
                                        break;

                                    }
                            }
                        }
                    }

                }


                txtName.Text = groundPost.Name;
                if (groundPost.GroundType == 0)
                {
                    txtGroundType.Text = "روزمینی";
                }
                else
                {
                    txtGroundType.Text = "زیرزمینی";
                }
                //**
                if (groundPost.Type == 0)
                {
                    txtType.Text = "عمومی";
                }
                if (groundPost.Type == 1)
                {
                    txtType.Text = "اختصاصی";
                }
                if (groundPost.Type == 2)
                {
                    txtType.Text = "پاساژ";
                }
                //**

                if (groundPost.AdvanceType == 0)
                {
                    txtAdvanceType.Text = "معمولی";
                }
                if (groundPost.AdvanceType == 1)
                {
                    txtAdvanceType.Text = "کامپکت";
                }

                if (groundPost.AdvanceType == 2)
                {
                    txtAdvanceType.Text = "کیوسک";
                }


                txtSelectedCapacity.Text = groundPost.Capacity.ToString();


            }


        }

        //In Use
        public void BindDataToIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            DataTable dtstatus = Atend.Base.Base.BEquipStatus.SelectAllX();
            cboIsExist.DataSource = dtstatus;

            if (Atend.Control.Common.IsExist == -1)
            {
                for (int i = 0; i < dtstatus.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtstatus.Rows[i]["IsDefault"].ToString()))
                    {
                        cboIsExist.SelectedValue = dtstatus.Rows[i]["Code"];
                    }
                }
            }
            else
            {
                cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
            }

            //cboIsExist.DisplayMember = "Name";
            //cboIsExist.ValueMember = "Code";
            //cboIsExist.DataSource = Atend.Control.Common.StatuseCode;
            //cboIsExist.SelectedValue = 4;
        }

        //In Use
        private void frmDrawDisconnector_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToclose)
            {
                e.Cancel = true;
                AllowToclose = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowToclose = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
            }
        }

        //Conformed
        private void BindDataToGridMain()
        {
            dtMerge = Atend.Base.Equipment.EGroundPost.SelectAllAndMerge();
            gvDisconnector.AutoGenerateColumns = false;
            gvDisconnector.DataSource = dtMerge;
            ChangeColor();

        }

        private void BindDataToGridSub()
        {
            dtSuEquip.Columns.Add("Code");
            dtSuEquip.Columns.Add("XCode");
            dtSuEquip.Columns.Add("Name");
            dtSuEquip.Columns.Add("IsSql");
            dtSuEquip.Columns.Add("Type");
            DataColumn dcIsExistance = new DataColumn("IsExistance", System.Type.GetType("System.Int32"));
            dtSuEquip.Columns.Add(dcIsExistance);
            DataColumn dcProjectCode = new DataColumn("ProjectCode", System.Type.GetType("System.Int32"));
            dtSuEquip.Columns.Add(dcProjectCode);
            gvSubEquip.AutoGenerateColumns = false;
            gvSubEquip.DataSource = dtSuEquip;
            BindDataToCboInGridView();
            AddProjectCodeColumn();


        }

        //In Use
        private void frmDrawGroundPost02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToGridMain();
            BindDataToGridSub();
            BindDataToIsExist();
            SetDefaultValues();
        }

        //In Use
        public void BindDataToCboInGridView()
        {
            DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX(); //Atend.Control.Common.StatuseCode.Copy();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dt;
            c.DisplayMember = "Name";
            c.ValueMember = "Code";
            c.DataPropertyName = "IsExistance";
            c.HeaderText = "وضعیت تجهیز";
            //c.Width = 50;
            gvSubEquip.Columns.Insert(gvSubEquip.Columns.Count - 1, c);
            gvSubEquip.Columns[gvSubEquip.Columns.Count - 1].FillWeight = 100;
            gvSubEquip.Columns[gvSubEquip.Columns.Count - 1].Width = 100;
            //gvSubEquip.Columns[gvSubEquip.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        //In Use
        public void AddProjectCodeColumn()
        {
            //System.Data.DataTable dt2 = Atend.Base.Base.BWorkOrder.SelectChilds();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dtWorkOrders;
            c.DisplayMember = "Name";
            c.ValueMember = "ACode";
            c.DataPropertyName = "ProjectCode";
            c.HeaderText = "شرح دستورکار";
            //c.Width = 100;
            gvSubEquip.Columns.Insert(gvSubEquip.Columns.Count - 1, c);
            gvSubEquip.Columns[gvSubEquip.Columns.Count - 1].FillWeight = 100;
            gvSubEquip.Columns[gvSubEquip.Columns.Count - 1].Width = 250;
            //gvSubEquip.Columns[gvSubEquip.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        //public void BindDataTocboInGrid()
        //{
        //    DataTable dt = Atend.Control.Common.StatuseCode.Copy();
        //    DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvSubEquip.Columns["Column9"];
        //    c.DataSource = dt;
        //    c.DisplayMember = "Name";
        //    c.ValueMember = "Code";

        //    DataTable dt2 = Atend.Base.Base.BProjectCode.SelectAll();
        //    DataGridViewComboBoxColumn c2 = (DataGridViewComboBoxColumn)gvSubEquip.Columns["Column12"];
        //    c2.DataSource = dt2;
        //    c2.DisplayMember = "Name";
        //    c2.ValueMember = "Code";
        //}
        //private void chkIsExist_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkIsExist.Checked)
        //    {
        //        cboIsExist.Items.Clear();
        //        cboIsExist.Items.Add("برکناری-مستعمل");
        //        cboIsExist.Items.Add("برکناری-اسقات");
        //    }
        //    else if (chkIsExist.CheckState == CheckState.Unchecked)
        //    {
        //        cboIsExist.Items.Clear();
        //        cboIsExist.Items.Add("نصب");
        //    }
        //    cboIsExist.SelectedIndex = 0;
        //    cboIsExist.Refresh();
        //}

        //In Use

        private void btnOk_Click(object sender, EventArgs e)
        {
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance.Clear();

            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleProjectCode.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekProjectCode.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerProjectCode.Clear();

            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers.Clear();


            int ProjectCode;
            int Existance = 0;
            if (Validation())
            {
                dtSuEquip = (DataTable)gvSubEquip.DataSource;
                //ed.WriteMessage("btnOK Click\n");
                //ed.WriteMessage("IsSql={0}\n", gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value);
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == true)
                {
                    //ed.WriteMessage("It Is Local Wquipment\n");
                    Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.GroundPostData.UseAccess = false;
                    Atend.Base.Acad.AcadGlobal.GroundPostData.eGroundPost = groundPost;


                    //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
                    foreach (DataRow dr in dtSuEquip.Rows)
                    {
                        Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                        Existance = status.ACode; //Convert.ToInt32(dr["IsExistance"].ToString());

                        if (dr["ProjectCode"].ToString() == "")
                            ProjectCode = 0;
                        else
                            ProjectCode = Convert.ToInt32(dr["ProjectCode"].ToString());

                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel))
                        {
                            Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.SelectByXCode(new Guid(dr["XCode"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleProjectCode.Add(ProjectCode);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles.Add(jackPanel);
                        }

                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                        {

                            Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(dr["XCode"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekProjectCode.Add(ProjectCode);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks.Add(jackPanelWeek);

                        }


                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                        {

                            Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(dr["XCode"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerProjectCode.Add(ProjectCode);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers.Add(transformer);
                        }

                    }
                }
                else
                {
                    //ed.WriteMessage("It Is Access Wquipment\n");

                    Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.GroundPostData.UseAccess = true;
                    Atend.Base.Acad.AcadGlobal.GroundPostData.eGroundPost = groundPost;

                    //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
                    foreach (DataRow dr in dtSuEquip.Rows)
                    {
                        Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                        Existance = status.ACode; //Convert.ToInt32(dr["IsExistance"].ToString());

                        if (dr["ProjectCode"].ToString() == "")
                            ProjectCode = 0;
                        else
                            ProjectCode = Convert.ToInt32(dr["ProjectCode"].ToString());

                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel))
                        {
                            Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleProjectCode.Add(ProjectCode);

                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles.Add(jackPanel);
                        }

                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                        {

                            Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekProjectCode.Add(ProjectCode);

                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks.Add(jackPanelWeek);

                        }


                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                        {

                            Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerProjectCode.Add(ProjectCode);

                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers.Add(transformer);
                        }

                    }
                }

                Atend.Base.Base.BEquipStatus status02 = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.GroundPostData.Existance = status02.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.GroundPostData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.GroundPostData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                if (cboProjCode.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;
            }
            //ed.WriteMessage("Finish btnOK\n");


        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {

            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks.Clear();
            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers.Clear();


            int projectcode = 0;
            int Existance = 0;
            if (Validation())
            {
                dtSuEquip = (DataTable)gvSubEquip.DataSource;

                //ed.WriteMessage("btnOK Click\n");
                //ed.WriteMessage("IsSql={0}\n", gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value);
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == true)
                {
                    //ed.WriteMessage("It Is Local Wquipment\n");
                    Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.GroundPostData.UseAccess = false;
                    Atend.Base.Acad.AcadGlobal.GroundPostData.eGroundPost = groundPost;
                    //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
                    foreach (DataRow dr in dtSuEquip.Rows)
                    {

                        //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[3];
                        //if (c.Value.ToString() == "موجود-موجود")
                        //{
                        //    Existance = 0;
                        //}
                        //if (c.Value.ToString() == "موجود-مستعمل")
                        //{
                        //    Existance = 1;
                        //}
                        //if (c.Value.ToString() == "موجود-اسقاط")
                        //{
                        //    Existance = 2;
                        //}
                        //if (c.Value.ToString() == "موجود-جابجایی")
                        //{
                        //    Existance = 3;
                        //}
                        //if (c.Value.ToString() == "پیشنهادی-نو")
                        //{
                        //    Existance = 4;
                        //}
                        //if (c.Value.ToString() == "پیشنهادی-جابجایی")
                        //{
                        //    Existance = 5;
                        //}
                        Existance = Convert.ToInt32(dr["IsExistance"].ToString());
                        //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[6];
                        projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                        //ed.WriteMessage("Type={0}\n", gvSubEquip.Rows[i].Cells[5].Value.ToString());
                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel))
                        {
                            //ed.WriteMessage("JavkpanelMiddle.Code={0}\n", dr["XCode"].ToString());
                            Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.SelectByXCode(new Guid(dr["XCode"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles.Add(jackPanel);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleProjectCode.Add(projectcode);
                            //ed.WriteMessage("FJavkpanelMiddle\n");

                        }

                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                        {
                            //ed.WriteMessage("JavkpanelWeek\n");

                            Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(dr["XCode"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks.Add(jackPanelWeek);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekProjectCode.Add(projectcode);
                            //ed.WriteMessage("FJavkpanelMiddle\n");

                        }


                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                        {
                            //ed.WriteMessage("Transformer\n");

                            Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(dr["XCode"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers.Add(transformer);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerProjectCode.Add(projectcode);
                            //ed.WriteMessage("FJavkpanelMiddle\n");

                        }

                    }
                }
                else
                {
                    //ed.WriteMessage("It Is Access Wquipment\n");

                    Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.GroundPostData.UseAccess = true;
                    Atend.Base.Acad.AcadGlobal.GroundPostData.eGroundPost = groundPost;
                    //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
                    foreach (DataRow dr in dtSuEquip.Rows)
                    {

                        //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[3];
                        //if (c.Value.ToString() == "موجود-موجود")
                        //{
                        //    Existance = 0;
                        //}
                        //if (c.Value.ToString() == "موجود-مستعمل")
                        //{
                        //    Existance = 1;
                        //}
                        //if (c.Value.ToString() == "موجود-اسقاط")
                        //{
                        //    Existance = 2;
                        //}
                        //if (c.Value.ToString() == "موجود-جابجایی")
                        //{
                        //    Existance = 3;
                        //}
                        //if (c.Value.ToString() == "پیشنهادی-نو")
                        //{
                        //    Existance = 4;
                        //}
                        //if (c.Value.ToString() == "پیشنهادی-جابجایی")
                        //{
                        //    Existance = 5;
                        //}
                        Existance = Convert.ToInt32(dr["IsExistance"].ToString());
                        //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[6];
                        projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel))
                        {
                            Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles.Add(jackPanel);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleProjectCode.Add(projectcode);
                        }

                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                        {
                            //Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(gvSubEquip.Rows[i].Cells[1].Value.ToString()));
                            Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks.Add(jackPanelWeek);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekProjectCode.Add(projectcode);
                        }


                        if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                        {

                            Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers.Add(transformer);
                            Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerProjectCode.Add(projectcode);
                        }

                    }
                }

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                if (cboProjCode.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;

                AllowToclose = true;
            }
            else
            {
                AllowToclose = false;
            }
            //ed.WriteMessage("Finish btnOK\n");
        }

        //In Use
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            AllowToclose = true;
        }

        private void cboProjCode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dtSuEquip.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSuEquip.Rows)
                {
                    dr["ProjectCode"] = Convert.ToInt32(cboProjCode.SelectedValue);
                }
            }
        }

        private void cboIsExist_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboIsExist.SelectedValue != null)
            {
                if (dtSuEquip.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSuEquip.Rows)
                    {
                        dr["IsExistance"] = Convert.ToInt32(cboIsExist.SelectedValue);
                    }
                }

                DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
                cboProjCode.DisplayMember = "Name";
                cboProjCode.ValueMember = "ACode";
                cboProjCode.DataSource = dtWorkOrder;
       
            }
        }

      
    }
}