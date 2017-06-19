using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Design
{
    public partial class frmDesignSearch01 : Form
    {
        DataTable dt = new DataTable();
        //string _Status = "";
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool AllowToClose = true;
        bool ForceToClose = false;


        public frmDesignSearch01()
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

        private void Search(string Code)
        {
            //if (!string.IsNullOrEmpty(txtArchiveNo.Text))
            //{
            //    gvDesign.AutoGenerateColumns = false;
            //    Atend.Base.Design.DDesign Des = Atend.Base.Design.DDesign.SelectByCode(Convert.ToInt32(Code));

            //    gvDesign.Rows.Clear();
            //    gvDesign.Rows.Add();
            //    gvDesign.Rows[gvDesign.Rows.Count - 1].Cells[0].Value = Des.Code;
            //    gvDesign.Rows[gvDesign.Rows.Count - 1].Cells[1].Value = Des.ArchiveNo;
            //    gvDesign.Rows[gvDesign.Rows.Count - 1].Cells[2].Value = Des.Title;

            //}
            //else
            //{
            //    gvDesign.AutoGenerateColumns = false;
            //    gvDesign.DataSource = Atend.Base.Design.DDesign.Search(Code);
            //}
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string _Title = "";
            string _ArchiveNo = "";
            bool check = false;
            string strFilter = "";
            if (chkTitle.Checked)
            {
                _Title = txtTitle.Text;
                strFilter = " Title Like'" + _Title + "%'";
                check = true;
            }
            if (chkArchiveNo.Checked)
            {
                _ArchiveNo = txtArchiveNo.Text;
                if (strFilter != "")
                {
                    strFilter += " AND ArchiveNo Like'" + _ArchiveNo + "%'";
                }
                else
                {
                    strFilter = " ArchiveNo Like'" + _ArchiveNo + "%'";
                }
                check = true;
            }
            if (check)
            {
                DataView dv = new DataView();
                dv.Table = dt;
                dv.RowFilter = strFilter;
                gvDesign.AutoGenerateColumns = false;
                gvDesign.DataSource = dv;
            }
            else
            {
                gvDesign.AutoGenerateColumns = false;
                gvDesign.DataSource = dt;
            }
            AssignImageToGrid();
            //Search(txtDesignCode.Text);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.Directory.Exists(folderBrowserDialog1.SelectedPath))
                {
                    txtFilePath.Text = folderBrowserDialog1.SelectedPath;
                }
                //else
                //    MessageBox.Show("خطا در انتخاب مسیر");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                AllowToClose = true;
                long  PRGCode = Atend.Base.Design.DDesign.SelectByID(Convert.ToInt32(gvDesign.Rows[gvDesign.CurrentRow.Index].Cells["Id"].Value.ToString())).PRGCode;
                int ID = Convert.ToInt32(gvDesign.Rows[gvDesign.CurrentRow.Index].Cells["Id"].Value.ToString());
                Atend.Control.Common.DesignId = ID;
                string Destination = "";
                if (txtFilePath.Text.LastIndexOf(@"\") == txtFilePath.Text.Length - 1)
                {
                    //Destination = string.Format(@"{0}{1}", txtFilePath.Text, ID);
                    Destination = string.Format(@"{0}{1}", txtFilePath.Text, PRGCode);

                }
                else
                {
                    //Destination = string.Format(@"{0}\{1}", txtFilePath.Text, ID);
                    Destination = string.Format(@"{0}\{1}", txtFilePath.Text, PRGCode);

                }
                Atend.Base.Design.DDesignFile df1 = Atend.Base.Design.DDesignFile.SelectByDesignId(ID);
                if (df1.Id != -1 && df1.File != null)
                {
                    //ed.WriteMessage("____OPEN\n");
                    try
                    {
                        //ed.WriteMessage("________3\n");
                        if (df1.File != null)
                        {
                            if (Directory.Exists(Destination))
                            {
                                Directory.Delete(Destination, true);
                            }

                            //Atend.Control.Common.DesignName = ID + ".DWG";
                            Atend.Control.Common.DesignName = PRGCode + ".DWG";

                            Atend.Control.Common.Edition = gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[5].Value.ToString();
                            //Atend.Control.Common.DesignFullAddress = string.Format(@"{0}\{1}", txtFilePath.Text, ID);
                            Atend.Control.Common.DesignFullAddress = string.Format(@"{0}\{1}", txtFilePath.Text, PRGCode);
                            //Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, ID + ".MDB");
                            Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, PRGCode + ".MDB");

                            Directory.CreateDirectory(Destination);
                            Stream st = new MemoryStream();
                            //st = File.Create(string.Format(@"{0}\{1}.ATNX", Destination, ID));
                            st = File.Create(string.Format(@"{0}\{1}.ATNX", Destination, PRGCode));

                            BinaryWriter binWriter = new BinaryWriter(st);
                            binWriter.Write((byte[])df1.File);
                            binWriter.Close();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        ed.WriteMessage("OPEN ERROR : {0} \n", ex.Message);
                    }
                }
                else
                {
                    //ed.WriteMessage("____NEW\n");
                    string DesignFile = string.Format(@"{0}\DesignFile\AtendEmpty.dwg", Atend.Control.Common.fullPath);
                    string DatabaseFile = string.Format(@"{0}\DatabaseFile\AtendLocal.mdb", Atend.Control.Common.fullPath);
                    if (File.Exists(DesignFile) && File.Exists(DatabaseFile))
                    {
                        try
                        {
                            if (Directory.Exists(Destination))
                            {
                                Directory.Delete(Destination, true);
                            }
                            //string dis = Destination + string.Format(@"\{0}.ATNX", ID);
                            string dis = Destination + string.Format(@"\{0}.ATNX", PRGCode);

                            Directory.CreateDirectory(Destination);
                            //File.Copy(DesignFile, string.Format(@"{0}\{1}.DWG", Destination, ID));
                            //File.Copy(DatabaseFile, string.Format(@"{0}\{1}.MDB", Destination, ID));

                            File.Copy(DesignFile, string.Format(@"{0}\{1}.DWG", Destination, PRGCode));
                            File.Copy(DatabaseFile, string.Format(@"{0}\{1}.MDB", Destination, PRGCode));

                            Atend.Global.Acad.DrawinOperation dOperation = new Atend.Global.Acad.DrawinOperation();
                            //dOperation.AddFileToAtendFile(dis, string.Format(@"{0}\{1}.DWG", Destination, ID));
                            //dOperation.AddFileToAtendFile(dis, string.Format(@"{0}\{1}.MDB", Destination, ID));

                            dOperation.AddFileToAtendFile(dis, string.Format(@"{0}\{1}.DWG", Destination, PRGCode));
                            dOperation.AddFileToAtendFile(dis, string.Format(@"{0}\{1}.MDB", Destination, PRGCode));

                            //File.SetAttributes(string.Format(@"{0}\{1}.DWG", Destination, ID), FileAttributes.Normal);
                            File.SetAttributes(string.Format(@"{0}\{1}.DWG", Destination, PRGCode), FileAttributes.Normal);


                            //Atend.Control.Common.DesignName = ID + ".DWG";
                            Atend.Control.Common.DesignName = PRGCode + ".DWG";

                            Atend.Control.Common.Edition = gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[5].Value.ToString();
                            //Atend.Control.Common.DesignFullAddress = string.Format(@"{0}\{1}", txtFilePath.Text, ID);
                            Atend.Control.Common.DesignFullAddress = string.Format(@"{0}\{1}", txtFilePath.Text,PRGCode );

                            //Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, ID + ".MDB");
                            Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, PRGCode + ".MDB");


                        }
                        catch (System.Exception ex)
                        {
                            ed.WriteMessage("NEW ERROR : {0} \n", ex.Message);
                        }
                    }
                }// end for new
            }
            else
            {
                AllowToClose = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDesignSearch01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dt = Atend.Base.Design.DDesign.SelectAll();
            gvDesign.AutoGenerateColumns = false;
            gvDesign.DataSource = dt;
            AssignImageToGrid();
    
            string FilePath = Atend.Control.ConnectionString.RecentPath;// Atend.Base.Base.BSetting.Select(Convert.ToInt32(Atend.Control.Enum.Setting.OpenDefaultPath)).Value;
            txtFilePath.Text = FilePath;

            if (Convert.ToBoolean(Atend.Base.Base.BSetting.SelectByCode(Convert.ToInt32(Atend.Control.Enum.Setting.UpdateProduct)).Value))
            {
                lnkUpdate.Enabled = true;
            }

        }

        private void AssignImageToGrid()
        {
            for (int i = 0; i <= gvDesign.Rows.Count - 1; i++)
            {
                DataGridViewImageCell _IC = gvDesign.Rows[i].Cells["StatusImage"] as DataGridViewImageCell;
                Atend.Base.Design.DDesignFile dfile = Atend.Base.Design.DDesignFile.SelectByDesignId(Convert.ToInt32(gvDesign.Rows[i].Cells["ID"].Value.ToString()));
                if (dfile.Id == -1)
                {
                    _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Equip16.png");
                    gvDesign.Refresh();

                }
                else
                {
                    _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\HaveFile.png");
                    gvDesign.Refresh();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void gvDesign_DoubleClick(object sender, EventArgs e)
        {
            //ed.WriteMessage("******************\n");
            //ed.WriteMessage("Start=" + gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString() + "\n");
            //Atend.Base.Design.DDesignFile desFile = Atend.Base.Design.DDesignFile.SelectByDesignId(Convert.ToInt32(gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString()));
            //ed.WriteMessage("DesFile.Code=" + desFile.DesignId + "\n");
            //string FileName;
            //FileName = gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString();
            //if (Validation())
            //{
            //    if (desFile.DesignId == 0)
            //    {
            //        //ed.WriteMessage("Insert\n");
            //        desFile.DesignId = Convert.ToInt32(gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString());
            //        Atend.Global.Acad.DrawinOperation newf = new Atend.Global.Acad.DrawinOperation();
            //        desFile.File = newf.NewAtendFile(txtFilePath.Text, FileName);

            //        //desFile.FileSize = 0;
            //        if (desFile.Insert())
            //        {
            //            ed.WriteMessage("اطلاعات با موفقیت ثبت شد");
            //        }
            //        else
            //        {
            //            ed.WriteMessage("اطلاعات با موفقیت ثبت نشد");

            //        }

            //        PromptPointOptions pp = new PromptPointOptions("dsds");
            //        PromptPointResult pr = ed.GetPoint(pp);
            //        newf = new Atend.Global.Acad.DrawinOperation();
            //        newf.LoadAtendFile(desFile.File, txtFilePath.Text, FileName);

            //    }
            //    else
            //    {
            //        ed.WriteMessage("LoadFile\n");
            //        Atend.Global.Acad.DrawinOperation newf = new Atend.Global.Acad.DrawinOperation();
            //        newf.LoadAtendFile(desFile.File, txtFilePath.Text, FileName);
            //    }
            //    Close();
            //}


        }

        public bool Validation()
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("لطفا مسیر فایل را مشخص نمایید", "خطا");
                btnBrowse.Focus();
                return false;
            }

            if (gvDesign.Rows.Count <= 0 && gvDesign.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفا فایل مورد نظر را مشخص نمایید", "خطا");
                gvDesign.Focus();
                return false;
            }

            return true;
        }

        private void frmDesignSearch01_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToClose)
            {
                e.Cancel = true;
            }
            AllowToClose = true;
        }

        private void gvDesign_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //DataGridViewImageCell _IC = gvDesign.Rows[e.RowIndex].Cells["StatusImage"] as DataGridViewImageCell;
            //if (_IC != null)
            //{
            //    _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\HaveFile.png");
            //    //ed.WriteMessage("image assigned \n");
            //    gvDesign.Refresh();
            //}

        }

        private void gvDesign_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
          

        }

        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                if (Atend.Base.Design.DDesign.GetFromPoshtiban())
                {
                    dt = Atend.Base.Design.DDesign.SelectAll();
                    gvDesign.AutoGenerateColumns = false;
                    gvDesign.DataSource = dt;
                    AssignImageToGrid();
                }
                this.Cursor = Cursors.Default;
            }
            catch (System.Exception ex)
            { }
        }

    }
}