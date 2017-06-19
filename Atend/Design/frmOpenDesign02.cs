using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Design
{
    public partial class frmOpenDesign02 : Form
    {

        bool AllowClosing = true;
        public string filePath;
        bool ForceToClose = false;

        public frmOpenDesign02()
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Atend files(*.ATNX)|*.ATNX";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(openFileDialog1.FileName);
                if (System.IO.File.Exists(openFileDialog1.FileName))
                {
                    txtFilePath.Text = openFileDialog1.FileName;
                }
            }
        }

        private bool Validation()
        {

            if (txtFilePath.Text == string.Empty)
            {

                MessageBox.Show("لطفا نام طرح را انتخاب نمایید", "خطا");
                txtFilePath.Focus();
                return false;
            }
            else
            {
                if (!File.Exists(txtFilePath.Text))
                {
                    MessageBox.Show("طرح انتخاب شده موجود نمی باشد", "خطا");
                    txtFilePath.Focus();
                    return false;
                }
            }

            return true;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Validation())
            {
                Atend.Control.Common.DesignId = 0;
                filePath = txtFilePath.Text;
                ////////////try
                ////////////{
                ////////////    FileInfo fi = new FileInfo(txtFilePath.Text);
                ////////////    string Destination = fi.Directory.FullName;
                ////////////    Atend.Global.Acad.DrawinOperation dOperation = new Atend.Global.Acad.DrawinOperation();
                ////////////    dOperation.GetFileFromAtendFile(txtFilePath.Text, Destination);
                ////////////    string NewName = fi.FullName.Replace(".ATNX", ".DWG");
                ////////////    Atend.Control.Common.DesignFullAddress = Destination;
                ////////////    //MessageBox.Show(fi.Name);
                ////////////    Atend.Control.Common.DesignName = fi.Name.Replace(".ATNX", ".DWG");
                ////////////    Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, fi.Name.Replace(".ATNX", ".MDB"));
                ////////////    MessageBox.Show(NewName);
                ////////////    //System.Diagnostics.Process.Start(NewName);

                ////////////    DocumentCollection _DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                ////////////    if (File.Exists(NewName))
                ////////////    {
                ////////////        _DocumentCollection.Open(NewName, false);
                ////////////    }
                ////////////    else
                ////////////    {
                ////////////        ed.WriteMessage("File was not exist : {0} \n",NewName);
                ////////////    }
                ////////////    //MessageBox.Show("startted");
                ////////////    Atend.Base.Acad.AT_COUNTER.ReadAll();
                ////////////    //MessageBox.Show("counter");
                ////////////    //ed.WriteMessage("DesignName:" + Atend.Control.Common.DesignName);
                ////////////    //ed.WriteMessage("AccessPath:" + Atend.Control.Common.AccessPath);
                ////////////    //ed.WriteMessage("DesignFullAddress:" + Atend.Control.Common.DesignFullAddress);
                ////////////}
                ////////////catch (System.Exception ex)
                ////////////{
                ////////////    ed.WriteMessage("OPEN ERROR : {0} \n", ex.Message);
                ////////////}


            }
            else
            {
                AllowClosing = false;
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.AddExtension = false;
            openFileDialog1.Filter = " AutoCAD File (*.dwg)| *.dwg";
            openFileDialog1.Title = "فایل مورد نظر را انتخاب کنید";
            openFileDialog1.FileName = "";
            //openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.ShowDialog();

            if (File.Exists(openFileDialog1.FileName))
            {
                txtFilePath.Text = openFileDialog1.FileName;
            }
            //else
            //    MessageBox.Show("خطا در انتخاب مسیر");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Close();
        }

        private void frmOpenDesign02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClosing)
            {
                e.Cancel = true;
                AllowClosing = true;
            }
        }

        private void frmOpenDesign02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            string path = Atend.Control.ConnectionString.RecentPath;// Atend.Base.Base.BSetting.Select(Convert.ToInt32(Atend.Control.Enum.Setting.OpenDefaultPath)).Value;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("PATH={0}\n", path);
            string filter = "*.ATNX";
            if (Directory.Exists(path))
            {
                string[] temp = Directory.GetDirectories(path);
                for (int i = 0; i < temp.Length; i++)
                {

                    //ed.WriteMessage("Temp={0}\n", temp[i]);
                    string[] Temp1 = Directory.GetFiles(temp[i], filter);
                    if (Temp1.Length > 0)
                    {
                        DateTime dt = System.IO.File.GetLastWriteTime(Temp1[0].ToString());
                        //ed.WriteMessage("Temp1={0}\n", Temp1[0].ToString());
                        string s = Temp1[0].ToString();

                        int len = s.Substring(0, s.LastIndexOf(@"\")).Length;
                        //ed.WriteMessage("S={0},len={1}\n", s, len);
                        string name = s.Substring(len + 1, s.Length - len - 1);
                        gvFile.Rows.Add();
                        gvFile.Rows[gvFile.Rows.Count - 1].Cells[0].Value = name;
                        gvFile.Rows[gvFile.Rows.Count - 1].Cells[1].Value = Temp1[0].ToString();
                        gvFile.Rows[gvFile.Rows.Count - 1].Cells[2].Value = dt;
                    }
                }
            }


            gvFile.Sort(gvFile.Columns[2], ListSortDirection.Descending);
        }

        private void gvFile_DoubleClick(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (gvFile.Rows.Count > 0)
            {
                filePath = gvFile.Rows[gvFile.CurrentRow.Index].Cells[1].Value.ToString();
                //ed.WriteMessage("aa\n");
                //return DialogResult.OK;
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }
      

        private void gvFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}