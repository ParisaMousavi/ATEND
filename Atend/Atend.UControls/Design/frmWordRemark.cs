using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.UControls.Design
{
    public partial class frmWordRemark : Form
    {


        private string fullPath;
        public string FullPath
        {
            get { return fullPath; }
            set { fullPath = value; }
        }

        private string designFullAddress;
        public string DesignFullAddress
        {
            get { return designFullAddress; }
            set { designFullAddress = value; }
        }

        int remarkCode = -1;
        bool ForceToClose = false;
        string fileName, SourceFileName;
        byte[] file;


        public frmWordRemark()
        {

            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("\nchecking.....\n");
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

        private void LoadTemplate()
        {

            try
            {
                objWinWordControl.CloseControl();
            }
            catch
            {
            }
            finally
            {
                objWinWordControl.document = null;
                Atend.UControls.WordControl.wd = null;
                Atend.UControls.WordControl.wordWnd = 0;
            }

            try
            {
                if (remarkCode == -1)
                {
                    if (System.IO.File.Exists(SourceFileName))
                    {
                        //if (System.IO.File.Exists(fileName))
                        //{
                        //    System.IO.File.Delete(fileName);
                        //}
                        //else
                        //{
                        if (!Directory.Exists(DesignFullAddress + @"\Data"))
                        {
                            Directory.CreateDirectory(DesignFullAddress + @"\Data");
                        }
                        //MessageBox.Show("s >> " + SourceFileName);
                        //MessageBox.Show("f >> " + fileName);
                        System.IO.File.Copy(SourceFileName, fileName, true);
                        //}
                    }
                    else
                    {
                        throw new System.Exception("Source file was not found");
                    }
                }

                if (System.IO.File.Exists(fileName))
                {
                    objWinWordControl.LoadDocument(fileName);
                    try
                    {
                        objWinWordControl.document.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdPrintView;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    //MessageBox.Show("file was not exist:" + fileName);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void frmWordRemark_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            SourceFileName = string.Format(@"{0}\SupportFiles\{1}", FullPath, "RemarkTemplate.doc");
            fileName = string.Format(@"{0}\Data\{1}", designFullAddress, "RemarkTemplate.doc");

            System.Globalization.CultureInfo MyCulture = new System.Globalization.CultureInfo("fa-IR");
            InputLanguage MyL;
            string BackL;
            MyL = InputLanguage.CurrentInputLanguage;
            MyCulture = MyL.Culture;
            BackL = MyCulture.Name;
            MyCulture = new System.Globalization.CultureInfo("fa-IR");
            InputLanguage.CurrentInputLanguage = System.Windows.Forms.InputLanguage.FromCulture(MyCulture);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            LoadTemplate();
        }

        private bool Validation()
        {

            object objMiss = Type.Missing;
            try
            {
                fileName = fileName.Replace("RemarkTemplate.doc", "RemarkTemplate1.doc");
                object objName = fileName;
                objWinWordControl.document.SaveAs(ref objName, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            //if (string.IsNullOrEmpty(rchText.Text))
            //{
            //    MessageBox.Show("لطفاً متن را مشخص نمایید", "خطا");
            //    rchText.Focus();
            //    return false;
            //}

            return true;
        }

        private void Reset()
        {
            //rchText.Text = string.Empty;
            remarkCode = -1;
        }

        private void Save()
        {
            Atend.Base.Design.DRemark remark = new Atend.Base.Design.DRemark();
            remark.Code = 1;
            remark.File = null;
            FileStream fs;
            if (File.Exists(fileName))
            {
                string tempAddress = fileName.Replace("RemarkTemplate1.doc", "temp.doc");
                File.Copy(fileName, tempAddress, true);
                fs = new FileStream(tempAddress, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                remark.File = br.ReadBytes((Int32)br.BaseStream.Length);
                fs.Dispose();
            }
            else
            {
                remark.File = file;
            }


            if (remarkCode == -1)
            {
                if (remark.AccessInsert())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                if (remark.AccessUpdate())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmWordRemarkSearch frmRemarkSearch = new frmWordRemarkSearch(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmRemarkSearch);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {

            Atend.Base.Design.DRemark remark = Atend.Base.Design.DRemark.AccessSelectByCode(1);
            if (remark.Code != -1)
            {
                //MessageBox.Show("record exist in db");
                //load file               
                if (remark.File.Length > 0)
                {
                    //MessageBox.Show("file exist");
                    file = remark.File;
                    Stream st = new MemoryStream();
                    //MessageBox.Show(fileName);
                    st = File.Create(fileName);
                    BinaryWriter binWriter = new BinaryWriter(st);
                    binWriter.Write((byte[])remark.File);
                    binWriter.Close();
                }
                remarkCode = remark.Code;
                LoadTemplate();
            }
            else
            {
                //MessageBox.Show("no record");
            }
        }

        public void BindDataToOwnControl(int Code)
        {
            Atend.Base.Design.DRemark Rem = Atend.Base.Design.DRemark.SelectByCode(Code);
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(breaker.ProductCode);
            //SelectedRemarkCode = Code;
            //Atend.Control.Common.selectedProductCode = Rem.ProductCode;
            //SelectProduct();

            //txtName.Text = Rem.Name;

            if (Rem.File.Length > 0)
            {
                //MessageBox.Show("file exist");
                file = Rem.File;
                Stream st = new MemoryStream();
                //MessageBox.Show(fileName);
                File.Delete(fileName);
                st = File.Create(fileName);
                BinaryWriter binWriter = new BinaryWriter(st);
                binWriter.Write((byte[])Rem.File);
                binWriter.Close();
            }
            remarkCode = Rem.Code;
            LoadTemplate();
            //txtText.Text = Rem.Text;
            Code = Rem.Code;
        }
    }
}