using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmEditDrawKhazan02 : Form
    {
        bool AllowClose = true;
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        public int Code;
        public Guid NodeCode;
        public Guid DpakageCode;
        public ObjectId ObjID;
        int selectedProductCode = -1;
        bool ForceToClose = false;

        public frmEditDrawKhazan02()
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

        public void ChangeColor()
        {
            for (int i = 0; i < gvBankKhazan.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvBankKhazan.Rows[i].Cells[3].Value) == false)
                {
                    gvBankKhazan.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        private void frmDrawKhazan_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dtMerge = Atend.Base.Equipment.EKhazanTip.SelectAllAndMerge();
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selectedProductCode = dpack.ProductCode;

            gvBankKhazan.AutoGenerateColumns = false;
            gvBankKhazan.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvBankKhazan, this);
            ChangeColor();
            for (int i = 0; i < gvBankKhazan.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvBankKhazan.Rows[i].Cells[4].Value.ToString()) == dpack.ProductCode && Convert.ToBoolean(gvBankKhazan.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    gvBankKhazan.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            BindDataToComboBoxIsExist();
            //BindDataTocboProjCode();

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;

            //Atend.Base.Acad.AcadGlobal.dPackageForKhazanTip = new Atend.Base.Design.DPackage();
            //Atend.Base.Acad.AcadGlobal.dPackageForKhazan.Clear();
            //gvBankKhazan.AutoGenerateColumns = false;
            //gvKhazan.AutoGenerateColumns = false;

            //gvBankKhazan.DataSource = Atend.Base.Equipment.EKhazanTip.DrawSearch(txName.Text);
            //gvKhazan.DataSource = null;


            //for (int i = 0; i < gvBankKhazan.Rows.Count; i++)
            //{
            //    if (new Guid(gvBankKhazan.Rows[i].Cells["Column2"].Value.ToString()) == Code)
            //    {
            //        gvBankKhazan.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            //    }
            //}

            //ContainerPackageCode
            //gvKhazan.DataSource = Atend.Base.Equipment.EKhazan.DrawSearch(
            //   Convert.ToInt32(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value));
            //gvKhazan.DataSource = Atend.Base.Equipment.EKhazan.DrawSearch(
            //   new Guid(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value.ToString()));

        }

        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Name = string.Empty;
            bool check = false;
            string strFilter = "";

            if (txName.Text != string.Empty)
            {
                Name = txName.Text;
                strFilter = " Name='" + Name.ToString() + "'";
                check = true;
            }

            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvBankKhazan.AutoGenerateColumns = false;
                gvBankKhazan.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvBankKhazan.AutoGenerateColumns = false;
                //gvBankKhazan.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvBankKhazan, this);
            ChangeColor();
            for (int i = 0; i < gvBankKhazan.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvBankKhazan.Rows[i].Cells[4].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvBankKhazan.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    gvBankKhazan.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            //gvBankKhazan.DataSource = Atend.Base.Equipment.EKhazanTip.DrawSearch(txName.Text);
            gvKhazan.DataSource = null;

        }

        //private void gvBankKhazan_DoubleClick(object sender, System.EventArgs e)
        //{
        //    //MessageBox.Show(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value.ToString());
        //    gvKhazan.DataSource = Atend.Base.Equipment.EKhazan.DrawSearch(Convert.ToInt32(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value));

        //    //MessageBox.Show(" COUNT :" + gvKhazan.Rows.Count.ToString());

        //}

        private bool Validation()
        {
            //if (gvBankKhazan.SelectedRows.Count == 0 && gvKhazan.Rows.Count == 0)
            //{
            //    return false;
            //}


            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            if (string.IsNullOrEmpty(cboIsExist.Text))
            {
                MessageBox.Show("لطفاً کد وضعیت تجهیز را انتخاب کنید", "خطا");
                return false;
            }

            if (gvBankKhazan.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا خازن مورد نظر را انتخاب نمایید", "خطا");
                return false;
            }

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawKhazan DrawKhazan = new Atend.Global.Acad.DrawEquips.AcDrawKhazan();
            if (Validation())
            {
                if (Convert.ToBoolean(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[3].Value) == false)
                {
                    DrawKhazan.UseAccess = true;
                    DrawKhazan.eKhazanTip = Atend.Base.Equipment.EKhazanTip.AccessSelectByCode(Convert.ToInt32(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[4].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[3].Value))
                {
                    DrawKhazan.UseAccess = false;
                    DrawKhazan.eKhazanTip = Atend.Base.Equipment.EKhazanTip.SelectByXCode(new Guid(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value.ToString()));
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawKhazan.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    DrawKhazan.ProjectCode = 0;
                else
                    DrawKhazan.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawKhazan.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                if (DrawKhazan.UpdateKhazanData(dpack.Code))
                {
                    ed.WriteMessage("Update Khazan Success \n");
                    AllowClose = true;
                    this.Close();
                }
            }



            //Extra
            //if (gvBankKhazan.Rows.Count > 0)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage(gvBankKhazan.SelectedRows[0].Cells["Column7"].Value.ToString() + "\n");
            //    //ed.WriteMessage(gvBankKhazan.SelectedRows[0].Index.ToString() + "\n");
            //    Atend.Base.Design.DPackage Pakage = Atend.Base.Design.DPackage.AccessSelectByCode(DpakageCode);
            //    //**EDIT**
            //    Pakage.ProductCode = Convert.ToInt32(gvBankKhazan.SelectedRows[0].Cells["Column7"].Value.ToString());
            //    //Pakage.ProductCode = new Guid(gvBankKhazan.SelectedRows[0].Cells["Column7"].Value.ToString());

            //    if (Pakage.AccessUpdate())
            //    {
            //        //ed.WriteMessage("OK");
            //        //Atend.Base.Acad.AT_INFO
            //        //Code = Convert.ToInt32(gvBankKhazan.SelectedRows[0].Cells["Column7"].Value.ToString());
            //        Code = new Guid(gvBankKhazan.SelectedRows[0].Cells["Column7"].Value.ToString());
            //        AllowClose = true;
            //        this.Close();
            //    }
            //    else
            //        MessageBox.Show("انجام ويرايش امكانپذير نيست");
            //}


            //if (Validation())
            //{
            //    //DataTable KhazanTipTable = Atend.Base.Equipment.EKhazanTip.SelectByCode(
            //    //    Convert.ToInt32(gvBankKhazan.CurrentRow.Index));
            //    Atend.Base.Acad.AcadGlobal.dPackageForKhazanTip.Count = 1;
            //    Atend.Base.Acad.AcadGlobal.dPackageForKhazanTip.Type = (int)Atend.Control.Enum.ProductType.BankKhazan;
            //    Atend.Base.Acad.AcadGlobal.dPackageForKhazanTip.ProductCode = Convert.ToInt32(
            //        gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells["Column7"].Value);



            //    Atend.Base.Design.DPackage dPackge;


            //    DataTable dt = (DataTable)gvKhazan.DataSource;
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        dPackge = new Atend.Base.Design.DPackage();
            //        dPackge.Count = Convert.ToInt32(dr["Count"]);
            //        dPackge.Type=Convert.ToInt32(dr["Type"]);
            //        dPackge.ProductCode = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);


            //        Atend.Base.Acad.AcadGlobal.dPackageForKhazan.Add(dPackge);
            //    }

            //    AllowClose = true;
            //}
            //else
            //{
            //    AllowClose = false;
            //}


        }

        private void frmDrawKhazan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AllowClose)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            this.Close();
        }

        private void gvBankKhazan_Click(object sender, EventArgs e)
        {
            if (gvBankKhazan.Rows.Count != 0)
            {
                if (Convert.ToBoolean(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[3].Value.ToString()))
                {
                    System.Data.DataTable st = Atend.Base.Equipment.EKhazan.DrawSearchX(new Guid(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value.ToString()));
                    gvKhazan.AutoGenerateColumns = false;
                    gvKhazan.DataSource = st;
                }
                else
                {
                    System.Data.DataTable st = Atend.Base.Equipment.EKhazanTip.AccessDrawSearch(Convert.ToInt32(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[4].Value.ToString()), (int)Atend.Control.Enum.ProductType.BankKhazan);
                    gvKhazan.AutoGenerateColumns = false;
                    gvKhazan.DataSource = st;
                }
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