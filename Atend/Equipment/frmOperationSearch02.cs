using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Equipment
{
    public partial class frmOperationSearch02 : Form
    {
        frmOperation02 frmOperation;
        bool ForceToClose = false;

        public frmOperationSearch02(frmOperation02 _frmOperation)
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
            frmOperation = _frmOperation;
        }

        private void Search()
        {
            //gvOperation.AutoGenerateColumns = false;
            //gvOperation.DataSource = Atend.Base.Base.BProduct.SelectAllX();

            gvOperation.AutoGenerateColumns = false;
            gvOperation.DataSource = Atend.Base.Base.BProduct.SearchX(Convert.ToInt32(Atend.Control.Enum.ProductType.Operation), tstName.Text);
            ShowNumberColumn(gvOperation);

        }

        private void ShowNumberColumn(DataGridView _DataGridView)
        {
            //DataGridViewTextBoxColumn tx=new DataGridViewTextBoxColumn()
            //DataGridViewColumn c = new DataGridViewColumn();
            //c.Index = 0;
            //c.Name = "COLNO";
            //c.HeaderText = "ردیف";
            _DataGridView.Columns.Add("COLNO", "ردیف");
            int counter = 1;
            //_DataGridView.Width = 10;

            foreach (DataGridViewRow gr in _DataGridView.Rows)
            {
                gr.Cells["COLNO"].Value = counter.ToString();
                counter++;
            }
            _DataGridView.EndEdit();
            _DataGridView.Refresh();
        }

        private void gvOperation_DoubleClick(object sender, EventArgs e)
        {
            if (gvOperation.Rows.Count > 0)
            {
                frmOperation.BindDataToOneControl(Convert.ToInt32(gvOperation.Rows[gvOperation.CurrentRow.Index].Cells[0].Value));
                Close();
            }
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmOperationSearch02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Search();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!Atend.Base.Base.BProduct.GetFromServer(Convert.ToInt32(Atend.Control.Enum.ProductType.Operation)))
                {
                    throw new System.Exception("Atend.Base.Base.BProduct.GetFromServer2 failed");
                }
                this.Cursor = Cursors.Default;
                Search();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("*** ERROR :{0} \n", ex.Message);

                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "خطا";
                notification.Msg = "به روز رسانی تجهیز مورد نظر امکان پذیر نمی باشد";
                notification.infoCenterBalloon();

                this.Cursor = Cursors.Default;
            }
        }
    }
}