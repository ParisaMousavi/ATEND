using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Design
{
    public partial class frmDrawGroundCabelTip03 : Form
    {
        bool ForceToClose = false;

        public frmDrawGroundCabelTip03()
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

            TypeTbl.Columns.Add(TypeCode);
            TypeTbl.Columns.Add(TypeName1);

            DataRow dr1 = TypeTbl.NewRow();
            dr1["Name"] = "XLPE از جنس آلمینیوم با عایق خشک";
            dr1["Code"] = 1;

            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "XLPE از جنس مس با عایق خشک";
            dr2["Code"] = 2;

            DataRow dr3 = TypeTbl.NewRow();
            dr3["Name"] = "PVC از جنس آلمینیوم با عایق خشک";
            dr3["Code"] = 3;

            DataRow dr4 = TypeTbl.NewRow();
            dr4["Name"] = "PVC از جنس مس با عایق خشک";
            dr4["Code"] = 4;

            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);
            TypeTbl.Rows.Add(dr3);
            TypeTbl.Rows.Add(dr4);
        }

        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        DataColumn TypeName1 = new DataColumn("Name", typeof(string));
        DataColumn TypeCode = new DataColumn("Code", typeof(int));
        DataTable TypeTbl = new DataTable();
        DataTable dtMerge = new DataTable();


        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabels = new List<Atend.Base.Equipment.EGroundCabel>();
            Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabelTip = new Atend.Base.Equipment.EGroundCabelTip();
            Atend.Base.Acad.AcadGlobal.GroundCableData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.GroundCableData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.GroundCableData.UseAccess = false;
        }

        //Confirmed
        public void ChangeColor()
        {
            for (int i = 0; i < gvConductor.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvConductor.Rows[i].Cells[2].Value) == false)
                {
                    gvConductor.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        private bool Validation()
        {
            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            //if (Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.ProductCode == 0)
            //{
            //    MessageBox.Show("لطفا سیم مورد نظر را انتخاب نمایید", "خطا");
            //    return false;
            //}

            if (gvConductor.Rows.Count > 0 && gvConductor.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;
            }


            return true;

        }

        private void BindMaterialToComboBox()
        {
            cboMaterial.ValueMember = "Code";
            cboMaterial.DisplayMember = "Name";
            cboMaterial.DataSource = TypeTbl;
            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;
            }

        }

        //Confirmed
        private void BindDataToGridMain()
        {

            dtMerge = Atend.Base.Equipment.EGroundCabel.SelectAllAndMerge();
            //ed.WriteMessage("\nDTMERGE ROWS = {0}\n" ,dtMerge.Rows.Count.ToString());
            DataColumn dcTypeName = new DataColumn("TypeName");
            dtMerge.Columns.Add(dcTypeName);

            foreach (DataRow dr in dtMerge.Rows)
            {
                if (Convert.ToInt32(dr["Type"].ToString()) == 1)
                {
                    dr["TypeName"] = "XLPE از جنس آلمینیوم با عایق خشک";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 2)
                {
                    dr["TypeName"] = "XLPE از جنس مس با عایق خشک";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 3)
                {
                    dr["TypeName"] = "PVC از جنس آلمینیوم با عایق خشک";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 4)
                {
                    dr["TypeName"] = "از جنس مس با عایق خشک";
                }
            }
            gvConductor.AutoGenerateColumns = false;
            gvConductor.DataSource = dtMerge;
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

        private void frmDrawBranch01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindMaterialToComboBox();
            BindDataToComboBoxIsExist();

            if (cboVoltage.Items.Count > 0)
            {
                cboVoltage.SelectedIndex = 0;
            }
            SetDefaultValues();
        }

        //Confirmed
        public void BindDataToComboBoxIsExist()
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

            //cboIsExist.DisplayMember = "Name";
            //cboIsExist.ValueMember = "Code";
            //cboIsExist.DataSource = Atend.Control.Common.StatuseCode;
            //cboIsExist.SelectedIndex = 4;
        }

        public void BindDataToCounductorTip()
        {
            float CrossSectionArea = -1;
            int TypeCode = -1;
            //int Voltage = -1;

            if (chkMaterail.Checked)
                TypeCode = Convert.ToInt32(cboMaterial.SelectedValue);

            if (chkSectionArea.Checked)
                CrossSectionArea = Convert.ToSingle(nudCrossSectionArea.Value);

            //if (chkVoltage.Checked)
            //    Voltage = Convert.ToInt32(cboVoltage.Text);


            //DataTable dt = Atend.Base.Equipment.EGroundCabel.DrawSearch(CrossSectionArea, TypeCode , Voltage);


            gvConductor.AutoGenerateColumns = false;
            //gvConductor.DataSource = dt;
            //gvConductor.DataSource = Atend.Base.Equipment.EConductor.DrawSearch(CrossSectionArea, MaterailCode);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            float CrossSectionArea = -1;
            int TypeCode = -1;
            int Voltage = -1;
            bool Check = false;
            string strFilter = "";

            if (chkMaterail.Checked)
            {
                if (!string.IsNullOrEmpty(cboMaterial.Text))
                {
                    TypeCode = Convert.ToInt32(cboMaterial.SelectedValue);
                    Check = true;
                    strFilter = " Type='" + TypeCode + "'";
                }
            }
            if (chkSectionArea.Checked)
            {
                Check = true;
                CrossSectionArea = Convert.ToSingle(nudCrossSectionArea.Value);
                if (strFilter != "")
                {
                    strFilter += "  AND CrossSectionArea='" + CrossSectionArea + "'";
                }
                else
                {
                    strFilter = " CrossSEctionArea='" + CrossSectionArea + "'";
                }
            }
            if (chkVoltage.Checked)
            {
                Check = true;
                Voltage = Convert.ToInt32(cboVoltage.Text);
                if (strFilter != "")
                {
                    strFilter += "  AND Vol ='" + Voltage + "'";
                }
                else
                {
                    strFilter = " Vol ='" + Voltage + "'";
                }
            }

            if (Check)
            {
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = strFilter;
                gvConductor.AutoGenerateColumns = false;
                gvConductor.DataSource = dv;
            }
            else
            {
                gvConductor.AutoGenerateColumns = false;
                gvConductor.DataSource = dtMerge;
            }
            ChangeColor();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[2].Value) == false)//Access
                {
                    Atend.Base.Equipment.EGroundCabelTip GroundCabelTip = Atend.Base.Equipment.EGroundCabelTip.AccessSelectByCode(Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));
                    if (GroundCabelTip.Code != -1)
                    {
                        Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabelTip = GroundCabelTip;
                        Atend.Base.Acad.AcadGlobal.GroundCableData.UseAccess = true;

                        Atend.Base.Equipment.EGroundCabel GroundPhase = Atend.Base.Equipment.EGroundCabel.AccessSelectByCode(GroundCabelTip.PhaseProductCode);
                        if (GroundPhase.Code != -1)
                        {
                            Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabels.Add(GroundPhase);
                            Atend.Base.Equipment.EGroundCabel GroundNeutral = Atend.Base.Equipment.EGroundCabel.AccessSelectByCode(GroundCabelTip.NeutralProductCode);
                            if (GroundNeutral.Code != -1)
                            {
                                Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabels.Add(GroundNeutral);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {

                    Atend.Base.Equipment.EGroundCabelTip GroundCabelTip = Atend.Base.Equipment.EGroundCabelTip.SelectByXCode(new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString()));
                    if (GroundCabelTip.Code != -1)
                    {
                        Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabelTip = GroundCabelTip;
                        Atend.Base.Acad.AcadGlobal.GroundCableData.UseAccess = false;

                        Atend.Base.Equipment.EGroundCabel GroundPhase = Atend.Base.Equipment.EGroundCabel.SelectByXCode(GroundCabelTip.PhaseProductXCode);
                        if (GroundPhase.Code != -1)
                        {
                            Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabels.Add(GroundPhase);

                            Atend.Base.Equipment.EGroundCabel GroundNeutral = Atend.Base.Equipment.EGroundCabel.SelectByXCode(GroundCabelTip.NeutralProductXCode);
                            if (GroundNeutral.Code != -1)
                            {
                                Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabels.Add(GroundNeutral);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.GroundCableData.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                {
                    Atend.Base.Acad.AcadGlobal.GroundCableData.ProjectCode = 0;
                }
                else
                {
                    Atend.Base.Acad.AcadGlobal.GroundCableData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void frmDrawBranch01_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void gvConductor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvConductor_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvConductor_Click(object sender, EventArgs e)
        {
            //ed.WriteMessage("1 \n");
            //ed.WriteMessage("Tip Code {0} \n", gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());

            if (gvConductor.Rows.Count > 0)
            {
                //ed.WriteMessage("Tip Code {0} \n", gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
                //Atend.Base.Acad.AcadGlobal.dBranch.ProductCode = Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
                //////Atend.Base.Acad.AcadGlobal.dBranch.XCode= new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
            }

        }

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

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        private void label6_Click(object sender, EventArgs e)
        {

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

            }
        }


    }
}