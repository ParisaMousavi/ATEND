using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmDrawAirPost02 : Form
    {

        public int transformerCount = 0;
        public int MiddlejackPanelCount = 0;
        public int WeekJackPAnelCount = 0;
        public int AirPostProductCode = 0;
        public ArrayList arMiddleJAckPAnel = new ArrayList();
        public ArrayList arweekJackPanel = new ArrayList();
        public ArrayList arTransformer = new ArrayList();
        DataTable dtMerge = new DataTable();
        DataTable dtsub = new DataTable();
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool AllowToclose = true;
        bool ForceToClose = false;
        System.Data.DataTable dtBWorkOrders;




        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.AirPostData.eAirPost = new Atend.Base.Equipment.EAirPost();
            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance = new ArrayList();
            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode = new ArrayList();
            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks = new List<Atend.Base.Equipment.EJackPanelWeek>();
            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance = new ArrayList();
            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerProjectCode = new ArrayList();
            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers = new List<Atend.Base.Equipment.ETransformer>();
            Atend.Base.Acad.AcadGlobal.AirPostData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.AirPostData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.AirPostData.UseAccess = false;
        }

        public frmDrawAirPost02()
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
            dtBWorkOrders = Atend.Base.Base.BWorkOrder.SelectChilds();
        }

        //public void BindDataTocboInGrid()
        //{
        //    DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX();

        //    DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvSubEquip.Columns["Column9"];
        //    c.DataSource = dt;
        //    c.DisplayMember = "Name";
        //    c.ValueMember = "Code";

        //    DataTable dt2 = Atend.Base.Base.BWorkOrder.SelectAll();
        //    DataGridViewComboBoxColumn c2 = (DataGridViewComboBoxColumn)gvSubEquip.Columns["Column5"];
        //    c2.DataSource = dt2;
        //    c2.DisplayMember = "Name";
        //    c2.ValueMember = "Code";

        //    //DataTable dt = Atend.Control.Common.StatuseCode.Copy();
        //    //cboIsExist.DisplayMember = "Name";
        //    //cboIsExist.ValueMember = "Code";
        //    //cboIsExist.DataSource = dt;
        //}


        //public void ChangeColor()
        //{
        //    for (int i = 0; i < gvDisconnector.Rows.Count; i++)
        //    {
        //        if (Convert.ToBoolean(gvDisconnector.Rows[i].Cells[4].Value) == false)
        //        {
        //            gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
        //        }
        //    }
        //}

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

            //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
            //{
            //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[3];
            //    if (gvSubEquip.Rows[i].Cells[3].Value == null)
            //    {
            //        MessageBox.Show("لطفاً کد صورت وضعیت را انتخاب کنید", "خطا");
            //        return false;
            //    }
            //}

            //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
            //{
            //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[6];
            //    if (gvSubEquip.Rows[i].Cells[6].Value == null)
            //    {
            //        MessageBox.Show("لطفاً کد وضعیت تجهیز را انتخاب کنید", "خطا");
            //        return false;
            //    }
            //}

            if (gvDisconnector.Rows.Count > 0 && gvDisconnector.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;
            }

            return true;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                int capacity = -1;
                bool Check = false;
                string strFilter = "";

                if (chkCapacity.Checked)
                {
                    if (!string.IsNullOrEmpty(txtCapacity.Text))
                    {
                        capacity = Convert.ToInt32(txtCapacity.Text);
                        strFilter = "Capacity ='" + capacity + "'";
                        Check = true;
                    }
                }
                if (Check)
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
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnCancel.Focus();
            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance.Clear();
            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks.Clear();
            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance.Clear();
            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers.Clear();
            int projectcode = 0;
            int Existance = 0;
            if (Validation())
            {
                dtsub = (System.Data.DataTable)gvSubEquip.DataSource;
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[4].Value) == true)
                {
                    Atend.Base.Equipment.EAirPost AirPost = Atend.Base.Equipment.EAirPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.AirPostData.UseAccess = false;
                    Atend.Base.Acad.AcadGlobal.AirPostData.eAirPost = AirPost;


                    txtName.Text = AirPost.Name;
                    txtCapacity.Text = AirPost.Capacity.ToString();
                    foreach (DataRow dr1 in dtsub.Rows)
                    {
                        Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr1["IsExistance"].ToString()));
                        Existance = status.ACode; //Convert.ToInt32(dr1["IsExistance"].ToString());

                        if (dr1["ProjectCode"].ToString() == "")
                            projectcode = 0;
                        else
                            projectcode = Convert.ToInt32(dr1["ProjectCode"].ToString());

                        if (Convert.ToInt32(dr1["Type"]) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                        {
                            Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(dr1["XCode"].ToString()));
                            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks.Add(jackPanelWeek);
                            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
                        }


                        if (Convert.ToInt32(dr1["Type"]) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                        {
                            Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(dr1["XCode"].ToString()));
                            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers.Add(transformer);
                            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerProjectCode.Add(projectcode);
                        }
                    }

                    //////////////////////
                    //Atend.Base.Equipment.EAirPost AirPost = Atend.Base.Equipment.EAirPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                    //Atend.Base.Acad.AcadGlobal.AirPostData.UseAccess = false;
                    //Atend.Base.Acad.AcadGlobal.AirPostData.eAirPost = AirPost;
                    //Atend.Base.Acad.AcadGlobal.AirPostData.Existance = Convert.ToByte(cboIsExist.SelectedValue);
                    //Atend.Base.Acad.AcadGlobal.AirPostData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                    //txtName.Text = AirPost.Name;
                    //txtCapacity.Text = AirPost.Capacity.ToString();
                    //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
                    //{

                    //    //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[3];
                    //    //if (c.Value.ToString() == "موجود-موجود")
                    //    //{
                    //    //    Existance = 0;
                    //    //}
                    //    //if (c.Value.ToString() == "موجود-مستعمل")
                    //    //{
                    //    //    Existance = 1;
                    //    //}
                    //    //if (c.Value.ToString() == "موجود-اسقاط")
                    //    //{
                    //    //    Existance = 2;
                    //    //}
                    //    //if (c.Value.ToString() == "موجود-جابجایی")
                    //    //{
                    //    //    Existance = 3;
                    //    //}
                    //    //if (c.Value.ToString() == "پیشنهادی-نو")
                    //    //{
                    //    //    Existance = 4;
                    //    //}
                    //    //if (c.Value.ToString() == "پیشنهادی-جابجایی")
                    //    //{
                    //    //    Existance = 5;
                    //    //}
                    //    //ed.WriteMessage("Type={0}\n", gvSubEquip.Rows[i].Cells[5].Value.ToString());

                    //    //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[6];
                    //    //projectcode = Convert.ToInt32(cPCode.Value);

                    //    if (Convert.ToInt32(gvSubEquip.Rows[i].Cells[5].Value) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                    //    {

                    //        Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(gvSubEquip.Rows[i].Cells[1].Value.ToString()));
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance.Add(Existance);
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks.Add(jackPanelWeek);
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
                    //    }


                    //    if (Convert.ToInt32(gvSubEquip.Rows[i].Cells[5].Value) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                    //    {

                    //        Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(gvSubEquip.Rows[i].Cells[1].Value.ToString()));
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance.Add(Existance);
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers.Add(transformer);
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
                    //    }

                    //}
                }
                else
                {
                    ed.WriteMessage("It Is Access Wquipment\n");
                    Atend.Base.Equipment.EAirPost AirPost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.AirPostData.UseAccess = true;
                    Atend.Base.Acad.AcadGlobal.AirPostData.eAirPost = AirPost;

                    txtName.Text = AirPost.Name;
                    txtCapacity.Text = AirPost.Capacity.ToString();
                    foreach (DataRow dr1 in dtsub.Rows)
                    {
                        Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr1["IsExistance"].ToString()));
                        Existance = status.ACode; //Convert.ToInt32(dr1["IsExistance"].ToString());

                        if (dr1["ProjectCode"].ToString() == "")
                            projectcode = 0;
                        else
                            projectcode = Convert.ToInt32(dr1["ProjectCode"].ToString());

                        if (Convert.ToInt32(dr1["Type"]) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                        {
                            Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(dr1["Code"]));
                            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks.Add(jackPanelWeek);
                            Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
                        }


                        if (Convert.ToInt32(dr1["Type"]) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                        {
                            Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(dr1["Code"]));
                            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance.Add(Existance);
                            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers.Add(transformer);
                            Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerProjectCode.Add(projectcode);
                        }
                    }

                    ///////////////////////
                    //Atend.Base.Equipment.EAirPost AirPost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                    //Atend.Base.Acad.AcadGlobal.AirPostData.UseAccess = true;
                    //Atend.Base.Acad.AcadGlobal.AirPostData.eAirPost = AirPost;
                    //Atend.Base.Acad.AcadGlobal.AirPostData.Existance = Convert.ToByte(cboIsExist.SelectedValue);
                    //Atend.Base.Acad.AcadGlobal.AirPostData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);
                    //txtName.Text = AirPost.Name;
                    //txtCapacity.Text = AirPost.Capacity.ToString();
                    //Atend.Base.Equipment.EAirPost AirPost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                    //Atend.Base.Acad.AcadGlobal.AirPostData.UseAccess = true;
                    //Atend.Base.Acad.AcadGlobal.AirPostData.eAirPost = AirPost;
                    //Atend.Base.Acad.AcadGlobal.AirPostData.Existance = Convert.ToByte(cboIsExist.SelectedValue);
                    //Atend.Base.Acad.AcadGlobal.AirPostData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);
                    //txtName.Text = AirPost.Name;
                    //txtCapacity.Text = AirPost.Capacity.ToString();
                    //for (int i = 0; i < gvSubEquip.Rows.Count; i++)
                    //{

                    //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[3];
                    //    if (c.Value.ToString() == "موجود-موجود")
                    //    {
                    //        Existance = 0;
                    //    }
                    //    if (c.Value.ToString() == "موجود-مستعمل")
                    //    {
                    //        Existance = 1;
                    //    }
                    //    if (c.Value.ToString() == "موجود-اسقاط")
                    //    {
                    //        Existance = 2;
                    //    }
                    //    if (c.Value.ToString() == "موجود-جابجایی")
                    //    {
                    //        Existance = 3;
                    //    }
                    //    if (c.Value.ToString() == "پیشنهادی-نو")
                    //    {
                    //        Existance = 4;
                    //    }
                    //    if (c.Value.ToString() == "پیشنهادی-جابجایی")
                    //    {
                    //        Existance = 5;
                    //    }

                    //    DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvSubEquip.Rows[i].Cells[6];
                    //    projectcode = Convert.ToInt32(cPCode.Value);

                    //    if (Convert.ToInt32(gvSubEquip.Rows[i].Cells[5].Value) == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                    //    {

                    //        //Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(new Guid(gvSubEquip.Rows[i].Cells[1].Value.ToString()));
                    //        Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(Convert.ToInt32(gvSubEquip.Rows[i].Cells[0].Value.ToString()));
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekExistance.Add(Existance);
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeeks.Add(jackPanelWeek);
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
                    //    }


                    //    if (Convert.ToInt32(gvSubEquip.Rows[i].Cells[5].Value) == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                    //    {
                    //        ed.WriteMessage("@Code{0}\n", gvSubEquip.Rows[i].Cells[0].Value);
                    //        ed.WriteMessage("@XCode{0}\n", gvSubEquip.Rows[i].Cells[1].Value);
                    //        Atend.Base.Equipment.ETransformer transformer = Atend.Base.Equipment.ETransformer.AccessSelectByCode(Convert.ToInt32(gvSubEquip.Rows[i].Cells[0].Value.ToString()));
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eTransformerExistance.Add(Existance);
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eTransformers.Add(transformer);
                    //        Atend.Base.Acad.AcadGlobal.AirPostData.eJackPanelWeekProjectCode.Add(projectcode);
                    //    }

                    //}
                }

                Atend.Base.Base.BEquipStatus status02 = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.AirPostData.Existance = status02.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.AirPostData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.AirPostData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

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
            ed.WriteMessage("Finish btnOK\n");

        }

        private void gvDisconnector_Click(object sender, EventArgs e)
        {
            bool Valid = false;
            DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX(); //Atend.Control.Common.StatuseCode.Copy();
            DataTable dt2 = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString())); //Atend.Base.Base.BProjectCode.AccessSelectAll();
            dtsub.Clear();
            Atend.Base.Equipment.EAirPost AirPost;
            if (gvDisconnector.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[4].Value) == true)
                {
                    AirPost = Atend.Base.Equipment.EAirPost.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                    for (int i = 0; i < Atend.Base.Equipment.EAirPost.nodeKeysEPackageX.Count; i++)
                    {
                        Valid = false;
                        int count = Convert.ToInt32(Atend.Base.Equipment.EAirPost.nodeCountEPackageX[i].ToString());
                        for (int j = 0; j < count; j++)
                        {
                            DataRow dr = dtsub.NewRow();
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
                            }
                            if (Valid == true)
                            {
                                dr["Name"] = name;
                                dr["IsSql"] = true;
                                dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                if (cboProjCode.Items.Count > 0)
                                {
                                    //MessageBox.Show(cboProjCode.SelectedValue.ToString());
                                    dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                }
                                else
                                {
                                    //dr["ProjectCode"] = 0;
                                    dr["ProjectCode"] = dtBWorkOrders.Rows[0]["ACode"];
                                }
                                dtsub.Rows.Add(dr);
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
                            DataRow dr = dtsub.NewRow();
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

                            }
                            if (Valid == true)
                            {
                                dr["Name"] = name;
                                dr["IsSql"] = false;
                                dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                                if (cboProjCode.Items.Count > 0)
                                {
                                    //MessageBox.Show(cboProjCode.SelectedValue.ToString());
                                    dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                                }
                                else
                                {
                                    //dr["ProjectCode"] = 0;
                                    dr["ProjectCode"] = dtBWorkOrders.Rows[0]["ACode"].ToString();
                                }
                                dtsub.Rows.Add(dr);
                            }
                        }
                    }
                }
                txtName.Text = AirPost.Name;
                txtSelectedCapacity.Text = AirPost.Capacity.ToString();
            }
            gvSubEquip.Refresh();
        }

        private void BindDataToGridSub()
        {
            DataColumn dcCode = new DataColumn("Code");
            dtsub.Columns.Add(dcCode);
            DataColumn dcXCode = new DataColumn("XCode");
            dtsub.Columns.Add(dcXCode);
            DataColumn dcName = new DataColumn("Name");
            dtsub.Columns.Add(dcName);
            DataColumn dcIsSql = new DataColumn("IsSql");
            dtsub.Columns.Add(dcIsSql);
            DataColumn dcCount = new DataColumn("Type", System.Type.GetType("System.Int32"));
            dtsub.Columns.Add(dcCount);
            DataColumn dcIsExistance = new DataColumn("IsExistance", System.Type.GetType("System.Int32"));
            dtsub.Columns.Add(dcIsExistance);
            DataColumn dcProjectCode = new DataColumn("ProjectCode", System.Type.GetType("System.Int32"));
            dtsub.Columns.Add(dcProjectCode);

            gvSubEquip.AutoGenerateColumns = false;
            gvSubEquip.DataSource = dtsub;
            AddProjectCodeColumn();
            BindDataToCboInGridView();


        }

        //Confirmed
        private void BindDataToGridMain()
        {

            dtMerge = Atend.Base.Equipment.EAirPost.SelectAllAndMerge();
            gvDisconnector.AutoGenerateColumns = false;
            gvDisconnector.DataSource = dtMerge;
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
            }
        }

        private void frmDrawDisconnector_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindDataToGridSub();
            BindDataToCboIsExist();
            SetDefaultValues();

        }

        //Confirmed
        public void ChangeColor()
        {
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[i].Cells["IsSql"].Value) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataToCboInGridView()
        {
            DataTable dt = Atend.Base.Base.BEquipStatus.SelectAllX();
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
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DisplayMember = "Name";
            c.ValueMember = "ACode";
            c.DataPropertyName = "ProjectCode";
            c.HeaderText = "شرح دستورکار";
            c.DataSource = dtBWorkOrders;
            c.Name = "ProjectCode";
            gvSubEquip.Columns.Insert(gvSubEquip.Columns.Count - 1, c);
        }

        //Confirmed
        public void BindDataToCboIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            DataTable dtstatus = Atend.Base.Base.BEquipStatus.SelectAllX();
            cboIsExist.DataSource = dtstatus;

            //if (Atend.Control.Common.IsExist == -1)
            //{
            DataRow[] drs = dtstatus.Select("IsDefault=True");
            if (drs.Length > 0)
            {
                cboIsExist.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
            }
            //}
            //else
            //{
            //    cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
            //}
        }

        //private void frmDrawDisconnector_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (!AllowToclose)
        //    {
        //        e.Cancel = true;
        //    }
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowToclose = true;
        }

        private void frmDrawAirPost02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToclose)
            {
                e.Cancel = true;
                //AllowToclose=
            }
        }

        //Confirmed
        private void cboProjCode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dtsub.Rows.Count > 0)
            {
                foreach (DataRow dr in dtsub.Rows)
                {
                    if (cboProjCode.SelectedValue != null)
                    {
                        dr["ProjectCode"] = Convert.ToInt32(cboProjCode.SelectedValue);
                    }
                }
            }
        }

        //Confirmed
        private void cboIsExist_SelectedValueChanged(object sender, EventArgs e)
        {

            if (cboIsExist.SelectedValue != null)
            {
                DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
                cboProjCode.DisplayMember = "Name";
                cboProjCode.ValueMember = "ACode";
                cboProjCode.DataSource = dtWorkOrder;

                if (dtsub.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtsub.Rows)
                    {
                        dr["IsExistance"] = Convert.ToInt32(cboIsExist.SelectedValue);
                    }
                }
            }
        }

    }
}