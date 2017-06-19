using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Equipment
{
    public partial class frmDB03 : Form
    {
        //public frmDB03()
        //{
        //    InitializeComponent();
        //}
        int Code = -1;
        bool ForceToClose = false;

        public frmDB03()
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
            dtInputDB = Atend.Base.Equipment.EDBPhuse.SelectByDBXCode(Guid.Empty);//ورودی
            dtOutputDB = Atend.Base.Equipment.EDBPhuse.SelectByDBXCode(Guid.Empty);//خروجی

            //dtDB1 = Atend.Base.Equipment.EDBPhuse.SelectByDBCode(-1);//تک فاز
            //ed.WriteMessage("Data table row; " + dtDB.Rows.Count.ToString() + "\n");
            Atend.Control.Common.selectedProductCode = -1;
            //dtSemshNum.Columns.Add(dc);

            //for (int i = 1; i <= 3; i++)
            //{
            //    DataRow dr = dtSemshNum.NewRow();
            //    //dr["Name"] = tvProduct.Nodes[i].Nodes[j].Text.ToString();
            //    dr["Num"] = i.ToString();
            //    dtSemshNum.Rows.Add(dr);
            //}
        }

        int Shemsh;
        public int ProductCode = -1;
        Guid selectedDBCode = Guid.Empty;
        DataTable dtInputDB = new DataTable();
        DataTable dtOutputDB = new DataTable();

        DataTable dtInputFeeder = new DataTable();
        DataTable dtOutputFeeder = new DataTable();

        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        decimal InputFeederNum = 0;
        decimal OutputFeederNum = 0;
        //DataTable dtSemshNum = new DataTable();
        DataColumn dc = new DataColumn("ShemshNum");
        public bool IsDefault = false;

        private void Reset()
        {
            selectedDBCode = Guid.Empty;
            int Counter = Convert.ToInt32(nudOutputCount.Value);
            IsDefault = false;
            tsbIsDefault.Checked = false;

            for (int i = 0; i < Counter; i++)
            {
                nudOutputCount.Value--;
            }
            dtInputDB = Atend.Base.Equipment.EDBPhuse.SelectByDBXCode(Guid.Empty);//سه فاز
            //ed.WriteMessage("Reset dtDBRow: " + dtDB.Rows.Count.ToString() + "\n");
            gvInputFeeder.DataSource = dtInputDB;

            int counter1 = Convert.ToInt32(nudInputCount.Value);
            for (int i = 0; i < counter1; i++)
            {
                nudInputCount.Value--;
            }
            dtOutputDB = Atend.Base.Equipment.EDBPhuse.SelectByDBXCode(Guid.Empty);//تک فاز
            //ed.WriteMessage("Reset dtDBRow: " + dtDB.Rows.Count.ToString() + "\n");
            //gvFeeder1.DataSource = dtDB1;

            txtComment.Text = string.Empty;
            txtName.Text = string.Empty;
            Atend.Control.Common.selectedProductCode = -1;
            txtBackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
            ClearCheckAndGrid(tvOperation, gvOperation);
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            ProductCode = -1;
            Code = -1;
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

        private void Save()
        {
            txtName.Focus();
            Atend.Base.Equipment.EDB DB = new Atend.Base.Equipment.EDB();
            ArrayList EOperation = new ArrayList();
            //DB.InputPhuse = -1;//Convert.ToInt32(cboInputPhuse.SelectedValue);
            DB.InputCount = Convert.ToInt32(nudInputCount.Value);
            DB.OutputCount = Convert.ToInt32(nudOutputCount.Value);
            DB.ProductCode = Atend.Control.Common.selectedProductCode;
            DB.Comment = txtComment.Text;
            DB.Name = txtName.Text;
            //DB.ShemshCount = -1;// Convert.ToInt32(nudInputCount.Value);
            DB.IsDefault = IsDefault;
            DB.Code = Code;
            txtComment.Focus();
            int i;
            //Atend.Base.Equipment.EDBPhuse DBPhuse = new Atend.Base.Equipment.EDBPhuse();
            //سه فاز

            if (!Atend.Base.Equipment.EDBPhuse.DeleteX(selectedDBCode))
            {
                MessageBox.Show("ثبت کردن اطلاعات امکانپذیر نمیباشد", "خطا");
                return;
            }
            dtInputFeeder = (DataTable)gvInputFeeder.DataSource;
            dtOutputFeeder = (DataTable)gvOutputFeeder.DataSource;
            foreach (DataRow dr in dtInputFeeder.Rows)
            {
                Atend.Base.Equipment.EDBPhuse DBPhuse = new Atend.Base.Equipment.EDBPhuse();
                DBPhuse.FeederNum = Convert.ToInt32(dr["FeederNum"].ToString());
                DBPhuse.ShemshNum = 0;
                DBPhuse.PhuseXCode = new Guid(dr["PhuseXCode"].ToString());
                Atend.Base.Equipment.EPhuse phuse = Atend.Base.Equipment.EPhuse.SelectByXCode(DBPhuse.PhuseXCode);
                DBPhuse.PhuseType = 1;
                DBPhuse.IOType = true;
                DB.SubEquipment.Add(DBPhuse);
            }
            foreach (DataRow dr in dtOutputFeeder.Rows)
            {
                Atend.Base.Equipment.EDBPhuse DBPhuse = new Atend.Base.Equipment.EDBPhuse();
                DBPhuse.FeederNum = Convert.ToInt32(dr["FeederNum"].ToString());
                DBPhuse.ShemshNum = 0;
                DBPhuse.PhuseXCode = new Guid(dr["PhuseXCode"].ToString());
                Atend.Base.Equipment.EPhuse phuse = Atend.Base.Equipment.EPhuse.SelectByXCode(DBPhuse.PhuseXCode);
                DBPhuse.PhuseType = 0;
                DBPhuse.IOType = false;
                DB.SubEquipment.Add(DBPhuse);
            }
            ////for (i = 0; i < gvInputFeeder.Rows.Count; i++)
            ////{
            ////    Atend.Base.Equipment.EDBPhuse DBPhuse = new Atend.Base.Equipment.EDBPhuse();
            ////    DBPhuse.FeederNum = Convert.ToInt32(gvInputFeeder.Rows[i].Cells[0].Value.ToString());
            ////    DBPhuse.ShemshNum = 0;// Convert.ToByte(gvInputFeeder.Rows[i].Cells[1].Value.ToString());
            ////    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvInputFeeder.Rows[i].Cells[2];
            ////    DBPhuse.PhuseXCode = new Guid(c.Value.ToString());
            ////    Atend.Base.Equipment.EPhuse phuse = Atend.Base.Equipment.EPhuse.SelectByXCode(DBPhuse.PhuseXCode);
            ////    DBPhuse.PhuseType = 1;
            ////    DBPhuse.IOType = true;
            ////    ed.WriteMessage("@@ feedernum:{0},phusexcode:{1}\n", DBPhuse.FeederNum, DBPhuse.PhuseXCode);
            ////    DB.SubEquipment.Add(DBPhuse);
            ////}

            ////for (i = 0; i < gvOutputFeeder.Rows.Count; i++)
            ////{
            ////    Atend.Base.Equipment.EDBPhuse DBPhuse = new Atend.Base.Equipment.EDBPhuse();
            ////    DBPhuse.FeederNum = Convert.ToInt32(gvOutputFeeder.Rows[i].Cells[0].Value.ToString());
            ////    DBPhuse.ShemshNum = 0;// Convert.ToByte(gvInputFeeder.Rows[i].Cells[1].Value.ToString());
            ////    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvOutputFeeder.Rows[i].Cells[2];
            ////    DBPhuse.PhuseXCode = new Guid(c.Value.ToString());
            ////    Atend.Base.Equipment.EPhuse phuse = Atend.Base.Equipment.EPhuse.SelectByXCode(DBPhuse.PhuseXCode);
            ////    DBPhuse.PhuseType = 0;
            ////    DBPhuse.IOType = false;
            ////    DB.SubEquipment.Add(DBPhuse);
            ////}

            // تک فاز
            //for (int i = 0; i < gvFeeder1.Rows.Count; i++)
            //{
            //    DBPhuse.PhuseType = 1;
            //    DBPhuse.FeederNum = Convert.ToInt32(gvFeeder1.Rows[i].Cells[0].Value.ToString());
            //    DataGridViewComboBoxCell cShemsh = (DataGridViewComboBoxCell)gvFeeder1.Rows[i].Cells[1];
            //    DBPhuse.ShemshNum = Convert.ToByte(cShemsh.Value);
            //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvFeeder1.Rows[i].Cells[1];
            //    DBPhuse.PhuseCode = Convert.ToInt32(c.Value);
            //    DB.SubEquipment.Add(DBPhuse);
            //    //ed.WriteMessage("aaa \n");
            //}

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
            DB.EquipmentList = EPackageProduct;

            //Operation
            for (i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);
            }

            DB.OperationList = EOperation;

            //**********
            //ed.WriteMessage("selectedDBCode" + selectedDBCode + "\n");
            if (selectedDBCode == Guid.Empty)
            {
                if (DB.InsertX())
                {
                    Reset();
                    //ed.WriteMessage("طلاعات به درستی ثبت شد\n");
                }
                else
                {
                    MessageBox.Show("امکان ثبت کردن اطلاعات نمی باشد", "خطا");
                }
            }
            else
            {
                DB.XCode = selectedDBCode;
                if (DB.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(selectedDBCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(selectedDBCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا مایل به حذف کردن اطلاعات می باشید؟", "خطا", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (selectedDBCode != Guid.Empty)
                {
                    //MessageBox.Show(selectedDBCode.ToString());
                    if (Atend.Base.Equipment.EDB.DeleteX(selectedDBCode))
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



            for (int i = dataGridView.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView.Rows.RemoveAt(i);
            }
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        private void BindTreeandGrid()
        {
            //EQUIPMENT
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            gvSelectedEquipment.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EDB.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EDB.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {
                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        if (Atend.Base.Equipment.EDB.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            if (chileNode.Tag.ToString() == s)
                            {
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquipment.Rows.Add();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EDB.nodeCountEPackageX[i].ToString();
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
            for (int i = 0; i < Atend.Base.Equipment.EDB.nodeKeysX.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EDB.nodeKeysX[i]);
                string s = Operation.ProductID.ToString();

                foreach (TreeNode rootnode in tvOperation.Nodes)
                {

                    //ed.WriteMessage("RootNode.Tag= " + rootnode.Tag.ToString() + "\n");
                    if (rootnode.Tag.ToString() == s)
                    {
                        ////ed.WriteMessage("I Am In IF" + "rootnode.Tag:= " + rootnode.Tag.ToString() + "S =" + s + "\n");
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

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (selectedDBCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EDB Equip = Atend.Base.Equipment.EDB.SelectByXCode(selectedDBCode);
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
            txtName.Focus();
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
            if (Atend.Base.Equipment.EDB.SearchByName(txtName.Text) == true && selectedDBCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است");
                return false;
            }
            if (nudOutputCount.Value == 0)
            {
                MessageBox.Show("لطفاً تعداد خروجی را مشخص نمایید", "خطا");
                nudOutputCount.Focus();
                return false;
            }

            if (nudInputCount.Value == 0)
            {
                MessageBox.Show("لطفاً تعداد ورودی را مشخص نمایید", "خطا");
                nudInputCount.Focus();
                return false;
            }

            int i;
            for (i = 0; i < gvInputFeeder.Rows.Count; i++)
            {
                //DataGridViewComboBoxCell c0 = (DataGridViewComboBoxCell)gvInputFeeder.Rows[i].Cells[1];
                DataGridViewComboBoxCell c1 = (DataGridViewComboBoxCell)gvInputFeeder.Rows[i].Cells[2];

                try
                {
                    //ed.WriteMessage("\nCombo Value = " + c1.Value.ToString() + "\n");
                    if (/*string.IsNullOrEmpty(c0.Value.ToString()) && */string.IsNullOrEmpty(c1.Value.ToString()))
                    {
                        MessageBox.Show(". لطفاً فیوز مربوط به فیدر را انتخاب کنید", "خطا در پر کردن فیدرهای ورودی ");
                        gvInputFeeder.Focus();
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show(". لطفاً فیوز مربوط به فیدر را انتخاب کنید", "خطا در پر کردن فیدرهای ورودی ");
                    gvInputFeeder.Focus();
                    return false;
                }
            }

            for (i = 0; i < gvOutputFeeder.Rows.Count; i++)
            {

                //DataGridViewComboBoxCell c0 = (DataGridViewComboBoxCell)gvInputFeeder.Rows[i].Cells[1];
                DataGridViewComboBoxCell c1 = (DataGridViewComboBoxCell)gvOutputFeeder.Rows[i].Cells[2];

                try
                {
                    if (/*string.IsNullOrEmpty(c0.Value.ToString()) && */string.IsNullOrEmpty(c1.Value.ToString()))
                    {
                        MessageBox.Show(". لطفاً فیوز مربوط به فیدر را انتخاب کنید", "خطا در پر کردن فیدرهای خروجی ");
                        gvOutputFeeder.Focus();
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show(". لطفاً فیوز مربوط به فیدر را انتخاب کنید", "خطا در پر کردن فیدرهای خروجی ");
                    gvOutputFeeder.Focus();
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

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(selectedDBCode, Convert.ToInt32(Atend.Control.Enum.ProductType.DB), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }
            }

            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        public void BindDataToOwnControl(Guid XCode)
        {
            //gvInputFeeder = new DataGridView();

            for (int i = 0; i < gvInputFeeder.Rows.Count; i++)
                gvInputFeeder.Rows.RemoveAt(i);

            selectedDBCode = XCode;
            //ed.WriteMessage("AS\n");
            Atend.Base.Equipment.EDB DB = Atend.Base.Equipment.EDB.SelectByXCode(XCode);

            //dtSemshNum.Clear();
            DataTable dtDBPhuseInput = Atend.Base.Equipment.EDBPhuse.SelectByDBXCodeType(DB.XCode, 1);
            dtInputDB.Rows.Clear();
            dtInputDB = dtDBPhuseInput.Copy();
            dtInputFeeder = dtDBPhuseInput.Copy();

            DataTable dtDBPhuseOutput = Atend.Base.Equipment.EDBPhuse.SelectByDBXCodeType(DB.XCode, 0);
            dtOutputDB.Rows.Clear();
            dtOutputDB = dtDBPhuseOutput.Copy();
            dtOutputFeeder = dtDBPhuseOutput.Copy();

            //MessageBox.Show(dtDB.Rows.Count.ToString());

            //for (int i = 0; i < DB.ShemshCount; i++)
            //{
            //    DataRow dr = dtSemshNum.NewRow();
            //    dr["ShemshNum"] = i + 1;
            //    dtSemshNum.Rows.Add(dr);
            //}


            //ed.WriteMessage("AD\n");

            Atend.Control.Common.selectedProductCode = DB.ProductCode;
            SelectProduct();
            txtName.Text = DB.Name;
            txtComment.Text = DB.Comment;
            Atend.Control.Common.selectedProductCode = DB.ProductCode;
            nudOutputCount.Value = Convert.ToDecimal(DB.OutputCount);
            nudInputCount.Value = Convert.ToDecimal(DB.InputCount);
            Code = DB.Code;
            //ed.WriteMessage("AF\n");

            //gvInputFeeder.Columns[1].DataPropertyName = "ShemshNum";


            InputFeederNum = Convert.ToDecimal(DB.InputCount);
            OutputFeederNum = Convert.ToDecimal(DB.OutputCount);

            gvInputFeeder.AutoGenerateColumns = false;
            gvInputFeeder.DataSource = dtDBPhuseInput;
            gvInputFeeder.Refresh();


            //MessageBox.Show(dtDB.Rows.Count.ToString());
            //MessageBox.Show(dtDBPhuse.Rows.Count.ToString());

            for (int i = 0; i < dtDBPhuseInput.Rows.Count; i++)
            {
                //MessageBox.Show(dtDBPhuse.Rows[i]["DBCode"].ToString() + "     " + i.ToString());

                if (gvInputFeeder.Rows[i].Cells[0].Value.ToString() == dtDBPhuseInput.Rows[i]["FeederNum"].ToString())
                    gvInputFeeder.Rows[i].Cells[2].Value = dtDBPhuseInput.Rows[i]["PhuseXCode"].ToString();

                //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvInputFeeder.Rows[i].Cells[1];
                //c.Value = dtDBPhuse.Rows[i]["ShemshNum"].ToString();
                //MessageBox.Show("END   " + i.ToString());
            }


            gvOutputFeeder.AutoGenerateColumns = false;
            gvOutputFeeder.DataSource = dtDBPhuseOutput;
            gvOutputFeeder.Refresh();
            tsbIsDefault.Checked = DB.IsDefault;


            //int counter = 1;
            //foreach (DataRow DrDBPhuse in dtDBPhuse.Rows)
            //{
            //    gvInputFeeder.Rows.Add();
            //    gvInputFeeder.Rows[gvInputFeeder.Rows.Count - 1].Cells[0].Value = Counter;
            //    gvInputFeeder.Rows[gvInputFeeder.Rows.Count - 1].Cells[1].Value = DrDBPhuse["ShemshNum"].ToString();

            //    Atend.Base.Equipment.EPhuse Phuse = Atend.Base.Equipment.EPhuse.SelectByCode(Convert.ToInt32(DrDBPhuse["PhuseCode"].ToString()));

            //    DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)gvInputFeeder.Rows[gvInputFeeder.Rows.Count - 1].Cells[2];
            //    c.Value = Phuse.Code;

            //    //gvInputFeeder.Rows[gvInputFeeder.Rows.Count - 1].Cells[2].Value = 1;
            //    Counter++;
            //}


            //gvInputFeeder.DataSource = dtDB;
            //ed.WriteMessage("AG\n");

            BindTreeandGrid();
            dtInputDB.Clear();
            dtInputDB.Rows.Clear();
            dtInputDB = dtDBPhuseInput.Copy();

            dtOutputDB.Clear();
            dtOutputDB.Rows.Clear();
            dtOutputDB = dtDBPhuseOutput.Copy();


        }

        private void BindPhuseToComboBox(DataGridViewComboBoxCell comboBox)
        {

            comboBox.DisplayMember = "FullName";
            comboBox.ValueMember = "Code";
            comboBox.DataSource = Atend.Base.Equipment.EPhuse.SelectAll();

        }

        public void BindInputPhuseToComboBox()
        {

            //cboInputPhuse.DisplayMember = "Name";
            //cboInputPhuse.ValueMember = "Code";
            //cboInputPhuse.DataSource = Atend.Base.Equipment.EPhuse.SelectAll();

        }

        private void frmDB_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            BindInputPhuseToComboBox();

            DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvInputFeeder.Columns["Column2"];

            c.DataSource = Atend.Base.Equipment.EPhuse.SelectAllX();
            c.DisplayMember = "Name";
            c.ValueMember = "XCode";

            DataGridViewComboBoxColumn c0 = (DataGridViewComboBoxColumn)gvOutputFeeder.Columns[2];

            c0.DataSource = Atend.Base.Equipment.EPhuse.SelectAllX();
            c0.DisplayMember = "Name";
            c0.ValueMember = "XCode";

            //DataGridViewComboBoxColumn c1 = (DataGridViewComboBoxColumn)gvInputFeeder.Columns["Column5"];

            //c1.DataSource = dtSemshNum;
            //c1.DisplayMember = "ShemshNum";
            //c1.ValueMember = "ShemshNum";

            gvInputFeeder.AutoGenerateColumns = false;
            gvInputFeeder.DataSource = dtInputDB;


            gvOutputFeeder.AutoGenerateColumns = false;
            gvOutputFeeder.DataSource = dtOutputDB;



            //تک فاز
            //DataGridViewComboBoxColumn c1 = (DataGridViewComboBoxColumn)gvFeeder1.Columns["Column7"];
            //c1.DisplayMember = "Name";
            //c1.ValueMember = "Code";
            //c1.DataSource = Atend.Base.Equipment.EPhuse.SelectAll();

            //DataGridViewComboBoxColumn c2 = (DataGridViewComboBoxColumn)gvFeeder1.Columns["Column6"];
            //c2.DisplayMember = "Num";
            //c2.ValueMember = "Num";
            //c2.DataSource = dtSemshNum;
            ////ed.WriteMessage("Items.Count= " + c2.Items.Count.ToString() + "\n");

            ////ed.WriteMessage("ShemshNum= " + dtSemshNum.Rows.Count.ToString() + "\n");

            //gvFeeder1.AutoGenerateColumns = false;
            //gvFeeder1.DataSource = dtDB1;
            BindDataToTreeView();

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

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDBSearch03 frmDBSearch = new frmDBSearch03(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmDBSearch);
        }

        private void nudOutputCount_ValueChanged(object sender, EventArgs e)
        {
            //ed.WriteMessage("Output FeederNum= " + InputFeederNum.ToString() + "\n");

            if (OutputFeederNum < nudOutputCount.Value)
            {
                //ed.WriteMessage("I Am IN The If\n");
                //DataRow _dataRow = dtOutputDB.NewRow();
                //_dataRow["FeederNum"] = nudOutputCount.Value;
                //dtOutputDB.Rows.Add(_dataRow);
                ////ed.WriteMessage("row count : " + dtDB.Rows.Count.ToString() + "\n");

                //gvOutputFeeder.AutoGenerateColumns = false;
                //gvOutputFeeder.DataSource = dtOutputDB;
                //gvOutputFeeder.Refresh();


                for (int i = (int)OutputFeederNum + 1; i <= nudOutputCount.Value; i++)
                {
                    DataRow _dataRow = dtOutputDB.NewRow();
                    _dataRow["FeederNum"] = i;
                    dtOutputDB.Rows.Add(_dataRow);

                    //ed.WriteMessage("ShemshNum = " + dtDB.Rows[i]["ShemshNum"].ToString() + "\n");

                    //if (gvInputFeeder.Rows[i].Cells[0].Value.ToString() == dtInputDB.Rows[i]["FeederNum"].ToString() /*&& !string.IsNullOrEmpty(dtInputDB.Rows[i]["ShemshNum"].ToString())*/)
                    //{
                    //    //ed.WriteMessage("\nIN IF\n");
                    //    gvInputFeeder.Rows[i].Cells[1].Value = dtInputDB.Rows[i]["ShemshNum"].ToString();
                    //}
                }

                gvOutputFeeder.AutoGenerateColumns = false;
                gvOutputFeeder.DataSource = dtOutputDB;
                gvOutputFeeder.Refresh();


            }
            else
            {
                //ed.WriteMessage("I AM IN THe Else\n");

                for (int i = (int)nudOutputCount.Value + 1; i <= OutputFeederNum; i++)
                {
                    if (dtOutputDB.Rows.Count > 0)
                    {
                        dtOutputDB.Rows.RemoveAt(dtOutputDB.Rows.Count - 1);
                    }
                }

                gvOutputFeeder.AutoGenerateColumns = false;
                gvOutputFeeder.DataSource = dtOutputDB;


            }


            //ed.WriteMessage("ValueChange\n");
            //ed.WriteMessage("FeederNum= " + FeederNum.ToString() + "\n");




            //if (FeederNum < nudOutputCount.Value)
            //{
            //    //ed.WriteMessage("I Am IN The If\n");
            //    DataRow _dataRow = dtDB.NewRow();
            //    _dataRow["FeederNum"] = nudOutputCount.Value;
            //    dtDB.Rows.Add(_dataRow);
            //    //ed.WriteMessage("row count : " + dtDB.Rows.Count.ToString() + "\n");

            //    gvInputFeeder.AutoGenerateColumns = false;
            //    gvInputFeeder.DataSource = dtDB;
            //    gvInputFeeder.Refresh();


            //    for (int i = 0; i < gvInputFeeder.Rows.Count; i++)
            //    {
            //        //ed.WriteMessage("ShemshNum = " + dtDB.Rows[i]["ShemshNum"].ToString() + "\n");

            //        if (gvInputFeeder.Rows[i].Cells[0].Value.ToString() == dtDB.Rows[i]["FeederNum"].ToString() && !string.IsNullOrEmpty(dtDB.Rows[i]["ShemshNum"].ToString()))
            //        {
            //            //ed.WriteMessage("\nIN IF\n");
            //            gvInputFeeder.Rows[i].Cells[1].Value = dtDB.Rows[i]["ShemshNum"].ToString();
            //        }
            //    }
            //}
            //else
            //{
            //    //ed.WriteMessage("I AM IN THe Else\n");
            //    if (dtDB.Rows.Count > 0)
            //        dtDB.Rows.RemoveAt(dtDB.Rows.Count - 1);
            //}
            ////BindPhuseToComboBox((DataGridViewComboBoxCell)gvFeeder.Rows[gvFeeder.Rows.Count - 1].Cells[1]);
            ////DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvFeeder.Columns["Column2"];
            ////c.DisplayMember = "FullName";
            ////c.ValueMember = "Code";

            ////c.DataSource = Atend.Base.Equipment.EPhuse.SelectAll();
            ////gvFeeder.AutoGenerateColumns = false;
            ////gvFeeder.DataSource = dtDB;
            //FeederNum = nudOutputCount.Value;

            OutputFeederNum = nudOutputCount.Value;

        }

        private void gvFeeder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex==1)
            //    dtDB.Rows[Convert.ToInt32(nudInputCount.Value - 1)][1] = gvFeeder.Rows[Convert.ToInt32(nudInputCount.Value - 1)].Cells[1].Value;
        }

        private void SelectProduct()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(Atend.Control.Common.selectedProductCode);
            txtName.Text = product.Name;
            txtCode.Text = product.Code.ToString();
            txtBackUpName.Text = product.Name;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.DB);

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRow _dataRow = dtInputDB.NewRow();

            dtInputDB.Rows.Add(_dataRow);
        }

        private void btnInsert_Click(object sender, EventArgs e)
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

        private void nudCount1_ValueChanged(object sender, EventArgs e)
        {
            //ed.WriteMessage("ValueChange1\n");
            //ed.WriteMessage("FeederNum= " + FeederNum1.ToString() + "\n");
            if (OutputFeederNum < nudInputCount.Value)
            {
                //ed.WriteMessage("I Am IN The If\n");
                DataRow _dataRow = dtOutputDB.NewRow();
                _dataRow["FeederNum"] = nudInputCount.Value;

                dtOutputDB.Rows.Add(_dataRow);
                //ed.WriteMessage("row count : " + dtDB1.Rows.Count.ToString() + "\n");

                //gvFeeder1.DataSource = dtDB1;
            }
            else
            {
                //ed.WriteMessage("I AM IN THe Else\n");
                dtOutputDB.Rows.RemoveAt(dtOutputDB.Rows.Count - 1);
            }

            OutputFeederNum = nudInputCount.Value;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void nudInputCount_ValueChanged(object sender, EventArgs e)
        {
            //ed.WriteMessage("Input FeederNum= " + InputFeederNum.ToString() + "\n");

            if (InputFeederNum < nudInputCount.Value)
            {
                //ed.WriteMessage("I Am IN The If\n");
                //DataRow _dataRow = dtInputDB.NewRow();
                //_dataRow["FeederNum"] = nudInputCount.Value;
                //dtInputDB.Rows.Add(_dataRow);
                ////ed.WriteMessage("row count : " + dtDB.Rows.Count.ToString() + "\n");

                //gvInputFeeder.AutoGenerateColumns = false;
                //gvInputFeeder.DataSource = dtInputDB;
                //gvInputFeeder.Refresh();


                for (int i = (int)InputFeederNum + 1; i <= nudInputCount.Value; i++)
                {
                    DataRow _dataRow = dtInputDB.NewRow();
                    _dataRow["FeederNum"] = i;
                    dtInputDB.Rows.Add(_dataRow);

                    //ed.WriteMessage("ShemshNum = " + dtDB.Rows[i]["ShemshNum"].ToString() + "\n");

                    //if (gvInputFeeder.Rows[i].Cells[0].Value.ToString() == dtInputDB.Rows[i]["FeederNum"].ToString() /*&& !string.IsNullOrEmpty(dtInputDB.Rows[i]["ShemshNum"].ToString())*/)
                    //{
                    //    //ed.WriteMessage("\nIN IF\n");
                    //    gvInputFeeder.Rows[i].Cells[1].Value = dtInputDB.Rows[i]["ShemshNum"].ToString();
                    //}
                }

                gvInputFeeder.AutoGenerateColumns = false;
                gvInputFeeder.DataSource = dtInputDB;
                gvInputFeeder.Refresh();

            }
            else
            {
                //ed.WriteMessage("I AM IN THe Else\n");

                for (int i = (int)nudInputCount.Value + 1; i <= InputFeederNum; i++)
                {
                    if (dtInputDB.Rows.Count > 0)
                    {
                        dtInputDB.Rows.RemoveAt(dtInputDB.Rows.Count - 1);
                    }
                }

                gvInputFeeder.AutoGenerateColumns = false;
                gvInputFeeder.DataSource = dtInputDB;

            }

            //////Halate Ghabli
            ////try
            ////{
            ////    for (int k = 0; k < gvInputFeeder.Rows.Count; k++)
            ////    {
            ////        if (Convert.ToDecimal(gvInputFeeder.Rows[k].Cells[1].Value.ToString()) > nudInputCount.Value)
            ////        {
            ////            MessageBox.Show("این عدد قابل تغییر نیست . از این شماره شمش در جدول استفاده شده . لطفاً ابتدا جدول را تغییر دهید");
            ////            nudInputCount.Value = Shemsh;
            ////            return;
            ////        }
            ////    }

            ////}
            ////catch
            ////{ }


            ////dtSemshNum.Rows.Clear();
            ////for (int i = 1; i <= nudInputCount.Value; i++)
            ////{
            ////    DataRow dr = dtSemshNum.NewRow();
            ////    //dr["Name"] = tvProduct.Nodes[i].Nodes[j].Text.ToString();
            ////    dr["ShemshNum"] = i.ToString();
            ////    dtSemshNum.Rows.Add(dr);
            ////}


            //DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvInputFeeder.Columns[1];
            //c.DataSource = dtSemshNum;
            //c.DisplayMember = "ShemshNum";
            //c.ValueMember = "ShemshNum";

            //gvInputFeeder.Refresh();


            //BindPhuseToComboBox((DataGridViewComboBoxCell)gvFeeder.Rows[gvFeeder.Rows.Count - 1].Cells[1]);
            //DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvFeeder.Columns["Column2"];
            //c.DisplayMember = "FullName";
            //c.ValueMember = "Code";

            //c.DataSource = Atend.Base.Equipment.EPhuse.SelectAll();
            //gvFeeder.AutoGenerateColumns = false;
            //gvFeeder.DataSource = dtDB;
            InputFeederNum = nudInputCount.Value;

        }

        private void nudInputCount_Enter(object sender, EventArgs e)
        {
            Shemsh = Convert.ToInt32(nudInputCount.Value);
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
            if (selectedDBCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.DB), selectedDBCode))
                {
                    Atend.Base.Equipment.EDB DB = Atend.Base.Equipment.EDB.SelectByXCode(selectedDBCode);
                    Code = DB.Code;
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

            //if (selectedDBCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EDB.ShareOnServer(selectedDBCode))
            //    {
            //        Atend.Base.Equipment.EDB d1 = Atend.Base.Equipment.EDB.SelectByXCode(selectedDBCode);
            //        Code = d1.Code;
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

        private void button2_Click_1(object sender, EventArgs e)
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

    }
}