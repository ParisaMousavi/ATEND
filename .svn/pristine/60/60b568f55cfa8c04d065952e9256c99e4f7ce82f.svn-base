using System;   
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.IO;


namespace Atend.Equipment
{
    public partial class frmConsol02 : Form
    {
        public int productCode = -1;
        Guid selectedConsolXCode = Guid.Empty;
        byte[] image;
        public bool IsDefault = false;
        int Code = -1;
        bool ForceToClose = false;

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPoleTip02));

        public frmConsol02()
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
            Atend.Control.Common.selectedProductCode = -1;
        }

        public void BindDataToOwnControl(Guid XCode)
        {
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.SelectByXCode(XCode);

            Atend.Control.Common.selectedProductCode = consol.ProductCode;
            SelectProduct();
            selectedConsolXCode = XCode;
            //nudLenght.Value = Convert.ToDecimal(consol.Length);
            txtLenght.Text = Convert.ToString(Math.Round(consol.Length, 4));
            cboVoltagelevel.Text = consol.VoltageLevel.ToString();
            cboType.SelectedIndex = consol.Type;
            cboConsolType.SelectedIndex = consol.ConsolType;
            txtComment.Text = consol.Comment;
            txtName.Text = consol.Name;
            txtDistanceCrossArm.Text = Convert.ToString(Math.Round(consol.DistanceCrossArm, 4));
            Byte[] byteBLOBData = new Byte[0];
            byteBLOBData = (Byte[])(consol.Image);
            MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
            image = consol.Image;
            Code = consol.Code;
            pictureBox1.Image = Image.FromStream(stmBLOBData);
            txtDistancePhase.Text = Convert.ToString(Math.Round(consol.DistancePhase, 4));
            tsbIsDefault.Checked = consol.IsDefault;
            BindTreeViwAndGridEquipment(tvEquipment, gvSelectedEquipment);
            BindTreeViwAndGrid(tvOperation, gvOperation);
        }

        private void BindTreeViwAndGrid(TreeView treeView, DataGridView dataGridView)
        {
            ClearCheckAndGrid(tvOperation, gvOperation);
            ClearCheckAndGrid(tvOperation, gvOperation);
            dataGridView.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EConsol.nodeKeys.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EConsol.nodeKeys[i]);
                string s = Operation.ProductID.ToString();

                foreach (TreeNode rootnode in tvOperation.Nodes)
                {
                    if (rootnode.Tag.ToString() == s)
                    {
                        rootnode.Checked = true;
                        gvOperation.Rows.Add();
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[0].Value = rootnode.Tag;
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[1].Value = rootnode.Text;
                        //gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPole.nodeCount[i].ToString();
                        Atend.Base.Base.BUnit Unit = Atend.Base.Base.BUnit.Select_ByProductID(Convert.ToInt32(rootnode.Tag.ToString()));
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[2].Value = Unit.Name;
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[3].Value = Operation.Count;
                    }

                }
            }
        }

        private void Reset()
        {
            txtName.Text = string.Empty;
            selectedConsolXCode = Guid.Empty;
            txtLenght.Text = string.Empty;
            txtDistancePhase.Text = string.Empty;
            txtImage.Text = string.Empty;
            txtDistanceCrossArm.Text = string.Empty;
            txtComment.Text = string.Empty;
            IsDefault = false;
            tsbIsDefault.Checked = false;
            Atend.Control.Common.selectedProductCode = -1;
            txtBackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
            pictureBox1.Image = Image.FromFile(Atend.Control.Common.fullPath + "\\Consol.jpg");
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            ClearCheckAndGrid(tvOperation, gvOperation);
            productCode = -1;
            Code = -1;


        }

        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        private void Save()
        {
            txtName.Focus();
            ArrayList EPackageProduct = new ArrayList();
            Atend.Base.Equipment.EConsol consol = new Atend.Base.Equipment.EConsol();
            consol.Length = Convert.ToDouble(txtLenght.Text);
            consol.Type = Convert.ToByte(cboType.SelectedIndex);
            consol.VoltageLevel = Convert.ToInt32(cboVoltagelevel.Text);
            consol.ConsolType = Convert.ToByte(cboConsolType.SelectedIndex);
            consol.ProductCode = Atend.Control.Common.selectedProductCode;
            consol.Comment = txtComment.Text;
            consol.Name = txtName.Text;
            consol.DistancePhase = Convert.ToDouble(txtDistancePhase.Text);
            consol.DistanceCrossArm = Convert.ToDouble(txtDistanceCrossArm.Text);
            consol.IsDefault = IsDefault;
            consol.Code = Code;
            System.Drawing.Image image1 = pictureBox1.Image;
            //ed.WriteMessage("1 \n");
            //ed.WriteMessage("picture picked \n");
            //MemoryStream ms = new MemoryStream();
            //image1.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            //ed.WriteMessage("image saved \n");
            //consol.Image = ms.ToArray();
            //BinaryReader br = new BinaryReader(ms);
            //consol.Image = br.ReadBytes((Int32)br.BaseStream.Length);

            //ed.WriteMessage("picture send to consol class \n");




            consol.Image = null;
            FileStream fs;
            if (txtImage.Text != String.Empty)
            {
                fs = new FileStream(txtImage.Text, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                consol.Image = br.ReadBytes((Int32)br.BaseStream.Length);
                fs.Dispose();
            }
            else
            {
                //fs = new FileStream(Atend.Control.Common.fullPath + "\\Consol1.jpg", FileMode.Open);
                consol.Image = image;

            }

           


            //update
            if (selectedConsolXCode != Guid.Empty)
            {
                //ed.WriteMessage("6 \n");
                if (txtImage.Text == string.Empty && pictureBox1.Image != null)
                {
                    consol.Image = image;
                    //ed.WriteMessage("7 \n");
                }
            }
            //ed.WriteMessage("Begin \n");



            for (int j = 0; j < gvSelectedEquipment.Rows.Count; j++)
            {
                if (Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString()) != 0)
                {
                    Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                    _EProductPackage.XCode = new Guid(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                    _EProductPackage.Count = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                    _EProductPackage.TableType = Convert.ToInt16(gvSelectedEquipment.Rows[j].Cells[3].Value.ToString());
                    EPackageProduct.Add(_EProductPackage);
                }
            }
            consol.EquipmentList = EPackageProduct;
            //ed.WriteMessage("selectedConsol" + selectedConsol.ToString());

            ArrayList EOperation = new ArrayList();
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);
            }
            consol.OperationList = EOperation;
            if (selectedConsolXCode == Guid.Empty)
            {
                if (consol.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");

            }
            else
            {
                consol.XCode = selectedConsolXCode;
                //ed.WriteMessage("consol.code:" + selectedConsol.ToString());
                //MessageBox.Show("GoToUpdate");
                if (consol.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");

            }
        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (selectedConsolXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EConsol Equip = Atend.Base.Equipment.EConsol.SelectByXCode(selectedConsolXCode);
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

            //ed.WriteMessage("i am in validation \n");


            btnInsert.Focus();
            //if (Atend.Control.Common.selectedProductCode == -1)
            //{
            //    MessageBox.Show("لطفا ابتدا یک کالا را از پشتیبان انتخاب کنید", "خطا");

            //    return false;
            //}
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفا نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }

            if (txtImage.Text != string.Empty )
            {
                if (!System.IO.File.Exists(txtImage.Text))
                {
                    MessageBox.Show("فایل انتخاب شده موجود نمی باشد", "خطا");
                    btnAddImage.Focus();
                    return false;

                }
            }

            if (Atend.Base.Equipment.EConsol.SearchByName(txtName.Text) == true && selectedConsolXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtLenght.Text))
            {
                MessageBox.Show("لطفاً طول کنسول را مشخص نمایید", "خطا");
                txtLenght.Focus();
                return false;

            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtLenght.Text))
            {
                MessageBox.Show("لطفاً طول کنسول را با فرمت مناسب وارد نمایید", "خطا");
                txtLenght.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDistancePhase.Text))
            {
                MessageBox.Show("لطفاً فاصله فازی  را مشخص نمایید", "خطا");
                txtDistancePhase.Focus();
                return false;

            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtDistancePhase.Text))
            {
                MessageBox.Show("لطفاً فاصله فازی را با فرمت مناسب وارد نمایید", "خطا");
                txtDistancePhase.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDistanceCrossArm.Text))
            {
                MessageBox.Show("لطفاً فاصله کراس آرم تا راس تیر  را مشخص نمایید", "خطا");
                txtDistanceCrossArm.Focus();
                return false;

            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtDistanceCrossArm.Text))
            {
                MessageBox.Show("لطفاً فاصله کراس آرم تا راس تیر را با فرمت مناسب وارد نمایید", "خطا");
                txtDistanceCrossArm.Focus();
                return false;
            }

            //if (nudLenght.Value == 0)
            //{
            //    MessageBox.Show("لطفاً طول کنسول را با فرمت مناسب وارد نمایید", "خطا");
            //    nudLenght.Focus();
            //    return false;
            //}
            gvSelectedEquipment.Refresh();
            //if (gvSelectedEquipment.Rows.Count == 0)
            //{
            //    MessageBox.Show("لطفا تجهیزات داخلی را مشخص کنید", "خطا");
            //    gvSelectedEquipment.Focus();
            //    return false;
            //}
            for (int i = 0; i < gvSelectedEquipment.Rows.Count; i++)
            {
                if (gvSelectedEquipment.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("لطفا تعداد تجهیزات داخلی را مشخص نمایید", "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.Int32Converter(gvSelectedEquipment.Rows[i].Cells[2].Value.ToString()))
                {
                    MessageBox.Show("لطفا تعداد تجهیزات داخلی را با فرمت مناسب وارد نمایید", "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }

            }
            Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.CheckForExist(Convert.ToDouble(txtLenght.Text), Convert.ToByte(cboType.SelectedIndex), Convert.ToDouble(txtDistancePhase.Text),
                                                                                            Convert.ToDouble(txtDistanceCrossArm.Text), Convert.ToByte(cboConsolType.SelectedIndex), Convert.ToInt32(cboVoltagelevel.Text));
            if (consol.Code != -1 && selectedConsolXCode == Guid.Empty)
            {
                if (MessageBox.Show("کنسول با مشخصات داده شده موجود میباشد\n\n کنسول با مشخصات فوق  : " + consol.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    txtLenght.Focus();
                    return false;
                }
            }

            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvOperation, 3))
            {
                MessageBox.Show("لطفا تعداد آماده سازی را با فرمت مناسب وارد نمایید", "خطا");
                gvOperation.Focus();
                return false;
            }
            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvSelectedEquipment, 2))
            {
                MessageBox.Show("لطفا تعداد تجهیزات جانبی را با فرمت مناسب وارد نمایید", "خطا");
                gvSelectedEquipment.Focus();
                return false;
            }

            for (int j = 0; j < gvSelectedEquipment.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                _EProductPackage.XCode = new Guid(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.Count = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedEquipment.Rows[j].Cells[3].Value.ToString());

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(selectedConsolXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }
            }

            return CheckStatuseOfAccessChangeDefault();
            // return true;
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(selectedConsolXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(selectedConsolXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (selectedConsolXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EConsol.DeleteX(selectedConsolXCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }

        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ed.WriteMessage("start saving method \n");
            if (Validation())
            {
                Save();
            }
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckStatuseOfAccessChangeDefault())
                Delete();
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsolSearch02 search = new frmConsolSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(search);
        }

        private void frmAirPost_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(productCode);
            //txtName.Text = product.Name;
            //ReadEquipGroups(tvEquipment);
            cboType.SelectedIndex = 0;
            cboConsolType.SelectedIndex = 0;
            cboVoltagelevel.SelectedIndex = 0;
            BindDataToTreeView();

            //string PicturePath = Atend.Control.Common.fullPath + "\\Consol.jpg";
            ////ed.WriteMessage(Environment.CurrentDirectory + "\\Consol.jpg");
            //pictureBox1.Image = Image.FromFile(PicturePath);
            image = imageToByteArray(((System.Drawing.Image)(resources.GetObject("pictureBox2.Image"))));

            BindDataToTreeViewOperation();
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void BindDataToTreeViewOperation()
        {
            DataTable ProductType = Atend.Base.Base.BProduct.SelectByTypeX(Convert.ToInt32(Atend.Control.Enum.ProductType.Operation));
            foreach (DataRow dr in ProductType.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = dr["Name"].ToString();
                node.Tag = dr["ID"].ToString();
                tvOperation.Nodes.Add(node);
            }
        }

        private void BindDataToTreeView()
        {
            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvEquipment);

            #region
            //for (int i = 0; i < tvEquipment.Nodes.Count; i++)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage("RootNode:" + Convert.ToInt32(tvEquipment.Nodes[i].Tag.ToString()) + "\n");
            //    switch ((Atend.Control.Enum.ProductType)Convert.ToInt32(tvEquipment.Nodes[i].Tag.ToString()))
            //    {
            //        case Atend.Control.Enum.ProductType.Pole:
            //            {
            //                DataTable db = Atend.Base.Equipment.EPole.SelectAll();
            //                //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Pole).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.DB:
            //            {
            //                DataTable db = Atend.Base.Equipment.EDB.SelectAll();
            //                //MessageBox.Show("DBRow" + db.Rows.Count.ToString());

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.DB).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;

            //        case Atend.Control.Enum.ProductType.AuoKey3p:
            //            {
            //                DataTable db = Atend.Base.Equipment.EAutoKey_3p.SelectAll();
            //                //MessageBox.Show("AuoKey3pRow" + db.Rows.Count.ToString());

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.AuoKey3p).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.Breaker:
            //            {
            //                DataTable db = Atend.Base.Equipment.EBreaker.SelectAll();
            //                //MessageBox.Show("BreakerRow" + db.Rows.Count.ToString());
            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.Bus:
            //            {
            //                DataTable db = Atend.Base.Equipment.EBus.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Bus).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.Conductor:
            //            {
            //                DataTable db = Atend.Base.Equipment.EConductor.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.CatOut:
            //            {
            //                DataTable db = Atend.Base.Equipment.ECatOut.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.CT:
            //            {
            //                DataTable db = Atend.Base.Equipment.ECT.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.CT).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.HeaderCabel:
            //            {
            //                DataTable db = Atend.Base.Equipment.EHeaderCabel.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;

            //        case Atend.Control.Enum.ProductType.Disconnector:
            //            {
            //                DataTable db = Atend.Base.Equipment.EDisconnector.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.Rod:
            //            {
            //                DataTable db = Atend.Base.Equipment.ERod.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Rod).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.Countor:
            //            {
            //                DataTable db = Atend.Base.Equipment.ECountor.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Countor).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.MiddleJackPanel:
            //            {
            //                DataTable db = Atend.Base.Equipment.EJAckPanel.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.PhotoCell:
            //            {
            //                DataTable db = Atend.Base.Equipment.EPhotoCell.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.PhotoCell).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.Phuse:
            //            {
            //                DataTable db = Atend.Base.Equipment.EPhuse.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.StreetBox:
            //            {
            //                DataTable db = Atend.Base.Equipment.EStreetBox.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.Transformer:
            //            {
            //                DataTable db = Atend.Base.Equipment.ETransformer.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.PT:
            //            {
            //                DataTable db = Atend.Base.Equipment.EPT.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.PT).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.Insulator:
            //            {
            //                DataTable db = Atend.Base.Equipment.EInsulator.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.ReCloser:
            //            {
            //                DataTable db = Atend.Base.Equipment.EReCloser.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.PhuseKey:
            //            {
            //                DataTable db = Atend.Base.Equipment.EPhuseKey.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.Consol:
            //            {
            //                DataTable db = Atend.Base.Equipment.EConsol.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.PhusePole:
            //            {
            //                DataTable db = Atend.Base.Equipment.EPhusePole.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole).ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        default:
            //            {
            //                DataTable db = Atend.Base.Base.BProduct.SelectByType(Convert.ToInt32(tvEquipment.Nodes[i].Tag.ToString()));
            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["ID"].ToString();
            //                    node.Name = dr["Type"].ToString();
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //                break;
            //            }


            //    }



            //}

            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //for (int i = 0; i < tvEquipment.Nodes.Count; i++)
            //{
            //    switch ((Atend.Control.Enum.ProductType)Convert.ToInt32(tvEquipment.Nodes[i].Tag.ToString()))
            //    {
            //        case Atend.Control.Enum.ProductType.Insulator:
            //            {
            //                DataTable db = Atend.Base.Equipment.EInsulator.SelectAll();

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["Code"].ToString();
            //                    node.Name = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator).ToString();
            //                    ed.WriteMessage(node.Name.ToString());
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.InsulatorPipe:
            //            {
            //                DataTable db = Atend.Base.Base.BProduct.SelectByType(Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorPipe));

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["ID"].ToString();
            //                    node.Name = "0";
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;
            //        case Atend.Control.Enum.ProductType.InsulatorChain:
            //            {
            //                DataTable db = Atend.Base.Base.BProduct.SelectByType(Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain));

            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    TreeNode node = new TreeNode();
            //                    node.Text = dr["Name"].ToString();
            //                    node.Tag = dr["ID"].ToString();
            //                    node.Name = "0";
            //                    tvEquipment.Nodes[i].Nodes.Add(node);

            //                }
            //            }
            //            break;

            //    }
            //}

            #endregion

        }

        private void ClearCheckAndGrid(TreeView treeView, DataGridView dataGridView)
        {
            foreach (TreeNode rootNode in treeView.Nodes)
            {
                rootNode.Checked = false;
                rootNode.BackColor = Color.White;
                foreach (TreeNode childNode in rootNode.Nodes)
                {
                    childNode.Checked = false;
                }
            }



            for (int i = dataGridView.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView.Rows.RemoveAt(i);
            }
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        private void BindTreeViwAndGridEquipment(TreeView treeView, DataGridView dataGridView)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            dataGridView.Refresh();
            //ed.WriteMessage("nodeKeysEPackage.Count:" + Atend.Base.Equipment.EConsol.nodeKeysEPackage.Count.ToString() + "\n");
            for (int i = 0; i < Atend.Base.Equipment.EConsol.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EConsol.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {

                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        //ed.WriteMessage("nodeTypeEPackage:" + Atend.Base.Equipment.EConsol.nodeTypeEPackage[i].ToString() + "\n");
                        //ed.WriteMessage("rootnode.Tag:" + rootnode.Tag.ToString() + "\n");
                        if (Atend.Base.Equipment.EConsol.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            //ed.WriteMessage(string.Format(" child tag : {0} , s: {1} \n", chileNode.Tag.ToString(), s));
                            if (chileNode.Tag.ToString() == s)
                            {
                                //ed.WriteMessage("I am in the if \n");
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquipment.Rows.Add();
                                //ed.WriteMessage("Child tag : " +  chileNode.Tag.ToString() + "\n");
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                //ed.WriteMessage("Child tag : " + chileNode.Text.ToString() + "\n");
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EConsol.nodeCountEPackageX[i].ToString();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[3].Value = chileNode.Name;

                            }
                        }

                    }

                }



            }
        }

        private void tvEquipment_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!e.Node.Checked)
            {

                for (int i = 0; i < e.Node.Nodes.Count; i++)
                {
                    e.Node.Nodes[i].Checked = true;
                }
            }
            else
            {

                for (int i = 0; i < e.Node.Nodes.Count; i++)
                {
                    e.Node.Nodes[i].Checked = false;
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //gvSelectedEquipment.Rows.Clear();

            Boolean canAdd = true;
            for (int i = 0; i < tvEquipment.Nodes.Count; i++)
            {
                for (int j = 0; j < tvEquipment.Nodes[i].Nodes.Count; j++)
                {
                    if (tvEquipment.Nodes[i].Nodes[j].Checked)
                    {
                        canAdd = true;
                        for (int k = 0; k < gvSelectedEquipment.Rows.Count; k++)
                        {
                            if ((gvSelectedEquipment.Rows[k].Cells[0].Value.ToString() == tvEquipment.Nodes[i].Nodes[j].Tag.ToString()) && (Convert.ToInt32(gvSelectedEquipment.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvEquipment.Nodes[i].Nodes[j].Name.ToString())))
                            {
                                canAdd = false;
                                //ed.WriteMessage("No Allow To Add Row\n");
                            }
                        }
                        if (canAdd)
                        {
                            //ed.WriteMessage("AddRow\n");
                            gvSelectedEquipment.Rows.Add();
                            gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = tvEquipment.Nodes[i].Nodes[j].Tag.ToString();
                            gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = tvEquipment.Nodes[i].Nodes[j].Text.ToString();
                            gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[3].Value = tvEquipment.Nodes[i].Nodes[j].Name.ToString();
                            gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = 1;

                        }

                    }
                    else
                    {
                        for (int k = 0; k < gvSelectedEquipment.Rows.Count; k++)
                        {
                            if ((gvSelectedEquipment.Rows[k].Cells[0].Value.ToString() == tvEquipment.Nodes[i].Nodes[j].Tag.ToString()) && (Convert.ToInt32(gvSelectedEquipment.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvEquipment.Nodes[i].Nodes[j].Name.ToString())))
                            {
                                //ed.WriteMessage("Name To Delete" + gvSelectedEquipment.Rows[k].Cells[1].Value.ToString() + "\n");
                                gvSelectedEquipment.Rows.RemoveAt(k);

                            }
                        }

                    }
                }
            }





            //for (int i = 0; i < tvEquipment.Nodes.Count; i++)
            //{
            //    for (int j = 0; j < tvEquipment.Nodes[i].Nodes.Count; j++)
            //    {
            //        if (tvEquipment.Nodes[i].Nodes[j].Checked)
            //        {
            //            gvSelectedEquipment.Rows.Add();
            //            gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = tvEquipment.Nodes[i].Nodes[j].Tag.ToString();
            //            gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = tvEquipment.Nodes[i].Nodes[j].Text.ToString();
            //            gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[3].Value = tvEquipment.Nodes[i].Nodes[j].Name.ToString();

            //        }
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(gvSelectedEquipment.Rows[gvSelectedEquipment.CurrentRow.Index].Cells[2].Value.ToString());
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {

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

        private void SelectProduct()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(Atend.Control.Common.selectedProductCode);
            txtName.Text = product.Name;
            txtBackUpName.Text = product.Name;
            txtCode.Text = product.Code.ToString();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.Consol);

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
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

            if (selectedConsolXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.Consol), selectedConsolXCode))
                {
                    Atend.Base.Equipment.EConsol  Consol = Atend.Base.Equipment.EConsol.SelectByXCode(selectedConsolXCode);
                    Code = Consol.Code;
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

            //if (selectedConsolXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EConsol.ShareOnServer(selectedConsolXCode))
            //    {
            //        Atend.Base.Equipment.EConsol c1 = Atend.Base.Equipment.EConsol.SelectByXCode(selectedConsolXCode);
            //        Code = c1.Code;
            //        MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
            //    }
            //    else
            //        MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            //}
            //else
            //    MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvSelectedEquipment.Rows.Count > 0)
            {
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {
                    foreach (TreeNode childNode in rootnode.Nodes)
                    {
                        if (childNode.Tag.ToString() == gvSelectedEquipment.Rows[gvSelectedEquipment.CurrentRow.Index].Cells[0].Value.ToString())
                        {
                            childNode.Checked = false;
                        }
                    }
                }

                gvSelectedEquipment.Rows.RemoveAt(gvSelectedEquipment.CurrentRow.Index);
            }
        }

        private void txtOperationName_TextChanged(object sender, EventArgs e)
        {
            SearchOperation();
        }

        private void SearchOperation()
        {
            tvOperation.Nodes.Clear();
            DataTable ProductType = Atend.Base.Base.BProduct.SelectByNameTypeX(Convert.ToInt32(Atend.Control.Enum.ProductType.Operation), txtOperationName.Text);

            foreach (DataRow dr in ProductType.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = dr["Name"].ToString();
                node.Tag = dr["ID"].ToString();
                tvOperation.Nodes.Add(node);
            }
        }

        private void btnInsertOperation_Click(object sender, EventArgs e)
        {
            //gvOperation.Rows.Clear();
            for (int i = 0; i < tvOperation.Nodes.Count; i++)
            {

                if (tvOperation.Nodes[i].Checked)
                {
                    bool sw = false;
                    for (int j = 0; j < gvOperation.Rows.Count; j++)
                    {
                        if (gvOperation.Rows[j].Cells[0].Value.ToString() == tvOperation.Nodes[i].Tag.ToString())
                            sw = true;
                    }

                    if (!sw)
                    {
                        gvOperation.Rows.Add();
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[0].Value = tvOperation.Nodes[i].Tag.ToString();
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[1].Value = tvOperation.Nodes[i].Text.ToString();
                        Atend.Base.Base.BUnit Unit = Atend.Base.Base.BUnit.Select_ByProductID(Convert.ToInt32(tvOperation.Nodes[i].Tag.ToString()));
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[2].Value = Unit.Name;
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[3].Value = 1;
                    }
                }
            }
        }

        private void btnDeleteOperation_Click(object sender, EventArgs e)
        {
            if (gvOperation.Rows.Count > 0)
            {
                foreach (TreeNode rootnode in tvOperation.Nodes)
                {
                    if (rootnode.Tag.ToString() == gvOperation.Rows[gvOperation.CurrentRow.Index].Cells[0].Value.ToString())
                    {
                        rootnode.Checked = false;
                    }
                }

                gvOperation.Rows.RemoveAt(gvOperation.CurrentRow.Index);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

    }
}