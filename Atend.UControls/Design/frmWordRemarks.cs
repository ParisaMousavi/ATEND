using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;


namespace Atend.UControls.Design
{
    public partial class frmWordRemarks : Form
    {
        

        
         public int productCode = -1;
        int SelectedRemarkCode = 0;
        public bool IsDefault = false;
        int Code = -1;
        bool ForceToClose = false;

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
        //bool ForceToClose = false;
        string fileName, SourceFileName;
        byte[] file;

        public frmWordRemarks()
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

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            string filNm;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "MS-Word Files (*.doc,*.dot) | *.doc;*.dot";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filNm = openFileDialog1.FileName;
            }
            else return;
            MessageBox.Show("Please wait while the document is being displayed");
            try
            {
                objWinWordControl.CloseControl();

            }
            catch { }
            finally
            {
                objWinWordControl.document = null;
                Atend.UControls.WordControl.wd = null;
                Atend.UControls.WordControl.wordWnd = 0;
            }
            try
            {

                //Load the template used for testing.
                objWinWordControl.LoadDocument(filNm);
                try
                {
                    objWinWordControl.document.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdPrintView;
                }
                catch { }
            }
            catch (Exception ex)
            {
                String err = ex.Message;
            }
        }

        private void LoadTemplate()
        {

            //MessageBox.Show(FullPath);
            //MessageBox.Show(DesignFullAddress);
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


        private void Reset()
        {
            //txtAmper.Text = string.Empty;
            txtName.Text = string.Empty;
            SelectedRemarkCode = 0;
            
            productCode = -1;
            Code = -1;
            remarkCode = -1;
        }

        
        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفاً نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }

            object objMiss = Type.Missing;
            try
            {
                fileName = fileName.Replace("RemarkTemplate.doc", "RemarkTemplate1.doc");

                object objName = fileName;
                objWinWordControl.document.SaveAs(ref objName, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss);
                objWinWordControl.CloseControl();
             }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

       
        private void Save()
        {
            //btnInsert.Focus();
            ArrayList EPackageProduct = new ArrayList();
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Atend.Base.Design.DRemark remark = new Atend.Base.Design.DRemark();
            remark.File = null;
            remark.Name = txtName.Text;
            FileStream fs;
            if (File.Exists(fileName))
            {
                string tempAddress = fileName.Replace("RemarkTemplate1.doc", "temp.doc");
                File.Copy(fileName, tempAddress, true);
                fs = new FileStream(tempAddress, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                remark.File = br.ReadBytes((Int32)br.BaseStream.Length);
                fs.Close();
                fs.Dispose();
                File.Delete(fileName);
            }
            else
            {
                remark.File = file;
            }

            if (SelectedRemarkCode == 0)
            {
                if (remark.Insert())
                {
                    Reset();
                }
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                remark.Code = SelectedRemarkCode;
                if (remark.Update())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void Delete()
        {
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            string name = string.Empty;
            

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedRemarkCode != 0)
                {
                    if (Atend.Base.Design.DRemark.Delete(SelectedRemarkCode))
                    {
                        Reset();
                        //FileStream fs;
                        //if (File.Exists(fileName))
                        //{
                        //    string tempAddress = fileName.Replace("RemarkTemplate1.doc", "temp.doc");
                        //    //File.Copy(fileName, tempAddress, true);
                        //    fs = new FileStream(tempAddress, FileMode.Open);
                        //    BinaryReader br = new BinaryReader(fs);
                        //    //remark.File = br.ReadBytes((Int32)br.BaseStream.Length);
                        //    fs.Close();
                        //    fs.Dispose();
                        //    File.Delete(fileName);
                        //}
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

                    }
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }
        }

       
        public void BindDataToOwnControl(int Code)
        {
            Atend.Base.Design.DRemark Rem = Atend.Base.Design.DRemark.SelectByCode(Code);
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(breaker.ProductCode);
            SelectedRemarkCode = Code;
            //Atend.Control.Common.selectedProductCode = Rem.ProductCode;
            //SelectProduct();

            txtName.Text = Rem.Name;

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

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Reset();
            LoadTemplate();
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }


        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWordRemarkSearch frmRemarkSearch = new frmWordRemarkSearch(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmRemarkSearch);
            
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmRemark03_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

        }

        

    }
}