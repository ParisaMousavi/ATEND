using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using System.Xml;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Design
{
    public partial class frmEditDrawPole : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public Guid NodeCode;

        //public ObjectId NodeObjID;
        bool AllowClose = false;
        bool ForceToClose = false;

        public frmEditDrawPole()
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
        }

        private void BindDataToComboBox()
        {
            //Extra
            //cboType.DataSource = Atend.Base.Equipment.EPoleType.SelectAll();
            //cboType.ValueMember = "Code";
            //cboType.DisplayMember = "Name";
        }

        private bool Validation()
        {

            /*if (!(chkCross.Checked || chkEnding.Checked || chkPulling.Checked || chkDP.Checked))
            {
                MessageBox.Show("لطفا نوع کنسول را مشخص نمایید", "خطا");
                tabControl1.TabPages[1].Focus();
                return false;
            }
            */
            if (txtHeight.Text == "")
            {
                MessageBox.Show("لطفا پایه مورد نظر را انتخاب نمایید", "خطا");
                tabControl1.TabPages[0].Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.Int32Converter(txtCountPole.Text))
            {
                MessageBox.Show("لطفا  تعداد پایه مورد نظر را انتخاب نمایید", "خطا");
                txtCountPole.Focus();
                return false;
            }

            return true;
        }

        private void Save()
        {
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dConsolCode.Clear();
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dPackages.Clear();
           //////////////////////// string strPoleType = "";

           //////////////////////// //ed.WriteMessage("Enter To Save\n");

           //////////////////////// // Save data in 3 different object and sent to AcadDrawPole
           //////////////////////// //ed.WriteMessage(string.Format("I am going to myDNode data \n"));
           //////////////////////// //myDNode.AutoCadCode = Atend.Control.Common.AutoCadId;
           //////////////////////// Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode(NodeCode);
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dNode.Code = myNode.Code;
           //////////////////////// //Atend.Base.Acad.AcadGlobal.PoleData.dNode.DesignCode = Atend.Control.Common.SelectedDesignCode;
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dNode.ProductCode = Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value.ToString());
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dNode.Code = myNode.Code;
           //////////////////////// //Atend.Base.Acad.AcadGlobal.dNode.LoadCode = myNode.LoadCode;
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dNode.Number = myNode.Number;


           //////////////////////// //ed.WriteMessage(string.Format("I am going to myDPoleInfo Data \n"));
           //////////////////////// Atend.Base.Design.DPoleInfo poleInfo = Atend.Base.Design.DPoleInfo.AccessSelectByNodeCode(myNode.Code);
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.Code = poleInfo.Code;
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.NodeCode = myNode.Code;
           //////////////////////// //Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.PoleType = 0;
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterCount = int.Parse(nudHalter.Value.ToString());
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.HalterType = int.Parse(cboHalterType.SelectedValue.ToString());
           //////////////////////// //Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.ConsolType = byte.Parse(cboConsolType.SelectedIndex.ToString());
           ////////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.CrossSectionValue = 0.000;
           //////////////////////// Atend.Base.Acad.AcadGlobal.PoleData.dPoleInfo.Factor = Convert.ToByte(txtCountPole.Text);

           //////////////////////// //ed.WriteMessage(string.Format("I am going to myDPackage data \n"));
           //////////////////////// //Atend.Base.Acad.AcadGlobal.dPackage.Count = 1;
           //////////////////////// //Atend.Base.Acad.AcadGlobal.dPackage.Type = (int)Atend.Control.Enum.ProductType.Pole;
           //////////////////////// //Atend.Base.Acad.AcadGlobal.dPackage.ProductCode = Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value);


           //////////////////////// Atend.Base.Design.DPackage dpackage = Atend.Base.Design.DPackage.AccessSelectByNodeCodeType(myNode.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
           //////////////////////// //Atend.Base.Acad.AcadGlobal.dConsolCount = gvPoleConsol.Rows.Count;

           //////////////////////// //after change

           //////////////////////// //ed.WriteMessage(string.Format("I am going to myAcadDrawPole data \n"));
           //////////////////////// //myAcadDrawPole.MyDNode = myDNode;
           //////////////////////// //myAcadDrawPole.MyDPoleInfo = myDPoleInfo;
           //////////////////////// //myAcadDrawPole.MyDPackage = myDPackage;
           //////////////////////// //myAcadDrawPole.MyNodeInformation = nodeInformation;

           //////////////////////// btnInsert.Focus();
           //////////////////////// DataTable dtConsol = Atend.Base.Design.DConsol.AccessSelectByParentCode(myNode.Code);
           //////////////////////// //Consol For Insert
           //////////////////////// //ed.WriteMessage("--------------------------------------------\n");
           //////////////////////// for (int i = 0; i < gvPoleConsol.Rows.Count; i++)
           //////////////////////// {
           ////////////////////////     DataRow[] dr = dtConsol.Select(" Code='" + gvPoleConsol.Rows[i].Cells[4].Value.ToString() + "'");
           ////////////////////////     //ed.WriteMessage("Code= " + gvPoleConsol.Rows[i].Cells[4].Value.ToString() + "\n");
           ////////////////////////     if (dr.Length == 0)
           ////////////////////////     {
           ////////////////////////         //ed.WriteMessage("It Is A New Consol\n");
           ////////////////////////         Atend.Base.Design.DPackage tempDPackage = new Atend.Base.Design.DPackage();

           ////////////////////////         DataGridViewCheckBoxCell chkBoxCell = (DataGridViewCheckBoxCell)gvPoleConsol.Rows[i].Cells[3];

           ////////////////////////         tempDPackage.IsExistance = Convert.ToByte(chkBoxCell.Value);

           ////////////////////////         tempDPackage.Count = 1;

           ////////////////////////         tempDPackage.Type = (int)Atend.Control.Enum.ProductType.Consol;
           ////////////////////////         tempDPackage.ParentCode = dpackage.Code;
           ////////////////////////         tempDPackage.ProductCode = Convert.ToInt32(gvPoleConsol.Rows[i].Cells[0].Value.ToString());

           ////////////////////////         Atend.Base.Acad.AcadGlobal.PoleData.dPackages.Add(tempDPackage);
                    
           ////////////////////////     }
           //////////////////////// }
           //////////////////////// //ed.WriteMessage("Atend.Base.Acad.AcadGlobal.dPackages.count= " + Atend.Base.Acad.AcadGlobal.dPackages.Count+"\n");
           //////////////////////// //ed.WriteMessage("--------------------------------------------\n");
           //////////////////////// DataTable dtGrid = new DataTable();
           //////////////////////// DataColumn dcCode = new DataColumn("Code");
           //////////////////////// DataColumn dcName = new DataColumn("Name");
           //////////////////////// DataColumn dcConsolType = new DataColumn("ConsolType");
           //////////////////////// DataColumn dcIsExistance = new DataColumn("IsExist");
           //////////////////////// DataColumn dcGuid = new DataColumn("Guid");
           //////////////////////// dtGrid.Columns.Add(dcCode);
           //////////////////////// dtGrid.Columns.Add(dcName);
           //////////////////////// dtGrid.Columns.Add(dcConsolType);
           //////////////////////// dtGrid.Columns.Add(dcIsExistance);
           //////////////////////// dtGrid.Columns.Add(dcGuid);

           //////////////////////// for (int i = 0; i < gvPoleConsol.Rows.Count; i++)
           //////////////////////// {
           ////////////////////////     //ed.WriteMessage("create Row\n");
           ////////////////////////     DataGridViewCheckBoxCell chkBoxCell = (DataGridViewCheckBoxCell)gvPoleConsol.Rows[i].Cells[3];
           ////////////////////////     DataRow dr = dtGrid.NewRow();

           ////////////////////////     dr["Code"] = gvPoleConsol.Rows[i].Cells[0].Value.ToString();

           ////////////////////////     dr["Name"] = gvPoleConsol.Rows[i].Cells[1].Value.ToString();

           ////////////////////////     dr["ConsolType"] = gvPoleConsol.Rows[i].Cells[2].Value.ToString();

           ////////////////////////     dr["IsExist"] = chkBoxCell.Value;

           ////////////////////////     dr["Guid"] = gvPoleConsol.Rows[i].Cells[4].Value.ToString();

           ////////////////////////     dtGrid.Rows.Add(dr);
           //////////////////////// }
           //////////////////////// //ed.WriteMessage("rows=" + dtGrid.Rows.Count + "\n");
           //////////////////////// //ed.WriteMessage("#############################################################\n");
           //////////////////////// foreach (DataRow dr in dtConsol.Rows)
           //////////////////////// {
           ////////////////////////     string Code = dr["Code"].ToString();
           ////////////////////////     DataRow[] dr1 = dtGrid.Select(" Guid='" + Code + "'");
           ////////////////////////     if (dr1.Length == 0)
           ////////////////////////     {
           ////////////////////////         //ed.WriteMessage("It Is For Delete:Code= " + dr["Code"] + "\n");
           ////////////////////////         Atend.Base.Acad.AcadGlobal.PoleData.dPackageForDelete.Add(dr["Code"].ToString());
           ////////////////////////     }

           //////////////////////// }
           //////////////////////// //ed.WriteMessage("Atend.Base.Acad.AcadGlobal.dPackageForDelete.Count= " + Atend.Base.Acad.AcadGlobal.dPackageForDelete.Count+"\n");
           //////////////////////// //ed.WriteMessage("#############################################################\n");
           //////////////////////// //ed.WriteMessage(string.Format("Grid row count is : {0} \n", gvPoleConsol.Rows.Count));
            
           //////////////////////// //for (int rowCounter = 0; rowCounter < gvPoleConsol.Rows.Count; rowCounter++)
           //////////////////////// //{
           //////////////////////// //    Atend.Base.Design.DPackage tempDPackage = new Atend.Base.Design.DPackage();
           //////////////////////// //    DataGridViewCheckBoxCell chkBoxCell = (DataGridViewCheckBoxCell)gvPoleConsol.Rows[rowCounter].Cells[3];
           //////////////////////// //    tempDPackage.IsExistance =Convert.ToBoolean(chkBoxCell.Value);
           //////////////////////// //    tempDPackage.Count = 1;
           //////////////////////// //    tempDPackage.Type = (int)Atend.Control.Enum.ProductType.Consol;
           //////////////////////// //    tempDPackage.ProductCode = Convert.ToInt32(gvPoleConsol.Rows[rowCounter].Cells[0].Value);
           //////////////////////// //    Atend.Base.Acad.AcadGlobal.dPackages.Add(tempDPackage);
           //////////////////////// //}
           //////////////////////// //ed.WriteMessage("Count DPackage= " + Atend.Base.Acad.AcadGlobal.dPackages.Count.ToString() + "\n");


           //////////////////////// //add operation rocords here
           //////////////////////// //System.Data.DataTable operatioTable = Atend.Base.Equipment.EOperation.SelectByProductCode
           //////////////////////// //    (Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value));
           //////////////////////// //foreach (DataRow row2 in operatioTable.Rows)
           //////////////////////// //{
           //////////////////////// //    Atend.Base.Design.DPackage tempDPackage = new Atend.Base.Design.DPackage();
           //////////////////////// //    tempDPackage.Count = 1;
           //////////////////////// //    tempDPackage.Type = (int)Atend.Control.Enum.ProductType.Operation;
           //////////////////////// //    tempDPackage.ProductCode = Convert.ToInt32(row2["ProductId"]);
           //////////////////////// //    Atend.Base.Acad.AcadGlobal.dPackages.Add(tempDPackage);
           //////////////////////// //}
           //////////////////////// //ed.WriteMessage("Count DPackage= " + myAcadDrawPole.MyDPackages.Count.ToString() + "\n");
           //////////////////////// //Text = myAcadDrawPole.MyDPackages.Count.ToString();
           //////////////////////// //using (DocumentLock dockLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
           //////////////////////// //{
           //////////////////////// //    myAcadDrawPole.SavePoleInformation();
           //////////////////////// //}
           //////////////////////// //---------------------------------------------------------
        }

        private void BindDataToHalterType()
        {
            cboHalterType.DataSource = Atend.Base.Base.BProduct.SelectByType((int)Atend.Control.Enum.ProductType.Halter);
            cboHalterType.DisplayMember = "Name";
            cboHalterType.ValueMember = "Id";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            double height = -1;
            double power = -1;
            int type = -1;
            if (chkHeight.Checked)
                height = Convert.ToDouble(nudHeight.Value);
            if (chkPower.Checked)
                power = Convert.ToDouble(nudPower.Value);
            if (chkType.Checked)
                type = Convert.ToInt32(cboType.SelectedValue);
            gvPole.AutoGenerateColumns = false;
            gvPole.DataSource = Atend.Base.Equipment.EPole.DrawSearch(height, power, type);
        }

        private void BindDataToConsolTip()
        {
            gvConsolsTip.AutoGenerateColumns = false;
            gvConsolsTip.DataSource = Atend.Base.Equipment.EConsol.SelectByType(cboConsolType.SelectedIndex);
        }

        private void frmDrawPole01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ////////////////BindDataToHalterType();

            ////////////////if (cboConsolType.Items.Count > 0)
            ////////////////{
            ////////////////    cboConsolType.SelectedIndex = cboConsolType.Items.Count - 1;
            ////////////////}

            ////////////////if (cboHalterType.Items.Count > 0)
            ////////////////{
            ////////////////    cboHalterType.SelectedIndex = cboHalterType.Items.Count - 1;
            ////////////////}

            ////////////////BindDataToConsolTip();
            ////////////////BindDataToComboBox();
            ////////////////BindDataToOwnControl();
            ////////////////Atend.Base.Acad.AcadGlobal.PoleData.dPackageForDelete.Clear();
            ////////////////Atend.Base.Acad.AcadGlobal.PoleData.dPackages.Clear();
            ////////////////Atend.Base.Acad.AcadGlobal.PoleData.dNode=new Atend.Base.Design.DNode();
            ////////////////Atend.Base.Acad.AcadGlobal.PoleData.dConsol = new Atend.Base.Design.DConsol();
        }

        private void gvPole_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvPole_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gvPole_Click(object sender, EventArgs e)
        {
            if (gvPole.Rows.Count > 0)
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.SelectByCode(Convert.ToInt32(
                    gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value));
                //Extra
                //Atend.Base.Equipment.EPoleType poleType = Atend.Base.Equipment.EPoleType.SelectByCode(pole.Type);
                //Atend.Base.Acad.AcadGlobal.dPoleInfo.Factor = Convert.ToByte(txtCountPole.Text);

                txtBottomCrossSectionArea.Text = pole.ButtomCrossSectionArea.ToString();

                txtHeight.Text = pole.Height.ToString();
                txtPower.Text = pole.Power.ToString();
                txtTopCrossSectionArea.Text = pole.TopCrossSectionArea.ToString();
                //Extra
                //txtType.Text = poleType.Name;

                //DataTable dtblock = Atend.Base.Base.BProductBlock.SelectByProductIdType(
                //    Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value),
                //    (int)Atend.Control.Enum.ProductType.Pole);
                //SelectProductBlock();
            }
        }

        //private void SelectProductBlock()
        //{
        //    DataTable dtblock = Atend.Base.Base.BProductBlock.SelectByProductIdType(
        // Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value),
        // (int)Atend.Control.Enum.ProductType.Pole);
        //    Atend.Control.Common.EquipmentType = Atend.Control.Enum.EquipmentType.Node;
        //    for (int i = 0; i < dtblock.Rows.Count; i++)
        //    {
        //        if (chkIsExistance.Checked)
        //        {
        //            if (Convert.ToBoolean(dtblock.Rows[i]["IsExistance"]))
        //            {

        //                //nodeInformation.ProductBlockId = Convert.ToInt64(dtblock.Rows[i]["Code"]);
        //                //nodeInformation.ProductBlockName = dtblock.Rows[i]["BlockId"].ToString();
        //                //nodeInformation.ProductCode = Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value);

        //                Atend.Base.Acad.AcadGlobal.dNode.ProductBlockCode = Convert.ToInt32(dtblock.Rows[i]["Code"]);


        //            }
        //        }
        //        else
        //        {
        //            if (!Convert.ToBoolean(dtblock.Rows[i]["IsExistance"]))
        //            {

        //                //nodeInformation.ProductBlockId = Convert.ToInt64(dtblock.Rows[i]["Code"]);
        //                //nodeInformation.ProductBlockName = dtblock.Rows[i]["BlockId"].ToString();
        //                //nodeInformation.ProductCode = Convert.ToInt32(gvPole.Rows[gvPole.CurrentRow.Index].Cells[0].Value);

        //                Atend.Base.Acad.AcadGlobal.dNode.ProductBlockCode = Convert.ToInt32(dtblock.Rows[i]["Code"]);

        //            }
        //        }
        //    }

        //}

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gvConsolsTip.DataSource;
            dt.DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", txtSearch.Text);
        }

        private void gvConsolsTip_Click(object sender, EventArgs e)
        {

            //tvConsolSubEquipment.Nodes.Clear();

            //int Type = (int)Atend.Control.Enum.ProductType.Consol;
            //int ContainerCode = Convert.ToInt32(gvConsolsTip.SelectedRows[0].Cells[0].Value);

            //ed.WriteMessage("Row Code is : " + ContainerCode + "\n");
            //Atend.Base.Equipment.EContainerPackage ContainerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(ContainerCode, Type);
            //ed.WriteMessage("Row Code in EContainerPackage is : " + ContainerPackage.Code + "\n");


            //DataTable ProductPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCode(ContainerPackage.Code);
            //ed.WriteMessage(string.Format("RowCount in productPackage : {0} \n", ProductPackageTable.Rows.Count));

            //foreach (DataRow row in ProductPackageTable.Rows)
            //{

            //    #region find each row TableType
            //    byte TableType = Convert.ToByte(row["TableType"]);
            //    #endregion
            //    ed.WriteMessage(string.Format("TableType : {0} \n", TableType));

            //    #region search in XML for Table of TableType value
            //    string Table = DetermineTableValue(TableType);
            //    #endregion

            //    ed.WriteMessage(string.Format("Table : {0} \n", Table));

            //    if (Table == "Self")
            //    {
            //        switch ((Atend.Control.Enum.ProductType)TableType)
            //        {
            //            case Atend.Control.Enum.ProductType.Insulator:

            //                ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
            //                Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.SelectByCode(Convert.ToInt32(row["ProductCode"]));
            //                tvConsolSubEquipment.Nodes.Add(insulator.Name);

            //                break;
            //            case Atend.Control.Enum.ProductType.InsulatorChain:

            //                break;
            //            case Atend.Control.Enum.ProductType.InsulatorPipe:
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        ed.WriteMessage("second productCode : " + Convert.ToInt32(row["ProductCode"]).ToString());
            //        Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(Convert.ToInt32(row["ProductCode"]));
            //        tvConsolSubEquipment.Nodes.Add(product.Name);
            //    }
            //}

            /////////////////////////////////////////////////////////////////////

            if (gvConsolsTip.Rows.Count > 0)
            {
                tvConsolSubEquipment.Nodes.Clear();

                int Type = (int)Atend.Control.Enum.ProductType.Consol;
                int ContainerCode = Convert.ToInt32(gvConsolsTip.SelectedRows[0].Cells[0].Value);

                //ed.WriteMessage("Row Code is : " + ContainerCode + "\n");
                //Atend.Base.Equipment.EContainerPackage ContainerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(ContainerCode, Type);
                //ed.WriteMessage("Row Code in EContainerPackage is : " + ContainerPackage.Code + "\n");


                DataTable ProductPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerCodeAndType(ContainerCode, Type);
                //ed.WriteMessage(string.Format("RowCount in productPackage : {0} \n", ProductPackageTable.Rows.Count));

                foreach (DataRow row in ProductPackageTable.Rows)
                {

                    #region find each row TableType
                    byte TableType = Convert.ToByte(row["TableType"]);
                    #endregion
                    //ed.WriteMessage(string.Format("TableType : {0} \n", TableType));

                    #region search in XML for Table of TableType value
                    string Table = DetermineTableValue(TableType);
                    #endregion

                    //ed.WriteMessage(string.Format("Table : {0} \n", Table));

                    if (Table == "Self")
                    {
                        switch ((Atend.Control.Enum.ProductType)TableType)
                        {


                            case Atend.Control.Enum.ProductType.Insulator:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(insulator.Name);

                                break;

                            case Atend.Control.Enum.ProductType.AirPost:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EAirPost airPost = Atend.Base.Equipment.EAirPost.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(airPost.Name);
                                break;
                            //case Atend.Control.Enum.ProductType.AuoKey3p:

                            //    //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                            //    Atend.Base.Equipment.EAutoKey_3p autokey3p = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                            //    tvConsolSubEquipment.Nodes.Add(autokey3p.Name);
                            //    break;

                            case Atend.Control.Enum.ProductType.Breaker:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EBreaker breaker = Atend.Base.Equipment.EBreaker.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(breaker.Name);
                                break;
                            //case Atend.Control.Enum.ProductType.Bus:

                            //    ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                            //    Atend.Base.Equipment.EBus bus = Atend.Base.Equipment.EBus.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                            //    tvConsolSubEquipment.Nodes.Add(conductor.Name);
                            //    break;
                            case Atend.Control.Enum.ProductType.GroundCabel:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EGroundCabel cabel = Atend.Base.Equipment.EGroundCabel.AccessSelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(cabel.Name);
                                break;
                            case Atend.Control.Enum.ProductType.CatOut:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.ECatOut catout = Atend.Base.Equipment.ECatOut.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(catout.Name);
                                break;
                            case Atend.Control.Enum.ProductType.Conductor:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(conductor.Name);
                                break;
                            case Atend.Control.Enum.ProductType.Consol:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(consol.Name);
                                break;
                            //case Atend.Control.Enum.ProductType.Countor:

                            //    ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                            //    Atend.Base.Equipment.ECountor counter = Atend.Base.Equipment.ECountor.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                            //    tvConsolSubEquipment.Nodes.Add(counter.Name);
                            //    break;
                            //case Atend.Control.Enum.ProductType.CT:

                            //    ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                            //    Atend.Base.Equipment.ECT ct = Atend.Base.Equipment.ECT.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                            //    tvConsolSubEquipment.Nodes.Add(ct.Name);
                            //    break;
                            //case Atend.Control.Enum.ProductType.DB:

                            //    ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                            //    Atend.Base.Equipment.EDB db = Atend.Base.Equipment.EDB.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                            //    tvConsolSubEquipment.Nodes.Add(db.Name);
                            //    break;
                            case Atend.Control.Enum.ProductType.Disconnector:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EDisconnector disconnector = Atend.Base.Equipment.EDisconnector.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(disconnector.Name);
                                break;
                            case Atend.Control.Enum.ProductType.GroundPost:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(groundPost.Name);
                                break;
                            case Atend.Control.Enum.ProductType.HeaderCabel:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EHeaderCabel headerCabel = Atend.Base.Equipment.EHeaderCabel.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(headerCabel.Name);
                                break;
                            case Atend.Control.Enum.ProductType.Jumper:
//Extra
                                ////ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                //Atend.Base.Equipment.EJumper jumper = Atend.Base.Equipment.EJumper.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                //tvConsolSubEquipment.Nodes.Add(jumper.Name);
                                //break;
                            case Atend.Control.Enum.ProductType.Khazan:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EKhazan khzan = Atend.Base.Equipment.EKhazan.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(khzan.Name);
                                break;
                            case Atend.Control.Enum.ProductType.Mafsal:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EMafsal mafsal = Atend.Base.Equipment.EMafsal.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(mafsal.Name);
                                break;
                            //case Atend.Control.Enum.ProductType.PhotoCell:

                            //    ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                            //    Atend.Base.Equipment.EPhotoCell phtoCell = Atend.Base.Equipment.EPhotoCell.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                            //    tvConsolSubEquipment.Nodes.Add(phtoCell.Name);
                            //    break;
                            case Atend.Control.Enum.ProductType.Pole:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(pole.Name);
                                break;
                            //case Atend.Control.Enum.ProductType.PT:

                            //    ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                            //    Atend.Base.Equipment.EPT pt = Atend.Base.Equipment.EPT.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                            //    tvConsolSubEquipment.Nodes.Add(pt.Name);
                            //    break;
                            //case Atend.Control.Enum.ProductType.ReCloser:

                            //    ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                            //    Atend.Base.Equipment.EReCloser recloser = Atend.Base.Equipment.EReCloser.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                            //    tvConsolSubEquipment.Nodes.Add(recloser.Name);
                            //    break;
                            case Atend.Control.Enum.ProductType.Rod:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.ERod rod = Atend.Base.Equipment.ERod.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(rod.Name);
                                break;
                            //case Atend.Control.Enum.ProductType.SectionLizer:

                            //    ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                            //    Atend.Base.Equipment.ESectionLizer sectionLizer = Atend.Base.Equipment.ESectionLizer.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                            //    tvConsolSubEquipment.Nodes.Add(sectionLizer.Name);
                            //    break;
                            case Atend.Control.Enum.ProductType.StreetBox:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.EStreetBox streetBox = Atend.Base.Equipment.EStreetBox.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(streetBox.Name);
                                break;
                            case Atend.Control.Enum.ProductType.Transformer:

                                //ed.WriteMessage("first productCode : " + Convert.ToInt32(row["ProductCode"]));
                                Atend.Base.Equipment.ETransformer transform = Atend.Base.Equipment.ETransformer.SelectByCode(Convert.ToInt32(row["ProductCode"]));
                                tvConsolSubEquipment.Nodes.Add(transform.Name);
                                break;

                            case Atend.Control.Enum.ProductType.InsulatorChain:

                                break;
                            case Atend.Control.Enum.ProductType.InsulatorPipe:
                                break;
                        }
                    }
                    else
                    {
                        //ed.WriteMessage("second productCode : " + Convert.ToInt32(row["ProductCode"]).ToString());
                        Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(Convert.ToInt32(row["ProductCode"]));
                        tvConsolSubEquipment.Nodes.Add(product.Name);
                    }
                }
            }

            /////////////////////////////////////////////////////////////////////



        }

        private void btnNewConsolTip_Click(object sender, EventArgs e)
        {
            //Atend.Equipment.frmConsol FrmConsol = new Atend.Equipment.frmConsol();
            //FrmConsol.ShowDialog();
            //BindDataToConsolTip();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (gvConsolsTip.SelectedRows.Count > 0)
            {
                gvPoleConsol.Rows.Add();
                gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[0].Value = gvConsolsTip.SelectedRows[0].Cells[0].Value;
                gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[1].Value = gvConsolsTip.SelectedRows[0].Cells[1].Value;
                gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[4].Value = "_";
                if (gvConsolsTip.SelectedRows[0].Cells[2].Value.ToString() == "0")
                {
                    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[2].Value = "کششی";
                }
                if (gvConsolsTip.SelectedRows[0].Cells[2].Value.ToString() == "1")
                {
                    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[2].Value = "انتهایی";
                }
                if (gvConsolsTip.SelectedRows[0].Cells[2].Value.ToString() == "2")
                {
                    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[2].Value = "عبوری";
                }
                if (gvConsolsTip.SelectedRows[0].Cells[2].Value.ToString() == "3")
                {
                    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[2].Value = "DP";
                }
            }
        }

        private string DetermineTableValue(int Code)
        {
            XmlDocument _xmlDoc = new XmlDocument();
            System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            string fullPath = m.FullyQualifiedName;
            try
            {
                fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
            }
            catch
            {
            }

            string xmlPath = fullPath + "\\EquipmentName.xml";
            _xmlDoc.Load(xmlPath);
            foreach (XmlElement xElement in _xmlDoc.DocumentElement)
            {
                foreach (XmlNode xnode in xElement.ChildNodes)
                {
                    if (xnode.Attributes["Code"].Value == Code.ToString())
                    {
                        return xnode.Attributes["Table"].Value;
                    }
                }
            }
            return "";
        }

        private void cboConsolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvConsolsTip.AutoGenerateColumns = false;
            gvConsolsTip.DataSource = Atend.Base.Equipment.EConsol.SelectByType(cboConsolType.SelectedIndex);

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (gvPoleConsol.Rows.Count == 0)
            {
                MessageBox.Show("لطفاً کنسول های پایه را انتخاب کنید", "خطا");
                return;
            }
            else
                if (gvPoleConsol.Rows.Count > 4)
                {
                    MessageBox.Show("شما مجاز به انتخاب حداکثر 4 کنسول برای این پایه هستید", "خطا");
                    return;
                }


            if (Validation())
            {
                Save();

                AllowClose = true;

            }
            else
            {
                //ed.WriteMessage("Don't Go to _POLE command.\n");
            }
        }

        private void chkIsExistance_CheckedChanged(object sender, EventArgs e)
        {
            //SelectProductBlock();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            AllowClose = true;

            Close();
        }

        private void gvConsolsTip_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmDrawPole01_FormClosing(object sender, FormClosingEventArgs e)
        {


            if (!AllowClose)
            {
                e.Cancel = true;
            }


        }

        private void cboHalterType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gvPoleConsol_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void gvPoleConsol_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvPoleConsol.SelectedRows.Count > 0)
            {
                gvPoleConsol.Rows.RemoveAt(gvPoleConsol.SelectedRows[0].Index);
            }

        }

        public void BindDataToOwnControl()
        {
            // BindDataTofeathurePole

            //ed.WriteMessage("DesignCode=" + Atend.Control.Common.SelectedDesignCode.ToString() + "\n");
            Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode(NodeCode);
            //ed.WriteMessage("myNode.Code= " + myNode.Code.ToString() + "\n");

            Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCodeType(myNode.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
            //ed.WriteMessage("myPackage.ProductCode= " + myPackage.ProductCode.ToString() + "\n");

            Atend.Base.Equipment.EPole myPole = Atend.Base.Equipment.EPole.AccessSelectByCode(myNode.ProductCode);
            //Extra
            //Atend.Base.Equipment.EPoleType myPoleType = Atend.Base.Equipment.EPoleType.SelectByCode(myPole.Type);
            //ed.WriteMessage("myPole.Height.ToString()" + myPole.Height.ToString() + "\n");
            gvPole.AutoGenerateColumns = false;
            gvPole.DataSource = Atend.Base.Equipment.EPole.DrawSearch(-1, -1, -1);
            for (int i = 0; i < gvPole.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvPole.Rows[i].Cells[0].Value.ToString()) == myNode.ProductCode)
                {
                    gvPole.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            //Extra
            //txtType.Text = myPoleType.Name;
            txtTopCrossSectionArea.Text = myPole.TopCrossSectionArea.ToString();
            txtPower.Text = myPole.Power.ToString();
            txtHeight.Text = myPole.Height.ToString();
            txtBottomCrossSectionArea.Text = myPole.ButtomCrossSectionArea.ToString();
            //ed.WriteMessage("Finish BindDataTofeathurePole\n");
            //****************************

            //BindDataToHalterAndPoleTypeAndConsolType
            Atend.Base.Design.DPoleInfo myPoleInfo = Atend.Base.Design.DPoleInfo.AccessSelectByNodeCode(myNode.Code);

            //ed.WriteMessage("myPoleInfo.HalterType:=" + myPoleInfo.HalterType.ToString() + "\n");
            //ed.WriteMessage("myPoleInfo.HalterCount:=" + myPoleInfo.HalterCount.ToString());

            cboHalterType.SelectedValue = myPoleInfo.HalterType;

            nudHalter.Value = myPoleInfo.HalterCount;

            //string poleType = myPoleInfo.PoleType.ToString();
            //if (poleType.EndsWith("1"))
            //{
            //    chkCross.Checked = true;
            //}

            //if (poleType.EndsWith("2"))
            //{
            //    chkEnding.Checked = true;
            //}

            //if (poleType.EndsWith("4"))
            //{
            //    chkPulling.Checked = true;
            //}
            //ed.WriteMessage("FinishBindDataToHalterAndPoleTypeAndConsolType\n");
            //*****************************

            //BindDataToConsol

            DataTable dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));

            foreach (DataRow drPackage in dtPackage.Rows)
            {
                //ed.WriteMessage("ProductCode= " + drPackage["ProductCode"].ToString() + "\n");
                Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.SelectByCodeForDesign(Convert.ToInt32(drPackage["ProductCode"]));
                gvPoleConsol.Rows.Add();
                //ed.WriteMessage("Code= "+consol.Code+"Name= "+consol.Name+"\n");
                gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[0].Value = consol.Code.ToString();
                gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[1].Value = consol.Name;
                //ed.WriteMessage("as\n");
                if (consol.ConsolType == 0)
                {
                    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[2].Value = "کششی";
                }
                if (consol.ConsolType == 1)
                {
                    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[2].Value = "انتهایی";
                }
                if (consol.ConsolType == 2)
                {
                    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[2].Value = "عبوری";
                }
                if (consol.ConsolType == 3)
                {
                    gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[2].Value = "DP";
                }
                gvPoleConsol.Rows[gvPoleConsol.Rows.Count - 1].Cells[4].Value = drPackage["Code"].ToString();
            }
            //ed.WriteMessage("FinishBindDataToconsol\n");
            //*****************************


        }
    }
}