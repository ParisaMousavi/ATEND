using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Global.Utility
{
    public class UBinding
    {
        //public static void BindDataToTreeView(TreeView tvNames)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

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
        //    foreach (XmlElement xElement in _xmlDoc.DocumentElement)
        //    {
        //        foreach (XmlNode xnode in xElement.ChildNodes)
        //        {
        //            TreeNode RootTreeNode = new TreeNode(xnode.Attributes["Value"].Value);
        //            RootTreeNode.Tag = xnode.Attributes["Code"].Value;
        //            RootTreeNode.Name = "None";

        //            if (xnode.Attributes["Table"].Value == "Self")
        //            {
        //                #region switch case part
        //                switch ((Atend.Control.Enum.ProductType)(int.Parse(xnode.Attributes["Code"].Value)))
        //                {
        //                    case Atend.Control.Enum.ProductType.AirPost:
        //                        DataTable AirPostTable = Atend.Base.Equipment.EAirPost.SelectAll();
        //                        foreach (DataRow row in AirPostTable.Rows)
        //                        {

        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            ////ed.writeMessage(((Int32)Atend.Control.Enum.ProductType.AirPost).ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.AuoKey3p:
        //                        DataTable AuoKey3pTable = Atend.Base.Equipment.EAutoKey_3p.SelectAll();
        //                        foreach (DataRow row in AuoKey3pTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    //case Atend.Control.Enum.ProductType.Branch:
        //                    //    break;
        //                    case Atend.Control.Enum.ProductType.Breaker:
        //                        DataTable BreakerTable = Atend.Base.Equipment.EBreaker.SelectAll();
        //                        foreach (DataRow row in BreakerTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.Bus:
        //                        DataTable BusTable = Atend.Base.Equipment.EBus.SelectAll();
        //                        foreach (DataRow row in BusTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.GroundCabel:
        //                        DataTable MiddlecabelTable = Atend.Base.Equipment.EGroundCabel.SelectAll();
        //                        foreach (DataRow row in MiddlecabelTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.CatOut:
        //                        DataTable CatcouTable = Atend.Base.Equipment.ECatOut.SelectAll();
        //                        foreach (DataRow row in CatcouTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.Conductor:
        //                        DataTable conductorTable = Atend.Base.Equipment.EConductor.SelectAll();
        //                        foreach (DataRow row in conductorTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.Consol:

        //                        DataTable ConsolTable = Atend.Base.Equipment.EConsol.SelectAll();
        //                        foreach (DataRow row in ConsolTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.Countor:
        //                        DataTable CounterTable = Atend.Base.Equipment.ECountor.SelectAll();
        //                        foreach (DataRow row in CounterTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.CT:
        //                        DataTable CtTable = Atend.Base.Equipment.ECT.SelectAll();
        //                        foreach (DataRow row in CtTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.DB:
        //                        DataTable DbTable = Atend.Base.Equipment.EDB.SelectAll();
        //                        foreach (DataRow row in DbTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.Disconnector:
        //                        DataTable DisconnectorTable = Atend.Base.Equipment.EDisconnector.SelectAll();
        //                        foreach (DataRow row in DisconnectorTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.Mafsal:
        //                        DataTable MafsalTable = Atend.Base.Equipment.EMafsal.SelectAll();
        //                        foreach (DataRow row in MafsalTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.GroundPost:
        //                        DataTable GroundPostTable = Atend.Base.Equipment.EGroundPost.SelectAll();
        //                        foreach (DataRow row in GroundPostTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;

        //                    case Atend.Control.Enum.ProductType.HeaderCabel:
        //                        DataTable HeaderCableTable = Atend.Base.Equipment.EHeaderCabel.SelectAll();
        //                        foreach (DataRow row in HeaderCableTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.Insulator:
        //                        DataTable InsulatorTable = Atend.Base.Equipment.EInsulator.SelectAll();
        //                        foreach (DataRow row in InsulatorTable.Rows)
        //                        {

        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;



        //                    case Atend.Control.Enum.ProductType.MiddleJackPanel:
        //                        //ed.writeMessage("ProductType= " + Atend.Control.Enum.ProductType.MiddleJackPanel.ToString()+"\n");
        //                        DataTable JackpanelTable = Atend.Base.Equipment.EJAckPanel.SelectAll();
        //                        foreach (DataRow row in JackpanelTable.Rows)
        //                        {
        //                            //ed.writeMessage("In The ForEAch\n");
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    ////////case Atend.Control.Enum.ProductType.Jumper:
        //                    ////////    DataTable JumperTable = Atend.Base.Equipment.EJumper.SelectAll();
        //                    ////////    foreach (DataRow row in JumperTable.Rows)
        //                    ////////    {
        //                    ////////        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                    ////////        ChildNode.Tag = row["Code"].ToString();
        //                    ////////        ChildNode.Name = RootTreeNode.Tag.ToString();
        //                    ////////        //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                    ////////        RootTreeNode.Nodes.Add(ChildNode);
        //                    ////////    }

        //                    ////////    break;
        //                    case Atend.Control.Enum.ProductType.Khazan:
        //                        DataTable KhazanTable = Atend.Base.Equipment.EKhazan.SelectAll();
        //                        foreach (DataRow row in KhazanTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.PhotoCell:
        //                        DataTable PhotoCellTable = Atend.Base.Equipment.EPhotoCell.SelectAll();
        //                        foreach (DataRow row in PhotoCellTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.Phuse:
        //                        DataTable PhuseTable = Atend.Base.Equipment.EPhuse.SelectAll();
        //                        foreach (DataRow row in PhuseTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.PhuseKey:
        //                        DataTable PhuseKeyTable = Atend.Base.Equipment.EPhuseKey.SelectAll();
        //                        foreach (DataRow row in PhuseKeyTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.PhusePole:
        //                        DataTable PhusePoleTable = Atend.Base.Equipment.EPhusePole.SelectAll();
        //                        foreach (DataRow row in PhusePoleTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.Pole:
        //                        DataTable PoleTable = Atend.Base.Equipment.EPole.SelectAll();
        //                        foreach (DataRow row in PoleTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    //case Atend.Control.Enum.ProductType.Post:
        //                    //    DataTable PostTable = Atend.Base.Equipment.EPost.SelectAll();
        //                    //    foreach (DataRow row in PostTable.Rows)
        //                    //    {
        //                    //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                    //        ChildNode.Tag = row["Code"].ToString();
        //                    //        ChildNode.Name = RootTreeNode.Tag.ToString();
        //                    //        RootTreeNode.Nodes.Add(ChildNode);
        //                    //    }

        //                    //    break;
        //                    case Atend.Control.Enum.ProductType.PT:
        //                        DataTable PtTable = Atend.Base.Equipment.EPT.SelectAll();
        //                        foreach (DataRow row in PtTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.ReCloser:
        //                        DataTable ReCloserTable = Atend.Base.Equipment.EReCloser.SelectAll();
        //                        foreach (DataRow row in ReCloserTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.Rod:
        //                        DataTable RodTable = Atend.Base.Equipment.ERod.SelectAll();
        //                        foreach (DataRow row in RodTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.StreetBox:
        //                        DataTable StreetBoxTable = Atend.Base.Equipment.EStreetBox.SelectAll();
        //                        foreach (DataRow row in StreetBoxTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }

        //                        break;
        //                    case Atend.Control.Enum.ProductType.Transformer:
        //                        DataTable TransformerTable = Atend.Base.Equipment.ETransformer.SelectAll();
        //                        foreach (DataRow row in TransformerTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString(); //Atend.Control.Enum.ProductType.Transformer.ToString();
        //                            //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.InsulatorPipe:
        //                        DataTable InsulatorPipeTable = Atend.Base.Equipment.ETransformer.SelectAll();
        //                        foreach (DataRow row in InsulatorPipeTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.InsulatorChain:
        //                        DataTable InsulatorChainTable = Atend.Base.Equipment.ETransformer.SelectAll();
        //                        foreach (DataRow row in InsulatorChainTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;
        //                    case Atend.Control.Enum.ProductType.WeekJackPanel:
        //                        DataTable WeekJackPanelTable = Atend.Base.Equipment.EJackPanelWeek.SelectAll();
        //                        foreach (DataRow row in WeekJackPanelTable.Rows)
        //                        {
        //                            TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                            ChildNode.Tag = row["Code"].ToString();
        //                            ChildNode.Name = RootTreeNode.Tag.ToString();
        //                            RootTreeNode.Nodes.Add(ChildNode);
        //                        }
        //                        break;

        //                    //case Atend.Control.Enum.ProductType.Halter:
        //                    //    DataTable HalterTable = Atend.Base.Equipment.EHalter.SelectAll();
        //                    //    foreach (DataRow row in HalterTable.Rows)
        //                    //    {
        //                    //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                    //        ChildNode.Tag = row["Code"].ToString();
        //                    //        ChildNode.Name = RootTreeNode.Tag.ToString();
        //                    //        RootTreeNode.Nodes.Add(ChildNode);
        //                    //    }
        //                    //    break;

        //                    //case Atend.Control.Enum.ProductType.KablSho:
        //                    //    DataTable KablShoTable = Atend.Base.Equipment.EKablsho.SelectAll();
        //                    //    foreach (DataRow row in KablShoTable.Rows)
        //                    //    {
        //                    //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                    //        ChildNode.Tag = row["Code"].ToString();
        //                    //        ChildNode.Name = RootTreeNode.Tag.ToString();
        //                    //        RootTreeNode.Nodes.Add(ChildNode);
        //                    //    }
        //                    //    break;

        //                    //case Atend.Control.Enum.ProductType.Kalamp:
        //                    //    DataTable KalampTable = Atend.Base.Equipment.EClamp.SelectAll();
        //                    //    foreach (DataRow row in KalampTable.Rows)
        //                    //    {
        //                    //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                    //        ChildNode.Tag = row["Code"].ToString();
        //                    //        ChildNode.Name = RootTreeNode.Tag.ToString();
        //                    //        RootTreeNode.Nodes.Add(ChildNode);
        //                    //    }
        //                    //    break;

        //                    //case Atend.Control.Enum.ProductType.Ramp:
        //                    //    DataTable RampTable = Atend.Base.Equipment.ERamp.SelectAll();
        //                    //    foreach (DataRow row in RampTable.Rows)
        //                    //    {
        //                    //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
        //                    //        ChildNode.Tag = row["Code"].ToString();
        //                    //        ChildNode.Name = RootTreeNode.Tag.ToString();
        //                    //        RootTreeNode.Nodes.Add(ChildNode);
        //                    //    }
        //                    //    break;
        //                }
        //                #endregion
        //            }
        //            else
        //            {
        //                DataTable ProductTable = Atend.Base.Base.BProduct.SelectByType(int.Parse(xnode.Attributes["Code"].Value));
        //                foreach (DataRow row in ProductTable.Rows)
        //                {
        //                    TreeNode ChidTreeNode = new TreeNode(row["Name"].ToString());
        //                    ChidTreeNode.Tag = Convert.ToInt32(row["Id"]);
        //                    ChidTreeNode.Name = RootTreeNode.Tag.ToString();
        //                }
        //            }

        //            tvNames.Nodes.Add(RootTreeNode);
        //        }
        //    }
        //}

        public static void BindDataToTreeViewX(TreeView tvNames)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

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
                                DataTable AirPostTable = Atend.Base.Equipment.EAirPost.SelectAllX();
                                foreach (DataRow row in AirPostTable.Rows)
                                {

                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    ////ed.writeMessage(((Int32)Atend.Control.Enum.ProductType.AirPost).ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.AuoKey3p:
                                DataTable AuoKey3pTable = Atend.Base.Equipment.EAutoKey_3p.SelectAllX();
                                foreach (DataRow row in AuoKey3pTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;

                            //case Atend.Control.Enum.ProductType.Branch:
                            //    break;

                            case Atend.Control.Enum.ProductType.Breaker:
                                DataTable BreakerTable = Atend.Base.Equipment.EBreaker.SelectAllX();
                                foreach (DataRow row in BreakerTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.MeasuredJackPanel:
                                DataTable MeasuredJackPanelTable = Atend.Base.Equipment.EMeasuredJackPanel.SelectAllX();
                                foreach (DataRow row in MeasuredJackPanelTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Bus:
                                DataTable BusTable = Atend.Base.Equipment.EBus.SelectAllX();
                                foreach (DataRow row in BusTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.GroundCabel:
                                DataTable MiddlecabelTable = Atend.Base.Equipment.EGroundCabel.SelectAllX();
                                foreach (DataRow row in MiddlecabelTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.CatOut:
                                DataTable CatcouTable = Atend.Base.Equipment.ECatOut.SelectAllX();
                                foreach (DataRow row in CatcouTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Floor:
                                DataTable FloorTable = Atend.Base.Equipment.EFloor.SelectAllX();
                                foreach (DataRow row in FloorTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Conductor:
                                DataTable conductorTable = Atend.Base.Equipment.EConductor.SelectAllX();
                                foreach (DataRow row in conductorTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.Consol:

                                DataTable ConsolTable = Atend.Base.Equipment.EConsol.SelectAllX();
                                foreach (DataRow row in ConsolTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.Countor:
                                DataTable CounterTable = Atend.Base.Equipment.ECountor.SelectAllX();
                                foreach (DataRow row in CounterTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.CT:
                                DataTable CtTable = Atend.Base.Equipment.ECT.SelectAllX();
                                foreach (DataRow row in CtTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.DB:
                                DataTable DbTable = Atend.Base.Equipment.EDB.SelectAllX();
                                foreach (DataRow row in DbTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.Disconnector:
                                DataTable DisconnectorTable = Atend.Base.Equipment.EDisconnector.SelectAllX();
                                foreach (DataRow row in DisconnectorTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Mafsal:
                                DataTable MafsalTable = Atend.Base.Equipment.EMafsal.SelectAllX();
                                foreach (DataRow row in MafsalTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Light:
                                DataTable LightTable = Atend.Base.Equipment.ELight.SelectAllX();
                                foreach (DataRow row in LightTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.GroundPost:
                                DataTable GroundPostTable = Atend.Base.Equipment.EGroundPost.SelectAllX();
                                foreach (DataRow row in GroundPostTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            //case Atend.Control.Enum.ProductType.Halter:
                            //    break;
                            case Atend.Control.Enum.ProductType.HeaderCabel:
                                DataTable HeaderCableTable = Atend.Base.Equipment.EHeaderCabel.SelectAllX();
                                foreach (DataRow row in HeaderCableTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Insulator:
                                DataTable InsulatorTable = Atend.Base.Equipment.EInsulator.SelectAllX();
                                foreach (DataRow row in InsulatorTable.Rows)
                                {

                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;



                            case Atend.Control.Enum.ProductType.MiddleJackPanel:
                                //ed.writeMessage("ProductType= " + Atend.Control.Enum.ProductType.MiddleJackPanel.ToString()+"\n");
                                DataTable JackpanelTable = Atend.Base.Equipment.EJAckPanel.SelectAllX();
                                foreach (DataRow row in JackpanelTable.Rows)
                                {
                                    //ed.writeMessage("In The ForEAch\n");
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            //////////case Atend.Control.Enum.ProductType.Jumper:
                            //////////    DataTable JumperTable = Atend.Base.Equipment.EJumper.SelectAll();
                            //////////    foreach (DataRow row in JumperTable.Rows)
                            //////////    {
                            //////////        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //////////        ChildNode.Tag = row["XCode"].ToString();
                            //////////        ChildNode.Name = RootTreeNode.Tag.ToString();
                            //////////        //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                            //////////        RootTreeNode.Nodes.Add(ChildNode);
                            //////////    }

                            //////////    break;
                            case Atend.Control.Enum.ProductType.Khazan:
                                DataTable KhazanTable = Atend.Base.Equipment.EKhazan.SelectAllX();
                                foreach (DataRow row in KhazanTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.WriteMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.PhotoCell:
                                DataTable PhotoCellTable = Atend.Base.Equipment.EPhotoCell.SelectAllX();
                                foreach (DataRow row in PhotoCellTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Phuse:
                                DataTable PhuseTable = Atend.Base.Equipment.EPhuse.SelectAllX();
                                foreach (DataRow row in PhuseTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.PhuseKey:
                                DataTable PhuseKeyTable = Atend.Base.Equipment.EPhuseKey.SelectAllX();
                                foreach (DataRow row in PhuseKeyTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.SelfKeeper:
                                DataTable SelfKeeperTable = Atend.Base.Equipment.ESelfKeeper.SelectAllX();
                                foreach (DataRow row in SelfKeeperTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;

                            case Atend.Control.Enum.ProductType.MiniatureKey:
                                DataTable MinKeyTable = Atend.Base.Equipment.EMiniatorKey.SelectAllX();
                                foreach (DataRow row in MinKeyTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;

                            case Atend.Control.Enum.ProductType.PhusePole:
                                DataTable PhusePoleTable = Atend.Base.Equipment.EPhusePole.SelectAllX();
                                foreach (DataRow row in PhusePoleTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Pole:
                                DataTable PoleTable = Atend.Base.Equipment.EPole.SelectAllX();
                                foreach (DataRow row in PoleTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            //case Atend.Control.Enum.ProductType.Post:
                            //    DataTable PostTable = Atend.Base.Equipment.EPost.SelectAll();
                            //    foreach (DataRow row in PostTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["XCode"].ToString();
                            //        ChildNode.Name = RootTreeNode.Tag.ToString();
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            case Atend.Control.Enum.ProductType.PT:
                                DataTable PtTable = Atend.Base.Equipment.EPT.SelectAllX();
                                foreach (DataRow row in PtTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.ReCloser:
                                DataTable ReCloserTable = Atend.Base.Equipment.EReCloser.SelectAllX();
                                foreach (DataRow row in ReCloserTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Rod:
                                DataTable RodTable = Atend.Base.Equipment.ERod.SelectAllX();
                                foreach (DataRow row in RodTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.StreetBox:
                                DataTable StreetBoxTable = Atend.Base.Equipment.EStreetBox.SelectAllX();
                                foreach (DataRow row in StreetBoxTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Transformer:
                                DataTable TransformerTable = Atend.Base.Equipment.ETransformer.SelectAllX();
                                foreach (DataRow row in TransformerTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString(); //Atend.Control.Enum.ProductType.Transformer.ToString();
                                    //ed.writeMessage(Atend.Control.Enum.ProductType.AirPost.ToString());
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.InsulatorPipe:
                                DataTable InsulatorPipeTable = Atend.Base.Equipment.EInsulatorPipe.SelectAllX();
                                foreach (DataRow row in InsulatorPipeTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.InsulatorChain:
                                DataTable InsulatorChainTable = Atend.Base.Equipment.EInsulatorChain.SelectAllX();
                                foreach (DataRow row in InsulatorChainTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.WeekJackPanel:
                                DataTable WeekJackPanelTable = Atend.Base.Equipment.EJackPanelWeek.SelectAllX();
                                foreach (DataRow row in WeekJackPanelTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.Halter:
                                DataTable HalterTable = Atend.Base.Equipment.EHalter.SelectAllX();
                                foreach (DataRow row in HalterTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;

                            case Atend.Control.Enum.ProductType.KablSho:
                                DataTable KablShoTable = Atend.Base.Equipment.EKablsho.SelectAllX();
                                foreach (DataRow row in KablShoTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Kalamp:
                                DataTable KalampTable = Atend.Base.Equipment.EClamp.SelectAllX();
                                foreach (DataRow row in KalampTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Ramp:
                                DataTable RampTable = Atend.Base.Equipment.ERamp.SelectAllX();
                                foreach (DataRow row in RampTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Prop:
                                DataTable PropTable = Atend.Base.Equipment.EProp.SelectAllX();
                                foreach (DataRow row in PropTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.SectionLizer:
                                DataTable SectionLizerTable = Atend.Base.Equipment.ESectionLizer.SelectAllX();
                                foreach (DataRow row in SectionLizerTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.Ground:
                                DataTable GroundTable = Atend.Base.Equipment.EGround.SelectAllX();
                                foreach (DataRow row in GroundTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.Cell:
                                DataTable CellTable = Atend.Base.Equipment.ECell.SelectAllX();
                                foreach (DataRow row in CellTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;

                            case Atend.Control.Enum.ProductType.Arm:
                                DataTable ArmTable = Atend.Base.Equipment.EArm.SelectAllX();
                                foreach (DataRow row in ArmTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                        }
                    
                        #endregion
                    }
                    ////////////else
                    ////////////{
                    ////////////    //////////DataTable ProductTable = Atend.Base.Base.BProduct.SelectByType(int.Parse(xnode.Attributes["Code"].Value));
                    ////////////    //////////foreach (DataRow row in ProductTable.Rows)
                    ////////////    //////////{
                    ////////////    //////////    TreeNode ChidTreeNode = new TreeNode(row["Name"].ToString());
                    ////////////    //////////    ChidTreeNode.Tag = Convert.ToInt32(row["Id"]);
                    ////////////    //////////    ChidTreeNode.Name = RootTreeNode.Tag.ToString();
                    ////////////    //////////}
                    ////////////}

                    tvNames.Nodes.Add(RootTreeNode);
                }
            }
        }

        public static void BindDataToTreeViewXForGetFromServer(TreeView tvNames)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

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

                    //ed.WriteMessage("\n Name = {0}\n", xnode.Attributes["Value"].Value);

                    if (xnode.Attributes["Table"].Value == "Self")
                    {
                        tvNames.Nodes.Add(RootTreeNode);
                    }

                }
            }
        }

        public static void BindDataToTreeViewForGetFromServer(TreeView tvNames)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

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

            string xmlPath = fullPath + "\\EquipmentNameServer.xml";
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
                        tvNames.Nodes.Add(RootTreeNode);
                    }

                }
            }
        }

        public static void BindDataToTreeViewXForPost(TreeView tvNames, int ix) //ix=0 airpost ,ix=1 groundpost
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

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
            string xmlPath = string.Empty;
            if (ix == 0)
            {
                xmlPath = fullPath + "\\AirPostEquipmentName.xml";
            }
            else if (ix == 1)
            {
                xmlPath = fullPath + "\\GroundPostEquipmentName.xml";
            }
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
                            //case Atend.Control.Enum.ProductType.AirPost:
                            //    DataTable AirPostTable = Atend.Base.Equipment.EAirPost.SelectAllX();
                            //    foreach (DataRow row in AirPostTable.Rows)
                            //    {

                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["XCode"].ToString();
                            //        ChildNode.Name = RootTreeNode.Tag.ToString();
                            //        ////ed.writeMessage(((Int32)Atend.Control.Enum.ProductType.AirPost).ToString());
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }
                            //    break;
                            //case Atend.Control.Enum.ProductType.GroundPost:
                            //    DataTable GroundPostTable = Atend.Base.Equipment.EGroundPost.SelectAllX();
                            //    foreach (DataRow row in GroundPostTable.Rows)
                            //    {
                            //        TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                            //        ChildNode.Tag = row["XCode"].ToString();
                            //        ChildNode.Name = RootTreeNode.Tag.ToString();
                            //        RootTreeNode.Nodes.Add(ChildNode);
                            //    }

                            //    break;
                            case Atend.Control.Enum.ProductType.MiddleJackPanel:
                                DataTable JackpanelTable = Atend.Base.Equipment.EJAckPanel.SelectAllX();
                                foreach (DataRow row in JackpanelTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }

                                break;
                            case Atend.Control.Enum.ProductType.Transformer:
                                DataTable TransformerTable = Atend.Base.Equipment.ETransformer.SelectAllX();
                                foreach (DataRow row in TransformerTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
                                    ChildNode.Name = RootTreeNode.Tag.ToString(); //Atend.Control.Enum.ProductType.Transformer.ToString();
                                    RootTreeNode.Nodes.Add(ChildNode);
                                }
                                break;
                            case Atend.Control.Enum.ProductType.WeekJackPanel:
                                DataTable WeekJackPanelTable = Atend.Base.Equipment.EJackPanelWeek.SelectAllX();
                                foreach (DataRow row in WeekJackPanelTable.Rows)
                                {
                                    TreeNode ChildNode = new TreeNode(row["Name"].ToString());
                                    ChildNode.Tag = row["XCode"].ToString();
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

                    tvNames.Nodes.Add(RootTreeNode);
                }
            }
        }

        //frmGroundPost
        public static bool ExistInSubEquip(Guid _Equip, out string name)
        {
            Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(_Equip);
            name = string.Empty;
            if (_ProductPackage.Code != -1)
            {
                 name = string.Empty;
                Atend.Base.Equipment.EContainerPackage _ContainerPackage = Atend.Base.Equipment.EContainerPackage.selectByCode(_ProductPackage.ContainerPackageCode);
                switch (_ContainerPackage.Type)
                {
                    case (int)Atend.Control.Enum.ProductType.AirPost:
                        Atend.Base.Equipment.EAirPost equ = Atend.Base.Equipment.EAirPost.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" پست هوایی : {0}", equ.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.AuoKey3p:
                        Atend.Base.Equipment.EAutoKey_3p AutoKey_3p = Atend.Base.Equipment.EAutoKey_3p.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کلید اتوماتیک3فاز : {0}", AutoKey_3p.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Breaker:
                        Atend.Base.Equipment.EBreaker Breaker = Atend.Base.Equipment.EBreaker.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" دژنکتور : {0}", Breaker.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Bus:
                        Atend.Base.Equipment.EBus Bus = Atend.Base.Equipment.EBus.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" شین : {0}", Bus.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.CatOut:
                        Atend.Base.Equipment.ECatOut CatOut = Atend.Base.Equipment.ECatOut.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کت اوت : {0}", CatOut.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Cell:
                        Atend.Base.Equipment.ECell Cell = Atend.Base.Equipment.ECell.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" سلول : {0}", Cell.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Kalamp:
                        Atend.Base.Equipment.EClamp Kalamp = Atend.Base.Equipment.EClamp.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کلمپ : {0}", Kalamp.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Conductor:
                        Atend.Base.Equipment.EConductor Conductor = Atend.Base.Equipment.EConductor.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" سیم : {0}", Conductor.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.ConductorTip:
                        Atend.Base.Equipment.EConductorTip ConductorTip = Atend.Base.Equipment.EConductorTip.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" تیپ بندی سیم : {0}", ConductorTip.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Consol:
                        Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کنسول : {0}", Consol.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Countor:
                        Atend.Base.Equipment.ECountor Countor = Atend.Base.Equipment.ECountor.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کنتور : {0}", Countor.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.CT:
                        Atend.Base.Equipment.ECT CT = Atend.Base.Equipment.ECT.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" CT : {0}", CT.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.DB:
                        Atend.Base.Equipment.EDB DB = Atend.Base.Equipment.EDB.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" جعبه انشعاب : {0}", DB.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Disconnector:
                        Atend.Base.Equipment.EDisconnector Disconnector = Atend.Base.Equipment.EDisconnector.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" سکسیونر : {0}", Disconnector.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Floor:
                        Atend.Base.Equipment.EFloor Floor = Atend.Base.Equipment.EFloor.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" سکو : {0}", Floor.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Ground:
                        Atend.Base.Equipment.EGround Ground = Atend.Base.Equipment.EGround.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" سیستم زمین : {0}", Ground.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.GroundCabel:
                        Atend.Base.Equipment.EGroundCabel GroundCabel = Atend.Base.Equipment.EGroundCabel.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کابل زمینی : {0}", GroundCabel.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.GroundCabelTip:
                        Atend.Base.Equipment.EGroundCabelTip GroundCabeltip = Atend.Base.Equipment.EGroundCabelTip.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" jتیپ بندی کابل زمینی : {0}", GroundCabeltip.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.GroundPost:
                        Atend.Base.Equipment.EGroundPost GroundPost = Atend.Base.Equipment.EGroundPost.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" پست زمینی : {0}", GroundPost.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Halter:
                        Atend.Base.Equipment.EHalter Halter = Atend.Base.Equipment.EHalter.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" مهار : {0}", Halter.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.HeaderCabel:
                        Atend.Base.Equipment.EHeaderCabel HeaderCabel = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" سرکابل : {0}", HeaderCabel.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Insulator:
                        Atend.Base.Equipment.EInsulator Insulator = Atend.Base.Equipment.EInsulator.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" مقره : {0}", Insulator.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.InsulatorChain:
                        Atend.Base.Equipment.EInsulatorChain InsulatorChain = Atend.Base.Equipment.EInsulatorChain.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" زنجیرمقره : {0}", InsulatorChain.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.InsulatorPipe:
                        Atend.Base.Equipment.EInsulatorPipe InsulatorPipe = Atend.Base.Equipment.EInsulatorPipe.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" میل مقره : {0}", InsulatorPipe.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.MiddleJackPanel:
                        Atend.Base.Equipment.EJAckPanel MiddleJackPanel = Atend.Base.Equipment.EJAckPanel.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" تابلوفشارمتوسط : {0}", MiddleJackPanel.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.WeekJackPanel:
                        Atend.Base.Equipment.EJackPanelWeek WeekJackPanel = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" تابلوفشارضعیف : {0}", WeekJackPanel.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.KablSho:
                        Atend.Base.Equipment.EKablsho KablSho = Atend.Base.Equipment.EKablsho.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کابلشو : {0}", KablSho.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Khazan:
                        Atend.Base.Equipment.EKhazan Khazan = Atend.Base.Equipment.EKhazan.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" خازن : {0}", Khazan.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.BankKhazan:
                        Atend.Base.Equipment.EKhazanTip Khazantip = Atend.Base.Equipment.EKhazanTip.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" تیپ بندی خازن : {0}", Khazantip.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Light:
                        MessageBox.Show(_ContainerPackage.XCode.ToString());
                        Atend.Base.Equipment.ELight Light = Atend.Base.Equipment.ELight.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" روشنایی : {0}", Light.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Mafsal:
                        Atend.Base.Equipment.EMafsal Mafsal = Atend.Base.Equipment.EMafsal.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" مفصل : {0}", Mafsal.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.MeasuredJackPanel:
                        Atend.Base.Equipment.EMeasuredJackPanel MeasuredJackPanel = Atend.Base.Equipment.EMeasuredJackPanel.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" تابلواندازه گیری : {0}", MeasuredJackPanel.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.MiniatureKey:
                        Atend.Base.Equipment.EMiniatorKey MiniatureKey = Atend.Base.Equipment.EMiniatorKey.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کلیدمینیاتور : {0}", MiniatureKey.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.PhotoCell:
                        Atend.Base.Equipment.EPhotoCell PhotoCell = Atend.Base.Equipment.EPhotoCell.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" فتوسل : {0}", PhotoCell.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Phuse:
                        Atend.Base.Equipment.EPhuse Phuse = Atend.Base.Equipment.EPhuse.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" فیوز : {0}", Phuse.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.PhuseKey:
                        Atend.Base.Equipment.EPhuseKey PhuseKey = Atend.Base.Equipment.EPhuseKey.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کلیدفیوز : {0}", PhuseKey.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.PhusePole:
                        Atend.Base.Equipment.EPhusePole PhusePole = Atend.Base.Equipment.EPhusePole.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" پایه فیوز : {0}", PhusePole.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Pole:
                        Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" پایه : {0}", Pole.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.PoleTip:
                        Atend.Base.Equipment.EPoleTip Poletip = Atend.Base.Equipment.EPoleTip.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" تیپ بندی پایه : {0}", Poletip.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Prop:
                        Atend.Base.Equipment.EProp Prop = Atend.Base.Equipment.EProp.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" تسمه حائل : {0}", Prop.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.PT:
                        Atend.Base.Equipment.EPT PT = Atend.Base.Equipment.EPT.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" PT : {0}", PT.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Ramp:
                        Atend.Base.Equipment.ERamp Ramp = Atend.Base.Equipment.ERamp.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" پیچ و مهره : {0}", Ramp.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.ReCloser:
                        Atend.Base.Equipment.EReCloser ReCloser = Atend.Base.Equipment.EReCloser.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" ریکلوزر : {0}", ReCloser.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Rod:
                        Atend.Base.Equipment.ERod Rod = Atend.Base.Equipment.ERod.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" برقگیر : {0}", Rod.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.SectionLizer:
                        Atend.Base.Equipment.ESectionLizer SectionLizer = Atend.Base.Equipment.ESectionLizer.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" سکشن لایزر : {0}", SectionLizer.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.SelfKeeper:
                        Atend.Base.Equipment.ESelfKeeper SelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" کابل خودنگهدار : {0}", SelfKeeper.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.SelfKeeperTip:
                        Atend.Base.Equipment.ESelfKeeperTip SelfKeepertip = Atend.Base.Equipment.ESelfKeeperTip.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" تیپ بندی کابل خودنگهدار : {0}", SelfKeepertip.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.StreetBox:
                        Atend.Base.Equipment.EStreetBox StreetBox = Atend.Base.Equipment.EStreetBox.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" شالتر : {0}", StreetBox.Name);
                        break;

                    case (int)Atend.Control.Enum.ProductType.Transformer:
                        Atend.Base.Equipment.ETransformer Transformer = Atend.Base.Equipment.ETransformer.SelectByXCode(_ContainerPackage.XCode);
                        name = string.Format(" ترانس : {0}", Transformer.Name);
                        break;
                }

                return false;
            }

            return true;
        }

        public static bool CheckGridValidation(DataGridView GridView , int ColumnIndex)
        {
            bool Answer = true;
            if (GridView.Rows.Count > 0)
            {
                for (int RowCounter = 0; RowCounter < GridView.Rows.Count; RowCounter++)
                {
                    if (GridView.Rows[RowCounter].Cells[ColumnIndex].Value == null)
                    {
                        Answer = false;
                    }
                    else
                    {
                        if (!Atend.Control.NumericValidation.DoubleConverter(GridView.Rows[RowCounter].Cells[ColumnIndex].Value.ToString()))
                        {
                            Answer = false;
                        }
                    }
                }
            }

            return Answer;
        }

        public static void SetGridToCurrentSelectedEquip(string SortBy, object[] Values, System.Data.DataTable _DataTable, DataGridView _DataGridView, System.Windows.Forms.Form _Form)
        {
            object[] objs = Values;
            //objs[0] = d1.ProductCode;
            //objs[1] = "00000000-0000-0000-0000-000000000000";
            _DataTable.DefaultView.Sort = SortBy; //"Code,XCode";
            int position = _DataTable.DefaultView.Find(objs); //dtMerge.DefaultView.Find(objs);

            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("position:{0}\n", position);
            CurrencyManager _CurrencyManager = (CurrencyManager)_Form.BindingContext[_DataTable];
            _CurrencyManager.Position = position;

            //_DataGridView.Rows[position].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;

        }

    }
}
