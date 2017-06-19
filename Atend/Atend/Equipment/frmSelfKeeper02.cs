using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Atend.Base;
using System.Collections;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Equipment
{
    public partial class frmSelfKeeper02 : Form
    {

        public int productCode = -1;
        public Guid SelectConductor = Guid.Empty;
        public bool IsDefault = false;
        int Code = -1;

        DataColumn DamperTypeName = new DataColumn("Name", typeof(string));
        DataColumn DamperTypeCode = new DataColumn("Code", typeof(int));

        DataTable DamperTypeTbl = new DataTable();

        DataColumn ConductorMaterialName = new DataColumn("Name", typeof(string));
        DataColumn ConductorMaterialCode = new DataColumn("Code", typeof(int));

        DataTable condMaterialTbl = new DataTable();
        bool ForceToClose = false;

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectConductor == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.ESelfKeeper selfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(SelectConductor);
                    if (selfKeeper.IsDefault || IsDefault)
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
            if (Atend.Base.Equipment.ESelfKeeper.SearchByName(txtName.Text) == true && SelectConductor == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCrossSectionArea.Text))
            {
                MessageBox.Show("لطفاً سطح مقطع را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCrossSectionArea.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDiagonal.Text))
            {
                MessageBox.Show("لطفاً قطر هادی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtDiagonal.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAlsticity.Text))
            {
                MessageBox.Show("لطفاً الستیسیتی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtAlsticity.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(txtWeight.Text))
            //{
            //    MessageBox.Show("لطفاً وزن را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            //    txtWeight.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtAlpha.Text))
            {
                MessageBox.Show("لطفاً آلفا را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtAlpha.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUTS.Text))
            {
                MessageBox.Show("لطفاً UTS را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtUTS.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaxCurrent.Text))
            {
                MessageBox.Show("لطفاً ماکزیمم جریان را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtMaxCurrent.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtResistance.Text))
            {
                MessageBox.Show("لطفاً مقاومت اهمی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtResistance.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtReactance.Text))
            {
                MessageBox.Show("لطفاً مقاومت سلفی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCrossSectionArea.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCapacitance.Text))
            {
                MessageBox.Show("لطفاً کپسیتانس را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCapacitance.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtCrossSectionArea.Text))
            {
                MessageBox.Show("لطفاٌ سطح مقطع را بافرمت عددی مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCrossSectionArea.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtDiagonal.Text))
            {
                MessageBox.Show("لطفاً قطر هادی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtDiagonal.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtAlsticity.Text))
            {
                MessageBox.Show("لطفاً الاستیسیتی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtAlsticity.Focus();
                return false;
            }
            //if (!Atend.Control.NumericValidation.DoubleConverter(txtWeight.Text))
            //{
            //    MessageBox.Show("لطفاً وزن را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            //    txtWeight.Focus();
            //    return false;
            //}
            if (!Atend.Control.NumericValidation.DoubleConverter(txtAlpha.Text))
            {
                MessageBox.Show("لطفاً آلفا را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtAlpha.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtUTS.Text))
            {
                MessageBox.Show("لطفاً UTS را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtUTS.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtMaxCurrent.Text))
            {
                MessageBox.Show("لطفاً ماکزیمم جریان را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtMaxCurrent.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtResistance.Text))
            {
                MessageBox.Show("لطفاً مقاومت اهمی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtResistance.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtReactance.Text))
            {
                MessageBox.Show("لطفاً مقاومت سلفی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtReactance.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtGMR.Text))
            {
                MessageBox.Show("لطفاً GRM را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtGMR.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtWC.Text))
            {
                MessageBox.Show("لطفاً وزن واحد طول سیم را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtWC.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtWC.Text))
            {
                MessageBox.Show("لطفاً وزن واحد طول سیم را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtWC.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtCapacitance.Text))
            {
                MessageBox.Show("لطفاٌ کپسیتانس را بافرمت عددی مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCapacitance.Focus();
                return false;
            }

            Atend.Base.Equipment.ESelfKeeper selfkeeper = Atend.Base.Equipment.ESelfKeeper.CheckForExist(Convert.ToInt32(cboMaterialConductor.SelectedValue), Convert.ToInt32(cboMaterialInsulator.SelectedValue), Convert.ToDouble(txtCrossSectionArea.Text),
                                                                                                       Convert.ToDouble(txtCrossSectionSelfKeeper.Text), Convert.ToDouble(txtDiagonal.Text), Convert.ToDouble(txtAlsticity.Text), Convert.ToDouble(txtAlpha.Text),
                                                                                                       Convert.ToDouble(txtUTS.Text), Convert.ToDouble(txtMaxCurrent.Text), Convert.ToDouble(txtResistance.Text), Convert.ToDouble(txtReactance.Text),
                                                                                                       Convert.ToDouble(txtWC.Text), Convert.ToDouble(txtNamedVoltage.Text), Convert.ToDouble(txtCapacitance.Text), Convert.ToDouble(txtMaxCurrent1Second.Text));
            if (selfkeeper.Code != -1 && SelectConductor == Guid.Empty)
            {
                if (MessageBox.Show("کابل خودنگهدار با مشخصات داده شده موجود میباشد\n\n کابل خودنگهدار با مشخصات فوق  : " + selfkeeper.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    cboMaterialConductor.Focus();
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

                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(SelectConductor, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper), _EProductPackage.XCode, _EProductPackage.TableType))
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
            txtName.Text = string.Empty;
            cboMaterialConductor.SelectedIndex = 0;
            txtAlpha.Text = string.Empty;
            txtAlsticity.Text = string.Empty;
            txtCrossSectionArea.Text = string.Empty;
            txtDiagonal.Text = string.Empty;
            txtMaxCurrent.Text = string.Empty;
            txtReactance.Text = string.Empty;
            txtResistance.Text = string.Empty;
            txtUTS.Text = string.Empty;
            //txtWeight.Text = "0";
            txtGMR.Text = string.Empty;
            SelectConductor = Guid.Empty;
            txtComment.Text = string.Empty;
            txtWC.Text = string.Empty;
            cboMaterialConductor.Focus();
            txtCapacitance.Text = string.Empty;
            txtNamedVoltage.Text = string.Empty;
            txtCrossSectionSelfKeeper.Text = string.Empty;
            IsDefault = false;
            tsbIsDefault.Checked = false;
            txtMaxCurrent1Second.Text = string.Empty;
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
            Atend.Base.Equipment.ESelfKeeper selfKeeper = new Atend.Base.Equipment.ESelfKeeper();
            selfKeeper.Alastisity = Convert.ToDouble(txtAlsticity.Text);
            selfKeeper.Alpha = Convert.ToDouble(txtAlpha.Text);

            selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(txtCrossSectionArea.Text);

            selfKeeper.CrossSectionKeeper = Convert.ToDouble(txtCrossSectionSelfKeeper.Text); ;
            selfKeeper.Diagonal = Convert.ToDouble(txtDiagonal.Text);
            selfKeeper.NamedVoltage = Convert.ToDouble(txtNamedVoltage.Text);
            selfKeeper.MaterialConductorCode = Convert.ToInt32(cboMaterialConductor.SelectedValue);
            selfKeeper.MaterialDamperCode = Convert.ToInt32(cboMaterialInsulator.SelectedValue);
            selfKeeper.MaxCurrent = Convert.ToDouble(txtMaxCurrent.Text);
            selfKeeper.ProductCode = Atend.Control.Common.selectedProductCode;
            selfKeeper.Reactance = Convert.ToDouble(txtReactance.Text);
            selfKeeper.Resistance = Convert.ToDouble(txtResistance.Text);
            selfKeeper.UTS = Convert.ToDouble(txtUTS.Text);
            selfKeeper.Weight = Convert.ToDouble(txtWC.Text);
            selfKeeper.GMR = Convert.ToDouble(txtGMR.Text);
            selfKeeper.Weight = Convert.ToDouble(txtWC.Text);
            selfKeeper.Comment = txtComment.Text;
            selfKeeper.Name = txtName.Text;
            selfKeeper.Capacitance = Convert.ToDouble(txtCapacitance.Text);
            selfKeeper.IsDefault = IsDefault;
            selfKeeper.MaxCurrent1Second = Convert.ToDouble(txtMaxCurrent1Second.Text);
            selfKeeper.Code = Code;

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
            selfKeeper.EquipmentList = EPackageProduct;

            //Operation
            ArrayList EOperation = new ArrayList();
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);
            }
            selfKeeper.OperationList = EOperation;
            if (SelectConductor == Guid.Empty)
            {
                if (selfKeeper.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                selfKeeper.XCode = SelectConductor;
                if (selfKeeper.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectConductor, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectConductor);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            Atend.Base.Equipment.ESelfKeeperTip NeutralProduct = Atend.Base.Equipment.ESelfKeeperTip.SelectByNeutralProductxCode(SelectConductor);
            if (NeutralProduct.Code != -1)
            {
                MessageBox.Show("حذف کابل خودنگهدار بدلیل وجود در تیپ بندی امکانپذیر نمی باشد ", "خطا");
                return;
            }
            Atend.Base.Equipment.ESelfKeeperTip NightProduct = Atend.Base.Equipment.ESelfKeeperTip.SelectByNightProductxCode(SelectConductor);
            if (NightProduct.Code != -1)
            {
                MessageBox.Show("حذف کابل خودنگهدار بدلیل وجود در تیپ بندی امکانپذیر نمی باشد ", "خطا");
                return;
            }
            Atend.Base.Equipment.ESelfKeeperTip PhaseProduct = Atend.Base.Equipment.ESelfKeeperTip.SelectByPhaseProductxCode(SelectConductor);
            if (PhaseProduct.Code != -1)
            {
                MessageBox.Show("حذف کابل خودنگهدار بدلیل وجود در تیپ بندی امکانپذیر نمی باشد ", "خطا");
                return;
            }

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectConductor != Guid.Empty)
                {
                    if (Atend.Base.Equipment.ESelfKeeper.DeleteX(SelectConductor))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }

        }

        public void BindDataToOwnControl(Guid _Code)
        {
            SelectConductor = _Code;
            //MessageBox.Show(" SelectConductor:" + SelectConductor.ToString() + "\n");
            Atend.Base.Equipment.ESelfKeeper selfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(_Code);
            Atend.Control.Common.selectedProductCode = selfKeeper.ProductCode;
            SelectProduct();
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(conductor.ProductCode);
            //MessageBox.Show("Name:"+conductor.Name.ToString());
            cboMaterialConductor.SelectedValue = selfKeeper.MaterialConductorCode;
            cboMaterialInsulator.SelectedValue = selfKeeper.MaterialDamperCode;
            txtName.Text = selfKeeper.Name;
            productCode = selfKeeper.ProductCode;

            txtAlpha.Text = Convert.ToString(Math.Round(selfKeeper.Alpha, 8));
            txtWC.Text = Convert.ToString(Math.Round(selfKeeper.Wc, 8));
            txtAlsticity.Text = Convert.ToString(Math.Round(selfKeeper.Alastisity, 8));
            txtCrossSectionArea.Text = Convert.ToString(Math.Round(selfKeeper.CrossSectionAreaConductor, 8));
            txtCrossSectionSelfKeeper.Text = Convert.ToString(Math.Round(selfKeeper.CrossSectionKeeper, 8));
            txtDiagonal.Text = Convert.ToString(Math.Round(selfKeeper.Diagonal, 8));
            txtMaxCurrent.Text = Convert.ToString(Math.Round(selfKeeper.MaxCurrent, 8));
            txtReactance.Text = Convert.ToString(Math.Round(selfKeeper.Reactance, 8));
            txtResistance.Text = Convert.ToString(Math.Round(selfKeeper.Resistance, 8));
            txtUTS.Text = Convert.ToString(Math.Round(selfKeeper.UTS, 8));
            txtWC.Text = Convert.ToString(Math.Round(selfKeeper.Weight, 8));
            txtGMR.Text = Convert.ToString(Math.Round(selfKeeper.GMR, 8));
            txtComment.Text = selfKeeper.Comment;
            txtNamedVoltage.Text = selfKeeper.NamedVoltage.ToString();
            txtCapacitance.Text = selfKeeper.Capacitance.ToString();
            tsbIsDefault.Checked = selfKeeper.IsDefault;
            txtMaxCurrent1Second.Text = selfKeeper.MaxCurrent1Second.ToString();
            Code = selfKeeper.Code;
            BindTreeViwAndGrid();
        }

        private void BindTreeViwAndGrid()
        {
            //EQUIPMENT
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            gvSelectedEquipment.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.ESelfKeeper.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.ESelfKeeper.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {
                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        if (Atend.Base.Equipment.ESelfKeeper.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            if (chileNode.Tag.ToString() == s)
                            {
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquipment.Rows.Add();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.ESelfKeeper.nodeCountEPackageX[i].ToString();
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
            for (int i = 0; i < Atend.Base.Equipment.ESelfKeeper.nodeKeys.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.ESelfKeeper.nodeKeys[i]);
                string s = Operation.ProductID.ToString();

                foreach (TreeNode rootnode in tvOperation.Nodes)
                {
                    if (rootnode.Tag.ToString() == s)
                    {
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

        public frmSelfKeeper02()
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

            DamperTypeTbl.Columns.Add(DamperTypeCode);
            DamperTypeTbl.Columns.Add(DamperTypeName);

            DataRow dr1 = DamperTypeTbl.NewRow();
            dr1["Name"] = "XLPE";
            dr1["Code"] = 1;

            DataRow dr2 = DamperTypeTbl.NewRow();
            dr2["Name"] = "PVC";
            dr2["Code"] = 2;

            //DataRow dr3 = DamperTypeTbl.NewRow();
            //dr3["Name"] = "کابل خود نگهدار(ABC)";
            //dr3["Code"] = 3;

            DamperTypeTbl.Rows.Clear();
            DamperTypeTbl.Rows.Add(dr1);
            DamperTypeTbl.Rows.Add(dr2);
            //DamperTypeTbl.Rows.Add(dr3);


            condMaterialTbl.Columns.Add(ConductorMaterialCode);
            condMaterialTbl.Columns.Add(ConductorMaterialName);

            DataRow drCond1 = condMaterialTbl.NewRow();
            drCond1["Name"] = "مس(CU)";
            drCond1["Code"] = 1;

            DataRow drCond2 = condMaterialTbl.NewRow();
            drCond2["Name"] = "آلومینیوم(AAC)";
            drCond2["Code"] = 2;

            DataRow drCond3 = condMaterialTbl.NewRow();
            drCond3["Name"] = "الومینیوم مغز فولاد(ACSR)";
            drCond3["Code"] = 3;

            DataRow drCond4 = condMaterialTbl.NewRow();
            drCond3["Name"] = "آلیاژ آلومینیوم";
            drCond3["Code"] = 4;

            condMaterialTbl.Rows.Clear();
            condMaterialTbl.Rows.Add(drCond1);
            condMaterialTbl.Rows.Add(drCond2);
            condMaterialTbl.Rows.Add(drCond3);
            condMaterialTbl.Rows.Add(drCond4);


        }

        private void SelectProductByCode()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(productCode);
            txtName.Text = product.Name;
        }

        private void BindMaterialToComboBox()
        {

            cboMaterialConductor.ValueMember = "Code";
            cboMaterialConductor.DisplayMember = "Name";
            cboMaterialConductor.DataSource = condMaterialTbl;

        }

        private void frmConductor_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //SelectProductByCode();
            BindMaterialToComboBox();
            BindTypeToComboBox();
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ById(productCode);
            //txtName.Text = product.Name;

            if (cboMaterialConductor.Items.Count > 0)
            {
                cboMaterialConductor.SelectedIndex = 0;// cboMaterialConductor.Items.Count - 1;
            }

            if (cboMaterialInsulator.Items.Count > 0)
            {
                cboMaterialInsulator.SelectedIndex = 0;// cboMaterialInsulator.Items.Count - 1;
            }

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
            //frmSelfKeeperMaterialConductor SelfKeeperMaterialConductor = new frmSelfKeeperMaterialConductor();
            //SelfKeeperMaterialConductor.ShowDialog();
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
            frmSelfKeeperSearch02 SelfKeeperSearch = new frmSelfKeeperSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(SelfKeeperSearch);
        }

        private void label25_Click(object sender, EventArgs e)
        {

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
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.SelfKeeper);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
            }
        }

        private void txtDiagonal_TextChanged(object sender, EventArgs e)
        {
            double val;
            if (!(string.IsNullOrEmpty(txtDiagonal.Text)))
            {
                try
                {
                    val = Convert.ToDouble(txtDiagonal.Text);
                    val *= .779;
                    txtGMR.Text = val.ToString();
                }
                catch
                {
                    MessageBox.Show("اطلاعات ورودي صحيح نمي باشد", "خطا");
                }
            }

        }

        private void btnNewType_Click(object sender, EventArgs e)
        {
            //Extra
            //frmSelfKeeperMaterialInsulator SelfKeeperMAterialInsulator = new frmSelfKeeperMaterialInsulator();
            //SelfKeeperMAterialInsulator.ShowDialog();
            //BindTypeToComboBox();
        }


        private void BindTypeToComboBox()
        {

            cboMaterialInsulator.ValueMember = "Code";
            cboMaterialInsulator.DisplayMember = "Name";
            cboMaterialInsulator.DataSource = DamperTypeTbl;

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
            if (SelectConductor != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper), SelectConductor))
                {
                    Atend.Base.Equipment.ESelfKeeper  SelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(SelectConductor);
                    Code = SelfKeeper.Code;
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

            //if (SelectConductor != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.ESelfKeeper.ShareOnServer(SelectConductor))
            //    {
            //        Atend.Base.Equipment.ESelfKeeper s1 = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(SelectConductor);
            //        Code = s1.Code;
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

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }



    }
}