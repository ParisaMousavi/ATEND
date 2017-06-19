using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Calculating
{
    public partial class frmHalterComment : Form
    {
       
        frmHalterResult frmHalterResult;
        public int _Top = 0;
        bool ForceToClose = false;
        double _Power;
        public frmHalterComment(frmHalterResult frmResult, double Power)
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
            frmHalterResult = frmResult;
            _Power = Power;
        }
        private void BindDataToCboPole()
        {
            
                cboPole.ValueMember = "XCode";
                cboPole.DisplayMember = "Name";
                cboPole.DataSource = Atend.Base.Equipment.EPole.SelectByPowerX(_Power);
           
       }


        private void cboCondTip_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboCondTip_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void frmCrossSectionComment_Load(object sender, EventArgs e)
        {

            if (ForceToClose)
                this.Close();

             BindDataToCboPole();
            //MessageBox.Show(_Top.ToString());
            this.Top = _Top;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.Top.ToString());
            if (cboPole.Items.Count > 0)
            {
                frmHalterResult.BindDataToGridCell(new Guid(cboPole.SelectedValue.ToString()));
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}