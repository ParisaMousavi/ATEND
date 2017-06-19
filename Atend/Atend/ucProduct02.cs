using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Atend
{
    public partial class ucProduct02 : UserControl
    {

        Atend.Control.Enum.ProductType SelectedProductType = new Atend.Control.Enum.ProductType();


        public ucProduct02()
        {
            InitializeComponent();
        }

        private void ShowContext(object Sender)
        {
            cmRight.Show(((Button)Sender), ((Button)Sender).Size.Width, 0);
        }


        private void btnCabel_Click(object sender, EventArgs e)
        {
            //Atend.Equipment.frmGroundCabel02 cabel = new Atend.Equipment.frmGroundCabel02();
            //cabel.ShowDialog();
            cmCabel.Show(((Button)sender), ((Button)sender).Size.Width, 0);

        }

        private void btnAutoKey_Click(object sender, EventArgs e)
        {

            cmKey.Show(btnAutoKey, btnAutoKey.Size.Width, 0);
        }

        private void btnBreaker_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmBreaker02 breaker = new Atend.Equipment.frmBreaker02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(breaker);
        }

        private void btnBus_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmBus02 bus = new Atend.Equipment.frmBus02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(bus);
        }

        private void btnConductor_Click(object sender, EventArgs e)
        {
            cmConductor.Show(btnConductor, btnConductor.Size.Width, 0);


        }

        private void btnCatOut_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmCatOut02 catout = new Atend.Equipment.frmCatOut02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(catout);
        }

        private void btnDB_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmDB03 db = new Atend.Equipment.frmDB03();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(db);
        }

        private void btnHeaderCabel_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmHeaderCabel02 headerCabel = new Atend.Equipment.frmHeaderCabel02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(headerCabel);
        }

        private void btnDisconnector_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmDisconnector02 disconector = new Atend.Equipment.frmDisconnector02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(disconector);
        }

        private void btnCountor_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmCountor02 countor = new Atend.Equipment.frmCountor02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(countor);

        }

        private void btnPhotoCell_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmPhotoCell02 photocell = new Atend.Equipment.frmPhotoCell02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(photocell);
        }

        private void btnPhuse_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmPhuse02 phuse = new Atend.Equipment.frmPhuse02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(phuse);
        }

        private void btnPole_Click(object sender, EventArgs e)
        {
            cmdPole.Show(btnPole, btnPole.Size.Width, 0);
           

        }

        private void btnRod_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmRod02 rod = new Atend.Equipment.frmRod02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(rod);
        }

        private void btnStreetBox_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmStreetBox02 streetBox = new Atend.Equipment.frmStreetBox02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(streetBox);
        }

        private void btnTower_Click(object sender, EventArgs e)
        {
        }

        private void btnTransformer_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmTransformer02 transformer = new Atend.Equipment.frmTransformer02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(transformer);
        }

        private void btnCT_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmCT02 ct = new Atend.Equipment.frmCT02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(ct);
        }

        private void btnPT_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmPT02 pt = new Atend.Equipment.frmPT02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(pt);
        }

        private void btnInsulator_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmInsulator02 insulator = new Atend.Equipment.frmInsulator02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(insulator);
        }

        private void btnReCloser_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmReCloser02 reCloser = new Atend.Equipment.frmReCloser02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(reCloser);
        }

        private void کلیداتوماتیک3فازToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmAutoKey3p02 autokey3 = new Atend.Equipment.frmAutoKey3p02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(autokey3);
        }

        private void کلیدفیوزToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmPhuseKey02 phuseKey = new Atend.Equipment.frmPhuseKey02();

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(phuseKey);
        }

        private void btnConsol_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmConsol02 consol = new Atend.Equipment.frmConsol02();
            consol.Show();
        }

        private void کلیدمینیاتورToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmMiniatorKey02 Miniatorkey = new Atend.Equipment.frmMiniatorKey02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Miniatorkey);
        }

        private void btnPhusePole_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmPhusePole02 phusePole = new Atend.Equipment.frmPhusePole02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(phusePole);
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            //Atend.Equipment.frmPost frmpost = new Atend.Equipment.frmPost();
            //frmpost.ShowDialog();
        }

        private void ucProduct_Load(object sender, EventArgs e)
        {

        }

        private void btnJackPanel_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frm20kvJackPanel02 jackPanel = new Atend.Equipment.frm20kvJackPanel02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(jackPanel);
        }

        private void btnAirPost_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmAirPost02 frmAirpost = new Atend.Equipment.frmAirPost02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmAirpost);
        }

        private void btnSectionLizer_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmSectionLizer02 sectionLizer = new Atend.Equipment.frmSectionLizer02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(sectionLizer);
        }

        private void btnGroundPost_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmGroundPost02 groundPost = new Atend.Equipment.frmGroundPost02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(groundPost);
        }

        private void btnKhazan_Click(object sender, EventArgs e)
        {
            cmKhazan.Show(btnKhazan, btnKhazan.Size.Width, 0);

        }

        private void btnMafsal_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmMafsal02 mafsal = new Atend.Equipment.frmMafsal02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(mafsal);
        }

        private void btnWeekJackPanelWeek_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmJackPanelWeek03 jackPanel = new Atend.Equipment.frmJackPanelWeek03();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(jackPanel);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmConductor02 conductor = new Atend.Equipment.frmConductor02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(conductor);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmConductorTip02 conductortip = new Atend.Equipment.frmConductorTip02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(conductortip);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Atend.Equipment.frmCell02 cell = new Atend.Equipment.frmCell02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(cell);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmkhazan02 khazan = new Atend.Equipment.frmkhazan02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(khazan);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmKhazanTip02 khazantip = new Atend.Equipment.frmKhazanTip02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(khazantip);
        }

        private void btnSelfKeeperCabel_Click(object sender, EventArgs e)
        {
            cmSelfKeeperCable.Show(btnSelfKeeperCabel, btnSelfKeeperCabel.Size.Width, 0);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmSelfKeeper02 selfKeeper = new Atend.Equipment.frmSelfKeeper02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(selfKeeper);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmSelfKeeperTip02 selfKeeperTip = new Atend.Equipment.frmSelfKeeperTip02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(selfKeeperTip);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmOperation02 pre = new Atend.Equipment.frmOperation02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(pre);
        }

        private void btnKablSho_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmKablsho02 kablsho = new Atend.Equipment.frmKablsho02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(kablsho);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmClamp02 clamp = new Atend.Equipment.frmClamp02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(clamp);
        }

        private void cmdPole_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmPole02 pole = new Atend.Equipment.frmPole02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(pole);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmPoleTip02 pole = new Atend.Equipment.frmPoleTip02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(pole);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmInsulatorChain02 insch= new Atend.Equipment.frmInsulatorChain02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(insch);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmGroundCabel02 GC = new Atend.Equipment.frmGroundCabel02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(GC);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmGroundCabelTip02 GC = new Atend.Equipment.frmGroundCabelTip02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(GC);

        }

        private void btnFloor_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmFloor F = new Atend.Equipment.frmFloor();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(F);
        }

        private void btnHalter_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmHalter03 F = new Atend.Equipment.frmHalter03();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(F);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmLight02 light = new Atend.Equipment.frmLight02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(light);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmGround02 ground = new Atend.Equipment.frmGround02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(ground);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmMeasuredJackPanel02 measure = new Atend.Equipment.frmMeasuredJackPanel02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(measure);
        }

        private void btnRamp_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmRamp02 ramp = new Atend.Equipment.frmRamp02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(ramp);
        }

        private void btnProp_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmProp02 Prop = new Atend.Equipment.frmProp02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Prop);
        }

        private void btnInsulatorPipe_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmInsulatorPipe02  Pipe = new Atend.Equipment.frmInsulatorPipe02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Pipe);
        }

        private void btnArm_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmArm02  Arm = new Atend.Equipment.frmArm02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Arm);
        }

    }
}
