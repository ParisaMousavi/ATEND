using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;


namespace Atend.Design
{
    public partial class frmInterface : Form
    {
        string Version = "";
        string Path = "";
        bool AllowClosing = true;
        public int _ISACTIVE = 0;
        bool ForceToClose = false;

        public frmInterface()
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

        private void frmInterface_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            using (RegistryKey Key = Registry.LocalMachine.OpenSubKey(@"Software\Autodesk\AutoCAD\R17.2\ACAD-7001:409\Applications\ATEND", false))
            {
                if (Key != null)
                {
                    lblPath.Text = Key.GetValue("LOADER").ToString();
                    Path = Key.GetValue("LOADER").ToString();
                 
                    lblVersion.Text = Key.GetValue("VERSION").ToString();
                    Version = Key.GetValue("VERSION").ToString();
                }
            }
            Path = Path.Replace("Atend.dll", "");
            Version = Version.Substring(6, 7).Replace(".", "");
            Atend.Global.Utility.UInterface inter = new Atend.Global.Utility.UInterface();
            
            string plainText = inter.GetInterface();
            string passPhrase = Version;
            string saltValue = Version;
            string hashAlgorithm = "SHA1";
            int passwordIterations = 2;
            string initVector = "@1B2c3D4e5F6g7H8";
            int keySize = 256;
            string EncryptText = inter.EnInterface(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);

            lblRequest.Text = EncryptText;
            
            FileStream stream = new FileStream(string.Format(@"{0}LICENCE.ATND", Path), System.IO.FileMode.Create);
            System.Text.ASCIIEncoding a = new ASCIIEncoding();
            byte[] reader = a.GetBytes(EncryptText); //Convert.FromBase64String(EncryptText);
            stream.Write(reader, 0, reader.Length);
            stream.Close();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            AllowClosing = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Path.Replace("Atend.dll", ""));
            }
            catch
            {
            }
        }

        private void btnRecive_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Active File (*.ATND)| *.ATND";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtActivePath.Text = openFileDialog1.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtActivePath.Text != "")
            {
                FileStream fs = new FileStream(txtActivePath.Text, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] s = br.ReadBytes((Int32)br.BaseStream.Length);
                fs.Dispose();
                System.Text.ASCIIEncoding a = new ASCIIEncoding();
                string activepass = a.GetString(s);

                string versionsoft = "";
                using (RegistryKey Key = Registry.LocalMachine.OpenSubKey(@"Software\Autodesk\AutoCAD\R17.2\ACAD-7001:409\Applications\ATEND", false))
                {
                    if (Key != null)
                    {
                        versionsoft = Key.GetValue("VERSION").ToString();
                    }
                }
                versionsoft = versionsoft.Substring(6, 7).Replace(".", "");
                Atend.Global.Utility.UInterface inter = new Atend.Global.Utility.UInterface();

                string passPhrase = versionsoft;
                string saltValue = versionsoft;
                string hashAlgorithm = "SHA1";
                int passwordIterations = 2;
                string initVector = "@1B2c3D4e5F6g7H8";
                int keySize = 256;
                string orginalpass = inter.DeInterface(activepass, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
                if (orginalpass != "")
                {
                    if (orginalpass.Length == 48 || orginalpass.Length == 47)
                    {
                        try
                        {
                            string pinterface = orginalpass.Substring(0, 16);
                            string hinterface = orginalpass.Substring(16, 10);
                            string dinterface = orginalpass.Substring(26, 8);
                            string vinterface = orginalpass.Substring(34, 4);
                            string tinterface = orginalpass.Substring(38, 8);
                            string duinterface = orginalpass.Substring(46);

                            string temp = inter.GetInterface();
                            string ptemp = temp.Substring(0, 16);
                            string htemp = orginalpass.Substring(16, 10);

                            if (ptemp != pinterface || htemp != hinterface)
                            {
                                MessageBox.Show("کد فعال سازی اشتباه می باشد");
                                AllowClosing = false;
                                return;
                            }
                            else
                            {
                                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Autodesk\AutoCAD\R17.2\ACAD-7001:409\Applications\ATEND", true);
                                if (key != null)
                                {
                                    key.SetValue("InterfacePI", pinterface, RegistryValueKind.String);
                                    key.SetValue("InterfaceHS", hinterface, RegistryValueKind.String);
                                    key.SetValue("InterfaceDT", tinterface, RegistryValueKind.String);
                                    key.SetValue("InterfaceDU", duinterface, RegistryValueKind.DWord);

                                    _ISACTIVE = 1;
                                    MessageBox.Show("فعال سازی با موفقیت نجام گردید ");
                                    AllowClosing = true;
                                }
                            }


                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show("خطا ." + ex.Message);
                            AllowClosing = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("کد فعال سازی اشتباه می باشد");
                        AllowClosing = false;
                    }
                }
                else
                {
                    MessageBox.Show("کد فعال سازی اشتباه می باشد");
                    AllowClosing = false;
                    return;
                }
                
            }
        }

        private void frmInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClosing)
            {
                e.Cancel = true;
                AllowClosing = true;
            }
        }
    }
}