using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmEditDrawKhazan : Form
    {
        bool AllowClose = false;
        public int Code;
        public Guid DpakageCode;
        bool ForceToClose = false;

        public frmEditDrawKhazan()
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

        private void frmDrawKhazan_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //Atend.Base.Acad.AcadGlobal.dPackageForKhazanTip = new Atend.Base.Design.DPackage();
            //Atend.Base.Acad.AcadGlobal.dPackageForKhazan.Clear();
            gvBankKhazan.AutoGenerateColumns = false;
            gvKhazan.AutoGenerateColumns = false;

            gvBankKhazan.DataSource = Atend.Base.Equipment.EKhazanTip.DrawSearch(txName.Text);
            gvKhazan.DataSource = null;

           

            for (int i = 0; i < gvBankKhazan.Rows.Count; i++)
            {
                if (int.Parse(gvBankKhazan.Rows[i].Cells["Column2"].Value.ToString()) == Code)
                {
                    gvBankKhazan.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }


            gvKhazan.DataSource = Atend.Base.Equipment.EKhazan.DrawSearch(
               Convert.ToInt32(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value));


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            gvBankKhazan.DataSource = Atend.Base.Equipment.EKhazanTip.DrawSearch(txName.Text);
            gvKhazan.DataSource = null;

        }

        private void gvBankKhazan_DoubleClick(object sender, System.EventArgs e)
        {
            //MessageBox.Show(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value.ToString());
            gvKhazan.DataSource = Atend.Base.Equipment.EKhazan.DrawSearch(
               Convert.ToInt32(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value));




            //MessageBox.Show(" COUNT :" + gvKhazan.Rows.Count.ToString());

        }

        private void gvBankKhazan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private bool Validation()
        {
            if (gvBankKhazan.SelectedRows.Count == 0 && gvKhazan.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (gvBankKhazan.Rows.Count > 0)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage(gvBankKhazan.SelectedRows[0].Cells["Column7"].Value.ToString() + "\n");
                //ed.WriteMessage(gvBankKhazan.SelectedRows[0].Index.ToString() + "\n");
                Atend.Base.Design.DPackage Pakage = Atend.Base.Design.DPackage.AccessSelectByCode(DpakageCode);// SelectByCode(DpakageCode);
                //Pakage. ProductCode = new Guid(gvBankKhazan.SelectedRows[0].Cells["Column7"].Value.ToString());

                //if (Pakage.Update())
                {
                    ed.WriteMessage("update is comment");
                    //Atend.Base.Acad.AT_INFO
                    Code = Convert.ToInt32(gvBankKhazan.SelectedRows[0].Cells["Column7"].Value.ToString());
                    AllowClose = true;
                    this.Close();
                }
                //else
                    //MessageBox.Show("انجام ويرايش امكانپذير نيست");
            }


            //if (Validation())
            //{
            //    //DataTable KhazanTipTable = Atend.Base.Equipment.EKhazanTip.SelectByCode(
            //    //    Convert.ToInt32(gvBankKhazan.CurrentRow.Index));
            //    Atend.Base.Acad.AcadGlobal.dPackageForKhazanTip.Count = 1;
            //    Atend.Base.Acad.AcadGlobal.dPackageForKhazanTip.Type = (int)Atend.Control.Enum.ProductType.BankKhazan;
            //    Atend.Base.Acad.AcadGlobal.dPackageForKhazanTip.ProductCode = Convert.ToInt32(
            //        gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells["Column7"].Value);



            //    Atend.Base.Design.DPackage dPackge;


            //    DataTable dt = (DataTable)gvKhazan.DataSource;
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        dPackge = new Atend.Base.Design.DPackage();
            //        dPackge.Count = Convert.ToInt32(dr["Count"]);
            //        dPackge.Type=Convert.ToInt32(dr["Type"]);
            //        dPackge.ProductCode = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);


            //        Atend.Base.Acad.AcadGlobal.dPackageForKhazan.Add(dPackge);
            //    }

            //    AllowClose = true;
            //}
            //else
            //{
            //    AllowClose = false;
            //}


        }

        private void frmDrawKhazan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AllowClose)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            this.Close();
        }

        private void gvBankKhazan_Click(object sender, EventArgs e)
        {
            if (gvBankKhazan.Rows.Count != 0)
            {
                DataTable st = Atend.Base.Equipment.EKhazan.DrawSearch(Convert.ToInt32(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value));
                MessageBox.Show("count : " + st.Rows.Count.ToString());
                gvKhazan.DataSource = st;
            }

        }


    }
}