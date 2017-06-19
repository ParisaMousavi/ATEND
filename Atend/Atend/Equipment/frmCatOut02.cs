using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Equipment
{
    public partial class frmCatOut02 : Form
    {
        public int productCode = -1;
        Guid SelectedCatoutXCode = Guid.Empty;
        public bool IsDefault = false;
        public int Code = -1;
        DataColumn TypeName = new DataColumn("Name", typeof(string));
        DataColumn TypeCode = new DataColumn("Code", typeof(int));
        DataTable TypeTbl = new DataTable();
        bool ForceToClose = false;

        public frmCatOut02()
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

            TypeTbl.Columns.Add(TypeCode);
            TypeTbl.Columns.Add(TypeName);

            DataRow dr1 = TypeTbl.NewRow();
            dr1["Name"] = "کات اوت نوع T";
            dr1["Code"] = 1;

            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "کات اوت نوع K";
            dr2["Code"] = 2;

            DataRow dr3 = TypeTbl.NewRow();
            dr3["Name"] = "کات اوت نوع KT";
            dr3["Code"] = 3;


            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);
            TypeTbl.Rows.Add(dr3);
        }

        private void Reset()
        {
            SelectedCatoutXCode = Guid.Empty;
            txtAmper.Text = "0";
            txtName.Text = string.Empty;
            //txtVol.Text = "0";
            cboVol.SelectedIndex = 0;
            txtComment.Text = string.Empty;
            txtAmper.Focus();
            IsDefault = false;
            tsbIsDefault.Checked = false;
            productCode = -1;
            Atend.Control.Common.selectedProductCode = -1;
            txtBackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
            Code = -1;
            ClearCheckAndGrid(tvOperation, gvOperation);
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
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

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectedCatoutXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.ECatOut Equip = Atend.Base.Equipment.ECatOut.SelectByXCode(SelectedCatoutXCode);
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
            //if (Atend.Control.Common.selectedProductCode == -1)
            //{
            //    MessageBox.Show("لطفا ابتدا یک کالا را از پشتیبان انتخاب کنید", "خطا");

            //    return false;
            //}
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفاً نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            if (Atend.Base.Equipment.ECatOut.SearchByName(txtName.Text) == true && SelectedCatoutXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAmper.Text))
            {
                MessageBox.Show("لطفاً آمپراژ را مشخص نمایید", "خطا");
                txtAmper.Focus();
                return false;
            }



            if (!Atend.Control.NumericValidation.DoubleConverter(txtAmper.Text))
            {
                MessageBox.Show("لطفاً آمپراژ را با فرمت مناسب وارد نمایید", "خطا");
                txtAmper.Focus();
                txtAmper.Select(0, txtAmper.Text.Length);
                return false;
            }

            if (string.IsNullOrEmpty(cboVol.Text))
            {
                MessageBox.Show("لطفاً ولتاژ را مشخص نمایید", "خطا");
                cboVol.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(cboVol.Text))
            {
                MessageBox.Show("لطفاً ولتاژ را با فرمت مناسب وارد نمایید", "خطا");
                cboVol.Focus();
                return false;
            }
            Atend.Base.Equipment.ECatOut catout = Atend.Base.Equipment.ECatOut.CheckForExist(Convert.ToDouble(txtAmper.Text), Convert.ToDouble(cboVol.Text), Convert.ToByte(cboType.SelectedValue));
            if (catout.Code != -1 && SelectedCatoutXCode == Guid.Empty)
            {
                if (MessageBox.Show("کت اوت با مشخصات داده شده موجود میباشد\n\n کت اوت با مشخصات فوق:  " + catout.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    txtAmper.Focus();
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

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(SelectedCatoutXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }
            }

            return CheckStatuseOfAccessChangeDefault();
            // return true;
        }

        private void Save()
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            txtName.Focus();
            ArrayList EPackageProduct = new ArrayList();
            ArrayList EOperation = new ArrayList();
            Atend.Base.Equipment.ECatOut catout = new Atend.Base.Equipment.ECatOut();
            catout.Amper = Convert.ToDouble(txtAmper.Text);
            catout.ProductCode = Atend.Control.Common.selectedProductCode;
            catout.Type = Convert.ToByte(cboType.SelectedValue);
            catout.Vol = Convert.ToDouble(cboVol.Text);
            catout.Comment = txtComment.Text;
            catout.Name = txtName.Text;
            catout.IsDefault = IsDefault;
            catout.Code = Code;
            //Equipment
            for (int j = 0; j < gvSelectedEquipment.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                //ed.WriteMessage("Begin 1\n");
                _EProductPackage.XCode = new Guid(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                //ed.WriteMessage("Begin 2\n" + gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());

                _EProductPackage.Count = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                //ed.WriteMessage("Value:" + gvSelectedEquipment.Rows[j].Cells[3].Value.ToString() + "\n");
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedEquipment.Rows[j].Cells[3].Value.ToString());
                EPackageProduct.Add(_EProductPackage);
                //ed.WriteMessage("aaa \n");
            }
            catout.EquipmentList = EPackageProduct;
            //************

            //Operation

            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);

            }

            catout.OperationList = EOperation;

            //**********



            //if (Atend.Control.Common.selectedProductCode != -1)
            //{
            if (SelectedCatoutXCode == Guid.Empty)
            {
                if (catout.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت کردن اطلاعات نمی باشد", "خطا");
            }
            else
            {
                catout.XCode = SelectedCatoutXCode;
                if (catout.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");

            }
            //}
            //else
            //{
            //    MessageBox.Show("لطفا ابتدا یک کالا را از پشتیبان انتخاب کنید","خطا");
            //}
        }

        private void BindToTypeComboBox()
        {
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "Code";
            cboType.DataSource = TypeTbl;
        }

        private void BindTreeViwAndGridEquipment()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //EQUIPMENT
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            gvSelectedEquipment.Refresh();
            //ed.WriteMessage("nodeKeysEPackage.Count:" + Atend.Base.Equipment.ECatOut.nodeKeysEPackage.Count.ToString() + "\n");
            for (int i = 0; i < Atend.Base.Equipment.ECatOut.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.ECatOut.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {

                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        //ed.WriteMessage("nodeTypeEPackage:" + Atend.Base.Equipment.ECatOut.nodeTypeEPackage[i].ToString() + "\n");
                        //ed.WriteMessage("rootnode.Tag:" + rootnode.Tag.ToString() + "\n");
                        if (Atend.Base.Equipment.ECatOut.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            //ed.WriteMessage(string.Format(" child tag : {0} , s: {1} \n", chileNode.Tag.ToString(), s));
                            if (chileNode.Tag.ToString() == s)
                            {
                                //ed.WriteMessage("I am in the if \n");
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquipment.Rows.Add();
                                //ed.WriteMessage("Child tag : " + chileNode.Tag.ToString() + "\n");
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                //ed.WriteMessage("Child tag : " + chileNode.Text.ToString() + "\n");
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.ECatOut.nodeCountEPackageX[i].ToString();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[3].Value = chileNode.Name;

                            }
                        }

                    }

                }



            }
            //************
            //Operation
            ClearCheckAndGrid(tvOperation, gvOperation);
            gvOperation.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.ECatOut.nodeKeysX.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.ECatOut.nodeKeysX[i]);
                string s = Operation.ProductID.ToString();

                foreach (TreeNode rootnode in tvOperation.Nodes)
                {

                    //ed.WriteMessage("RootNode.Tag= " + rootnode.Tag.ToString() + "\n");
                    if (rootnode.Tag.ToString() == s)
                    {
                        //ed.WriteMessage("I Am In IF" + "rootnode.Tag:= " + rootnode.Tag.ToString() + "S =" + s + "\n");
                        rootnode.Checked = true;
                        gvOperation.Rows.Add();
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[0].Value = rootnode.Tag;
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[1].Value = rootnode.Text;
                        Atend.Base.Base.BUnit Unit = Atend.Base.Base.BUnit.Select_ByProductID(Convert.ToInt32(rootnode.Tag.ToString()));
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[2].Value = Unit.Name;
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[3].Value = Operation.Count;

                    }

                }
            }
        }

        public void BindDataToOwnControl(Guid XCode)
        {

            SelectedCatoutXCode = XCode;
            Atend.Base.Equipment.ECatOut catOut = Atend.Base.Equipment.ECatOut.SelectByXCode(XCode);
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(catOut.ProductCode);
            Atend.Control.Common.selectedProductCode = catOut.ProductCode;
            SelectProduct();
            txtName.Text = catOut.Name;
            txtComment.Text = catOut.Comment;
            txtAmper.Text = Convert.ToString(Math.Round(catOut.Amper, 4));
            cboVol.Text = Convert.ToString(Math.Round(catOut.Vol, 2));
            cboType.SelectedValue = catOut.Type;
            tsbIsDefault.Checked = catOut.IsDefault;
            Code = catOut.Code;
            BindTreeViwAndGridEquipment();
        }

        private void BindDataToTreeViewOperation()
        {
            //BindData To tvOperation
            DataTable ProductType = Atend.Base.Base.BProduct.SelectByTypeX(Convert.ToInt32(Atend.Control.Enum.ProductType.Operation));
            //int i = 0;
            foreach (DataRow dr in ProductType.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = dr["Name"].ToString();
                node.Tag = dr["ID"].ToString();
                tvOperation.Nodes.Add(node);
                //i++;
            }
        }

        private void BindDataToTreeView()
        {
            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvEquipment);
            BindDataToTreeViewOperation();
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectedCatoutXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectedCatoutXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا مایل به حذف کردن اطلاعات می باشید؟", "خطا", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedCatoutXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.ECatOut.DeleteX(SelectedCatoutXCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "حذف");
            }
        }

        private void frmCatOut_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(productCode);
            txtName.Text = product.Name;
            BindToTypeComboBox();

            if (cboType.Items.Count > 0)
            {
                cboType.SelectedIndex = 0;// cboType.Items.Count - 1;
            }
            if (cboVol.Items.Count > 0)
            {
                cboVol.SelectedIndex = 0;
            }
            BindDataToTreeView();
        }

        private void SelectProduct()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(Atend.Control.Common.selectedProductCode);
            txtName.Text = product.Name;
            txtBackUpName.Text = product.Name;
            txtCode.Text = product.Code.ToString();
        }

        private void frmCatOut_FormClosing(object sender, FormClosingEventArgs e)
        {

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
            frmCatOutSearch02 frmcatOutSearch = new frmCatOutSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmcatOutSearch);
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNewType_Click(object sender, EventArgs e)
        {
            //frmCatOutType frmcatOutType = new frmCatOutType();
            //frmcatOutType.ShowDialog();
            BindToTypeComboBox();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

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

        private void btnInsert_Click(object sender, EventArgs e)
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.CatOut);

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
            }
        }

        private void frmCatOut_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void gvSelectedEquipment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void tvEquipment_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tsbShare_Click(object sender, EventArgs e)
        {

            if (SelectedCatoutXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut), SelectedCatoutXCode))
                {
                    Atend.Base.Equipment.ECatOut CatOut = Atend.Base.Equipment.ECatOut.SelectByXCode(SelectedCatoutXCode);
                    Code = CatOut.Code;
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

            //if (SelectedCatoutXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.ECatOut.ShareOnServer(SelectedCatoutXCode))
            //    {
            //        Atend.Base.Equipment.ECatOut c1 = Atend.Base.Equipment.ECatOut.SelectByXCode(SelectedCatoutXCode);
            //        Code = c1.Code;
            //        MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
            //    }
            //    else
            //        MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            //}
            //else
            //    MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
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

        private void txtOperationName_TextChanged(object sender, EventArgs e)
        {
            SearchOperation();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchOperation();
        }


    }
}