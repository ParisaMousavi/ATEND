using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Base
{
    public partial class frmBSetting : Form
    {
        int bCode;
        bool ForceToClose = false;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //int ConnectToPoshtiban = 1;
        //int UpdateProduct = 2;
        //int UpdateDesign = 3;
        //int ShowProduct = 4;

        public frmBSetting()
        {


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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtOpenPath.Text))
            {
                MessageBox.Show("لطفا مسیر فایل پیش فرض  را مشخص نمایید","خطا");
                txtOpenPath.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtLogo.Text))
            {
                MessageBox.Show("لطفا مسیر فایل آرم شرکت کارفرما را مشخص نمایید", "خطا");
                txtLogo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفا نام شرکت کارفرما در زبانه گزارشات را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            return true;
        }

        private void Save()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                //ed.WriteMessage("chk={0}\n", chkUpdateDesign.Checked);
                this.Cursor = Cursors.WaitCursor;
                if (chkUpdateDesign.Checked)
                {
                    ed.WriteMessage("1\n");
                    if (Atend.Base.Base.BRegion.GetFromPoshtiban())
                    {
                        ed.WriteMessage("Update B_Region\n");
                    }
                    ed.WriteMessage("2\n");
                    if (Atend.Base.Design.DDesign.GetFromPoshtiban())
                    {
                        ed.WriteMessage("update DDesign\n");
                    }
                }
                if (chkUpdateProduct.Checked)
                {
                    ed.WriteMessage("3\n");
                    if (Atend.Base.Base.BProduct.GetFromPoshtiban())
                    {
                        ed.WriteMessage("Update B_Product\n");
                    }
                    ed.WriteMessage("4\n");
                    if (Atend.Base.Base.BUnit.GetFromPoshtiban())
                    {
                        ed.WriteMessage("Update B_Unit\n");
                    }

                }
                ed.WriteMessage("5\n");
                string[] value ={ "False", "False", "False", "False","C:" };

                value[0] = chkConnectToPoshtiban.Checked.ToString();
                value[1] = chkUpdateProduct.Checked.ToString();
                value[2] = chkUpdateDesign.Checked.ToString();
                value[3] = chkShowProduct.Checked.ToString();
                value[4] = txtOpenPath.Text;
                Atend.Control.ConnectionString.RecentPath = txtOpenPath.Text; ;
                ed.WriteMessage("6\n");
                for (int i = 0; i <= 4; i++)
                {
                    Atend.Base.Base.BSetting BS = Atend.Base.Base.BSetting.SelectByCode(i + 1);
                    BS.Value = value[i];
                    if (!BS.Update())
                    {
                        ed.WriteMessage("Error In Update\n");
                    }
                }
                ed.WriteMessage("7\n");
                string FileName = txtLogo.Text.Substring(txtLogo.Text.LastIndexOf("\\") + 1);
                if(txtLogo.Text != Atend.Control.Common.fullPath + "\\SupportFiles\\" + FileName)
                    System.IO.File.Copy(txtLogo.Text, Atend.Control.Common.fullPath + "\\SupportFiles\\" + FileName, true);

                Atend.Control.ConnectionString.LogoName = txtName.Text;
                Atend.Control.ConnectionString.LogoPath = Atend.Control.Common.fullPath + "\\SupportFiles\\" + FileName;


                if (chkStatusDef.Checked)
                {
                    Atend.Control.ConnectionString.StatusDef = true;
                }
                else
                {
                    Atend.Control.ConnectionString.StatusDef = false;
                }


                if (chkFollowParent.Checked)
                {
                    Atend.Control.ConnectionString.FollowParent = true;
                }
                else
                {
                    Atend.Control.ConnectionString.FollowParent = false;
                }

                this.Cursor = Cursors.Default;
                ed.WriteMessage("8\n");
            }
            catch (System.Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(string.Format("error in Transfer Data={0}", ex.Message));
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Atend.Base.Base.BProduct.GetFromPoshtiban())
                {
                    ed.WriteMessage("Update B_Product\n");
                }

                if (Atend.Base.Base.BUnit.GetFromPoshtiban())
                {
                    ed.WriteMessage("Update B_Unit\n");
                }

                if (Atend.Base.Base.BRegion.GetFromPoshtiban())
                {
                    ed.WriteMessage("Update B_Region\n");
                }
            }
            catch (System.Exception ex)
            { }
        }

        private void frmBSetting_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            txtName.Enabled = true;
            txtName.RightToLeft = RightToLeft.Yes;
            BindDataToPath();

            if (Atend.Control.ConnectionString.LogoPath != string.Empty)
            {
                //MessageBox.Show(Atend.Control.ConnectionString.LogoPath);

                if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                    pictureBox1.ImageLocation = Atend.Control.ConnectionString.LogoPath;
            }
            else
            {
                if (System.IO.File.Exists(Atend.Control.Common.fullPath + "\\SupportFiles\\SelectPic.JPG"))
                {
                    pictureBox1.ImageLocation = Atend.Control.Common.fullPath + "\\SupportFiles\\SelectPic.JPG";

                    txtLogo.Text = Atend.Control.Common.fullPath + "\\SupportFiles\\SelectPic.JPG";
                }
            }

            BindDatatoCheckBox();
            EventArgs e1 = new EventArgs();
            chkConnectToPoshtiban_CheckedChanged(chkConnectToPoshtiban, e1);
            BindDataToDBVersion();
        }

        private void BindDataToPath()
        {
            //txtOpenPath.Text = Atend.Base.Base.BSetting.Select(Convert.ToInt32(Atend.Control.Enum.Setting.OpenDefaultPath)).Value;
            txtOpenPath.Text = Atend.Control.ConnectionString.RecentPath;
            txtName.Text = Atend.Control.ConnectionString.LogoName;
            txtLogo.Text = Atend.Control.ConnectionString.LogoPath;
        }

        private void BindDataToDBVersion()
        {
            DataTable DTVersion = Atend.Base.Base.BDBVersion.SelectAll();
            label5.Text = DTVersion.Rows[0]["Version"].ToString();

            DataTable DTVersionServer = Atend.Base.Base.BDBVersion.ServerSelectAll();
            label6.Text = DTVersionServer.Rows[0]["Version"].ToString();

        }

        private void BindDatatoCheckBox()
        {
            
            chkConnectToPoshtiban.Checked = Convert.ToBoolean(Atend.Base.Base.BSetting.SelectByCode(Convert.ToInt32(Atend.Control.Enum.Setting.ConnectToPoshtiban)).Value);
            chkShowProduct.Checked = Convert.ToBoolean(Atend.Base.Base.BSetting.SelectByCode(Convert.ToInt32(Atend.Control.Enum.Setting.ShowProduct)).Value);
            chkUpdateDesign.Checked = Convert.ToBoolean(Atend.Base.Base.BSetting.SelectByCode(Convert.ToInt32(Atend.Control.Enum.Setting.UpdateDesign)).Value);
            chkUpdateProduct.Checked = Convert.ToBoolean(Atend.Base.Base.BSetting.SelectByCode(Convert.ToInt32(Atend.Control.Enum.Setting.UpdateProduct)).Value);

            if (Atend.Control.ConnectionString.StatusDef)
            {
                chkStatusDef.Checked = true;
            }
            if (Atend.Control.ConnectionString.FollowParent)
            {
                chkFollowParent.Checked = true;
            }
            
        }

        private void chkConnectToPoshtiban_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConnectToPoshtiban.Checked)
            {
                chkUpdateDesign.Enabled = true;
                chkUpdateProduct.Enabled = true;
            }
            else
            {
                chkUpdateDesign.Enabled = false;
                chkUpdateProduct.Enabled = false;
            }
        }

        private void btnUpdateDesign_Click(object sender, EventArgs e)
        {
            if (Atend.Base.Design.DDesign.GetFromPoshtiban())
            {
                ed.WriteMessage("update DDesign\n");
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
           if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
           {
               txtOpenPath.Text = folderBrowserDialog1.SelectedPath;
           }
        }

        private void btnLogo_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "Image File (JPG , JPEG , PNG , BMP) |*.jpg|*.jpeg|*.PNG|*.BMP";
            //MessageBox.Show("ردیییییییییییییییییییییفه");
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLogo.Text = openFileDialog1.FileName;
                pictureBox1.ImageLocation = txtLogo.Text;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkUpdateProduct_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkUpdateDesign_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}