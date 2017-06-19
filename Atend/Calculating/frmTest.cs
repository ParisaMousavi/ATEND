using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Calculating
{
    public partial class frmTest : Form
    {
        bool ForceToClose = false;
        public frmTest()
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

        Atend.Global.Calculation.Sectioning section;

        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        int PID;
        DataTable DT = new DataTable();

        private void button1_Click(object sender, EventArgs e)
        {
            //ed.WriteMessage("Start\n ");
            //section.DetermineSection();
            //ed.WriteMessage("Start\n");
            //section.FinalSection();
            //section.TransferSectionToDataBase();
            //section.TransferSectionToDataBase();

            ed.WriteMessage("Success\n");
            DataTable GroupDT = new DataTable();

            DataColumn Col0 = new DataColumn("Code");
            DataColumn Col1 = new DataColumn("Name");
            DataColumn Col2 = new DataColumn("Count");
            DataColumn Col3 = new DataColumn("Unit");
            DataColumn Col4 = new DataColumn("IsProduct");

            GroupDT.Columns.Add(Col0);
            GroupDT.Columns.Add(Col1);
            GroupDT.Columns.Add(Col2);
            GroupDT.Columns.Add(Col3);
            GroupDT.Columns.Add(Col4);


            ArrayList CodeArray = new ArrayList();

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (DT.Rows[i]["Code"].ToString() != "0")
                {
                    if (CodeArray.IndexOf(Convert.ToInt32(DT.Rows[i]["Code"].ToString())) == -1)
                    {
                        CodeArray.Add(Convert.ToInt32(DT.Rows[i]["Code"].ToString()));
                        DataRow[] ArrayDr = DT.Select("Code = " + Convert.ToInt32(DT.Rows[i]["Code"].ToString()));

                        int Count = 0;
                        for (int j = 0; j < ArrayDr.Length; j++)
                            Count += Convert.ToInt32(ArrayDr[j]["Count"].ToString());

                        DataRow GroupDR = GroupDT.NewRow();
                        GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                        GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                        GroupDR["Count"] = Count;
                        GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                        GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();

                        GroupDT.Rows.Add(GroupDR);
                    }
                }
                else
                {
                    DataRow GroupDR = GroupDT.NewRow();
                    GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                    GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                    GroupDR["Count"] = DT.Rows[i]["Count"].ToString(); ;
                    GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                    GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();
                    GroupDT.Rows.Add(GroupDR);
                }
            }

            dataGridView1.DataSource = GroupDT;
        }

        //private void frmTest_Load(object sender, EventArgs e)
        //{

        //    DataColumn Col0 = new DataColumn("Code");
        //    DataColumn Col1 = new DataColumn("Name");
        //    DataColumn Col2 = new DataColumn("Count");
        //    DataColumn Col3 = new DataColumn("Unit");
        //    DataColumn Col4 = new DataColumn("IsProduct");

        //    DT.Columns.Add(Col0);
        //    DT.Columns.Add(Col1);
        //    DT.Columns.Add(Col2);
        //    DT.Columns.Add(Col3);
        //    DT.Columns.Add(Col4);



        //    DataTable NodeTable = Atend.Base.Design.DNode.SelectByDesignCode(Atend.Control.Common.SelectedDesignCode);
        //    ed.WriteMessage(NodeTable.Rows.Count.ToString());

        //    int i = 0;

        //    for (i = 0; i < NodeTable.Rows.Count; i++)
        //    {
        //        Guid NodeID = new Guid(NodeTable.Rows[i]["Code"].ToString());
        //        Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByNodeCode(NodeID);// SelectByNodeCode(NodeID);
        //        DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code);// SelectByParentCode(Package.Code);

        //        ed.WriteMessage(Package.Code.ToString());

        //        DataRow dr = DT.NewRow();

        //        dr[2] = Package.Count;//Childs.Rows[i]["Count"];

        //        int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());

        //        ed.WriteMessage("Product Code = " + ProCode.ToString() + "\n");


        //        PID = 0;
        //        dr[1] = FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/,  ProCode);


        //        ed.WriteMessage("Name = " + dr[1].ToString() + "\n" + " Product Code = " + PID.ToString() + "\n");
        //        Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID);

        //        dr[0] = Product.Code;
        //        dr[3] = Product.Unit;

        //        if (Product.IsProduct)
        //            dr[4] = 1;
        //        else
        //            dr[4] = 0;
        //        DT.Rows.Add(dr);

        //        ChildFind(PackageTable);

        //    }

        //    dataGridView1.DataSource = DT;

        //    //DT.Rows.Add(DR);

        //    //dataGridView1.DataSource = DT;

        //    //Col.DataType = Type.GetType(;

        //    //DataTable GroupDT = new DataTable();

        //    //GroupDT.Columns.Add(Col0);
        //    //GroupDT.Columns.Add(Col1);
        //    //GroupDT.Columns.Add(Col2);
        //    //GroupDT.Columns.Add(Col3);
        //    //GroupDT.Columns.Add(Col4);


        //    //ArrayList CodeArray = new ArrayList();

        //    //for (i = 0; i < DT.Rows.Count; i++)
        //    //{
        //    //    if (CodeArray.IndexOf(Convert.ToInt32(DT.Rows[i]["Code"].ToString())) == -1)
        //    //    {
        //    //        CodeArray.Add(Convert.ToInt32(DT.Rows[i]["Code"].ToString()));
        //    //        DataRow[] ArrayDr = DT.Select("Code = " + Convert.ToInt32(DT.Rows[i]["Code"].ToString()));

        //    //        DataRow GroupDR = GroupDT.NewRow();
        //    //        GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
        //    //        GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
        //    //        GroupDR["Count"] = ArrayDr.GetLength(1);
        //    //        GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
        //    //        GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();

        //    //        GroupDT.Rows.Add(GroupDR);
        //    //    }
        //    //}
        //    //for(i =0 ; i < CodeArray.Count ; i++ )
        //    //{

        //    //}

        //}

        private void ChildFind(DataTable Childs)
        {
            if (Childs.Rows.Count == 0)
                return;

            //DataTable Childs = Atend.Base.Design.DPackage.SelectByParentCode(Package.Code);

            for (int i = 0; i < Childs.Rows.Count; i++)
            {
                //ed.WriteMessage(Childs.Rows[i]["Count"];

                DataRow dr = DT.NewRow();
                dr[2] = Childs.Rows[i]["Count"];
                int ProCode = Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());

                PID = 0;
                dr[1] = FindNameAndProductCode(Convert.ToInt32(Childs.Rows[i]["Type"].ToString()), ProCode);
                Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID);

                ed.WriteMessage("\n Type = " + Childs.Rows[i]["Type"].ToString() + "\n PID = " + PID.ToString() + "\n");
                if (PID == 0 && Convert.ToInt32(Childs.Rows[i]["Type"].ToString()) == 26)
                {
                    //continue;
                    //dr[1] = FindNameOperation(
                    //Atend.Base.Equipment.EOperation eOP = Atend.Base.Equipment.EOperation.SelectByCode(Code);

                    ed.WriteMessage("\nProCode = " + ProCode.ToString());
                    Atend.Base.Base.BProduct Pro = Atend.Base.Base.BProduct.Select_ByIdX(ProCode);
                    ed.WriteMessage("\nProName = " + Pro.Name);

                    dr[1] = Pro.Name;
                    dr[0] = Pro.Code;
                    dr[3] = Pro.Unit;

                    if (Pro.IsProduct)
                        dr[4] = 1;
                    else
                        dr[4] = 0;

                    DT.Rows.Add(dr);
                    continue;
                }

                ed.WriteMessage("\nNAME  " + dr[1].ToString() + "\n");
                dr[0] = Product.Code;
                dr[3] = Product.Unit;

                if (Product.IsProduct)
                    dr[4] = 1;
                else
                    dr[4] = 0;

                DT.Rows.Add(dr);

                Guid Package = new Guid(Childs.Rows[i]["Code"].ToString());
                DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package);//SelectByParentCode(Package);
                ChildFind(PackageTable);
            }
        }


        //private string FindNameOperation(Guid Code)
        //{
        //    string DBName = string.Empty;
        //    Atend.Base.Equipment.EOperation eOP = Atend.Base.Equipment.EOperation.SelectByCode(Code);
        //    Atend.Base.Base.BProduct BP = Atend.Base.Base.BProduct.Select_ById(eOP.ProductID);

        //    DBName = BP.Name;
        //    PID = BP.Code;
        //    return DBName;
        //}

        private string FindNameAndProductCode(int Type, int ProductCode)
        {
            //Atend.Base.Equipment.EPole. p = new Atend.Base.Equipment.EPole();

            string DBName = string.Empty;

            switch ((Atend.Control.Enum.ProductType)Type)
            {

                case Atend.Control.Enum.ProductType.Pole:
                    Atend.Base.Equipment.EPole ePole = Atend.Base.Equipment.EPole.SelectByCode(ProductCode);
                    DBName = ePole.Name;
                    PID = ePole.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Conductor:
                    Atend.Base.Equipment.EConductor eConductor = Atend.Base.Equipment.EConductor.SelectByCode(ProductCode);
                    DBName = eConductor.Name;
                    PID = eConductor.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.AuoKey3p:
                    Atend.Base.Equipment.EAutoKey_3p eAuto = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(ProductCode);
                    DBName = eAuto.Name;
                    PID = eAuto.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Breaker:
                    Atend.Base.Equipment.EBreaker eBreaker = Atend.Base.Equipment.EBreaker.SelectByCode(ProductCode);
                    DBName = eBreaker.Name;
                    PID = eBreaker.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Bus:
                    Atend.Base.Equipment.EBus eBus = Atend.Base.Equipment.EBus.SelectByCode(ProductCode);
                    DBName = eBus.Name;
                    PID = eBus.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.CatOut:
                    Atend.Base.Equipment.ECatOut eCatOut = Atend.Base.Equipment.ECatOut.SelectByCode(ProductCode);
                    DBName = eCatOut.Name;
                    PID = eCatOut.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.CT:
                    Atend.Base.Equipment.ECT eCT = Atend.Base.Equipment.ECT.SelectByCode(ProductCode);
                    DBName = eCT.Name;
                    PID = eCT.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.DB:
                    Atend.Base.Equipment.EDB eDB = Atend.Base.Equipment.EDB.SelectByCode(ProductCode);
                    DBName = eDB.Name;
                    PID = eDB.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.HeaderCabel:
                    Atend.Base.Equipment.EHeaderCabel eHeader = Atend.Base.Equipment.EHeaderCabel.SelectByCode(ProductCode);
                    DBName = eHeader.Name;
                    PID = eHeader.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Disconnector:
                    Atend.Base.Equipment.EDisconnector eDC = Atend.Base.Equipment.EDisconnector.SelectByCode(ProductCode);
                    DBName = eDC.Name;
                    PID = eDC.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Rod:
                    Atend.Base.Equipment.ERod eRod = Atend.Base.Equipment.ERod.SelectByCode(ProductCode);
                    DBName = eRod.Name;
                    PID = eRod.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Countor:
                    Atend.Base.Equipment.ECountor eCounter = Atend.Base.Equipment.ECountor.SelectByCode(ProductCode);
                    DBName = eCounter.Name;
                    PID = eCounter.ProductCode;
                    break;

                //case Atend.Control.Enum.ProductType.JackPanel:
                //    Atend.Base.Equipment.EJAckPanel eJack = Atend.Base.Equipment.EJAckPanel.SelectByCode(ProductCode);
                //    break;

                case Atend.Control.Enum.ProductType.PhotoCell:
                    Atend.Base.Equipment.EPhotoCell ePhotoCell = Atend.Base.Equipment.EPhotoCell.SelectByCode(ProductCode);
                    DBName = ePhotoCell.Name;
                    PID = ePhotoCell.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Phuse:
                    Atend.Base.Equipment.EPhuse ePhuse = Atend.Base.Equipment.EPhuse.SelectByCode(ProductCode);
                    DBName = ePhuse.Name;
                    PID = ePhuse.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.StreetBox:
                    Atend.Base.Equipment.EStreetBox eStreet = Atend.Base.Equipment.EStreetBox.SelectByCode(ProductCode);
                    DBName = eStreet.Name;
                    PID = eStreet.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Transformer:
                    Atend.Base.Equipment.ETransformer eTrans = Atend.Base.Equipment.ETransformer.SelectByCode(ProductCode);
                    DBName = eTrans.Name;
                    PID = eTrans.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.PT:
                    Atend.Base.Equipment.EPT ePT = Atend.Base.Equipment.EPT.SelectByCode(ProductCode);
                    DBName = ePT.Name;
                    PID = ePT.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Insulator:
                    Atend.Base.Equipment.EInsulator eInsulator = Atend.Base.Equipment.EInsulator.SelectByCode(ProductCode);
                    DBName = eInsulator.Name;
                    PID = eInsulator.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.ReCloser:
                    Atend.Base.Equipment.EReCloser eRecloser = Atend.Base.Equipment.EReCloser.SelectByCode(ProductCode);
                    DBName = eRecloser.Name;
                    PID = eRecloser.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.PhuseKey:
                    Atend.Base.Equipment.EPhuseKey ePhusekey = Atend.Base.Equipment.EPhuseKey.SelectByCode(ProductCode);
                    DBName = ePhusekey.Name;
                    PID = ePhusekey.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Consol:
                    Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.SelectByCode(ProductCode);
                    DBName = eConsol.Name;
                    PID = eConsol.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.PhusePole:
                    Atend.Base.Equipment.EPhusePole ePhusepole = Atend.Base.Equipment.EPhusePole.SelectByCode(ProductCode);
                    DBName = ePhusepole.Name;
                    PID = ePhusepole.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                    Atend.Base.Equipment.EJAckPanel eJackP = Atend.Base.Equipment.EJAckPanel.SelectByCode(ProductCode);
                    DBName = eJackP.Name;
                    PID = eJackP.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Khazan:
                    Atend.Base.Equipment.EKhazan eKhazan = Atend.Base.Equipment.EKhazan.SelectByCode(ProductCode);
                    DBName = eKhazan.Name;
                    PID = eKhazan.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.GroundPost:
                    Atend.Base.Equipment.EGroundPost ePost = Atend.Base.Equipment.EGroundPost.SelectByCode(ProductCode);
                    DBName = ePost.Name;
                    PID = ePost.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.AirPost:
                    Atend.Base.Equipment.EAirPost eAirpost = Atend.Base.Equipment.EAirPost.SelectByCode(ProductCode);
                    DBName = eAirpost.Name;
                    PID = eAirpost.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.WeekJackPanel:
                    Atend.Base.Equipment.EJackPanelWeek eJPW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(ProductCode);
                    DBName = eJPW.Name;
                    PID = eJPW.ProductCode;
                    break;
            }

            return DBName;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Atend.Base.Design.NodeTransaction.InsertAirPost(Convert.ToInt32(textBox1.Text));
            // foreach(Atend.Base.Acad.AcadGlobal.AirPostData.PostEquips post in Atend.Base.Acad.AcadGlobal.AirPostData.PostEquipInserted)
            //{
            //    ed.WriteMessage("CodeGuid= "+post.CodeGuid +" ParentCode = "+post.ParentCode+" ProductCode= "+post.ProductCode+" ProductType= "+post.ProductType +"\n");
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = Atend.Base.Equipment.EAirPost.SelectAllAndMerge();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView3.ReadOnly = true;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.AllowUserToDeleteRows = false;
            //dataGridView3.DataSource = Atend.Base.Design.DPackage.AccessSelectByParentCodeForConsol(new Guid(textBox1.Text));
            //dataGridView3.DataSource=

            //System.Data.DataTable dt = Atend.Control.Common.StatuseCode.Copy();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    ed.WriteMessage("Code : {0} Name:{1} \n",dr["Code"],dr["Name"]);
            //}


            //DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvPoleConsol.Columns["Column10"];
            //DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            //c.DataSource = dt;
            //c.DisplayMember = "Name";
            //c.ValueMember = "Code";
            //c.DataPropertyName = "IsExistance";
            //dataGridView3.Columns.Add(c);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Atend.Global.Utility.UReport Report = new Atend.Global.Utility.UReport();
            System.Data.DataTable Table = Report.CreateExcelStatus();
            dataGridView4.DataSource = Report.DT;
            
        }

        private void frmTest_Load_1(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }
    }
}