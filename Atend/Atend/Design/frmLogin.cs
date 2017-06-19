using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Xml;
using System.Collections;
using System.IO;

namespace Atend.Design
{
    public partial class frmLogin : Form
    {

        bool AllowClosing = true;
        bool ForceToClose = false;


        public frmLogin()
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

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("لطفا نام کاربری را مشخص نمایید", "خطا");
                txtUserName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("لطفا کلمه عبور را مشخص نمایید", "خطا");
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        private void Login()
        {
            Atend.Base.Design.DUser user = Atend.Base.Design.DUser.SelectByUsernameAndPassword(txtUserName.Text, txtPassword.Text);
            if (user.Code == -1)
            {
                MessageBox.Show("اطلاعات معتبر نمی باشد", "خطا");
                txtPassword.Text = string.Empty;
                txtUserName.Text = string.Empty;
                txtUserName.Focus();
                AllowClosing = false;
            }
            else
            {
                System.Globalization.CultureInfo MyCulture = new System.Globalization.CultureInfo("fa-IR");
                InputLanguage MyL;

                string BackL;
                MyL = InputLanguage.CurrentInputLanguage;
                MyCulture = MyL.Culture;
                BackL = MyCulture.Name;
                MyCulture = new System.Globalization.CultureInfo("fa-IR");
                InputLanguage.CurrentInputLanguage = System.Windows.Forms.InputLanguage.FromCulture(MyCulture);



                Atend.Control.Common.userCode = user.Code;
                //Atend.Control.Common.StatuseCode = StatuseCode();
                //ed.WriteMessage("go for fill access :{0} \n", user.Code);
                Atend.Control.Common.AccessList = Atend.Base.Design.DUserAccess.SelectByUserId(user.Code);
                //ed.WriteMessage("access count:{0} \n",Atend.Control.Common.AccessList.Rows.Count);

                foreach (System.Data.DataRow dr in Atend.Control.Common.AccessList.Rows)
                {
                    switch (Convert.ToInt32(dr["IdAccess"]))
                    {
                        case 1:

                            break;
                        case 2:

                            break;
                        case 3:

                            //ed.WriteMessage("You  Have Access To ChngeDefault\n");
                            Atend.Control.Common.AccessChangeDefault = true;
                            break;
                        case 4:

                            break;
                        case 5:

                            break;
                        case 6:
                            // ed.WriteMessage("6\n");
                            break;
                        case 7:
                            //ed.WriteMessage("7\n");
                            break;
                        case 8:
                            // ed.WriteMessage("8\n");
                            break;
                        case 9:
                        //ed.WriteMessage("9\n");
                        case 10:
                            //ed.WriteMessage("PaletProduct\n");

                            Atend.Control.Common.AccessProductPallet = true;
                            break;
                    }
                }

                //Atend.Base.Base.BSetting bs = Atend.Base.Base.BSetting.Select(Convert.ToInt32(Atend.Control.Enum.Setting.OpenDefaultPath));
                string DefaultPath = Atend.Control.ConnectionString.RecentPath;
                if (/*bs.Value*/DefaultPath == "-")
                {
                    string path = Atend.Control.Common.fullPath;
                    ed.WriteMessage("PATH={0}\n", path);
                    path = path.Substring(0, 2);
                    path = string.Format(@"{0}\AtendTempFile", path);

                    if (Directory.Exists(path))
                    {
                        ed.WriteMessage("Exist Path\n");
                    }
                    else
                    {
                        Directory.CreateDirectory(path);
                        ed.WriteMessage("Create Path\n");
                    }
                    Atend.Control.ConnectionString.RecentPath = path;
                    //bs.Value = path;
                    //if (!bs.Update())
                    //{
                    //    ed.WriteMessage("Error in Update Setting\n");
                    //}
                }
                AllowClosing = true;
                //Close();
            }

        }

        //public DataTable StatuseCode()
        //{
        //    DataTable dtStatuse = new DataTable();
        //    DataColumn dcName = new DataColumn("Name");
        //    DataColumn dcCode = new DataColumn("Code", System.Type.GetType("System.Byte"));



        //    dtStatuse.Columns.Add(dcName);
        //    dtStatuse.Columns.Add(dcCode);

        //    DataRow dr0 = dtStatuse.NewRow();
        //    dr0["Name"] = "موجود-موجود";
        //    dr0["Code"] = 0;

        //    DataRow dr1 = dtStatuse.NewRow();
        //    dr1["Name"] = "موجود-مستعمل";
        //    dr1["Code"] = 1;

        //    DataRow dr2 = dtStatuse.NewRow();
        //    dr2["Name"] = "موجود-اسقاط";
        //    dr2["Code"] = 2;

        //    DataRow dr3 = dtStatuse.NewRow();
        //    dr3["Name"] = "موجود-جابجایی";
        //    dr3["Code"] = 3;

        //    DataRow dr4 = dtStatuse.NewRow();
        //    dr4["Name"] = "پیشنهادی-نو";
        //    dr4["Code"] = 4;

        //    DataRow dr5 = dtStatuse.NewRow();
        //    dr5["Name"] = "پیشنهادی-جابجایی";
        //    dr5["Code"] = 5;


        //    dtStatuse.Rows.Add(dr0);
        //    dtStatuse.Rows.Add(dr1);
        //    dtStatuse.Rows.Add(dr2);
        //    dtStatuse.Rows.Add(dr3);
        //    dtStatuse.Rows.Add(dr4);
        //    dtStatuse.Rows.Add(dr5);
        //    return dtStatuse;
        //}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Login();


                ////////uint status = 0;

                ////////try
                ////////{
                ////////    //Call RNBOsproFormatPacket API at the time of Main form load
                ////////    status = oSuperPro.RNBOsproFormatPacket(Atend.Global.Acad.DrawEquips.Superpro.SPRO_APIPACKET_SIZE);
                ////////    if (status != 0)
                ////////    {
                ////////        MessageBox.Show("Error during FormatPacket API call.", "Application Error");
                ////////        //Close();
                ////////        //return;
                ////////    }
                ////////    //ComboBoxProtocolFlag.SelectedIndex = 0;
                ////////    ed.WriteMessage("status:{0}\n", status);
                ////////    ed.WriteMessage("go to initialize \n");
                ////////    //**************RNBOsproInitialize API **********************/
                ////////    status = oSuperPro.RNBOsproInitialize();
                ////////    //If API get successfull
                ////////    if (status == 0)
                ////////    {
                ////////        //Clear all input box on API success
                ////////        //clearTextBox_succ("Initialize");
                ////////        ed.WriteMessage("AT_Initialize Done \n");
                ////////    }
                ////////    else
                ////////    {
                ////////        //Display API failure message with return code of API
                ////////        //clearTextBox_fail("Initialize", status);
                ////////        ed.WriteMessage("AT_Initialize Failed \n");

                ////////    }
                ////////    ed.WriteMessage("status:{0}\n", status);
                ////////}
                ////////catch
                ////////{
                ////////    ed.WriteMessage("status:{0}\n", status);
                ////////    MessageBox.Show("Error during API call. Probably sx32w.dll is missing.", "Application Error");
                ////////    //Close();
                ////////    //return;
                ////////}




            }
            else
            {
                AllowClosing = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            AllowClosing = true;
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClosing)
            {
                e.Cancel = true;
                AllowClosing = true;
            }
        }

        //private void ConnectionValidation()
        //{
        //    bool ServerCheck = false, LocalCheck = false;

        //    Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}\{2}",
        //        Environment.CurrentDirectory,
        //        "DataBase",
        //        "AtendLocal.mdb");
        //    ed.WriteMessage("AcessPath From Login=" + Atend.Control.Common.AccessPath + "\n");

        //    //check local
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    try
        //    {

        //        connection.Open();
        //        if (connection.State == ConnectionState.Open)
        //        {
        //            //Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
        //            //notification.Title = "پایگاه داده محلی";
        //            //notification.Msg = "ارتباط با پایگاه داده محلی برقرار شد";
        //            //notification.infoCenterBalloon();

        //            btnLogin.Enabled = true;

        //        }
        //        else
        //        {
        //            throw new System.Exception("Connection failed");
        //        }
        //        connection.Close();
        //        LocalCheck = true;
        //    }
        //    catch
        //    {

        //        connection.Close();
        //        //Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
        //        //notification.Title = "پایگاه داده محلی";
        //        //notification.Msg = "ارتباط با پایگاه داده محلی برقرار نشد";
        //        //notification.infoCenterBalloon();

        //        LocalCheck = false;
        //        btnLogin.Enabled = false;
        //        //btnServerSetting.Visible = true;
        //        //btnServerSetting.Enabled = true;

        //    }

        //    //check server
        //    connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    try
        //    {

        //        connection.Open();
        //        if (connection.State == ConnectionState.Open)
        //        {
        //            //Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
        //            //notification.Title = "پایگاه داده سرور";
        //            //notification.Msg = "ارتباط با پایگاه داده سرور برقرار شد";
        //            //notification.infoCenterBalloon();
        //        }
        //        else
        //        {
        //            throw new System.Exception("Connection failed");
        //        }
        //        connection.Close();
        //        ServerCheck = true;
        //    }
        //    catch
        //    {

        //        connection.Close();
        //        //Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
        //        //notification.Title = "پایگاه داده سرور";
        //        //notification.Msg = "ارتباط با پایگاه داده سرور برقرار نشد";
        //        //notification.infoCenterBalloon();

        //        ServerCheck = false;
        //        //btnServerSetting.Visible = true;
        //        //btnServerSetting.Enabled = true;

        //    }



        //    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
        //    notification.Title = "ارتباط با پایگاه داده";
        //    notification.Msg = string.Format("Server : {0} , Local : {1} \n", (ServerCheck ? "Connected" : "DicConnected"), (ServerCheck ? "Connected" : "DisConnected"));
        //    notification.infoCenterBalloon();


        //}

        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            txtUserName.Focus();
            btnLogin.Enabled = true;
            //ConnectionValidation();
            //~~//Atend.Control.Common.fullPath = Environment.CurrentDirectory;

            //ConnectionValidation();
            //Atend.Control.ConnectionString.AccessCnString;
            //ed.WriteMessage("AccessSting=" + Atend.Control.ConnectionString.AccessCnString + "\n");

            

        }

        //private void btnServerSetting_Click(object sender, EventArgs e)
        //{
        //    //Atend.Design.frmSetting FRMsetting = new Atend.Design.frmSetting();
        //    //FRMsetting.ShowDialog();

        //    //ConnectionValidation();

        //    Atend.Design.frmSetting fsetting = new frmSetting();
        //    fsetting.ShowDialog();

        //    if (fsetting.IsOK)
        //    {
        //        btnLogin.Enabled = true;
        //        //btnServerSetting.Visible = false;
        //    }



        //}

    }
}