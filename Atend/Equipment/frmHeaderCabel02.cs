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
    public partial class frmHeaderCabel02 : Form
    {
        public int productCode = -1;
        Guid SelectedHeaderCabelCode = Guid.Empty;
        public bool IsDefault = false;
        public int Code = -1;
        DataColumn TypeName = new DataColumn("Name", typeof(string));
        DataColumn TypeCode = new DataColumn("Code", typeof(int));
        DataTable TypeTbl = new DataTable();


        DataColumn MaterialName = new DataColumn("Name", typeof(string));
        DataColumn MaterialCode = new DataColumn("Code", typeof(int));
        DataTable MaterialTbl = new DataTable();

        DataTable CSRTbl = new DataTable();


        bool ForceToClose = false;

        public frmHeaderCabel02()
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

            CSRTbl = Atend.Base.Equipment.EGroundCabel.SelectAllCrossSection();


            TypeTbl.Columns.Add(TypeCode);
            TypeTbl.Columns.Add(TypeName);

            DataRow dr1 = TypeTbl.NewRow();
            dr1["Name"] = "سرکابل هوایی";
            dr1["Code"] = 1;

            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "سرکابل داخلی";
            dr2["Code"] = 2;


            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);



            MaterialTbl.Columns.Add(MaterialCode);
            MaterialTbl.Columns.Add(MaterialName);

            DataRow dr11 = MaterialTbl.NewRow();
            dr11["Name"] = "خشک";
            dr11["Code"] = 1;

            DataRow dr22 = MaterialTbl.NewRow();
            dr22["Name"] = "روغنی";
            dr22["Code"] = 2;


            DataRow dr33 = MaterialTbl.NewRow();
            dr33["Name"] = "رزینی(سیلیکونی)";
            dr33["Code"] = 3;


            MaterialTbl.Rows.Clear();
            MaterialTbl.Rows.Add(dr11);
            MaterialTbl.Rows.Add(dr22);
            MaterialTbl.Rows.Add(dr33);

        }

        private void Reset()
        {
            SelectedHeaderCabelCode = Guid.Empty;
            txtName.Text = string.Empty;
            cboMat.SelectedIndex = 0;
            cboType.SelectedIndex = 0;
            txtComment.Text = string.Empty;
            cboVol.SelectedIndex = 0;
            IsDefault = false;
            tsbIsDefault.Checked = false;
            productCode = -1;
            Code = -1;
            Atend.Control.Common.selectedProductCode = -1;
            txtBackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
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

        private void Save()
        {
            txtName.Focus();
            Atend.Base.Equipment.EHeaderCabel headerCabel = new Atend.Base.Equipment.EHeaderCabel();
            ArrayList EOperation = new ArrayList();
            headerCabel.CrossSectionArea = Convert.ToDouble(cboCrossSectionArea.Text);
            headerCabel.Material = Convert.ToByte(cboMat.SelectedValue);
            headerCabel.ProductCode = Atend.Control.Common.selectedProductCode;
            headerCabel.Type = Convert.ToByte(cboType.SelectedValue);
            headerCabel.Comment = txtComment.Text;
            headerCabel.Name = txtName.Text;
            headerCabel.Voltage = Convert.ToDouble(cboVol.Text);
            headerCabel.IsDefault = IsDefault;
            headerCabel.Code = Code;

            //Equipment
            ArrayList EPackageProduct = new ArrayList();
            for (int j = 0; j < gvSelectedEquipment.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                _EProductPackage.XCode = new Guid(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.Count = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedEquipment.Rows[j].Cells[3].Value.ToString());
                EPackageProduct.Add(_EProductPackage);
            }
            headerCabel.EquipmentList = EPackageProduct;

            //Operation
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);
            }
            headerCabel.OperationList = EOperation;

            if (SelectedHeaderCabelCode == Guid.Empty)
            {
                if (headerCabel.InsertX())
                    Reset();
                else
                {
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
                }

                //MessageBox.Show(Atend.Control.Messages.Read("Insert_Error"));

            }
            else
            {
                headerCabel.XCode = SelectedHeaderCabelCode;
                if (headerCabel.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectedHeaderCabelCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;

            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectedHeaderCabelCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedHeaderCabelCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EHeaderCabel.DeleteX(SelectedHeaderCabelCode))
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
            SelectedHeaderCabelCode = XCode;
            Atend.Base.Equipment.EHeaderCabel headerCabel = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(XCode);


            Atend.Control.Common.selectedProductCode = headerCabel.ProductCode;
            SelectProduct();
            cboMat.SelectedValue = headerCabel.Material;
            cboVol.Text = headerCabel.Voltage.ToString();
            cboCrossSectionArea.Text = headerCabel.CrossSectionArea.ToString();
            cboType.SelectedValue = headerCabel.Type;
            txtComment.Text = headerCabel.Comment;
            txtName.Text = headerCabel.Name;
            SelectedHeaderCabelCode = headerCabel.XCode;
            tsbIsDefault.Checked = headerCabel.IsDefault;

            Code = headerCabel.Code;
            BindTreeViwAndGrid();
        }

        private void BindTreeViwAndGrid()
        {
            //EQUIPMENT
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            gvSelectedEquipment.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EHeaderCabel.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EHeaderCabel.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {
                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        if (Atend.Base.Equipment.EHeaderCabel.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            if (chileNode.Tag.ToString() == s)
                            {
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquipment.Rows.Add();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EHeaderCabel.nodeCountEPackageX[i].ToString();
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
            for (int i = 0; i < Atend.Base.Equipment.EHeaderCabel.nodeKeys.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EHeaderCabel.nodeKeys[i]);
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

        private void BindDataToMaterialComboBox()
        {
            cboMat.DisplayMember = "Name";
            cboMat.ValueMember = "Code";
            cboMat.DataSource = MaterialTbl;


            if (cboMat.Items.Count > 0)
            {
                cboMat.SelectedIndex = 0;
            }

        }

        private void BindDataToTypeComboBox()
        {
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "Code";
            cboType.DataSource = TypeTbl;

            if (cboType.Items.Count > 0)
            {
                cboType.SelectedIndex = 0;// cboType.Items.Count - 1
            }


        }

        private void BindDataToCrossSectionAreaComboBox()
        {
            cboCrossSectionArea.DisplayMember = "CrossSection";
            cboCrossSectionArea.DataSource = CSRTbl;

            if (cboCrossSectionArea.Items.Count > 0)
            {
                cboCrossSectionArea.SelectedIndex = 0;// cboType.Items.Count - 1
            }


        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectedHeaderCabelCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EHeaderCabel Equip = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(SelectedHeaderCabelCode);
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
                MessageBox.Show("لطفا نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            if (Atend.Base.Equipment.EHeaderCabel.SearchByName(txtName.Text) == true && SelectedHeaderCabelCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboType.Text))
            {
                MessageBox.Show("لطفا نوع را مشخص نمایید", "خطا");
                cboType.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cboMat.Text))
            {
                MessageBox.Show("لطفا جنس را مشخص نمایید", "خطا");
                cboMat.Focus();
                return false;
            }

            //if (!Atend.Control.NumericValidation.DoubleConverter(cboMat.Text))
            //{
            //    MessageBox.Show("لطفا سطح مقطع رابا فرمت مناسب وارد نمایید", "خطا");
            //    cboMat.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(cboVol.Text))
            {
                MessageBox.Show("لطفا ولتاژ را مشخص نمایید", "خطا");
                cboVol.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cboCrossSectionArea.Text))
            {
                MessageBox.Show("لطفا سطح مقطع را مشخص نمایید", "خطا");
                cboCrossSectionArea.Focus();
                return false;
            }
            else
            {
                if (!Atend.Control.NumericValidation.DoubleConverter(cboCrossSectionArea.Text))
                {
                    MessageBox.Show("لطفا سطح مقطع را مشخص نمایید", "خطا");
                    cboCrossSectionArea.Focus();
                    return false;

                }
            }



            //if (!Atend.Control.NumericValidation.DoubleConverter(cboVol.Text))
            //{
            //    MessageBox.Show("لطفا ولتاژ رابا فرمت مناسب وارد نمایید", "خطا");
            //    cboVol.Focus();
            //    return false;
            //}

            Atend.Base.Equipment.EHeaderCabel headercabel = Atend.Base.Equipment.EHeaderCabel.CheckForExist(Convert.ToByte(cboMat.SelectedValue), Convert.ToByte(cboType.SelectedValue));
            if (headercabel.Code != -1 && SelectedHeaderCabelCode == Guid.Empty)
            {

                if (MessageBox.Show("سرکابل با مشخصات داده شده موجود میباشد\n\n سرکابل با مشخصات فوق  : " + headercabel.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    cboMat.Focus();
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

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(SelectedHeaderCabelCode, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }
            }

            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void frmHeaderCabel_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ById(productCode);
            //txtName.Text = product.Name;
            //BindDataToMaterialComboBox();
            BindDataToTypeComboBox();
            BindDataToMaterialComboBox();
            BindDataToCrossSectionAreaComboBox();

            if (cboVol.Items.Count > 0)
            {
                cboVol.SelectedIndex = 0;// cboType.Items.Count - 1
            }


            //if (cboMaterial.Items.Count > 0)
            //{
            //    cboMaterial.SelectedIndex = cboMaterial.Items.Count - 1;
            //}


            BindDataToTreeView();
        }

        private void BindDataToTreeView()
        {
            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvEquipment);

            DataTable ProductType = Atend.Base.Base.BProduct.SelectByTypeX(Convert.ToInt32(Atend.Control.Enum.ProductType.Operation));
            foreach (DataRow dr in ProductType.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = dr["Name"].ToString();
                node.Tag = dr["ID"].ToString();
                tvOperation.Nodes.Add(node);
            }
        }

        private void btnNewMaterial_Click(object sender, EventArgs e)
        {
            //Extra
            //frmHeaderCabelMaterial frmheaderCabelMaterial = new frmHeaderCabelMaterial();
            //frmheaderCabelMaterial.ShowDialog();
            //BindDataToMaterialComboBox();
        }

        private void btnNewType_Click(object sender, EventArgs e)
        {
            //Extra
            //frmHeaderCabelType type = new frmHeaderCabelType();
            //type.ShowDialog();
            //BindDataToTypeComboBox();
        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
            }
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            frmHeaderCabelSearch02 frmheaderCabelSearch = new frmHeaderCabelSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmheaderCabelSearch);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cboMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCrossSectionArea_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //gvOperation.Rows.Clear();
            //for (int i = 0; i < tvOperation.Nodes.Count; i++)
            //{

            //    if (tvOperation.Nodes[i].Checked)
            //    {
            //        gvOperation.Rows.Add();
            //        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[0].Value = tvOperation.Nodes[i].Tag.ToString();
            //        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[1].Value = tvOperation.Nodes[i].Text.ToString();

            //    }
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //gvOperation.Rows.RemoveAt(gvOperation.CurrentRow.Index);

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
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.HeaderCabel);

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
            if (SelectedHeaderCabelCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel), SelectedHeaderCabelCode))
                {
                    Atend.Base.Equipment.EHeaderCabel HeaderCabel = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(SelectedHeaderCabelCode);
                    Code = HeaderCabel.Code;
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

            //if (SelectedHeaderCabelCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EHeaderCabel.ShareOnServer(SelectedHeaderCabelCode))
            //    {
            //        Atend.Base.Equipment.EHeaderCabel h1 = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(SelectedHeaderCabelCode);
            //        Code = h1.Code;
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

        private void txtOperationName_TextChanged_1(object sender, EventArgs e)
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

        private void btnInsert_Click_1(object sender, EventArgs e)
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

        private void btnDelete_Click_1(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
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

        private void button2_Click(object sender, EventArgs e)
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

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboMaterial_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

    }
}