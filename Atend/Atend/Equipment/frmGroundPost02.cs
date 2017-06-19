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
    public partial class frmGroundPost02 : Form
    {
        public int ProductCode = -1;
        byte[] image;
        Guid selectedGroundPostXCode = Guid.Empty;
        public bool IsDefault = false;
        int Code = -1;

        //////////DataTable dt = new DataTable();
        //////////DataColumn dc = new DataColumn("productCode");
        //////////DataColumn dc1 = new DataColumn("TableType");
        //////////DataColumn dc2 = new DataColumn("cell");
        //////////DataColumn dc3 = new DataColumn("Count");
        //DataColumn dc4 = new DataColumn("Name");
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        ArrayList arItems = new ArrayList();
        bool ForceToClose = false;


        public frmGroundPost02()
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

            //////////////dc.DataType = System.Type.GetType("System.Int32");
            //////////////dt.Columns.Add(dc);
            //////////////dc1.DataType = System.Type.GetType("System.Int32");
            //////////////dt.Columns.Add(dc1);
            //////////////dc2.DataType = System.Type.GetType("System.Int32");
            //////////////dt.Columns.Add(dc2);
            //////////////dc3.DataType = System.Type.GetType("System.Int32");
            //////////////dt.Columns.Add(dc3);
            //dc4.DataType = System.Type.GetType("System.String");
            //dt.Columns.Add(dc4);
        }

        private void Reset()
        {
            selectedGroundPostXCode = Guid.Empty;
            txtCapacity.Text = string.Empty;
            txtName.Text = string.Empty;
            txtComment.Text = string.Empty;
            //cboAdvanceType.SelectedIndex = 0;
            //cboGroundType.SelectedIndex = 0;
            //cboOperation.SelectedIndex = 0;
            //cboType.SelectedIndex = 0;
            //nudCountCell.Value = 0;
            //nudSelectCount.Value = 0;
            //nudCountCell.Value = 0;
            //cboNumCount.Items.Clear();
            /////////////dt.Rows.Clear();
            tsbIsDefault.Checked = false;
            IsDefault = false;
            ClearCheckAndGrid(tvProduct, gvSelectedProduct);
            ClearCheckAndGrid(tvOperation, gvOperation);
            ProductCode = -1;
            txtImage.Text = string.Empty;
            pictureBox1.Image = Image.FromFile(Atend.Control.Common.fullPath + "\\Consol.jpg");
            Code = -1;

            cboAdvanceType.SelectedIndex = 0;
            cboGroundType.SelectedIndex = 0;
            cboType.SelectedIndex = 0;

            txtName.Focus();
        }

        private void Save()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            txtName.Focus();
            btnInsert.Focus();

            ArrayList EPackageProduct = new ArrayList();
            ArrayList cellPackage = new ArrayList();
            ArrayList EOperation = new ArrayList();

            Atend.Base.Equipment.EGroundPost groundpost = new Atend.Base.Equipment.EGroundPost();
            groundpost.Name = txtName.Text;
            groundpost.GroundType = Convert.ToByte(cboGroundType.SelectedIndex);
            groundpost.Type = Convert.ToByte(cboType.SelectedIndex);
            groundpost.AdvanceType = Convert.ToByte(cboAdvanceType.SelectedIndex);
            groundpost.Capacity = Convert.ToInt32(txtCapacity.Text);
            groundpost.Comment = txtComment.Text;
            groundpost.IsDefault = IsDefault;
            groundpost.Code = Code;

            //Operation
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                //ed.WriteMessage("In Add OperationToArray");
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();

                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);

                EOperation.Add(_EOperation);

            }

            groundpost.OperationList = EOperation;
            //******

            for (int j = 0; j < gvSelectedProduct.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();

                //_EProductPackage.ProductCode = 0;// Convert.ToInt32(gvSelectedProduct.Rows[j].Cells[0].Value.ToString());
                //MessageBox.Show(gvSelectedProduct.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.XCode = new Guid(gvSelectedProduct.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.Count = Convert.ToInt32(gvSelectedProduct.Rows[j].Cells[2].Value.ToString());
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedProduct.Rows[j].Cells[3].Value.ToString());
                EPackageProduct.Add(_EProductPackage);
                //ed.WriteMessage("aaa \n");
            }
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
            //   //Atend.Base.Equipment.EGroundPostCell   _cell=new Atend.Base.Equipment.EGroundPostCell();
            //   ed.WriteMessage("Begin 1\n");
            //    _EProductPackage.ProductCode = Convert.ToInt32(dr["productCode"]);
            //    ed.WriteMessage("Begin 2\n");

            //    _EProductPackage.Count = Convert.ToInt32(dr["Count"]);
            //    ed.WriteMessage("Value:" + "\n");
            //    _EProductPackage.TableType = Convert.ToInt16(dr["TableType"]);
            //    //_cell.Num = Convert.ToInt32(dr["cell"]);
            //    EPackageProduct.Add(_EProductPackage);
            //    //cellPackage.Add(_cell);
            //    ed.WriteMessage("aaa \n");
            //}

            groundpost.EquipmentList = EPackageProduct;
            //groundpost.CellList = cellPackage;
            //ed.WriteMessage("selectedgroundPost" + selectedGroundPost.ToString());

            groundpost.Image = null;
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
            groundpost.Image = br.ReadBytes((Int32)br.BaseStream.Length);
            fs.Dispose();

            if (selectedGroundPostXCode == Guid.Empty)
            {
                if (groundpost.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");

            }
            else
            {
                groundpost.XCode = selectedGroundPostXCode;
                //ed.WriteMessage("consol.code:" + selectedConsol.ToString());
                //MessageBox.Show("GoToUpdate");
                if (groundpost.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");

            }
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(selectedGroundPostXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(selectedGroundPostXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (selectedGroundPostXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EGroundPost.DeleteX(selectedGroundPostXCode))
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
            //ed.WriteMessage(Code.ToString() + "\n");
            Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(XCode);
            selectedGroundPostXCode = XCode;
            txtName.Text = groundPost.Name;
            cboType.SelectedIndex = groundPost.Type;
            cboAdvanceType.SelectedIndex = groundPost.AdvanceType;
            cboGroundType.SelectedIndex = groundPost.GroundType;
            //cboOperation.SelectedValue = groundPost.OperationCode;
            txtCapacity.Text = groundPost.Capacity.ToString();
            txtComment.Text = groundPost.Comment;
            //nudCountCell.Value = groundPost.CellCount;
            tsbIsDefault.Checked = groundPost.IsDefault;

            Byte[] byteBLOBData = new Byte[0];
            byteBLOBData = (Byte[])(groundPost.Image);
            MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
            image = groundPost.Image;
            
            Code = groundPost.Code;
            pictureBox1.Image = Image.FromStream(stmBLOBData);

            BindTreeViwAndGridEquipment();
            //BindTreeViwAndGridContainer(tvContainer, gvSelectedContainer);
        }

        private void BindTreeViwAndGridEquipment()
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            ClearCheckAndGrid(tvProduct, gvSelectedProduct);
            ClearCheckAndGrid(tvProduct, gvSelectedProduct);
            gvSelectedProduct.Refresh();

            for (int i = 0; i < Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EGroundPost.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvProduct.Nodes)
                {

                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        //ed.WriteMessage("nodeTypeEPackage:" + Atend.Base.Equipment.EGroundPost.nodeTypeEPackage[i].ToString() + "\n");
                        //ed.WriteMessage("rootnode.Tag:" + rootnode.Tag.ToString() + "\n");
                        if (Atend.Base.Equipment.EGroundPost.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            //ed.WriteMessage(string.Format(" child tag : {0} , s: {1} \n", chileNode.Tag.ToString(), s));
                            if (chileNode.Tag.ToString() == s)
                            {
                                //ed.WriteMessage("I am in the if \n");
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedProduct.Rows.Add();
                                //ed.WriteMessage("Child tag : " + chileNode.Tag.ToString() + "\n");
                                gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                //ed.WriteMessage("Child tag : " + chileNode.Text.ToString() + "\n");
                                gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EGroundPost.nodeCountEPackageX[i].ToString();
                                gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value = chileNode.Name;
                                gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[4].Value = rootnode.Tag;

                            }
                        }

                    }

                }

            }


            //(((((((((((((((
            ClearCheckAndGrid(tvOperation, gvOperation);
            gvOperation.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EGroundPost.nodeKeysX.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EGroundPost.nodeKeysX[i]);
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
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

            // dt.Rows.Clear();
            // foreach (DataRow drGround in Atend.Base.Equipment.EGroundPost.groundPostSubEquip.Rows)
            // {
            //     DataRow dr = dt.NewRow();
            //     dr["productCode"] = Convert.ToInt32(drGround["productCode"].ToString());
            //     dr["Cell"] = Convert.ToInt32(drGround["Cell"].ToString());
            //     dr["count"] = Convert.ToInt32(drGround["count"].ToString());
            //     dr["TableType"] = Convert.ToInt32(drGround["TableType"].ToString());
            //     dt.Rows.Add(dr);
            // }
            ////EventArgs e=new EventArgs();
            //// cboNumCount_SelectedIndexChanged(cboNumCount,e);
            // //Opeartion
            // ClearCheckAndGrid(tvOperation, gvOperation);
            // gvOperation.Refresh();
            // for (int i = 0; i < Atend.Base.Equipment.EGroundPost.nodeKeys.Count; i++)
            // {
            //     string s = Atend.Base.Equipment.EGroundPost.nodeKeys[i].ToString();
            //     //MessageBox.Show(Atend.Base.Equipment.EGroundPost.nodeKeys[i].ToString());
            //     foreach (TreeNode rootnode in tvOperation.Nodes)
            //     {

            //         //foreach (TreeNode chileNode in rootnode.Nodes)
            //         //{
            //         ed.WriteMessage("RootNOde.Tag= "+rootnode.Tag.ToString()+"  NodeKey = "+Atend.Base.Equipment.EGroundPost.nodeKeys[i].ToString()+"\n");
            //         if (rootnode.Tag.ToString() == s)
            //         {
            //             ed.WriteMessage("I Am In The IF");
            //             rootnode.Checked = true;
            //             gvOperation.Rows.Add();
            //             gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[0].Value = rootnode.Tag;
            //             gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[1].Value = rootnode.Text;
            //             //gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPole.nodeCount[i].ToString();

            //         }

            //     }
            // }
            //******

        }

        //private void BindDataToOperationType()
        //{
        //    cboOperation.DisplayMember = "Name";
        //    cboOperation.ValueMember = "ID";
        //    cboOperation.DataSource = Atend.Base.Base.BProduct.SelectByType(Convert.ToInt32(Atend.Control.Enum.ProductType.Operation));
        //}

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

        private void tvProduct_BeforeCheck(object sender, TreeViewCancelEventArgs e)
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
            //Atend.Global.Utility.UBinding.BindDataToTreeViewXForPost(tvProduct, 1);
            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvProduct);
            BindDataToTreeViewOperation();

        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (selectedGroundPostXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EGroundPost Equip = Atend.Base.Equipment.EGroundPost.SelectByXCode(selectedGroundPostXCode);
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
                txtName.Select(0, txtName.Text.Length);
                return false;
            }
            if (Atend.Base.Equipment.EGroundPost.SearchByName(txtName.Text) == true && selectedGroundPostXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCapacity.Text))
            {
                MessageBox.Show("لطفا ظرفیت را مشخص نمایید", "خطا");
                txtCapacity.Focus();
                txtCapacity.Select(0, txtCapacity.Text.Length);
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtCapacity.Text))
            {
                MessageBox.Show("لطفاً ظرفیت پست را با فرمت مناسب وارد نمایید", "خطا");
                txtCapacity.Focus();
                txtCapacity.Select(0, txtCapacity.Text.Length);
                return false;
            }

            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvSelectedProduct, 2))
            {
                MessageBox.Show("لطفاً فرمت تعداد زیر تجهیزات را اصلاح نمایید", "خطا");
                //txtCapacity.Focus();
                //txtCapacity.Select(0, txtCapacity.Text.Length);
                return false;

            }

            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvOperation, 3))
            {
                MessageBox.Show("لطفاً فرمت تعداد آماده سازی را اصلاح نمایید", "خطا");
                //txtCapacity.Focus();
                //txtCapacity.Select(0, txtCapacity.Text.Length);
                return false;

            }


            //if (nudCountCell.Value == 0)
            //{
            //    MessageBox.Show("لطفاً تعداد سلول را مشخص نمایید", "خطا");
            //    nudCountCell.Focus();
            //    return false;
            //}
            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvOperation, 3))
            {
                MessageBox.Show("لطفا تعداد آماده سازی را با فرمت مناسب وارد نمایید", "خطا");
                gvOperation.Focus();
                return false;
            }
            if (!Atend.Global.Utility.UBinding.CheckGridValidation(gvSelectedProduct, 2))
            {
                MessageBox.Show("لطفا تعداد تجهیزات جانبی را با فرمت مناسب وارد نمایید", "خطا");
                gvSelectedProduct.Focus();
                return false;
            }

            for (int j = 0; j < gvSelectedProduct.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                _EProductPackage.XCode = new Guid(gvSelectedProduct.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.Count = Convert.ToInt32(gvSelectedProduct.Rows[j].Cells[2].Value.ToString());
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedProduct.Rows[j].Cells[3].Value.ToString());

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(selectedGroundPostXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedProduct.Focus();
                    return false;
                }
            }

            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        //private void nudCountCell_ValueChanged(object sender, EventArgs e)
        //{
        //    cboNumCount.Items.Clear();
        //    //MessageBox.Show(nudCountCell.Value.ToString());
        //    for (int i = 1; i <=Convert.ToInt32(nudCountCell.Value); i++)
        //    {
        //        cboNumCount.Items.Add(i);
        //    }
        //    cboNumCount.SelectedIndex = 0;

        //    for (int i = 0; i < nudCountCell.Value; i++)
        //    {
        //        DataTable dtItems = new DataTable();

        //    }
        //}

        private void frmGroundPostcs_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //ArrayList ar = new ArrayList();

            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(ProductCode);
            txtName.Text = product.Name;
            //ReadEquipGroups(tvProduct);
            BindDataToTreeView();
            //BindDataToOperationType();
            cboAdvanceType.SelectedIndex = 0;
            cboGroundType.SelectedIndex = 0;
            //cboOperation.SelectedIndex = 0;
            cboType.SelectedIndex = 0;

            string PicturePath = Atend.Control.Common.fullPath + "\\Consol.jpg";
            pictureBox1.Image = Image.FromFile(PicturePath);

        }

        //private void ReadEquipGroups(TreeView tvNames)
        //{

        //    int i;
        //    string RootKey;
        //    XmlDocument _xmlDoc = new XmlDocument();
        //    System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
        //    string fullPath = m.FullyQualifiedName;
        //    try
        //    {
        //        fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
        //    }
        //    catch
        //    {
        //    }
        //    string xmlPath = fullPath + "\\EquipmentName.xml";
        //    _xmlDoc.Load(xmlPath);
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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //Equipment
            Boolean canAdd = true;
            for (int i = 0; i < tvProduct.Nodes.Count; i++)
            {
                for (int j = 0; j < tvProduct.Nodes[i].Nodes.Count; j++)
                {
                    if (tvProduct.Nodes[i].Nodes[j].Checked)
                    {
                        canAdd = true;
                        for (int k = 0; k < gvSelectedProduct.Rows.Count; k++)
                        {
                            if ((gvSelectedProduct.Rows[k].Cells[0].Value.ToString() == tvProduct.Nodes[i].Nodes[j].Tag.ToString()) && (Convert.ToInt32(gvSelectedProduct.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Name.ToString())))
                            {
                                canAdd = false;
                                //ed.WriteMessage("No Allow To Add Row\n");
                            }
                        }
                        if (canAdd)
                        {
                            //ed.WriteMessage("AddRow\n");
                            //ed.WriteMessage("TAg= " + tvProduct.Nodes[i].Nodes[j].Tag.ToString()+"\n");
                            //ed.WriteMessage("Text= " + tvProduct.Nodes[i].Nodes[j].Text.ToString()+"\n");
                            //ed.WriteMessage("Name= " + tvProduct.Nodes[i].Nodes[j].Name.ToString()+"\n");
                            gvSelectedProduct.Rows.Add();
                            //MessageBox.Show(tvProduct.Nodes[i].Nodes[j].Tag.ToString());
                            gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value = tvProduct.Nodes[i].Nodes[j].Tag.ToString();
                            gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[1].Value = tvProduct.Nodes[i].Nodes[j].Text.ToString();
                            gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value = tvProduct.Nodes[i].Nodes[j].Name.ToString();
                            gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = 1;
                            gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[4].Value = tvProduct.Nodes[i].Tag.ToString();



                        }

                    }
                    else
                    {
                        for (int k = 0; k < gvSelectedProduct.Rows.Count; k++)
                        {
                            if ((gvSelectedProduct.Rows[k].Cells[0].Value.ToString() == tvProduct.Nodes[i].Nodes[j].Tag.ToString()) && (Convert.ToInt32(gvSelectedProduct.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Name.ToString())))
                            {
                                //ed.WriteMessage("Name To Delete" + gvSelectedProduct.Rows[k].Cells[1].Value.ToString() + "\n");
                                gvSelectedProduct.Rows.RemoveAt(k);

                            }
                        }

                    }
                }
            }


            gvSelectedProduct.Refresh();


            //double Cap = 0;//Convert.ToInt32(txtCapacity.Text);

            //for (int i = 0; i < gvSelectedProduct.Rows.Count; i++)
            //{
            //    if (Convert.ToInt32(gvSelectedProduct.Rows[i].Cells[4].Value.ToString()) == (int)Atend.Control.Enum.ProductType.Transformer)
            //    {
            //        Atend.Base.Equipment.ETransformer Trans = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(gvSelectedProduct.Rows[i].Cells[0].Value.ToString()));
            //        Cap = Cap + Convert.ToInt32(gvSelectedProduct.Rows[i].Cells[2].Value.ToString()) * Trans.Capaciy;
            //    }
            //}

            txtCapacity.Text = CalculateCapasity().ToString();// Cap.ToString();

            //************************************************^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            //Boolean canAdd=true;
            //if (!string.IsNullOrEmpty(cboNumCount.Text))
            //{
            //    for (int i = 0; i < tvProduct.Nodes.Count; i++)
            //    {
            //        for (int j = 0; j < tvProduct.Nodes[i].Nodes.Count; j++)
            //        {
            //            if (tvProduct.Nodes[i].Nodes[j].Checked)
            //            {
            //                canAdd = true;
            //                for (int k = 0; k < gvSelectedProduct.Rows.Count; k++)
            //                {
            //                    if ((Convert.ToInt32(gvSelectedProduct.Rows[k].Cells[0].Value.ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Tag.ToString())) && (Convert.ToInt32(gvSelectedProduct.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Name.ToString())))
            //                    {
            //                        canAdd = false;
            //                        ed.WriteMessage("No Allow To Add Row\n");
            //                    }
            //                }
            //                if (canAdd)
            //                {
            //                    ed.WriteMessage("AddRow\n");
            //                    gvSelectedProduct.Rows.Add();
            //                    gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value = tvProduct.Nodes[i].Nodes[j].Tag.ToString();
            //                    gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[1].Value = tvProduct.Nodes[i].Nodes[j].Text.ToString();
            //                    gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value = tvProduct.Nodes[i].Nodes[j].Name.ToString();
            //                    gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = 0;
            //                    DataRow dr = dt.NewRow();
            //                    //dr["Name"] = tvProduct.Nodes[i].Nodes[j].Text.ToString();
            //                    dr["productCode"] = Convert.ToInt32(gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value.ToString());
            //                    dr["Cell"] = Convert.ToInt32(cboNumCount.Text);
            //                    dr["count"] = Convert.ToInt32(gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value.ToString());
            //                    dr["TableType"] = Convert.ToInt32(gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value.ToString());
            //                    dt.Rows.Add(dr);
            //                }

            //            }
            //            else
            //            {
            //                for (int k = 0; k < gvSelectedProduct.Rows.Count; k++)
            //                {
            //                    if ((Convert.ToInt32(gvSelectedProduct.Rows[k].Cells[0].Value.ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Tag.ToString())) && (Convert.ToInt32(gvSelectedProduct.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Name.ToString())))
            //                    {
            //                        ed.WriteMessage("Name To Delete" + gvSelectedProduct.Rows[k].Cells[1].Value.ToString() + "\n");
            //                        gvSelectedProduct.Rows.RemoveAt(k);
            //                        ed.WriteMessage("Dr.Count= " + dt.Rows.Count.ToString() + "\n");
            //                        DataRow[] drs = dt.Select(" ProductCode=" + tvProduct.Nodes[i].Nodes[j].Tag.ToString() + " and TableType=" + tvProduct.Nodes[i].Nodes[j].Name.ToString());
            //                        drs[0].Delete();
            //                        ed.WriteMessage("dr.CountAfterDelete= " + dt.Rows.Count.ToString() + "\n");
            //                        //foreach (DataRow dr in dt.Rows)
            //                        //{
            //                        //    if ((Convert.ToInt32(dr["productCode"].ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Tag.ToString())) && (Convert.ToInt32(dr["TableType"].ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Name.ToString())))
            //                        //    {
            //                        //        ed.WriteMessage("DeleteRowInDataTable\n");
            //                        //        dr.inde);
            //                        //        ed.WriteMessage("Deleted RowInDataTable\n");
            //                        //    }
            //                        //}

            //                    }
            //                }

            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("لطفا شماره سلول را مشخص نمایید", "خطا");
            //}


        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void cboNumCount_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ClearCheckAndGrid(tvProduct, gvSelectedProduct);
        //   foreach(TreeNode rootNode in tvProduct.Nodes)
        //       foreach (TreeNode childNode in rootNode.Nodes)
        //       {
        //           foreach (DataRow dr in dt.Rows)
        //           {
        //               //ed.WriteMessage(""+);
        //               if ((Convert.ToInt32(dr["productCode"]) == Convert.ToInt32(childNode.Tag.ToString())) && (Convert.ToInt32(dr["TableType"]) == Convert.ToInt32(childNode.Name.ToString())) && (Convert.ToInt32(dr["Cell"]) == Convert.ToInt32(cboNumCount.Text)))
        //               {
        //                   childNode.Checked = true;
        //                   gvSelectedProduct.Rows.Add();
        //                   gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value = childNode.Tag.ToString();
        //                   gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[1].Value = childNode.Text.ToString();
        //                   gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value = childNode.Name.ToString();
        //                   gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = dr["Count"].ToString();
        //               }
        //           }
        //       }
        //}

        private void cboNumCount_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("cboNumCount_MouseDown");
            //for (int i = 0; i < gvSelectedProduct.Rows.Count; i++)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        ed.WriteMessage("dr[productCode]:=" + dr["productCode"].ToString() + "  " + "Cells[0]:=" + gvSelectedProduct.Rows[i].Cells[0].Value.ToString()+"\n");
            //        ed.WriteMessage("dr[TableType]:=" + dr["TableType"].ToString()+"   "+"Cells[3]:="+gvSelectedProduct.Rows[i].Cells[3].Value.ToString()+"\n");
            //        ed.WriteMessage("dr[Cell]:=" + dr["Cell"].ToString() + "   " + "Cell:=" + cboNumCount.Text + "\n");
            //        if ((Convert.ToInt32(dr["productCode"])==Convert.ToInt32(gvSelectedProduct.Rows[i].Cells[0].Value.ToString())) && (Convert.ToInt32(dr["TableType"])==Convert.ToInt32(gvSelectedProduct.Rows[i].Cells[3].Value.ToString())) && (Convert.ToInt32(dr["Cell"])==Convert.ToInt32(cboNumCount.Text)))
            //        {
            //            ed.WriteMessage("In The IF");
            //            dr["Count"]=gvSelectedProduct.Rows[i].Cells[2].Value;
            //        }
            //    }
            //}

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
            }
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

            frmGroundPostSearch02 search = new frmGroundPostSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(search);
        }

        private void gvSelectedProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!Atend.Control.NumericValidation.Int32Converter((gvSelectedProduct.Rows[e.RowIndex].Cells[2].Value.ToString())))
            {
                MessageBox.Show("تعداد را با فرمت مناسب وارد کنید");
                return;
            }

            //double Cap = 0;//Convert.ToInt32(txtCapacity.Text);

            //for (int i = 0; i < gvSelectedProduct.Rows.Count; i++)
            //{

            //    if (Convert.ToInt32(gvSelectedProduct.Rows[i].Cells[4].Value.ToString()) == (int)Atend.Control.Enum.ProductType.Transformer)
            //    {
            //        Atend.Base.Equipment.ETransformer Trans = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(gvSelectedProduct.Rows[i].Cells[0].Value.ToString()));
            //        Cap = Cap + Convert.ToInt32(gvSelectedProduct.Rows[i].Cells[2].Value.ToString()) * Trans.Capaciy;
            //    }
            //}

            txtCapacity.Text = CalculateCapasity().ToString();// Cap.ToString();
        }

        private void gvSelectedProduct_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            // Boolean newRow = true;
            //if (Atend.Control.NumericValidation.DoubleConverter(gvSelectedProduct.Rows[e.RowIndex].Cells[2].Value.ToString()))
            //{
            // foreach (DataRow dr in dt.Rows)
            // {
            //     if ((Convert.ToInt32(dr["productCode"]) == Convert.ToInt32(gvSelectedProduct.Rows[e.RowIndex].Cells[0].Value.ToString())) && (Convert.ToInt32(dr["TableType"]) == Convert.ToInt32(gvSelectedProduct.Rows[e.RowIndex].Cells[3].Value.ToString())) && (Convert.ToInt32(dr["Cell"]) == Convert.ToInt32(cboNumCount.Text)))
            //     {
            //         ed.WriteMessage("In The IF EDIT \n");
            //         dr["Count"] = gvSelectedProduct.Rows[e.RowIndex].Cells[2].Value;
            //         newRow = false;
            //     }
            // }
            //if (newRow)
            //{
            //    ed.WriteMessage("IN THE IF NEW \n");
            //    DataRow dr = dt.NewRow();
            //    //dr["Name"] = tvProduct.Nodes[i].Nodes[j].Text.ToString();
            //    dr["productCode"] = Convert.ToInt32(gvSelectedProduct.Rows[e.RowIndex].Cells[0].Value.ToString());
            //    dr["Cell"] = Convert.ToInt32(cboNumCount.Text);
            //    dr["count"] = Convert.ToInt32(gvSelectedProduct.Rows[e.RowIndex].Cells[2].Value.ToString());
            //    dr["TableType"] = Convert.ToInt32(gvSelectedProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
            //    dt.Rows.Add(dr);
            //}

            //}
            //else
            //{
            //    MessageBox.Show("لطفا تعداد تجهیزات داخلی را با فرمت مناسب وارد نمایید","خطا");
            //    gvSelectedProduct.Focus();
            //    gvSelectedProduct.Rows[e.RowIndex].Cells[2].Value = 0;
            //}
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (CheckStatuseOfAccessChangeDefault())
                Delete();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
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

            if (selectedGroundPostXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost), selectedGroundPostXCode))
                {
                    Atend.Base.Equipment.EGroundPost GroundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(selectedGroundPostXCode);
                    Code = GroundPost.Code;
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

            //if (selectedGroundPostXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EGroundPost.ShareOnServer(selectedGroundPostXCode))
            //    {
            //        Atend.Base.Equipment.EGroundPost g1 = Atend.Base.Equipment.EGroundPost.SelectByXCode(selectedGroundPostXCode);
            //        Code = g1.Code;
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
            if (gvSelectedProduct.Rows.Count > 0)
            {
                foreach (TreeNode rootnode in tvProduct.Nodes)
                {
                    foreach (TreeNode childNode in rootnode.Nodes)
                    {
                        if (childNode.Tag.ToString() == gvSelectedProduct.Rows[gvSelectedProduct.CurrentRow.Index].Cells[0].Value.ToString())
                        {
                            childNode.Checked = false;
                        }
                    }
                }

                gvSelectedProduct.Rows.RemoveAt(gvSelectedProduct.CurrentRow.Index);
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

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
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
            double Cap = 0;//Convert.ToInt32(txtCapacity.Text);

            for (int i = 0; i < gvSelectedProduct.Rows.Count; i++)
            {

                if (Convert.ToInt32(gvSelectedProduct.Rows[i].Cells[4].Value.ToString()) == (int)Atend.Control.Enum.ProductType.Transformer)
                {
                    Atend.Base.Equipment.ETransformer Trans = Atend.Base.Equipment.ETransformer.SelectByXCode(new Guid(gvSelectedProduct.Rows[i].Cells[0].Value.ToString()));
                    Cap = Cap + Convert.ToInt32(gvSelectedProduct.Rows[i].Cells[2].Value.ToString()) * Trans.Capaciy;
                }
            }
            return Cap;
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

    }
}