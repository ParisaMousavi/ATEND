using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Atend.Design
{
    public partial class frmFile : Form
    {
        public frmFile()
        {

            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Atend.Control.ConnectionString.LST = txtServerIP.Text;
            if (Atend.Global.Acad.DrawEquips.Dicision.IsThere())
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "شناسایی قفل";
                notification.Msg = "قفل نرم افزار شناسایی شد";
                notification.infoCenterBalloon();

            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "شناسایی قفل";
                notification.Msg = "قفل نرم افزار شناسایی نشد";
                notification.infoCenterBalloon();

            }
        }

        private void frmFile_Load(object sender, EventArgs e)
        {
            txtServerIP.Text = Atend.Control.ConnectionString.LST;
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Save()
        {
            Atend.Control.ConnectionString.LST = txtServerIP.Text;
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

    }
}