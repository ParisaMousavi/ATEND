using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Equipment
{
    public partial class frm20KvProductSelect02 : Form
    {
        frm20kvJackPanel02 frmjackPanel;
        DataColumn dc1 = new DataColumn("CodeT", System.Type.GetType("System.Byte"));
        DataColumn dc2 = new DataColumn("Name");
        DataTable CellTypeTable = new DataTable();
        bool ForceToClose = false;

        public frm20KvProductSelect02(frm20kvJackPanel02 JackPanel)
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
            //frmjackPanel = new frm20kvJackPanel02();
            frmjackPanel = JackPanel;

            CellTypeTable.Columns.Add(dc1);
            CellTypeTable.Columns.Add(dc2);
            //-----------------
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

            //ed.WriteMessage("Finish Initialize\n");
            //-----------------


        }

        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        private void ReadEquipGroups()
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
                    TreeNode RootTreeNode = new TreeNode(xnode.Attributes["Value"].Value);
                    RootTreeNode.Tag = xnode.Attributes["Code"].Value;
                    RootTreeNode.Name = "None";

                    if (xnode.Attributes["Table"].Value == "Self")
                    {
                        #region switch case part
                        switch ((Atend.Control.Enum.ProductType)(int.Parse(xnode.Attributes["Code"].Value)))
                        {
                            case Atend.Control.Enum.ProductType.AirPost:
                                DataTable AirPostTable = Atend.Base.Equipment.EAirPost.SelectAll();
                                foreach (DataRow row in AirPostTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["Code"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(((Int32)Atend.Control.Enum.ProductType.AirPost).ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            //case Atend.Control.Enum.ProductType.AuoKey3p:
                            //    DataTable AuoKey3pTable = Atend.Base.Equipment.EAutoKey_3p.SelectAll();
                            //    foreach (DataRow row in AuoKey3pTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.AuoKey3p.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }
                            //    break;
                            //case Atend.Control.Enum.ProductType.Branch:
                            //    break;
                            //case Atend.Control.Enum.ProductType.Breaker:
                            //    DataTable BreakerTable = Atend.Base.Equipment.EBreaker.SelectAll();
                            //    foreach (DataRow row in BreakerTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.Breaker.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.Bus:
                            //    DataTable BusTable = Atend.Base.Equipment.EBus.SelectAll();
                            //    foreach (DataRow row in BusTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.Bus.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }
                            //    break;
                            //case Atend.Control.Enum.ProductType.GroundCabel:
                            //    break;
                            //case Atend.Control.Enum.ProductType.CatOut:
                            //    DataTable CatcouTable = Atend.Base.Equipment.ECatOut.SelectAll();
                            //    foreach (DataRow row in CatcouTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.CatOut.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            case Atend.Control.Enum.ProductType.Conductor:
                                DataTable conductorTable = Atend.Base.Equipment.EConductor.SelectAll();
                                foreach (DataRow row in conductorTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["Code"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.Consol:
                                DataTable ConsolTable = Atend.Base.Equipment.EConsol.SelectAll();
                                foreach (DataRow row in ConsolTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["Code"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            //case Atend.Control.Enum.ProductType.Countor:
                            //    DataTable CounterTable = Atend.Base.Equipment.ECountor.SelectAll();
                            //    foreach (DataRow row in CounterTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.Countor.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }
                            //    break;
                            //case Atend.Control.Enum.ProductType.CT:
                            //    DataTable CtTable = Atend.Base.Equipment.ECT.SelectAll();
                            //    foreach (DataRow row in CtTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.CT.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }
                            //    break;
                            //case Atend.Control.Enum.ProductType.DB:
                            //    DataTable DbTable = Atend.Base.Equipment.EDB.SelectAll();
                            //    foreach (DataRow row in DbTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.DB.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }
                            //    break;
                            case Atend.Control.Enum.ProductType.Disconnector:
                                DataTable DisconnectorTable = Atend.Base.Equipment.EDisconnector.SelectAll();
                                foreach (DataRow row in DisconnectorTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["Code"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            //case Atend.Control.Enum.ProductType.Halter:
                            //    break;
                            //case Atend.Control.Enum.ProductType.HeaderCabel:
                            //    DataTable HeaderCableTable = Atend.Base.Equipment.EHeaderCabel.SelectAll();
                            //    foreach (DataRow row in HeaderCableTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.HeaderCabel.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            case Atend.Control.Enum.ProductType.Insulator:
                                DataTable InsulatorTable = Atend.Base.Equipment.EInsulator.SelectAll();
                                foreach (DataRow row in InsulatorTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["Code"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            //case Atend.Control.Enum.ProductType.JackPanel:
                            //    DataTable JackpanelTable = Atend.Base.Equipment.EJAckPanel.SelectAll();
                            //    foreach (DataRow row in JackpanelTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.JackPanel.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.Jumper:
                            //    DataTable JumperTable = Atend.Base.Equipment.EJAckPanel.SelectAll();
                            //    foreach (DataRow row in JumperTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.JackPanel.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.PhotoCell:
                            //    DataTable PhotoCellTable = Atend.Base.Equipment.EPhotoCell.SelectAll();
                            //    foreach (DataRow row in PhotoCellTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.PhotoCell.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.Phuse:
                            //    DataTable PhuseTable = Atend.Base.Equipment.EPhuse.SelectAll();
                            //    foreach (DataRow row in PhuseTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.Phuse.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.PhuseKey:
                            //    DataTable PhuseKeyTable = Atend.Base.Equipment.EPhuseKey.SelectAll();
                            //    foreach (DataRow row in PhuseKeyTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.PhuseKey.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.PhusePole:
                            //    DataTable PhusePoleTable = Atend.Base.Equipment.EPhusePole.SelectAll();
                            //    foreach (DataRow row in PhusePoleTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.PhusePole.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            case Atend.Control.Enum.ProductType.Pole:
                                DataTable PoleTable = Atend.Base.Equipment.EPole.SelectAll();
                                foreach (DataRow row in PoleTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["Code"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

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
                            //case Atend.Control.Enum.ProductType.PT:
                            //    DataTable PtTable = Atend.Base.Equipment.EPT.SelectAll();
                            //    foreach (DataRow row in PtTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.PT.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.ReCloser:
                            //    DataTable ReCloserTable = Atend.Base.Equipment.EReCloser.SelectAll();
                            //    foreach (DataRow row in ReCloserTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.ReCloser.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.Rod:
                            //    DataTable RodTable = Atend.Base.Equipment.ERod.SelectAll();
                            //    foreach (DataRow row in RodTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.Rod.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.StreetBox:
                            //    DataTable StreetBoxTable = Atend.Base.Equipment.EStreetBox.SelectAll();
                            //    foreach (DataRow row in StreetBoxTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.StreetBox.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            //case Atend.Control.Enum.ProductType.Transformer:
                            //    DataTable TransformerTable = Atend.Base.Equipment.ETransformer.SelectAll();
                            //    foreach (DataRow row in TransformerTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["Code"].ToString();
                            //        ChildNode.Name = Atend.Control.Enum.ProductType.Transformer.ToString();
                            //        ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }
                            //    break;
                            case Atend.Control.Enum.ProductType.InsulatorPipe:
                                DataTable InsulatorPipeTable = Atend.Base.Equipment.ETransformer.SelectAll();
                                foreach (DataRow row in InsulatorPipeTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["Code"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.InsulatorChain:
                                DataTable InsulatorChainTable = Atend.Base.Equipment.ETransformer.SelectAll();
                                foreach (DataRow row in InsulatorChainTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["Code"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.WeekJackPanel:
                                DataTable WeekJackPanelTable = Atend.Base.Equipment.ETransformer.SelectAll();
                                foreach (DataRow row in WeekJackPanelTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["Code"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        DataTable ProductTable = Atend.Base.Base.BProduct.SelectByType(int.Parse(xnode.Attributes["Code"].Value));
                        foreach (DataRow row in ProductTable.Rows)
                        {
                            TreeNode ChidTreeNode = new TreeNode(row["Name"].ToString());
                            ChidTreeNode.Tag = Convert.ToInt32(row["Id"]);
                            ChidTreeNode.Name = RootTreeNode.Tag.ToString();
                        }
                    }

                    tvProduct.Nodes.Add(RootTreeNode);
                }
            }
        }

        private void frm20kvProductSelect_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //ReadEquipGroups();



            //
            //gvProduct.DataSource = Atend.Base.Equipment.ECell.SelectAll();



            DataTable dt = Atend.Base.Equipment.ECell.SelectAllX();
            DataColumn dc = new DataColumn("Kind");
            dt.Columns.Add(dc);
            foreach (DataRow dr in dt.Rows)
            {


                dr["Kind"] = CellTypeTable.Select(string.Format("CodeT={0}", dr["Type"].ToString()))[0]["Name"].ToString();

                //if (dr["Type"].ToString() == "0")
                //    dr["Kind"] = "اندازه گيري";

                //if (dr["Type"].ToString() == "1")
                //    dr["Kind"] = "كليد";

                //if (dr["Type"].ToString() == "2")
                //    dr["Kind"] = "رله";

                //if (dr["Type"].ToString() == "3")
                //    dr["Kind"] = "Bus Coupler";
            }
            //MessageBox.Show(dt.Rows.Count.ToString());
            gvProduct.AutoGenerateColumns = false;
            gvProduct.DataSource = dt;
        }

        private void tvProduct_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tvProduct_DoubleClick(object sender, EventArgs e)
        {
            if (tvProduct.SelectedNode.Name != "None")
            {
                frmjackPanel.selectedProductXCode = new Guid(tvProduct.SelectedNode.Tag.ToString());
                frmjackPanel.selectedTypeCode = Convert.ToByte(tvProduct.SelectedNode.Name);
                frmjackPanel.productName = tvProduct.SelectedNode.Text;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void gvProduct_DoubleClick(object sender, EventArgs e)
        {
            frmjackPanel.productName = gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[1].Value.ToString();
            frmjackPanel.selectedProductXCode = new Guid(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            frmjackPanel.selectedTypeCode = Convert.ToByte(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[3].Value.ToString());
            Close();
            /*if (gvProduct.Rows.Count > 0)
            {
                frmjackPanel.BindDataToOwnControl(Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString()));
                Close();
            }*/
        }
    }
}