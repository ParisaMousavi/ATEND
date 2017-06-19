using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using System.Collections;
namespace Atend.Equipment
{
    public partial class frmGroundCabel02 : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        public int ProductCode = -1;
        Guid SelectedCabelCode = Guid.Empty;
        int Code = -1;
        public bool IsDefault = false;
        //string[] CrosSection1 ={"1.5", "2.5", "4", "6","10","16","25","35","50","70","95","120","150","185","240","300"};
        //string[] CrossSection2 ={"3*1.5","4*25","3*25+16","3*35+16","3*35+25","3*50+25","3*70+35","3*95+50","3*120+70","3*150+70","3*185+95","3*240+120","3*300+150"};
        //string[] CrossSectionValue1 ={ "4.5", "100", "91", "121", "130", "175", "245", "335", "430", "520", "650", "840", "1050" };

        DataColumn TypeName = new DataColumn("Name", typeof(string));
        DataColumn TypeCode = new DataColumn("Code", typeof(int));
        DataTable TypeTbl = new DataTable();
        bool ForceToClose = false;


        public frmGroundCabel02()
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
            dr1["Name"] = "XLPE از جنس آلمینیوم با عایق خشک";
            dr1["Code"] = 1;

            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "XLPE از جنس مس با عایق خشک";
            dr2["Code"] = 2;

            DataRow dr3 = TypeTbl.NewRow();
            dr3["Name"] = "PVC از جنس آلمینیوم با عایق خشک";
            dr3["Code"] = 3;

            DataRow dr4 = TypeTbl.NewRow();
            dr4["Name"] = "PVC از جنس مس با عایق خشک";
            dr4["Code"] = 4;

            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);
            TypeTbl.Rows.Add(dr3);
            TypeTbl.Rows.Add(dr4);

        }

        private void Reset()
        {
            SelectedCabelCode = Guid.Empty;
            nudNumString.Value = 0;
            txtCrossSectionArea.Text = string.Empty;
            cboType.SelectedIndex = 0;
            ClearCheckAndGrid(tvOperation, gvOperation);
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            txtName.Text = string.Empty;
            txtComment.Text = string.Empty;
            txtReactance.Text = string.Empty;
            txtResistance.Text = string.Empty;
            txtMaxCurrent.Text = string.Empty;
            txtCapacitance.Text = string.Empty;
            txtSize.Text = string.Empty;
            txtMaxCurrent1Second.Text = string.Empty;
            Code = -1;
            Atend.Control.Common.selectedProductCode = -1;
            txtCode.Text = string.Empty;
            txtBackUpName.Text = string.Empty;
            IsDefault = false;
            tsbIsDefault.Checked = false;
            ProductCode = -1;
        }

        private void BindDataToTypeComboBox()
        {
            //cboType.SelectedIndex = 0;
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "Code";
            cboType.DataSource = TypeTbl;
        }

        private void Save()
        {
            txtName.Focus();
            //ed.WriteMessage("I aM In sve Method");
            Atend.Base.Equipment.EGroundCabel middleGroundCabel = new Atend.Base.Equipment.EGroundCabel();
            ArrayList EOperation = new ArrayList();
            middleGroundCabel.ProductCode = Atend.Control.Common.selectedProductCode;
            middleGroundCabel.Vol = double.Parse(cboVol.Text);
            middleGroundCabel.Type = Convert.ToInt16(cboType.SelectedValue.ToString()); ;
            middleGroundCabel.Code = Code;
            //if (nudNumString.SelectedIndex == 0 | nudNumString.SelectedIndex == 1 | cboNumString.SelectedIndex == 2 | cboNumString.SelectedIndex == 3 | cboNumString.SelectedIndex == 4)
            //{
            //    middleGroundCabel.NumString = Convert.ToInt32(cboNumString.Text);
            //}
            //else
            //{
            //    middleGroundCabel.NumString = 6;
            //}
            middleGroundCabel.NumString = Convert.ToInt32(nudNumString.Value);
            middleGroundCabel.Name = txtName.Text;
            //if (cboNumString.SelectedIndex == 0 | cboNumString.SelectedIndex == 1 | cboNumString.SelectedIndex == 2 | cboNumString.SelectedIndex == 3 | cboNumString.SelectedIndex == 4)
            //{
            //    middleGroundCabel.CrossSectionArea =Convert.ToDouble(cboCrossSectionArea.Text);
            //}
            //else
            //{
            //    middleGroundCabel.CrossSectionArea = Convert.ToDouble(CrossSectionValue1[cboCrossSectionArea.SelectedIndex]);
            //}
            middleGroundCabel.CrossSectionArea = Convert.ToDouble(txtCrossSectionArea.Text);
            middleGroundCabel.Comment = txtComment.Text;
            middleGroundCabel.IsDefault = IsDefault;
            middleGroundCabel.MaxCurrent = Convert.ToDouble(txtMaxCurrent.Text);
            middleGroundCabel.Reactance = Convert.ToDouble(txtReactance.Text);
            middleGroundCabel.Resistance = Convert.ToDouble(txtResistance.Text);
            middleGroundCabel.Capacitance = Convert.ToDouble(txtCapacitance.Text);
            middleGroundCabel.Size = Convert.ToInt32(txtSize.Text);
            middleGroundCabel.MaxCurrent1Second = Convert.ToDouble(txtMaxCurrent1Second.Text);
            //saveOperation
            //ed.WriteMessage("I aM In sve Opearation");

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
            middleGroundCabel.EquipmentList = EPackageProduct;

            //Operation
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);
            }
            middleGroundCabel.OperationList = EOperation;

            if (SelectedCabelCode == Guid.Empty)
            {
                if (middleGroundCabel.InsertX())
                {
                    Reset();
                }
                else
                    MessageBox.Show("امکان ثبت کردن اطلاعات نمی باشد", "خطا");
            }
            else
            {
                middleGroundCabel.XCode = SelectedCabelCode;
                //MessageBox.Show("go to update method : " + Pole.Code + "\n");
                if (middleGroundCabel.UpdateX())
                {
                    Reset();
                }
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
                if (SelectedCabelCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EGroundCabel Equip = Atend.Base.Equipment.EGroundCabel.SelectByXCode(SelectedCabelCode);
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
            //ed.WriteMessage("GoToName ValiDation\n");
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
            if (Atend.Base.Equipment.EGroundCabel.SearchByName(txtName.Text) == true && SelectedCabelCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCapacitance.Text))
            {
                MessageBox.Show("لطفا ظرفیت را مشخص نمایید", "خطا");
                txtCapacitance.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtResistance.Text))
            {
                MessageBox.Show("لطفا مقاومت اهمی را مشخص نمایید", "خطا");
                txtResistance.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtReactance.Text))
            {
                MessageBox.Show("لطفا مقاومت سلفی را مشخص نمایید", "خطا");
                txtReactance.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtMaxCurrent.Text))
            {
                MessageBox.Show("لطفا جریان را مشخص نمایید", "خطا");
                txtMaxCurrent.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtSize.Text))
            {
                MessageBox.Show("لطفا سایز را مشخص نمایید", "خطا");
                txtSize.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtMaxCurrent1Second.Text))
            {
                MessageBox.Show("لطفا حداکثر جریان در یک ثانیه را مشخص نمایید", "خطا");
                txtMaxCurrent1Second.Focus();
                return false;
            }


            /////////////////////////////////////////////////////////////////////

            if (!Atend.Control.NumericValidation.DoubleConverter(txtCapacitance.Text))
            {
                MessageBox.Show("لطفا ظرفیت را با فرمت مناسب وارد کنید", "خطا");
                txtCapacitance.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtResistance.Text))
            {
                MessageBox.Show("لطفا مقاومت اهمی را با فرمت مناسب وارد کنید", "خطا");
                txtResistance.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtReactance.Text))
            {
                MessageBox.Show("لطفا مقاومت سلفی را با فرمت مناسب وارد کنید", "خطا");
                txtReactance.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtMaxCurrent.Text))
            {
                MessageBox.Show("لطفا جریان را با فرمت مناسب وارد کنید", "خطا");
                txtMaxCurrent.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.Int32Converter(txtSize.Text))
            {
                MessageBox.Show("لطفا سایز را با فرمت مناسب وارد کنید", "خطا");
                txtSize.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.Int32Converter(txtMaxCurrent1Second.Text))
            {
                MessageBox.Show("لطفا حداکثر جریان در یک ثانیه را با فرمت مناسب وارد کنید", "خطا");
                txtMaxCurrent1Second.Focus();
                return false;
            }

            //ed.WriteMessage("GoToVol ValiDation\n");

            if (string.IsNullOrEmpty(cboVol.Text))
            {
                MessageBox.Show("لطفا ولتاژ را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cboVol.Focus();
                return false;
            }
            //ed.WriteMessage("GoToVol ValiDation\n");


            if (!Atend.Control.NumericValidation.DoubleConverter(cboVol.Text))
            {
                MessageBox.Show("لطفاً ولتاژ را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cboVol.Focus();
                //txtVol.Select(0, txtVol.Text.Length);
                return false;
            }

            //ed.WriteMessage("GoToCrossSection ValiDation\n");



            if (string.IsNullOrEmpty(txtCrossSectionArea.Text))
            {
                MessageBox.Show("لطفا سطح مقطع را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCrossSectionArea.Focus();

                return false;
            }

            //ed.WriteMessage("IGoToNumString ValiDation\n");


            if (string.IsNullOrEmpty(nudNumString.Text))
            {
                MessageBox.Show("لطفا تعداد رشته را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                nudNumString.Focus();
                return false;
            }
            //ed.WriteMessage("GoToType ValiDation\n");



            if (string.IsNullOrEmpty(cboType.Text))
            {
                MessageBox.Show("لطفا نوع را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                cboType.Focus();
                return false;
            }

            Atend.Base.Equipment.EGroundCabel groundcabel = Atend.Base.Equipment.EGroundCabel.CheckForExist(Convert.ToInt32(nudNumString.Value), Convert.ToDouble(txtCrossSectionArea.Text), Convert.ToInt16(cboType.SelectedValue.ToString()),
                                                                                                          double.Parse(cboVol.Text), Convert.ToInt32(txtSize.Text), Convert.ToDouble(txtResistance.Text), Convert.ToDouble(txtReactance.Text),
                                                                                                          Convert.ToDouble(txtMaxCurrent.Text), Convert.ToDouble(txtCapacitance.Text));
            if (groundcabel.Code != -1 && SelectedCabelCode == Guid.Empty)
            {
                if (MessageBox.Show("کابل زمینی با مشخصات داده شده موجود میباشد\n\n کابل زمینی با مشخصات فوق  : " + groundcabel.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    nudNumString.Focus();
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

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(SelectedCabelCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel), _EProductPackage.XCode, _EProductPackage.TableType))
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

            foreach (DataRow dr in ProductType.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = dr["Name"].ToString();
                node.Tag = dr["ID"].ToString();
                tvOperation.Nodes.Add(node);
            }

        }

        public void BindDataToOwnControl(Guid XCode)
        {

            ClearCheckAndGrid(tvOperation, gvOperation);
            ClearCheckAndGrid(tvOperation, gvOperation);

            SelectedCabelCode = XCode;

            Atend.Base.Equipment.EGroundCabel middleGrounCabel = Atend.Base.Equipment.EGroundCabel.SelectByXCode(XCode);
            txtComment.Text = middleGrounCabel.Comment;
            Atend.Control.Common.selectedProductCode = middleGrounCabel.ProductCode;
            SelectProduct();

            txtName.Text = middleGrounCabel.Name;
            txtMaxCurrent.Text = Convert.ToString(Math.Round(middleGrounCabel.MaxCurrent, 8));
            txtCapacitance.Text = Convert.ToString(Math.Round(middleGrounCabel.Capacitance, 8));
            txtResistance.Text = Convert.ToString(Math.Round(middleGrounCabel.Resistance, 8));
            txtReactance.Text = Convert.ToString(Math.Round(middleGrounCabel.Reactance, 8));
            txtSize.Text = middleGrounCabel.Size.ToString();
            txtMaxCurrent1Second.Text = Convert.ToString(Math.Round(middleGrounCabel.MaxCurrent1Second, 8));
            cboVol.Text = Convert.ToString(Math.Round(middleGrounCabel.Vol, 8));
            cboType.SelectedValue = middleGrounCabel.Type;
            tsbIsDefault.Checked = middleGrounCabel.IsDefault;
            Code = middleGrounCabel.Code;
            //if (middleGrounCabel.NumString < 5)
            //{
            //    cboNumString.Text = middleGrounCabel.NumString.ToString();
            //}
            //else
            //{
            //    cboNumString.SelectedIndex=5;
            //}
            ed.WriteMessage("middleGrounCabel.NumString:{0}\n", middleGrounCabel.NumString);
            nudNumString.Value = middleGrounCabel.NumString;
            txtCrossSectionArea.Text = Convert.ToString(Math.Round(middleGrounCabel.CrossSectionArea, 8));
            //if (middleGrounCabel.NumString < 5)
            //{
            //    cboCrossSectionArea.Text = middleGrounCabel.CrossSectionArea.ToString();
            //}
            //else
            //{
            //    for (int i = 0; i < CrossSectionValue1.Length; i++)
            //    {
            //        if (CrossSectionValue1[i].ToString() == middleGrounCabel.CrossSectionArea.ToString())
            //        {
            //            cboCrossSectionArea.SelectedIndex = i;
            //        }
            //    }
            //}
            BindTreeViwAndGrid();
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectedCabelCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectedCabelCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            Atend.Base.Equipment.EGroundCabelTip NeutralProduct = Atend.Base.Equipment.EGroundCabelTip.SelectByNeutralProductXCode(SelectedCabelCode);
            if (NeutralProduct.Code != -1)
            {
                MessageBox.Show("حذف کابل زمینی بدلیل وجود در تیپ بندی امکانپذیر نمی باشد ", "خطا");
                return;
            }
            Atend.Base.Equipment.EGroundCabelTip PhaseProduct = Atend.Base.Equipment.EGroundCabelTip.SelectByPhaseProductXCode(SelectedCabelCode);
            if (PhaseProduct.Code != -1)
            {
                MessageBox.Show("حذف کابل زمینی بدلیل وجود در تیپ بندی امکانپذیر نمی باشد ", "خطا");
                return;
            }

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedCabelCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EGroundCabel.DeleteX(SelectedCabelCode))
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
            gvOperation.Rows.Clear();
            dataGridView.Refresh();
        }

        private void BindTreeViwAndGrid()
        {
            //EQUIPMENT
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            gvSelectedEquipment.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EGroundCabel.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EGroundCabel.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {
                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        if (Atend.Base.Equipment.EGroundCabel.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            if (chileNode.Tag.ToString() == s)
                            {
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquipment.Rows.Add();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EGroundCabel.nodeCountEPackageX[i].ToString();
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
            for (int i = 0; i < Atend.Base.Equipment.EGroundCabel.nodeKeysX.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EGroundCabel.nodeKeysX[i]);
                string s = Operation.ProductID.ToString();

                foreach (TreeNode rootnode in tvOperation.Nodes)
                {

                    //foreach (TreeNode chileNode in rootnode.Nodes)
                    //{
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

        private void frmGroundMiddleCabel_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            nudNumString.Value = 0;
            cboVol.SelectedIndex = 0;
            BindDataToTreeView();
            BindDataToTypeComboBox();
            cboType.SelectedIndex = 0;

        }

        private void cboNumString_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboCrossSectionArea.Items.Clear();
            //if (cboNumString.SelectedIndex == 0 | cboNumString.SelectedIndex == 1 | cboNumString.SelectedIndex == 2 | cboNumString.SelectedIndex == 3 | cboNumString.SelectedIndex == 4)
            //{
            //    for (int i = 0; i < CrosSection1.Length; i++)
            //    {
            //        cboCrossSectionArea.Items.Add(CrosSection1[i]);
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < CrossSection2.Length; i++)
            //    {
            //        cboCrossSectionArea.Items.Add(CrossSection2[i]);
            //    }
            //}
            //cboCrossSectionArea.SelectedIndex = 0;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
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

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            frmGroundCabelSearch02 frmCabelSearch02 = new frmGroundCabelSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmCabelSearch02);
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (CheckStatuseOfAccessChangeDefault())
                Delete();
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
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

        private void tvOperation_BeforeCheck(object sender, TreeViewCancelEventArgs e)
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

        private void SelectProduct()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(Atend.Control.Common.selectedProductCode);
            txtName.Text = product.Name;
            txtBackUpName.Text = product.Name;
            txtCode.Text = product.Code.ToString();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.GroundCabel);

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
            }
        }

        private void btnDefineType_Click(object sender, EventArgs e)
        {
            //Atend.Equipment.frmCabelType cabelType = new Atend.Equipment.frmCabelType();
            //cabelType.ShowDialog();
            //BindDataToTypeComboBox();
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

            if (SelectedCabelCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel), SelectedCabelCode))
                {
                    Atend.Base.Equipment.EGroundCabel GroundCabel = Atend.Base.Equipment.EGroundCabel.SelectByXCode(SelectedCabelCode);
                    Code = GroundCabel.Code;
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

            //if (SelectedCabelCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EGroundCabel.ShareOnServer(SelectedCabelCode))
            //    {
            //        Atend.Base.Equipment.EGroundCabel g1 = Atend.Base.Equipment.EGroundCabel.SelectByXCode(SelectedCabelCode);
            //        Code = g1.Code;
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

        private void label30_Click(object sender, EventArgs e)
        {

        }
    }
}