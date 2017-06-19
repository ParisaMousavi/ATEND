using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;






namespace Atend.Global.Design
{
    public partial class frmDrawBranch : Form
    {
        public double Length;
        bool AllowClosing = true;
        private Guid _StartPole, _EndPole;
        bool ForceToClose = false;


        public frmDrawBranch(Guid StartPole, Guid EndPole)
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
            _StartPole = StartPole;
            _EndPole = EndPole;
            InitializeComponent();
            txtLength.Text = Convert.ToString(Length);
        }

        private void frmDrawBranch_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            txtLength.Text = Length.ToString();
            btnOk.Focus();
        }

        private bool Validation()
        {
            if (!Atend.Control.NumericValidation.DoubleConverter(txtLength.Text))
            {
                MessageBox.Show("لطفا طول هادی را با فرمت مناسب وارد نمایید");
                txtLength.Focus();
                return false;
            }

            if (_StartPole != Guid.Empty && _EndPole != Guid.Empty)
            {
                Atend.Base.Design.DBranch _DBranch = Atend.Base.Design.DBranch.AccessSelectByRigthAndLeftNodeCode(_StartPole, _EndPole);
                if (_DBranch.Code != Guid.Empty)
                {
                    if (Convert.ToDouble(txtLength.Text) != _DBranch.Lenght)
                    {
                        MessageBox.Show("لطفا طول سیم را تصحیح نمایید" + "\n" + "طول مورد تایید :" + _DBranch.Lenght.ToString());
                        txtLength.Text = _DBranch.Lenght.ToString();
                        btnOk.Focus();
                        return false;
                    }
                }
                else
                {
                    _DBranch = Atend.Base.Design.DBranch.AccessSelectByRigthAndLeftNodeCode(_EndPole, _StartPole);
                    if (_DBranch.Code != Guid.Empty)
                    {
                        if (Convert.ToDouble(txtLength.Text) != _DBranch.Lenght)
                        {
                            MessageBox.Show("لطفا طول سیم را تصحیح نمایید" + "\n" + "طول مورد تایید :" + _DBranch.Lenght.ToString());
                            txtLength.Text = _DBranch.Lenght.ToString();
                            btnOk.Focus();
                            return false;
                        }
                    }

                }
            }
            return true;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Length = double.Parse(txtLength.Text);
                AllowClosing = true;
            }
            else
            {
                AllowClosing = false;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtLength_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmDrawBranch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClosing)
            {
                e.Cancel = true;
            }
            AllowClosing = true;
        }



    }
}