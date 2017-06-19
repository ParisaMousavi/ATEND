using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Atend.Equipment;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

//using System.Configuration;

namespace Atend.Base
{
    public partial class frmProductSearch : Form
    {
        Atend.Control.Enum.ProductType type;
        bool ForceToClose = false;
        public frmProductSearch(Atend.Control.Enum.ProductType Type)
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
            type = Type;
        }

        private void search()
        {
            gvProduct.AutoGenerateColumns = false;
            gvProduct.DataSource = Atend.Base.Base.BProduct.SearchX(Convert.ToInt32(type), tstName.Text);
            ShowNumberColumn(gvProduct);
        }

        private void ShowNumberColumn(DataGridView _DataGridView)
        {
            //DataGridViewTextBoxColumn tx=new DataGridViewTextBoxColumn()
            //DataGridViewColumn c = new DataGridViewColumn();
            //c.Index = 0;
            //c.Name = "COLNO";
            //c.HeaderText = "ردیف";
            _DataGridView.Columns.Add("COLNO", "ردیف");
            int counter = 1;
            //_DataGridView.Width = 10;

            foreach (DataGridViewRow gr in _DataGridView.Rows)
            {
                gr.Cells["COLNO"].Value = counter.ToString();
                counter++;
            }
            _DataGridView.EndEdit();
            _DataGridView.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            Close();
            
        }


        private void frmProductSearch_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //gvProduct.AutoGenerateColumns = false;
            //gvProduct.DataSource = Atend.Base.Base.BProduct.SelectByTypeX(Convert.ToInt32(type));
            search();

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tstName_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void gvProduct_DoubleClick(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (gvProduct.Rows.Count > 0)
            {
                //ed.WriteMessage("@@@Selected Product{0}\n", gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
                Atend.Control.Common.selectedProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells["COLID"].Value.ToString());
                //ed.WriteMessage("BeforPAge\n");
                this.Close();
            }
            ////if (type == Atend.Control.Enum.ProductType.SectionLizer)
            ////{
            ////    Atend.Equipment.frmSectionLizer frmsectionLizer = new Atend.Equipment.frmSectionLizer();
            ////    frmsectionLizer.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmsectionLizer.ShowDialog();
            ////}

            ////if (type == Atend.Control.Enum.ProductType.Conductor)
            ////{
            ////    Atend.Equipment.frmConductor frmconductor = new Atend.Equipment.frmConductor();
            ////    frmconductor.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmconductor.ShowDialog();

            ////}
            ////if (type == Atend.Control.Enum.ProductType.Countor)
            ////{
            ////    Atend.Equipment.frmCountor frmcountor = new frmCountor();
            ////    frmcountor.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmcountor.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.AirPost)
            ////{
            ////    Atend.Equipment.frmAirPost airPost = new Atend.Equipment.frmAirPost();
            ////    Atend.Control.Common.flag = false;
            ////    airPost.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    airPost.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.AuoKey3p)
            ////{
            ////    Atend. Equipment.frmAutoKey3p autoKey3p = new frmAutoKey3p();
            ////    autoKey3p.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    autoKey3p.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.Breaker)
            ////{
            ////    Atend.Equipment.frmBreaker frmbreaker = new frmBreaker();
            ////    frmbreaker.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmbreaker.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.Bus)
            ////{
            ////    Atend.Equipment.frmBus frmbus = new frmBus();
            ////    frmbus.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmbus.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.GroundCabel)
            ////{
            ////    frmGroundMiddleCabel frmcabel = new frmGroundMiddleCabel();
            ////    frmcabel.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmcabel.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.CatOut)
            ////{
            ////    //frmCatOut frmcatOut = new frmCatOut();
            ////    //frmcatOut.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    //frmcatOut.ShowDialog();
            ////    Atend.Control.Common.selectedProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    //MessageBox.Show(" Atend.Control.Common.selectedProductCode= " + Atend.Control.Common.selectedProductCode.ToString()+"\n");
            ////    this.Close();
            ////}

            ////if (type == Atend.Control.Enum.ProductType.CT)
            ////{
            ////    frmCT frmct = new frmCT();
            ////    frmct.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmct.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.PT)
            ////{
            ////    frmPT frmpt = new frmPT();
            ////    frmpt.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmpt.ShowDialog();

            ////}


            ////if (type == Atend.Control.Enum.ProductType.DB)
            ////{
            ////    frmDB frmdb = new frmDB();
            ////    Atend.Control.Common.flag = false;
            ////    frmdb.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmdb.ShowDialog();

            ////}


            ////if (type == Atend.Control.Enum.ProductType.HeaderCabel)
            ////{
            ////    frmHeaderCabel frmheaderCabel = new frmHeaderCabel();
            ////    frmheaderCabel.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmheaderCabel.ShowDialog();

            ////}


            ////if (type == Atend.Control.Enum.ProductType.Disconnector)
            ////{
            ////    frmDisconnector frmdisconnector = new frmDisconnector();
            ////    frmdisconnector.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmdisconnector.ShowDialog();

            ////}


            //////if (type == Atend.Control.Enum.ProductType.DistributePost)
            //////{
            //////    frmDistributePost frmdistributePost = new frmDistributePost();
            //////    frmdistributePost.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            //////    frmdistributePost.ShowDialog();

            //////}

            ////if (type == Atend.Control.Enum.ProductType.GroundPost)
            ////{
            ////    frmGroundPostcs frmgroundPost = new frmGroundPostcs();
            ////    frmgroundPost.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmgroundPost.ShowDialog();

            ////}
            ////if (type == Atend.Control.Enum.ProductType.JackPanel)
            ////{
            ////    frm20kvJackPanel frmjackPanel = new frm20kvJackPanel();
            ////    Atend.Control.Common.flag = false;
            ////    frmjackPanel.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmjackPanel.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.PhotoCell)
            ////{
            ////    frmPhotoCell frmphotoCell = new frmPhotoCell();
            ////    frmphotoCell.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmphotoCell.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.Phuse)
            ////{
            ////    frmPhuse frmphuse = new frmPhuse();
            ////    frmphuse.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmphuse.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.Pole)
            ////{
            ////    //frmPole frmpole = new frmPole();
            ////    ////Text = gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString();
            ////    //frmpole.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    //Atend.Control.Common.flag = false;
            ////    //frmpole.ShowDialog();
            ////    Atend.Control.Common.selectedProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    this.Close();


            ////}

            ////if (type == Atend.Control.Enum.ProductType.Rod)
            ////{
            ////    frmRod frmrod = new frmRod();
            ////    //Text = gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString();
            ////    frmrod.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmrod.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.StreetBox)
            ////{
            ////    frmStreetBox frmstreetBox = new frmStreetBox();
            ////    Atend.Control.Common.flag = false;
            ////    frmstreetBox.ProductCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmstreetBox.ShowDialog();

            ////}

            //////if (type == Atend.Control.Enum.ProductType.Tower)
            //////{
            //////    frmTower frmtower = new frmTower();
            //////    frmtower.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            //////    frmtower.ShowDialog();

            //////}
            ////if (type == Atend.Control.Enum.ProductType.Transformer)
            ////{
            ////    frmTransformer frmtransformer = new frmTransformer();
            ////    frmtransformer.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmtransformer.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.Insulator)
            ////{
            ////    frmInsulator frminsulator = new frmInsulator();
            ////    frminsulator.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frminsulator.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.ReCloser)
            ////{
            ////    frmReCloser frmreCloser = new frmReCloser();
            ////    frmreCloser.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmreCloser.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.PhusePole)
            ////{
            ////    frmPhusePole frmphusePole = new frmPhusePole();
            ////    frmphusePole.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmphusePole.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.PhuseKey)
            ////{
            ////    frmPhuseKey frmphuseKey = new frmPhuseKey();
            ////    frmphuseKey.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmphuseKey.IsPhuseKey = true;
            ////    frmphuseKey.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.MiniatureKey)
            ////{
            ////    frmPhuseKey frmphuseKey = new frmPhuseKey();
            ////    frmphuseKey.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmphuseKey.IsPhuseKey = false;
            ////    frmphuseKey.ShowDialog();

            ////}

            ////if (type == Atend.Control.Enum.ProductType.Consol)
            ////{
            ////    frmConsol frmconsol = new frmConsol();
            ////    Atend.Control.Common.flag = false;
            ////    frmconsol.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmconsol.ShowDialog();
            ////}

            ////if (type == Atend.Control.Enum.ProductType.Khazan)
            ////{
            ////    frmkhazan  frmkhazan = new frmkhazan();
            ////    Atend.Control.Common.flag = false;
            ////    frmkhazan.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmkhazan.ShowDialog();
            ////}

            ////if (type == Atend.Control.Enum.ProductType.Mafsal)
            ////{
            ////    frmMafsal frmMafsal = new frmMafsal();
            ////    Atend.Control.Common.flag = false;
            ////    frmMafsal.productCode = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
            ////    frmMafsal.ShowDialog();
            ////}


        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tstName_TextBoxTextAlignChanged(object sender, EventArgs e)
        {

        }

        private void gvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("btnOk");
            //btnOk.DialogResult = DialogResult.OK;
            //MessageBox.Show(btnOk.DialogResult.ToString());
            //this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!Atend.Base.Base.BProduct.GetFromServer(Convert.ToInt32(type)))
                {
                    throw new System.Exception("Atend.Base.Base.BProduct.GetFromServer2 failed");
                }
                this.Cursor = Cursors.Default;
                search();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("*** ERROR :{0} \n", ex.Message);

                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "خطا";
                notification.Msg = "به روز رسانی تجهیز مورد نظر امکان پذیر نمی باشد";
                notification.infoCenterBalloon();

                this.Cursor = Cursors.Default;
            }
        }
    }
}