using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using System.Collections;


namespace Atend.Design
{
    public partial class frmUser : Form
    {

        public int SelectUser = 0;
        public string SelectUserName = string.Empty;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        public frmUser()
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

        private bool validation()
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

            if (string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                MessageBox.Show("لطفا تایید کلمه عبور را مشخص نمایید", "خطا");
                txtConfirmPassword.Focus();
                return false;

            }

            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                MessageBox.Show("لطفا کلمه عبور و تایید کلمه عبور را مانند هم وارد نمایید", "خطا");
                txtConfirmPassword.Focus();
                return false;
            }
            Atend.Base.Design.DUser user = Atend.Base.Design.DUser.SelectByUsernameAndPassword(txtUserName.Text, txtPassword.Text);
            //ed.WriteMessage("SelectUser:" + SelectUser.ToString() + "\n");
            //ed.WriteMessage("SelectUserName:" + SelectUserName.ToString() + "\n");

            if (SelectUser == 0)
            {
                if (user.Code != -1)
                {
                    MessageBox.Show("نام کاربری تکراری می باشد", "خطا");
                    txtUserName.Focus();
                    return false;
                }
            }
            else
            {
                //ed.WriteMessage("user.UserName:" + user.UserName + " SelectUserName:" + SelectUserName + "\n");
                if (user.Code != -1)
                {
                    if (user.Code != SelectUser)
                    {
                        MessageBox.Show("نام کاربری تکراری می باشد", "خطا");
                        txtUserName.Focus();
                        return false;
                    }
                }

            }
            return true;

        }

        private void Reset()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFamily.Text = string.Empty;
            SelectUser = 0;
            ClearCheck(tvAccess);
            rdoCustome.Checked = true;
        }

        private void ClearCheck(TreeView treeView)
        {
            foreach (TreeNode rootNode in treeView.Nodes)
            {
                rootNode.Checked = false;
            }

        }

        public void BindDataToOwnControl(int Code)
        {
            SelectUser = Code;
            //MessageBox.Show(" SelectConductor:" + SelectUser.ToString() + "\n");
            Atend.Base.Design.DUser user = Atend.Base.Design.DUser.SelectByCode(Code);
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(conductor.ProductCode);
            //MessageBox.Show("");
            //MessageBox.Show("Code:" +user.Code.ToString());
            SelectUser = user.Code;
            //ed.WriteMessage("SelectUser:{0}\n", SelectUser);
            SelectUserName = user.UserName;
            txtUserName.Text = user.UserName;
            txtPassword.Text = user.Password;
            txtName.Text = user.Name;
            txtFamily.Text = user.Family;

            DataTable dt = Atend.Base.Design.DUserAccess.SelectByUserId(user.Code);
            foreach (DataRow dr in dt.Rows)
            {

                foreach (TreeNode tr in tvAccess.Nodes)
                {
                    if (Convert.ToInt32(dr["IDAccess"]) == Convert.ToInt32(tr.Tag))
                    {
                        tr.Checked = true;
                    }
                }

            }

            //////if (user.Permission == 1)
            //////{
            //////    rdbAddmin.Checked = true;
            //////}
            //////else
            //////{
            //////    rdbDesigner.Checked = true;
            //////}

            //BindTreeViwAndGrid(tvAccess);

        }

        public void BindDataToTreeViw()
        {

            DataTable Access = Atend.Base.Design.DAccess.SelectAll();
            foreach (DataRow dr in Access.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = dr["Name"].ToString();
                node.Tag = dr["Code"].ToString();
                tvAccess.Nodes.Add(node);

            }

        }

        private void BindTreeViwAndGrid(TreeView treeView, DataGridView dataGridView)
        {
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            //ClearCheckAndGrid(tvAccess, gvAccess);
            //dataGridView.Refresh();
            //for (int i = 0; i < Atend.Base.Design.DUser.nodeKey.Count; i++)
            //{
            //    string s = Atend.Base.Design.DUser.nodeKey[i].ToString();
            //    foreach (TreeNode rootnode in treeView.Nodes)
            //    {

            //        foreach (TreeNode chileNode in rootnode.Nodes)
            //        {

            //            if (int.Parse(chileNode.Tag.ToString()) == int.Parse(s))
            //            {

            //                chileNode.Checked = true;
            //                dataGridView.Rows.Add();
            //                dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0].Value = chileNode.Tag;
            //                dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[1].Value = chileNode.Text;

            //            }

            //        }

            //    }



            //}
        }

        private void Save()
        {
            //ed.WriteMessage("Save\n");
            Atend.Base.Design.DUser user = new Atend.Base.Design.DUser();
            user.UserName = txtUserName.Text;
            user.Password = txtPassword.Text;
            user.Name = txtName.Text;
            user.Family = txtFamily.Text;

            foreach (TreeNode tr in tvAccess.Nodes)
            {
                if (tr.Checked)
                {
                    user.AccessList.Add(Convert.ToInt32(tr.Tag));
                }
            }



            if (SelectUser == 0)
            {
                ed.WriteMessage("INSERT \n");
                if (user.insertX())
                {
                    Reset();
                }
                else
                {
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");

                }
            }
            else
            {
                ed.WriteMessage("UPDATE \n");
                user.Code = SelectUser;
                if (user.updateX())
                {
                    Reset();
                }
                else
                {
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");

                }
            }
        }

        private void Delete()
        {
            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectUser != 0)
                {
                    if (Atend.Base.Design.DUser.Delete(SelectUser))
                    {
                        Reset();
                    }
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                Save();
                Close();
            }
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            frmUserSearch search = new frmUserSearch(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(search);
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(login);
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            BindDataToTreeViw();
        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                Save();
            }

        }

        private void adoFull_CheckedChanged(object sender, EventArgs e)
        {
            if (adoFull.Checked)
            {
                groupBox1.Enabled = false;
                foreach (TreeNode tr in tvAccess.Nodes)
                {
                    tr.Checked = true;
                }
            }
        }

        private void rdoCustome_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCustome.Checked)
            {
                groupBox1.Enabled = true;
            }
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            if (Atend.Base.Design.DUser.ShareOnServer())
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "";
                notification.Msg = "عملیات انتقال با موفقیت انجام شد";
                notification.infoCenterBalloon();
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "";
                notification.Msg = "خطا در عملیات انتقال اطلاعات";
                notification.infoCenterBalloon();
            }

        }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            if (Atend.Base.Design.DAccess.GetUserAccessFromServer())
                MessageBox.Show("عملیات دریافت با موفقیت انجام شد");
            else
                MessageBox.Show("عملیات دریافت با خطا مواجه شد");
        }

        private void txtFamily_TextChanged(object sender, EventArgs e)
        {

        }
    }
}