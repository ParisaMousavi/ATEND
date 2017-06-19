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
    public partial class frmCrossSectionComment : Form
    {
        int _typeCode = 0;
        double _crossSection = 0;
        frmCrossSectionResult frmCrossResult;
        public int _Top = 0;
        bool ForceToClose = false;
        int _ProductType;
        public frmCrossSectionComment(frmCrossSectionResult frmcrResult,int typeCode,double crossSection,int ProductType)
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
            frmCrossResult = frmcrResult;
            _typeCode = typeCode;
            _crossSection = crossSection;
            _ProductType = ProductType;
        }
        private void BindDataToCboCondTip()
        {
            if (_ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            {
                cboCondTip.ValueMember = "XCode";
                cboCondTip.DisplayMember = "Name";
                cboCondTip.DataSource = Atend.Base.Equipment.EConductorTip.SearchConductorConductorTipConductorTypeX(_crossSection, _typeCode);
            }
            if (_ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
            {
                cboCondTip.ValueMember = "XCode";
                cboCondTip.DisplayMember = "Name";
                cboCondTip.DataSource = Atend.Base.Equipment.ESelfKeeperTip.SearchConductorSelfKeeperTipConductorTypeX(_crossSection, _typeCode);
            }

            //if (_ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel))
            //{
            //    cboCondTip.ValueMember = "XCode";
            //    cboCondTip.DisplayMember = "Name";
            //    //cboCondTip.DataSource = Atend.Base.Equipment.EGroundCabelTip.SearchConductorConductorTipConductorTypeX(_crossSection, _typeCode);
            //}
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

            BindDataToCboCondTip();
            //MessageBox.Show(_Top.ToString());
            this.Top = _Top;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.Top.ToString());
            if (cboCondTip.Items.Count > 0)
            {
                frmCrossResult.BindDataToGridCell(new Guid(cboCondTip.SelectedValue.ToString()));
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}