using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmGetFromServer : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        bool ForceToClose = false;

        public frmGetFromServer()
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

        int Type = 0;
        DataTable Table = new DataTable();

        private void frmGetFromServer_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            tvEquipment.CheckBoxes = false;
            Atend.Global.Utility.UBinding.BindDataToTreeViewForGetFromServer(tvEquipment);
        }

        private void tvEquipment_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtName.Text = string.Empty;
            Type = Convert.ToInt32(e.Node.Tag);

            //MessageBox.Show(Convert.ToInt32(e.Node.Tag).ToString());
            
            #region switch case part
            switch ((Atend.Control.Enum.ProductType)(Convert.ToInt32(e.Node.Tag)))
            {
                case Atend.Control.Enum.ProductType.AirPost:
                    Table = Atend.Base.Equipment.EAirPost.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.AuoKey3p:
                    Table = Atend.Base.Equipment.EAutoKey_3p.SelectAll();
                    break;

                //case Atend.Control.Enum.ProductType.Branch:
                //    break;

                case Atend.Control.Enum.ProductType.BankKhazan:
                    Table = Atend.Base.Equipment.EKhazanTip.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Breaker:
                    Table = Atend.Base.Equipment.EBreaker.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Bus:
                    Table = Atend.Base.Equipment.EBus.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.GroundCabel:
                    Table = Atend.Base.Equipment.EGroundCabel.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.GroundCabelTip:
                    Table = Atend.Base.Equipment.EGroundCabelTip.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.CatOut:
                    Table = Atend.Base.Equipment.ECatOut.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Cell:
                    Table = Atend.Base.Equipment.ECell.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Conductor:
                    Table = Atend.Base.Equipment.EConductor.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.ConductorTip:
                    Table = Atend.Base.Equipment.EConductorTip.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.SelfKeeper:
                    Table = Atend.Base.Equipment.ESelfKeeper.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Consol:

                    Table = Atend.Base.Equipment.EConsol.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Countor:
                    Table = Atend.Base.Equipment.ECountor.SelectAll();
                    break;
                                    
                case Atend.Control.Enum.ProductType.CT:
                    Table = Atend.Base.Equipment.ECT.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.DB:
                    Table = Atend.Base.Equipment.EDB.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Floor:
                    Table = Atend.Base.Equipment.EFloor.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Disconnector:
                    Table = Atend.Base.Equipment.EDisconnector.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Mafsal:
                    Table = Atend.Base.Equipment.EMafsal.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.MiniatureKey:
                    Table = Atend.Base.Equipment.EMiniatorKey.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.GroundPost:
                    Table = Atend.Base.Equipment.EGroundPost.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Halter:
                    Table = Atend.Base.Equipment.EHalter.SelectAll();
                    break;
                case Atend.Control.Enum.ProductType.HeaderCabel:
                    Table = Atend.Base.Equipment.EHeaderCabel.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Insulator:
                    Table = Atend.Base.Equipment.EInsulator.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.KablSho:
                    Table = Atend.Base.Equipment.EKablsho.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Kalamp:
                    Table = Atend.Base.Equipment.EClamp.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                    //ed.writeMessage("ProductType= " + Atend.Control.Enum.ProductType.MiddleJackPanel.ToString()+"\n");
                    Table = Atend.Base.Equipment.EJAckPanel.SelectAll();
                    break;

                ////////case Atend.Control.Enum.ProductType.Jumper:
                ////////    DataTable JumperTable = Atend.Base.Equipment.EJumper.SelectAll();
                ////////    foreach (DataRow row in JumperTable.Rows)
                ////////    {
                ////////        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                ////////        ChildNode.Tag = row["Code"].ToString();
                ////////        ChildNode.Name = RootTreeNode.Tag.ToString();
                ////////        //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                ////////        RootTreeNode.Nodes.Add(ChildNode);
                ////////    }

                ////////    break;
                case Atend.Control.Enum.ProductType.Khazan:
                    Table = Atend.Base.Equipment.EKhazan.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.PhotoCell:
                    Table = Atend.Base.Equipment.EPhotoCell.SelectAll();
                    break;

                

                case Atend.Control.Enum.ProductType.Phuse:
                    Table = Atend.Base.Equipment.EPhuse.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.PhuseKey:
                    Table = Atend.Base.Equipment.EPhuseKey.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.PhusePole:
                    Table = Atend.Base.Equipment.EPhusePole.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Pole:
                    Table = Atend.Base.Equipment.EPole.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.PoleTip:
                    Table = Atend.Base.Equipment.EPoleTip.SelectAll();
                    break;

                //case Atend.Control.Enum.ProductType.Post:
                //    DataTable PostTable = Atend.Base.Equipment.EPost.SelectAll();
                //    foreach (DataRow row in PostTable.Rows)
                //    {
                //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                //        ChildNode.Tag = row["Code"].ToString();
                //        ChildNode.Name = RootTreeNode.Tag.ToString();
                //        RootTreeNode.Nodes.Add(ChildNode);
                //    }

                //    break;
                case Atend.Control.Enum.ProductType.PT:
                    Table = Atend.Base.Equipment.EPT.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.ReCloser:
                    Table = Atend.Base.Equipment.EReCloser.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Rod:
                    Table = Atend.Base.Equipment.ERod.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.SectionLizer:
                    Table = Atend.Base.Equipment.ESectionLizer.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.SelfKeeperTip:
                    Table = Atend.Base.Equipment.ESelfKeeperTip.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.StreetBox:
                    Table = Atend.Base.Equipment.EStreetBox.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Transformer:
                    Table = Atend.Base.Equipment.ETransformer.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.InsulatorPipe:
                    Table = Atend.Base.Equipment.EInsulatorPipe.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.InsulatorChain:
                    Table = Atend.Base.Equipment.EInsulatorChain.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.WeekJackPanel:
                    Table = Atend.Base.Equipment.EJackPanelWeek.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Light:
                    Table = Atend.Base.Equipment.ELight.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Ground:
                    Table = Atend.Base.Equipment.EGround.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.MeasuredJackPanel:
                    Table = Atend.Base.Equipment.EMeasuredJackPanel.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Arm:
                    Table = Atend.Base.Equipment.EArm.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Ramp:
                    Table = Atend.Base.Equipment.ERamp.SelectAll();
                    break;

                case Atend.Control.Enum.ProductType.Prop:
                    Table = Atend.Base.Equipment.EProp.SelectAll();
                    break;

                //default:
                //    MessageBox.Show(e.Node.Tag.ToString());
                //    break;

            }
            #endregion


            gvEquipment.AutoGenerateColumns = false;
            gvEquipment.DataSource = Table;
            gvEquipment.AutoGenerateColumns = false;
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbGetFromServer_Click(object sender, EventArgs e)
        {
            bool Result =false;

            if (Type == 0)
            {
                MessageBox.Show("لطفاً تجهیز مورد نظر را انتخاب کنید");
                return ;
            }

            if (gvEquipment.SelectedRows.Count < 1)
            {
                MessageBox.Show("لطفاً تجهیز مورد نظر برای دریافت از سرور را انتخاب کنید");
                return;
            }

            //MessageBox.Show(gvEquipment.SelectedRows[0].Cells[0].Value.ToString());

            #region switch case part
            switch ((Atend.Control.Enum.ProductType)Type)
            {
                case Atend.Control.Enum.ProductType.AirPost:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Arm:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                case Atend.Control.Enum.ProductType.AuoKey3p:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                ////case Atend.Control.Enum.ProductType.Branch:
                ////    break;

                case Atend.Control.Enum.ProductType.BankKhazan:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                case Atend.Control.Enum.ProductType.Breaker:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Bus:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;


                case Atend.Control.Enum.ProductType.GroundCabel:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.GroundCabelTip:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                case Atend.Control.Enum.ProductType.CatOut:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Conductor:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.ConductorTip:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                case Atend.Control.Enum.ProductType.SelfKeeper:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Consol:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Countor:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Cell:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.CT:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.DB:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Floor:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Disconnector:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Mafsal:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;


                case Atend.Control.Enum.ProductType.MiniatureKey:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.GroundPost:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Halter:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;
                case Atend.Control.Enum.ProductType.HeaderCabel:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Insulator:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.KablSho:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Kalamp:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                //////////case Atend.Control.Enum.ProductType.Jumper:
                //////////    DataTable JumperTable = Atend.Base.Equipment.EJumper.SelectAll();
                //////////    foreach (DataRow row in JumperTable.Rows)
                //////////    {
                //////////        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                //////////        ChildNode.Tag = row["Code"].ToString();
                //////////        ChildNode.Name = RootTreeNode.Tag.ToString();
                //////////        //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                //////////        RootTreeNode.Nodes.Add(ChildNode);
                //////////    }

                //////////    break;
                case Atend.Control.Enum.ProductType.Khazan:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.PhotoCell:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Phuse:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.PhuseKey:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.PhusePole:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Pole:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Prop:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                case Atend.Control.Enum.ProductType.PoleTip:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                ////case Atend.Control.Enum.ProductType.Post:
                ////    DataTable PostTable = Atend.Base.Equipment.EPost.SelectAll();
                ////    foreach (DataRow row in PostTable.Rows)
                ////    {
                ////        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                ////        ChildNode.Tag = row["Code"].ToString();
                ////        ChildNode.Name = RootTreeNode.Tag.ToString();
                ////        RootTreeNode.Nodes.Add(ChildNode);
                ////    }

                ////    break;
                case Atend.Control.Enum.ProductType.PT:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Ramp:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                case Atend.Control.Enum.ProductType.ReCloser:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Rod:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.SectionLizer:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.SelfKeeperTip:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                case Atend.Control.Enum.ProductType.StreetBox:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Transformer:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.InsulatorChain:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.InsulatorPipe:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()), Type);
                    break;

                case Atend.Control.Enum.ProductType.WeekJackPanel:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Light:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.Ground:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

                case Atend.Control.Enum.ProductType.MeasuredJackPanel:
                    Result = Atend.Base.Equipment.EContainerPackage.GetFromServer(Convert.ToInt32(gvEquipment.SelectedRows[0].Cells[0].Value.ToString()),Type);
                    break;

            }
            #endregion

            if (Result)
                MessageBox.Show("عملیات دریافت از سرور با موفقیت انجام شد");
            else
                MessageBox.Show("خطا در عملیات دریافت از سرور");


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            #region switch case part
            switch ((Atend.Control.Enum.ProductType)(Type))
            {
                case Atend.Control.Enum.ProductType.AirPost:
                    Table = Atend.Base.Equipment.EAirPost.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.AuoKey3p:
                    Table = Atend.Base.Equipment.EAutoKey_3p.Search(txtName.Text);
                    break;

                //case Atend.Control.Enum.ProductType.Branch:
                //    break;
                case Atend.Control.Enum.ProductType.Breaker:
                    Table = Atend.Base.Equipment.EBreaker.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Bus:
                    Table = Atend.Base.Equipment.EBus.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.GroundCabel:
                    Table = Atend.Base.Equipment.EGroundCabel.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.GroundCabelTip:
                    Table = Atend.Base.Equipment.EGroundCabelTip.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.CatOut:
                    Table = Atend.Base.Equipment.ECatOut.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Conductor:
                    Table = Atend.Base.Equipment.EConductor.Search(txtName.Text ,false);
                    break;

                case Atend.Control.Enum.ProductType.ConductorTip:
                    Table = Atend.Base.Equipment.EConductorTip.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.SelfKeeper:
                    Table = Atend.Base.Equipment.ESelfKeeper.Search(txtName.Text , false);
                    break;

                case Atend.Control.Enum.ProductType.Consol:

                    Table = Atend.Base.Equipment.EConsol.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Countor:
                    Table = Atend.Base.Equipment.ECountor.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.CT:
                    Table = Atend.Base.Equipment.ECT.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.DB:
                    Table = Atend.Base.Equipment.EDB.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Floor:
                    Table = Atend.Base.Equipment.EFloor.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Disconnector:
                    Table = Atend.Base.Equipment.EDisconnector.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Mafsal:
                    Table = Atend.Base.Equipment.EMafsal.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.MiniatureKey:
                    Table = Atend.Base.Equipment.EMiniatorKey.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.GroundPost:
                    Table = Atend.Base.Equipment.EGroundPost.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Halter:
                    Table = Atend.Base.Equipment.EHalter.Search(txtName.Text);
                    break;
                case Atend.Control.Enum.ProductType.HeaderCabel:
                    Table = Atend.Base.Equipment.EHeaderCabel.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Insulator:
                    Table = Atend.Base.Equipment.EInsulator.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.KablSho:
                    Table = Atend.Base.Equipment.EKablsho.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Kalamp:
                    Table = Atend.Base.Equipment.EClamp.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                    //ed.writeMessage("ProductType= " + Atend.Control.Enum.ProductType.MiddleJackPanel.ToString()+"\n");
                    Table = Atend.Base.Equipment.EJAckPanel.Search(txtName.Text);
                    break;

                ////////case Atend.Control.Enum.ProductType.Jumper:
                ////////    DataTable JumperTable = Atend.Base.Equipment.EJumper.Search(textBox1.Text);
                ////////    foreach (DataRow row in JumperTable.Rows)
                ////////    {
                ////////        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                ////////        ChildNode.Tag = row["Code"].ToString();
                ////////        ChildNode.Name = RootTreeNode.Tag.ToString();
                ////////        //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                ////////        RootTreeNode.Nodes.Add(ChildNode);
                ////////    }

                ////////    break;
                case Atend.Control.Enum.ProductType.Khazan:
                    Table = Atend.Base.Equipment.EKhazan.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.BankKhazan:
                    Table = Atend.Base.Equipment.EKhazanTip.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.PhotoCell:
                    Table = Atend.Base.Equipment.EPhotoCell.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Cell:
                    Table = Atend.Base.Equipment.ECell.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Phuse:
                    Table = Atend.Base.Equipment.EPhuse.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.PhuseKey:
                    Table = Atend.Base.Equipment.EPhuseKey.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.PhusePole:
                    Table = Atend.Base.Equipment.EPhusePole.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Pole:
                    Table = Atend.Base.Equipment.EPole.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.PoleTip:
                    Table = Atend.Base.Equipment.EPoleTip.Search(txtName.Text);
                    break;

                //case Atend.Control.Enum.ProductType.Post:
                //    DataTable PostTable = Atend.Base.Equipment.EPost.Search(textBox1.Text);
                //    foreach (DataRow row in PostTable.Rows)
                //    {
                //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                //        ChildNode.Tag = row["Code"].ToString();
                //        ChildNode.Name = RootTreeNode.Tag.ToString();
                //        RootTreeNode.Nodes.Add(ChildNode);
                //    }

                //    break;
                case Atend.Control.Enum.ProductType.PT:
                    Table = Atend.Base.Equipment.EPT.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.ReCloser:
                    Table = Atend.Base.Equipment.EReCloser.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Rod:
                    Table = Atend.Base.Equipment.ERod.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.SectionLizer:
                    Table = Atend.Base.Equipment.ESectionLizer.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.SelfKeeperTip:
                    Table = Atend.Base.Equipment.ESelfKeeperTip.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.StreetBox:
                    Table = Atend.Base.Equipment.EStreetBox.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Transformer:
                    Table = Atend.Base.Equipment.ETransformer.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.InsulatorPipe:
                    Table = Atend.Base.Equipment.ETransformer.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.InsulatorChain:
                    Table = Atend.Base.Equipment.ETransformer.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.WeekJackPanel:
                    Table = Atend.Base.Equipment.EJackPanelWeek.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Light:
                    Table = Atend.Base.Equipment.ELight.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.Ground:
                    Table = Atend.Base.Equipment.EGround.Search(txtName.Text);
                    break;

                case Atend.Control.Enum.ProductType.MeasuredJackPanel:
                    Table = Atend.Base.Equipment.EMeasuredJackPanel.Search(txtName.Text);
                    break;

            }
            #endregion

            gvEquipment.AutoGenerateColumns = false;
            gvEquipment.DataSource = Table;

        }




    }
}