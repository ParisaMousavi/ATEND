using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Equipment
{
    public partial class frmKhazanTip02 : Form
    {
        public string Descript="";
        public Guid selectedKhazanTipXCode=Guid.Empty;
        public bool IsDefault = false;
        bool ForceToClose = false;
        int Code = -1;

        public frmKhazanTip02()
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

        private void frmKhazanTip_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            gvKhazan.AutoGenerateColumns = false;
            gvKhazan.DataSource = Atend.Base.Equipment.EKhazan.SelectAllX();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bool check = true;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("count={0}\n",gvKhazanTip.Rows.Count);

            if (gvKhazan.CurrentRow.Index < 0)
            {
                MessageBox.Show("لطفاً خازنهای مورد نظر را انتخاب کنید.");
                return;
            }
            for (int i = 0; i < gvKhazanTip.Rows.Count; i++)
            {

                //MessageBox.Show(i.ToString() + "        " + gvKhazanTip.Rows[i].Cells[0].Value.ToString());
                //MessageBox.Show(gvKhazan.SelectedRows[0].Cells[0].Value.ToString());
                if ((gvKhazan.CurrentRow.Cells[0].Value.ToString() == gvKhazanTip.Rows[i].Cells[0].Value.ToString()) && check)
                {
                    check = false;
                    MessageBox.Show("خازن انتخاب شده در مجموعه خازنی وجود دارد\n");
                }
            }
            if (check)
            {
                gvKhazanTip.Rows.Add();
                gvKhazanTip.Rows[gvKhazanTip.Rows.Count - 1].Cells[0].Value = gvKhazan.Rows[gvKhazan.CurrentRow.Index].Cells[0].Value.ToString();
                gvKhazanTip.Rows[gvKhazanTip.Rows.Count - 1].Cells[1].Value = gvKhazan.Rows[gvKhazan.CurrentRow.Index].Cells[1].Value.ToString();
                gvKhazanTip.Rows[gvKhazanTip.Rows.Count - 1].Cells[2].Value = 1;
                gvKhazanTip.Rows[gvKhazanTip.Rows.Count - 1].Cells[3].Value = gvKhazan.Rows[gvKhazan.CurrentRow.Index].Cells[4].Value.ToString();//Vol
                gvKhazanTip.Rows[gvKhazanTip.Rows.Count - 1].Cells[4].Value = gvKhazan.Rows[gvKhazan.CurrentRow.Index].Cells[2].Value.ToString();//Capacity
                gvKhazanTip.Refresh();
            }
        
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            gvKhazanTip.Rows.RemoveAt(gvKhazanTip.CurrentRow.Index);

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            selectedKhazanTipXCode = Guid.Empty;
            Descript = string.Empty;
            txtName.Text = string.Empty;
            IsDefault = false;
            tsbIsDefault.Checked = false;
            Code = -1;

            gvKhazanTip.Rows.Clear();
            ClearGrid(gvKhazanTip);
        }

        private void ClearGrid(DataGridView dataGridView)
        {
            for (int i = 0; i <= dataGridView.Rows.Count - 1; i++)
            {
                dataGridView.Rows.RemoveAt(i);
            }
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if ( selectedKhazanTipXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EKhazanTip Equip = Atend.Base.Equipment.EKhazanTip.SelectByXCode(selectedKhazanTipXCode);
                    if (Equip.IsDefault || IsDefault)
                    {
                        MessageBox.Show("کاربر گرامی شما اجازه ویرایش  تجهیز به صورت پیش فرض ندارید", "خطا");
                        return false;
                    }
                }
            }
            return true;
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفا نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            if (Atend.Base.Equipment.EKhazanTip.SearchByName(txtName.Text) == true && selectedKhazanTipXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            return CheckStatuseOfAccessChangeDefault();
            //return true;

        }

        private void Save()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            txtName.Focus();
            Atend.Base.Equipment.EKhazanTip khazantip = new Atend.Base.Equipment.EKhazanTip();
            khazantip.Name = txtName.Text;
            khazantip.IsDefault = IsDefault;
            //khazantip.Description = Descript;
            ArrayList EEquipment = new ArrayList();

            //ed.WriteMessage(gvKhazanTip.Rows.Count.ToString()+"<--\n");
            for (int j = 0; j < gvKhazanTip.Rows.Count; j++)
            {

                if (Convert.ToInt32(gvKhazanTip.Rows[j].Cells[2].Value.ToString()) != 0)
                {
                    Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                    //_EProductPackage.ProductCode = 0;// Convert.ToInt32(gvKhazanTip.Rows[j].Cells[0].Value.ToString());
                    _EProductPackage.XCode = new Guid(gvKhazanTip.Rows[j].Cells[0].Value.ToString());
                    _EProductPackage.Count = Convert.ToInt32(gvKhazanTip.Rows[j].Cells[2].Value.ToString());
                    _EProductPackage.TableType = Convert.ToInt16(Atend.Control.Enum.ProductType.Khazan);  //???
                    EEquipment.Add(_EProductPackage);
                    //Description
                    Descript += string.Format("{0}*({1})KVar,{2}KV  \n", gvKhazanTip.Rows[j].Cells[2].Value,gvKhazanTip.Rows[j].Cells[4].Value, gvKhazanTip.Rows[j].Cells[3].Value);
                    //ed.WriteMessage(Descript+"\n");
                }
            }
            khazantip.EquipmentList = EEquipment;
            khazantip.Description = Descript;
            khazantip.Code = Code;
            if (selectedKhazanTipXCode == Guid.Empty)
            {
                if (khazantip.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");

            }
            else
            {
                khazantip.XCode = selectedKhazanTipXCode;
                if (khazantip.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");

            }
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (CheckStatuseOfAccessChangeDefault())
            Delete();
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(selectedKhazanTipXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(selectedKhazanTipXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (selectedKhazanTipXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EKhazanTip.DeleteX(selectedKhazanTipXCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }

        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            frmKhazanTipSearch02 search = new frmKhazanTipSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(search);
        }

        public void BindDataToOwnControl(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Base.Equipment.EKhazanTip khazantip = Atend.Base.Equipment.EKhazanTip.SelectByXCode(XCode);
            txtName.Text = khazantip.Name;
            selectedKhazanTipXCode = XCode;
            Descript = "";
            tsbIsDefault.Checked = khazantip.IsDefault;
            Code = khazantip.Code;
            //Descript = khazantip.Description;
            BindGridEquipment(gvKhazanTip);
        }

        private void BindGridEquipment(DataGridView dataGridView)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            for (int i = 0; i <= dataGridView.Rows.Count - 1; i++)
            {
                dataGridView.Rows.RemoveAt(i);
            }
            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            for (int i = 0; i < Atend.Base.Equipment.EKhazanTip.nodeKeysEPackage.Count; i++)
            {                
                string s = Atend.Base.Equipment.EKhazanTip.nodeKeysEPackage[i].ToString();
                gvKhazanTip.Rows.Add();
                gvKhazanTip.Rows[gvKhazanTip.Rows.Count - 1].Cells[0].Value = Atend.Base.Equipment.EKhazanTip.nodeKeysEPackage[i].ToString();
                gvKhazanTip.Rows[gvKhazanTip.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EKhazanTip.nodeCountEPackage[i].ToString();
                Atend.Base.Equipment.EKhazan kh = Atend.Base.Equipment.EKhazan.SelectByXCode(new Guid(Atend.Base.Equipment.EKhazanTip.nodeKeysEPackage[i].ToString()));
                gvKhazanTip.Rows[gvKhazanTip.Rows.Count - 1].Cells[1].Value = kh.Name; 
            }


        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
            }
        }

        private void tsbIsDefault_Click(object sender, EventArgs e)
        {
            if (IsDefault)
            {
                IsDefault = false;
                tsbIsDefault.Checked = false;
            }
            else
            {
                IsDefault = true;
                tsbIsDefault.Checked = true;
            }
        }

        private void tsbShare_Click(object sender, EventArgs e)
        {

            if (selectedKhazanTipXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan), selectedKhazanTipXCode))
                {
                    Atend.Base.Equipment.EKhazanTip KhazanTip = Atend.Base.Equipment.EKhazanTip.SelectByXCode(selectedKhazanTipXCode);
                    Code = KhazanTip.Code;
                    MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
                }
                else
                {
                    MessageBox.Show("خطا در به اشتراک گذاری .");
                }
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
            }

            //if (selectedKhazanTipXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EKhazanTip.ShareOnServer(selectedKhazanTipXCode))
            //        MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
            //    else
            //        MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            //}
            //else
            //    MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
        }

    }
}