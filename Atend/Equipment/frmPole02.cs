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

namespace Atend.Equipment
{
    public partial class frmPole02 : Form
    {

        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        public int ProductCode = -1;
        Guid SelectedPoleCode = Guid.Empty;
        public bool IsDefault = false;
        int code = -1;
        bool ForceToClose = false;

        public frmPole02()
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

        private void Reset()
        {
            SelectedPoleCode = Guid.Empty;
            txtHeight.Text = string.Empty;
            txtPower.Text = string.Empty;
            txtTopCrossSectionArea.Text = string.Empty;
            txtButtomCrossSectionArea.Text = string.Empty;
            txtName.Text = string.Empty;
            cboPoleType.SelectedIndex = 0;
            cboShape.SelectedIndex = 0;
            gvSelectedProduct.Rows.Clear();
            IsDefault = false;
            tsbIsDefault.Checked = false;
            ClearCheckAndGrid(tvOperation, gvSelectedProduct);
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            txtComment.Text = string.Empty;
            Atend.Control.Common.selectedProductCode = -1;
            txtBackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
        }

        private void Save()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            btnInsert.Focus();
            ed.WriteMessage("Start Save\n");
            Atend.Base.Equipment.EPole Pole = new Atend.Base.Equipment.EPole();
            ArrayList EOperation = new ArrayList();
            //ArrayList EPackage = new ArrayList();
            //ArrayList EContainer = new ArrayList();
            Pole.ProductCode = Atend.Control.Common.selectedProductCode;
            Pole.Height = double.Parse(txtHeight.Text);
            Pole.Power = double.Parse(txtPower.Text);
            //Pole.Wc = double.Parse(txtWC.Text);
            ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~PoleType={0}\n", cboPoleType.SelectedIndex.ToString());
            Pole.Type = Convert.ToByte(cboPoleType.SelectedIndex);
            Pole.TopCrossSectionArea = double.Parse(txtTopCrossSectionArea.Text);
            Pole.ButtomCrossSectionArea = double.Parse(txtButtomCrossSectionArea.Text);
            //Pole.ContainerCode = SelectedPoleCode;
            Pole.Name = txtName.Text;
            Pole.Comment = txtComment.Text;
            Pole.Shape = Convert.ToByte(cboShape.SelectedIndex);
            Pole.IsDefault = IsDefault;
            Pole.Code = code;
            //saveOperation

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
            Pole.EquipmentList = EPackageProduct;

            //Operation
            for (int i = 0; i < gvSelectedProduct.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvSelectedProduct.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvSelectedProduct.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);

            }


            Pole.OperationList = EOperation;
            //if (Atend.Control.Common.selectedProductCode != -1)
            //{
            if (SelectedPoleCode == Guid.Empty)
            {
                if (Pole.InsertX())
                {
                    Reset();
                }
                else
                    MessageBox.Show("امکان ثبت کردن اطلاعات نمی باشد", "خطا");
            }
            else
            {
                Pole.XCode = SelectedPoleCode;
                //MessageBox.Show("go to update method : " + Pole.Code + "\n");
                if (Pole.UpdateX())
                {
                    ed.WriteMessage("Finish Updated\n");
                    Reset();
                }
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
            //}
            //else
            //{
            //    MessageBox.Show("لطفا ابتدا یک کالا را از پشتیبان انتخاب کنید", "خطا");
            //}
        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectedPoleCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.SelectByXCode(SelectedPoleCode);
                    if (Pole.IsDefault || IsDefault)
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
            if (Atend.Base.Equipment.EPole.SearchByName(txtName.Text) == true && SelectedPoleCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtHeight.Text))
            {
                MessageBox.Show("لطفا ارتفاع را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtHeight.Focus();
                txtHeight.Select(0, txtHeight.Text.Length);
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtHeight.Text))
            {
                MessageBox.Show("لطفاً ارتفاع را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtHeight.Focus();
                txtHeight.Select(0, txtHeight.Text.Length);
                return false;
            }


            if (string.IsNullOrEmpty(txtPower.Text))
            {
                MessageBox.Show("لطفا قدرت را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtPower.Focus();
                txtPower.Select(0, txtPower.Text.Length);
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtPower.Text))
            {
                MessageBox.Show("لطفاً قدرت را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtPower.Focus();
                txtPower.Select(0, txtPower.Text.Length);
                return false;
            }


            if (string.IsNullOrEmpty(txtButtomCrossSectionArea.Text))
            {
                MessageBox.Show("لطفا سطح مقطع بالایی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtButtomCrossSectionArea.Focus();
                txtButtomCrossSectionArea.Select(0, txtButtomCrossSectionArea.Text.Length);
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtTopCrossSectionArea.Text))
            {
                MessageBox.Show("لطفاً سطح مقطع بالایی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtTopCrossSectionArea.Focus();
                txtTopCrossSectionArea.Select(0, txtTopCrossSectionArea.Text.Length);
                return false;
            }


            if (string.IsNullOrEmpty(txtButtomCrossSectionArea.Text))
            {
                MessageBox.Show("لطفا سطح مقطع پایینی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtButtomCrossSectionArea.Focus();
                txtButtomCrossSectionArea.Select(0, txtButtomCrossSectionArea.Text.Length);
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtButtomCrossSectionArea.Text))
            {
                MessageBox.Show("لطفاً سطح مقطع پایینی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtButtomCrossSectionArea.Focus();
                txtButtomCrossSectionArea.Select(0, txtButtomCrossSectionArea.Text.Length);
                return false;
            }


            if (string.IsNullOrEmpty(cboShape.Text))
            {
                MessageBox.Show("لطفا نوع شکل را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cboShape.Focus();

                return false;
            }


            Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.CheckForExist(Convert.ToDouble(txtHeight.Text), Convert.ToDouble(txtPower.Text), Convert.ToDouble(txtTopCrossSectionArea.Text),
                                                                                        Convert.ToDouble(txtButtomCrossSectionArea.Text), Convert.ToByte(cboPoleType.SelectedIndex), Convert.ToByte(cboShape.SelectedIndex));
            if (pole.Code != -1 && SelectedPoleCode == Guid.Empty)
            {
                if (MessageBox.Show("پایه با مشخصات داده شده موجود میباشد\n\n پایه با مشخصات فوق  : " + pole.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    txtHeight.Focus();
                    return false;
                }
            }


            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvSelectedEquipment, 2))
            {
                MessageBox.Show("لطفا تعداد تجهیزات جانبی را با فرمت مناسب وارد نمایید", "خطا");
                return false;
            }
            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvSelectedProduct, 3))
            {
                MessageBox.Show("لطفا تعداد آماده سازی را با فرمت مناسب وارد نمایید", "خطا");
                return false;
            }

            for (int j = 0; j < gvSelectedEquipment.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                _EProductPackage.XCode = new Guid(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.Count = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedEquipment.Rows[j].Cells[3].Value.ToString());

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(SelectedPoleCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }

            }

            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void BindDataToTreeView()
        {
            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvEquipment);

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

        //private void ReadEquipGroups(TreeView tvNames)
        //{

        //    int i;
        //    string RootKey;

        //    XmlDocument _xmlDoc = new XmlDocument();
        //    _xmlDoc.Load("EquipmentName.xml");
        //    i = 1;
        //    foreach (XmlElement xElement in _xmlDoc.DocumentElement)
        //    {
        //        foreach (XmlNode xnode in xElement.ChildNodes)
        //        {
        //            RootKey = i.ToString();
        //            tvNames.Nodes.Add(RootKey, xnode.Attributes[2].Value);
        //            tvNames.Nodes[tvNames.Nodes.Count - 1].Tag = xnode.Attributes[1].Value;
        //            i++;
        //        }
        //    }
        //}

        //private void ReadContainGroups(TreeView tvNames)
        //{

        //    int i;
        //    string RootKey;

        //    XmlDocument _xmlDoc = new XmlDocument();
        //    _xmlDoc.Load("ContainerName.xml");
        //    i = 1;
        //    foreach (XmlElement xElement in _xmlDoc.DocumentElement)
        //    {
        //        foreach (XmlNode xnode in xElement.ChildNodes)
        //        {
        //            RootKey = i.ToString();
        //            tvNames.Nodes.Add(RootKey, xnode.Attributes[2].Value);
        //            tvNames.Nodes[tvNames.Nodes.Count - 1].Tag = xnode.Attributes[1].Value;
        //            i++;
        //        }
        //    }
        //}

        public void BindDataToOwnControl(Guid XCode)
        {
            ClearCheckAndGrid(tvOperation, gvSelectedProduct);
            ClearCheckAndGrid(tvOperation, gvSelectedProduct);


            SelectedPoleCode = XCode;

            Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.SelectByXCode(XCode);
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ById(Pole.ProductCode);
            Atend.Control.Common.selectedProductCode = Pole.ProductCode;
            SelectProduct();

            txtName.Text = Pole.Name;


            //ProductCode = product.ID;
            txtHeight.Text = Convert.ToString(Math.Round(Pole.Height, 4));
            txtPower.Text = Convert.ToString(Math.Round(Pole.Power, 4));
            //txtWC.Text = Convert.ToString(Math.Round(Pole.Wc, 2));
            txtTopCrossSectionArea.Text = Convert.ToString(Math.Round(Pole.TopCrossSectionArea, 4));
            txtButtomCrossSectionArea.Text = Convert.ToString(Math.Round(Pole.ButtomCrossSectionArea, 4));
            cboPoleType.SelectedIndex = Pole.Type;
            cboShape.SelectedIndex = Pole.Shape;
            txtComment.Text = Pole.Comment;
            tsbIsDefault.Checked = Pole.IsDefault;
            code = Pole.Code;
            BindTreeViwAndGrid();

        }

        private void BindTreeViwAndGrid()
        {
            //EQUIPMENT
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            gvSelectedEquipment.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EPole.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EPole.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {
                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        if (Atend.Base.Equipment.EPole.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            if (chileNode.Tag.ToString() == s)
                            {
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquipment.Rows.Add();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPole.nodeCountEPackageX[i].ToString();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[3].Value = chileNode.Name;
                            }
                        }
                    }
                }
            }
            //************
            //Operation
            ClearCheckAndGrid(tvOperation, gvSelectedProduct);
            gvSelectedProduct.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EPole.nodeKeys.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EPole.nodeKeys[i]);
                string s = Operation.ProductID.ToString();

                foreach (TreeNode rootnode in tvOperation.Nodes)
                {

                    //foreach (TreeNode chileNode in rootnode.Nodes)
                    //{
                    if (rootnode.Tag.ToString() == s)
                    {
                        
                        gvSelectedProduct.Rows.Add();
                        gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value = rootnode.Tag;
                        gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[1].Value = rootnode.Text;
                        //gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPole.nodeCount[i].ToString();
                        Atend.Base.Base.BUnit Unit = Atend.Base.Base.BUnit.Select_ByProductID(Convert.ToInt32(rootnode.Tag.ToString()));
                        gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = Unit.Name;
                        gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value = Operation.Count;
                    }

                }
            }
        }

        private void BindDataToPoleType()
        {
            //Extra
            //cboPoleType.DisplayMember = "Name";
            //cboPoleType.ValueMember = "Code";
            //cboPoleType.DataSource = Atend.Base.Equipment.EPoleType.SelectAll();
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectedPoleCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectedPoleCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            Atend.Base.Equipment.EPoleTip ept = Atend.Base.Equipment.EPoleTip.SelectByPoleXCode(SelectedPoleCode);
            if (ept.Code != -1)
            {
                MessageBox.Show("حذف پایه بدلیل وجود در تیپ بندی امکانپذیر نمی باشد ", "خطا");
                return;
            }
            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedPoleCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EPole.DeleteX(SelectedPoleCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                {
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
                }
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

            int count = dataGridView.Rows.Count - 1;

            for (int i = count; i >= 0; i--)
            {
                dataGridView.Rows.RemoveAt(i);
            }
            gvSelectedProduct.Rows.Clear();
            dataGridView.Refresh();
        }

        //private void BindTreeViwAndGridEquipment(TreeView treeView, DataGridView dataGridView)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
        //    ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
        //    dataGridView.Refresh();

        //    for (int i = 0; i < Atend.Base.Equipment.EPole.nodeKeysEPackage.Count; i++)
        //    {
        //        string s = Atend.Base.Equipment.EPole.nodeKeysEPackage[i].ToString();

        //        foreach (TreeNode rootnode in tvEquipment.Nodes)
        //        {

        //            foreach (TreeNode chileNode in rootnode.Nodes)
        //            {

        //                if (Convert.ToBoolean(Atend.Base.Equipment.EPole.nodeIsContainerEPackage[i].ToString()) == false)
        //                {
        //                    if (int.Parse(chileNode.Tag.ToString()) == int.Parse(s))
        //                    {
        //                        chileNode.Checked = true;
        //                        gvSelectedEquipment.Rows.Add();
        //                        gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
        //                        gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
        //                        gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPole.nodeCountEPackage[i].ToString();

        //                    }
        //                }

        //            }
        //        }
        //    }
        //}
        //private void BindTreeViwAndGridContainer(TreeView treeView, DataGridView dataGridView)
        //{

        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    ClearCheckAndGrid(tvContainer, gvSelectedContainer);
        //    ClearCheckAndGrid(tvContainer, gvSelectedContainer);
        //    dataGridView.Refresh();

        //    for (int i = 0; i < Atend.Base.Equipment.EPole.nodeKeysEPackage.Count; i++)
        //    {
        //        string s = Atend.Base.Equipment.EPole.nodeKeysEPackage[i].ToString();

        //        foreach (TreeNode rootnode in tvContainer.Nodes)
        //        {

        //            foreach (TreeNode chileNode in rootnode.Nodes)
        //            {
        //                if (Convert.ToBoolean(Atend.Base.Equipment.EPole.nodeIsContainerEPackage[i].ToString()) == true)
        //                {
        //                    if (int.Parse(chileNode.Tag.ToString()) == int.Parse(s))
        //                    {
        //                        chileNode.Checked = true;
        //                        gvSelectedContainer.Rows.Add();
        //                        gvSelectedContainer.Rows[gvSelectedContainer.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
        //                        gvSelectedContainer.Rows[gvSelectedContainer.Rows.Count - 1].Cells[1].Value = chileNode.Text;
        //                        gvSelectedContainer.Rows[gvSelectedContainer.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPole.nodeCountEPackage[i].ToString();

        //                    }
        //                }

        //            }
        //        }
        //    }

        //}

        private void frmPole_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //BindDataToPoleType();

            //ReadEquipGroups(tvEquipment);
            //ReadContainGroups(tvContainer);
            BindDataToTreeView();

            //ed.WriteMessage("@@@@@@@@@******************~~~~~~~~~~~~~~~\n");
            if (cboPoleType.Items.Count > 0)
            {
                cboPoleType.SelectedIndex = 0;// cboPoleType.Items.Count - 1;
            }
            if (cboShape.Items.Count > 0)
            {
                cboShape.SelectedIndex = 0;// cboShape.Items.Count - 1;
            }

        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                gvSelectedProduct.Refresh();
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

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPoleSearch02 frmPoleSearch = new frmPoleSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmPoleSearch);
        }

        private void btnNewPoleType_Click(object sender, EventArgs e)
        {
            //Extra
            //frmPoleType frmPoletype = new frmPoleType();
            //frmPoletype.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            //Extra
            //frmPoleType frmpoleType = new frmPoleType();
            //frmpoleType.ShowDialog();
            //BindDataToPoleType();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //Add Selected Operation Nodes To DataGridView
            //gvSelectedProduct.Rows.Clear();
            for (int i = 0; i < tvOperation.Nodes.Count; i++)
            {

                if (tvOperation.Nodes[i].Checked)
                {
                    bool sw = false;
                    for (int j = 0; j < gvSelectedProduct.Rows.Count; j++)
                    {
                        if (gvSelectedProduct.Rows[j].Cells[0].Value.ToString() == tvOperation.Nodes[i].Tag.ToString())
                            sw = true;
                    }

                    if (!sw)
                    {
                        gvSelectedProduct.Rows.Add();
                        gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value = tvOperation.Nodes[i].Tag.ToString();
                        gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[1].Value = tvOperation.Nodes[i].Text.ToString();
                        Atend.Base.Base.BUnit Unit = Atend.Base.Base.BUnit.Select_ByProductID(Convert.ToInt32(tvOperation.Nodes[i].Tag.ToString()));
                        gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = Unit.Name;
                        gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value = 1;
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

        private void tvProduct_AfterCheck(object sender, TreeViewEventArgs e)
        {
        }

        private void tvEquipment_BeforeCheck_1(object sender, TreeViewCancelEventArgs e)
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

        private void tvContainer_BeforeCheck(object sender, TreeViewCancelEventArgs e)
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

        private void gvSelectedProduct_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //int i=Convert.ToInt32(gvSelectedProduct.Rows[e.RowIndex-1].Cells[0].Value);
            //ed.WriteMessage("E.ROWINDEX:=" + e.RowIndex.ToString()+"\n");
            //ed.WriteMessage("i:=" + i.ToString()+"\n");
            //for (int j = 0; j < tvOperation.Nodes.Count; j++)
            //{
            //    ed.WriteMessage("BeforIf Tag"+tvOperation.Nodes[j].Tag.ToString()+"\n");
            //    if ((tvOperation.Nodes[j].Checked) && (Convert.ToInt32(tvOperation.Nodes[j].Tag)==i))
            //    {
            //        ed.WriteMessage("After IF Tag"+tvOperation.Nodes[j].Tag.ToString()+"\n");
            //        tvOperation.Nodes[j].Checked = false;
            //    }
            //}

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvSelectedProduct.Rows.Count > 0)
            {
                foreach (TreeNode rootnode in tvOperation.Nodes)
                {
                    if (rootnode.Tag.ToString() == gvSelectedProduct.Rows[gvSelectedProduct.CurrentRow.Index].Cells[0].Value.ToString())
                    {
                        rootnode.Checked = false;
                    }
                }

                gvSelectedProduct.Rows.RemoveAt(gvSelectedProduct.CurrentRow.Index);
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
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.Pole);

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
            }
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                gvSelectedProduct.Refresh();
                Save();
            }
        }

        private void toolStripLabel8_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void toolStripLabel9_Click(object sender, EventArgs e)
        {
            frmPoleSearch02 frmPoleSearch = new frmPoleSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmPoleSearch);
        }

        private void toolStripLabel10_Click(object sender, EventArgs e)
        {
            Close();
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

            if (SelectedPoleCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.Pole), SelectedPoleCode))
                {
                    Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.SelectByXCode(SelectedPoleCode);
                    code  = Pole.Code;
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

            //if (SelectedPoleCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EPole.ShareOnServer(SelectedPoleCode))
            //    {
            //        Atend.Base.Equipment.EPole p1 = Atend.Base.Equipment.EPole.SelectByXCode(SelectedPoleCode);
            //        code = p1.Code;
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

        private void button1_Click_1(object sender, EventArgs e)
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}