using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmEditDrawGroundPost01 : Form
    {

        bool AllowToclose=true;
        public Guid GPCode;
        public int PostCode;

        public int transformerCount = 0;
        public int MiddlejackPanelCount = 0;
        public int WeekJackPAnelCount = 0;
        public int GroundPostProductCode = 0;
        public ArrayList arMiddleJAckPAnel = new ArrayList();
        public ArrayList arweekJackPanel = new ArrayList();
        public ArrayList arTransformer = new ArrayList();
        bool ForceToClose = false;


        public frmEditDrawGroundPost01()
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

        private bool Validation()
        {
            bool flag = true;
            if (chkCapacity.Checked)
            {
                if (string.IsNullOrEmpty(txtCapacity.Text))
                {
                    MessageBox.Show("لطفا ظرفیت را وارد کنید", "خطا");
                    txtCapacity.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(txtCapacity.Text))
                {
                    MessageBox.Show("لطفا ظرفیت را با فرمت مناسب وارد نمایید", "خطا");
                    txtCapacity.Focus();
                    return false;
                }
            }

            return true;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            double capacity = -1;
            //int cellCount = -1;
            if (Validation())
            {

                if (chkCapacity.Checked)
                    capacity = Convert.ToDouble(txtCapacity.Text);
                //if (chkCellCount.Checked)
                //    cellCount = Convert.ToInt32(nudCellCount.Value);

                gvDisconnector.AutoGenerateColumns = false;
                gvDisconnector.DataSource = Atend.Base.Equipment.EGroundPost.DrawSearch(capacity);
            }
        }

        private void gvDisconnector_Click(object sender, EventArgs e)
        {
            if (gvDisconnector.Rows.Count > 0)
            {
                Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByCode(
                    Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value));

                txtName.Text = groundPost.Name;
                txtCellCount.Text = groundPost.CellCount.ToString();
                if (groundPost.GroundType == 0)
                {
                    txtGroundType.Text = "روزمینی";
                }
                else
                {
                    txtGroundType.Text = "زیرزمینی";
                }
                //**
                if (groundPost.Type == 0)
                {
                    txtType.Text = "عمومی";
                }
                if (groundPost.Type == 1)
                {
                    txtType.Text = "اختصاصی";
                }
                if (groundPost.Type == 2)
                {
                    txtType.Text = "پاساژ";
                }
                //**

                if (groundPost.AdvanceType == 0)
                {
                    txtAdvanceType.Text = "معمولی";
                }
                if (groundPost.AdvanceType == 1)
                {
                    txtAdvanceType.Text = "کامپکت";
                }

                if (groundPost.AdvanceType == 2)
                {
                    txtAdvanceType.Text = "کیوسک";
                }


                txtSelectedCapacity.Text = groundPost.Capacity.ToString();
            }

        }

        private void frmEditDrawGroundPost01_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToclose)
            {
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowToclose = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //if (!Atend.Global.Desig.NodeTransaction.DeleteGroundPost(GPCode))
            //{
            //    ed.WriteMessage("\nEXIT\n");
            //    return;
            //}

            //if (Validation())
            //{
            //    Atend.Base.Equipment.EGroundPost groundPost = Atend.Base.Equipment.EGroundPost.SelectByCode(
            //        Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value));
            //    GroundPostProductCode = groundPost.Code;
            //    DataTable dtWeekJackPanel = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndTypeAndTAbleType(groundPost.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost), Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel));
            //    DataTable dtMiddleJAckanel = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndTypeAndTAbleType(groundPost.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost), Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel));
            //    DataTable dtTransform = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndTypeAndTAbleType(groundPost.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost), Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer));
            //    transformerCount = dtTransform.Rows.Count;
            //    MiddlejackPanelCount = dtMiddleJAckanel.Rows.Count;

            //    MiddlejackPanelCount = 0;
            //    foreach (DataRow dr in dtMiddleJAckanel.Rows)
            //    {

            //        int MiddleCounter = Convert.ToInt32(dr["Count"]);
            //        for (int i = 1; i <= MiddleCounter; i++)
            //        {
            //            arMiddleJAckPAnel.Add(dr["ProductCode"].ToString());
            //            MiddlejackPanelCount++;
            //        }
            //    }

            //    //WeekJackPAnelCount = dtWeekJackPanel.Rows.Count;
            //    WeekJackPAnelCount = 0;
            //    foreach (DataRow dr in dtWeekJackPanel.Rows)
            //    {

            //        int WeekCounter = Convert.ToInt32(dr["Count"]);
            //        for (int i = 1; i <= WeekCounter; i++)
            //        {
            //            arweekJackPanel.Add(dr["ProductCode"]);
            //            WeekJackPAnelCount++;
            //        }
            //    }

            //    transformerCount = 0;
            //    foreach (DataRow dr in dtTransform.Rows)
            //    {

            //        int TransformerCounter = Convert.ToInt32(dr["Count"]);
            //        for (int i = 1; i <= TransformerCounter; i++)
            //        {
            //            arTransformer.Add(dr["ProductCode"]);
            //            transformerCount++;
            //        }

            //    }

            //    PostCode = Convert.ToInt32(gvDisconnector.SelectedRows[0].Cells[0].Value.ToString());

            //    AllowToclose = true;
            //}

        }

        private void frmEditDrawGroundPost01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            double capacity = -1;
            //int cellCount = -1;


            if (chkCapacity.Checked)
                capacity = Convert.ToDouble(txtCapacity.Text);
            //if (chkCellCount.Checked)
            //    cellCount = Convert.ToInt32(nudCellCount.Value);

            gvDisconnector.AutoGenerateColumns = false;
            gvDisconnector.DataSource = Atend.Base.Equipment.EGroundPost.DrawSearch(capacity);

            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (int.Parse(gvDisconnector.Rows[i].Cells[0].Value.ToString()) == PostCode)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

        }
    }
}