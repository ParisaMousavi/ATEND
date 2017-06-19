using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Global.Design
{
    public partial class frmDrawBus : Form
    {


        DataTable dtMerge = new DataTable();
        bool AllowClose = true;
        bool ForceToClose = false;
        string MainRowFilter = "";


        public frmDrawBus()
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

        private void BindDataToGridMain()
        {

            dtMerge = Atend.Base.Equipment.EBus.SelectAllAndMerge();
            dtMerge.DefaultView.RowFilter = MainRowFilter;
            gvBus.AutoGenerateColumns = false;
            gvBus.DataSource = dtMerge;
            ChangeColor();

        }

        private void frmDrawBus_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToGridMain();
            BindDataToCboIsExist();
            SetDefaultValues();

        }

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

        public void ChangeColor()
        {
            for (int i = 0; i < gvBus.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvBus.Rows[i].Cells["IsSql"].Value) == false)
                {
                    gvBus.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataToCboIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            DataTable dtstatus = Atend.Base.Base.BEquipStatus.SelectAllX();
            cboIsExist.DataSource = dtstatus;

            DataRow[] drs = dtstatus.Select("IsDefault=True");
            if (drs.Length > 0)
            {
                cboIsExist.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();

        }


        private bool Validation()
        {
            if (gvBus.Rows.Count > 0 && gvBus.SelectedRows.Count > 0)
            {
                //do nothing
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }

            return true;

        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Base.Equipment.EBus eBus;
                if (Convert.ToBoolean(gvBus.Rows[gvBus.CurrentRow.Index].Cells["IsSql"].Value) == false)//Access
                {
                    Atend.Base.Acad.AcadGlobal.BusData.UseAccess = true;
                    eBus = Atend.Base.Equipment.EBus.AccessSelectByCode(Convert.ToInt32(gvBus.Rows[gvBus.CurrentRow.Index].Cells[0].Value));
                }
                else
                {
                    Atend.Base.Acad.AcadGlobal.BusData.UseAccess = false;
                    eBus = Atend.Base.Equipment.EBus.SelectByXCode(new Guid(gvBus.Rows[gvBus.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                if (eBus.Code != -1)
                {
                    Atend.Base.Acad.AcadGlobal.BusData.eBus = eBus;
                    Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                    Atend.Base.Acad.AcadGlobal.BusData.Existance = status.ACode;

                    if (cboProjCode.Items.Count == 0)
                        Atend.Base.Acad.AcadGlobal.BusData.ProjectCode = 0;
                    else
                        Atend.Base.Acad.AcadGlobal.BusData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());

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
            else
            {
                AllowClose = false;
            }
        }

    }
}