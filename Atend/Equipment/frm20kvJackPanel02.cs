using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Equipment
{
    public partial class frm20kvJackPanel02 : Form
    {
        public Guid selectedProductXCode = Guid.Empty;
        public byte selectedTypeCode = 0;
        //public int ProductCode = 0;
        public Guid SelectedJackPanelXCode = Guid.Empty;
        public string productName = string.Empty;
        public bool IsDefault = false;
        int Code = -1;
        bool ForceToClose = false;

        public frm20kvJackPanel02()
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

        private void BindDataToComboBox()
        {
            cboBus.DisplayMember = "Name";
            cboBus.ValueMember = "XCode";
            cboBus.DataSource = Atend.Base.Equipment.EBus.SelectAllX();
        }

        private void Reset()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SelectedJackPanelXCode = Guid.Empty;

            txtName.Text = string.Empty;
            txtComment.Text = string.Empty;
            nudCellCount.Value = 0;
            gvJackPanelCell.Rows.Clear();
            IsDefault = false;
            tsbIsDefault.Checked = false;
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


            int count = dataGridView.Rows.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                dataGridView.Rows.RemoveAt(i);
            }
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        public void BindDataToOwnControl(Guid Xcode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(code.ToString() + "\n");
            Atend.Base.Equipment.EJAckPanel jackPanel = Atend.Base.Equipment.EJAckPanel.SelectByXCode(Xcode);
            //ed.WriteMessage("Code:="+code.ToString());
            Atend.Control.Common.selectedProductCode = jackPanel.ProductCode;
            SelectProduct();

            SelectedJackPanelXCode = Xcode;
            txtName.Text = jackPanel.Name;
            txtComment.Text = jackPanel.Comment;
            cboBus.SelectedValue = jackPanel.MasterProductXCode;
            //ed.WriteMessage("Befor\n");
            nudCellCount.Value = jackPanel.CellCount;
            //ed.WriteMessage("After\n");
            tsbIsDefault.Checked = jackPanel.IsDefault;

            Code = jackPanel.Code;
            BindDataToGridViw(gvJackPanelCell);
            BindTreeViwAndGrid();
        }

        private void BindTreeViwAndGrid()
        {
            //EQUIPMENT
            ClearCheckAndGrid(tvEquipment, gvSelectedEquipment);
            gvSelectedEquipment.Refresh();
            for (int i = 0; i < Atend.Base.Equipment.EJAckPanel.nodeKeysEPackageX.Count; i++)
            {
                string s = Atend.Base.Equipment.EJAckPanel.nodeKeysEPackageX[i].ToString();
                foreach (TreeNode rootnode in tvEquipment.Nodes)
                {
                    foreach (TreeNode chileNode in rootnode.Nodes)
                    {
                        if (Atend.Base.Equipment.EJAckPanel.nodeTypeEPackageX[i].ToString() == chileNode.Name.ToString())
                        {
                            if (chileNode.Tag.ToString() == s)
                            {
                                rootnode.BackColor = Color.FromArgb(203, 214, 235);
                                chileNode.Checked = true;
                                gvSelectedEquipment.Rows.Add();
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[1].Value = chileNode.Text;
                                gvSelectedEquipment.Rows[gvSelectedEquipment.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeCountEPackageX[i].ToString();
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
            for (int i = 0; i < Atend.Base.Equipment.EJAckPanel.nodeKeys.Count; i++)
            {
                Atend.Base.Equipment.EOperation Operation = ((Atend.Base.Equipment.EOperation)Atend.Base.Equipment.EJAckPanel.nodeKeys[i]);
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

                        //gvSelectedProduct.Rows[gvSelectedProduct.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EPole.nodeCount[i].ToString();

                    }

                }
            }
        }

        private void BindDataToGridViw(DataGridView dataGridViw)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            gvJackPanelCell.Rows.Clear();
            //*******************************

            //*******************************
            for (int i = 0; i < Atend.Base.Equipment.EJAckPanel.nodeNumCell.Count; i++)
            {
                //ed.WriteMessage("nodeProductType" + Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString());
                //ed.WriteMessage("nodeProductCode" + Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString());

                int s = Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductType[i]);


                //EDIT
                gvJackPanelCell.Rows.Add();
                gvJackPanelCell.Rows[gvJackPanelCell.Rows.Count - 1].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                gvJackPanelCell.Rows[gvJackPanelCell.Rows.Count - 1].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                gvJackPanelCell.Rows[gvJackPanelCell.Rows.Count - 1].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                Atend.Base.Equipment.ECell cell = Atend.Base.Equipment.ECell.SelectByXCode(new Guid(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                gvJackPanelCell.Rows[gvJackPanelCell.Rows.Count - 1].Cells[3].Value = cell.Name.ToString(); ;






                //EDIT
                /*switch ((Atend.Control.Enum.ProductType)s)
                {
                    case Atend.Control.Enum.ProductType.Pole:
                        {
                            Atend.Base.Equipment.EPole db = Atend.Base.Equipment.EPole.SelectByCode((Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString())));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            ed.WriteMessage("AfterPole\n");
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();


                        }
                        break;
                    case Atend.Control.Enum.ProductType.AuoKey3p:
                        {
                            Atend.Base.Equipment.EAutoKey_3p db = Atend.Base.Equipment.EAutoKey_3p.SelectByCode((Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString())));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            ed.WriteMessage("AfterAuoKey3p\n");
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();


                        }
                        break;
                    case Atend.Control.Enum.ProductType.Conductor:
                        {
                            Atend.Base.Equipment.EConductor db = Atend.Base.Equipment.EConductor.SelectByCode((Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString())));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            ed.WriteMessage("AfterConductor\n");
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();


                        }
                        break;
                    case Atend.Control.Enum.ProductType.Breaker:
                        {
                            Atend.Base.Equipment.EBreaker db = Atend.Base.Equipment.EBreaker.SelectByCode((Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString())));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            ed.WriteMessage("AfterBreaker\n");
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();


                        }
                        break;
                    case Atend.Control.Enum.ProductType.Bus:
                        {
                            Atend.Base.Equipment.EBus db = Atend.Base.Equipment.EBus.SelectByCode((Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString())));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            ed.WriteMessage("AfterBu\n");
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();


                        }
                        break;

                    case Atend.Control.Enum.ProductType.CatOut:
                        {
                            Atend.Base.Equipment.ECatOut db = Atend.Base.Equipment.ECatOut.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.CT:
                        {
                            Atend.Base.Equipment.ECT db = Atend.Base.Equipment.ECT.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.DB:
                        {
                            Atend.Base.Equipment.EDB db = Atend.Base.Equipment.EDB.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.HeaderCabel :
                        {
                            Atend.Base.Equipment.EHeaderCabel db = Atend.Base.Equipment.EHeaderCabel.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;
                    case Atend.Control.Enum.ProductType.Disconnector:
                        {
                            Atend.Base.Equipment.EDisconnector db = Atend.Base.Equipment.EDisconnector.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;
                    case Atend.Control.Enum.ProductType.Rod:
                        {
                            Atend.Base.Equipment.ERod db = Atend.Base.Equipment.ERod.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;
                    case Atend.Control.Enum.ProductType.Countor:
                        {
                            Atend.Base.Equipment.ECountor db = Atend.Base.Equipment.ECountor.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;
                    case Atend.Control.Enum.ProductType.JackPanel:
                        {
                            Atend.Base.Equipment.EJAckPanel db = Atend.Base.Equipment.EJAckPanel.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;
                    case Atend.Control.Enum.ProductType.PhotoCell:
                        {
                            Atend.Base.Equipment.EPhotoCell db = Atend.Base.Equipment.EPhotoCell.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.Phuse:
                        {
                            Atend.Base.Equipment.EPhuse db = Atend.Base.Equipment.EPhuse.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.StreetBox:
                        {
                            Atend.Base.Equipment.EStreetBox db = Atend.Base.Equipment.EStreetBox.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.Transformer:
                        {
                            Atend.Base.Equipment.ETransformer db = Atend.Base.Equipment.ETransformer.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.PT :
                        {
                            Atend.Base.Equipment.EPT db = Atend.Base.Equipment.EPT.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.Insulator:
                        {
                            Atend.Base.Equipment.EInsulator db = Atend.Base.Equipment.EInsulator.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.ReCloser:
                        {
                            Atend.Base.Equipment.EReCloser db = Atend.Base.Equipment.EReCloser.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.Consol:
                        {
                            Atend.Base.Equipment.EConsol db = Atend.Base.Equipment.EConsol.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.PhusePole:
                        {
                            Atend.Base.Equipment.EPhusePole db = Atend.Base.Equipment.EPhusePole.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;

                    case Atend.Control.Enum.ProductType.SectionLizer:
                        {
                            Atend.Base.Equipment.ESectionLizer db = Atend.Base.Equipment.ESectionLizer.SelectByCode(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //MessageBox.Show("PoleRow" + db.Rows.Count.ToString());
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                        }
                        break;
                    default:
                        {

                            Atend.Base.Base.BProduct  db = Atend.Base.Base.BProduct.Select_ById(Convert.ToInt32(Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString()));
                            //gvJackPanelCell.Rows.Add();
                            gvJackPanelCell.Rows[i].Cells[0].Value = Atend.Base.Equipment.EJAckPanel.nodeProductCode[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[1].Value = Atend.Base.Equipment.EJAckPanel.nodeProductType[i].ToString();
                            gvJackPanelCell.Rows[i].Cells[3].Value = db.Name.ToString();
                            gvJackPanelCell.Rows[i].Cells[2].Value = Atend.Base.Equipment.EJAckPanel.nodeNumCell[i].ToString();
                            
                        }break;
                    
                }*/
            }
        }

        private void Save()
        {
            txtComment.Focus();
            Atend.Base.Equipment.EJAckPanel jackPanel = new Atend.Base.Equipment.EJAckPanel();
            jackPanel.CellCount = Convert.ToByte(nudCellCount.Value);
            jackPanel.Comment = txtComment.Text;
            jackPanel.Is20kv = true;
            jackPanel.MasterProductXCode = new Guid(cboBus.SelectedValue.ToString());
            jackPanel.MasterProductType = Convert.ToByte(Atend.Control.Enum.ProductType.Bus);
            jackPanel.Name = txtName.Text;
            jackPanel.IsDefault = IsDefault;
            jackPanel.ProductCode = Atend.Control.Common.selectedProductCode;
            jackPanel.Code = Code;
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
            jackPanel.EquipmentList = EPackageProduct;

            //Operation
            for (int i = 0; i < gvJackPanelCell.Rows.Count; i++)
            {
                Atend.Base.Equipment.EJackPanelCell jackpanelCell = new Atend.Base.Equipment.EJackPanelCell();
                jackpanelCell.Num = Convert.ToByte(gvJackPanelCell.Rows[i].Cells[2].Value);
                jackpanelCell.ProductXCode = new Guid(gvJackPanelCell.Rows[i].Cells[0].Value.ToString());
                jackpanelCell.ProductType = Convert.ToByte(gvJackPanelCell.Rows[i].Cells[1].Value);
                //jackpanelCell.EquipName = Convert.ToString(gvJackPanelCell.Rows[i].Cells[3].Value);  //EDIT
                jackPanel.JackPanelCell.Add(jackpanelCell);
            }

            ArrayList EOperation = new ArrayList();
            for (int i = 0; i < gvOperation.Rows.Count; i++)
            {
                Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                _EOperation.ProductID = Convert.ToInt32(gvOperation.Rows[i].Cells[0].Value);
                _EOperation.Count = Convert.ToDouble(gvOperation.Rows[i].Cells[3].Value);
                EOperation.Add(_EOperation);
            }
            jackPanel.OperationList = EOperation;

            if (SelectedJackPanelXCode == Guid.Empty)
            {
                if (jackPanel.InsertX())
                {
                    //MessageBox.Show("اطلاعات به درستی ثبت شد");
                    Reset();
                }
                else
                {
                    MessageBox.Show("امکان ذخیره سازی اطلاعات نمی باشد");
                }
            }
            else
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("SelectedJackPanel:"+SelectedJackPanel.ToString()+"\n");
                jackPanel.XCode = SelectedJackPanelXCode;
                if (jackPanel.UpdateX())
                {
                    //MessageBox.Show("اطلاعات به درستی ثبت شد");
                    Reset();
                }
                else
                {
                    MessageBox.Show("امکان ذخیره سازی اطلاعات نمی باشد");

                }
            }

        }

        private void nudCellCount_ValueChanged(object sender, EventArgs e)
        {
            decimal j = 0;
            if (gvJackPanelCell.Rows.Count < nudCellCount.Value)
            {
                j = nudCellCount.Value - gvJackPanelCell.Rows.Count;
                for (int i = 0; i < j; i++)
                {
                    gvJackPanelCell.Rows.Add();
                    gvJackPanelCell.Rows[gvJackPanelCell.Rows.Count - 1].Cells[2].Value = gvJackPanelCell.Rows.Count;
                }
            }
            else
            {
                j = gvJackPanelCell.Rows.Count - nudCellCount.Value;
                for (int i = 0; i < j; i++)
                {
                    gvJackPanelCell.Rows.RemoveAt(gvJackPanelCell.Rows.Count - 1);
                }
            }

        }

        private void frm20kvJackPanel_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("START\n");
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ById(ProductCode);
            //txtName.Text = product.Name;

            //after change drawing way
            BindDataToComboBox();
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

        private void gvJackPanelCell_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                frm20KvProductSelect02 frm20kvselect = new frm20KvProductSelect02(this);
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm20kvselect);
                //MessageBox.Show(productName.ToString());
                gvJackPanelCell.Rows[e.RowIndex].Cells[3].Value = productName;
                gvJackPanelCell.Rows[e.RowIndex].Cells[0].Value = selectedProductXCode;
                gvJackPanelCell.Rows[e.RowIndex].Cells[1].Value = selectedTypeCode;
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(selectedProductXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(selectedProductXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedJackPanelXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EJAckPanel.DeleteX(SelectedJackPanelXCode))
                    {
                        Reset();
                    }
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            Reset();
        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.MiddleJackPanel);

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
            }
        }

        private void toolStripLabel2_Click_1(object sender, EventArgs e)
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
                if (SelectedJackPanelXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EJAckPanel Equip = Atend.Base.Equipment.EJAckPanel.SelectByXCode(SelectedJackPanelXCode);
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
            btnDelete.Focus();
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفاً نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            if (Atend.Base.Equipment.EJAckPanel.SearchByName(txtName.Text) == true && SelectedJackPanelXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (nudCellCount.Value == 0)
            {
                MessageBox.Show("لطفاً تعداد سلول را مشخص نمایید", "خطا");
                nudCellCount.Focus();
                return false;
            }
            for (int j = 0; j < gvSelectedEquipment.Rows.Count; j++)
            {
                Atend.Base.Equipment.EProductPackage _EProductPackage = new Atend.Base.Equipment.EProductPackage();
                _EProductPackage.XCode = new Guid(gvSelectedEquipment.Rows[j].Cells[0].Value.ToString());
                _EProductPackage.Count = Convert.ToInt32(gvSelectedEquipment.Rows[j].Cells[2].Value.ToString());
                _EProductPackage.TableType = Convert.ToInt16(gvSelectedEquipment.Rows[j].Cells[3].Value.ToString());
                
                if (Atend.Base.Equipment.EContainerPackage.FindLoopNode(SelectedJackPanelXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel), _EProductPackage.XCode, _EProductPackage.TableType))
                {
                    MessageBox.Show(string.Format("تجهیز '{0}' در زیر تجهیزات موجود می باشد", txtName.Text), "خطا");
                    gvSelectedEquipment.Focus();
                    return false;
                }
            }
            
            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void toolStripLabel3_Click_1(object sender, EventArgs e)
        {
            if (CheckStatuseOfAccessChangeDefault())
                Delete();
        }

        private void toolStripLabel4_Click_1(object sender, EventArgs e)
        {
            frm20kvJackPanelSearch02 search = new frm20kvJackPanelSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(search);
        }

        private void toolStripLabel5_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click_2(object sender, EventArgs e)
        {
            Reset();
        }

        private void toolStripLabel2_Click_2(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void toolStripLabel3_Click_2(object sender, EventArgs e)
        {
            Delete();
        }

        private void toolStripLabel4_Click_2(object sender, EventArgs e)
        {
            frm20kvJackPanelSearch02 search = new frm20kvJackPanelSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(search);
        }

        private void toolStripLabel5_Click_2(object sender, EventArgs e)
        {
            Close();
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
            }

        }

        private void cboBus_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            if (SelectedJackPanelXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel),SelectedJackPanelXCode))
                {
                    Atend.Base.Equipment.EJAckPanel j1 = Atend.Base.Equipment.EJAckPanel.SelectByXCode(selectedProductXCode);
                    Code = j1.Code;
                    MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
                }
                else
                    MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            }
            else
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");

            //if (SelectedJackPanelXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EJAckPanel.ShareOnServer(SelectedJackPanelXCode))
            //    {
            //        Atend.Base.Equipment.EJAckPanel j1 = Atend.Base.Equipment.EJAckPanel.SelectByXCode(selectedProductXCode);
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

        private void gvJackPanelCell_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtBackUpName_TextChanged(object sender, EventArgs e)
        {

        }


    }
}