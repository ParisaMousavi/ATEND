﻿namespace Atend.Design
{
    partial class frmSettingServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettingServer));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboMode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnServerTest = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDataBase = new System.Windows.Forms.TextBox();
            this.txtPasswordServer = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtUsernameServer = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnLocalTest = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPasswordLocal = new System.Windows.Forms.TextBox();
            this.txtUsernamelocal = new System.Windows.Forms.TextBox();
            this.txtaddressLocal = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.پروندهToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ذخیرهسازیToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ذخیرهسازیوخروجToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.خروجToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboMode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnServerTest);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDataBase);
            this.groupBox1.Controls.Add(this.txtPasswordServer);
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Controls.Add(this.txtUsernameServer);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 182);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "تنظیمات با پایگاه داده سرور";
            // 
            // cboMode
            // 
            this.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMode.FormattingEnabled = true;
            this.cboMode.Items.AddRange(new object[] {
            "SQL Server Authentication",
            "Windows Authentication"});
            this.cboMode.Location = new System.Drawing.Point(214, 46);
            this.cboMode.Name = "cboMode";
            this.cboMode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboMode.Size = new System.Drawing.Size(190, 21);
            this.cboMode.TabIndex = 2;
            this.cboMode.SelectedIndexChanged += new System.EventHandler(this.cboMode_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(410, 119);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "نام DataBase  :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(435, 94);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "کلمه رمز :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnServerTest
            // 
            this.btnServerTest.Location = new System.Drawing.Point(6, 155);
            this.btnServerTest.Name = "btnServerTest";
            this.btnServerTest.Size = new System.Drawing.Size(75, 23);
            this.btnServerTest.TabIndex = 5;
            this.btnServerTest.Text = "تست اتصال";
            this.btnServerTest.UseVisualStyleBackColor = true;
            this.btnServerTest.Click += new System.EventHandler(this.btnServerTest_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 72);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "کلمه عیور :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(434, 26);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "نام سرور :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDataBase
            // 
            this.txtDataBase.Location = new System.Drawing.Point(214, 117);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.ReadOnly = true;
            this.txtDataBase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDataBase.Size = new System.Drawing.Size(190, 21);
            this.txtDataBase.TabIndex = 15;
            this.txtDataBase.Text = "AtendServer";
            // 
            // txtPasswordServer
            // 
            this.txtPasswordServer.Location = new System.Drawing.Point(214, 92);
            this.txtPasswordServer.Name = "txtPasswordServer";
            this.txtPasswordServer.PasswordChar = '*';
            this.txtPasswordServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPasswordServer.Size = new System.Drawing.Size(190, 21);
            this.txtPasswordServer.TabIndex = 4;
            this.txtPasswordServer.UseSystemPasswordChar = true;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(214, 23);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtServerName.Size = new System.Drawing.Size(190, 21);
            this.txtServerName.TabIndex = 1;
            // 
            // txtUsernameServer
            // 
            this.txtUsernameServer.Location = new System.Drawing.Point(214, 69);
            this.txtUsernameServer.Name = "txtUsernameServer";
            this.txtUsernameServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUsernameServer.Size = new System.Drawing.Size(190, 21);
            this.txtUsernameServer.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btnLocalTest);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtPasswordLocal);
            this.groupBox2.Controls.Add(this.txtUsernamelocal);
            this.groupBox2.Controls.Add(this.txtaddressLocal);
            this.groupBox2.Location = new System.Drawing.Point(12, 277);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(522, 176);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "تنظیم Local";
            this.groupBox2.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(450, 59);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "آدرس :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLocalTest
            // 
            this.btnLocalTest.Location = new System.Drawing.Point(3, 147);
            this.btnLocalTest.Name = "btnLocalTest";
            this.btnLocalTest.Size = new System.Drawing.Size(75, 23);
            this.btnLocalTest.TabIndex = 1;
            this.btnLocalTest.Text = "تست اتصال";
            this.btnLocalTest.UseVisualStyleBackColor = true;
            this.btnLocalTest.Click += new System.EventHandler(this.btnLocalTest_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(444, 113);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "کلمه رمز :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(438, 86);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "کلمه عبور :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPasswordLocal
            // 
            this.txtPasswordLocal.Location = new System.Drawing.Point(323, 113);
            this.txtPasswordLocal.Name = "txtPasswordLocal";
            this.txtPasswordLocal.PasswordChar = '*';
            this.txtPasswordLocal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPasswordLocal.Size = new System.Drawing.Size(81, 21);
            this.txtPasswordLocal.TabIndex = 21;
            this.txtPasswordLocal.UseSystemPasswordChar = true;
            // 
            // txtUsernamelocal
            // 
            this.txtUsernamelocal.Location = new System.Drawing.Point(323, 86);
            this.txtUsernamelocal.Name = "txtUsernamelocal";
            this.txtUsernamelocal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUsernamelocal.Size = new System.Drawing.Size(81, 21);
            this.txtUsernamelocal.TabIndex = 20;
            // 
            // txtaddressLocal
            // 
            this.txtaddressLocal.Location = new System.Drawing.Point(247, 59);
            this.txtaddressLocal.Name = "txtaddressLocal";
            this.txtaddressLocal.Size = new System.Drawing.Size(157, 21);
            this.txtaddressLocal.TabIndex = 19;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.پروندهToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(539, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // پروندهToolStripMenuItem
            // 
            this.پروندهToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ذخیرهسازیToolStripMenuItem,
            this.ذخیرهسازیوخروجToolStripMenuItem,
            this.toolStripMenuItem4,
            this.خروجToolStripMenuItem});
            this.پروندهToolStripMenuItem.Name = "پروندهToolStripMenuItem";
            this.پروندهToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.پروندهToolStripMenuItem.Text = "پرونده";
            // 
            // ذخیرهسازیToolStripMenuItem
            // 
            this.ذخیرهسازیToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ذخیرهسازیToolStripMenuItem.Image")));
            this.ذخیرهسازیToolStripMenuItem.Name = "ذخیرهسازیToolStripMenuItem";
            this.ذخیرهسازیToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.ذخیرهسازیToolStripMenuItem.Text = "ذخیره سازی";
            this.ذخیرهسازیToolStripMenuItem.Click += new System.EventHandler(this.ذخیرهسازیToolStripMenuItem_Click);
            // 
            // ذخیرهسازیوخروجToolStripMenuItem
            // 
            this.ذخیرهسازیوخروجToolStripMenuItem.Name = "ذخیرهسازیوخروجToolStripMenuItem";
            this.ذخیرهسازیوخروجToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.ذخیرهسازیوخروجToolStripMenuItem.Text = "ذخیره سازی و خروج";
            this.ذخیرهسازیوخروجToolStripMenuItem.Click += new System.EventHandler(this.ذخیرهسازیوخروجToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(172, 6);
            // 
            // خروجToolStripMenuItem
            // 
            this.خروجToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("خروجToolStripMenuItem.Image")));
            this.خروجToolStripMenuItem.Name = "خروجToolStripMenuItem";
            this.خروجToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.خروجToolStripMenuItem.Text = "خروج";
            this.خروجToolStripMenuItem.Click += new System.EventHandler(this.خروجToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripSeparator1,
            this.toolStripLabel5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(539, 25);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel2.Image")));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(90, 22);
            this.toolStripLabel2.Text = "ذخیره سازی";
            this.toolStripLabel2.Click += new System.EventHandler(this.ذخیرهسازیToolStripMenuItem_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel5.Image")));
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel5.Text = "خروج";
            this.toolStripLabel5.Click += new System.EventHandler(this.خروجToolStripMenuItem_Click);
            // 
            // frmSettingServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 253);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettingServer";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تنظيمات ارتباط با پایگاه داده سرور";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDataBase;
        private System.Windows.Forms.TextBox txtPasswordServer;
        private System.Windows.Forms.TextBox txtUsernameServer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPasswordLocal;
        private System.Windows.Forms.TextBox txtUsernamelocal;
        private System.Windows.Forms.TextBox txtaddressLocal;
        private System.Windows.Forms.Button btnServerTest;
        private System.Windows.Forms.Button btnLocalTest;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem پروندهToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ذخیرهسازیToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ذخیرهسازیوخروجToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem خروجToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.ComboBox cboMode;
        private System.Windows.Forms.ToolStripButton toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripLabel5;
    }
}