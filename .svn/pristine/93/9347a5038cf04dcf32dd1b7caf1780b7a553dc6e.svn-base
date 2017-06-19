using System;
using System.Collections.Generic;

using System.Collections;
using System.ComponentModel;
using System.Data;
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
    public partial class frmEditDrawPoleTip02 : Form
    {
        System.Data.DataTable dtMergePoleTip = new System.Data.DataTable();
        System.Data.DataTable dtMergeConsol = new System.Data.DataTable();
        System.Data.DataTable dtgvPoleConsol = new System.Data.DataTable();
        System.Data.DataTable dtCurrent = new System.Data.DataTable();
        System.Data.DataTable dtLastConsol = new System.Data.DataTable();
        System.Data.DataTable dtHalt = new System.Data.DataTable();
        public Guid NodeCode;
        public ObjectId objID;
        int selectedProductCode = -1;


        public int shape;
        public int Type;
        public bool AllowClose = true;
        bool ForceToClose = false;



        public frmEditDrawPoleTip02()
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

        private void frmDrawPoleTip02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToComboBoxIsExist();
            BindDataToCboInGridView();
            AddProjectCodeColumn();

            dtMergePoleTip = Atend.Base.Equipment.EPoleTip.SelectAllAndMerge();
            Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByNodeCode(NodeCode);
            selectedProductCode = dPack.ProductCode;

            gvPoleTip.AutoGenerateColumns = false;
            gvPoleTip.DataSource = dtMergePoleTip;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMergePoleTip, gvPoleTip, this);
            ChangeColor();
            for (int i = 0; i < gvPoleTip.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvPoleTip.Rows[i].Cells[1].Value) == dPack.ProductCode && Convert.ToBoolean(gvPoleTip.Rows[i].Cells[2].Value) == false)
                {
                    gvPoleTip.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    //gvPoleTip.Rows[i].Selected = true;
                }
            }

            Atend.Base.Design.DNode node = Atend.Base.Design.DNode.AccessSelectByCode(NodeCode);
            txtHeight.Text = Convert.ToString(Math.Round(node.Height,2));

            dtgvPoleConsol = Atend.Base.Design.DPackage.AccessSelectByParentCodeForConsol(dPack.Code, (int)Atend.Control.Enum.ProductType.Consol);
            
            dtHalt = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dPack.Code,Convert.ToInt32(Atend.Control.Enum.ProductType.Halter));
            if (dtHalt.Rows.Count > 0)
            {
                Atend.Base.Equipment.EHalter Halter = Atend.Base.Equipment.EHalter.AccessSelectByCode(Convert.ToInt32(dtHalt.Rows[0]["ProductCode"].ToString()));
                txtHalter.Text = Halter.Name;
            }

            System.Data.DataColumn dcIsSql = new System.Data.DataColumn("IsSql");
            dtgvPoleConsol.Columns.Add(dcIsSql);

            System.Data.DataColumn dcAccessCode = new System.Data.DataColumn("AccessCode");
            dtgvPoleConsol.Columns.Add(dcAccessCode);

            System.Data.DataColumn dcXCode = new System.Data.DataColumn("XCode");
            dtgvPoleConsol.Columns.Add(dcXCode);

            foreach (DataRow dr in dtgvPoleConsol.Rows)
            {
                dr["IsExistance"] = Atend.Base.Base.BEquipStatus.SelectByACode(Convert.ToInt32(dr["IsExistance"].ToString())).Code;
                dr["IsSql"] = false;
                dr["XCode"] = "_";
                dr["AccessCode"] = dr["ProductCode"].ToString();
            }


            gvConsol.AutoGenerateColumns = false;
            gvConsol.DataSource = dtgvPoleConsol;

            dtLastConsol = dtgvPoleConsol.Copy();

            //for (int i = 0; i < gvPoleTip.Rows.Count; i++)
            //{
            //    gvPoleTip.Rows[i].Selected = false;
            //}

            Atend.Base.Equipment.EPoleTip EPolT = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(dPack.ProductCode);
            if (EPolT.Code != -1)
            {
                txtHalterCount.Text = EPolT.HalterCount.ToString();
            }

            

            //BindDataTocboProjCode();

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dPack.IsExistance).Code;
            cboProjCode.SelectedValue = dPack.ProjectCode;

            if (dtHalt.Rows.Count > 0)
            {
                cboHIsExistance.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(Convert.ToInt32(dtHalt.Rows[0]["IsExistance"].ToString())).Code;
                cboHProjectCode.SelectedValue = Convert.ToInt32(dtHalt.Rows[0]["ProjectCode"].ToString());
            }
            dtCurrent = dtgvPoleConsol.Copy();

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
            gvConsol.Columns.Insert(gvConsol.Columns.Count - 1, c);

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
            gvConsol.Columns.Insert(gvConsol.Columns.Count - 1, c);
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvPoleTip.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvPoleTip.Rows[i].Cells[2].Value) == false)
                {
                    gvPoleTip.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
            //cboIsExist.SelectedValue = Atend.Control.Common.StatuseCode.Rows[4]["Code"].ToString();

            cboHIsExistance.DisplayMember = "Name";
            cboHIsExistance.ValueMember = "Code";
            cboHIsExistance.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
            //cboHIsExistance.SelectedValue = Atend.Control.Common.StatuseCode.Rows[4]["Code"].ToString();
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
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
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
            btnOk.Focus();
            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtHeight.Text))
            {
                MessageBox.Show("لطفا ارتفاع پایه از سطح زمین را وارد نمایید", "خطا");
                txtHeight.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtHeight.Text))
            {
                MessageBox.Show("لطفا ارتفاع پایه از سطح زمین را با فرمت مناسب وارد نمایید","خطا");
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
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Validation())
            {
                bool useAccess = false;
                int projectcode = 0;

                List<Atend.Base.Equipment.EConsol> list = new List<Atend.Base.Equipment.EConsol>();
                ArrayList consoluse = new ArrayList();
                ArrayList consolexis = new ArrayList();
                ArrayList consolcount = new ArrayList();
                ArrayList ConsolProjCode = new ArrayList();
                ArrayList DelConsol = new ArrayList();

                Atend.Base.Equipment.EConsol eConsol = new Atend.Base.Equipment.EConsol();
                int Existance = 0;
                Atend.Base.Equipment.EPole ePole = new Atend.Base.Equipment.EPole();
                Atend.Base.Equipment.EPoleTip ePoletip = new Atend.Base.Equipment.EPoleTip();
                //ed.WriteMessage("IsSqlSelected={0}\n", gvPoleTip.SelectedRows[0].Cells[2].Value.ToString());
                Atend.Base.Equipment.EHalter halt = new Atend.Base.Equipment.EHalter();
                
                if (Convert.ToBoolean(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[2].Value) == true)
                {
                    ePoletip = Atend.Base.Equipment.EPoleTip.SelectByXCode(new Guid(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[0].Value.ToString()));
                    ePole = Atend.Base.Equipment.EPole.SelectByXCode(ePoletip.PoleXCode);
                    useAccess = false;
                    if (ePoletip.HalterCount != 0)
                        halt = Atend.Base.Equipment.EHalter.SelectByXCode(ePoletip.HalterXID);
                }
                else
                {
                    ePoletip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(Convert.ToInt32(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[1].Value.ToString()));//gvPoleTip.SelectedRows[0].Cells[1].Value
                    ePole = Atend.Base.Equipment.EPole.AccessSelectByCode(ePoletip.PoleCode);
                    useAccess = true;
                    if (ePoletip.HalterCount != 0)
                        halt = Atend.Base.Equipment.EHalter.AccessSelectByCode(ePoletip.HalterID);
                }

                foreach (DataRow dr in dtLastConsol.Rows)
                {
                    DelConsol.Add(dr["Code"].ToString());
                }



                //if (ePole.Shape == 0)
                //{
                //    ed.WriteMessage("**********I AM IN Shape=0\n");
                //    shape = 0;
                //    Atend.Global.Acad.DrawEquips.AcDrawCirclePole ACDCP = new Atend.Global.Acad.DrawEquips.AcDrawCirclePole();
                //    ACDCP.ePole = ePole;
                //    ACDCP.ePoleTip = ePoletip;
                //    ACDCP.UseAccess = useAccess;
                //    ACDCP.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);
                //    ACDCP.Existance = Convert.ToByte(cboIsExist.SelectedValue);

                //    ACDCP.HalterExistance = Convert.ToByte(cboHIsExistance.SelectedValue);
                //    ACDCP.HalterProjectCode = Convert.ToInt32(cboHProjectCode.SelectedValue);
                //    ACDCP.Height = Convert.ToInt32(txtHeight.Text);
                //    ACDCP.eHalter = halt;
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Clear();
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Clear();
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance.Clear();
                //    ed.WriteMessage("oo\n");
                //    ACDCP.eConsols.Clear();
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess.Clear();
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode.Clear();

                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo.HalterCount = ePoletip.HalterCount;
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo.HalterType = ePoletip.HalterID;
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo.Factor = ePoletip.Factor;
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.Existance = Convert.ToByte(cboIsExist.SelectedValue);
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.UseAccess = useAccess;
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.ePole = ePole;
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.ePoleTip = ePoletip;
                //    //Atend.Base.Acad.AcadGlobal.CirclePoleData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                //    foreach (DataRow dr in dtgvPoleConsol.Rows)
                //    {
                //        if (Convert.ToInt32(dr["Count"].ToString()) != 0)
                //        {
                //            //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[3];
                //            //Existance = Convert.ToInt32(c.Value);

                //            //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[5];
                //            //projectcode = Convert.ToInt32(cPCode.Value);
                //            if (Convert.ToBoolean(dr["IsSql"].ToString()))
                //            {
                //                ed.WriteMessage("LocalConsolXCODE={0}\n", dr["XCode"].ToString());
                //                eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                //                    dr["XCode"].ToString()));
                //                ed.WriteMessage("Consol.NAme={0}\n", eConsol.Name);
                //                ACDCP.eConsols.Add(eConsol);
                //                ed.WriteMessage("HH\n");
                //                ACDCP.eConsolUseAccess.Add(false);
                //                ed.WriteMessage("AA\n");
                //                ACDCP.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                //                ed.WriteMessage("Ab\n");

                //                ACDCP.eConsolExistance.Add(Convert.ToInt32(dr["IsExistance"].ToString()));
                //                ed.WriteMessage("Ac\n");

                //                ACDCP.eConsolProjectCode.Add(Convert.ToInt32(dr["ProjectCode"].ToString()));
                //            }
                //            else
                //            {
                //                ed.WriteMessage("Access Consol\n");
                //                eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                //                   dr["AccessCode"].ToString()));
                //                ACDCP.eConsols.Add(eConsol);
                //                ACDCP.eConsolUseAccess.Add(true);
                //                ACDCP.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                //                ed.WriteMessage("eConsolExistance={0}\n", Existance);
                //                ACDCP.eConsolExistance.Add(Convert.ToInt32(dr["IsExistance"].ToString()));
                //                ACDCP.eConsolProjectCode.Add(Convert.ToInt32(dr["ProjectCode"].ToString()));

                //            }

                //        }
                //    }
                //    ed.WriteMessage("Consol.Count={0}\n", Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Count);

                //    if (ACDCP.UpdatePoleDataTip(objID, DelConsol, NodeCode))
                //    {
                //        AllowClose = true;
                //        return;
                //    }
                //}
                //else
                //{
                //    shape = 1;
                //    ed.WriteMessage("TYPE={0}\n", ePole.Type);
                //    if (ePole.Type == 2)
                //    {
                //        Atend.Global.Acad.DrawEquips.AcDrawPolygonPole ACDP = new Atend.Global.Acad.DrawEquips.AcDrawPolygonPole();
                //        ed.WriteMessage("***************Type=2\n");
                //        Type = 2;
                //        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Clear();
                //        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolCount.Clear();
                //        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolExistance.Clear();
                //        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Clear();
                //        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolUseAccess.Clear();
                //        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsolProjectCode.Clear();

                //        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo.HalterCount = ePoletip.HalterCount;
                //        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo.HalterType = ePoletip.HalterID;
                //        //Atend.Base.Acad.AcadGlobal.PolygonPoleData.dPoleInfo.Factor = ePoletip.Factor;
                //        ACDP.Existance = Convert.ToByte(cboIsExist.SelectedValue);
                //        ACDP.UseAccess = useAccess;
                //        ACDP.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                //        ACDP.ePole = ePole;
                //        ACDP.ePoleTip = ePoletip;


                //        ACDP.HalterExistance = Convert.ToByte(cboHIsExistance.SelectedValue);
                //        ACDP.HalterProjectCode = Convert.ToInt32(cboHProjectCode.SelectedValue);
                //        ACDP.Height = Convert.ToInt32(txtHeight.Text);
                //        ACDP.eHalter = halt;
                //        foreach (DataRow dr in dtgvPoleConsol.Rows)
                //        {
                //            if (Convert.ToInt32(dr["Count"].ToString()) != 0)
                //            {
                //                //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[3];


                //                //Existance = Convert.ToInt32(c.Value);
                //                //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[5];
                //                //projectcode = Convert.ToInt32(cPCode.Value);
                //                ed.WriteMessage("***IsSQL={0}\n",dr["IsSql"].ToString());
                //                if (Convert.ToBoolean(dr["IsSql"].ToString()))
                //                {
                //                    eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                //                       dr["XCode"].ToString()));
                //                    ACDP.eConsols.Add(eConsol);
                //                    ACDP.eConsolUseAccess.Add(false);
                //                    ACDP.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                //                    ed.WriteMessage("eConsolExistance={0}\n", Existance);
                //                    ACDP.eConsolExistance.Add(Convert.ToInt32(dr["IsExistance"].ToString()));
                //                    ACDP.eConsolProjectCode.Add(Convert.ToInt32(dr["ProjectCode"].ToString()));
                //                }
                //                else
                //                {
                //                    eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                //                       dr["AccessCode"].ToString()));
                //                    ACDP.eConsols.Add(eConsol);
                //                    ACDP.eConsolUseAccess.Add(true);
                //                    ACDP.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                //                    ed.WriteMessage("eConsolExistance={0}\n", Existance);
                //                    ACDP.eConsolExistance.Add(Convert.ToInt32(dr["IsExistance"].ToString()));
                //                    ACDP.eConsolProjectCode.Add(Convert.ToInt32(dr["ProjectCode"].ToString()));
                //                }
                //            }
                //        }
                //        if (ACDP.UpdatePoleDataTip(objID, DelConsol, NodeCode))
                //        {
                //            AllowClose = true;
                //            return;
                //        }
                //        //ed.WriteMessage("Consol.Count={0}\n", Atend.Base.Acad.AcadGlobal.PolygonPoleData.eConsols.Count);

                //    }
                //    //if (ePole.Type == 3)
                //    //{
                //Type = 3;
                Atend.Global.Acad.DrawEquips.AcDrawPole ADP = new Atend.Global.Acad.DrawEquips.AcDrawPole();
                //Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Clear();
                //Atend.Base.Acad.AcadGlobal.PoleData.eConsolCount.Clear();
                //Atend.Base.Acad.AcadGlobal.PoleData.eConsolExistance.Clear();
                //Atend.Base.Acad.AcadGlobal.PoleData.eConsols.Clear();
                //Atend.Base.Acad.AcadGlobal.PoleData.eConsolUseAccess.Clear();
                //Atend.Base.Acad.AcadGlobal.PoleData.eConsolProjectCode.Clear();

                //Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterCount = ePoletip.HalterCount;
                //Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterType = ePoletip.HalterID;
                //Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.Factor = ePoletip.Factor;
                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
                ADP.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    ADP.ProjectCode = 0;
                else
                    ADP.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);
                ADP.UseAccess = useAccess;
                ADP.ePoleTip = ePoletip;
                ADP.ePole = ePole;


                Atend.Base.Base.BEquipStatus statusHalter = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboHIsExistance.SelectedValue.ToString()));
                ADP.HalterExist = statusHalter.ACode;

                if (cboHProjectCode.Items.Count == 0)
                    ADP.HalterProjectCode = 0;
                else
                    ADP.HalterProjectCode = Convert.ToInt32(cboHProjectCode.SelectedValue);

                ADP.Height = Convert.ToInt32(txtHeight.Text);
                ADP.eHalter = halt;
                ADP.eHalterCount = Convert.ToInt32(txtHalterCount.Text);

                foreach (DataRow dr in dtgvPoleConsol.Rows)
                {
                    if (Convert.ToInt32(dr["Count"].ToString()) != 0)
                    {
                        //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[3];

                        //Existance = Convert.ToInt32(c.Value);
                        //DataGridViewComboBoxCell cPCode = (DataGridViewComboBoxCell)gvConsol.Rows[i].Cells[5];
                        //projectcode = Convert.ToInt32(cPCode.Value);
                        if (Convert.ToBoolean(dr["IsSql"].ToString()))
                        {
                            eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                                dr["XCode"].ToString()));
                            ADP.eConsols.Add(eConsol);
                            ADP.eConsolUseAccess.Add(false);
                            ADP.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                            
                            Atend.Base.Base.BEquipStatus status2 = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                            ADP.eConsolExistance.Add(status2.ACode);

                            if (dr["ProjectCode"].ToString() == "")
                                ADP.eConsolProjectCode.Add(0);
                            else
                                ADP.eConsolProjectCode.Add(Convert.ToInt32(dr["ProjectCode"].ToString()));

                        }
                        else
                        {
                            eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                               dr["AccessCode"].ToString()));
                            ADP.eConsols.Add(eConsol);
                            ADP.eConsolUseAccess.Add(true);
                            ADP.eConsolCount.Add(Convert.ToInt32(dr["Count"].ToString()));
                            
                            Atend.Base.Base.BEquipStatus status2 = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(dr["IsExistance"].ToString()));
                            ADP.eConsolExistance.Add(status2.ACode);

                            if (dr["ProjectCode"].ToString() == "")
                                ADP.eConsolProjectCode.Add(0);
                            else
                                ADP.eConsolProjectCode.Add(Convert.ToInt32(dr["ProjectCode"].ToString()));
                        }
                    }
                }
                if (ADP.UpdatePoleDataTip(objID, DelConsol, NodeCode))
                {
                    AllowClose = true;
                    return;
                }

                //}


                //}

                //AllowClose = true;

            }
            else
                AllowClose = false;
        }

        private void gvPoleTip_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            dtgvPoleConsol.Rows.Clear();
            Atend.Base.Equipment.EPoleTip poleTip = new Atend.Base.Equipment.EPoleTip();
            System.Data.DataTable dtExistance = Atend.Base.Base.BEquipStatus.SelectAllX(); //Atend.Control.Common.StatuseCode;
            System.Data.DataTable dtProjectCode = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString())); //Atend.Base.Base.BProjectCode.AccessSelectAll();

            
            if (gvPoleTip.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[2].Value) == true)
                {
                    poleTip = Atend.Base.Equipment.EPoleTip.SelectByXCode(new Guid(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[0].Value.ToString()));
                    //poleTip = Atend.Base.Equipment.EPoleTip.SelectByXCode(new Guid(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[0].Value.ToString()));
                    Atend.Base.Equipment.EHalter halter = Atend.Base.Equipment.EHalter.SelectByXCode(poleTip.HalterXID);
                    if (halter.Code != -1)
                    {
                        txtHalter.Text = halter.Name;
                        txtHalterCount.Text = poleTip.HalterCount.ToString();
                    }
                    
                    dtgvPoleConsol.Rows.Clear();
                    for (int i = 0; i < Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX.Count; i++)
                    {
                        Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX[i].ToString()));
                        DataRow dr = dtgvPoleConsol.NewRow();
                        dr["Code"] = Guid.Empty;
                        dr["XCode"] = Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX[i];
                        dr["AccessCode"] = "_";
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
                        //dr["IsExistance"] = dtExistance.Rows[4]["Code"].ToString();
                        //dr["ProjectCode"] = dtProjectCode.Rows[0]["Code"].ToString();
                        dr["Name"] = consol.Name;
                        dr["Count"] = "1";
                        dtgvPoleConsol.Rows.Add(dr);
                        //gvConsol.Rows[gvConsol.Rows.Count - 1].Cells[0].Value = Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX[i];
                        //Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX[i].ToString()));
                        //gvConsol.Rows[gvConsol.Rows.Count - 1].Cells[2].Value = Consol.Name;
                        //gvConsol.Rows[gvConsol.Rows.Count - 1].Cells[4].Value = Atend.Base.Equipment.EPoleTip.nodeCountEPackageX[i].ToString();

                    }

                }
                else
                {
                    poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(Convert.ToInt32(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[1].Value.ToString()));
                    //poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(Convert.ToInt32(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[1].Value.ToString()));
                    Atend.Base.Equipment.EHalter halter = Atend.Base.Equipment.EHalter.AccessSelectByCode(poleTip.HalterID);
                    if (halter.Code != -1)
                    {
                        txtHalter.Text = halter.Name;
                        txtHalterCount.Text = poleTip.HalterCount.ToString();
                    }
                    else
                    {
                        txtHalter.Text = "";
                        txtHalterCount.Text = "0";
                    }

                    dtgvPoleConsol.Rows.Clear();
                    for (int i = 0; i < Atend.Base.Equipment.EPoleTip.nodeKeysEPackage.Count; i++)
                    {
                        Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(Atend.Base.Equipment.EPoleTip.nodeKeysEPackage[i].ToString()));
                        DataRow dr = dtgvPoleConsol.NewRow();
                        dr["Name"] = Consol.Name;
                        dr["Code"] = Guid.Empty;//Atend.Base.Equipment.EPoleTip.nodeKeysEPackage[i];
                        dr["XCode"] = "_";
                        dr["AccessCode"] = Atend.Base.Equipment.EPoleTip.nodeKeysEPackage[i];
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
                        //dr["IsExistance"] = dtExistance.Rows[4]["Code"].ToString();
                        //dr["ProjectCode"] = dtProjectCode.Rows[0]["Code"].ToString();
                        dr["count"] = "1";
                        dtgvPoleConsol.Rows.Add(dr);
                    }
                }

                //if (Convert.ToBoolean(gvPoleTip.Rows[gvPoleTip.CurrentRow.Index].Cells[2].Value) == false)
                //{
                //    dtgvPoleConsol = dtCurrent.Copy();
                //    gvConsol.AutoGenerateColumns = false;
                //    gvConsol.DataSource = dtgvPoleConsol;
                //    return;
                //}
                //else
                //{
                //    gvConsol.AutoGenerateColumns = false;
                //    gvConsol.DataSource = dtgvPoleConsol;
                //}

            }
            
        }

        private void cboHIsExistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboHIsExistance.SelectedValue.ToString()));
            cboHProjectCode.DisplayMember = "Name";
            cboHProjectCode.ValueMember = "ACode";
            cboHProjectCode.DataSource = dtWorkOrder;
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