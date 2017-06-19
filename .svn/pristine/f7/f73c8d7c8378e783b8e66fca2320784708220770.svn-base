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
    public partial class frmRampSearch02 : Form
    {
        frmRamp02  frm;
        bool ForceToClose = false;

        public frmRampSearch02(frmRamp02 _frm)
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
            frm  = _frm;
        }

        private void Search()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = Atend.Base.Equipment.ERamp.SearchLocal(tstName.Text);
            ShowNumberColumn(dataGridView1);
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tstName_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Guid XCode = new Guid(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
                frm.BindDataToOwnControl(XCode);
                Close();
            }
        }

        private void frmInsulatorChainSearch02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Search();
        }
    }
}