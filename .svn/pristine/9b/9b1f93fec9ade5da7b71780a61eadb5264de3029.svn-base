using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Equipment
{
    public partial class frmOperation02 : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        int selectedID = 0;
        bool ForceToClose = false;

        public frmOperation02()
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

        private void frmOperation02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToCboUnit();
        }

        private void Reset()
        {
            txtName.Text = string.Empty;
            selectedID = 0;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفا نام را وارد نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            return true;
        }

        private void Save()
        {
            if (Validation())
            {
                txtName.Focus();
                Atend.Base.Base.BProduct bproduct = new Atend.Base.Base.BProduct();
                bproduct.Name = txtName.Text;
                bproduct.Unit = Convert.ToByte(cboUnit.SelectedValue);
                bproduct.WagePrice = 0;
                bproduct.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Operation);
                bproduct.Price = 0;
                //bproduct.Number = "0";
                bproduct.IsProduct = false;
                //bproduct.ID = 0;
                bproduct.ExecutePrice = 0;
                bproduct.Code = 0;
                if (selectedID == 0)
                {
                    if (!bproduct.InsertX())
                    {
                    }

                    else
                    {
                        Reset();
                    }
                }
                else
                {
                    bproduct.ID = selectedID;
                    if (!bproduct.UpdateXBYID())
                    {
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }

        private void BindDataToCboUnit()
        {
            cboUnit.DisplayMember = "Name";
            cboUnit.ValueMember = "Code";
            cboUnit.DataSource = Atend.Base.Base.BUnit.SelectAll();
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            frmOperationSearch02 frm = new frmOperationSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);
        }

        public void BindDataToOneControl(int id)
        {
            Atend.Base.Base.BProduct bProduct = Atend.Base.Base.BProduct.Select_ByIdX(id);
            ed.WriteMessage("bProduct={0}\n", id);
            txtName.Text = bProduct.Name;
            cboUnit.SelectedValue = bProduct.Unit;
            selectedID = bProduct.ID;
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {

            if (MessageBox.Show("آیا مایل به حذف کردن اطلاعات می باشید؟", "خطا", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (selectedID != 0)
                {
                    if (Atend.Base.Base.BProduct.DeleteXByID(selectedID))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "حذف");
            }
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}