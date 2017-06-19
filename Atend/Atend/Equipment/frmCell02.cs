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
    public partial class frmCell02 : Form
    {
        Guid SelectedCellCode = Guid.Empty;
        public bool IsDefault = false;
        DataColumn dc1 = new DataColumn("CodeT", System.Type.GetType("System.Byte"));
        DataColumn dc2 = new DataColumn("Name");
        DataTable CellTypeTable = new DataTable();
        int Code = -1;
        bool ForceToClose = false;

        public frmCell02()
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
            CellTypeTable.Columns.Add(dc1);
            CellTypeTable.Columns.Add(dc2);
        }

        void BindDatatoCboCellType()
        {
            CellTypeTable.Rows.Clear();

            DataRow dr = CellTypeTable.NewRow();
            dr["CodeT"] = 1;
            dr["Name"] = "اندازه گیری";
            CellTypeTable.Rows.Add(dr);

            dr = CellTypeTable.NewRow();
            dr["CodeT"] = 2;
            dr["Name"] = "رله";
            CellTypeTable.Rows.Add(dr);

            dr = CellTypeTable.NewRow();
            dr["CodeT"] = 3;
            dr["Name"] = "کلید سکسیونر";
            CellTypeTable.Rows.Add(dr);

            dr = CellTypeTable.NewRow();
            dr["CodeT"] = 4;
            dr["Name"] = "کلید دژنکتور";
            CellTypeTable.Rows.Add(dr);

            dr = CellTypeTable.NewRow();
            dr["CodeT"] = 5;
            dr["Name"] = "BusCoupler سکسیونر";
            CellTypeTable.Rows.Add(dr);

            dr = CellTypeTable.NewRow();
            dr["CodeT"] = 6;
            dr["Name"] = "BusCoupler دژنکتور";
            CellTypeTable.Rows.Add(dr);

            dr = CellTypeTable.NewRow();
            dr["CodeT"] = 7;
            dr["Name"] = "فوزیبل";
            CellTypeTable.Rows.Add(dr);


            cboKind.DisplayMember = "Name";
            cboKind.ValueMember = "CodeT";
            cboKind.DataSource = CellTypeTable;


        }

        private void frmCell_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //cboKind.SelectedIndex = 0;
            BindDataToTreeView();
            BindDatatoCboCellType();
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

        private void BindDataToTreeView()
        {
            tvEquipment.Nodes.Clear();
            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvEquipment);

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

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

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txtName.Focus();
            SelectedCellCode = Guid.Empty;
            txtName.Text = string.Empty;
            cboKind.SelectedIndex = 0;
            IsDefault = false;
            tsbIsDefault.Checked = false;
            Code = -1;
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            ClearCheckAndGrid(tvOperation, gvOperation);
        }

        private void ClearCheckAndGrid(TreeView treeView, DataGridView dataGridView)
        {
            foreach (TreeNode rootNode in treeView.Nodes)
            {
                rootNode.BackColor = Color.White;
                rootNode.Checked = false;
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
                if (SelectedCellCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.ECell Equip = Atend.Base.Equipment.ECell.SelectByXCode(SelectedCellCode);
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
                MessageBox.Show("لطفاً نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cboKind.Text))
            {
                MessageBox.Show("لطفاً نوع سلول را مشخص نمایید", "خطا");
                cboKind.Focus();
                return false;
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

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(SelectedCellCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Cell), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }
            }
            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void Save()
        {
            txtName.Focus();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ArrayList EPackageProduct = new ArrayList();
            ArrayList equipmentlist = new ArrayList();
            Atend.Base.Equipment.ECell cell = new Atend.Base.Equipment.ECell();


            for (int j = 0; j < gvSelectedEquipment.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage productPackage = new Atend.Base.Equipment.EProductPackage();
                productPackage.Count = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                //productPackage.ProductCode = 0;// Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                productPackage.XCode = new Guid(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                productPackage.TableType = Convert.ToByte(gvSelectedEquipment.Rows[j].Cells[3].Value.ToString());

                //cellsub.ProductCode = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                //cellsub.Count = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                ////cellsub.CellCode = cell.Code;
                /*MessageBox.Show("0>>" + gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                MessageBox.Show("1>>" + gvSelectedEquipment.Rows[j].Cells[1].Value.ToString());
                MessageBox.Show("2>>" + gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                MessageBox.Show("3>>" + gvSelectedEquipment.Rows[j].Cells[3].Value.ToString());
                */
                equipmentlist.Add(productPackage);

            }
            cell.EquipmentList = equipmentlist;

            cell.Type = Convert.ToByte(cboKind.SelectedValue);
            cell.Name = txtName.Text;
            cell.IsDefault = IsDefault;
            cell.Code = Code;

            ArrayList EOperation = new ArrayList();
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);
            }
            cell.OperationList = EOperation;
            if (SelectedCellCode == Guid.Empty)
            {
                if (cell.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت کردن اطلاعات نمی باشد", "خطا");
            }
            else
            {
                cell.XCode = SelectedCellCode;
                if (cell.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");

            }




        }

        private void Delete()
        {
            txtName.Focus();
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectedCellCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectedCellCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا مایل به حذف کردن اطلاعات می باشید؟", "خطا", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedCellCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.ECell.DeleteX(SelectedCellCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "حذف");
            }
        }

        public void BindDataToOwnControl(Guid XCode)
        {
            SelectedCellCode = XCode;
            Atend.Base.Equipment.ECell cell = Atend.Base.Equipment.ECell.SelectByXCode(XCode);
            txtName.Text = cell.Name;
            cboKind.SelectedValue = cell.Type;
            tsbIsDefault.Checked = cell.IsDefault;
            Code = cell.Code;
            BindTreeViwAndGridEquipment();
        }

        private void BindTreeViwAndGridEquipment()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            gvSelectedEquipment.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.ECell.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.ECell.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {

                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {                                   //nodeTypeEPackage[i]
                        if (Atend.Base.Equipment.ECell.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            if (chileNode.Tag.ToString() == s)
                            {
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquipment.Rows.Add();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.ECell.nodeCountEPackageX[i].ToString();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[3].Value = chileNode.Name;

                            }
                        }
                    }
                }
            }
            //Operation
            ClearCheckAndGrid(tvOperation, gvOperation);
            ClearCheckAndGrid(tvOperation, gvOperation);
            gvOperation.Refresh();
            //MessageBox.Show(Atend.Base.Equipment.ECell.nodeKeys.Count.ToString());
            for (int i = 0; i < Atend.Base.Equipment.ECell.nodeKeys.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.ECell.nodeKeys[i]);
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

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            frmCellSearch02 frmcellsearch02 = new frmCellSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmcellsearch02);
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void پروندهToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckStatuseOfAccessChangeDefault())
                Delete();
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


            if (SelectedCellCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.Cell), SelectedCellCode))
                {
                    Atend.Base.Equipment.ECell Cell = Atend.Base.Equipment.ECell.SelectByXCode(SelectedCellCode);
                    Code = Cell.Code;
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

            //if (SelectedCellCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.ECell.ShareOnServer(SelectedCellCode))
            //    {
            //        Atend.Base.Equipment.ECell c1 = Atend.Base.Equipment.ECell.SelectByXCode(SelectedCellCode);
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

    }
}