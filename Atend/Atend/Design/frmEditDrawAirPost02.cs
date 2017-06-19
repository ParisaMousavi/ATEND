using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Design
{
    public partial class frmEditDrawAirPost02 : Form
    {
        public int transformerCount = 0;
        public int WeekJackPAnelCount = 0;

        public ArrayList arweekJackPanel = new ArrayList();
        public ArrayList arTransformer = new ArrayList();

        System.Data.DataTable dtMerge = new System.Data.DataTable();
        System.Data.DataTable dtSuEquip = new System.Data.DataTable();
        System.Data.DataTable dtCurrent = new System.Data.DataTable();
        
        public ObjectId ObjID;
        public Guid NodeCode;
        int selecetdProductCode = -1;

        bool AllowToclose = true;
        bool ForceToClose = false;

        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        public frmEditDrawAirPost02()
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
        }

        public void BindDataTocboInGrid()
        {
            //System.Data.DataTable dt = Atend.Control.Common.StatuseCode.Copy();
            //DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvSubEquip.Columns["Column9"];
            //c.DataSource = dt;
            //c.DisplayMember = "Name";
            //c.ValueMember = "Code";

            //System.Data.DataTable dt2 = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //DataGridViewComboBoxColumn c2 = (DataGridViewComboBoxColumn)gvSubEquip.Columns["Column5"];
            //c2.DataSource = dt2;
            //c2.DisplayMember = "Name";
            //c2.ValueMember = "Code";
        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }


        public void ChangeColor()
        {
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[i].Cells[4].Value) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        private bool Validation()
        {
            //bool flag = true;
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
            return true;
        }

        private void frmEditDrawAirPost02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToCboInGridView();
            AddProjectCodeColumn();

            System.Data.DataColumn dcCode = new System.Data.DataColumn("Code");
            dtSuEquip.Columns.Add(dcCode);
            System.Data.DataColumn dcXCode = new System.Data.DataColumn("XCode");
            dtSuEquip.Columns.Add(dcXCode);
            System.Data.DataColumn dcName = new System.Data.DataColumn("Name");
            dtSuEquip.Columns.Add(dcName);
            System.Data.DataColumn dcIsSql = new System.Data.DataColumn("IsSql");
            dtSuEquip.Columns.Add(dcIsSql);
            System.Data.DataColumn dcCount = new System.Data.DataColumn("Type", System.Type.GetType("System.Int32"));
            dtSuEquip.Columns.Add(dcCount);
            System.Data.DataColumn dcIsExistance = new System.Data.DataColumn("IsExistance", System.Type.GetType("System.Int32"));
            dtSuEquip.Columns.Add(dcIsExistance);
            System.Data.DataColumn dcProjectCode = new System.Data.DataColumn("ProjectCode", System.Type.GetType("System.Int32"));
            dtSuEquip.Columns.Add(dcProjectCode);

           
            dtMerge = Atend.Base.Equipment.EAirPost.SelectAllAndMerge();
            Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selecetdProductCode = dPack.ProductCode;
            //MessageBox.Show(selecetdProductCode.ToString());

            gvDisconnector.AutoGenerateColumns = false;
            gvDisconnector.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { dPack.ProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvDisconnector, this);
            ChangeColor();
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvDisconnector.Rows[i].Cells[1].Value.ToString()) == selecetdProductCode && Convert.ToBoolean(gvDisconnector.Rows[i].Cells[4].Value.ToString()) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            BindDataToCboIsExist();
            //BindDataTocboProjCode();

            //gvSubEquip.AutoGenerateColumns = false;
            //gvSubEquip.DataSource = dtSuEquip;

            dtCurrent = Atend.Base.Design.DPackage.AccessSelectByParentCode(dPack.Code);

            foreach (DataRow Dr in dtCurrent.Rows)
            {
                DataRow SubEqRow = dtSuEquip.NewRow();
                SubEqRow["Code"] = Dr["ProductCode"];
                SubEqRow["IsExistance"] = Atend.Base.Base.BEquipStatus.SelectByACode(Convert.ToInt32(Dr["IsExistance"])).Code;

                SubEqRow["ProjectCode"] = Dr["ProjectCode"];
                //SubEqRow["IsSql"] = Dr["IsSql"];
                SubEqRow["Type"] = Dr["Type"];

                //if (Convert.ToInt32(Dr["Type"].ToString()) == (int)(Atend.Control.Enum.ProductType.MiddleJackPanel))
                //{
                //    Atend.Base.Equipment.EJAckPanel JP = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(Convert.ToInt32(Dr["ProductCode"].ToString()));
                //    SubEqRow["Name"] = JP.Name;
                //    SubEqRow["XCode"] = JP.XCode;
                //}

                if (Convert.ToInt32(Dr["Type"].ToString()) == (int)(Atend.Control.Enum.ProductType.WeekJackPanel))
                {
                    Atend.Base.Equipment.EJackPanelWeek JPW = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(Dr["ProductCode"].ToString()));
                    SubEqRow["Name"] = JPW.Name;
                    SubEqRow["XCode"] = JPW.XCode;
                }

                if (Convert.ToInt32(Dr["Type"].ToString()) == (int)(Atend.Control.Enum.ProductType.Transformer))
                {
                    Atend.Base.Equipment.ETransformer Trns = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(Dr["ProductCode"].ToString()));
                    SubEqRow["Name"] = Trns.Name;
                    SubEqRow["XCode"] = Trns.XCode;
                }

                dtSuEquip.Rows.Add(SubEqRow);
            }

            dtCurrent = dtSuEquip.Copy();

            gvSubEquip.AutoGenerateColumns = false;
            gvSubEquip.DataSource = dtSuEquip;
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;

            //Atend.Calculating.frmTest _frmTest = new Atend.Calculating.frmTest();
            //_frmTest.dataGridView4.DataSource = dtMerge;
            //_frmTest.ShowDialog();
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            double capacity = -1;
            string strFilter = "";
            bool check = false;

            if (chkCapacity.Checked)
            {
                capacity = Convert.ToInt32(txtCapacity.Text);
                if (strFilter != "")
                {
                    strFilter += " AND Capacity='" + capacity.ToString() + "'";
                }
                else
                {
                    strFilter = " Capacity='" + capacity.ToString() + "'";
                }
                check = true;
            }
            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvDisconnector.AutoGenerateColumns = false;
                gvDisconnector.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvDisconnector.AutoGenerateColumns = false;
                //gvDisconnector.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selecetdProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvDisconnector, this);
            ChangeColor();
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvDisconnector.Rows[i].Cells[1].Value.ToString()) == selecetdProductCode && Convert.ToBoolean(gvDisconnector.Rows[i].Cells[4].Value.ToString()) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        public  Atend.Global.Acad.DrawEquips.AcDrawAirPost AirPost = new Atend.Global.Acad.DrawEquips.AcDrawAirPost();
        private void btnOk_Click(object sender, EventArgs e)
        {
            txtCapacity.Focus();
            int projectcode = 0;
            int Existance = 0;
            if (Validation())
            {
                List<Atend.Base.Equipment.EJackPanelWeek> JackPanelWeeks = new List<Atend.Base.Equipment.EJackPanelWeek>();
                List<Atend.Base.Equipment.ETransformer> Transformers = new List<Atend.Base.Equipment.ETransformer>();
                ArrayList JackPanelWeekExistance = new ArrayList();
                ArrayList TransformerExistance = new ArrayList();
                ArrayList JackPanelWeekProjectCode = new ArrayList();
                ArrayList TransformerProjectCode = new ArrayList();


                //Atend.Global.Acad.DrawEquips.AcDrawAirPost AirPost = new Atend.Global.Acad.DrawEquips.AcDrawAirPost();
                dtSuEquip = (System.Data.DataTable)gvSubEquip.DataSource;

                //##
                //##
                
                Atend.Base.Base.BEquipStatus statusAir = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                AirPost.Existance = statusAir.ACode; //Convert.ToInt32(cboIsExist.SelectedValue);

                if (cboProjCode.Items.Count == 0)
                    AirPost.ProjectCode = 0;
                else
                    AirPost.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                AirPost.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);


                //AllowToclose = true;
                //if (AirPost.UpdateAirPostData(dpack.Code))
                //{
                //    AllowToclose = true;
                //    this.Close();
                //}
                //else
                //{
                //    AllowToclose = false;
                //}
                if (Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()) != selecetdProductCode)
                {
                    if (MessageBox.Show("آیا مایل به حذف پست موجود و \nجایگزین کردن پست جدید می باشید", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //Draw&Save
                        if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[4].Value) == true)
                        {
                            Atend.Base.Equipment.EAirPost airPost = Atend.Base.Equipment.EAirPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                            AirPost.UseAccess = false;
                            AirPost.eAirPost = airPost;

                            foreach (DataRow dr in dtSuEquip.Rows)
                            {
                                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                                Existance = status.ACode; //Convert.ToInt32(dr["IsExistance"].ToString());

                                if (dr["ProjectCode"].ToString() == "")
                                    projectcode = 0;
                                else
                                    projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                                {
                                    Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(dr["XCode"].ToString()));
                                    JackPanelWeekExistance.Add(Existance);
                                    JackPanelWeeks.Add(jackPanelWeek);
                                    JackPanelWeekProjectCode.Add(projectcode);
                                }

                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                                {
                                    Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(dr["XCode"].ToString()));
                                    TransformerExistance.Add(Existance);
                                    Transformers.Add(transformer);
                                    TransformerProjectCode.Add(projectcode);
                                }
                            }
                        }
                        else
                        {
                            Atend.Base.Equipment.EAirPost airPost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                            AirPost.UseAccess = true;
                            AirPost.eAirPost = airPost;

                            foreach (DataRow dr in dtSuEquip.Rows)
                            {
                                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                                Existance = status.ACode; //Convert.ToInt32(dr["IsExistance"].ToString());

                                if (dr["ProjectCode"].ToString() == "")
                                    projectcode = 0;
                                else
                                    projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                                {
                                    Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                                    JackPanelWeekExistance.Add(Existance);
                                    JackPanelWeeks.Add(jackPanelWeek);
                                    JackPanelWeekProjectCode.Add(projectcode);
                                }

                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                                {
                                    Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                                    TransformerExistance.Add(Existance);
                                    Transformers.Add(transformer);
                                    TransformerProjectCode.Add(projectcode.ToString());
                                }
                            }
                        }

                        AirPost.eJackPanelWeeks = JackPanelWeeks;
                        AirPost.eJackPanelWeekExistance = JackPanelWeekExistance;
                        AirPost.eJackPanelWeekProjectCode = JackPanelWeekProjectCode;
                        AirPost.eTransformers = Transformers;
                        AirPost.eTransformerExistance = TransformerExistance;
                        AirPost.eTransformerProjectCode = TransformerProjectCode;

                        if (AirPost.UpdateAirPostData(dpack.Code))
                        {
                            AirPost.DrawAirPost();
                            AllowToclose = true;
                            this.Close();
                        }
                        else
                        {
                            AllowToclose = false;
                        }
                    }
                    else
                        AllowToclose = false;
                }
                else
                {
                    //SaveWithoutDrawAllowToclose = false
                    foreach (DataRow dr in dtSuEquip.Rows)
                    {
                        dr["IsExistance"] = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["Isexistance"].ToString())).ACode;
                    }
                    if (!AirPost.UpdateAirPostWithoutDraw(dpack.Code, dtSuEquip))
                    {
                        Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                        notification.Title = "خطا";
                        notification.Msg = "خطا در ویرایش پست هوایی";
                        notification.ShowStatusBarBalloon();
                        AllowToclose = false;
                    }
                    else
                    {
                        AllowToclose = true;
                    }
                    
                }
            }
            else
            {
                AllowToclose = false;
            }
            //OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
            //Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance.Clear();
            //Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks.Clear();
            //Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance.Clear();
            //Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers.Clear();
            //int projectcode = 0;
            //int Existance = 0;
            //if (Validation())
            //{
            //    dtsub = (System.Data.DataTable)gvSubEquip.DataSource;
            //    if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[4].Value) == true)
            //    {
            //        ed.WriteMessage("It Is Local Wquipment\n");
            //        Atend.Base.Equipment.EAirPost AirPost = Atend.Base.Equipment.EAirPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
            //        Atend.Base.Acad.AcadGlobal.AirPostData.UseAccess = false;
            //        Atend.Base.Acad.AcadGlobal.AirPostData.eAirPost = AirPost;
            //        Atend.Base.Acad.AcadGlobal.AirPostData.Existance = Convert.ToByte(cboIsExist.SelectedValue);
            //        Atend.Base.Acad.AcadGlobal.AirPostData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

            //        txtName.Text = AirPost.Name;
            //        txtCapacity.Text = AirPost.Capacity.ToString();
            //        foreach (DataRow dr1 in dtsub.Rows)
            //        {
            //            Existance = Convert.ToInt32(dr1["IsExistance"].ToString());
            //            projectcode = Convert.ToInt32(dr1["ProjectCode"].ToString());
            //            if (Convert.ToInt32(dr1["Type"]) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
            //            {
            //                Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(dr1["XCode"].ToString()));
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance.Add(Existance);
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks.Add(jackPanelWeek);
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
            //            }


            //            if (Convert.ToInt32(dr1["Type"]) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
            //            {
            //                Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(dr1["XCode"].ToString()));
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance.Add(Existance);
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers.Add(transformer);
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
            //            }
            //        }

            //    }
            //    else
            //    {
            //        ed.WriteMessage("It Is Access Wquipment\n");
            //        Atend.Base.Equipment.EAirPost AirPost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
            //        Atend.Base.Acad.AcadGlobal.AirPostData.UseAccess = true;
            //        Atend.Base.Acad.AcadGlobal.AirPostData.eAirPost = AirPost;
            //        Atend.Base.Acad.AcadGlobal.AirPostData.Existance = Convert.ToByte(cboIsExist.SelectedValue);
            //        Atend.Base.Acad.AcadGlobal.AirPostData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);
            //        txtName.Text = AirPost.Name;
            //        txtCapacity.Text = AirPost.Capacity.ToString();
            //        foreach (DataRow dr1 in dtsub.Rows)
            //        {
            //            Existance = Convert.ToInt32(dr1["IsExistance"].ToString());
            //            projectcode = Convert.ToInt32(dr1["ProjectCode"].ToString());
            //            if (Convert.ToInt32(dr1["Type"]) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
            //            {
            //                Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(dr1["Code"]));
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance.Add(Existance);
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks.Add(jackPanelWeek);
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
            //            }


            //            if (Convert.ToInt32(dr1["Type"]) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
            //            {
            //                Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(dr1["Code"]));
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance.Add(Existance);
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers.Add(transformer);
            //                Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
            //            }
            //        }
            //    }
            //    AllowToclose = true;
            //}
            //else
            //{
            //    AllowToclose = false;
            //}
        }

        private void gvDisconnector_Click(object sender, EventArgs e)
        {
            bool Valid = false;
            System.Data.DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX();
            System.Data.DataTable dt2 = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
            dtSuEquip.Clear();
            Atend.Base.Equipment.EAirPost AirPost;
            if (gvDisconnector.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[4].Value) == true)
                {
                    AirPost = Atend.Base.Equipment.EAirPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                    for (int i = 0; i < Atend.Base.Equipment.EAirPost.nodeKeysEPackageX.Count; i++)
                    {
                        int count = Convert.ToInt32(Atend.Base.Equipment.EAirPost.nodeCountEPackageX[i].ToString());
                        for (int j = 0; j < count; j++)
                        {
                            Valid = false;
                            DataRow dr = dtSuEquip.NewRow();
                            dr["XCode"] = Atend.Base.Equipment.EAirPost.nodeKeysEPackageX[i];
                            string name = "";
                            switch ((Atend.Control.Enum.ProductType)(int.Parse(Atend.Base.Equipment.EAirPost.nodeTypeEPackageX[i].ToString())))
                            {
                                case Atend.Control.Enum.ProductType.Transformer:
                                    {
                                        Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(Atend.Base.Equipment.EAirPost.nodeKeysEPackageX[i].ToString()));
                                        name = transformer.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                                        Valid = true;
                                        break;

                                    }
                                case Atend.Control.Enum.ProductType.WeekJackPanel:
                                    {
                                        Atend.Base.Equipment.EJackPanelWeek jackPanelweek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(Atend.Base.Equipment.EAirPost.nodeKeysEPackageX[i].ToString()));
                                        name = jackPanelweek.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                                        Valid = true;
                                        break;

                                    }
                                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                                    {
                                        Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.SelectByXCode(new Guid(Atend.Base.Equipment.EAirPost.nodeKeysEPackageX[i].ToString()));
                                        name = jackPanel.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
                                        Valid = true;
                                        break;

                                    }
                            }
                            if (Valid == true)
                            {
                                dr["Name"] = name;
                                dr["IsSql"] = true;
                                dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                if (cboProjCode.Items.Count > 0)
                                {
                                    dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                }
                                else
                                {
                                    dr["ProjectCode"] = 0;
                                }
                                //dr["IsExistance"] = Convert.ToByte(dt.Rows[0]["Code"]);
                                //dr["ProjectCode"] = Convert.ToInt32(dt2.Rows[0]["Code"]);
                                dtSuEquip.Rows.Add(dr);
                            }
                        }
                    }

                }
                else
                {
                    AirPost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                    for (int i = 0; i < Atend.Base.Equipment.EAirPost.nodeKeysEPackage.Count; i++)
                    {
                        int count = Convert.ToInt32(Atend.Base.Equipment.EAirPost.nodeCountEPackage[i].ToString());
                        for (int j = 0; j < count; j++)
                        {
                            Valid = false;
                            DataRow dr = dtSuEquip.NewRow();
                            dr["Code"] = Atend.Base.Equipment.EAirPost.nodeKeysEPackage[i];
                            string name = "";
                            switch ((Atend.Control.Enum.ProductType)(int.Parse(Atend.Base.Equipment.EAirPost.nodeTypeEPackage[i].ToString())))
                            {
                                case Atend.Control.Enum.ProductType.Transformer:
                                    {
                                        Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EAirPost.nodeKeysEPackage[i].ToString()));
                                        name = transformer.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                                        Valid = true;
                                        break;
                                    }
                                case Atend.Control.Enum.ProductType.WeekJackPanel:
                                    {
                                        Atend.Base.Equipment.EJackPanelWeek jackPanelweek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EAirPost.nodeKeysEPackage[i].ToString()));
                                        name = jackPanelweek.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                                        Valid = true;
                                        break;
                                    }
                                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                                    {
                                        Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EAirPost.nodeKeysEPackage[i].ToString()));
                                        name = jackPanel.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
                                        Valid = true;
                                        break;
                                    }
                            }
                            if (Valid == true)
                            {
                                dr["Name"] = name;
                                dr["IsSql"] = false;
                                dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                if (cboProjCode.Items.Count > 0)
                                {
                                    dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                }
                                else
                                {
                                    dr["ProjectCode"] = 0;
                                }
                                //dr["IsExistance"] = Convert.ToByte(dt.Rows[0]["Code"]);
                                //dr["ProjectCode"] = Convert.ToInt32(dt2.Rows[0]["Code"]);
                                dtSuEquip.Rows.Add(dr);
                            }
                        }
                    }
                }
                txtName.Text = AirPost.Name;
                txtSelectedCapacity.Text = AirPost.Capacity.ToString();

                //if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[4].Value) == false)
                //{
                //    dtSuEquip = dtCurrent.Copy();
                //    gvSubEquip.AutoGenerateColumns = false;
                //    gvSubEquip.DataSource = dtSuEquip;
                //    return;
                //}
            }

            gvSubEquip.Refresh();

            //Atend.Calculating.frmTest a = new Atend.Calculating.frmTest();
            //a.dataGridView4.DataSource = dtSuEquip;
            //a.ShowDialog();
        }

        public void BindDataToCboInGridView()
        {
            System.Data.DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dt;
            c.DisplayMember = "Name";
            c.ValueMember = "Code";
            c.DataPropertyName = "IsExistance";
            c.HeaderText = "وضعیت تجهیز";
            gvSubEquip.Columns.Insert(gvSubEquip.Columns.Count - 1, c);
        }

        public void AddProjectCodeColumn()
        {
            System.Data.DataTable dt2 = Atend.Base.Base.BWorkOrder.SelectChilds();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dt2;
            c.DisplayMember = "Name";
            c.ValueMember = "ACode";
            c.DataPropertyName = "ProjectCode";
            c.HeaderText = "شرح دستورکار";
            gvSubEquip.Columns.Insert(gvSubEquip.Columns.Count - 1, c);
        }

        public void BindDataToCboIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowToclose = true;
        }

        private void frmEditDrawAirPost02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToclose)
            {
                e.Cancel = true;
            }
        }

        private void cboIsExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
            cboProjCode.DisplayMember = "Name";
            cboProjCode.ValueMember = "ACode";
            cboProjCode.DataSource = dtWorkOrder;
        }

        private void txtCapacity_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvSubEquip_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(gvSubEquip.Rows[gvSubEquip.CurrentRow.Index].Cells[0].Value.ToString() + "\n" + gvSubEquip.Rows[gvSubEquip.CurrentRow.Index].Cells[1].Value.ToString());
        }

    }
}