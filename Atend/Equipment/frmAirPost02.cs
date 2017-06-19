using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Xml;
using System.Collections;
using System.IO;

namespace Atend.Equipment
{
    public partial class frmAirPost02 : Form
    {
        public int productCode = -1;
        Guid selectedPostXCode = Guid.Empty;
        int SelectedPostCode = 0;
        int Code = -1;
        public bool IsDefault = false;
        byte[] image;
        bool ForceToClose = false;

        public frmAirPost02()
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

        public void BindDataToOwnControl(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(Code.ToString()+"\n");
            Atend.Base.Equipment.EAirPost airPost = Atend.Base.Equipment.EAirPost.SelectByXCode(XCode);

            txtName.Text = airPost.Name;
            productCode = airPost.ProductCode;
            selectedPostXCode = new Guid(XCode.ToString());
            txtCapacity.Text = Convert.ToString(Math.Round(airPost.Capacity, 2));

            //nudOutputFeeder.Value = Convert.ToDecimal(airPost.OutPutFeederCount);
            txtComment.Text = airPost.Comment;
            SelectedPostCode = airPost.Code;
            //if (airPost.IsDefault == 0)
            //    tsbIsDefault.CheckState = CheckState.Unchecked;
            //else if(airPost.IsDefault == 1 || airPost.IsDefault == 2)
            //    tsbIsDefault.CheckState = CheckState.Checked;

            tsbIsDefault.Checked = airPost.IsDefault;

            Byte[] byteBLOBData = new Byte[0];
            byteBLOBData = (Byte[])(airPost.Image);
            MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
            image = airPost.Image;

            Code = airPost.Code;
            pictureBox1.Image = Image.FromStream(stmBLOBData);

            BindTreeViwAndGridEquipment(tvEquipment, gvSelectedEquip);
            BindTreeViwAndGridOperation(tvOperation, gvOperation);
            //BindTreeViwAndGridContainer(tvContainer, gvSelectedContainer);
        }

        private void BindTreeViwAndGridOperation(TreeView treeView, DataGridView dataGridView)
        {
            //dataGridView.Rows.Clear();
            //dataGridView.Update();
            ClearCheckAndGrid(tvOperation, gvOperation);
            ClearCheckAndGrid(tvOperation, gvOperation);
            dataGridView.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EAirPost.nodeKeys.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EAirPost.nodeKeys[i]);
                string s = Operation.ProductID.ToString();

                foreach (TreeNode rootnode in tvOperation.Nodes)
                {
                    if (rootnode.Tag.ToString() == s)
                    {
                        rootnode.Checked = true;
                        gvOperation.Rows.Add();
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[0].Value = rootnode.Tag;
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[1].Value = rootnode.Text;
                        Atend.Base.Base.BUnit Unit = Atend.Base.Base.BUnit.Select_ByProductID(Convert.ToInt32(rootnode.Tag.ToString()));
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[2].Value = Unit.Name;
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[3].Value = Operation.Count;
                        //gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPole.nodeCount[i].ToString();

                    }

                }
            }
        }

        private void Reset()
        {
            selectedPostXCode = Guid.Empty;
            txtName.Text = string.Empty;
            txtCapacity.Text = string.Empty;
            //nudOutputFeeder.Value = 0;
            txtComment.Text = string.Empty;
            txtCapacity.Focus();
            //ClearCheckAndGrid(tvContainer, gvSelectedContainer);
            ClearCheckAndGrid(tvEquipment, gvSelectedEquip);
            ClearCheckAndGrid(tvOperation, gvOperation);
            IsDefault = false;
            tsbIsDefault.Checked = false;
            txtImage.Text = string.Empty;
            pictureBox1.Image = Image.FromFile(Atend.Control.Common.fullPath + "\\Consol.jpg");
            productCode = -1;
            Code = -1;

        }

        private void Save()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            txtName.Focus();
            btnInsertEquip.Focus();
            Atend.Base.Equipment.EAirPost airPost = new Atend.Base.Equipment.EAirPost();
            ArrayList EEquipment = new ArrayList();
            //ArrayList EContainer = new ArrayList();
            airPost.Capacity = Convert.ToDouble(txtCapacity.Text);
            //airPost.OutPutFeederCount = Convert.ToInt32(nudOutputFeeder.Value);
            airPost.ProductCode = productCode;
            airPost.Comment = txtComment.Text;
            //airPost.XCode = selectedPostXCode;
            //airPost.ContainerCode = 0;
            airPost.Name = txtName.Text;
            airPost.Code = Code;
            airPost.IsDefault = IsDefault;
            //ed.WriteMessage("aaab \n");
            //SaveEquipment
            for (int j = 0; j < gvSelectedEquip.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();

                //_EProductPackage.ProductCode = 0;
                _EProductPackage.XCode = new Guid(gvSelectedEquip.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.Count = Convert.ToInt32(gvSelectedEquip.Rows[j].Cells[2].Value.ToString());
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedEquip.Rows[j].Cells[3].Value.ToString());
                EEquipment.Add(_EProductPackage);
                //ed.WriteMessage("aaa \n");
            }
            airPost.EquipmentList = EEquipment;
            //Operation
            ArrayList EOperation = new ArrayList();
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);
            }
            airPost.OperationList = EOperation;
            airPost.Image = null;
            FileStream fs;
            if (txtImage.Text != String.Empty)
            {
                fs = new FileStream(txtImage.Text, FileMode.Open);
            }
            else
            {
                fs = new FileStream(Atend.Control.Common.fullPath + "\\Consol1.jpg", FileMode.Open);
            }
            BinaryReader br = new BinaryReader(fs);
            airPost.Image = br.ReadBytes((Int32)br.BaseStream.Length);
            fs.Dispose();
            if (selectedPostXCode == Guid.Empty)
            {
                if (airPost.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");

            }
            else
            {
                airPost.XCode = selectedPostXCode;
                //airPost.Code = SelectedPostCode;
                
                if (airPost.UpdateX())
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
                if (selectedPostXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EAirPost Equip = Atend.Base.Equipment.EAirPost.SelectByXCode(selectedPostXCode);
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
            btnInsertEquip.Focus();
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفاً نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            if (Atend.Base.Equipment.EAirPost.SearchByName(txtName.Text) == true && selectedPostXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCapacity.Text))
            {
                MessageBox.Show("لطفاً ظریف پست را مشخص نمایید", "خطا");
                txtCapacity.Focus();
                return false;

            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtCapacity.Text))
            {
                MessageBox.Show("لطفاً ظرفیت پست را با فرمت مناسب وارد نمایید", "خطا");
                txtCapacity.Focus();
                txtCapacity.Select(0, txtCapacity.Text.Length);
                return false;
            }
            //if (nudOutputFeeder.Value == 0)
            //{
            //    MessageBox.Show("لطفاً تعداد فیدر خروجی را مشخص نمایید","خطا");
            //    nudOutputFeeder.Focus();
            //    return false;
            //}


            gvSelectedEquip.Refresh();
            if (gvSelectedEquip.Rows.Count == 0)
            {
                MessageBox.Show("لطفا تجهیزات داخلی را مشخص کنید", "خطا");
                gvSelectedEquip.Focus();
                return false;
            }

            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvOperation, 3))
            {
                MessageBox.Show("لطفا تعداد آماده سازی را با فرمت مناسب وارد نمایید", "خطا");
                gvOperation.Focus();
                return false;
            }
            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvSelectedEquip, 2))
            {
                MessageBox.Show("لطفا تعداد تجهیزات جانبی را با فرمت مناسب وارد نمایید", "خطا");
                gvSelectedEquip.Focus();
                return false;
            }


            //****************
            for (int j = 0; j < gvSelectedEquip.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                _EProductPackage.XCode = new Guid(gvSelectedEquip.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.Count = Convert.ToInt32(gvSelectedEquip.Rows[j].Cells[2].Value.ToString());
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedEquip.Rows[j].Cells[3].Value.ToString());

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(selectedPostXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedEquip.Focus();
                    return false;
                }
            }
            //****************


            //for (int i = 0; i < gvSelectedEquip.Rows.Count; i++)
            //{
            //    if (gvSelectedEquip.Rows[i].Cells[2].Value == null)
            //    {
            //        MessageBox.Show("لطفا تعداد تجهیزات داخلی را مشخص نمایید", "خطا");
            //        gvSelectedEquip.Focus();
            //        return false;
            //    }
            //    if (!Atend.Control.NumericValidation.Int32Converter(gvSelectedEquip.Rows[i].Cells[2].Value.ToString()))
            //    {
            //        MessageBox.Show("لطفا تعداد تجهیزات داخلی را با فرمت مناسب وارد نمایید", "خطا");
            //        gvSelectedEquip.Focus();
            //        return false;
            //    }

            //}
            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(selectedPostXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(selectedPostXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (selectedPostXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EAirPost.DeleteX(selectedPostXCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }


        }

        private void BindDataToTreeView()
        {

            //Atend.Global.Utility.UBinding.BindDataToTreeViewXForPost(tvEquipment, 0);
            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvEquipment);

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
            //gvSelectedProduct.Rows.Clear();
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        private void BindTreeViwAndGridEquipment(TreeView treeView, DataGridView dataGridView)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            ClearCheckAndGrid(tvEquipment, gvSelectedEquip);
            ClearCheckAndGrid(tvEquipment, gvSelectedEquip);
            dataGridView.Refresh();

            for (int i = 0; i < Atend.Base.Equipment.EAirPost.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EAirPost.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {

                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        //ed.WriteMessage("nodeTypeEPackage:" + Atend.Base.Equipment.EAirPost.nodeTypeEPackage[i].ToString() + "\n");
                        //ed.WriteMessage("rootnode.Tag:" + rootnode.Tag.ToString() + "\n");
                        if (Atend.Base.Equipment.EAirPost.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            if (chileNode.Tag.ToString() == s)
                            {
                                //ed.WriteMessage(string.Format(" child tag : {0} , s: {1} , ChildName {2}\n", chileNode.Tag.ToString(), s, chileNode.Text));

                                //ed.WriteMessage("I am in the if \n");
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquip.Rows.Add();
                                //ed.WriteMessage("Child tag : " + chileNode.Tag.ToString() + "\n");
                                gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                //ed.WriteMessage("Child tag : " + chileNode.Text.ToString() + "\n");
                                gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EAirPost.nodeCountEPackageX[i].ToString();
                                gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[3].Value = chileNode.Name;
                                gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[4].Value = rootnode.Tag;

                            }
                        }

                    }

                }

            }
        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
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

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAirPostSearch02 search = new frmAirPostSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(search);
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAirPost02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(productCode);
            //txtName.Text = product.Name;
            //ReadEquipGroups(tvEquipment);

            string PicturePath = Atend.Control.Common.fullPath + "\\Consol.jpg";
            pictureBox1.Image = Image.FromFile(PicturePath);

            BindDataToTreeView();
            BindDataToTreeViewOperation();
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

        private void btnInsertEquip_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //Equipment
            Boolean canAdd = true;
            for (int i = 0; i < tvEquipment.Nodes.Count; i++)
            {
                for (int j = 0; j < tvEquipment.Nodes[i].Nodes.Count; j++)
                {
                    if (tvEquipment.Nodes[i].Nodes[j].Checked)
                    {
                        canAdd = true;
                        for (int k = 0; k < gvSelectedEquip.Rows.Count; k++)
                        {
                            if ((gvSelectedEquip.Rows[k].Cells[0].Value.ToString() == tvEquipment.Nodes[i].Nodes[j].Tag.ToString()) && (Convert.ToInt32(gvSelectedEquip.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvEquipment.Nodes[i].Nodes[j].Name.ToString())))
                            {
                                canAdd = false;
                                //ed.WriteMessage("No Allow To Add Row\n");
                            }
                        }
                        if (canAdd)
                        {
                            //ed.WriteMessage("AddRow\n");
                            gvSelectedEquip.Rows.Add();
                            gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[0].Value = tvEquipment.Nodes[i].Nodes[j].Tag.ToString();
                            gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[1].Value = tvEquipment.Nodes[i].Nodes[j].Text.ToString();
                            gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[3].Value = tvEquipment.Nodes[i].Nodes[j].Name.ToString();
                            gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[2].Value = 1;
                            gvSelectedEquip.Rows[gvSelectedEquip.Rows.Count - 1].Cells[4].Value = tvEquipment.Nodes[i].Tag.ToString();

                        }

                    }
                    else
                    {
                        for (int k = 0; k < gvSelectedEquip.Rows.Count; k++)
                        {
                            if (gvSelectedEquip.Rows[k].Cells[0].Value.ToString() == tvEquipment.Nodes[i].Nodes[j].Tag.ToString())
                            {
                                //ed.WriteMessage("\n\n" + gvSelectedEquip.Rows[k].Cells[3].Value.ToString() + "     " + tvEquipment.Nodes[i].Nodes[j].Name.ToString() + "\n\n");
                                if (Convert.ToInt32(gvSelectedEquip.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvEquipment.Nodes[i].Nodes[j].Name.ToString()))
                                {
                                    //ed.WriteMessage("Name To Delete" + gvSelectedEquip.Rows[k].Cells[1].Value.ToString() + "\n");
                                    gvSelectedEquip.Rows.RemoveAt(k);
                                }
                            }
                        }

                    }
                }
            }
            txtCapacity.Text = CalculateCapasity().ToString();

        }

        private void tvEquipment_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvSelectedEquip.Rows.Count > 0)
            {
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {
                    foreach (TreeNode childNode in rootnode.Nodes)
                    {
                        if (childNode.Tag.ToString() == gvSelectedEquip.Rows[gvSelectedEquip.CurrentRow.Index].Cells[0].Value.ToString())
                        {
                            childNode.Checked = false;
                        }
                    }
                }

                gvSelectedEquip.Rows.RemoveAt(gvSelectedEquip.CurrentRow.Index);
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void پروندهToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Guid XCode = new Guid("8ab220d5-9721-49f3-9e7c-4ad6d97e1a37");
            //Atend.Base.Equipment.EAirPost Apost = Atend.Base.Equipment.EAirPost.SelectByXCode(XCode);
            //MessageBox.Show(Apost.XCode.ToString());
            //if (selectedPostXCode != Guid.Empty)
            //    MessageBox.Show(Atend.Base.Equipment.EAirPost.ShareOnServer(selectedPostXCode).ToString());

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (selectedPostXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost), selectedPostXCode))
                {
                    Atend.Base.Equipment.EAirPost airPost = Atend.Base.Equipment.EAirPost.SelectByXCode(selectedPostXCode);
                    Code = airPost.Code;
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
            //if (selectedPostXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EAirPost.ShareOnServer(selectedPostXCode))
            //    {
            //        Atend.Base.Equipment.EAirPost a1 = Atend.Base.Equipment.EAirPost.SelectByXCode(selectedPostXCode);
            //        Code = a1.Code;
            //        MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
            //    }
            //    else
            //        MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            //}
            //else
            //    MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
        }

        private void gvSelectedEquip_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //if (!Atend.Control.NumericValidation.Int32Converter((gvSelectedEquip.Rows[e.RowIndex].Cells[2].Value.ToString())))
            //{
            //    MessageBox.Show("تعداد را با فرمت مناسب وارد کنید");
            //    return;
            //}

            //double Cap = 0;

            //for (int i = 0; i < gvSelectedEquip.Rows.Count; i++)
            //{
            //    //MessageBox.Show(gvSelectedEquip.Rows[i].Cells[4].Value.ToString());
            //    if (Convert.ToInt32(gvSelectedEquip.Rows[i].Cells[4].Value.ToString()) == (int)Atend.Control.Enum.ProductType.Transformer)
            //    {
            //        Atend.Base.Equipment.ETransformer Trans = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(gvSelectedEquip.Rows[i].Cells[0].Value.ToString()));
            //        Cap = Cap + Convert.ToInt32(gvSelectedEquip.Rows[i].Cells[2].Value.ToString()) * Trans.Capaciy;
            //    }
            //}

            txtCapacity.Text = CalculateCapasity().ToString();// Cap.ToString();
        }

        private void txtOperationName_TextChanged(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
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

        private void btnImage_Click(object sender, EventArgs e)
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

        private double CalculateCapasity()
        {
            double Cap = 0;

            for (int i = 0; i < gvSelectedEquip.Rows.Count; i++)
            {
                //MessageBox.Show(gvSelectedEquip.Rows[i].Cells[4].Value.ToString());
                if (Convert.ToInt32(gvSelectedEquip.Rows[i].Cells[4].Value.ToString()) == (int)Atend.Control.Enum.ProductType.Transformer)
                {
                    Atend.Base.Equipment.ETransformer Trans = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(gvSelectedEquip.Rows[i].Cells[0].Value.ToString()));
                    Cap = Cap + Convert.ToInt32(gvSelectedEquip.Rows[i].Cells[2].Value.ToString()) * Trans.Capaciy;
                }
            }
            return Cap;
        }



    }
}