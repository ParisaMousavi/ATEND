using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Calculating
{
    public partial class frmSetDefaultMec : Form
    {
        bool ForceToClose = false;
        public frmSetDefaultMec()
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

        private void button1_Click(object sender, EventArgs e)
        {
            Atend.Design.frmWeather Weather = new Atend.Design.frmWeather();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Weather);
        }

        private void btnNetCross_Click(object sender, EventArgs e)
        {
            Atend.Calculating.frmNetWorkCross NetCross = new frmNetWorkCross();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(NetCross);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            if (Validation())
            {
                Atend.Base.Calculating.CSetDefaultMec DefMec = Atend.Base.Calculating.CSetDefaultMec.AccessSelect();

                Atend.Base.Calculating.CPower.AccessDelete();

                DefMec.Distance = Convert.ToDouble(txtDistance.Text);
                DefMec.Start = Convert.ToDouble(txtStart.Text);
                DefMec.End = Convert.ToDouble(txtEnd.Text);
                DefMec.NetCross = Convert.ToInt32(cboNetCross.SelectedValue.ToString());
                DefMec.TrustBorder = Convert.ToDouble(txtTrustBorder.Text);
                DefMec.Uts = Convert.ToDouble(txtUTS.Text);

                for (int i = 0; i < lstPower.Items.Count; i++)
                {
                    Atend.Base.Calculating.CPower defPower = new Atend.Base.Calculating.CPower();
                    defPower.Power = Convert.ToDouble(lstPower.Items[i].ToString());
                    ed.WriteMessage("Power={0}\n", defPower.ToString());
                    if (!defPower.AccessInsert())
                    {
                        ed.WriteMessage("Failed Insert Power\n");
                    }
                }
                ed.WriteMessage("UPDATE\n");
                if (!DefMec.AccessUpdate())
                {
                    ed.WriteMessage("Failed Update\n");
                }
                else
                {
                    Close();
                }
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtUTS.Text))
            {
                MessageBox.Show("لطفا مقدار UTS را وارد نمایید", "خطا");
                txtUTS.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtStart.Text))
            {
                MessageBox.Show("لطفا مقدار شروع دما را وارد نمایید", "خطا");
                txtStart.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtEnd.Text))
            {
                MessageBox.Show("لطفا مقدار پایات دما را وارد نمایید", "خطا");
                txtEnd.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDistance.Text))
            {
                MessageBox.Show("لطفا مقدار فاصله دمایی را وارد نمایید", "خطا");
                txtDistance.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTrustBorder.Text))
            {
                MessageBox.Show("لطفا مقدار حاشیه اطمینان را وارد نمایید", "خطا");
                txtTrustBorder.Focus();
                return false;
            }


            if (!Atend.Control.NumericValidation.DoubleConverter(txtUTS.Text))
            {
                MessageBox.Show("لطفا UTS را با فرمت مناسب وارد نمایید", "خطا");
                txtUTS.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtStart.Text))
            {
                MessageBox.Show("لطفا شروع دما را با فرمت مناسب وارد نمایید", "خطا");
                txtStart.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtEnd.Text))
            {
                MessageBox.Show("لطفا پایان دما را با فرمت مناسب وارد نمایید", "خطا");
                txtEnd.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtDistance.Text))
            {
                MessageBox.Show("لطفا فاصله دمایی را با فرمت مناسب وارد نمایید", "خطا");
                txtDistance.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtTrustBorder.Text))
            {
                MessageBox.Show("لطفا حاشیه اطمینان را با فرمت مناسب وارد نمایید", "خطا");
                txtTrustBorder.Focus();
                return false;
            }
            return true;
        }

        private void BindDataToNetCross()
        {
            cboNetCross.DisplayMember = "Name";
            cboNetCross.ValueMember = "Code";
            cboNetCross.DataSource = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
        }

        private void frmSetDefaultMec_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToNetCross();
            if (cboNetCross.Items.Count > 0)
                cboNetCross.SelectedIndex = 0;
            cboPower.SelectedIndex = 0;
            Atend.Base.Calculating.CSetDefaultMec DefMec = Atend.Base.Calculating.CSetDefaultMec.AccessSelect();
            txtTrustBorder.Text = DefMec.TrustBorder.ToString();
            txtUTS.Text = DefMec.Uts.ToString();
            txtTrustBorder.Text = DefMec.TrustBorder.ToString();
            txtStart.Text = DefMec.Start.ToString();
            txtEnd.Text = DefMec.End.ToString();
            txtDistance.Text = DefMec.Distance.ToString();
            //cboNetCross.SelectedValue = DefMec.NetCross;
            DataTable dtPower = Atend.Base.Calculating.CPower.AccessSelect();
            foreach (DataRow dr in dtPower.Rows)
            {
                lstPower.Items.Add(dr["Power"].ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bool Chk = true;
            if (lstPower.Items.Count == 0)
            {
                lstPower.Items.Add(cboPower.Text);
            }

            for (int i = 0; i < lstPower.Items.Count; i++)
            {
                if (cboPower.Text == lstPower.Items[i].ToString())
                {
                    Chk = false;
                }
            }
            if (Chk)
            {
                lstPower.Items.Add(cboPower.Text);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstPower.SelectedIndex != -1)
                lstPower.Items.RemoveAt(lstPower.SelectedIndex);
        }

    }
}