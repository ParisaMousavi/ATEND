using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

//get from tehran 7/15
namespace Atend.Design
{
    public partial class frmEditAngle : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        public double Angle=0;
        bool AllowToClose = true;

        public frmEditAngle(double _angle)
        {
            InitializeComponent();
            Angle = _angle;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();    
        }

        private void frmEditAngle_Load(object sender, EventArgs e)
        {
            txtAngle.Text = Angle.ToString();
            txtAngle.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Atend.Control.NumericValidation.DoubleConverter(txtAngle.Text))
                Angle = Convert.ToDouble(txtAngle.Text);
            else
            {
                ed.WriteMessage("لطفا زاویه چرخش را مشخص نمایید\n");
                AllowToClose = false;
            }
        }

        private void txtAngle_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmEditAngle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToClose)
            {
                e.Cancel = true;
            }
            AllowToClose = true;
        }
    }
}