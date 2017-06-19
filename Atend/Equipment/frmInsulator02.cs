using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Atend.Base;
using System.Collections;
using System.Xml;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
namespace Atend.Equipment
{
    public partial class frmInsulator02 : Form
    {

        public int productCode = -1;
        public Guid SelectInsulatorXCode = Guid.Empty;
        public bool IsDefault = false;
        int Code = -1;

        DataColumn TypeName = new DataColumn("Name", typeof(string));
        DataColumn TypeCode = new DataColumn("Code", typeof(int));

        DataTable TypeTbl = new DataTable();


        DataColumn MaterialName = new DataColumn("Name", typeof(string));
        DataColumn MaterialCode = new DataColumn("Code", typeof(int));
        DataTable MaterialTbl = new DataTable();
        bool ForceToClose = false;

        public frmInsulator02()
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
            dr1["Name"] = "چرخی تک شیاره";
            dr1["Code"] = 1;

            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "چرخی دو شیاره";
            dr2["Code"] = 2;

            DataRow dr3 = TypeTbl.NewRow();
            dr3["Name"] = "بوشینگ ترانس";
            dr3["Code"] = 3;

            DataRow dr4 = TypeTbl.NewRow();
            dr4["Name"] = "بشقابی";
            dr4["Code"] = 4;

            DataRow dr5 = TypeTbl.NewRow();
            dr5["Name"] = "سوزنی";
            dr5["Code"] = 5;

            DataRow dr6 = TypeTbl.NewRow();
            dr6["Name"] = "اتکایی";
            dr6["Code"] = 6;

            DataRow dr7 = TypeTbl.NewRow();
            dr7["Name"] = "آویزی";
            dr7["Code"] = 7;

            DataRow dr8 = TypeTbl.NewRow();
            dr8["Name"] = "لنکرود";
            dr8["Code"] = 8;

            DataRow dr9 = TypeTbl.NewRow();
            dr9["Name"] = "مهار";
            dr9["Code"] = 9;

            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);
            TypeTbl.Rows.Add(dr3);
            TypeTbl.Rows.Add(dr4);
            TypeTbl.Rows.Add(dr5);
            TypeTbl.Rows.Add(dr6);
            TypeTbl.Rows.Add(dr7);
            TypeTbl.Rows.Add(dr8);
            TypeTbl.Rows.Add(dr9);

            /////////////////

            MaterialTbl.Columns.Add(MaterialCode);
            MaterialTbl.Columns.Add(MaterialName);

            DataRow dr11 = MaterialTbl.NewRow();
            dr11["Name"] = "چینی";
            dr11["Code"] = 1;

            DataRow dr21 = MaterialTbl.NewRow();
            dr21["Name"] = "سیلیکون رابر";
            dr21["Code"] = 2;

            DataRow dr31 = MaterialTbl.NewRow();
            dr31["Name"] = "روی";
            dr31["Code"] = 3;

            DataRow dr41 = MaterialTbl.NewRow();
            dr41["Name"] = "شیشه";
            dr41["Code"] = 4;

            DataRow dr51 = MaterialTbl.NewRow();
            dr51["Name"] = "کامپوزیت";
            dr51["Code"] = 5;

            MaterialTbl.Rows.Clear();
            MaterialTbl.Rows.Add(dr11);
            MaterialTbl.Rows.Add(dr21);
            MaterialTbl.Rows.Add(dr31);
            MaterialTbl.Rows.Add(dr41);
            MaterialTbl.Rows.Add(dr51);

        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectInsulatorXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EInsulator Equip = Atend.Base.Equipment.EInsulator.SelectByXCode(SelectInsulatorXCode);
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
            btnInsert.Focus();
            //if (Atend.Control.Common.selectedProductCode == -1)
            //{
            //    MessageBox.Show("لطفا ابتدا یک کالا را از پشتیبان انتخاب کنید", "خطا");

            //    return false;
            //}
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفا نام را مشخص کنید", "خطا");
                txtName.Focus();
                return false;

            }
            if (Atend.Base.Equipment.EInsulator.SearchByName(txtName.Text) == true && SelectInsulatorXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
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

            Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.CheckForExist(Convert.ToByte(cboMaterial.SelectedValue.ToString()), Convert.ToDouble(cboVol.Text), Convert.ToByte(cboType.SelectedIndex));
            if (insulator.Code != -1 && SelectInsulatorXCode == Guid.Empty)
            {
                if (MessageBox.Show("مقره با مشخصات داده شده موجود میباشد\n\n مقره با مشخصات فوق  : " + insulator.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    cboMaterial.Focus();
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

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(SelectInsulatorXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }
            }

            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void Reset()
        {
            cboMaterial.SelectedIndex = 0;
            cboVol.SelectedIndex = 0;
            SelectInsulatorXCode = Guid.Empty;
            txtName.Text = string.Empty;
            txtComment.Text = string.Empty;
            cboMaterial.Focus();
            IsDefault = false;
            tsbIsDefault.Checked = false;
            Atend.Control.Common.selectedProductCode = -1;
            txtBackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
            productCode = -1;
            Code = -1;
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            ClearCheckAndGrid(tvOperation, gvOperation);
        }

        private void Save()
        {
            txtName.Focus();
            btnInsert.Focus();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("EnTer To Save\n");
            Atend.Base.Equipment.EInsulator insulator = new Atend.Base.Equipment.EInsulator();
            ArrayList EPackageProduct = new ArrayList();
            ArrayList EOperation = new ArrayList();
            insulator.MaterialCode = Convert.ToByte(cboMaterial.SelectedValue.ToString());
            insulator.ProductCode = Atend.Control.Common.selectedProductCode;
            insulator.Volt = Convert.ToDouble(cboVol.Text);
            insulator.Comment = txtComment.Text;
            insulator.Name = txtName.Text;
            insulator.Type = Convert.ToByte(cboType.SelectedIndex);
            //insulator.LenghtInsulatorChain = Convert.ToDouble(txtLenghtPipeinsulator.Text);
            insulator.Code = Code;
            insulator.IsDefault = IsDefault;

            //Equipment
            for (int j = 0; j < gvSelectedEquipment.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                //ed.WriteMessage("Begin 1\n");
                //_EProductPackage.ProductCode = 0;// Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.XCode = new Guid(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                //ed.WriteMessage("Begin 2\n" + gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());

                _EProductPackage.Count = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                //ed.WriteMessage("Value:" + gvSelectedEquipment.Rows[j].Cells[3].Value.ToString() + "\n");
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedEquipment.Rows[j].Cells[3].Value.ToString());
                EPackageProduct.Add(_EProductPackage);
                //ed.WriteMessage("aaa \n");
            }
            insulator.EquipmentList = EPackageProduct;
            //************

            //Operation

            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);

            }

            insulator.OperationList = EOperation;

            //**********



            if (SelectInsulatorXCode == Guid.Empty)
            {
                if (insulator.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                insulator.XCode = SelectInsulatorXCode;
                if (insulator.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void BindTreeViwAndGridEquipment(TreeView treeView, DataGridView dataGridView)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //EQUIPMENT
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            dataGridView.Refresh();
            //ed.WriteMessage("nodeKeysEPackage.Count:" + Atend.Base.Equipment.EInsulator.nodeKeysEPackage.Count.ToString() + "\n");
            for (int i = 0; i < Atend.Base.Equipment.EInsulator.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EInsulator.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {

                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        //ed.WriteMessage("nodeTypeEPackage:" + Atend.Base.Equipment.EInsulator.nodeTypeEPackage[i].ToString() + "\n");
                        //ed.WriteMessage("rootnode.Tag:" + rootnode.Tag.ToString() + "\n");
                        if (Atend.Base.Equipment.EInsulator.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
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
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EInsulator.nodeCountEPackageX[i].ToString();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[3].Value = chileNode.Name;

                            }
                        }

                    }

                }



            }
            //************
            //Operation
            ClearCheckAndGrid(tvOperation, gvOperation);
            dataGridView.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EInsulator.nodeKeysX.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EInsulator.nodeKeysX[i]);
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
                        //gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPole.nodeCount[i].ToString();
                        Atend.Base.Base.BUnit Unit = Atend.Base.Base.BUnit.Select_ByProductID(Convert.ToInt32(rootnode.Tag.ToString()));
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[2].Value = Unit.Name;
                        gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[3].Value = Operation.Count;
                    }

                }
            }
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

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectInsulatorXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectInsulatorXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectInsulatorXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EInsulator.DeleteX(SelectInsulatorXCode))
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
            SelectInsulatorXCode = XCode;
            Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.SelectByXCode(XCode);
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(insulator.ProductCode);
            Atend.Control.Common.selectedProductCode = insulator.ProductCode;
            SelectProduct();
            
            cboMaterial.SelectedValue = insulator.MaterialCode;
            cboVol.Text = insulator.Volt.ToString();
            txtName.Text = insulator.Name;
            cboType.SelectedIndex = Convert.ToByte(insulator.Type);
            txtComment.Text = insulator.Comment;
            //txtLenghtPipeinsulator.Text = insulator.LenghtInsulatorChain.ToString();
            tsbIsDefault.Checked = insulator.IsDefault;
            Code = insulator.Code;
            BindTreeViwAndGridEquipment(tvEquipment, gvSelectedEquipment);
        }

        private void SelectProductByCode()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByXCode(productCode);
            txtName.Text = product.Name;
        }

        private void BindMaterialToComboBox()
        {
            cboMaterial.ValueMember = "Code";
            cboMaterial.DisplayMember = "Name";
            cboMaterial.DataSource = MaterialTbl;

        }

        private void BindToTypeComboBox()
        {
            cboType.ValueMember = "Code";
            cboType.DisplayMember = "Name";
            cboType.DataSource = TypeTbl;

        }

        private void ReadEquipGroups(TreeView tvNames)
        {

            int i;
            string RootKey;


            XmlDocument _xmlDoc = new XmlDocument();
            //ed.WriteMessage("_xmlDoc Created\n");
            System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            //ed.WriteMessage("m Created\n");
            string fullPath = m.FullyQualifiedName;
            //ed.WriteMessage("fullPath : " + m.FullyQualifiedName + "\n");
            try
            {
                fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
                //ed.WriteMessage("New fllPath : " + fullPath.Substring(0, fullPath.LastIndexOf('\\')) + " No Error \n");
            }
            catch
            {
                //ed.WriteMessage("Exception \n");
            }
            string xmlPath = fullPath + "\\EquipmentName.xml";


            _xmlDoc.Load(xmlPath);
            i = 1;
            foreach (XmlElement xElement in _xmlDoc.DocumentElement)
            {
                foreach (XmlNode xnode in xElement.ChildNodes)
                {
                    RootKey = i.ToString();
                    tvNames.Nodes.Add(RootKey, xnode.Attributes[2].Value);
                    tvNames.Nodes[tvNames.Nodes.Count - 1].Tag = xnode.Attributes[1].Value;
                    i++;
                }
            }
        }

        private void BindDataToTreeView()
        {
            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvEquipment);
            BindDataToTreeViewOperation();
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

        private void frmConductor_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //btnNewMaterial.Visible = false;
            //SelectProductByCode();
            BindMaterialToComboBox();

            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;// cboMaterial.Items.Count - 1;
            }
            if (cboType.Items.Count > 0)
            {
                cboType.SelectedIndex = 0;
            }
            if (cboVol.Items.Count > 0)
            {
                cboVol.SelectedIndex = 0;   
            }
            
            //ReadEquipGroups(tvEquipment);
            BindDataToTreeView();
            BindToTypeComboBox();
        }

        private void btnNewMaterial_Click(object sender, EventArgs e)
        {
            //frmInsulatorType frminsulatorType = new frmInsulatorType();
            //frminsulatorType.ShowDialog();
            //BindMaterialToComboBox();
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

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsulatorSearch02 frminsulatorSearch = new frmInsulatorSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frminsulatorSearch);
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

            //*****


            //gvSelectedEquipment.Rows.Clear();
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

        private void btnDelete_Click(object sender, EventArgs e)
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

        private void btnDeletEquip_Click(object sender, EventArgs e)
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

        private void SelectProduct()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(Atend.Control.Common.selectedProductCode);
            txtName.Text = product.Name;
            txtCode.Text = product.Code.ToString();
            txtBackUpName.Text = product.Name;
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.Insulator);

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

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (CheckStatuseOfAccessChangeDefault())
                Delete();
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void tsbShare_Click(object sender, EventArgs e)
        {

            if (SelectInsulatorXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator), SelectInsulatorXCode))
                {
                    Atend.Base.Equipment.EInsulator  Insulator = Atend.Base.Equipment.EInsulator.SelectByXCode(SelectInsulatorXCode );
                    Code = Insulator.Code;
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

            //if (SelectInsulatorXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EInsulator.ShareOnServer(SelectInsulatorXCode))
            //    {
            //        Atend.Base.Equipment.EInsulator i1 = Atend.Base.Equipment.EInsulator.SelectByXCode(SelectInsulatorXCode);
            //        Code = i1.Code;
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

        private void txtBackUpName_TextChanged(object sender, EventArgs e)
        {

        }

    }
}