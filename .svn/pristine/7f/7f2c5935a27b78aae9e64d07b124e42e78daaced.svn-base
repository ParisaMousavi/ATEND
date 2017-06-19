using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Atend.Base;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Equipment
{
    public partial class frmBreaker02 : Form
    {
        public int productCode = -1;
        Guid SelectedBreakerXCode = Guid.Empty;
        public bool IsDefault = false;
        int Code = -1;
        DataColumn TypeName = new DataColumn("Name", typeof(string));
        DataColumn TypeCode = new DataColumn("Code", typeof(int));
        DataTable TypeTbl = new DataTable();
        bool ForceToClose = false;

        public frmBreaker02()
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
            dr1["Name"] = "روغنی";
            dr1["Code"] = 1;

            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "SF6";
            dr2["Code"] = 2;

            DataRow dr3 = TypeTbl.NewRow();
            dr3["Name"] = "خلاء";
            dr3["Code"] = 3;

            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);
            TypeTbl.Rows.Add(dr3);
        }

        private void Reset()
        {
            txtAmper.Text = string.Empty;
            txtName.Text = string.Empty;
            SelectedBreakerXCode = Guid.Empty;
            txtComment.Text = string.Empty;
            txtBackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
            Atend.Control.Common.selectedProductCode = -1;

            txtAmper.Focus();
            IsDefault = false;
            tsbIsDefault.Checked = false;
            ClearCheckAndGrid(tvEquipment, gvEquipment);
            ClearCheckAndGrid(tvOperation, gvOperation);
            productCode = -1;
            Code = -1;
        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectedBreakerXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EBreaker Equip = Atend.Base.Equipment.EBreaker.SelectByXCode(SelectedBreakerXCode);
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
            if (Atend.Base.Equipment.EBreaker.SearchByName(txtName.Text) == true && SelectedBreakerXCode == Guid.Empty)
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
                MessageBox.Show("لطفاً آمپراژ را با فرمت صحیح وارد نمایید", "خطا");
                txtAmper.Select(0, txtAmper.Text.Length);
                return false;
            }

            if (string.IsNullOrEmpty(cboType.Text))
            {
                MessageBox.Show("لطفاً نوع را مشخص نمایید", "خطا");
                cboType.Focus();
                return false;
            }
            Atend.Base.Equipment.EBreaker breaker = Atend.Base.Equipment.EBreaker.CheckForExist(Convert.ToDouble(txtAmper.Text), Convert.ToByte(cboType.SelectedValue));
            if (breaker.Code != -1 && SelectedBreakerXCode == Guid.Empty)
            {
                if (MessageBox.Show("دژنگتور با مشخصات داده شده موجود میباشد\n\n دژنگتور با مشخصات فوق  : " + breaker.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvEquipment, 2))
            {
                MessageBox.Show("لطفا تعداد تجهیزات جانبی را با فرمت مناسب وارد نمایید", "خطا");
                gvEquipment.Focus();
                return false;
            }
            for (int j = 0; j < gvEquipment.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                _EProductPackage.XCode = new Guid(gvEquipment.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.Count = Convert.ToInt32(gvEquipment.Rows[j].Cells[2].Value.ToString());
                _EProductPackage.TableType = Convert.ToInt16(gvEquipment.Rows[j].Cells[3].Value.ToString());

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(SelectedBreakerXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvEquipment.Focus();
                    return false;
                }
            }
            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void BindDataToTreeView()
        {
            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvEquipment);
        }

        private void Save()
        {
            txtAmper.Focus();
            btnInsert.Focus();
            ArrayList EPackageProduct = new ArrayList();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Atend.Base.Equipment.EBreaker breaker = new Atend.Base.Equipment.EBreaker();
            breaker.Amper = Convert.ToDouble(txtAmper.Text);
            breaker.ProductCode = Atend.Control.Common.selectedProductCode;
            breaker.Type = Convert.ToByte(cboType.SelectedValue);
            breaker.Comment = txtComment.Text;
            breaker.Name = txtName.Text;
            breaker.IsDefault = IsDefault;
            breaker.Code = Code;
            //Equipment
            for (int j = 0; j < gvEquipment.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                //ed.WriteMessage("Begin 1\n");
                //_EProductPackage.ProductCode = 0;// Convert.ToInt32(gvEquipment.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.XCode = new Guid(gvEquipment.Rows[j].Cells[0].Value.ToString());

                //ed.WriteMessage("Begin 2\n" + gvEquipment.Rows[j].Cells[2].Value.ToString());

                _EProductPackage.Count = Convert.ToInt32(gvEquipment.Rows[j].Cells[2].Value.ToString());
                //ed.WriteMessage("Value:" + gvEquipment.Rows[j].Cells[3].Value.ToString() + "\n");
                _EProductPackage.TableType = Convert.ToInt16(gvEquipment.Rows[j].Cells[3].Value.ToString());
                EPackageProduct.Add(_EProductPackage);
                //ed.WriteMessage("aaa \n");
            }
            breaker.EquipmentList = EPackageProduct;
            //Operation
            ArrayList EOperation = new ArrayList();
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);
            }
            breaker.OperationList = EOperation;
            //************

            if (SelectedBreakerXCode == Guid.Empty)
            {
                if (breaker.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                breaker.XCode = new Guid(SelectedBreakerXCode.ToString());
                if (breaker.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void Delete()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectedBreakerXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }


            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedBreakerXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EBreaker.DeleteX(SelectedBreakerXCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }
        }

        private void BindTreeViwAndGridEquipment(TreeView treeView, DataGridView dataGridView)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //EQUIPMENT
            ClearCheckAndGrid(tvEquipment, gvEquipment);
            dataGridView.Refresh();
            //ed.WriteMessage("nodeKeysEPackage.Count:" + Atend.Base.Equipment.EInsulator.nodeKeysEPackage.Count.ToString() + "\n");
            for (int i = 0; i < Atend.Base.Equipment.EBreaker.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EBreaker.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {

                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        //ed.WriteMessage("nodeTypeEPackage:" + Atend.Base.Equipment.EInsulator.nodeTypeEPackage[i].ToString() + "\n");
                        //ed.WriteMessage("rootnode.Tag:" + rootnode.Tag.ToString() + "\n");
                        if (Atend.Base.Equipment.EBreaker.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            //ed.WriteMessage(string.Format(" child tag : {0} , s: {1} \n", chileNode.Tag.ToString(), s));
                            if (chileNode.Tag.ToString() == s)
                            {
                                //ed.WriteMessage("I am in the if \n");
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvEquipment.Rows.Add();
                                //ed.WriteMessage("Child tag : " +  chileNode.Tag.ToString() + "\n");
                                gvEquipment.Rows[gvEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                //ed.WriteMessage("Child tag : " + chileNode.Text.ToString() + "\n");
                                gvEquipment.Rows[gvEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvEquipment.Rows[gvEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EBreaker.nodeCountEPackageX[i].ToString();
                                gvEquipment.Rows[gvEquipment.Rows.Count - 1].Cells[3].Value = chileNode.Name;

                            }
                        }

                    }

                }



            }
        }

        public void BindDataToOwnControl(Guid XCode)
        {
            Atend.Base.Equipment.EBreaker breaker = Atend.Base.Equipment.EBreaker.SelectByXCode(XCode);
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(breaker.ProductCode);
            SelectedBreakerXCode = XCode;
            Atend.Control.Common.selectedProductCode = breaker.ProductCode;
            SelectProduct();

            txtName.Text = breaker.Name;

            txtAmper.Text = Convert.ToString(Math.Round(breaker.Amper, 4));
            cboType.SelectedValue = breaker.Type;
            txtComment.Text = breaker.Comment;
            tsbIsDefault.Checked = breaker.IsDefault;
            Code = breaker.Code;
            BindTreeViwAndGridEquipment(tvEquipment, gvEquipment);
            BindTreeViwAndGridOpration(tvOperation, gvOperation);
        }

        private void BindTreeViwAndGridOpration(TreeView treeView, DataGridView dataGridView)
        {
            //dataGridView.Rows.Clear();
            //dataGridView.Update();
            ClearCheckAndGrid(tvOperation, gvOperation);
            ClearCheckAndGrid(tvOperation, gvOperation);
            dataGridView.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EBreaker.nodeKeys.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EBreaker.nodeKeys[i]);
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

        private void BindDataToComboBox()
        {
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "Code";
            cboType.DataSource = TypeTbl;
        }

        private void frmBreaker_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToComboBox();
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(productCode);
            //txtName.Text = product.Name;

            if (cboType.Items.Count > 0)
            {
                cboType.SelectedIndex = 0;// cboType.Items.Count - 1;
            }
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
            Delete();
        }

        //private void btnNewBreakerType_Click(object sender, EventArgs e)
        //{
        //    frmBreakerType frmbreakerType = new frmBreakerType();
        //    frmbreakerType.ShowDialog();
        //    BindDataToComboBox();
        //}

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBreakerSearch02 frmbreakerSearch = new frmBreakerSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmbreakerSearch);
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
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
                        for (int k = 0; k < gvEquipment.Rows.Count; k++)
                        {
                            if ((gvEquipment.Rows[k].Cells[0].Value.ToString() == tvEquipment.Nodes[i].Nodes[j].Tag.ToString()) && (Convert.ToInt32(gvEquipment.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvEquipment.Nodes[i].Nodes[j].Name.ToString())))
                            {
                                canAdd = false;
                                //ed.WriteMessage("No Allow To Add Row\n");
                            }
                        }
                        if (canAdd)
                        {
                            //ed.WriteMessage("AddRow\n");
                            gvEquipment.Rows.Add();
                            gvEquipment.Rows[gvEquipment.Rows.Count - 1].Cells[0].Value = tvEquipment.Nodes[i].Nodes[j].Tag.ToString();
                            gvEquipment.Rows[gvEquipment.Rows.Count - 1].Cells[1].Value = tvEquipment.Nodes[i].Nodes[j].Text.ToString();
                            gvEquipment.Rows[gvEquipment.Rows.Count - 1].Cells[3].Value = tvEquipment.Nodes[i].Nodes[j].Name.ToString();
                            gvEquipment.Rows[gvEquipment.Rows.Count - 1].Cells[2].Value = 1;

                        }

                    }
                    else
                    {
                        for (int k = 0; k < gvEquipment.Rows.Count; k++)
                        {
                            if ((gvEquipment.Rows[k].Cells[0].Value.ToString() == tvEquipment.Nodes[i].Nodes[j].Tag.ToString()) && (Convert.ToInt32(gvEquipment.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvEquipment.Nodes[i].Nodes[j].Name.ToString())))
                            {
                                //ed.WriteMessage("Name To Delete" + gvEquipment.Rows[k].Cells[1].Value.ToString() + "\n");
                                gvEquipment.Rows.RemoveAt(k);

                            }
                        }

                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvEquipment.Rows.Count > 0)
            {
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {
                    foreach (TreeNode childNode in rootnode.Nodes)
                    {
                        if (childNode.Tag.ToString() == gvEquipment.Rows[gvEquipment.CurrentRow.Index].Cells[0].Value.ToString())
                        {
                            childNode.Checked = false;
                        }
                    }
                }

                gvEquipment.Rows.RemoveAt(gvEquipment.CurrentRow.Index);

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
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.Breaker);

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

            if (SelectedBreakerXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker), SelectedBreakerXCode))
                {
                    Atend.Base.Equipment.EBreaker Breaker = Atend.Base.Equipment.EBreaker.SelectByXCode(SelectedBreakerXCode);
                    Code = Breaker.Code;
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


            //if (SelectedBreakerXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EBreaker.ShareOnServer(SelectedBreakerXCode))
            //    {
            //        Atend.Base.Equipment.EBreaker b1 = Atend.Base.Equipment.EBreaker.SelectByXCode(SelectedBreakerXCode);
            //        Code = b1.Code;
            //        MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
            //    }
            //    else
            //        MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            //}
            //else
            //    MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
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

        private void button3_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


    }
}