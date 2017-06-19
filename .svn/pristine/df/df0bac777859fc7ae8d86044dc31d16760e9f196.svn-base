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

namespace Atend.Equipment
{
    public partial class frmJackPanelWeek03 : Form
    {
        public int ProductCode = -1;
        Guid selectedJackPanelWeekXCode = Guid.Empty;
        public bool IsDefault = false;
        int Code = -1;

        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("XCode");
        DataColumn dc1 = new DataColumn("TableType");
        DataColumn dc2 = new DataColumn("cell");
        DataColumn dc3 = new DataColumn("Count");
        DataColumn dc4 = new DataColumn("IsNightLight");
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        // dehqani
        ArrayList arItems = new ArrayList();



        DataTable dtCell = new DataTable();
        DataColumn dcCellNum = new DataColumn("CellNum");
        DataColumn dcIsNightLight = new DataColumn("IsNight");
        bool ForceToClose = false;

        public frmJackPanelWeek03()
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
            dc.DataType = System.Type.GetType("System.Guid");
            dt.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            dt.Columns.Add(dc4);
            Atend.Control.Common.selectedProductCode = -1;
            //ed.WriteMessage("New***********\n");



            //dcCellNum.DataType = System.Type.GetType("System.Int32");
            dtCell.Columns.Add(dcCellNum);
            //dcCellNum.DataType = System.Type.GetType("System.Boolean");
            dtCell.Columns.Add(dcIsNightLight);
        }

        private void Reset()
        {

            selectedJackPanelWeekXCode = Guid.Empty;
            txtName.Text = string.Empty;
            txtComment.Text = string.Empty;
            txtName.Focus();
            dt.Rows.Clear();
            IsDefault = false;
            tsbIsDefault.Checked = false;
            nudCountFeeder.Value = 1;
            chkIsLight.Checked = false;
            Atend.Control.Common.selectedProductCode = -1;
            txtbackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
            ClearCheckAndGrid(tvProduct, gvSelectedProduct);
            ClearCheckAndGrid(tvOperation, gvOperation);
            ProductCode = -1;
            Code = -1;
        }

        private void Save()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            txtName.Focus();
            btnInsert.Focus();
            ArrayList EPackageProduct = new ArrayList();
            ArrayList cellPackage = new ArrayList();
            Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = new Atend.Base.Equipment.EJackPanelWeek();
            jackPanelWeek.Name = txtName.Text;
            jackPanelWeek.AutoKey3pXCode = new Guid(cboAutoKey.SelectedValue.ToString());
            jackPanelWeek.Comment = txtComment.Text;
            jackPanelWeek.FeederCount = Convert.ToInt32(nudCountFeeder.Value);
            jackPanelWeek.ProductCode = Atend.Control.Common.selectedProductCode;
            jackPanelWeek.IsDefault = IsDefault;
            jackPanelWeek.Code = Code;
            jackPanelWeek.BusXCode = new Guid(cboBus.SelectedValue.ToString());
            //ed.WriteMessage("Begin \n");

            ////Operation
            ArrayList EOperation = new ArrayList();
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);

            }
            jackPanelWeek.OperationList = EOperation;
            ////******

            Atend.Base.Equipment.EJackPanelWeekCell _cell = new Atend.Base.Equipment.EJackPanelWeekCell();
            foreach (DataRow dr in dt.Rows)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                //_cell = new Atend.Base.Equipment.EJackPanelWeekCell();
                //ed.WriteMessage("Begin 1\n");
                // _EProductPackage.ProductCode = 0;// Convert.ToInt32(dr["productCode"]);
                _EProductPackage.XCode = new Guid(dr["XCode"].ToString());
                //ed.WriteMessage("Begin 2\n");

                _EProductPackage.Count = Convert.ToInt32(dr["Count"]);
                //ed.WriteMessage("Value:" + "\n");
                _EProductPackage.TableType = Convert.ToInt16(dr["TableType"]);
                //_cell.Num = Convert.ToInt32(dr["cell"]);
                //_cell.IsNightLight = Convert.ToBoolean(dr["IsNightLight"]);
                EPackageProduct.Add(_EProductPackage);
                //cellPackage.Add(_cell);
                //ed.WriteMessage("aaa \n");
            }

            //for (int i = 1; i <= jackPanelWeek.FeederCount; i++)
            //{
            //    _cell = new Atend.Base.Equipment.EJackPanelWeekCell();
            //    DataRow[] dr = dt.Select("Cell='" + i.ToString() + "'");
            //    _cell.Num = Convert.ToInt32(dr[0]["cell"]);
            //    _cell.IsNightLight = Convert.ToBoolean(dr[0]["IsNightLight"]);
            //    cellPackage.Add(_cell);
            //}

            ed.WriteMessage("*********dtcell.rows.count={0}", dtCell.Rows.Count);
            foreach (DataRow dr in dtCell.Rows)
            {
                _cell = new Atend.Base.Equipment.EJackPanelWeekCell();
                _cell.Num = Convert.ToInt32(dr["cellNum"]);
                _cell.IsNightLight = Convert.ToBoolean(dr["IsNight"]);
                cellPackage.Add(_cell);
            }

            jackPanelWeek.EquipmentList = EPackageProduct;
            jackPanelWeek.CellList = cellPackage;
            jackPanelWeek.dtglobal = dt;
            //ed.WriteMessage("selectedJackPanelweek" + selectedJackPanelWeek.ToString());
            if (selectedJackPanelWeekXCode == Guid.Empty)
            {
                if (jackPanelWeek.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");

            }
            else
            {
                jackPanelWeek.XCode = selectedJackPanelWeekXCode;
                //ed.WriteMessage("consol.code:" + selectedConsol.ToString());
                //MessageBox.Show("GoToUpdate");
                //ed.WriteMessage("Go To Update\n");
                if (jackPanelWeek.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");

            }
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(selectedJackPanelWeekXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(selectedJackPanelWeekXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (selectedJackPanelWeekXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EJackPanelWeek.DeleteX(selectedJackPanelWeekXCode))
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
            //ed.WriteMessage("Code= " + Code.ToString() + "\n");
            Atend.Base.Equipment.EJackPanelWeek jackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(XCode);
            Atend.Control.Common.selectedProductCode = Convert.ToInt32(jackPanelWeek.ProductCode);
            SelectProduct();

            selectedJackPanelWeekXCode = XCode;
            txtName.Text = jackPanelWeek.Name;
            ed.WriteMessage("AutoKey3pXCode={0}\n", jackPanelWeek.AutoKey3pXCode);
            cboAutoKey.SelectedValue = jackPanelWeek.AutoKey3pXCode;
            //ed.WriteMessage("2\n");
            txtComment.Text = jackPanelWeek.Comment;
            //ed.WriteMessage("3\n");
            dtCell.Rows.Clear();
            nudCountFeeder.Value = jackPanelWeek.FeederCount;
            //ed.WriteMessage("4\n");
            //ed.WriteMessage("5\n");
            tsbIsDefault.Checked = jackPanelWeek.IsDefault;
            Code = jackPanelWeek.Code;
            BindTreeViwAndGridEquipment();
            BindTreeViwAndGrid(tvOperation, gvOperation);
            //BindTreeViwAndGridContainer(tvContainer, gvSelectedContainer);
        }

        private void BindTreeViwAndGrid(TreeView treeView, DataGridView dataGridView)
        {
            ClearCheckAndGrid(tvOperation, gvOperation);
            ClearCheckAndGrid(tvOperation, gvOperation);
            dataGridView.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EJackPanelWeek.nodeKeys.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EJackPanelWeek.nodeKeys[i]);
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

        private void BindTreeViwAndGridEquipment()
        {
            dt.Rows.Clear();
            //ed.WriteMessage("CountOfRows In JackPanelWeekSubEquip= " + Atend.Base.Equipment.EJackPanelWeek.JackPanelWeekSubEquip.Rows.Count.ToString() + "\n");
            foreach (DataRow drJackPanel in Atend.Base.Equipment.EJackPanelWeek.JackPanelWeekSubEquip.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["XCode"] = new Guid(drJackPanel["XCode"].ToString());
                dr["Cell"] = Convert.ToInt32(drJackPanel["Cell"].ToString());
                dr["count"] = Convert.ToInt32(drJackPanel["count"].ToString());
                dr["TableType"] = Convert.ToInt32(drJackPanel["TableType"].ToString());
                dr["IsNightLight"] = Convert.ToBoolean(drJackPanel["IsNightLight"]);

                dt.Rows.Add(dr);

            }
            //foreach (DataRow d in dt.Rows)
            //{
            //    ed.WriteMessage("PCc= " + d["ProductCode"].ToString() + "\n");
            //    ed.WriteMessage("Celll= " + d["Cell"].ToString() + "\n");
            //    ed.WriteMessage("TableTypee= " + d["TableType"].ToString() + "\n");
            //    ed.WriteMessage("Countt= " + d["Count"].ToString() + "\n");
            //    ed.WriteMessage("IsNightLightt= " + d["IsNightLight"].ToString() + "\n");
            //    ed.WriteMessage("######\n");
            //}
            EventArgs e = new EventArgs();
            cboNumCount_SelectedIndexChanged(cboNumCount, e);



        }

        private void BindDataToAutoKey()
        {
            cboAutoKey.DisplayMember = "Name";
            cboAutoKey.ValueMember = "XCode";
            cboAutoKey.DataSource = Atend.Base.Equipment.EAutoKey_3p.SelectAllX();
        }

        private void BindDataToBus()
        {
            cboBus.DisplayMember = "Name";
            cboBus.ValueMember = "XCode";
           cboBus.DataSource = Atend.Base.Equipment.EBus.SelectAllX();
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

        //private void BindDataToTreeViewOperation()
        //{
        //    //BindData To tvOperation
        //    DataTable ProductType = Atend.Base.Base.BProduct.SelectByType(Convert.ToInt32(Atend.Control.Enum.ProductType.Operation));
        //    //int i = 0;
        //    foreach (DataRow dr in ProductType.Rows)
        //    {
        //        TreeNode node = new TreeNode();
        //        node.Text = dr["Name"].ToString();
        //        node.Tag = dr["ID"].ToString();
        //        tvOperation.Nodes.Add(node);
        //        //i++;
        //    }
        //}

        private void BindDataToTreeView()
        {


            Atend.Global.Utility.UBinding.BindDataToTreeViewX(tvProduct);
            //BindDataToTreeViewOperation();

        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (selectedJackPanelWeekXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EJackPanelWeek Equip = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(selectedJackPanelWeekXCode);
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
                txtName.Select(0, txtName.Text.Length);
                return false;
            }
            if (Atend.Base.Equipment.EJackPanelWeek.SearchByName(txtName.Text) == true && selectedJackPanelWeekXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboAutoKey.Text))
            {
                MessageBox.Show("لطفا کلید اتوماتیک را مشخص نمایید", "خطا");
                cboAutoKey.Focus();
                return false;
            }


            if (nudCountFeeder.Value == 0)
            {
                MessageBox.Show("لطفاً تعداد فیدر را مشخص نمایید", "خطا");
                nudCountFeeder.Focus();
                return false;
            }
            
            foreach (DataRow dr in dt.Rows)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                _EProductPackage.XCode = new Guid(dr["XCode"].ToString());
                _EProductPackage.Count = Convert.ToInt32(dr["Count"]);
                _EProductPackage.TableType = Convert.ToInt16(dr["TableType"]);

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(selectedJackPanelWeekXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedProduct.Focus();
                    return false;
                }
            }

            bool sw = true;
            //for (int i = 1; i <= Convert.ToInt32(nudCountFeeder.Value); i++)
            //{
            //    DataRow[] drs = dt.Select("Cell=" + i.ToString());
            //    if (drs.Length <= 0)
            //    {
            //        sw = false;
            //    }
            //}
            //if (sw == false)
            //{
            //    MessageBox.Show("لطفاً تجهیزات فیدر ها را مشخص نمایید", "خطا");
            //    return false;
            //}

            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void nudCountCell_ValueChanged(object sender, EventArgs e)
        {
            if (dtCell.Rows.Count == 0)
            {
                DataRow drNewCell = dtCell.NewRow();
                drNewCell["CellNum"] = 1;
                drNewCell["IsNight"] = true;
                dtCell.Rows.Add(drNewCell);

            }
            ed.WriteMessage("value={0}\n", nudCountFeeder.Value);
            for (int i = 0; i < Convert.ToInt32(nudCountFeeder.Value); i++)
            {
                DataRow[] drCell = dtCell.Select(string.Format("CellNum={0}", i + 1));
                if (drCell.Length == 0)
                {
                    DataRow drNewCell = dtCell.NewRow();
                    drNewCell["CellNum"] = i + 1;
                    drNewCell["IsNight"] = true;
                    dtCell.Rows.Add(drNewCell);
                    //ed.WriteMessage("AddRow={0}\n",i+1);
                }
            }

            cboNumCount.Items.Clear();
            //MessageBox.Show(nudCountCell.Value.ToString());
            for (int i = 1; i <= Convert.ToInt32(nudCountFeeder.Value); i++)
            {
                cboNumCount.Items.Add(i);

            }
            bool IsDel = true;
            int removeCell = 0;
            ed.WriteMessage("&&Dtcell={0}\n", dtCell.Rows.Count);
            for (int j = 0; j < dtCell.Rows.Count; j++)
            {
                IsDel = true;
                for (int i = 0; i < cboNumCount.Items.Count; i++)
                {
                    //ed.WriteMessage("##CellNum={0},J={1}\n",dtCell.Rows[j]["CellNum"].ToString(),j);
                    if (Convert.ToInt32(dtCell.Rows[j]["CellNum"].ToString()) == i + 1)
                    {
                        IsDel = false;
                        removeCell = j;
                    }
                }
                if (IsDel)
                {
                    ed.WriteMessage("DeleteCell\n");
                    dtCell.Rows[j].Delete();
                }
            }

            ed.WriteMessage("$$={0}\n", dtCell.Rows.Count);
            if (cboNumCount.Items.Count > 0)
                cboNumCount.SelectedIndex = 0;

            //for (int i = 0; i < nudCountFeeder.Value; i++)
            //{
            //    DataTable dtItems = new DataTable();

            //}
        }

        private void frmGroundPostcs_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ArrayList ar = new ArrayList();

            BindDataToTreeView();
            BindDataToAutoKey();
            BindDataToBus();
            for (int i = 0; i < cboAutoKey.Items.Count; i++)
            {
                cboAutoKey.SelectedIndex = 0;// i;
                //ed.WriteMessage("&&&&XCode={0}\n",cboAutoKey.SelectedValue.ToString());
            }
            if (cboBus.Items.Count>0)
            {
                cboBus.SelectedIndex = 0;
            }
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

        private void btnInsert_Click(object sender, EventArgs e)
        {

            Boolean canAdd = true;
            if (!string.IsNullOrEmpty(cboNumCount.Text))
            {
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
                                btnSelect.Focus();
                                gvSelectedProduct.Rows.Add();
                                //MessageBox.Show(tvProduct.Nodes[i].Nodes[j].Tag.ToString());
                                gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value = tvProduct.Nodes[i].Nodes[j].Tag.ToString();//Code
                                gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[1].Value = tvProduct.Nodes[i].Nodes[j].Text.ToString();//Text
                                //MessageBox.Show(tvProduct.Nodes[i].Nodes[j].Name.ToString());
                                gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value = tvProduct.Nodes[i].Nodes[j].Name.ToString();//TableType
                                gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = 1;//Count
                                //ed.WriteMessage("after assign to grid\n");
                                DataRow dr = dt.NewRow();

                                //ed.WriteMessage("ProductCode= " + gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value.ToString() + "\n");
                                dr["XCode"] = new Guid(gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value.ToString());
                                //ed.WriteMessage("cboNumCount.Text= " + cboNumCount.Text + "\n");
                                dr["Cell"] = Convert.ToInt32(cboNumCount.Text);
                                //ed.WriteMessage("Count =" + gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value.ToString() + "\n");
                                dr["count"] = Convert.ToInt32(gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value.ToString());
                                //ed.WriteMessage("TableType= " + gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value.ToString() + "\n");
                                dr["TableType"] = Convert.ToInt32(gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value.ToString());
                                //ed.WriteMessage("IsLightNight\n");
                                if (chkIsLight.Checked)
                                {

                                    dr["IsNightLight"] = true;
                                }
                                else
                                {
                                    dr["IsNightLight"] = false;
                                }
                                //ed.WriteMessage("After Assign to Night Light\n");
                                dt.Rows.Add(dr);
                                //ed.WriteMessage("After add to dt\n");
                            }

                        }
                        else
                        {
                            for (int k = 0; k < gvSelectedProduct.Rows.Count; k++)
                            {
                                //Guid gvXCode = new Guid((gvSelectedProduct.Rows[k].Cells[0].Value.ToString()));
                                //Guid tvXCode = new Guid(tvProduct.Nodes[i].Nodes[j].Tag.ToString());

                                if ((gvSelectedProduct.Rows[k].Cells[0].Value.ToString() == tvProduct.Nodes[i].Nodes[j].Tag.ToString()) && (Convert.ToInt32(gvSelectedProduct.Rows[k].Cells[3].Value.ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Name.ToString())))
                                {
                                    //ed.WriteMessage("Name To Delete" + gvSelectedProduct.Rows[k].Cells[1].Value.ToString() + "\n");
                                    gvSelectedProduct.Rows.RemoveAt(k);
                                    //ed.WriteMessage("Dr.Count= " + dt.Rows.Count.ToString() + "\n");
                                    DataRow[] drs = dt.Select(" XCode= \'" + tvProduct.Nodes[i].Nodes[j].Tag.ToString() + "\' and TableType=" + tvProduct.Nodes[i].Nodes[j].Name.ToString());
                                    drs[0].Delete();
                                    //ed.WriteMessage("dr.CountAfterDelete= " + dt.Rows.Count.ToString() + "\n");
                                    //foreach (DataRow dr in dt.Rows)
                                    //{
                                    //    if ((Convert.ToInt32(dr["productCode"].ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Tag.ToString())) && (Convert.ToInt32(dr["TableType"].ToString()) == Convert.ToInt32(tvProduct.Nodes[i].Nodes[j].Name.ToString())))
                                    //    {
                                    //        ed.WriteMessage("DeleteRowInDataTable\n");
                                    //        dr.inde);
                                    //        ed.WriteMessage("Deleted RowInDataTable\n");
                                    //    }
                                    //}

                                }
                            }

                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("لطفا شماره فیدر را مشخص نمایید", "خطا");
            }


        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    ed.WriteMessage("XCode: {0} tableTYpe : {1} call : {2} count: {3} IsNightLight : {4}\n",
            //    dr["XCODE"], dr["TableType"], dr["Cell"], dr["COUNT"], dr["IsNightLight"]);
            //}



        }

        private void cboNumCount_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void cboNumCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            //chkIsLight.Checked = false;
            Boolean chdefault = false;
            foreach (DataRow d in dt.Rows)
            {
                if (Convert.ToInt32(d["Cell"]) == Convert.ToInt32(cboNumCount.Text))
                {
                    chdefault = true;
                }
            }
            if (!chdefault)
            {
                chkIsLight.Checked = false;
            }
            foreach (DataRow d in dt.Rows)
            {
                //ed.WriteMessage("Cell= " + d["Cell"].ToString() + "\n");
                //ed.WriteMessage("IsNightLight= " + d["IsNightLight"].ToString() + "\n");
                //ed.WriteMessage("*************\n");
            }
            ClearCheckAndGrid(tvProduct, gvSelectedProduct);
            foreach (TreeNode rootNode in tvProduct.Nodes)
                foreach (TreeNode childNode in rootNode.Nodes)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //ed.WriteMessage(""+);

                        //Guid XCode1 = new Guid(dr["productCode"].ToString());
                        //Guid XCode2 = new Guid(childNode.Tag.ToString());

                        if ((dr["XCode"].ToString() == childNode.Tag.ToString()) && (Convert.ToInt32(dr["TableType"]) == Convert.ToInt32(childNode.Name.ToString())) && (Convert.ToInt32(dr["Cell"]) == Convert.ToInt32(cboNumCount.Text)))
                        {
                            rootNode.BackColor = Color.FromArgb(203, 214, 235);
                            childNode.Checked = true;
                            gvSelectedProduct.Rows.Add();
                            gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[0].Value = childNode.Tag.ToString();
                            gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[1].Value = childNode.Text.ToString();
                            gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[3].Value = childNode.Name.ToString();
                            gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = dr["Count"].ToString();
                            //ed.WriteMessage("IsNightLighh= " + dr["IsNightLight"].ToString() + "Cell= " + dr["Cell"].ToString() + "\n");
                            chkIsLight.Checked = Convert.ToBoolean(dr["IsNightLight"]);

                        }

                    }
                }
            //EventArgs e1=new EventArgs();
            //chkIsLight_CheckedChanged(chkIsLight,e1);

        }

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
            frmJackPanelWeekSearch03 search02 = new frmJackPanelWeekSearch03(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(search02);
        }

        private void gvSelectedProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvSelectedProduct_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            Boolean newRow = true;
            //ed.WriteMessage("I Am In Th CellEndEdit\n");
            if (Atend.Control.NumericValidation.DoubleConverter(gvSelectedProduct.Rows[e.RowIndex].Cells[2].Value.ToString()))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Guid XCode1 = new Guid(dr["XCode"].ToString());
                    Guid XCode2 = new Guid(gvSelectedProduct.Rows[e.RowIndex].Cells[0].Value.ToString());

                    if ((XCode1 == XCode2) && (Convert.ToInt32(dr["TableType"]) == Convert.ToInt32(gvSelectedProduct.Rows[e.RowIndex].Cells[3].Value.ToString())) && (Convert.ToInt32(dr["Cell"]) == Convert.ToInt32(cboNumCount.Text)))
                    {
                        //ed.WriteMessage("In The IF EDIT \n");
                        dr["Count"] = gvSelectedProduct.Rows[e.RowIndex].Cells[2].Value;
                        newRow = false;
                    }
                }
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

            }
            else
            {
                MessageBox.Show("لطفا تعداد تجهیزات داخلی را با فرمت مناسب وارد نمایید", "خطا");
                gvSelectedProduct.Focus();
                gvSelectedProduct.Rows[e.RowIndex].Cells[2].Value = 0;
            }
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

        //private void btnInsertOperation_Click(object sender, EventArgs e)
        //{
        //    gvOperation.Rows.Clear();
        //    for (int i = 0; i < tvOperation.Nodes.Count; i++)
        //    {

        //        if (tvOperation.Nodes[i].Checked)
        //        {
        //            gvOperation.Rows.Add();
        //            gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[0].Value = tvOperation.Nodes[i].Tag.ToString();
        //            gvOperation.Rows[gvOperation.Rows.Count - 1].Cells[1].Value = tvOperation.Nodes[i].Text.ToString();

        //        }
        //    }
        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chkIsLight_CheckedChanged(object sender, EventArgs e)
        {
            //ed.WriteMessage("Enter Changing\n");
            foreach (DataRow dr in dt.Rows)
            {
                //ed.WriteMessage("Cell= " + dr["Cell"].ToString() + "Text= " + cboNumCount.Text + "\n");
                if (Convert.ToInt32(dr["Cell"]) == Convert.ToInt32(cboNumCount.Text))
                {
                    //ed.WriteMessage("CHK Changeing =" + chkIsLight.Checked.ToString() + dr["Cell"].ToString() + "\n");
                    if (chkIsLight.Checked)
                    {
                        //ed.WriteMessage("If1\n");
                        dr["IsNightLight"] = true;
                    }
                    else
                    {
                        //ed.WriteMessage("IF2\n");

                        dr["IsNightLight"] = false;

                    }
                }
            }

            foreach (DataRow dr in dtCell.Rows)
            {
                //ed.WriteMessage("Cell= " + dr["Cell"].ToString() + "Text= " + cboNumCount.Text + "\n");
                if (Convert.ToInt32(dr["CellNum"]) == Convert.ToInt32(cboNumCount.Text))
                {
                    //ed.WriteMessage("CHK Changeing =" + chkIsLight.Checked.ToString() + dr["Cell"].ToString() + "\n");
                    if (chkIsLight.Checked)
                    {
                        //ed.WriteMessage("If1\n");
                        dr["IsNight"] = true;
                    }
                    else
                    {
                        //ed.WriteMessage("IF2\n");

                        dr["IsNight"] = false;

                    }
                }
            }
        }

        private void SelectProduct()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(Atend.Control.Common.selectedProductCode);
            txtName.Text = product.Name;
            txtCode.Text = product.Code.ToString();
            txtbackUpName.Text = product.Name;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.WeekJackPanel);

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
            }
        }

        private void btnSelect_Click_1(object sender, EventArgs e)
        {
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.WeekJackPanel);

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
            }
        }

        private void tvProduct_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show("Tag= "+e.Node.Tag.ToString());
            //MessageBox.Show("Name= " + e.Node.Name.ToString());

            //for (int i = 0; i < e.Node.Nodes.Count; i++)
            //{
            //    if (e.Node.Nodes[i].Checked)
            //    {
            //        ed.WriteMessage("Code= " + e.Node.Nodes[i].Tag.ToString() + "\n");
            //        ed.WriteMessage("TableType =" + e.Node.Nodes[i].Name.ToString() + "\n");
            //    }
            //}
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

            if (selectedJackPanelWeekXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel), selectedJackPanelWeekXCode))
                {
                    Atend.Base.Equipment.EJackPanelWeek JackPanelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(selectedJackPanelWeekXCode);
                    Code = JackPanelWeek.Code;
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

            //if (selectedJackPanelWeekXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EJackPanelWeek.ShareOnServer(selectedJackPanelWeekXCode))
            //    {
            //        Atend.Base.Equipment.EJackPanelWeek j1 = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(selectedJackPanelWeekXCode);
            //        Code = j1.Code;
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

        private void button2_Click(object sender, EventArgs e)
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

        private void btnDeleEquip_Click(object sender, EventArgs e)
        {
            if (gvSelectedProduct.SelectedRows.Count > 0)
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


    }
}