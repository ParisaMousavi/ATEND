using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using System.Xml;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Design
{
    public partial class frmEditDrawPole02 : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public Guid NodeCode;//InUse
        public ObjectId ObjID;
        public System.Data.DataTable lastConsol = new System.Data.DataTable();//InUse

        System.Data.DataTable dtMerge = new System.Data.DataTable();//InUse
        System.Data.DataTable dtMergeConsol = new System.Data.DataTable();//InUse
        bool AllowClose = true;
        System.Data.DataTable dtgvPoleConsol = new System.Data.DataTable();//InUse
        ArrayList consolsCodeDEl = new ArrayList();//
        ArrayList del = new ArrayList();
        System.Data.DataTable newConsol = new System.Data.DataTable();
        int SelectedProductCode = -1;
        bool ForceToClose = false;



        public frmEditDrawPole02()
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
        }

        //InUse
        private bool Validation()
        {

            /*if (!(chkCross.Checked || chkEnding.Checked || chkPulling.Checked || chkDP.Checked))
            {
                MessageBox.Show("لطفا نوع کنسول را مشخص نمایید", "خطا");
                tabControl1.TabPages[1].Focus();
                return false;
            }
            */
            //ed.WriteMessage("****************\n");
            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

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

            //if (gvPoleConsol.Rows.Count == 0)
            //{
            //    MessageBox.Show("لطفاً کنسول های پایه را انتخاب کنید", "خطا");
            //    gvPoleConsol.Focus();
            //    return false;
            //}

            if (gvPoleConsol.Rows.Count > 4)
            {
                MessageBox.Show("شما مجاز به انتخاب حداکثر 4 کنسول برای این پایه هستید", "خطا");
                gvPoleConsol.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cboIsExist.Text))
            {
                MessageBox.Show("لطفاً موجود یاپیشنهادی پایه را انتخاب کنید", "خطا");
                return false;
            }

            //for (int i = 0; i < gvPoleConsol.Rows.Count; i++)
            //{
            //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvPoleConsol.Rows[i].Cells[gvPoleConsol.Columns.Count - 2];

            //    if (c.Value == null)
            //    {
            //        MessageBox.Show("لطفاً کد وضعیت تجهیز کنسول را انتخاب کنید", "خطا");
            //        return false;
            //    }
            //}

            //for (int i = 0; i < gvPoleConsol.Rows.Count; i++)
            //{
            //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvPoleConsol.Rows[i].Cells[gvPoleConsol.Columns.Count - 1];

            //    if (c.Value == null)
            //    {
            //        MessageBox.Show("لطفاً کد دستور کار کنسول را انتخاب کنید", "خطا");
            //        return false;
            //    }
            //}

            return true;
        }

        private void Save()
        {
            string strPoleType = "";
            Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode(NodeCode);// SelectByCode(NodeCode);
            Atend.Base.Design.DPoleInfo poleInfo = Atend.Base.Design.DPoleInfo.AccessSelectByNodeCode(myNode.Code);// SelectByNodeCode(myNode.Code);
            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.Code = poleInfo.Code;
            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.NodeCode = myNode.Code;
            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterCount = int.Parse(nudHalter.Value.ToString());
            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterType = int.Parse(cboHalterType.SelectedValue.ToString());
            Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.Factor = Convert.ToByte(txtCountPole.Text);

            Atend.Base.Design.DPackage dpackage = Atend.Base.Design.DPackage.AccessSelectByNodeCodeType(myNode.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));// SelectByNodeCodeType(myNode.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));

            btnInsert.Focus();
            System.Data.DataTable dtConsol = Atend.Base.Design.DConsol.AccessSelectByParentCode(myNode.Code);
            for (int i = 0; i < gvPoleConsol.Rows.Count; i++)
            {
                DataRow[] dr = dtConsol.Select(" Code='" + gvPoleConsol.Rows[i].Cells[4].Value.ToString() + "'");
                if (dr.Length == 0)
                {
                    Atend.Base.Design.DPackage tempDPackage = new Atend.Base.Design.DPackage();

                    DataGridViewCheckBoxCell chkBoxCell = (DataGridViewCheckBoxCell)gvPoleConsol.Rows[i].Cells[3];
                    tempDPackage.IsExistance = Convert.ToByte(chkBoxCell.Value);
                    tempDPackage.Count = 1;
                    tempDPackage.Type = (int)Atend.Control.Enum.ProductType.Consol;
                    tempDPackage.ParentCode = dpackage.Code;
                    tempDPackage.ProductCode = Convert.ToInt32(gvPoleConsol.Rows[i].Cells[0].Value.ToString());

                }
            }

            System.Data.DataTable dtGrid = new System.Data.DataTable();
            System.Data.DataColumn dcCode = new System.Data.DataColumn("Code");
            System.Data.DataColumn dcName = new System.Data.DataColumn("Name");
            System.Data.DataColumn dcConsolType = new System.Data.DataColumn("ConsolType");
            System.Data.DataColumn dcIsExistance = new System.Data.DataColumn("IsExist");
            System.Data.DataColumn dcGuid = new System.Data.DataColumn("Guid");
            dtGrid.Columns.Add(dcCode);
            dtGrid.Columns.Add(dcName);
            dtGrid.Columns.Add(dcConsolType);
            dtGrid.Columns.Add(dcIsExistance);
            dtGrid.Columns.Add(dcGuid);

            for (int i = 0; i < gvPoleConsol.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chkBoxCell = (DataGridViewCheckBoxCell)gvPoleConsol.Rows[i].Cells[3];
                DataRow dr = dtGrid.NewRow();

                dr["Code"] = gvPoleConsol.Rows[i].Cells[0].Value.ToString();
                dr["Name"] = gvPoleConsol.Rows[i].Cells[1].Value.ToString();
                dr["ConsolType"] = gvPoleConsol.Rows[i].Cells[2].Value.ToString();
                dr["IsExist"] = chkBoxCell.Value;
                dr["Guid"] = gvPoleConsol.Rows[i].Cells[4].Value.ToString();
                dtGrid.Rows.Add(dr);
            }
            foreach (DataRow dr in dtConsol.Rows)
            {
                string Code = dr["Code"].ToString();
                DataRow[] dr1 = dtGrid.Select(" Guid='" + Code + "'");

            }
        }

        //InUse
        private void BindDataToHalterType()
        {
            cboHalterType.DataSource = Atend.Base.Equipment.EHalter.SelectAllX();
            cboHalterType.DisplayMember = "Name";
            cboHalterType.ValueMember = "XCode";
        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;

            //System.Data.DataTable dt1 = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboMProjectCode.DisplayMember = "Name";
            //cboMProjectCode.ValueMember = "Code";
            //cboMProjectCode.DataSource = dt1;
        }

        //InUse
        private void btnSearch_Click(object sender, EventArgs e)
        {
            double height = -1;
            double power = -1;
            int type = -1;
            string strFilter = "";
            bool check = false;

            if (chkHeight.Checked)
            {
                height = Convert.ToDouble(nudHeight.Value.ToString());
                if (strFilter != "")
                {
                    strFilter += " AND Height='" + height.ToString() + "'";
                }
                else
                {
                    strFilter = " Height='" + height.ToString() + "'";
                }
                check = true;
            }
            if (chkPower.Checked)
            {
                power = Convert.ToDouble(nudPower.Value.ToString());
                if (strFilter != "")
                {
                    strFilter += " AND Power='" + power.ToString() + "'";
                }
                else
                {
                    strFilter = " Power='" + power.ToString() + "'";
                }
                check = true;
            }
            if (chkType.Checked)
            {
                type = cboType.SelectedIndex;
                if (strFilter != "")
                {
                    strFilter += " AND Type='" + type.ToString() + "'";
                }
                else
                {
                    strFilter = " Type='" + type.ToString() + "'";
                }
                check = true;
            }
            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                //gvPole.AutoGenerateColumns = false;
                //gvPole.DataSource = dv;
                dtMerge.DefaultView.RowFilter = strFilter;

            }
            else
            {
                //gvPole.AutoGenerateColumns = false;
                dtMerge.DefaultView.RowFilter = "";
                //gvPole.DataSource = dtMerge;

            }

            //ChangeColor(3, gvPole);
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,Xcode", new object[2] { SelectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvPole, this);
            ChangeColor(3, gvPole);
            for (int i = 0; i < gvPole.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvPole.Rows[i].Cells[0].Value.ToString()) == SelectedProductCode)
                {
                    gvPole.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }


        }

        //InUse
        private void BindDataToConsolTip()
        {
            DataView dv = new DataView();
            dv.Table = dtMergeConsol;
            dv.RowFilter = " Type='" + cboConsolType.SelectedIndex + "'";
            gvConsolsTip.AutoGenerateColumns = false;
            gvConsolsTip.DataSource = dv;
            ChangeColor(4, gvConsolsTip);
        }

        //InUse
        private void frmDrawPole01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dtMerge = Atend.Base.Equipment.EPole.SelectAllAndMerge();
            Atend.Base.Design.DPackage d1 = Atend.Base.Design.DPackage.AccessSelectByNodeCode(NodeCode);// Consol.AccessSelectByCode(NodeCode);
            SelectedProductCode = d1.ProductCode;

            #region gvPole Setting
            gvPole.AutoGenerateColumns = false;
            gvPole.DataSource = dtMerge;
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,Xcode", new object[2] { SelectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvPole, this);
            ChangeColor(3, gvPole);
            for (int i = 0; i < gvPole.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvPole.Rows[i].Cells[0].Value.ToString()) == d1.ProductCode)
                {
                    gvPole.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            #endregion

            #region comboBoxes Setting
            BindDataToComboBoxIsExist();
            BindDataToHalterType();
            BindDataToConsolTip();
            BindDataToOwnControl();

            if (cboConsolType.Items.Count > 0)
            {
                cboConsolType.SelectedIndex = cboConsolType.Items.Count - 1; //0;
            }

            if (cboHalterType.Items.Count > 0)
            {
                cboHalterType.SelectedIndex = 0; //cboHalterType.Items.Count - 1;
            }


            #endregion


            System.Data.DataTable HalterTbl = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(d1.Code, (int)Atend.Control.Enum.ProductType.Halter);
            if (HalterTbl.Rows.Count > 0)
            {
                Atend.Base.Equipment.EHalter Halt = Atend.Base.Equipment.EHalter.AccessSelectByCode(Convert.ToInt32(HalterTbl.Rows[0]["ProductCode"].ToString()));
                if (Halt.Code != -1)
                {
                    cboHalterType.SelectedValue = Halt.XCode;
                }
                cboMIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(Convert.ToInt32(HalterTbl.Rows[0]["IsExistance"].ToString())).Code;
                cboMProjectCode.SelectedValue = HalterTbl.Rows[0]["ProjectCode"];
            }

            dtgvPoleConsol = Atend.Base.Design.DPackage.AccessSelectByParentCodeForConsol(d1.Code, (int)Atend.Control.Enum.ProductType.Consol);

            System.Data.DataColumn dcc = new System.Data.DataColumn("ConsolTypeMe");
            dtgvPoleConsol.Columns.Add(dcc);
            System.Data.DataColumn dcIsSql = new System.Data.DataColumn("IsSql");
            dtgvPoleConsol.Columns.Add(dcIsSql);
            System.Data.DataColumn dcAccessCode = new System.Data.DataColumn("AccessCode");
            dtgvPoleConsol.Columns.Add(dcAccessCode);
            System.Data.DataColumn dcXCode = new System.Data.DataColumn("XCode");
            dtgvPoleConsol.Columns.Add(dcXCode);

            foreach (DataRow dr in dtgvPoleConsol.Rows)
            {
                dr["IsExistance"] = Atend.Base.Base.BEquipStatus.SelectByACode(Convert.ToInt32(dr["IsExistance"].ToString())).Code;
                if (Convert.ToInt32(dr["ConsolType"].ToString()) == 0)
                    dr["ConsolTypeMe"] = "کششی";
                if (Convert.ToInt32(dr["ConsolType"].ToString()) == 1)
                    dr["ConsolTypeMe"] = "انتهایی";
                if (Convert.ToInt32(dr["ConsolType"].ToString()) == 2)
                    dr["ConsolTypeMe"] = "عبوری";
                if (Convert.ToInt32(dr["ConsolType"].ToString()) == 3)
                    dr["ConsolTypeMe"] = "DP";

                dr["IsSql"] = false;
                dr["AccessCode"] = "_";
                dr["XCode"] = "_";
            }
            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(d1.IsExistance).Code;
            cboProjCode.SelectedValue = d1.ProjectCode;

            #region gvPoleConsol Setting
            BindDataToCboInGridView();
            AddProjectCodeColumn();
            lastConsol = dtgvPoleConsol.Copy();
            gvPoleConsol.AutoGenerateColumns = false;
            gvPoleConsol.DataSource = dtgvPoleConsol;
            #endregion

        }

        //InUse
        public void BindDataToCboInGridView()
        {
            ////try
            ////{
            System.Data.DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX();
            //MessageBox.Show(dt.Rows.Count.ToString());
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DisplayMember = "Name";
            c.ValueMember = "Code";
            c.DataPropertyName = "IsExistance";
            c.HeaderText = "کد صورت وضعیت";
            c.DataSource = dt;
            //gvPoleConsol.Columns.Add(c);
            gvPoleConsol.Columns.Insert(gvPoleConsol.Columns.Count - 1, c);
            //}
            //catch (System.Exception ex)
            //{
            //    ed.WriteMessage("Errorr={0}\n", ex.Message);
            //}
        }

        //InUse
        public void AddProjectCodeColumn()
        {
            System.Data.DataTable dt2 = Atend.Base.Base.BWorkOrder.SelectChilds();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dt2;
            c.DisplayMember = "Name";
            c.ValueMember = "ACode";
            c.DataPropertyName = "ProjectCode";
            c.HeaderText = "کد دستورکار";
            //gvPoleConsol.Columns.Add(c);
            gvPoleConsol.Columns.Insert(gvPoleConsol.Columns.Count - 1, c);
        }

        //private void BindToComboBox(DataGridViewComboBoxCell comboBox)
        //{
        //    comboBox.DisplayMember = "IsExistance";
        //    comboBox.ValueMember = "Code";
        //    comboBox.DataSource = Atend.Control.Common.StatuseCode.Copy();

        //    comboBox.DisplayMember = "ProjectCode";
        //    comboBox.ValueMember = "Code";
        //    comboBox.DataSource = Atend.Base.Base.BProjectCode.AccessSelectAll();


        //}*

        //InUser
        public void ChangeColor(int Index, DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgv.Rows[i].Cells[Index].Value) == false)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        //InUse
        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();

            cboMIsExist.DisplayMember = "Name";
            cboMIsExist.ValueMember = "Code";
            cboMIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        //InUse
        private void gvPole_Click(object sender, EventArgs e)
        {
            Atend.Base.Equipment.EPole pole;
            if (gvPole.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvPole.Rows[gvPole.CurrentRow.Index].Cells[3].Value))
                {
                    pole = Atend.Base.Equipment.EPole.SelectByXCode(new Guid(gvPole.Rows[gvPole.CurrentRow.Index].Cells[1].Value.ToString()));
                }
                else
                {
                    pole = Atend.Base.Equipment.EPole.AccessSelectByCode(Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                txtBottomCrossSectionArea.Text = Math.Round(pole.ButtomCrossSectionArea, 4).ToString();
                txtHeight.Text = Math.Round(pole.Height, 4).ToString();
                txtPower.Text = Math.Round(pole.Power, 4).ToString();
                txtTopCrossSectionArea.Text = Math.Round(pole.TopCrossSectionArea, 4).ToString();
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
                // //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                // //**EDIT**
                // //Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.SelectByCode(Convert.ToInt32(
                // //    gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value));
                // Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.SelectByXCode(new Guid(
                //     gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value.ToString()));
                // //EXtra
                //// Atend.Base.Equipment.EPoleType poleType = Atend.Base.Equipment.EPoleType.SelectByCode(pole.Type);
                // //Atend.Base.Acad.AcadGlobal.dPoleInfo.Factor = Convert.ToByte(txtCountPole.Text);

                // txtBottomCrossSectionArea.Text = pole.ButtomCrossSectionArea.ToString();

                // txtHeight.Text = pole.Height.ToString();
                // txtPower.Text = pole.Power.ToString();
                // txtTopCrossSectionArea.Text = pole.TopCrossSectionArea.ToString();
                // //Extra
                // //txtType.Text = poleType.Name;

                // //DataTable dtblock = Atend.Base.Base.BProductBlock.SelectByProductIdType(
                // //    Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value),
                // //    (int)Atend.Control.Enum.ProductType.Pole);
                // //SelectProductBlock();
            }
        }

        //InUse
        private void button1_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = (System.Data.DataTable)gvConsolsTip.DataSource;
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
                System.Data.DataTable ProductPackageTable;
                int Type = (int)Atend.Control.Enum.ProductType.Consol;
                bool IsSql = Convert.ToBoolean(gvConsolsTip.SelectedRows[0].Cells[4].Value);
                //ed.WriteMessage("IsSql={0}\n", IsSql);

                if (IsSql == true)
                {
                    ContainerXCode = new Guid(gvConsolsTip.SelectedRows[0].Cells[0].Value.ToString());
                    ed.WriteMessage("ContainerXCode={0}\n", ContainerXCode);

                    ProductPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerXCodeType(ContainerXCode, Type);
                }
                else
                {
                    ContainerCode = Convert.ToInt32(gvConsolsTip.SelectedRows[0].Cells[1].Value.ToString());
                    ProductPackageTable = Atend.Base.Equipment.EProductPackage.AccessSelectByContainerCodeAndType(ContainerCode, Type);

                }

                ed.WriteMessage("ProductPackage.Rows.Count={0}\n", ProductPackageTable.Rows.Count);

                foreach (DataRow row in ProductPackageTable.Rows)
                {

                    #region find each row TableType
                    byte TableType = Convert.ToByte(row["TableType"]);
                    #endregion
                    ed.WriteMessage(string.Format("TableType : {0} \n", TableType));

                    #region search in XML for Table of TableType value
                    string Table = DetermineTableValue(TableType);
                    #endregion

                    ed.WriteMessage(string.Format("Table : {0} \n", Table));
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

                                    break;
                                case Atend.Control.Enum.ProductType.InsulatorPipe:
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

                            #region Switch Local
                            switch ((Atend.Control.Enum.ProductType)TableType)
                            {


                                case Atend.Control.Enum.ProductType.Insulator:

                                    ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"])); //SelectByCode(Convert.ToInt32(row["ProductCode"]));       
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

                                    break;
                                case Atend.Control.Enum.ProductType.InsulatorPipe:
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
                }
            }

        }

        private void btnNewConsolTip_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmConsol02 FrmConsol = new Atend.Equipment.frmConsol02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmConsol);
            BindDataToConsolTip();
        }

        //InUse
        private void btnInsert_Click(object sender, EventArgs e)
        {
            //System.Data.DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX(); //Atend.Control.Common.StatuseCode.Copy();
            //System.Data.DataTable dt2 = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString())); //Atend.Base.Base.BProjectCode.AccessSelectAll();
            if (gvConsolsTip.SelectedRows.Count > 0)
            {
                DataRow drr = dtgvPoleConsol.NewRow();
                if (Convert.ToBoolean(gvConsolsTip.SelectedRows[0].Cells[4].Value) == false)//Access
                {
                    drr["XCode"] = "_";
                    drr["AccessCode"] = gvConsolsTip.SelectedRows[0].Cells[1].Value;
                    drr["Code"] = Guid.Empty;
                    drr["Name"] = gvConsolsTip.SelectedRows[0].Cells[2].Value;
                    if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "0")
                    {
                        drr["ConsolTypeMe"] = "کششی";
                    }
                    if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "1")
                    {
                        drr["ConsolTypeMe"] = "انتهایی";
                    }
                    if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "2")
                    {
                        drr["ConsolTypeMe"] = "عبوری";
                    }
                    if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "3")
                    {
                        drr["ConsolTypeMe"] = "DP";
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
                        System.Data.DataTable dttest = Atend.Base.Base.BWorkOrder.SelectChilds();
                        drr["ProjectCode"] =dttest.Rows[0]["ACode"].ToString();
                        //drr["ProjectCode"]=0;
                    }
                    //drr["IsExistance"] = dt.Rows[4]["Code"].ToString();
                    //drr["ProjectCode"] = dt2.Rows[0]["Code"].ToString();
                    dtgvPoleConsol.Rows.Add(drr);
                }
                else//Local
                {
                    drr["XCode"] = gvConsolsTip.SelectedRows[0].Cells[0].Value;
                    drr["Code"] = Guid.Empty;
                    drr["AccessCode"] = "_";
                    drr["Name"] = gvConsolsTip.SelectedRows[0].Cells[2].Value;
                    if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "0")
                    {
                        drr["ConsolTypeMe"] = "کششی";
                    }
                    if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "1")
                    {
                        drr["ConsolTypeMe"] = "انتهایی";
                    }
                    if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "2")
                    {
                        drr["ConsolTypeMe"] = "عبوری";
                    }
                    if (gvConsolsTip.SelectedRows[0].Cells[3].Value.ToString() == "3")
                    {
                        drr["ConsolTypeMe"] = "DP";
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
                        System.Data.DataTable dttest = Atend.Base.Base.BWorkOrder.SelectChilds();
                        drr["ProjectCode"] = dttest.Rows[0]["ACode"].ToString();
                        //drr["ProjectCode"] = 0;
                    }
                    //drr["IsExistance"] = dt.Rows[4]["Code"].ToString();
                    //drr["ProjectCode"] = dt2.Rows[0]["Code"].ToString();
                    dtgvPoleConsol.Rows.Add(drr);
                }
            }



        }

        //InUse
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

        //InUse
        private void cboConsolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gvConsolsTip.AutoGenerateColumns = false;
            //gvConsolsTip.DataSource = Atend.Base.Equipment.EConsol.SelectByType(cboConsolType.SelectedIndex);
            BindDataToConsolTip();
        }

        //InUse
        private void btnOk_Click(object sender, EventArgs e)
        {
            btnCancel.Focus();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawPole DrawPole = new Atend.Global.Acad.DrawEquips.AcDrawPole();
            if (Validation())
            {
                consolsCodeDEl.Clear();
                dtgvPoleConsol = (System.Data.DataTable)gvPoleConsol.DataSource;

                if (Convert.ToBoolean(gvPole.Rows[gvPole.CurrentRow.Index].Cells[3].Value) == false)
                {
                    DrawPole.UseAccess = true;
                    DrawPole.ePole = Atend.Base.Equipment.EPole.AccessSelectByCode(Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvPole.Rows[gvPole.CurrentRow.Index].Cells[3].Value))
                {
                    DrawPole.UseAccess = false;
                    DrawPole.ePole = Atend.Base.Equipment.EPole.SelectByXCode(new Guid(gvPole.Rows[gvPole.CurrentRow.Index].Cells[1].Value.ToString()));

                }

                foreach (DataRow dr in lastConsol.Rows)
                {
                    DataRow[] drConsol = dtgvPoleConsol.Select(string.Format("Code='{0}'", dr["Code"].ToString()));
                    if (drConsol.Length == 0)
                    {
                        consolsCodeDEl.Add(dr["Code"].ToString());
                    }
                }

                List<Atend.Base.Equipment.EConsol> list = new List<Atend.Base.Equipment.EConsol>();
                ArrayList consoluse = new ArrayList();
                ArrayList consolexis = new ArrayList();
                ArrayList consolcount = new ArrayList();
                ArrayList ConsolProjCode = new ArrayList();

                //مجموع کنسول های جدید اضاف شده
                DataRow[] drConsol1 = dtgvPoleConsol.Select(string.Format("Code='{0}'", Guid.Empty));
                foreach (DataRow drAddConsol in drConsol1)
                {
                    if (Convert.ToBoolean(drAddConsol["IsSql"]) == true)
                    {
                        list.Add(Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(drAddConsol["XCode"].ToString())));
                        Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(drAddConsol["IsExistance"].ToString()));
                        consolexis.Add(status.ACode);
                        //consolexis.Add(Convert.ToByte(drAddConsol["IsExistance"]));
                        consoluse.Add(false);
                        consolcount.Add(Convert.ToInt32(drAddConsol["Count"]));

                        if (drAddConsol["ProjectCode"].ToString() == "")
                            ConsolProjCode.Add(0);
                        else
                            ConsolProjCode.Add(Convert.ToInt32(drAddConsol["ProjectCode"]));
                    }
                    else
                    {
                        list.Add(Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(drAddConsol["AccessCode"].ToString())));
                        Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(drAddConsol["IsExistance"].ToString()));
                        consolexis.Add(status.ACode);
                        //consolexis.Add(Convert.ToByte(drAddConsol["IsExistance"]));
                        consoluse.Add(true);
                        consolcount.Add(Convert.ToInt32(drAddConsol["Count"]));

                        if (drAddConsol["ProjectCode"].ToString() == "")
                            ConsolProjCode.Add(0);
                        else
                            ConsolProjCode.Add(Convert.ToInt32(drAddConsol["ProjectCode"]));
                    }
                }

                //مجموع کنسول های موجود از قبل
                DataRow[] drBeforConsol = dtgvPoleConsol.Select(string.Format("Code <>'{0}'", Guid.Empty));
                foreach (DataRow drBefor in drBeforConsol)
                {
                    Atend.Base.Design.DPackage dPackBefor = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(drBefor["Code"].ToString()));
                    Atend.Base.Base.BEquipStatus status2 = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(drBefor["IsExistance"].ToString()));
                    dPackBefor.IsExistance = status2.ACode; //Convert.ToInt32(drBefor["IsExistance"].ToString());
                    if (drBefor["ProjectCode"].ToString() == "")
                        dPackBefor.ProjectCode = 0;
                    else
                        dPackBefor.ProjectCode = Convert.ToInt32(drBefor["ProjectCode"].ToString());
                    dPackBefor.Count = Convert.ToInt32(drBefor["Count"].ToString());
                    DrawPole.BeforDPackage.Add(dPackBefor);
                }

                Atend.Base.Equipment.EHalter Halt = new Atend.Base.Equipment.EHalter();
                if (!string.IsNullOrEmpty(cboHalterType.Text))
                {
                    Halt = Atend.Base.Equipment.EHalter.SelectByXCode(new Guid(cboHalterType.SelectedValue.ToString()));
                }

                Atend.Base.Design.DPoleInfo dPoleInfo = Atend.Base.Design.DPoleInfo.AccessSelectByNodeCode(NodeCode);
                dPoleInfo.HalterCount = Convert.ToInt32(nudHalter.Value.ToString());
                dPoleInfo.HalterType = Halt.Code;
                DrawPole.dPoleInfo = dPoleInfo;

                Atend.Base.Base.BEquipStatus statusHalter = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboMIsExist.SelectedValue));
                DrawPole.HalterExist = statusHalter.ACode;
                if (cboMProjectCode.Items.Count == 0)
                    DrawPole.HalterProjectCode = 0;
                else
                    DrawPole.HalterProjectCode = Convert.ToInt32(cboMProjectCode.SelectedValue);
                DrawPole.eHalter = Halt;
                DrawPole.eHalterCount = Convert.ToInt32(nudHalter.Value);

                DrawPole.eConsols = list;
                DrawPole.eConsolExistance = consolexis;
                DrawPole.eConsolProjectCode = ConsolProjCode;
                DrawPole.eConsolUseAccess = consoluse;
                DrawPole.eConsolCount = consolcount;

                Atend.Base.Base.BEquipStatus statusPole = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawPole.Existance = statusPole.ACode;
                if (cboProjCode.Items.Count == 0)
                    DrawPole.ProjectCode = 0;
                else
                    DrawPole.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawPole.NodeCode = NodeCode;
                DrawPole.Height = Convert.ToDouble(txtHeightFromGround.Text);

                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByNodeCode(NodeCode);
                DrawPole.EXCode = dpack.Code;
                Atend.Base.Design.DPackage dp = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(gvPole.Rows[gvPole.CurrentRow.Index].Cells[1].Value.ToString()));
                if (DrawPole.UpdatePoleData(ObjID, consolsCodeDEl, dp.Code))
                {
                    AllowClose = true;
                }
            }

        }

        //InUse
        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
        }

        //InUse
        private void frmDrawPole01_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        //InUse
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvPoleConsol.SelectedRows.Count > 0)
            {
                //consolsCodeDEl.Add(gvPoleConsol.Rows[gvPoleConsol.CurrentRow.Index].Cells[0].Value.ToString());
                //del.Add(gvPoleConsol.Rows[gvPoleConsol.CurrentRow.Index].Cells[0].Value.ToString());
                //int i = 0;
                //foreach (DataRow dr in dtgvPoleConsol.Rows)
                //{
                //    if (new Guid(dr["XCode"].ToString()) == new Guid(gvPoleConsol.Rows[gvPoleConsol.CurrentRow.Index].Cells[0].Value.ToString()))
                //    {
                //        //dtgvPoleConsol.Rows.RemoveAt(i);
                //        //gvPoleConsol.Refresh();
                //    }
                //    i++;
                //}
                dtgvPoleConsol.Rows.RemoveAt(gvPoleConsol.SelectedRows[0].Index);
                gvPoleConsol.Refresh();
            }
        }

        //InUse
        public void BindDataToOwnControl()
        {

            Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode(NodeCode);
            Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCodeType(myNode.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
            Atend.Base.Equipment.EPole myPole = Atend.Base.Equipment.EPole.AccessSelectByCode(myNode.ProductCode);
            gvPole.AutoGenerateColumns = false;

            txtTopCrossSectionArea.Text = Convert.ToString(Math.Round(myPole.TopCrossSectionArea, 4));
            txtPower.Text = Convert.ToString(Math.Round(myPole.Power, 2));
            txtHeight.Text = Convert.ToString(Math.Round(myPole.Height, 2));
            txtBottomCrossSectionArea.Text = Convert.ToString(Math.Round(myPole.ButtomCrossSectionArea, 2));
            txtHeightFromGround.Text = Convert.ToString(Math.Round(myNode.Height, 2));

            if (myPole.Type == 0)
            {
                txtType.Text = "بتونی";
            }
            if (myPole.Type == 1)
            {
                txtType.Text = "فلزی";
            }
            if (myPole.Type == 2)
            {
                txtType.Text = "تلسکوپی";
            }
            if (myPole.Type == 3)
            {
                txtType.Text = "چوبی";
            }


            Atend.Base.Design.DPoleInfo myPoleInfo = Atend.Base.Design.DPoleInfo.AccessSelectByNodeCode(myNode.Code);
            nudHalter.Value = myPoleInfo.HalterCount;
            if (myPoleInfo.HalterCount != 0)
                cboHalterType.SelectedValue = myPoleInfo.HalterType;
            else
                cboHalterType.SelectedIndex = 0;

        }

        //InUse
        private void cboIsExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
            cboProjCode.DisplayMember = "Name";
            cboProjCode.ValueMember = "ACode";
            cboProjCode.DataSource = dtWorkOrder;
        }

        //InUse
        private void cboMIsExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboMIsExist.SelectedValue.ToString()));
            cboMProjectCode.DisplayMember = "Name";
            cboMProjectCode.ValueMember = "ACode";
            cboMProjectCode.DataSource = dtWorkOrder;
        }


    }
}