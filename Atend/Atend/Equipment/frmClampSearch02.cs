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
    public partial class frmClampSearch02 : Form
    {
        frmClamp02 frmClamp;
        bool ForceToClose = false;

        public frmClampSearch02(frmClamp02 _Clamp)
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
            frmClamp = _Clamp;
        }

        private void frmClampSearch02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Search();

        }


        private void ShowNumberColumn(DataGridView _DataGridView)
        {
            //DataGridViewTextBoxColumn tx=new DataGridViewTextBoxColumn()
            //DataGridViewColumn c = new DataGridViewColumn();
            //c.Index = 0;
            //c.Name = "COLNO";
            //c.HeaderText = "ردیف";
            //_DataGridView.Columns.Add("COLNO", "ردیف");
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


        private void Search()
        {
            gvProduct.AutoGenerateColumns = false;
            gvProduct.DataSource = Atend.Base.Equipment.EClamp.SearchLocal(tstName.Text);
            ShowNumberColumn(gvProduct);
        }

        private void tstName_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gvProduct_DoubleClick(object sender, EventArgs e)
        {
            if (gvProduct.Rows.Count > 0)
            {
                frmClamp.BindDataToOwnControl(new Guid(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString()));
                Close();
            }
        }
    }
}