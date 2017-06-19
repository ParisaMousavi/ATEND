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
using System.Collections;
using System.IO;

//get from tehran 7/15
namespace Atend.Equipment
{
    public partial class frmPoleTip02 : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public bool IsDefault = false;
        public bool IsSelectPole = false;
        public Guid selectedPoleTip = Guid.Empty;
        public int SelectedPoleTipCode = 0;
        int code = -1;
        byte[] image;
        bool ForceToClose = false;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPoleTip02));

        public frmPoleTip02()
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

        private void BindDataToHalterType()
        {
            cboHalterType.DataSource = Atend.Base.Equipment.EHalter.SelectAllX();
            cboHalterType.DisplayMember = "Name";
            cboHalterType.ValueMember = "XCode";
        }

        private void frmPoleTip02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToHalterType();

            if (cboType.Items.Count > 0)
            {
                cboType.SelectedIndex = 0;
            }

            if (cboVoltagelevel.Items.Count > 0)
            {
                cboVoltagelevel.SelectedIndex = 0;
            }

            if (cboConsolType.Items.Count > 0)
            {
                cboConsolType.SelectedIndex = 0;
            }

            if (cboTypeOfConsol.Items.Count > 0)
            {
                cboTypeOfConsol.SelectedIndex = 0;
            }

            image = imageToByteArray(((System.Drawing.Image)(resources.GetObject("pictureBox2.Image"))));

            //btnSearch
            btnSearch_Click(sender, e);
            button1_Click(sender, e);
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
           imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            double height = -1;
            double power = -1;
            int type = -1;
            if (chkHeight.Checked)
                height = Convert.ToDouble(nudHeight.Value);
            if (chkPower.Checked)
                power = Convert.ToDouble(nudPower.Value);
            if (chkType.Checked)
                type = Convert.ToInt32(cboType.SelectedValue);
            gvPole.AutoGenerateColumns = false;
            gvPole.DataSource = Atend.Base.Equipment.EPole.DrawSearchLocal(height, power, type);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int voltage = -1;
            int consoltype = -1;
            int type = -1;
            if (chkVoltagelevel.Checked)
                if(chkVoltagelevel.Text != "")
                    voltage = Convert.ToInt32(cboVoltagelevel.Text);

            if (chkConsolType.Checked)
                consoltype = Convert.ToInt32(cboConsolType.SelectedIndex);
            if (chkTypeOfConsol.Checked)
                type = Convert.ToInt32(cboTypeOfConsol.SelectedIndex);
            gvConsol.AutoGenerateColumns = false;
            DataTable dtConosl = new DataTable();
            dtConosl = Atend.Base.Equipment.EConsol.DrawSearch(voltage, consoltype, type);
            DataColumn dcType = new DataColumn("NameConsolType");
            dtConosl.Columns.Add(dcType);
            foreach (DataRow dr in dtConosl.Rows)
            {
                if (dr["ConsolType"].ToString()=="0")
                {
                    dr["NameConsolType"]="کششی";
                }
                if (dr["ConsolType"].ToString()=="1")
                {
                    dr["NameConsolType"]="انتهایی";
                }
                if (dr["ConsolType"].ToString()=="2")
                {
                    dr["NameConsolType"] = "عبوری";
                }
                if (dr["ConsolType"].ToString()=="3")
                {
                    dr["NameConsolType"] = "DP";
                }
            }
            gvConsol.DataSource = dtConosl;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (gvConsol.Rows.Count > 0)
            {
                gvPoleTip.Rows.Add();
                gvPoleTip.Rows[gvPoleTip.Rows.Count - 1].Cells[0].Value = gvConsol.Rows[gvConsol.CurrentRow.Index].Cells[0].Value.ToString();
                gvPoleTip.Rows[gvPoleTip.Rows.Count - 1].Cells[1].Value = gvConsol.Rows[gvConsol.CurrentRow.Index].Cells[1].Value.ToString();
                gvPoleTip.Rows[gvPoleTip.Rows.Count - 1].Cells[2].Value = "1";
            }
            else
            {
                MessageBox.Show("لطفا کنسول مورد نظر را انتخاب نمایید", "خطا");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvPoleTip.Rows.Count>0)
            gvPoleTip.Rows.RemoveAt(gvPoleTip.CurrentRow.Index);
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            selectedPoleTip = Guid.Empty;
            txtName.Text = string.Empty;
            txtComment.Text = string.Empty;
            IsDefault = false;
            IsSelectPole = false;
            tsbIsDefault.Checked = false;
            chkConsolType.CheckState = CheckState.Unchecked;
            chkHeight.CheckState = CheckState.Unchecked;
            chkPower.CheckState = CheckState.Unchecked;
            chkType.CheckState = CheckState.Unchecked;
            chkTypeOfConsol.CheckState = CheckState.Unchecked;
            chkVoltagelevel.CheckState = CheckState.Unchecked;
            nudHalter.Value = 0;
            nudHeight.Value = 0;
            nudPower.Value = 0;
            txtImage.Text = string.Empty;
            pictureBox1.Image = Image.FromFile(Atend.Control.Common.fullPath + "\\Consol.jpg");
            cboConsolType.SelectedIndex = 0;
            cboHalterType.SelectedIndex = 0;
            cboType.SelectedIndex = 0;
            cboTypeOfConsol.SelectedIndex = 0;
            cboVoltagelevel.SelectedIndex = 0;
            ClearGrid(gvPoleTip);
            code = -1;
        }

        private void ClearGrid(DataGridView dataGridView)
        {
            for (int i = dataGridView.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView.Rows.RemoveAt(i);
            }

            for (int i = 0; i < gvPole.Rows.Count; i++)
            {
                gvPole.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (selectedPoleTip == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EPoleTip PoleTip = Atend.Base.Equipment.EPoleTip.SelectByXCode(selectedPoleTip);
                    if (PoleTip.IsDefault || IsDefault)
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
            if (Atend.Base.Equipment.EPoleTip.SearchByName(txtName.Text) == true && selectedPoleTip == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (gvPoleTip.Rows.Count == 0)
            {
                MessageBox.Show("لطفا کنسول را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            if (gvPoleTip.Rows.Count > 4)
            {
                MessageBox.Show("حداکثر تعداد کنسول 4 عدد می باشد", "خطا");
                txtName.Focus();
                return false;
            }
            if (gvPole.SelectedRows.Count==0)
            {
                MessageBox.Show("لطفا پایه مورد نظر را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }

            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvPoleTip, 2))
            {
                MessageBox.Show("لطفا تعداد کنسول را با فرمت مناسب وارد نمایید", "خطا");
                return false;
            }

            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void Save()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            btnInsert.Focus();
            Atend.Base.Equipment.EPoleTip poletip = new Atend.Base.Equipment.EPoleTip();
            poletip.Name = txtName.Text;
            poletip.Comment = txtComment.Text;
            poletip.IsDefault = IsDefault;
            ed.WriteMessage("IsDefault={0}\n", IsDefault);
            poletip.HalterCount = Convert.ToInt32(nudHalter.Value);
            poletip.HalterXID = new Guid(cboHalterType.SelectedValue.ToString());
            ed.WriteMessage("kkk\n");
            poletip.Factor = 0;  //////////////
            ed.WriteMessage("gvPole={0}\n", gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value.ToString());
            poletip.PoleXCode = new Guid(gvPole.SelectedRows[0].Cells[0].Value.ToString());
            poletip.Code = code;
            ed.WriteMessage("ss\n");




            System.Drawing.Image image1 = pictureBox1.Image;
                        poletip.Image = null;
            FileStream fs;
            if (txtImage.Text != String.Empty)
            {
                fs = new FileStream(txtImage.Text, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                poletip.Image = br.ReadBytes((Int32)br.BaseStream.Length);
                fs.Dispose();
            }
            else
            {
                //fs = new FileStream(Atend.Control.Common.fullPath + "\\Consol1.jpg", FileMode.Open);
                poletip.Image = image;
                //((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            }

           


            //update
            if (selectedPoleTip != Guid.Empty)
            {
                //ed.WriteMessage("6 \n");
                if (txtImage.Text == string.Empty && pictureBox1.Image != null)
                {
                    poletip.Image = image;
                    //ed.WriteMessage("7 \n");
                }
            }






            ArrayList EEquipment = new ArrayList();
            for (int j = 0; j < gvPoleTip.Rows.Count; j++)
            {

                if (Convert.ToInt32(gvPoleTip.Rows[j].Cells[2].Value.ToString()) != 0)
                {
                    Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                    _EProductPackage.XCode = new Guid(gvPoleTip.Rows[j].Cells[0].Value.ToString());
                    _EProductPackage.Count = Convert.ToInt32(gvPoleTip.Rows[j].Cells[2].Value.ToString());
                    _EProductPackage.TableType = Convert.ToInt16(Atend.Control.Enum.ProductType.Consol);  
                    EEquipment.Add(_EProductPackage);
                }
            }
            ed.WriteMessage("bb\n");
            poletip.EquipmentList = EEquipment;
            if (selectedPoleTip == Guid.Empty)
            {
                if (poletip.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");

            }
            else
            {
                poletip.XCode = selectedPoleTip;
                poletip.Code = SelectedPoleTipCode;
                if (poletip.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");

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

        private void gvPole_Click(object sender, EventArgs e)
        {
            if (gvPole.Rows.Count > 0)
            {
                IsSelectPole = true;
                Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.SelectByXCode(new Guid(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value.ToString()));
                txtPoleName.Text = pole.Name;
                txtHeight.Text = Convert.ToString(Math.Round(pole.Height, 4));
                txtPower.Text = Convert.ToString(Math.Round(pole.Power, 4));
            }
            else
            {
                IsSelectPole = false;
            }

           
        }

        private void toolStripLabel8_Click(object sender, EventArgs e)
        {
            if (CheckStatuseOfAccessChangeDefault())
                Delete();
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(selectedPoleTip, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(selectedPoleTip);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //ed.WriteMessage("DELETE\n");
                if (selectedPoleTip != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EPoleTip.DeleteX(selectedPoleTip))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }

        }

        public void BindDataToOwnControl(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Base.Equipment.EPoleTip poletip = Atend.Base.Equipment.EPoleTip.SelectByXCode(XCode);
            Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.SelectByXCode(poletip.PoleXCode);
            txtName.Text = poletip.Name;
            txtComment.Text = poletip.Comment;
            nudHalter.Value = poletip.HalterCount;
            txtPoleName.Text = pole.Name;
            txtHeight.Text = Convert.ToString(Math.Round(pole.Height,2));
            txtPower.Text = Convert.ToString(Math.Round(pole.Power,2));
           ed.WriteMessage("HalterID={0}\n",poletip.HalterXID);
            cboHalterType.SelectedValue = poletip.HalterXID;
            selectedPoleTip = XCode;
            SelectedPoleTipCode = poletip.Code;
            tsbIsDefault.Checked = poletip.IsDefault;

            Byte[] byteBLOBData = new Byte[0];
            byteBLOBData = (Byte[])(poletip.Image);
            MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
            image = poletip.Image;
            pictureBox1.Image = Image.FromStream(stmBLOBData);

            BindGridEquipment(gvPoleTip);


            gvPole.AutoGenerateColumns = false;
            gvPole.DataSource = Atend.Base.Equipment.EPole.DrawSearchLocal(-1, -1, -1);

            for (int i = 0; i < gvPole.Rows.Count; i++)
            {
                gvPole.Rows[i].Selected = false;
            }

            for (int i = 0; i < gvPole.Rows.Count; i++)
            {
                //gvPole.Rows[i].Selected = false;
                if (gvPole.Rows[i].Cells[0].Value.ToString() == poletip.PoleXCode.ToString())
                {
                    ed.WriteMessage("Find\n");
                    gvPole.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    gvPole.Rows[i].Selected = true;
                   
                }
            }
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
            ed.WriteMessage("ll\n");
            ed.WriteMessage("Count={0}\n", Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX.Count);
            for (int i = 0; i < Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX.Count; i++)
            {
                //string s = Atend.Base.Equipment.EPoleTip.nodeKeysEPackage[i].ToString();
                ed.WriteMessage("i={0},Key={1}\n", i.ToString(), Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX[i]);
                gvPoleTip.Rows.Add();
                gvPoleTip.Rows[gvPoleTip.Rows.Count - 1].Cells[0].Value = Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX[i].ToString();
                gvPoleTip.Rows[gvPoleTip.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPoleTip.nodeCountEPackageX[i].ToString();
                Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(Atend.Base.Equipment.EPoleTip.nodeKeysEPackageX[i].ToString()));
                gvPoleTip.Rows[gvPoleTip.Rows.Count - 1].Cells[1].Value = consol.Name;
            }


        }

        private void toolStripLabel9_Click(object sender, EventArgs e)
        {
            frmPoleTipSearch02 search = new frmPoleTipSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(search);
        }

        private void toolStripLabel10_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (selectedPoleTip != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip), selectedPoleTip))
                {
                    Atend.Base.Equipment.EPoleTip pole = Atend.Base.Equipment.EPoleTip.SelectByXCode(selectedPoleTip);
                    code = pole.Code;
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
            //if (selectedPoleTip != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EPoleTip.ShareOnServer(selectedPoleTip))
            //        MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
            //    else
            //        MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            //}
            //else
            //    MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //openFileDialog1.Filter = "Image Files (*.jpg)"; 
                txtImage.Text = openFileDialog1.FileName;
                FileStream fs = new FileStream(txtImage.Text, FileMode.Open);
                System.Drawing.Image myimage = System.Drawing.Bitmap.FromStream(fs);
                pictureBox1.Image = myimage;
                fs.Dispose();
            }
        }

    }
}