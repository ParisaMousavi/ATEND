using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Calculating
{
    public partial class frmRudSurfaceGraphic : Form
    {
        bool ForceToClose = false;
        public frmRudSurfaceGraphic()
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

        private void frmRudSurfaceGraphic_Paint(object sender, PaintEventArgs e)
        {
            Pen red = new Pen(Color.Red,1);
            float x1 = 20.0F, y1 = 20.0F;
            float x2 = 900.0F, y2 = 900.0F;
            Point pBaseStart = new Point(0,400);
            Point pBaseEnd = new Point(400,400);
            e.Graphics.DrawLine(red,pBaseStart,pBaseEnd);
        }

        private void frmRudSurfaceGraphic_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }
    }
}