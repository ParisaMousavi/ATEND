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
    public partial class frmDrawPoleTip03 : Form
    {

        DataTable dtMergePoleTip = new DataTable();
        DataTable dtMergeConsol = new DataTable();
        public int shape;
        public int Type;
        public bool AllowClose = true;
        public DataTable dtgvPoleConsol = new DataTable();
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

        public frmDrawPoleTip03()
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

        //Confirmed
        private void SetDefaultValues()
        {
            if (Atend.Control.ConnectionString.StatusDef)
            {
                if (Atend.Control.Common.IsExist != -1)
                    cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
                if (Atend.Control.Common.ProjectCode != -1)
                {
                    cboProjCode.SelectedValue = Atend.Control.Common.ProjectCode;
                    cboHProjectCode.SelectedValue = Atend.Control.Common.ProjectCode;
                }

            }
        }

        //Confirmed
        private void BindDataToGridMain()
        {
            dtMergePoleTip = Atend.Base.Equipment.EPoleTip.SelectAllAndMerge();
            gvPoleTip.AutoGenerateColumns = false;
            gvPoleTip.DataSource = dtMergePoleTip;
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("+++ :{0}\n", dtMergePoleTip.Rows.Count);
            //BindDataTocboProjCode();

            //DataTable dt = Atend.Base.Equipment.EPoleTip.SelectAllAndMergeByConsolType(2);
            //MessageBox.Show(dt.Rows.Count.ToString());
            ChangeColor();
        }

        private void BindDataToGridConsol()
        {
            DataColumn dcXCode = new DataColumn("XCode");
            dtgvPoleConsol.Columns.Add(dcXCode);
            DataColumn dcCode = new DataColumn("Code");
            dtgvPoleConsol.Columns.Add(dcCode);
            DataColumn dcName = new DataColumn("Name");
            dtgvPoleConsol.Columns.Add(dcName);

            DataColumn dcCount = new DataColumn("Count");
            dtgvPoleConsol.Columns.Add(dcCount);
            DataColumn dcIsExistance = new DataColumn("IsExistance", System.Type.GetType("System.Int32"));
            dtgvPoleConsol.Columns.Add(dcIsExistance);
            DataColumn dcProjectCode = new DataColumn("ProjectCode", System.Type.GetType("System.Int32"));
            dtgvPoleConsol.Columns.Add(dcProjectCode);

            BindDataToCboInGridView();
            AddProjectCodeColumn();

            gvConsol.AutoGenerateColumns = false;
            gvConsol.DataSource = dtgvPoleConsol;
        }

        private void frmDrawPoleTip02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindDataToGridConsol();
            BindDataToComboBoxIsExist();
            cboType.SelectedIndex = 0;
            cboConsolType.SelectedIndex = 0;
            SetDefaultValues();
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvPoleTip.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvPoleTip.Rows[i].Cells["IsSql"].Value) == false)
                {
                    gvPoleTip.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }

        }

        //Confirmed
        public void BindDataToComboBoxIsExist()
        {

            DataTable dtstatus = Atend.Base.Base.BEquipStatus.SelectAllX();
            cboHIsExist.DisplayMember = "Name";
            cboHIsExist.ValueMember = "Code";
            cboHIsExist.DataSource = dtstatus.Copy();

            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = dtstatus.Copy();


            //if (Atend.Control.Common.IsExist == -1)
            //{
            DataRow[] drs = dtstatus.Select("IsDefault=True");
            if (drs.Length > 0)
            {
                //cboIsExist.SelectedIndex = i;
                cboIsExist.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
                cboHIsExist.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
            }
            //}
            //else
            //{
            //    cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
            //}

            //cboIsExist.DisplayMember = "Name";
            //cboIsExist.ValueMember = "Code";
            //cboIsExist.DataSource = Atend.Control.Common.StatuseCode;
            //cboIsExist.SelectedValue = Atend.Control.Common.StatuseCode.Rows[4]["Code"].ToString();
            //cboHIsExist.DisplayMember = "Name";
            //cboHIsExist.ValueMember = "Code";
            //cboHIsExist.DataSource = Atend.Control.Common.StatuseCode.Copy();
            //cboHIsExist.SelectedValue = Atend.Control.Common.StatuseCode.Copy().Rows[4]["Code"];
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
            gvConsol.Columns.Insert(gvConsol.Columns.Count - 1, c);

        }

        public void AddProjectCodeColumn()
        {
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dtWorkOrders;
            c.DisplayMember = "Name";
            c.ValueMember = "ACode";
            c.DataPropertyName = "ProjectCode";
            c.HeaderText = "شرح دستورکار";
            gvConsol.Columns.Insert(gvConsol.Columns.Count - 1, c);
        }

        //public void BindDataToComboBoxInGrid()
        //{
        //    DataTable dt = Atend.Control.Common.StatuseCode.Copy();
        //    DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvConsol.Columns["Column5"];
        //    c.DataSource = dt;
        //    c.DisplayMember = "Name";
        //    c.ValueMember = "Code";
        //}

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;

            //cboHProjectCode.DisplayMember = "Name";
            //cboHProjectCode.ValueMember = "Code";
            //cboHProjectCode.DataSource = dt.Copy();
        }

        //public void BindDataTocboProjCodeInGrid()
        //{
        //    DataTable dt = Atend.Base.Base.BProjectCode.SelectAll().Copy();
        //    DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvConsol.Columns["Column7"];
        //    c.DataSource = dt;
        //    c.DisplayMember = "Name";
        //    c.ValueMember = "Code";
        //}

        private bool Validation()
        {
            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}


            if (gvPoleTip.Rows.Count > 0 && gvPoleTip.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                gvPoleTip.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtHeight.Text))
            {
                MessageBox.Show("لطفا ارتفاع از سطح زمین را وارد نمایید", "خطا");
                txtHeight.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtHeight.Text))
            {
                MessageBox.Show("لطفا ارتفاع از سطح زمین را با فرمت مناسب وارد نمایید", "خطا");
                txtHeight.Focus();
                return false;
            }

            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView();
            dv.Table = dtMergePoleTip;//NO Filter
            gvPoleTip.AutoGenerateColumns = false;
            gvPoleTip.DataSource = dv;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Validation())
            {
                //Atend.Base.Base.BEquipStatus statusaaaa = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                gvPoleTip.Focus();
                bool useAccess = false;
                int projectcode = 0;
                Atend.Base.Equipment.EConsol eConsol = new Atend.Base.Equipment.EConsol();
                int Existance = 0;
                Atend.Base.Equipment.EPole ePole = new Atend.Base.Equipment.EPole();
                Atend.Base.Equipment.EPoleTip ePoletip = new Atend.Base.Equipment.EPoleTip();

                Atend.Base.Equipment.EHalter Halt = new Atend.Base.Equipment.EHalter();


                if (Convert.ToBoolean(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[2].Value) == true)
                {
                    ePoletip = Atend.Base.Equipment.EPoleTip.SelectByXCode(new Guid(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[0].Value.ToString()));
                    ePole = Atend.Base.Equipment.EPole.SelectByXCode(ePoletip.PoleXCode);
                    useAccess = false;
                    Halt = Atend.Base.Equipment.EHalter.SelectByXCode(ePoletip.HalterXID);
                }
                else
                {
                    //ed.WriteMessage("Access\n");
                    ePoletip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(Convert.ToInt32(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[1].Value.ToString()));
                    ePole = Atend.Base.Equipment.EPole.AccessSelectByCode(ePoletip.PoleCode);
                    useAccess = true;
                    Halt = Atend.Base.Equipment.EHalter.AccessSelectByCode(ePoletip.HalterID);
                }




                if (ePole.Shape == 0)
                {
                    shape = 0;

                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Clear();
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Clear();
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance.Clear();
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Clear();
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess.Clear();
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode.Clear();

                    Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo.HalterCount = ePoletip.HalterCount;
                    //Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo.HalterType = ePoletip.HalterID;
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo.Factor = ePoletip.Factor;
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.UseAccess = useAccess;

                    Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.Existance = status.ACode;
                    if (cboProjCode.Items.Count == 0)
                        Atend.Base.Acad.AcadGlobal.CirclePoleData.ProjectCode = 0;
                    else
                        Atend.Base.Acad.AcadGlobal.CirclePoleData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                    Atend.Base.Acad.AcadGlobal.CirclePoleData.ePole = ePole;
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.ePoleTip = ePoletip;
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.Height = Convert.ToDouble(txtHeight.Text);

                    Atend.Base.Base.BEquipStatus statusHalteer = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboHIsExist.SelectedValue));
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterExistance = statusHalteer.ACode;
                    if (cboHProjectCode.Items.Count == 0)
                        Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterProjectCode = 0;
                    else
                        Atend.Base.Acad.AcadGlobal.CirclePoleData.HalterProjectCode = Convert.ToInt32(cboHProjectCode.SelectedValue);

                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalter = Halt;
                    Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalterCount = ePoletip.HalterCount;

                    //for (int i = 0; i < gvConsol.Rows.Count; i++)
                    foreach (DataRow dr in dtgvPoleConsol.Rows)
                    {
                        if (Convert.ToInt32(dr["Count"].ToString()) != 0)
                        {
                            //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[3];
                            Atend.Base.Base.BEquipStatus statusPole = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                            Existance = statusPole.ACode;
                            if (dr["ProjectCode"].ToString() == "")
                                projectcode = 0;
                            else
                                projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                            //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[5];
                            if (!useAccess)
                            {
                                eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(dr["XCode"].ToString()));
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Add(eConsol);
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess.Add(false);
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance.Add(Existance);
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode.Add(projectcode);
                            }
                            else
                            {
                                eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Add(eConsol);
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess.Add(true);
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance.Add(Existance);
                                Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode.Add(projectcode);

                            }

                        }
                    }

                }
                else
                {
                    shape = 1;
                    //ed.WriteMessage("*******EPOLe.Type={0}\n", ePole.Type);

                    if (ePole.Type == 2)
                    {
                        //ed.WriteMessage("Type=2\n");
                        Type = 2;
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess.Clear();
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode.Clear();

                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo.HalterCount = ePoletip.HalterCount;
                        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo.HalterType = ePoletip.HalterID;
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo.Factor = ePoletip.Factor;

                        Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.Existance = status.ACode;
                        if (cboProjCode.Items.Count == 0)
                            Atend.Base.Acad.AcadGlobal.PolygonPoleData.ProjectCode = 0;
                        else
                            Atend.Base.Acad.AcadGlobal.PolygonPoleData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.UseAccess = useAccess;
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.Height = Convert.ToDouble(txtHeight.Text);

                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePole = ePole;
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.ePoleTip = ePoletip;

                        Atend.Base.Base.BEquipStatus statusHalteer = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboHIsExist.SelectedValue));
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterExistance = statusHalteer.ACode;
                        if (cboHProjectCode.Items.Count == 0)
                            Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterProjectCode = 0;
                        else
                            Atend.Base.Acad.AcadGlobal.PolygonPoleData.HalterProjectCode = Convert.ToInt32(cboHProjectCode.SelectedValue);

                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalter = Halt;
                        Atend.Base.Acad.AcadGlobal.PolygonPoleData.eHalterCount = ePoletip.HalterCount;

                        //for (int i = 0; i < gvConsol.Rows.Count; i++)
                        foreach (DataRow dr in dtgvPoleConsol.Rows)
                        {
                            if (Convert.ToInt32(dr["Count"].ToString()) != 0)
                            {
                                //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[3];

                                Atend.Base.Base.BEquipStatus statusPole = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                                Existance = statusPole.ACode;
                                if (dr["ProjectCode"].ToString() == "")
                                    projectcode = 0;
                                else
                                    projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                                //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[5];

                                if (!useAccess)
                                {
                                    eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                        dr["XCode"].ToString()));
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Add(eConsol);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess.Add(false);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                                    //ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance.Add(Existance);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode.Add(projectcode);
                                }
                                else
                                {
                                    eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                                       dr["Code"].ToString()));
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Add(eConsol);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess.Add(true);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                                    //ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance.Add(Existance);
                                    Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode.Add(projectcode);
                                }
                            }
                        }
                        //ed.WriteMessage("Consol.Count={0}\n", Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Count);

                    }
                    else   //if (ePole.Type == 3)
                    {
                        Type = 3;
                        //ed.WriteMessage("Type=3\n");
                        Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Clear();
                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Clear();
                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Clear();
                        Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Clear();
                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Clear();
                        Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Clear();

                        Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterCount = ePoletip.HalterCount;
                        //Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterType = ePoletip.HalterID;
                        Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.Factor = ePoletip.Factor;

                        Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                        Atend.Base.Acad.AcadGlobal.PoleData.Existance = status.ACode;
                        if (cboProjCode.Items.Count == 0)
                            Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode = 0;
                        else
                            Atend.Base.Acad.AcadGlobal.PoleData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                        Atend.Base.Acad.AcadGlobal.PoleData.UseAccess = useAccess;
                        Atend.Base.Acad.AcadGlobal.PoleData.ePoleTip = ePoletip;
                        Atend.Base.Acad.AcadGlobal.PoleData.ePole = ePole;
                        Atend.Base.Acad.AcadGlobal.PoleData.Height = Convert.ToDouble(txtHeight.Text);

                        Atend.Base.Base.BEquipStatus statusHalteer = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboHIsExist.SelectedValue));
                        Atend.Base.Acad.AcadGlobal.PoleData.HalterExistance = statusHalteer.ACode;
                        if (cboHProjectCode.Items.Count == 0)
                            Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode = 0;
                        else
                            Atend.Base.Acad.AcadGlobal.PoleData.HalterProjectCode = Convert.ToInt32(cboHProjectCode.SelectedValue);

                        Atend.Base.Acad.AcadGlobal.PoleData.eHalter = Halt;
                        Atend.Base.Acad.AcadGlobal.PoleData.eHalterCount = ePoletip.HalterCount;

                        //for (int i = 0; i < gvConsol.Rows.Count; i++)
                        foreach (DataRow dr in dtgvPoleConsol.Rows)
                        {
                            if (Convert.ToInt32(dr["Count"].ToString()) != 0)
                            {
                                //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[3];
                                Atend.Base.Base.BEquipStatus statusPole = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                                Existance = statusPole.ACode;
                                if (dr["ProjectCode"].ToString() == "")
                                    projectcode = 0;
                                else
                                    projectcode = Convert.ToInt32(dr["ProjectCode"].ToString());

                                //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[5];
                                if (!useAccess)
                                {
                                    eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(dr["XCode"].ToString()));
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Add(eConsol);
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Add(false);
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                                    //ed.WriteMessage("eConsolExistance={0}\n", Existance);
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Add(Existance);
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Add(projectcode);

                                }
                                else
                                {
                                    eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()));
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Add(eConsol);
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Add(true);
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Add(Existance);
                                    Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Add(projectcode);
                                }
                            }
                        }
                        //ed.WriteMessage("Consol.Count={0}\n", Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Count);
                    }

                }

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                if (cboProjCode.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;

                AllowClose = true;

            }
            else
            {
                AllowClose = false;
            }
        }

        private void gvPoleTip_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //MessageBox.Show(dtMergePoleTip.Rows[gvPoleTip.CurrentRow.Index]["Height"].ToString());
            dtgvPoleConsol.Rows.Clear();
            DataTable dtExistance = Atend.Base.Base.BEquipStatus.SelectAllX();
            DataTable dtProjectCode = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString())); //Atend.Base.Base.BProjectCode.AccessSelectAll();
            Atend.Base.Equipment.EPoleTip poleTip = new Atend.Base.Equipment.EPoleTip();
            if (gvPoleTip.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells["IsSql"].Value) == true)
                {
                    poleTip = Atend.Base.Equipment.EPoleTip.SelectByXCode(new Guid(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[0].Value.ToString()));
                    Atend.Base.Equipment.EHalter halter = Atend.Base.Equipment.EHalter.SelectByXCode(poleTip.HalterXID);
                    if (halter.Code != -1)
                    {
                        txtHalter.Text = halter.Name;
                        txtHalterCount.Text = poleTip.HalterCount.ToString();
                    }
                    Byte[] byteBLOBData = new Byte[0];
                    byteBLOBData = (Byte[])(poleTip.Image);
                    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                    pictureBox1.Image = Image.FromStream(stmBLOBData);

                    for (int i = 0; i < Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX.Count; i++)
                    {
                        //int count = Convert.ToInt32(Atend.Base.Equipment.EPoleTip.nodeCountEPackageX[i].ToString());
                        DataRow dr = dtgvPoleConsol.NewRow();
                        dr["XCode"] = Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX[i];
                        Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX[i].ToString()));
                        dr["Name"] = Consol.Name;
                        dr["Count"] = Atend.Base.Equipment.EPoleTip.nodeCountEPackageX[i].ToString();

                        dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                        if (cboProjCode.Items.Count > 0)
                        {
                            dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                        }
                        else
                        {
                            dr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"];
                        }
                        //dr["IsExistance"] = Convert.ToInt32(dtExistance.Rows[4]["Code"]);
                        //dr["ProjectCode"] = Convert.ToInt32(dtProjectCode.Rows[0]["Code"]);
                        dr["Code"] = Guid.Empty;
                        dtgvPoleConsol.Rows.Add(dr);
                    }

                }
                else
                {
                    //ed.WriteMessage("Code={0}\n", gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[1].Value.ToString());
                    poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(Convert.ToInt32(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[1].Value.ToString()));
                    Atend.Base.Equipment.EHalter halter = Atend.Base.Equipment.EHalter.AccessSelectByCode(poleTip.HalterID);
                    if (halter.Code != -1)
                    {
                        txtHalter.Text = halter.Name;
                        txtHalterCount.Text = poleTip.HalterCount.ToString();
                    }
                    Byte[] byteBLOBData = new Byte[0];
                    byteBLOBData = (Byte[])(poleTip.Image);
                    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                    pictureBox1.Image = Image.FromStream(stmBLOBData);
                    for (int i = 0; i < Atend.Base.Equipment.EPoleTip.nodeKeysEPackage.Count; i++)
                    {
                        //int count = Convert.ToInt32(Atend.Base.Equipment.EPoleTip.nodeCountEPackage[i].ToString());
                        DataRow dr = dtgvPoleConsol.NewRow();
                        dr["Code"] = Atend.Base.Equipment.EPoleTip.nodeKeysEPackage[i];
                        string name = "";
                        //ed.WriteMessage("EPoleTip.nodeKeysEPackage[i]={0}\n", Atend.Base.Equipment.EPoleTip.nodeKeysEPackage[i]);
                        Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EPoleTip.nodeKeysEPackage[i].ToString()));
                        dr["Name"] = Consol.Name;
                        dr["Count"] = Atend.Base.Equipment.EPoleTip.nodeCountEPackage[i].ToString();

                        dr["IsExistance"] = cboIsExist.SelectedValue.ToString();
                        if (cboProjCode.Items.Count > 0)
                        {
                            dr["ProjectCode"] = cboProjCode.SelectedValue.ToString();
                        }
                        else
                        {
                            dr["ProjectCode"] = dtWorkOrders.Rows[0]["ACode"];
                        }
                        //dr["IsExistance"] = Convert.ToInt32(dtExistance.Rows[4]["Code"]);
                        //dr["ProjectCode"] = Convert.ToInt32(dtProjectCode.Rows[0]["Code"]);
                        //dr["Code"] = Guid.Empty;
                        dtgvPoleConsol.Rows.Add(dr);
                    }
                }
            }
        }

        private void cboProjCode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboProjCode.SelectedValue != nudHeight)
            {
                //cboHProjectCode.SelectedValue = cboProjCode.SelectedValue;
                if (dtgvPoleConsol.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtgvPoleConsol.Rows)
                    {
                        dr["ProjectCode"] = Convert.ToInt32(cboProjCode.SelectedValue);
                    }
                }
            }
        }

        private void cboIsExist_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboIsExist.SelectedValue != null)
            {
                cboHIsExist.SelectedValue = cboIsExist.SelectedValue;
                if (dtgvPoleConsol.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtgvPoleConsol.Rows)
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

        private void frmDrawPoleTip03_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
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
                type = Convert.ToInt32(cboType.SelectedIndex);
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
            if (chkConsolType.Checked)
            {
                check = true;
            }

            if (check)
            {
                DataView dv = new DataView();
                dv.Table = dtMergePoleTip;
                dv.RowFilter = strFilter;

                if (chkConsolType.Checked)
                {
                    DataTable dtTemp = dv.Table;
                    DataTable dt2 = dtMergePoleTip.Copy();
                    dt2.Rows.Clear();
                    DataTable dtPoleTipWithConsolType = Atend.Base.Equipment.EPoleTip.SelectAllAndMergeByConsolType(cboConsolType.SelectedIndex);

                    //Atend.Calculating.frmTest _frmTest = new Atend.Calculating.frmTest();
                    //_frmTest.dataGridView4.DataSource = dtTemp;
                    //_frmTest.dataGridView5.DataSource = dtPoleTipWithConsolType;
                    //_frmTest.ShowDialog();

                    for (int i = 0; i < dtPoleTipWithConsolType.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtPoleTipWithConsolType.Rows[i]["IsSql"]) == true)
                        {
                            //foreach (DataRow dr in dv.Table.Rows)
                            foreach (DataRow dr in dtTemp.Rows)
                            {
                                if (new Guid(dtPoleTipWithConsolType.Rows[i]["XCode"].ToString()) == new Guid(dr["XCode"].ToString()))
                                {
                                    DataRow d = dt2.NewRow();
                                    d["XCode"] = dr["XCode"];
                                    d["Code"] = dr["Code"];
                                    d["Name"] = dr["Name"];
                                    d["Comment"] = dr["Comment"];
                                    d["HalterCount"] = dr["HalterCount"];
                                    d["Factor"] = dr["Factor"];
                                    d["PoleXCode"] = dr["PoleXCode"];
                                    d["IsDefault"] = dr["IsDefault"];
                                    d["Power"] = dr["Power"];
                                    d["Height"] = dr["Height"];
                                    d["Type"] = dr["Type"];
                                    dt2.Rows.Add(d);
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow dr in dtTemp.Rows)
                            {
                                if (Convert.ToInt32(dtPoleTipWithConsolType.Rows[i]["Code"].ToString()) == Convert.ToInt32(dr["Code"].ToString()))
                                {
                                    DataRow d = dt2.NewRow();
                                    d["XCode"] = dr["XCode"];
                                    d["Code"] = dr["Code"];
                                    d["Name"] = dr["Name"];
                                    d["Comment"] = dr["Comment"];
                                    d["HalterCount"] = dr["HalterCount"];
                                    d["Factor"] = dr["Factor"];
                                    d["PoleXCode"] = dr["PoleXCode"];
                                    d["IsDefault"] = dr["IsDefault"];
                                    d["Power"] = dr["Power"];
                                    d["Height"] = dr["Height"];
                                    d["Type"] = dr["Type"];
                                    d["IsSql"] = false;
                                    dt2.Rows.Add(d);
                                }
                            }
                        }
                    }

                    gvPoleTip.AutoGenerateColumns = false;
                    gvPoleTip.DataSource = dt2;
                }
                else
                {
                    gvPoleTip.AutoGenerateColumns = false;
                    gvPoleTip.DataSource = dv;
                }

            }
            else
            {
                gvPoleTip.AutoGenerateColumns = false;
                gvPoleTip.DataSource = dtMergePoleTip;
            }
            ChangeColor();
        }

        private void cboHIsExist_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboHIsExist.SelectedValue != null)
            {
                DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboHIsExist.SelectedValue.ToString()));
                cboHProjectCode.DisplayMember = "Name";
                cboHProjectCode.ValueMember = "ACode";
                cboHProjectCode.DataSource = dtWorkOrder;
            }

        }
    }
}