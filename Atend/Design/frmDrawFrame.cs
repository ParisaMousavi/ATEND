using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;


namespace Atend.Design
{
    public partial class frmDrawFrame : Form
    {
        System.Data.DataTable PrintSizes;
        bool AllowToClose = true;
        public double _Width = 0, _Height = 0;
        System.Data.DataTable dt_ProductList;
        public System.Data.DataTable dt_Products = new System.Data.DataTable();
        bool ForceToClose = false;

        bool _HaveSign;
        public bool HaveSign
        {
            get { return _HaveSign; }
            set { _HaveSign = value; }
        }
        bool _HaveDescription;
        public bool HaveDescription
        {
            get { return _HaveDescription; }
            set { _HaveDescription = value; }
        }

        bool _HaveInformation;
        public bool HaveInformation
        {
            get { return _HaveInformation; }
            set { _HaveInformation = value; }
        }


        public frmDrawFrame()
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

        private void btnCancle_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BindToCboPrintSize()
        {
            PrintSizes = Atend.Base.Design.DPrintSize.SelectAll();
            cboPageSize.DisplayMember = "Name";
            cboPageSize.ValueMember = "Code";
            cboPageSize.DataSource = PrintSizes;
            if (cboPageSize.Items.Count > 0)
            {
                cboPageSize.SelectedIndex = cboPageSize.SelectedIndex = 0;
            }
        }

        private void frmDrawFrame_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dt_Products.Columns.Add(new System.Data.DataColumn("Type", System.Type.GetType("System.Int32")));
            dt_Products.Columns.Add(new System.Data.DataColumn("ProductCode", System.Type.GetType("System.Int32")));
            dt_Products.Columns.Add(new System.Data.DataColumn("SignText", System.Type.GetType("System.String")));
            Atend.Base.Design.DDesignProfile SelectedDesign = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (SelectedDesign.DesignId != -1)
            {
                txtScale.Text = SelectedDesign.Scale.ToString();
            }
            BindToCboPrintSize();
            BindTogvSigns();

        }

        private void BindTogvSigns()
        {
            //equips = Atend.Global.Acad.Global.FindAllEquips();
            dt_ProductList = Atend.Global.Acad.Global.FindAllEquips();
            foreach (DataRow dr in dt_ProductList.Rows)
            {
                DataGridViewImageCell _IC;
                int NewIndex;

                switch ((Atend.Control.Enum.FrameProductType)Convert.ToInt32(dr["Type"]))
                {
                    case Atend.Control.Enum.FrameProductType.Pole:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "پایه چهارگوش";
                        break;

                    case Atend.Control.Enum.FrameProductType.PoleCircle:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "پایه دایره ای";
                        break;

                    case Atend.Control.Enum.FrameProductType.PolePolygon:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "پایه پرتیک";
                        break;


                    case Atend.Control.Enum.FrameProductType.Conductor:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "سیم";
                        break;
                    case Atend.Control.Enum.FrameProductType.GroundCable:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "کابل زمینی";
                        break;
                    case Atend.Control.Enum.FrameProductType.SelfKeeper:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "کابل خودنگهدار";
                        break;


                    case Atend.Control.Enum.FrameProductType.Breaker:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "دژنکتور";
                        break;

                    case Atend.Control.Enum.FrameProductType.Disconnector:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "سکسیونر";
                        break;

                    case Atend.Control.Enum.FrameProductType.HeaderCable:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "سرکابل";
                        break;
                    case Atend.Control.Enum.FrameProductType.Klamp:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "کلمپ";
                        break;
                    case Atend.Control.Enum.FrameProductType.Kablsho:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "کابلشو";
                        break;



                    case Atend.Control.Enum.FrameProductType.GroundPostOn:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "پست رو زمینی";
                        break;

                    case Atend.Control.Enum.FrameProductType.GroundPostUnder:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "پست زیرزمینی";
                        break;

                    case Atend.Control.Enum.FrameProductType.GroundPostKiusk:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "پست زمینی کیوسک";
                        break;


                    case Atend.Control.Enum.FrameProductType.AirPost:
                        NewIndex = gvSigns.Rows.Add();
                        gvSigns.Rows[NewIndex].Cells["Type"].Value = Convert.ToInt32(dr["Type"]);
                        gvSigns.Rows[NewIndex].Cells["ProductCode"].Value = Convert.ToInt32(dr["ProductCode"]);
                        gvSigns.Rows[NewIndex].Cells[1].Value = 1;


                        _IC = gvSigns.Rows[NewIndex].Cells[2] as DataGridViewImageCell;
                        _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Empty_16.png");
                        gvSigns.Refresh();
                        gvSigns.Rows[NewIndex].Cells[3].Value = "پست هوایی";
                        break;


                }
            }
        }

        private void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow[] rows = PrintSizes.Select("Code=" + cboPageSize.SelectedValue);
            if (rows.Length > 0)
            {
                txtTol1.Text = rows[0]["Height"].ToString();
                txtArz1.Text = rows[0]["Width"].ToString();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPort.Checked)
            {
                int tool = 0, arz = 0;
                tool = 10 * 15;
                arz = 7 * 15;

                pictureBox1.Image = null;
                //MessageBox.Show("ok");
                //Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("QQQQQQ\n");
                //btnOk.Focus();
                Rectangle r = new Rectangle((pictureBox1.Height / 2) - (arz / 2), (pictureBox1.Width / 2) - (tool / 2), arz, tool);
                if (r != null)
                {
                    pictureBox1.CreateGraphics().DrawRectangle(new Pen(Color.DarkBlue, 1), r);
                    //MessageBox.Show("ok");
                }
                //pictureBox1.Refresh();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLand.Checked)
            {
                int tool = 0, arz = 0;
                tool = 10 * 15;
                arz = 7 * 15;

                pictureBox1.Image = null;
                //btnOk.Focus();
                Rectangle r = new Rectangle((pictureBox1.Width / 2) - (tool / 2), (pictureBox1.Height / 2) - (arz / 2), tool, arz);
                if (r != null)
                {
                    pictureBox1.CreateGraphics().DrawRectangle(new Pen(Color.DarkBlue, 1), r);
                    //MessageBox.Show("ok");
                }
                //pictureBox1.Refresh();
            }
        }

        private bool Validation()
        {
            if (rdoSize1.Checked)
            {
                if (txtArz1.Text == string.Empty)
                {
                    txtArz1.Focus();
                    return false;
                }
                if (txtTol1.Text == string.Empty)
                {
                    txtTol1.Focus();
                    return false;
                }

            }


            if (rdoSize2.Checked)
            {
                if (txtTol.Text == string.Empty)
                {
                    txtTol.Focus();
                    return false;
                }
                if (txtArz.Text == string.Empty)
                {
                    txtArz.Focus();
                    return false;
                }

            }



            if (txtScale.Text == string.Empty)
            {
                txtScale.Focus();
                return false;
            }

            //equips.Clear();
            foreach (DataGridViewRow vr in gvSigns.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)vr.Cells[1];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    DataRow ndr = dt_Products.NewRow();
                    ndr["Type"] = vr.Cells["Type"].Value;
                    ndr["ProductCode"] = vr.Cells["ProductCode"].Value;
                    ndr["SignText"] = vr.Cells["SignText"].Value;
                    dt_Products.Rows.Add(ndr);
                }
                //equips.Add(Convert.ToInt32(vr.Cells["Type"].Value));
            }


            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                double Tool = 0, Arz = 0;

                if (rdoSize1.Checked)
                {
                    Tool = Convert.ToInt32(txtTol1.Text) * (Convert.ToInt32(txtScale.Text) / 1000);
                    Arz = Convert.ToInt32(txtArz1.Text) * (Convert.ToInt32(txtScale.Text) / 1000);
                }

                if (rdoSize2.Checked)
                {
                    Tool = Convert.ToInt32(txtTol.Text) * (Convert.ToInt32(txtScale.Text) / 1000);
                    Arz = Convert.ToInt32(txtArz.Text) * (Convert.ToInt32(txtScale.Text) / 1000);
                }

                if (rdoLand.Checked)
                {
                    _Width = Tool;
                    _Height = Arz;
                }
                else
                {
                    _Width = Arz;
                    _Height = Tool;

                }

                HaveDescription = chkDescription.Checked;
                HaveInformation = chkInformation.Checked;
                HaveSign = chkSigns.Checked;

            }
            else
            {
                AllowToClose = false;
            }
        }

        private void frmDrawFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToClose)
            {
                e.Cancel = true;
            }
            AllowToClose = true;
        }

        private void gvSigns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }


    }
}