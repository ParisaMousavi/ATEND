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
    public partial class frmEditDrawGroundPost : Form
    {

        public int SelectedPostCode;
        public Guid DPCode;
        bool ForceToClose = false;

        public frmEditDrawGroundPost()
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

        private void frmEditDrawGroundPost_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Atend.Base.Equipment.EGroundPost GPost = Atend.Base.Equipment.EGroundPost.SelectByCode(SelectedPostCode);

            txtName.Text = GPost.Name;
            txtSelectedCapacity.Text = GPost.Capacity.ToString();

            DataTable JPanel = Atend.Base.Equipment.EJAckPanel.SelectAll();
            gvMiddleAll.AutoGenerateColumns = false;
            gvMiddleAll.DataSource = JPanel;
            


            DataTable WPanel = Atend.Base.Equipment.EJackPanelWeek.SelectAll();
            gvWeekAll.AutoGenerateColumns = false;
            gvWeekAll.DataSource = WPanel;
            


            DataTable Trans = Atend.Base.Equipment.ETransformer.SelectAll();
            gvTransAll.AutoGenerateColumns = false;
            gvTransAll.DataSource = Trans;
            

            //Atend.Base.Design.DPackage PostPack = Atend.Base.Design.DPackage.SelectByNodeCode(Nodecode);

            SetGrideMiddle();
            SetGrideWeek();
            SetGrideTrans();


        }


        private void SetGrideMiddle()
        {
            //Atend.Base.Design.DPackage PostPack = Atend.Base.Design.DPackage.SelectByNodeCode(Nodecode);
            DataTable PackTbl = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(DPCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel);// SelectByParentCodeAndType(DPCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel);

            DataTable JPanelSelected = new DataTable();
            DataColumn Col1 = new DataColumn("Code", typeof(int));
            DataColumn Col2 = new DataColumn("Name", typeof(string));

            JPanelSelected.Columns.Add(Col1);
            JPanelSelected.Columns.Add(Col2);

            foreach (DataRow Dr in PackTbl.Rows)
            {
                Atend.Base.Equipment.EJAckPanel JP = Atend.Base.Equipment.EJAckPanel.SelectByCode(Convert.ToInt32(Dr["ProductCode"].ToString()));
                DataRow JPRow = JPanelSelected.NewRow();
                JPRow["Code"] = JP.Code;
                JPRow["Name"] = JP.Name;
                JPanelSelected.Rows.Add(JPRow);
            }

            gvMiddleSelected.AutoGenerateColumns = false;
            gvMiddleSelected.DataSource = JPanelSelected;
            

        }

        private void SetGrideWeek()
        {
            //Atend.Base.Design.DPackage PostPack = Atend.Base.Design.DPackage.SelectByNodeCode(Nodecode);
            DataTable PackTbl = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(DPCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel); //SelectByParentCodeAndType(DPCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel);

            DataTable JPanelWSelected = new DataTable();
            DataColumn Col1 = new DataColumn("Code", typeof(int));
            DataColumn Col2 = new DataColumn("Name", typeof(string));

            JPanelWSelected.Columns.Add(Col1);
            JPanelWSelected.Columns.Add(Col2);

            foreach (DataRow Dr in PackTbl.Rows)
            {
                Atend.Base.Equipment.EJackPanelWeek JPW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(Convert.ToInt32(Dr["ProductCode"].ToString()));
                DataRow JPRow = JPanelWSelected.NewRow();
                JPRow["Code"] = JPW.Code;
                JPRow["Name"] = JPW.Name;
                JPanelWSelected.Rows.Add(JPRow);
            }
            
            gvWeekSelected.AutoGenerateColumns = false;
            gvWeekSelected.DataSource = JPanelWSelected;


        }


        private void SetGrideTrans()
        {
            //Atend.Base.Design.DPackage PostPack = Atend.Base.Design.DPackage.SelectByNodeCode(Nodecode);
            DataTable PackTbl = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(DPCode, (int)Atend.Control.Enum.ProductType.Transformer);// SelectByParentCodeAndType(DPCode, (int)Atend.Control.Enum.ProductType.Transformer);

            DataTable TransSelected = new DataTable();
            DataColumn Col1 = new DataColumn("Code", typeof(int));
            DataColumn Col2 = new DataColumn("Name", typeof(string));

            TransSelected.Columns.Add(Col1);
            TransSelected.Columns.Add(Col2);

            foreach (DataRow Dr in PackTbl.Rows)
            {
                Atend.Base.Equipment.ETransformer Trans = Atend.Base.Equipment.ETransformer.SelectByCode(Convert.ToInt32(Dr["ProductCode"].ToString()));
                DataRow TRow = TransSelected.NewRow();
                TRow["Code"] = Trans.Code;
                TRow["Name"] = Trans.Name;
                TransSelected.Rows.Add(TRow);
            }

            gvTransSelected.AutoGenerateColumns = false;
            gvTransSelected.DataSource = TransSelected;
            

        }

    }
}