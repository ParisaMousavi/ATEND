using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Design
{
    public partial class frmDesignSaveAs : Form
    {
        bool ForceToClose = false;

        public frmDesignSaveAs()
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

        private void frmDesignSaveAs_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            string str = "";
            saveFileDialog1.FileName = Atend.Control.Common.DesignName.Replace(".DWG", "");
            saveFileDialog1.Filter = "AutoCAD File (*.DWG)|*.DWG";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                str = saveFileDialog1.FileName;
            }

            if (str != "")
            {
                string[] s = str.Split('\\');
                txtName.Text = s[s.Length - 1].Replace(".DWG", "").ToString();
                txtPath.Text = str.Substring(0, str.Length - s[s.Length - 1].Length - 1);
            }

            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (!System.IO.Directory.Exists(string.Format(@"{0}\{1}", txtPath.Text, txtName.Text)))
                    System.IO.Directory.CreateDirectory(string.Format(@"{0}\{1}", txtPath.Text, txtName.Text));

                string source = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX"));
                string destination = string.Format(@"{0}\{1}\{2}", txtPath.Text, txtName.Text, string.Format("{0}.ATNX", txtName.Text));
                System.IO.File.Copy(source, destination);

                try
                {
                //    System.IO.FileInfo fi = new System.IO.FileInfo(destination); //frmOpenDesign.txtFilePath.Text
                //    string Destination = fi.Directory.FullName;
                //    Atend.Global.Acad.DrawinOperation dOperation = new Atend.Global.Acad.DrawinOperation();
                //    dOperation.GetFileFromAtendFile(destination, Destination);
                //    string NewName = fi.FullName.Replace(".ATNX", ".DWG");
                //    MessageBox.Show(NewName);
                //    Atend.Control.Common.DesignFullAddress = Destination;
                //    Atend.Control.Common.DesignName = fi.Name.Replace(".ATNX", ".DWG");
                //    Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, fi.Name.Replace(".ATNX", ".MDB"));

                //    DocumentCollection _DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                //    if (System.IO.File.Exists(NewName))
                //    {
                //        _DocumentCollection.Open(NewName,false);
                //    }
                //    else
                //    {
                //        ed.WriteMessage("File was not exist : {0} \n", NewName);
                //    }
                //    Atend.Base.Acad.AT_COUNTER.ReadAll();
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("OPEN ERROR : {0} \n", ex.Message);
                }
            }
            catch (System.Exception ex)
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "خطا در ذخیره کردن اطلاعات";
                notification.Msg = "اطلاعات ثبت نگردید";
                notification.ShowStatusBarBalloon();
                return; 
            }

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}