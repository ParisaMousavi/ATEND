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
    public partial class frmEditDrawGroundPost02 : Form
    {
        bool AllowToclose=true;
        public Guid GPCode;
        //public int PostCode;
        public Guid PostCode;
        public int transformerCount = 0;
        public int MiddlejackPanelCount = 0;
        public int WeekJackPAnelCount = 0;
        //public int GroundPostProductCode = 0;
        public Guid GroundPostProductCode = Guid.Empty;
        public ArrayList arMiddleJAckPAnel = new ArrayList();
        public ArrayList arweekJackPanel = new ArrayList();
        public ArrayList arTransformer = new ArrayList();

        System.Data.DataTable dtMerge = new System.Data.DataTable();
        System.Data.DataTable dtSuEquip = new System.Data.DataTable();
        System.Data.DataTable dtCurrent = new System.Data.DataTable();
        public ObjectId ObjID;
        public Guid NodeCode;
        int selectedProductCode = -1;
        bool ForceToClose = false;

        public frmEditDrawGroundPost02()
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

        private bool Validation()
        {
            bool flag = true;

            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

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

            return true;

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
                //gvDisconnector.AutoGenerateColumns = false;
                gvDisconnector.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvDisconnector.AutoGenerateColumns = false;
                //gvDisconnector.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvDisconnector, this);
            ChangeColor();
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvDisconnector.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvDisconnector.Rows[i].Cells[2].Value.ToString()) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            
            
            ////////////////////
            //double capacity = -1;
            ////int cellCount = -1;
            //if (Validation())
            //{

            //    if (chkCapacity.Checked)
            //        capacity = Convert.ToDouble(txtCapacity.Text);
            //    //if (chkCellCount.Checked)
            //    //    cellCount = Convert.ToInt32(nudCellCount.Value);

            //    gvDisconnector.AutoGenerateColumns = false;
            //    gvDisconnector.DataSource = Atend.Base.Equipment.EGroundPost.DrawSearch(capacity);
            //}
        }

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

        private void gvDisconnector_Click(object sender, EventArgs e)
        {
            if (gvDisconnector.Rows.Count > 0)
            {
                Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(
                    new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));

                txtName.Text = groundPost.Name;
                txtCellCount.Text = groundPost.CellCount.ToString();
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

        private void frmEditDrawGroundPost01_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToclose)
            {
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowToclose = true;
        }

        public Atend.Global.Acad.DrawEquips.AcDrawGroundPost GroundPost = new Atend.Global.Acad.DrawEquips.AcDrawGroundPost();

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            txtAdvanceType.Focus();
            int projectcode = 0;
            int Existance = 0;
            if (Validation())
            {
                List<Atend.Base.Equipment.EJAckPanel> JackPanelMiddles = new List<Atend.Base.Equipment.EJAckPanel>();
                List<Atend.Base.Equipment.EJackPanelWeek> JackPanelWeeks = new List<Atend.Base.Equipment.EJackPanelWeek>();
                List<Atend.Base.Equipment.ETransformer> Transformers = new List<Atend.Base.Equipment.ETransformer>();
                ArrayList JackPanelMiddleExistance = new ArrayList();
                ArrayList JackPanelWeekExistance = new ArrayList();
                ArrayList TransformerExistance = new ArrayList();
                ArrayList JackPanelMiddleProjectCode = new ArrayList();
                ArrayList JackPanelWeekProjectCode = new ArrayList();
                ArrayList TransformerProjectCode = new ArrayList();


                dtSuEquip = (System.Data.DataTable)gvSubEquip.DataSource;

                //##
                //##

                Atend.Base.Base.BEquipStatus statusGround = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                GroundPost.Existance = statusGround.ACode; //Convert.ToInt32(cboIsExist.SelectedValue);

                if (cboProjCode.Items.Count == 0)
                    GroundPost.ProjectCode = 0;
                else
                    GroundPost.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                //MessageBox.Show(ObjID.ToString());
                GroundPost.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);


                //AllowToclose = true;
                //if (GroundPost.UpdateGroundPostData(dpack.Code))
                //{
                //    AllowToclose = true;
                //    this.Close();
                //}
                //else
                //{
                //    AllowToclose = false;
                //}
                if (Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()) != selectedProductCode)
                {
                    if (MessageBox.Show("آیا مایل به حذف پست موجود و \nجایگزین کردن پست جدید می باشید", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == true)
                        {
                            Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                            //Atend.Base.Acad.AcadGlobal.GroundPostData.UseAccess = false;
                            //Atend.Base.Acad.AcadGlobal.GroundPostData.eGroundPost = groundPost;
                            GroundPost.UseAccess = false;
                            GroundPost.eGroundPost = groundPost;

                            //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
                            foreach (DataRow dr in dtSuEquip.Rows)
                            {
                                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                                Existance = status.ACode; //Convert.ToInt32(dr["IsExistance"].ToString());

                                if (dr["ProjectCode"].ToString() == "")
                                    projectcode = 0;
                                else
                                    projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                                //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[6];
                                //ed.WriteMessage("Type={0}\n", gvSubEquip.Rows[i].Cells[5].Value.ToString());
                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel))
                                {
                                    Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.SelectByXCode(new Guid(dr["XCode"].ToString()));
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance.Add(Existance);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles.Add(jackPanel);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleProjectCode.Add(projectcode);
                                    JackPanelMiddleExistance.Add(Existance);
                                    JackPanelMiddles.Add(jackPanel);
                                    JackPanelMiddleProjectCode.Add(projectcode);
                                }

                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                                {
                                    Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(dr["XCode"].ToString()));
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance.Add(Existance);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks.Add(jackPanelWeek);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekProjectCode.Add(projectcode);
                                    JackPanelWeekExistance.Add(Existance);
                                    JackPanelWeeks.Add(jackPanelWeek);
                                    JackPanelWeekProjectCode.Add(projectcode.ToString());
                                }


                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                                {
                                    Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(dr["XCode"].ToString()));
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance.Add(Existance);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers.Add(transformer);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerProjectCode.Add(projectcode);
                                    TransformerExistance.Add(Existance);
                                    Transformers.Add(transformer);
                                    TransformerProjectCode.Add(projectcode.ToString());
                                }

                            }
                        }
                        else
                        {
                            Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                            //Atend.Base.Acad.AcadGlobal.GroundPostData.UseAccess = true;
                            //Atend.Base.Acad.AcadGlobal.GroundPostData.eGroundPost = groundPost;
                            GroundPost.UseAccess = true;
                            GroundPost.eGroundPost = groundPost;

                            //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
                            foreach (DataRow dr in dtSuEquip.Rows)
                            {
                                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                                Existance = status.ACode; //Convert.ToInt32(dr["IsExistance"].ToString());

                                if (dr["ProjectCode"].ToString() == "")
                                    projectcode = 0;
                                else
                                    projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                                //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[6];
                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel))
                                {
                                    Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleExistance.Add(Existance);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddles.Add(jackPanel);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelMiddleProjectCode.Add(projectcode);
                                    JackPanelMiddleExistance.Add(Existance);
                                    JackPanelMiddles.Add(jackPanel);
                                    JackPanelMiddleProjectCode.Add(projectcode.ToString());
                                }

                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                                {
                                    //Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(gvSubEquip.Rows[i].Cells[1].Value.ToString()));
                                    Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekExistance.Add(Existance);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeeks.Add(jackPanelWeek);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eJackPanelWeekProjectCode.Add(projectcode);
                                    JackPanelWeekExistance.Add(Existance);
                                    JackPanelWeeks.Add(jackPanelWeek);
                                    JackPanelWeekProjectCode.Add(projectcode.ToString());
                                }


                                if (Convert.ToInt32(dr["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                                {

                                    Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerExistance.Add(Existance);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformers.Add(transformer);
                                    //Atend.Base.Acad.AcadGlobal.GroundPostData.eTransformerProjectCode.Add(projectcode);
                                    TransformerExistance.Add(Existance);
                                    Transformers.Add(transformer);
                                    TransformerProjectCode.Add(projectcode.ToString());
                                }

                            }
                        }
                        GroundPost.eJackPanelMiddles = JackPanelMiddles;
                        GroundPost.eJackPanelMiddleExistance = JackPanelMiddleExistance;
                        GroundPost.eJackPanelMiddleProjectCode = JackPanelMiddleProjectCode;
                        GroundPost.eJackPanelWeeks = JackPanelWeeks;
                        GroundPost.eJackPanelWeekExistance = JackPanelWeekExistance;
                        GroundPost.eJackPanelWeekProjectCode = JackPanelWeekProjectCode;
                        GroundPost.eTransformers = Transformers;
                        GroundPost.eTransformerExistance = TransformerExistance;
                        GroundPost.eTransformerProjectCode = TransformerProjectCode;

                        if (GroundPost.UpdateGroundPostData(dpack.Code))
                        {
                            GroundPost.DrawGroundPost();
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
                    if (!GroundPost.UpdateGroundPostWithoutDraw(dpack.Code, dtSuEquip))
                    {
                        Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                        notification.Title = "خطا";
                        notification.Msg = "خطا در ویرایش پست زمینی";
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


            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //if (!Atend.Global.Desig.NodeTransaction.DeleteGroundPost(GPCode))
            //{
            //    ed.WriteMessage("\nEXIT\n");
            //    return;
            //}

            //if (Validation())
            //{
            //    Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(
            //        new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
            //    //**EDIT**
            //    //GroundPostProductCode = groundPost.Code;
            //    GroundPostProductCode = groundPost.XCode;
            //    DataTable dtWeekJackPanel = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndTypeAndTAbleType(groundPost.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost), Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel));
            //    DataTable dtMiddleJAckanel = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndTypeAndTAbleType(groundPost.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost), Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel));
            //    DataTable dtTransform = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndTypeAndTAbleType(groundPost.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost), Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer));
            //    transformerCount = dtTransform.Rows.Count;
            //    MiddlejackPanelCount = dtMiddleJAckanel.Rows.Count;

            //    MiddlejackPanelCount = 0;
            //    foreach (DataRow dr in dtMiddleJAckanel.Rows)
            //    {

            //        int MiddleCounter = Convert.ToInt32(dr["Count"]);
            //        for (int i = 1; i <= MiddleCounter; i++)
            //        {
            //            arMiddleJAckPAnel.Add(dr["ProductCode"].ToString());
            //            MiddlejackPanelCount++;
            //        }
            //    }

            //    //WeekJackPAnelCount = dtWeekJackPanel.Rows.Count;
            //    WeekJackPAnelCount = 0;
            //    foreach (DataRow dr in dtWeekJackPanel.Rows)
            //    {

            //        int WeekCounter = Convert.ToInt32(dr["Count"]);
            //        for (int i = 1; i <= WeekCounter; i++)
            //        {
            //            arweekJackPanel.Add(dr["ProductCode"]);
            //            WeekJackPAnelCount++;
            //        }
            //    }

            //    transformerCount = 0;
            //    foreach (DataRow dr in dtTransform.Rows)
            //    {

            //        int TransformerCounter = Convert.ToInt32(dr["Count"]);
            //        for (int i = 1; i <= TransformerCounter; i++)
            //        {
            //            arTransformer.Add(dr["ProductCode"]);
            //            transformerCount++;
            //        }

            //    }

            //    //PostCode = Convert.ToInt32(gvDisconnector.SelectedRows[0].Cells[0].Value.ToString());
            //    PostCode = new Guid(gvDisconnector.SelectedRows[0].Cells[0].Value.ToString());

            //    AllowToclose = true;
            //}

        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        private void frmEditDrawGroundPost01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            dtMerge = Atend.Base.Equipment.EGroundPost.SelectAllAndMerge();
            Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selectedProductCode = dPack.ProductCode;
            
            gvDisconnector.AutoGenerateColumns = false;
            gvDisconnector.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvDisconnector, this);
            ChangeColor();
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvDisconnector.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvDisconnector.Rows[i].Cells[2].Value.ToString()) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            System.Data.DataColumn dtCode = new System.Data.DataColumn("Code");
            dtSuEquip.Columns.Add(dtCode);
            System.Data.DataColumn dtXCode = new System.Data.DataColumn("XCode");
            dtSuEquip.Columns.Add(dtXCode);
            System.Data.DataColumn dtName = new System.Data.DataColumn("Name");
            dtSuEquip.Columns.Add(dtName);
            System.Data.DataColumn dtIsSql = new System.Data.DataColumn("IsSql");
            dtSuEquip.Columns.Add(dtIsSql);
            System.Data.DataColumn dtType = new System.Data.DataColumn("Type");
            dtSuEquip.Columns.Add(dtType);
            System.Data.DataColumn dcIsExistance = new System.Data.DataColumn("IsExistance", System.Type.GetType("System.Int32"));
            dtSuEquip.Columns.Add(dcIsExistance);
            System.Data.DataColumn dcProjectCode = new System.Data.DataColumn("ProjectCode", System.Type.GetType("System.Int32"));
            dtSuEquip.Columns.Add(dcProjectCode);


            //Atend.Base.Design.DNode node = Atend.Base.Design.DNode.AccessSelectByCode(NodeCode);
            //txtHeight.Text = Convert.ToString(Math.Round(node.Height, 2));

            dtCurrent = Atend.Base.Design.DPackage.AccessSelectByParentCode(dPack.Code);

            foreach (DataRow Dr in dtCurrent.Rows)
            {
                DataRow SubEqRow = dtSuEquip.NewRow();
                SubEqRow["Code"] = Dr["ProductCode"];
                SubEqRow["IsExistance"] = Atend.Base.Base.BEquipStatus.SelectByACode(Convert.ToInt32(Dr["IsExistance"])).Code;

                SubEqRow["ProjectCode"] = Dr["ProjectCode"];
                //SubEqRow["IsSql"] = Dr["IsSql"];
                SubEqRow["Type"] = Dr["Type"];

                if(Convert.ToInt32(Dr["Type"].ToString()) == (int)(Atend.Control.Enum.ProductType.MiddleJackPanel))
                {
                    Atend.Base.Equipment.EJAckPanel JP = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(Convert.ToInt32(Dr["ProductCode"].ToString()));
                    SubEqRow["Name"] = JP.Name;
                    SubEqRow["XCode"] = JP.XCode;
                }

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

            BindDataToIsExist();
            //BindDataTocboProjCode();
            
            BindDataToCboInGridView();
            AddProjectCodeColumn();

            gvSubEquip.AutoGenerateColumns = false;
            gvSubEquip.DataSource = dtSuEquip;
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;


            //Atend.Calculating.frmTest _frmTest = new Atend.Calculating.frmTest();
            //_frmTest.dataGridView4.DataSource = dtSuEquip;
            //_frmTest.ShowDialog();
        }

        public void BindDataToIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
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

        private void gvDisconnector_Click_1(object sender, EventArgs e)
        {
            bool Valid = false;
            dtSuEquip.Rows.Clear();
            Atend.Base.Equipment.EGroundPost groundPost;
            System.Data.DataTable dtExistance = Atend.Base.Base.BEquipStatus.SelectAllX();
            System.Data.DataTable dtProjCode = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));

            if (gvDisconnector.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == true)
                {
                    groundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                    for (int i = 0; i < Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX.Count; i++)
                    {
                        int count = Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeCountEPackageX[i].ToString());
                        for (int j = 0; j < count; j++)
                        {
                            Valid = false;
                            DataRow dr = dtSuEquip.NewRow();

                            dr["XCode"] = Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX[i];
                            string name = "";
                            switch ((Atend.Control.Enum.ProductType)(int.Parse(Atend.Base.Equipment.EGroundPost.nodeTypeEPackageX[i].ToString())))
                            {
                                case Atend.Control.Enum.ProductType.Transformer:
                                    {
                                        Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX[i].ToString()));
                                        name = transformer.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                                        Valid = true;
                                        break;

                                    }
                                case Atend.Control.Enum.ProductType.WeekJackPanel:
                                    {
                                        Atend.Base.Equipment.EJackPanelWeek jackPanelweek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX[i].ToString()));
                                        name = jackPanelweek.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                                        Valid = true;
                                        break;

                                    }
                                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                                    {
                                        Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.SelectByXCode(new Guid(Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX[i].ToString()));
                                        name = jackPanel.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
                                        Valid = true;
                                        break;

                                    }
                            }

                            if (Valid == true)
                            {
                                dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                if (cboProjCode.Items.Count > 0)
                                {
                                    dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                }
                                else
                                {
                                    dr["ProjectCode"] = 0;
                                }
                                //dr["IsExistance"] = Convert.ToInt32(dtExistance.Rows[0]["Code"].ToString());
                                //dr["ProjectCode"] = Convert.ToInt32(dtProjCode.Rows[0]["Code"].ToString());
                                dr["Name"] = name;
                                dr["IsSql"] = true;
                                dtSuEquip.Rows.Add(dr);
                            }
                        }
                    }

                }
                else
                {
                    groundPost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                    for (int i = 0; i < Atend.Base.Equipment.EGroundPost.nodeKeysEPackage.Count; i++)
                    {
                        int count = Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeCountEPackage[i].ToString());
                        for (int j = 0; j < count; j++)
                        {
                            Valid = false;
                            DataRow dr = dtSuEquip.NewRow();
                            dr["Code"] = Atend.Base.Equipment.EGroundPost.nodeKeysEPackage[i];
                            string name = "";
                            switch ((Atend.Control.Enum.ProductType)(int.Parse(Atend.Base.Equipment.EGroundPost.nodeTypeEPackage[i].ToString())))
                            {
                                case Atend.Control.Enum.ProductType.Transformer:
                                    {
                                        Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeKeysEPackage[i].ToString()));
                                        name = transformer.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                                        Valid = true;
                                        break;
                                    }
                                case Atend.Control.Enum.ProductType.WeekJackPanel:
                                    {
                                        Atend.Base.Equipment.EJackPanelWeek jackPanelweek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeKeysEPackage[i].ToString()));
                                        name = jackPanelweek.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                                        Valid = true;
                                        break;

                                    }
                                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                                    {
                                        Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EGroundPost.nodeKeysEPackage[i].ToString()));
                                        name = jackPanel.Name;
                                        dr["Type"] = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
                                        Valid = true;
                                        break;

                                    }
                            }
                            if (Valid)
                            {
                                dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                if (cboProjCode.Items.Count > 0)
                                {
                                    dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                }
                                else
                                {
                                    dr["ProjectCode"] = 0;
                                }
                                //dr["IsExistance"] = Convert.ToInt32(dtExistance.Rows[0]["Code"].ToString());
                                //dr["ProjectCode"] = Convert.ToInt32(dtProjCode.Rows[0]["Code"].ToString());
                                dr["Name"] = name;
                                dr["IsSql"] = false;
                                dtSuEquip.Rows.Add(dr);
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

                //if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == false)
                //{
                //    dtSuEquip = dtCurrent.Copy();
                //    gvSubEquip.AutoGenerateColumns = false;
                //    gvSubEquip.DataSource = dtSuEquip;
                //    return;
                //}
            }

        }

        private void cboIsExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
            cboProjCode.DisplayMember = "Name";
            cboProjCode.ValueMember = "ACode";
            cboProjCode.DataSource = dtWorkOrder;
        }

    }
}