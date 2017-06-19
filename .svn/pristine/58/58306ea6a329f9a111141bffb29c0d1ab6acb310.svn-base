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
    public partial class frmNewDesign02 : Form
    {

        bool AllowClosing = true;
        bool ForceToClose = false;

        public frmNewDesign02()
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
            folderBrowserDialog1.ShowDialog();

            if (System.IO.Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                txtFilePath.Text = folderBrowserDialog1.SelectedPath;
            }
            //else
            //    MessageBox.Show("خطا در انتخاب مسیر");
        }

        private bool Validation()
        {
            if (txtFileName.Text == "[NoName]" || txtFileName.Text == string.Empty)
            {
                MessageBox.Show("لطفا نام طرح را مشخص نمایید", "خطا");
                txtFileName.Focus();
                AllowClosing = false;
                return false;
            }

            if (txtFilePath.Text == string.Empty)
            {
                MessageBox.Show("لطفا مسیر طرح را مشخص نمایید", "خطا");
                txtFileName.Focus();
                AllowClosing = false;
                return false;

            }

            return true;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Validation())
            {
                Atend.Control.Common.DesignId = 0;
                string DesignFile = string.Format(@"{0}\DesignFile\AtendEmpty.dwg", Atend.Control.Common.fullPath);
                //ed.WriteMessage("111:{0}",DesignFile);
                string DatabaseFile = string.Format(@"{0}\DatabaseFile\AtendLocal.mdb", Atend.Control.Common.fullPath);
                //ed.WriteMessage("222:{0}", DatabaseFile);
                string Destination = string.Format(@"{0}\{1}", txtFilePath.Text, txtFileName.Text);
                //ed.WriteMessage("333:{0}", Destination);

                if (File.Exists(DesignFile) && File.Exists(DatabaseFile))
                {
                    if (Directory.Exists(string.Format(@"{0}\{1}", txtFilePath.Text, txtFileName.Text)))
                    {
                        Directory.Delete(string.Format(@"{0}\{1}", txtFilePath.Text, txtFileName.Text), true);
                    }
                    if (!Directory.Exists(string.Format(@"{0}\{1}", txtFilePath.Text, txtFileName.Text)))
                    {
                        Directory.CreateDirectory(string.Format(@"{0}\{1}", txtFilePath.Text, txtFileName.Text));

                        File.Copy(DesignFile, string.Format(@"{0}\{1}.DWG", Destination, txtFileName.Text));
                        File.Copy(DatabaseFile, string.Format(@"{0}\{1}.MDB", Destination, txtFileName.Text));
                        Atend.Global.Acad.DrawinOperation dOperation = new Atend.Global.Acad.DrawinOperation();
                        dOperation.AddFileToAtendFile(txtFullPath.Text, string.Format(@"{0}\{1}.DWG", Destination, txtFileName.Text));
                        dOperation.AddFileToAtendFile(txtFullPath.Text, string.Format(@"{0}\{1}.MDB", Destination, txtFileName.Text));
                        File.SetAttributes(string.Format(@"{0}\{1}.DWG", Destination, txtFileName.Text), FileAttributes.Normal);
                        Atend.Control.Common.DesignName = txtFileName.Text + ".DWG";
                        Atend.Control.Common.DesignFullAddress = string.Format(@"{0}\{1}", txtFilePath.Text, txtFileName.Text);
                        Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, txtFileName.Text + ".MDB");
                        System.Diagnostics.Process.Start(string.Format(@"{0}\{1}.DWG", Destination, txtFileName.Text));

                        //////DocumentCollection _DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                        //////if (File.Exists(string.Format(@"{0}\{1}.DWG", Destination, txtFileName.Text)))
                        //////{
                        //////    _DocumentCollection.Open(string.Format(@"{0}\{1}.DWG", Destination, txtFileName.Text), false);
                        //////}
                        //////else
                        //////{
                        //////    ed.WriteMessage("File was not exist : {0} \n", string.Format(@"{0}\{1}.DWG", Destination, txtFileName.Text));
                        //////}


                        Atend.Base.Acad.AT_COUNTER.ReadAll();
                    }

                }

            }//

            else
            {
                AllowClosing = false;
            }
        }

        //private void SetDesign(string DirPath)
        //{
        //    if (gvDesign.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("لطفاً طرح مورد نظر را انتخاب کنید ");
        //        return;
        //    }

        //    if (gvDesign.Rows.Count > 0)
        //    {
        //        //Atend.Base.Base.BSqlServer bsql = Atend.Base.Base.BSqlServer.Select_ByCode(3);
        //        //if (bsql.Value1 == string.Empty)
        //        //{
        //        //    MessageBox.Show("لطفامسيرذخيرهسازي طرحهاي موجودرامشخص نماييد");
        //        //    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        //        //    {
        //        //        bsql.Value1 = folderBrowserDialog1.SelectedPath;
        //        //    }
        //        //    if (!bsql.Update())
        //        //        MessageBox.Show("تغيير تنظيمات امكان پذير نمي باشد");


        //        //    bsql = Atend.Base.Base.BSqlServer.Select_ByCode(3);
        //        //}
        //        //if (!Directory.Exists(bsql.Value1))
        //        //{
        //        //    Directory.CreateDirectory(bsql.Value1);
        //        //}


        //        // close all current  documents
        //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //        string txtFileName;
        //        //Atend.Base.Base.BSqlServer sqlGlobal = Atend.Base.Base.BSqlServer.Select_ByCode(3);
        //        try
        //        {
        //            if (Atend.Control.Common.SelectedDesignCode != 0)
        //            {


        //                txtFileName = DirPath + string.Format(@"\{0}.dwg", gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString());


        //                if (!Directory.Exists(DirPath + @"\AtendTemp"))
        //                {
        //                    Directory.CreateDirectory(DirPath + @"\AtendTemp");
        //                }
        //                //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.CloseAndSave(Atend.Control.Common.SelectedDesignCode.ToString());
        //                FileStream fs;
        //                File.Copy(DirPath + @"\" + Atend.Control.Common.SelectedDesignCode + ".dwg", DirPath + @"\AtendTemp\" + Atend.Control.Common.SelectedDesignCode + ".dwg", true);
        //                fs = File.Open(DirPath + @"\AtendTemp\" + Atend.Control.Common.SelectedDesignCode + ".dwg", FileMode.Open);    //txtFileName, FileMode.Open);
        //                //fs = File.Open(bsql.Value1 +@"\"+ Atend.Control.Common.SelectedDesignCode + ".dwg", FileMode.Open);    //txtFileName, FileMode.Open);
        //                BinaryReader br = new BinaryReader(fs);
        //                Atend.Base.Design.DDesignFile dDesignFile1 = new Atend.Base.Design.DDesignFile();
        //                dDesignFile1.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //                dDesignFile1.FileSize = Convert.ToInt64(br.BaseStream.Length);
        //                dDesignFile1.File = br.ReadBytes((Int32)br.BaseStream.Length);
        //                if (!dDesignFile1.Insert())
        //                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            //ed.WriteMessage("Error SAVING : {0} \n", ex.Message);
        //        }


        //        // end of close all current  documents


        //        //ed.WriteMessage("10 \n");
        //        Atend.Control.Common.SelectedDesignCode = Convert.ToInt32(gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString());

        //        Atend.Base.Base.BDesignProfile DP = Atend.Base.Base.BDesignProfile.Select_ByDesignCode(Atend.Control.Common.SelectedDesignCode);
        //        if (DP.DesignerName != "NONE")
        //            Atend.Control.Common.SelectedDesignScale = Convert.ToSingle(DP.Scale);
        //        else
        //            Atend.Control.Common.SelectedDesignScale = 1;

        //        //InitializeEquipLayerTable();
        //        //ed.WriteMessage("11 \n");
        //        Atend.Base.Design.DDesignFile dDesignFile = new Atend.Base.Design.DDesignFile();
        //        //ed.WriteMessage(gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString() + "\n");
        //        //Atend.Base.Design.DDesignSetting setting = new Atend.Base.Design.DDesignSetting();
        //        try
        //        {

        //            Atend.Control.Common.SelectedDesignCode = Convert.ToInt32(gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString());
        //            //Atend.Control.Common.SelectedDesignCode = 1000002;

        //        }
        //        catch (Exception ex)
        //        {

        //            ed.WriteMessage(ex.Message);
        //        }
        //        //ed.WriteMessage("Befor Select \n");
        //        //EXTRA
        //        //Atend.Base.Design.DDesignSetting setting = Atend.Base.Design.DDesignSetting.SelectByDesingCodeType(
        //        //    Atend.Control.Common.SelectedDesignCode, Atend.Control.Enum.DesignSettingType.LastNodeNumber);

        //        ////ed.WriteMessage(setting.DesignCode + "\n");
        //        //if (setting.DesignCode == 0)
        //        //{
        //        //    //ed.WriteMessage(Atend.Control.Common.SelectedDesignCode + "\n");
        //        //    setting.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //        //    //ed.WriteMessage(Atend.Control.Enum.DesignSettingType.LastNodeNumber + "\n");
        //        //    setting.Type = Atend.Control.Enum.DesignSettingType.LastNodeNumber;
        //        //    setting.Value = 0;
        //        //    setting.Insert();
        //        //}
        //        //setting = Atend.Base.Design.DDesignSetting.SelectByDesingCodeType(
        //        //    Atend.Control.Common.SelectedDesignCode, Atend.Control.Enum.DesignSettingType.LastBranchNumber);
        //        //if (setting.DesignCode == 0)
        //        //{
        //        //    setting.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //        //    setting.Type = Atend.Control.Enum.DesignSettingType.LastBranchNumber;
        //        //    setting.Value = 0;
        //        //    setting.Insert();
        //        //}

        //        dDesignFile = Atend.Base.Design.DDesignFile.SelectByDesignCode(Convert.ToInt32(gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString()));
        //        if (dDesignFile.File != null)
        //        {
        //            Stream st = new MemoryStream();
        //            st = File.Create(DirPath + @"\" + Atend.Control.Common.SelectedDesignCode + ".dwg");
        //            BinaryWriter binWriter = new BinaryWriter(st);
        //            binWriter.Write((byte[])dDesignFile.File);
        //            binWriter.Close();
        //            System.Diagnostics.Process.Start(DirPath + @"\" + Atend.Control.Common.SelectedDesignCode + ".dwg");
        //        }
        //        else
        //        {

        //            //ed.WriteMessage("FullPath**** : {0} \n",Atend.Control.Common.fullPath);
        //            FileStream fs;

        //            //File.Copy(bsql.Value1 + @"\" + Atend.Control.Common.SelectedDesignCode + ".dwg", bsql.Value1 + @"\AtendTemp\" + Atend.Control.Common.SelectedDesignCode + ".dwg", true);
        //            fs = File.Open(Atend.Control.Common.fullPath + @"\AtendEmpty.dwg", FileMode.Open);    //txtFileName, FileMode.Open);
        //            ////fs = File.Open(bsql.Value1 +@"\"+ Atend.Control.Common.SelectedDesignCode + ".dwg", FileMode.Open);    //txtFileName, FileMode.Open);
        //            BinaryReader br = new BinaryReader(fs);
        //            Atend.Base.Design.DDesignFile dDesignFile1 = new Atend.Base.Design.DDesignFile();
        //            dDesignFile1.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //            dDesignFile1.FileSize = Convert.ToInt64(br.BaseStream.Length);
        //            dDesignFile1.File = br.ReadBytes((Int32)br.BaseStream.Length);
        //            if (dDesignFile1.Insert())
        //            {
        //                dDesignFile = Atend.Base.Design.DDesignFile.SelectByDesignCode(Convert.ToInt32(gvDesign.Rows[gvDesign.CurrentRow.Index].Cells[0].Value.ToString()));
        //                if (dDesignFile.File != null)
        //                {
        //                    Stream st = new MemoryStream();
        //                    st = File.Create(DirPath + @"\" + Atend.Control.Common.SelectedDesignCode + ".dwg");
        //                    BinaryWriter binWriter = new BinaryWriter(st);
        //                    binWriter.Write((byte[])dDesignFile.File);
        //                    binWriter.Close();
        //                    System.Diagnostics.Process.Start(DirPath + @"\" + Atend.Control.Common.SelectedDesignCode + ".dwg");

        //                }

        //            }
        //            else
        //            {
        //                //ed.WriteMessage("Error while saving\n");
        //            }

        //        }


        //    }

        //}

        //private void btnBrowse2_Click(object sender, EventArgs e)
        //{
        //    openFileDialog1.Multiselect = false;
        //    openFileDialog1.AddExtension = false;
        //    openFileDialog1.Filter = " AutoCAD File (*.dwg)| *.dwg";
        //    openFileDialog1.Title = "فایل مورد نظر را انتخاب کنید";
        //    openFileDialog1.FileName = "";
        //    openFileDialog1.InitialDirectory = "C:\\";
        //    openFileDialog1.ShowDialog();

        //    if (File.Exists(openFileDialog1.FileName))
        //    {
        //        txtFilePath2.Text = openFileDialog1.FileName;
        //    }
        //    //else
        //    //    MessageBox.Show("خطا در انتخاب مسیر");
        //}

        private void btnBrows3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            if (System.IO.Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                txtFilePath.Text = folderBrowserDialog1.SelectedPath;
                txtFullPath.Text = string.Format(@"{0}\{1}\{2}.ATNX",
                    txtFilePath.Text,
                    txtFileName.Text,
                    txtFileName.Text);

            }


            //else
            //    MessageBox.Show("خطا در انتخاب مسیر");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Close();
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
            if (txtFileName.Text == string.Empty)
            {
                //txtFileName.Text = "[NoName]";

            }
            txtFullPath.Text = string.Format(@"{0}\{1}\{2}.ATNX",
                txtFilePath.Text,
                txtFileName.Text,
                txtFileName.Text);
        }

        private void frmNewDesign02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClosing)
            {
                e.Cancel = true;
                AllowClosing = true;
            }
        }

        private void frmNewDesign02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            string filePath = Atend.Control.ConnectionString.RecentPath;// Atend.Base.Base.BSetting.Select(Convert.ToInt32(Atend.Control.Enum.Setting.OpenDefaultPath)).Value;
            if (Directory.Exists(filePath))
            {
                txtFilePath.Text = filePath;
                txtFullPath.Text = string.Format(@"{0}\{1}\{2}.ATNX",
                    txtFilePath.Text,
                    txtFileName.Text,
                    txtFileName.Text);
            }
        }

    }
}