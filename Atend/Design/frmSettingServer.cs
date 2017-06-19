using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Design
{
    public partial class frmSettingServer : Form
    {
        public bool IsOK = false;
        bool ForceToClose = false;

        public frmSettingServer()
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtaddressLocal.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        string strconnection;

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtServerName.Text = "";
            txtUsernameServer.Text = "";
            txtPasswordServer.Text = "";

            txtaddressLocal.Text = "";
            txtUsernamelocal.Text = "";
            txtPasswordLocal.Text = "";
            cboMode.SelectedIndex = 0;
        }

        private bool Validation()
        {
            if (cboMode.SelectedIndex == 0)
            {
                if (txtServerName.Text == string.Empty)
                {
                    txtServerName.Focus();
                    return false;
                }

                if (txtUsernameServer.Text == string.Empty)
                {
                    txtUsernameServer.Focus();
                    return false;
                }

                if (txtPasswordServer.Text == string.Empty)
                {
                    txtPasswordServer.Focus();
                    return false;
                }
            }
            else if (cboMode.SelectedIndex == 1)
            {
                if (txtServerName.Text == string.Empty)
                {
                    txtServerName.Focus();
                    return false;
                }
            }



            return true;
        }

        //private bool Save()
        //{

        //    //Atend.Base.Base.BSqlServer sql = new Atend.Base.Base.BSqlServer();
        //    //sql.Code = 2;
        //    //sql.Value1 = strconnection;
        //    //if (sql.Update())
        //    //{
        //    //    IsOK = true;
        //    //    Atend.Control.ConnectionString.cnString = strconnection;
        //    //    return true;
        //    //}
        //    //else
        //    //    return false;

        //}

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (cboMode.SelectedIndex == 0)
                {
                    strconnection = string.Format(@"Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}", txtPasswordServer.Text, txtUsernameServer.Text, txtDataBase.Text, txtServerName.Text);
                }
                else if (cboMode.SelectedIndex == 1)
                {
                    strconnection = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True",
                    txtServerName.Text,
                    txtDataBase.Text);
                }
                if (Atend.Control.ConnectionString.ConnectionValidation(strconnection))
                    Atend.Control.ConnectionString.ServercnString = strconnection;
                //WriteToConfig("ServerConnectionString", strconnection);
            }
            else
            {
                MessageBox.Show("امکان ثبت اطلاعات موجود نمی باشد", "خطا");
            }

        }

        private void btnServerTest_Click(object sender, EventArgs e)
        {
            if (cboMode.SelectedIndex == 0)
            {
                strconnection = string.Format(@"Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                    txtPasswordServer.Text,
                    txtUsernameServer.Text,
                    txtDataBase.Text,
                    txtServerName.Text);
            }
            else if (cboMode.SelectedIndex == 1)
            {
                strconnection = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True",
                    txtServerName.Text,
                    txtDataBase.Text);
            }
            try
            {
                SqlConnection connection = new SqlConnection(strconnection);
                connection.Open();
                connection.Close();

                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "پایگاه داده سرور";
                notification.Msg = "اتصال به پایگاه داده برقرار شد";
                notification.infoCenterBalloon();


            }
            catch
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "پایگاه داده سرور";
                notification.Msg = "اتصال به پایگاه داده برقرار نشد";
                notification.infoCenterBalloon();

            }
        }

        private void btnLocalTest_Click(object sender, EventArgs e)
        {

        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            //Atend.Base.Base.BSqlServer sql = Atend.Base.Base.BSqlServer.Select_ByCode(2);
            //string strTemp = sql.Value1;
            cboMode.SelectedIndex = 0;


            string CNS = Atend.Control.ConnectionString.ServercnString;
            string[] CNSParts = CNS.Split(';');
            if (CNSParts.Length == 5)
            {
                //Password
                int index = CNSParts[0].IndexOf('=');
                if (index != -1)
                {
                    txtPasswordServer.Text = CNSParts[0].Substring(index + 1, (CNSParts[0].Length - 1) - index);
                }

                //User ID
                index = CNSParts[2].IndexOf('=');
                if (index != -1)
                {
                    txtUsernameServer.Text = CNSParts[2].Substring(index + 1, (CNSParts[2].Length - 1) - index);
                }

                //Data Source
                index = CNSParts[4].IndexOf('=');
                if (index != -1)
                {
                    txtServerName.Text = CNSParts[4].Substring(index + 1, (CNSParts[4].Length - 1) - index);
                }

                cboMode.SelectedIndex = 0;

            }
            else if (CNSParts.Length == 3)
            {

                //Data Source
                int index = CNSParts[0].IndexOf('=');
                if (index != -1)
                {
                    txtServerName.Text = CNSParts[0].Substring(index + 1, (CNSParts[0].Length - 1) - index);
                }


                cboMode.SelectedIndex = 1;
            }
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (cboMode.SelectedIndex == 0)
                {
                    strconnection = string.Format(@"Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
        txtPasswordServer.Text,
        txtUsernameServer.Text,
        txtDataBase.Text,
        txtServerName.Text);
                }
                else if (cboMode.SelectedIndex == 1)
                {
                    strconnection = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True",
                    txtServerName.Text,
                    txtDataBase.Text);
                }
                if (Atend.Control.ConnectionString.ConnectionValidation(strconnection))
                    Atend.Control.ConnectionString.ServercnString = strconnection;
                //WriteToConfig("ServerConnectionString", strconnection);

                Close();
            }
            else
            {
                MessageBox.Show("امکان ثبت اطلاعات موجود نمی باشد", "خطا");
            }

        }

        //private void WriteToConfig(string keyName, string newValue)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("KeyName={0},newValue={1}\n", keyName, newValue);

        //    System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
        //    string fullPath = m.FullyQualifiedName;
        //    try
        //    {
        //        fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
        //    }
        //    catch
        //    {
        //    }

        //    string xmlPath = fullPath + "\\Atend.dll.config";


        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(xmlPath);

        //    foreach (XmlElement xElement in xmlDoc.DocumentElement/* int i = 0; ! string.IsNullOrEmpty(xmlDoc.DocumentElement[i].Name); i++ */)
        //    {

        //        if (xElement.Name == "appSettings")
        //        {

        //            foreach (XmlNode Xnode in xElement.ChildNodes)
        //            {
        //                if (Xnode.Attributes[0].Value == keyName)
        //                {
        //                    Xnode.Attributes[1].Value = newValue;
        //                }
        //            }

        //        }

        //    }


        //    xmlDoc.Save(xmlPath);
        //}

        private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMode.SelectedIndex == 0)
            {
                txtUsernameServer.Enabled = true;
                txtPasswordServer.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
            }
            else if (cboMode.SelectedIndex == 1)
            {
                txtUsernameServer.Enabled = false;
                txtPasswordServer.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
            }
        }


    }
}