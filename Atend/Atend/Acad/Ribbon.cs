﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;
using System.Windows.Forms;

using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using Autodesk.AutoCAD.Ribbon;
using System.Runtime.InteropServices;
using System.Windows.Interop;

//get from tehran 7/15
namespace Atend.Acad
{

    public class Ribbon
    {

        //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        // if you want to draw a node on screen you should set a value for this instance
        //public static Atend.Control.Common.NodeInformation MyNodeInformation = new Atend.Control.Common.NodeInformation();
        //public static ResourceDictionary RibbonDictionary;

        public static void EquipmentRibbon()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //RibbonButton rbutton = new RibbonButton();
            //rbutton.Text = "تعیین بلاک برای تجهیزات";
            //rbutton.Id = "btnProductBindBlock";
            //rbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            //rbutton.Size = RibbonItemSize.Large;
            //rbutton.ShowText = true;
            //rbutton.ShowImage = true;
            //rbutton.Click += new System.Windows.RoutedEventHandler(btnProductBindBlock_Click);

            RibbonButton rPolebutton = new RibbonButton();
            rPolebutton.Text = "پایه";
            rPolebutton.Id = "btnPole";
            rPolebutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rPolebutton.Size = RibbonItemSize.Standard;
            rPolebutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Pole32.png", UriKind.RelativeOrAbsolute));
            rPolebutton.ShowText = true;
            rPolebutton.ShowImage = true;
            rPolebutton.Click += new System.Windows.RoutedEventHandler(btnPole_Click);

            RibbonButton rPoleTipbutton = new RibbonButton();
            rPoleTipbutton.Text = "تیپ پایه ";
            rPoleTipbutton.Id = "btnPole";
            rPoleTipbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rPoleTipbutton.Size = RibbonItemSize.Standard;
            rPoleTipbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Pole32.png", UriKind.RelativeOrAbsolute));
            rPoleTipbutton.ShowText = true;
            rPoleTipbutton.ShowImage = true;
            rPoleTipbutton.Click += new System.Windows.RoutedEventHandler(btnPoleTip_Click);

            RibbonButton rConductorbutton = new RibbonButton();
            rConductorbutton.Text = "سیم";
            rConductorbutton.Id = "btnConductor";
            rConductorbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rConductorbutton.Size = RibbonItemSize.Standard;
            rConductorbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Conductor32.png", UriKind.RelativeOrAbsolute));
            rConductorbutton.ShowText = true;
            rConductorbutton.ShowImage = true;
            rConductorbutton.Click += new System.Windows.RoutedEventHandler(btnCounductor_Click);

            RibbonButton btnSelfKeeper = new RibbonButton();
            btnSelfKeeper.Text = "کابل خودنگهدار";
            btnSelfKeeper.Id = "btnSelfKeeper";
            btnSelfKeeper.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnSelfKeeper.Size = RibbonItemSize.Standard;
            btnSelfKeeper.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\SelfKeeperCabel.png", UriKind.RelativeOrAbsolute));
            btnSelfKeeper.ShowText = true;
            btnSelfKeeper.ShowImage = true;
            btnSelfKeeper.Click += new System.Windows.RoutedEventHandler(btnSelfKeeper_Click);

            RibbonButton rGroundCablebutton = new RibbonButton();
            rGroundCablebutton.Text = "کابل زمینی";
            rGroundCablebutton.Id = "btnGroundCable";
            rGroundCablebutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rGroundCablebutton.Size = RibbonItemSize.Standard;
            rGroundCablebutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\GroundCabel.png", UriKind.RelativeOrAbsolute));
            rGroundCablebutton.ShowText = true;
            rGroundCablebutton.ShowImage = true;
            //rGroundCablebutton.IsEnabled = false;
            rGroundCablebutton.Click += new System.Windows.RoutedEventHandler(rGroundCablebutton_Click);

            RibbonButton rDisconnectorbutton = new RibbonButton();
            rDisconnectorbutton.Text = "سکسیونر";
            rDisconnectorbutton.Id = "btnDisconnector";
            rDisconnectorbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rDisconnectorbutton.Size = RibbonItemSize.Standard;
            rDisconnectorbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Dissconnector.png", UriKind.RelativeOrAbsolute));
            rDisconnectorbutton.ShowText = true;
            rDisconnectorbutton.ShowImage = true;
            rDisconnectorbutton.IsEnabled = true;
            rDisconnectorbutton.Click += new System.Windows.RoutedEventHandler(rDisconnectorbutton_Click);

            RibbonButton btnBreaker = new RibbonButton();
            btnBreaker.Text = "دژنگتور";
            btnBreaker.Id = "btnBreaker";
            btnBreaker.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnBreaker.Size = RibbonItemSize.Standard;
            btnBreaker.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Breaker.png", UriKind.RelativeOrAbsolute));
            btnBreaker.ShowText = true;
            btnBreaker.ShowImage = true;
            btnBreaker.Click += new System.Windows.RoutedEventHandler(btnBreaker_Click);

            RibbonButton rCatOutbutton = new RibbonButton();
            rCatOutbutton.Text = "کت اوت";
            rCatOutbutton.Id = "btnCatOut";
            rCatOutbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rCatOutbutton.Size = RibbonItemSize.Standard;
            rCatOutbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\016.png", UriKind.RelativeOrAbsolute));
            rCatOutbutton.ShowText = true;
            rCatOutbutton.ShowImage = true;
            rCatOutbutton.IsEnabled = true;
            rCatOutbutton.IsVisible = true;
            rCatOutbutton.Click += new System.Windows.RoutedEventHandler(rCatOutbutton_Click);

            RibbonButton rJumperbutton = new RibbonButton();
            rJumperbutton.Text = "جامپر";
            rJumperbutton.Id = "btnJumper";
            rJumperbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rJumperbutton.Size = RibbonItemSize.Standard;
            rJumperbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Jumper32.png", UriKind.RelativeOrAbsolute));
            rJumperbutton.ShowText = true;
            rJumperbutton.ShowImage = true;
            //            rJumperbutton.IsEnabled = false;
            rJumperbutton.Click += new System.Windows.RoutedEventHandler(rJumperbutton_Click);

            RibbonButton rAirPostbutton = new RibbonButton();
            rAirPostbutton.Text = "پست هوایی";
            rAirPostbutton.Id = "btnAirPost";
            rAirPostbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rAirPostbutton.Size = RibbonItemSize.Standard;
            rAirPostbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Transformer32.png", UriKind.RelativeOrAbsolute));
            rAirPostbutton.ShowText = true;
            rAirPostbutton.ShowImage = true;
            //EXTRA
            rAirPostbutton.Click += new System.Windows.RoutedEventHandler(rAirPostbutton_Click);

            RibbonButton rGroundPostbutton = new RibbonButton();
            rGroundPostbutton.Text = "پست زمینی";
            rGroundPostbutton.Id = "btnGroundPost";
            rGroundPostbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rGroundPostbutton.Size = RibbonItemSize.Standard;
            rGroundPostbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\GroundPost32.png", UriKind.RelativeOrAbsolute));
            rGroundPostbutton.ShowText = true;
            rGroundPostbutton.ShowImage = true;
            //EXTRA
            rGroundPostbutton.Click += new System.Windows.RoutedEventHandler(rGroundPostbutton_Click);

            RibbonButton rRodbutton = new RibbonButton();
            rRodbutton.Text = "برق گیر";
            rRodbutton.Id = "btnRod";
            rRodbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rRodbutton.Size = RibbonItemSize.Standard;
            rRodbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Rod32.png", UriKind.RelativeOrAbsolute));
            rRodbutton.ShowText = true;
            rRodbutton.ShowImage = true;
            rRodbutton.IsEnabled = true;
            rRodbutton.Click += new System.Windows.RoutedEventHandler(rRodbutton_Click);


            RibbonButton rKhazanTipbutton = new RibbonButton();
            rKhazanTipbutton.Text = "خازن";
            rKhazanTipbutton.Id = "btnKhazanTip";
            rKhazanTipbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rKhazanTipbutton.Size = RibbonItemSize.Standard;
            rKhazanTipbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Capacitor32.png", UriKind.RelativeOrAbsolute));
            rKhazanTipbutton.ShowText = true;
            rKhazanTipbutton.ShowImage = true;
            rKhazanTipbutton.IsEnabled = true;
            rKhazanTipbutton.Click += new System.Windows.RoutedEventHandler(rKhazanTipbutton_Click);


            RibbonButton btnMafsal = new RibbonButton();
            btnMafsal.Text = "مفصل";
            btnMafsal.Id = "btnMafsal";
            btnMafsal.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnMafsal.Size = RibbonItemSize.Standard;
            btnMafsal.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Equip32.png", UriKind.RelativeOrAbsolute));
            btnMafsal.ShowText = true;
            btnMafsal.ShowImage = true;
            btnMafsal.IsEnabled = false;
            btnMafsal.IsVisible = false;
            //btnMafsal.Click += new System.Windows.RoutedEventHandler(btnMafsal_Click);


            RibbonButton btnHeaderCable = new RibbonButton();
            btnHeaderCable.Text = "سرکابل";
            btnHeaderCable.Id = "btnHeaderCable";
            btnHeaderCable.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnHeaderCable.Size = RibbonItemSize.Standard;
            btnHeaderCable.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\HeaderCabel.png", UriKind.RelativeOrAbsolute));
            btnHeaderCable.ShowText = true;
            btnMafsal.ShowImage = true;
            btnHeaderCable.Click += new System.Windows.RoutedEventHandler(btnHeaderCable_Click);

            RibbonButton btnCalamp = new RibbonButton();
            btnCalamp.Text = "کلمپ";
            btnCalamp.Id = "btnCalamp";
            btnCalamp.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnCalamp.Size = RibbonItemSize.Standard;
            btnCalamp.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Calamp.png", UriKind.RelativeOrAbsolute));
            btnCalamp.ShowText = true;
            btnCalamp.ShowImage = true;
            btnCalamp.Click += new System.Windows.RoutedEventHandler(btnCalamp_Click);


            RibbonButton btnKablsho = new RibbonButton();
            btnKablsho.Text = "کابلشو";
            btnKablsho.Id = "btnbtnKablsho";
            btnKablsho.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnKablsho.Size = RibbonItemSize.Standard;
            btnKablsho.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Kablsho.png", UriKind.RelativeOrAbsolute));
            btnKablsho.ShowText = true;
            btnKablsho.ShowImage = true;
            btnKablsho.Click += new System.Windows.RoutedEventHandler(btnKablsho_Click);

            RibbonButton rConsolbutton = new RibbonButton();
            rConsolbutton.Text = "کنسول";
            rConsolbutton.Id = "btnConsol";
            rConsolbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rConsolbutton.Size = RibbonItemSize.Standard;
            rConsolbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Consol.png", UriKind.RelativeOrAbsolute));
            rConsolbutton.ShowText = true;
            rConsolbutton.ShowImage = true;
            rConsolbutton.Click += new System.Windows.RoutedEventHandler(btnConsol_Click);

            RibbonButton btnStreetBox = new RibbonButton();
            btnStreetBox.Text = "شالتر";
            btnStreetBox.Id = "btnStreetBox";
            btnStreetBox.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnStreetBox.Size = RibbonItemSize.Standard;
            btnStreetBox.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\StreetBox.png", UriKind.RelativeOrAbsolute));
            btnStreetBox.ShowText = true;
            btnStreetBox.ShowImage = true;
            btnStreetBox.Click += new System.Windows.RoutedEventHandler(btnStreetBox_Click);


            RibbonButton btnDb = new RibbonButton();
            btnDb.Text = "جعبه انشعاب";
            btnDb.Id = "btnDb";
            btnDb.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnDb.Size = RibbonItemSize.Standard;
            btnDb.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\DB.png", UriKind.RelativeOrAbsolute));
            btnDb.ShowText = true;
            btnDb.ShowImage = true;
            btnDb.Click += new System.Windows.RoutedEventHandler(btnDb_Click);


            RibbonButton btnGround = new RibbonButton();
            btnGround.Text = "سیستم زمین";
            btnGround.Id = "btnGround";
            btnGround.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnGround.Size = RibbonItemSize.Standard;
            btnGround.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Ground.png", UriKind.RelativeOrAbsolute));
            btnGround.ShowText = true;
            btnGround.ShowImage = true;
            btnGround.Click += new System.Windows.RoutedEventHandler(btnGround_Click);

            RibbonButton btnMeasuredJackPanel = new RibbonButton();
            btnMeasuredJackPanel.Text = "تابلو اندازه گیری";
            btnMeasuredJackPanel.Id = "btnMeasuredJackPanel";
            btnMeasuredJackPanel.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnMeasuredJackPanel.Size = RibbonItemSize.Standard;
            btnMeasuredJackPanel.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\MeasureJackPanel.png", UriKind.RelativeOrAbsolute));
            btnMeasuredJackPanel.ShowText = true;
            btnMeasuredJackPanel.ShowImage = true;
            btnMeasuredJackPanel.Click += new System.Windows.RoutedEventHandler(btnMeasuredJackPanel_Click);

            RibbonButton btnLight = new RibbonButton();
            btnLight.Text = "روشنایی";
            btnLight.Id = "btnLight";
            btnLight.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnLight.Size = RibbonItemSize.Standard;
            btnLight.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\6.png", UriKind.RelativeOrAbsolute));
            btnLight.ShowText = true;
            btnLight.ShowImage = true;
            btnLight.Click += new System.Windows.RoutedEventHandler(btnLight_Click);

            RibbonButton btnTerminal = new RibbonButton();
            btnTerminal.Text = "اتصال";
            btnTerminal.Id = "btnTerminal";
            btnTerminal.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnTerminal.Size = RibbonItemSize.Standard;
            btnTerminal.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Terminal.png", UriKind.RelativeOrAbsolute));
            btnTerminal.ShowText = true;
            btnTerminal.ShowImage = true;
            btnTerminal.Click += new System.Windows.RoutedEventHandler(btnTerminal_Click);


            //------------------------------------------------------------------------------------

            RibbonButton btnForbiddenArea = new RibbonButton();
            btnForbiddenArea.Text = "ناحیه";
            btnForbiddenArea.Id = "btnForbiddenArea";
            btnForbiddenArea.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnForbiddenArea.Size = RibbonItemSize.Standard;
            btnForbiddenArea.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Forb.png", UriKind.RelativeOrAbsolute));
            btnForbiddenArea.ShowText = true;
            btnForbiddenArea.ShowImage = true;
            btnForbiddenArea.Click += new System.Windows.RoutedEventHandler(btnForbiddenArea_Click);



            RibbonButton btnAutoPoleInstallation = new RibbonButton();
            btnAutoPoleInstallation.Text = "پایه گذاری اتوماتیک";
            btnAutoPoleInstallation.Id = "btnAutoPoleInstallation";
            btnAutoPoleInstallation.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnAutoPoleInstallation.Size = RibbonItemSize.Standard;
            btnAutoPoleInstallation.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\AutoPole.png", UriKind.RelativeOrAbsolute));
            btnAutoPoleInstallation.ShowText = true;
            btnAutoPoleInstallation.ShowImage = true;
            //btnAutoPoleInstallation.IsEnabled = false;
            btnAutoPoleInstallation.Click += new System.Windows.RoutedEventHandler(btnAutoPoleInstallation_Click);


            RibbonButton rEditbutton = new RibbonButton();
            rEditbutton.Text = "ویرایش";
            rEditbutton.Id = "btnEdit";
            rEditbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rEditbutton.Size = RibbonItemSize.Standard;
            rEditbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Edit32.png", UriKind.RelativeOrAbsolute));
            rEditbutton.ShowText = true;
            rEditbutton.ShowImage = true;
            //rEditbutton.IsEnabled = false;
            rEditbutton.Click += new System.Windows.RoutedEventHandler(rEditbutton_Click);

            RibbonButton rEditbuttonForConductor = new RibbonButton();
            rEditbuttonForConductor.Text = "ویرایش سیم";
            rEditbuttonForConductor.Id = "btnEditForConductor";
            rEditbuttonForConductor.Orientation = System.Windows.Controls.Orientation.Vertical;
            rEditbuttonForConductor.Size = RibbonItemSize.Standard;
            rEditbuttonForConductor.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\EditBranch32.png", UriKind.RelativeOrAbsolute));
            rEditbuttonForConductor.ShowText = true;
            rEditbuttonForConductor.ShowImage = true;
            rEditbuttonForConductor.IsVisible = false;
            //rEditbutton.IsEnabled = false;
            rEditbuttonForConductor.Click += new System.Windows.RoutedEventHandler(rEditbuttonForConductor_Click);

            RibbonButton rEditCellStatus = new RibbonButton();
            rEditCellStatus.Text = "تغییر وضعیت کلید";
            rEditCellStatus.Id = "btnChangeCellStatus";
            rEditCellStatus.Orientation = System.Windows.Controls.Orientation.Vertical;
            rEditCellStatus.Size = RibbonItemSize.Standard;
            rEditCellStatus.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\ChangeStatuseCell.png", UriKind.RelativeOrAbsolute));
            rEditCellStatus.ShowText = true;
            rEditCellStatus.ShowImage = true;
            rEditCellStatus.IsVisible = true;
            //rEditbutton.IsEnabled = false;
            rEditCellStatus.Click += new System.Windows.RoutedEventHandler(rEditCellStatus_Click);

            RibbonButton btnDeleteEquip = new RibbonButton();
            btnDeleteEquip.Text = "حذف تجهیز";
            btnDeleteEquip.Id = "btnDeleteEquip";
            btnDeleteEquip.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnDeleteEquip.Size = RibbonItemSize.Standard;
            btnDeleteEquip.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Delete.png", UriKind.RelativeOrAbsolute));
            btnDeleteEquip.ShowText = true;
            btnDeleteEquip.ShowImage = true;
            btnDeleteEquip.IsVisible = true;
            //rEditbutton.IsEnabled = false;
            btnDeleteEquip.Click += new System.Windows.RoutedEventHandler(btnDeleteEquip_Click);


            RibbonButton btnRotateEquip = new RibbonButton();
            btnRotateEquip.Text = "چرخش پایه";
            btnRotateEquip.Id = "btnRotateEquip";
            btnRotateEquip.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnRotateEquip.Size = RibbonItemSize.Standard;
            btnRotateEquip.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Equip32.png", UriKind.RelativeOrAbsolute));
            btnRotateEquip.ShowText = true;
            btnRotateEquip.ShowImage = true;
            btnRotateEquip.IsVisible = true;
            //rEditbutton.IsEnabled = false;
            btnRotateEquip.Click += new System.Windows.RoutedEventHandler(btnRotateEquip_Click);


            RibbonRow rbEquipment = new RibbonRow();
            rbEquipment.Title = "تجهیزات";
            //rbEquipment.Items.Add(rEditbutton);
            rbEquipment.Items.Add(rPolebutton);
            rbEquipment.Items.Add(rPoleTipbutton);
            rbEquipment.Items.Add(rConductorbutton);
            rbEquipment.Items.Add(rGroundCablebutton);
            rbEquipment.Items.Add(btnSelfKeeper);

            rbEquipment.Items.Add(rDisconnectorbutton);
            rbEquipment.Items.Add(rCatOutbutton);
            rbEquipment.Items.Add(btnBreaker);
            rbEquipment.Items.Add(rJumperbutton);
            rbEquipment.Items.Add(rAirPostbutton);
            rbEquipment.Items.Add(rGroundPostbutton);
            rbEquipment.Items.Add(rRodbutton);
            rbEquipment.Items.Add(rKhazanTipbutton);
            rbEquipment.Items.Add(btnMafsal);
            rbEquipment.Items.Add(btnHeaderCable);
            rbEquipment.Items.Add(btnCalamp);
            rbEquipment.Items.Add(btnKablsho);
            rbEquipment.Items.Add(rConsolbutton);

            rbEquipment.Items.Add(btnStreetBox);
            rbEquipment.Items.Add(btnDb);
            rbEquipment.Items.Add(btnGround);
            rbEquipment.Items.Add(btnMeasuredJackPanel);
            rbEquipment.Items.Add(btnLight);
            rbEquipment.Items.Add(btnTerminal);
            //rbEquipment.Items.Add(btnForbiddenArea);
            //rbEquipment.Items.Add(btnMiddleJackPanel);
            //rbEquipment.Items.Add(rOffLinebutton);
            //rbEquipment.Items.Add(rConductorbuttonWeek);

            RibbonRow rbEquipWeek = new RibbonRow();
            rbEquipWeek.Title = "تجهیزات";
            rbEquipWeek.Items.Add(rEditbutton);
            rbEquipWeek.Items.Add(rEditbuttonForConductor);
            rbEquipWeek.Items.Add(rEditCellStatus);
            rbEquipWeek.Items.Add(btnDeleteEquip);
            rbEquipWeek.Items.Add(btnRotateEquip);
            //rbEquipWeek.Items.Add(rConductorbuttonWeek);

            RibbonRow rbAutoPole = new RibbonRow();
            rbAutoPole.Title = "پایه گذاری اتوماتیک";
            rbAutoPole.Items.Add(btnAutoPoleInstallation);
            rbAutoPole.Items.Add(btnForbiddenArea);

            RibbonRow rRow = new RibbonRow();
            rRow.Title = "Custome Row";

            /*RibbonRow rRow1 = new RibbonRow();
            rRow1.Title = "Custome Row1";
            rRow1.Items.Add(rbutton);
            */

            /*RibbonPanelSource rps = new RibbonPanelSource();
            rps.Title = "شماتیک";
            rps.Rows.Add(rRow);
            rps.Rows.Add(rRow1);
            */

            RibbonPanelSource rpsMidEquipment = new RibbonPanelSource();
            rpsMidEquipment.Title = "تجهیزات";
            rpsMidEquipment.Rows.Add(rbEquipment);


            RibbonPanelSource rpsWeekEquipment = new RibbonPanelSource();
            rpsWeekEquipment.Title = "ويرايش/حذف";
            rpsWeekEquipment.Rows.Add(rbEquipWeek);


            RibbonPanelSource rpsAutoPole = new RibbonPanelSource();
            rpsAutoPole.Title = "پایه گذاری اتوماتیک";
            rpsAutoPole.Rows.Add(rbAutoPole);

            //RibbonPanel rPanel = new RibbonPanel();
            //rPanel.Source = rps;

            RibbonPanel rpMidEquipment = new RibbonPanel();
            rpMidEquipment.Source = rpsMidEquipment;

            RibbonPanel rpWeekEquipment = new RibbonPanel();
            rpWeekEquipment.Source = rpsWeekEquipment;


            RibbonPanel rpAutoPole = new RibbonPanel();
            rpAutoPole.Source = rpsAutoPole;



            RibbonTab rTab = new RibbonTab();
            rTab.Title = "تجهیزات";
            rTab.Id = "ID_Equipments";
            rTab.IsContextualTab = false;
            //rTab.Panels.Add(rPanel);
            rTab.Panels.Add(rpMidEquipment);
            rTab.Panels.Add(rpWeekEquipment);
            rTab.Panels.Add(rpAutoPole);


            RibbonControl rControl = Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl;
            rControl.Tabs.Add(rTab);
            //rControl.ActiveTab = rTab;

        }

        static void btnTerminal_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawTerminal FrmDrawTerminal = new Atend.Design.frmDrawTerminal();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDrawTerminal) == System.Windows.Forms.DialogResult.OK)
                {
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_Terminal ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void btnDeleteEquip_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //throw new System.Exception("The method or operation is not implemented.");
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                PromptSelectionOptions pso = new PromptSelectionOptions();
                pso.MessageForAdding = "انتخاب محدوده یا تجهیز";
                PromptSelectionResult psr = ed.GetSelection(pso);
                if (psr.Status == PromptStatus.OK)
                {
                    ObjectId[] OIS = psr.Value.GetObjectIds();
                    Atend.Global.Acad.AcadRemove.DeleteCollection(OIS);
                    //foreach (ObjectId oi in OIS)
                    //{
                    //    ed.WriteMessage("~~~@@@~~~:{0}\n",oi);
                    //}
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void btnRotateEquip_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


                PromptEntityOptions peo = new PromptEntityOptions("لطفا پایه مورد نظر را انتخاب نمایید\n");
                PromptEntityResult per = ed.GetEntity(peo);
                if (per.Status == PromptStatus.OK && per.ObjectId != ObjectId.Null)
                {
                    ObjectId Selectedentity = per.ObjectId;
                    if (Selectedentity != ObjectId.Null)
                    {
                        Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(Selectedentity);
                        if (atinfo.ParentCode != "NONE" && (atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole || atinfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
                        {
                            Atend.Design.frmEditAngle frmEditAngle = new Atend.Design.frmEditAngle(atinfo.Angle);
                            if (frmEditAngle.ShowDialog() == DialogResult.OK)
                            {
                                Atend.Global.Acad.DrawEquips.AcDrawPole.RotatePole(frmEditAngle.Angle, new Guid(atinfo.NodeCode));
                            }
                        }
                        else
                        {
                            peo.Message = "تجهیز مورد نظر مجاز نمی باشد";
                        }
                    }
                }
                else
                {
                    ed.WriteMessage("Please select an entity");
                }

            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void btnForbiddenArea_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                doc.SendStringToExecute("_FORBIDENAREA ", true, false, false);
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnLight_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawLight03 frmDrawLight = new Atend.Design.frmDrawLight03();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmDrawLight) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.SendStringToExecute("_LIGHT ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void btnDb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawDB03 frmDrawDB = new Atend.Design.frmDrawDB03();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmDrawDB) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.SendStringToExecute("_DB ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnGround_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawGround02 frmDrawGround = new Atend.Design.frmDrawGround02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmDrawGround) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.SendStringToExecute("_Ground ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnMeasuredJackPanel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawMeasuredJackPanel02 frmDrawMeasuredJackPanel = new Atend.Design.frmDrawMeasuredJackPanel02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmDrawMeasuredJackPanel) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.SendStringToExecute("_MeasuredJackPanel ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnBreaker_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawBreaker02 FrmDrawBreaker = new Atend.Design.frmDrawBreaker02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDrawBreaker) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.SendStringToExecute("_BREAKER ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void btnStreetBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frm frmStreetBox = new Atend.Design.frm();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmStreetBox) == DialogResult.OK)
                {
                    //ed.WriteMessage("go for drw stre");
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_StreetBox ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnHeaderCable_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawHeaderCable02 frmHeaderCable = new Atend.Design.frmDrawHeaderCable02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmHeaderCable) == DialogResult.OK)
                {

                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_HeaderCable ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnCalamp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawCalamp02 frmCalamp = new Atend.Design.frmDrawCalamp02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmCalamp) == DialogResult.OK)
                {
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_Kalamp ", true, false, false);

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnKablsho_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawKablsho02 frmKablsho = new Atend.Design.frmDrawKablsho02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmKablsho) == DialogResult.OK)
                {
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_Kablsho ", true, false, false);

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        public static void ManagementRibbon()
        {

            RibbonButton btnDefineUser;
            btnDefineUser = new RibbonButton();
            btnDefineUser.Text = "تعریف کاربر";
            btnDefineUser.Id = "btnDefineUser";
            btnDefineUser.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnDefineUser.Size = RibbonItemSize.Standard;
            btnDefineUser.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\User32.png", UriKind.RelativeOrAbsolute));
            btnDefineUser.ShowText = true;
            btnDefineUser.ShowImage = true;
            btnDefineUser.IsEnabled = false;
            btnDefineUser.Click += new System.Windows.RoutedEventHandler(btnDefineUser_Click);

            //------------------------------------------------------------------------------

            RibbonButton btnProjectCode = new RibbonButton();
            btnProjectCode.Text = "تنظیم کد دستورکار";
            btnProjectCode.Id = "btnProjectCode";
            btnProjectCode.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnProjectCode.Size = RibbonItemSize.Standard;
            btnProjectCode.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\StatusCode.png", UriKind.RelativeOrAbsolute));
            btnProjectCode.ShowText = true;
            btnProjectCode.ShowImage = true;
            btnProjectCode.Click += new System.Windows.RoutedEventHandler(btnProjectCode_Click);

            RibbonButton btnIsExist = new RibbonButton();
            btnIsExist.Text = "تنظیم وضعیت تجهیز";
            btnIsExist.Id = "btnIsExist";
            btnIsExist.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnIsExist.Size = RibbonItemSize.Standard;
            btnIsExist.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\StatusProduct.png", UriKind.RelativeOrAbsolute));
            btnIsExist.ShowText = true;
            btnIsExist.ShowImage = true;
            btnIsExist.Click += new System.Windows.RoutedEventHandler(btnIsExist_Click);

            RibbonButton btnTransferBProduct = new RibbonButton();
            btnTransferBProduct.Text = "به روز رسانی لیست فهرست بها";
            btnTransferBProduct.Id = "btnTransferProduct";
            btnTransferBProduct.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnTransferBProduct.Size = RibbonItemSize.Standard;
            btnTransferBProduct.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Update.png", UriKind.RelativeOrAbsolute));
            btnTransferBProduct.ShowText = true;
            btnTransferBProduct.ShowImage = true;
            btnTransferBProduct.Click += new System.Windows.RoutedEventHandler(btnTransferBProduct_Click);


            RibbonButton btnShowPallete = new RibbonButton();
            btnShowPallete.Text = "نمایش پانل مدیریت تجهیزات";
            btnShowPallete.Id = "btnShowPallete";
            btnShowPallete.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnShowPallete.Size = RibbonItemSize.Standard;
            btnShowPallete.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Panel.png", UriKind.RelativeOrAbsolute));
            btnShowPallete.ShowText = true;
            btnShowPallete.ShowImage = true;
            btnShowPallete.Click += new System.Windows.RoutedEventHandler(btnShowPallete_Click);


            //---------------------------------------------------------------------

            RibbonButton btnPreparation = new RibbonButton();
            btnPreparation.Text = "آماده سازی جدید";
            btnPreparation.Id = "btnPreparation";
            btnPreparation.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnPreparation.Size = RibbonItemSize.Standard;
            btnPreparation.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Operation.png", UriKind.RelativeOrAbsolute));
            btnPreparation.ShowText = true;
            btnPreparation.ShowImage = true;
            btnPreparation.Click += new System.Windows.RoutedEventHandler(btnPreparation_Click);


            //----------------------------------------------------------------

            RibbonButton btnRemarkSetting;
            btnRemarkSetting = new RibbonButton();
            btnRemarkSetting.Text = "تعریف توضیحات";
            btnRemarkSetting.Id = "btnRemarkSetting";
            btnRemarkSetting.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnRemarkSetting.Size = RibbonItemSize.Standard;
            btnRemarkSetting.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Remark.png", UriKind.RelativeOrAbsolute));
            btnRemarkSetting.ShowText = true;
            btnRemarkSetting.ShowImage = true;
            btnRemarkSetting.IsEnabled = false;
            btnRemarkSetting.Click += new System.Windows.RoutedEventHandler(btnRemarkSetting_Click);



            //---------------------------------------------------------------------------

            RibbonRow RibbonRowUsers;
            RibbonRowUsers = new RibbonRow();
            RibbonRowUsers.Title = "RibbonRowData";
            RibbonRowUsers.Items.Add(btnDefineUser);

            RibbonRow RibbonRowEquips;
            RibbonRowEquips = new RibbonRow();
            RibbonRowEquips.Title = "RibbonRowEquips";
            RibbonRowEquips.Items.Add(btnProjectCode);
            RibbonRowEquips.Items.Add(btnIsExist);
            //RibbonRowEquips.Items.Add(btnTransferBProduct);
            RibbonRowEquips.Items.Add(btnShowPallete);


            RibbonRow RibbonRowPreparation;
            RibbonRowPreparation = new RibbonRow();
            RibbonRowPreparation.Title = "RibbonRowPreparation";
            RibbonRowPreparation.Items.Add(btnPreparation);


            RibbonRow RibbonRowOthar;
            RibbonRowOthar = new RibbonRow();
            RibbonRowOthar.Title = "RibbonRowPreparation";
            RibbonRowOthar.Items.Add(btnRemarkSetting);



            RibbonPanelSource RibbonPanelSourceUsers;
            RibbonPanelSourceUsers = new RibbonPanelSource();
            RibbonPanelSourceUsers.Title = "کاربران";
            RibbonPanelSourceUsers.Rows.Add(RibbonRowUsers);

            RibbonPanelSource RibbonPanelSourceEquips;
            RibbonPanelSourceEquips = new RibbonPanelSource();
            RibbonPanelSourceEquips.Title = "دستورکار";
            RibbonPanelSourceEquips.Rows.Add(RibbonRowEquips);

            RibbonPanelSource RibbonPanelSourcePreparation;
            RibbonPanelSourcePreparation = new RibbonPanelSource();
            RibbonPanelSourcePreparation.Title = "تجهیزات";
            RibbonPanelSourcePreparation.Rows.Add(RibbonRowPreparation);

            RibbonPanelSource RibbonPanelSourceOthar;
            RibbonPanelSourceOthar = new RibbonPanelSource();
            RibbonPanelSourceOthar.Title = "سایر";
            RibbonPanelSourceOthar.Rows.Add(RibbonRowOthar);




            RibbonPanel RibbonPanelUsers;
            RibbonPanelUsers = new RibbonPanel();
            RibbonPanelUsers.Source = RibbonPanelSourceUsers;

            RibbonPanel RibbonPanelEquips;
            RibbonPanelEquips = new RibbonPanel();
            RibbonPanelEquips.Source = RibbonPanelSourceEquips;

            RibbonPanel RibbonPanelPreparation;
            RibbonPanelPreparation = new RibbonPanel();
            RibbonPanelPreparation.Source = RibbonPanelSourcePreparation;

            RibbonPanel RibbonPanelOthar;
            RibbonPanelOthar = new RibbonPanel();
            RibbonPanelOthar.Source = RibbonPanelSourceOthar;



            RibbonTab rTab;
            rTab = new RibbonTab();
            rTab.Title = "مدیریت اطلاعات";
            rTab.Id = "ID_DataManagement";
            rTab.IsContextualTab = false;
            rTab.Panels.Add(RibbonPanelUsers);
            rTab.Panels.Add(RibbonPanelEquips);
            rTab.Panels.Add(RibbonPanelPreparation);
            rTab.Panels.Add(RibbonPanelOthar);

            RibbonControl rControl = Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl;
            rControl.Tabs.Add(rTab);
            //rControl.ActiveTab = rTab;

        }

        static void btnShowPallete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //SHOWPALETTE

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {

                foreach (System.Data.DataRow dr in Atend.Control.Common.AccessList.Rows)
                {
                    switch (Convert.ToInt32(dr["IdAccess"]))
                    {
                        case 10:
                            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                            doc.SendStringToExecute("_SHOWPALETTE ", true, false, false);
                            break;
                    }
                }

            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }



        }

        static void btnPreparation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Atend.Equipment.frmOperation02 pre = new Atend.Equipment.frmOperation02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(pre);
        }

        public static void SettingRibbon()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("i am in SettingRibbon : FP:{0} \n", Atend.Control.Common.fullPath);

            RibbonButton btnFile;
            btnFile = new RibbonButton();
            btnFile.Text = "تنظیم قفل نرم افزار";
            btnFile.Id = "btnFile";
            btnFile.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnFile.Size = RibbonItemSize.Standard;
            btnFile.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\UsbLock.png", UriKind.RelativeOrAbsolute));
            btnFile.ShowText = true;
            btnFile.ShowImage = true;
            btnFile.Click += new System.Windows.RoutedEventHandler(btnFile_Click);
            //ed.WriteMessage("1\n");

            RibbonButton btnLocal;
            btnLocal = new RibbonButton();
            btnLocal.Text = "تنظیم ارتباط با پایگاه داده محلی";
            btnLocal.Id = "btnLocal";
            btnLocal.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnLocal.Size = RibbonItemSize.Standard;
            btnLocal.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\ReportSetting32.png", UriKind.RelativeOrAbsolute));
            btnLocal.ShowText = true;
            btnLocal.ShowImage = true;
            btnLocal.Click += new System.Windows.RoutedEventHandler(btnLocal_Click);
            //ed.WriteMessage("11\n");

            RibbonButton btnServer;
            btnServer = new RibbonButton();
            btnServer.Text = "تنظیم ارتباط با پایگاه داده سرور";
            btnServer.Id = "btnServerSetting";
            btnServer.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnServer.Size = RibbonItemSize.Standard;
            btnServer.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\ServerSetting.png", UriKind.RelativeOrAbsolute));
            btnServer.ShowText = true;
            btnServer.ShowImage = true;
            btnServer.Click += new System.Windows.RoutedEventHandler(btnServer_Click);
            //ed.WriteMessage("12\n");

            RibbonButton btnLogin;
            btnLogin = new RibbonButton();
            btnLogin.Text = "ورود به نرم افزار";
            btnLogin.Id = "btnLogin";
            btnLogin.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnLogin.Size = RibbonItemSize.Standard;
            btnLogin.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Login.png", UriKind.RelativeOrAbsolute));
            btnLogin.ShowText = true;
            btnLogin.ShowImage = true;
            btnLogin.Click += new System.Windows.RoutedEventHandler(btnLogin_Click);
            //ed.WriteMessage("13\n");

            RibbonButton btnGetUserAccess;
            btnGetUserAccess = new RibbonButton();
            btnGetUserAccess.Text = "دریافت اطلاعات افراد از سرور";
            btnGetUserAccess.Id = "btnGetUserAccess";
            btnGetUserAccess.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnGetUserAccess.Size = RibbonItemSize.Standard;
            btnGetUserAccess.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Equip32.png", UriKind.RelativeOrAbsolute));
            btnGetUserAccess.ShowText = true;
            btnGetUserAccess.ShowImage = true;
            btnGetUserAccess.Click +=new System.Windows.RoutedEventHandler(btnGetUserAccess_Click);
            //ed.WriteMessage("14\n");

            RibbonButton btnShowAccess;
            btnShowAccess = new RibbonButton();
            btnShowAccess.Text = "نمایش سطح دسترسی";
            btnShowAccess.Id = "btnShowAccess";
            btnShowAccess.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnShowAccess.Size = RibbonItemSize.Standard;
            btnShowAccess.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Security.png", UriKind.RelativeOrAbsolute));
            btnShowAccess.ShowText = true;
            btnShowAccess.ShowImage = true;
            btnShowAccess.IsEnabled = false;
            btnShowAccess.Click += new System.Windows.RoutedEventHandler(btnShowAccess_Click);
            //ed.WriteMessage("15\n");

            RibbonButton btnBSetting;
            btnBSetting = new RibbonButton();
            btnBSetting.Text = "تنظیم نرم افزار";
            btnBSetting.Id = "btnBSetting";
            btnBSetting.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnBSetting.Size = RibbonItemSize.Standard;
            btnBSetting.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Setting.png", UriKind.RelativeOrAbsolute));
            btnBSetting.ShowText = true;
            btnBSetting.ShowImage = true;
            btnBSetting.IsEnabled = false;
            btnBSetting.Click += new System.Windows.RoutedEventHandler(btnBSetting_Click);
            //ed.WriteMessage("16\n");

            RibbonRow RibbonRowData;
            RibbonRowData = new RibbonRow();
            RibbonRowData.Title = "RibbonRowData";
            RibbonRowData.Items.Add(btnFile);
            RibbonRowData.Items.Add(btnLocal);
            RibbonRowData.Items.Add(btnServer);
            RibbonRowData.Items.Add(btnBSetting);
            //ed.WriteMessage("17\n");

            RibbonRow RibbonRowUser;
            RibbonRowUser = new RibbonRow();
            RibbonRowUser.Title = "Row2";
            RibbonRowUser.Items.Add(btnLogin);
            RibbonRowUser.Items.Add(btnGetUserAccess);
            RibbonRowUser.Items.Add(btnShowAccess);
            //ed.WriteMessage("18\n");

            RibbonPanelSource RibbonPanelSourceData;
            RibbonPanelSourceData = new RibbonPanelSource();
            RibbonPanelSourceData.Title = "تنظیمات پایگاه داده";
            RibbonPanelSourceData.Rows.Add(RibbonRowData);
            //ed.WriteMessage("19\n");

            RibbonPanelSource RibbonPanelSourceUser;
            RibbonPanelSourceUser = new RibbonPanelSource();
            RibbonPanelSourceUser.Title = "کاربر";
            RibbonPanelSourceUser.Rows.Add(RibbonRowUser);
            //ed.WriteMessage("20\n");

            RibbonPanel RibbonPanelData;
            RibbonPanelData = new RibbonPanel();
            RibbonPanelData.Source = RibbonPanelSourceData;
            //ed.WriteMessage("21\n");

            RibbonPanel RibbonPanelUser;
            RibbonPanelUser = new RibbonPanel();
            RibbonPanelUser.Source = RibbonPanelSourceUser;
            //ed.WriteMessage("22\n");

            RibbonTab rTab;
            rTab = new RibbonTab();
            rTab.Title = "تنظیمات";
            rTab.Id = "ID_Setting";
            rTab.IsContextualTab = false;
            rTab.Panels.Add(RibbonPanelData);
            rTab.Panels.Add(RibbonPanelUser);
            //ed.WriteMessage("23\n");

            RibbonControl rControl = Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl;
            rControl.Tabs.Add(rTab);
            rControl.ActiveTab = rTab;
            //ed.WriteMessage("24\n");

            //ed.WriteMessage("i am going out SettingRibbon : FP:{0} \n", Atend.Control.Common.fullPath);

        }

        static void btnRemarkSetting_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {

                Atend.UControls.Design.frmWordRemarks remark = new Atend.UControls.Design.frmWordRemarks();
                remark.FullPath = Atend.Control.Common.fullPath;
                remark.DesignFullAddress = Atend.Control.Common.DesignFullAddress;
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(remark) == DialogResult.OK)
                {
                    //Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    //doc.SendStringToExecute("_StreetBox ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void btnFile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            //{

            Atend.Design.frmFile _frmFile = new Atend.Design.frmFile();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmFile);
            //}
            //else
            //{
            //    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
            //    notification.Title = "انتخاب طرح";
            //    notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
            //    notification.ShowStatusBarBalloon();
            //    return;
            //}
        }

        public static void MechanicalCalculationRibbon()
        {

            RibbonButton rbutton = new RibbonButton();
            rbutton.Text = "روش UTS%";
            rbutton.Id = "btnSagTension";
            rbutton.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbutton.Size = RibbonItemSize.Standard;
            rbutton.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\UtsPercent32.png", UriKind.RelativeOrAbsolute));
            rbutton.ShowText = true;
            rbutton.ShowImage = true;
            rbutton.Click += new System.Windows.RoutedEventHandler(rbutton_Click);

            //RibbonButton rbuttonTest = new RibbonButton();
            //rbuttonTest.Text = "روش شماره 1UTS%Test";
            //rbuttonTest.Id = "btnSagTensionTest";
            //rbuttonTest.Orientation = System.Windows.Controls.Orientation.Vertical;
            //rbuttonTest.Size = RibbonItemSize.Standard;
            //rbuttonTest.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\UtsPercent32.png", UriKind.RelativeOrAbsolute));
            //rbuttonTest.ShowText = true;
            //rbuttonTest.ShowImage = true;
            //rbuttonTest.Click += new System.Windows.RoutedEventHandler(rbuttonTest_Click);


            RibbonButton rbutton1 = new RibbonButton();
            rbutton1.Text = "محل عبور شبکه";
            rbutton1.Id = "btnNetWorkCross";
            rbutton1.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbutton1.Size = RibbonItemSize.Standard;
            rbutton1.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\NetCross32.png", UriKind.RelativeOrAbsolute));
            rbutton1.ShowText = true;
            rbutton1.ShowImage = true;
            rbutton1.Click += new System.Windows.RoutedEventHandler(rbutton1_Click);



            RibbonButton rbuttonMaxF = new RibbonButton();
            rbuttonMaxF.Text = "روش MaxF";
            rbuttonMaxF.Id = "btnMaxF";
            rbuttonMaxF.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonMaxF.Size = RibbonItemSize.Standard;
            rbuttonMaxF.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\MaxF32.png", UriKind.RelativeOrAbsolute));
            rbuttonMaxF.ShowText = true;
            rbuttonMaxF.ShowImage = true;
            rbuttonMaxF.Click += new System.Windows.RoutedEventHandler(rbuttonMaxF_Click);


            RibbonButton rbuttonRudSurface = new RibbonButton();
            rbuttonRudSurface.Text = "محاسبه سطوح نا هموار";
            rbuttonRudSurface.Id = "btnRudSurface";
            rbuttonRudSurface.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonRudSurface.Size = RibbonItemSize.Standard;
            rbuttonRudSurface.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\5.png", UriKind.RelativeOrAbsolute));
            rbuttonRudSurface.ShowText = true;
            rbuttonRudSurface.ShowImage = true;
            rbuttonRudSurface.Click += new System.Windows.RoutedEventHandler(rbuttonRudSurface_Click);






            RibbonButton rbuttonSetDefault = new RibbonButton();
            rbuttonSetDefault.Text = "تعیین پیش فرض";
            rbuttonSetDefault.Id = "btnSetDefault";
            rbuttonSetDefault.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonSetDefault.Size = RibbonItemSize.Standard;
            rbuttonSetDefault.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\DefaultMec.png", UriKind.RelativeOrAbsolute));
            rbuttonSetDefault.ShowText = true;
            rbuttonSetDefault.ShowImage = true;
            rbuttonSetDefault.Click += new System.Windows.RoutedEventHandler(rbuttonSetDefault_Click);


            RibbonButton rbuttonDefine = new RibbonButton();
            rbuttonDefine.Text = "تعریف بار انشعابی";
            rbuttonDefine.Id = "btnDefine";
            rbuttonDefine.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonDefine.Size = RibbonItemSize.Standard;
            rbuttonDefine.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\DloadF23.png", UriKind.RelativeOrAbsolute));
            rbuttonDefine.ShowText = true;
            rbuttonDefine.ShowImage = true;
            rbuttonDefine.Click += new System.Windows.RoutedEventHandler(rbuttonDefine_Click);

            RibbonButton rbuttonLoad = new RibbonButton();
            rbuttonLoad.Text = "تخصیص بار";
            rbuttonLoad.Id = "btnLoad";
            rbuttonLoad.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonLoad.Size = RibbonItemSize.Standard;
            rbuttonLoad.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\DLoad32.png", UriKind.RelativeOrAbsolute));
            rbuttonLoad.ShowText = true;
            rbuttonLoad.ShowImage = true;
            rbuttonLoad.Click += new System.Windows.RoutedEventHandler(rbuttonLoad_Click);


            RibbonButton rbuttonElectrical = new RibbonButton();
            rbuttonElectrical.Text = "پخش بار";
            rbuttonElectrical.Id = "btnElectrical";
            rbuttonElectrical.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonElectrical.Size = RibbonItemSize.Standard;
            rbuttonElectrical.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\LoadDis32.png", UriKind.RelativeOrAbsolute));
            rbuttonElectrical.ShowText = true;
            rbuttonElectrical.ShowImage = true;
            rbuttonElectrical.Click += new System.Windows.RoutedEventHandler(rbuttonElectrical_Click);


            RibbonButton rbuttonCalcCrossSectionArea = new RibbonButton();
            rbuttonCalcCrossSectionArea.Text = "محاسبه سطح مقطع";
            rbuttonCalcCrossSectionArea.Id = "btnCalcCrossSectionArea";
            rbuttonCalcCrossSectionArea.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonCalcCrossSectionArea.Size = RibbonItemSize.Standard;
            rbuttonCalcCrossSectionArea.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\CrossSection.png", UriKind.RelativeOrAbsolute));
            rbuttonCalcCrossSectionArea.ShowText = true;
            rbuttonCalcCrossSectionArea.ShowImage = true;
            rbuttonCalcCrossSectionArea.Click += new System.Windows.RoutedEventHandler(rbuttonCalcCrossSectionArea_Click);

            RibbonButton rbuttonCalcShortCircuit = new RibbonButton();
            rbuttonCalcShortCircuit.Text = "محاسبه اتصال کوتاه";
            rbuttonCalcShortCircuit.Id = "btnCalcShortCircuit";
            rbuttonCalcShortCircuit.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonCalcShortCircuit.Size = RibbonItemSize.Standard;
            rbuttonCalcShortCircuit.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\MaxCurrent.png", UriKind.RelativeOrAbsolute));
            rbuttonCalcShortCircuit.ShowText = true;
            rbuttonCalcShortCircuit.ShowImage = true;
            rbuttonCalcShortCircuit.Click += new System.Windows.RoutedEventHandler(rbuttonCalcShortCircuit_Click);


            RibbonButton rbuttonCalcTransCapacity = new RibbonButton();
            rbuttonCalcTransCapacity.Text = "محاسبه تعیین ظرفیت ترانس";
            rbuttonCalcTransCapacity.Id = "btnCalcTransCapacity";
            rbuttonCalcTransCapacity.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonCalcTransCapacity.Size = RibbonItemSize.Standard;
            rbuttonCalcTransCapacity.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\TransCapacity.png", UriKind.RelativeOrAbsolute));
            rbuttonCalcTransCapacity.ShowText = true;
            rbuttonCalcTransCapacity.ShowImage = true;
            rbuttonCalcTransCapacity.Click += new System.Windows.RoutedEventHandler(rbuttonCalcTransCapacity_Click);

            RibbonButton rbuttonreport = new RibbonButton();
            rbuttonreport.Text = "گزارش به روش UTS";
            rbuttonreport.Id = "btnReportUTS";
            rbuttonreport.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonreport.Size = RibbonItemSize.Standard;
            rbuttonreport.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\UtsPercent32.png", UriKind.RelativeOrAbsolute));
            rbuttonreport.ShowText = true;
            rbuttonreport.ShowImage = true;
            rbuttonreport.Click += new System.Windows.RoutedEventHandler(rbuttonreport_Click);




            RibbonButton rbuttonreportElec = new RibbonButton();
            rbuttonreportElec.Text = "گزارش الکتریکی";
            rbuttonreportElec.Id = "btnReportElec";
            rbuttonreportElec.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonreportElec.Size = RibbonItemSize.Standard;
            rbuttonreportElec.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\UtsPercent32.png", UriKind.RelativeOrAbsolute));
            rbuttonreportElec.ShowText = true;
            rbuttonreportElec.ShowImage = true;
            rbuttonreportElec.Click += new System.Windows.RoutedEventHandler(rbuttonreportElec_Click);


            RibbonRow rbEquipment = new RibbonRow();
            rbEquipment.Title = "محاسبات مکانیکی";
            rbEquipment.Items.Add(rbutton);
            rbEquipment.Items.Add(rbuttonMaxF);
            rbEquipment.Items.Add(rbuttonRudSurface);
            rbEquipment.Items.Add(rbutton1);
            rbEquipment.Items.Add(rbuttonSetDefault);
            //rbEquipment.Items.Add(btnAutoPoleInstallation);

            //rbEquipment.Items.Add(rbuttonTest);

            RibbonRow rbElectrical = new RibbonRow();
            rbElectrical.Title = "محاسبات الکتریکی";
            rbElectrical.Items.Add(rbuttonDefine);
            rbElectrical.Items.Add(rbuttonLoad);
            rbElectrical.Items.Add(rbuttonElectrical);
            rbElectrical.Items.Add(rbuttonCalcCrossSectionArea);
            rbElectrical.Items.Add(rbuttonCalcShortCircuit);
            rbElectrical.Items.Add(rbuttonCalcTransCapacity);

            RibbonRow rbReport = new RibbonRow();
            rbReport.Title = "گزارشات";
            rbReport.Items.Add(rbuttonreport);
            rbReport.Items.Add(rbuttonreportElec);

            RibbonPanelSource rpsMechanical = new RibbonPanelSource();
            rpsMechanical.Title = "محاسبات مکانیکی";
            rpsMechanical.Rows.Add(rbEquipment);

            RibbonPanelSource rpsElectrical = new RibbonPanelSource();
            rpsElectrical.Title = "محاسبات الکتریکی";
            rpsElectrical.Rows.Add(rbElectrical);

            RibbonPanelSource rpsReport = new RibbonPanelSource();
            rpsReport.Title = "گزارشات";
            rpsReport.Rows.Add(rbReport);

            RibbonPanel rPanel = new RibbonPanel();
            rPanel.Source = rpsMechanical;

            RibbonPanel rPanel1 = new RibbonPanel();
            rPanel1.Source = rpsElectrical;

            RibbonPanel rPanel2 = new RibbonPanel();
            rPanel2.Source = rpsReport;

            RibbonTab rTab = new RibbonTab();
            rTab.Title = "محاسبات";
            rTab.Id = "ID_Calculation";
            rTab.IsContextualTab = false;
            rTab.Panels.Add(rPanel);
            rTab.Panels.Add(rPanel1);
            //rTab.Panels.Add(rPanel2);

            RibbonControl rControl = Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl;
            rControl.Tabs.Add(rTab);
            //rControl.ActiveTab = rTab;

        }

        static void btnAutoPoleInstallation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmAutoPoleInstallation _frmAutoPoleInstallation = new Atend.Calculating.frmAutoPoleInstallation();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmAutoPoleInstallation) == DialogResult.OK)
                { }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        public static void ReportingRibbon()
        {
            RibbonButton rbuttonreport = new RibbonButton();
            rbuttonreport.Text = "گزارش مکانیکی";
            rbuttonreport.Id = "btnReportUTS";
            rbuttonreport.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonreport.Size = RibbonItemSize.Standard;
            rbuttonreport.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\CrystalMec.png", UriKind.RelativeOrAbsolute));
            rbuttonreport.ShowText = true;
            rbuttonreport.ShowImage = true;
            rbuttonreport.Click += new System.Windows.RoutedEventHandler(rbuttonreport_Click);

            RibbonButton rbuttonreportElec = new RibbonButton();
            rbuttonreportElec.Text = "گزارش الکتریکی";
            rbuttonreportElec.Id = "btnReportElec";
            rbuttonreportElec.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonreportElec.Size = RibbonItemSize.Standard;
            rbuttonreportElec.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\CrystalElec.png", UriKind.RelativeOrAbsolute));
            rbuttonreportElec.ShowText = true;
            rbuttonreportElec.ShowImage = true;
            rbuttonreportElec.Click += new System.Windows.RoutedEventHandler(rbuttonreportElec_Click);

            RibbonButton rbuttonreportGIS = new RibbonButton();
            rbuttonreportGIS.Text = "GIS گزارش";
            rbuttonreportGIS.Id = "btnReportGIS";
            rbuttonreportGIS.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonreportGIS.Size = RibbonItemSize.Standard;
            rbuttonreportGIS.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Equip32.png", UriKind.RelativeOrAbsolute));
            rbuttonreportGIS.ShowText = true;
            rbuttonreportGIS.ShowImage = true;
            rbuttonreportGIS.Click += new System.Windows.RoutedEventHandler(rbuttonreportGIS_Click);

            RibbonButton rbuttonreportEXcelUTS = new RibbonButton();
            rbuttonreportEXcelUTS.Text = "گزارش مکانیکی  UTS";
            rbuttonreportEXcelUTS.Id = "btnReportUTSExcel";
            rbuttonreportEXcelUTS.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonreportEXcelUTS.Size = RibbonItemSize.Standard;
            rbuttonreportEXcelUTS.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\ExcelMec.png", UriKind.RelativeOrAbsolute));
            rbuttonreportEXcelUTS.ShowText = true;
            rbuttonreportEXcelUTS.ShowImage = true;
            rbuttonreportEXcelUTS.Click += new System.Windows.RoutedEventHandler(rbuttonreportEXcelUTS_Click);

            RibbonButton rbuttonreportEXcelMAXF = new RibbonButton();
            rbuttonreportEXcelMAXF.Text = "گزارش مکانیکی  MaxF";
            rbuttonreportEXcelMAXF.Id = "btnReportUTSExcel";
            rbuttonreportEXcelMAXF.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonreportEXcelMAXF.Size = RibbonItemSize.Standard;
            rbuttonreportEXcelMAXF.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\ExcelMec.png", UriKind.RelativeOrAbsolute));
            rbuttonreportEXcelMAXF.ShowText = true;
            rbuttonreportEXcelMAXF.ShowImage = true;
            rbuttonreportEXcelMAXF.Click += new System.Windows.RoutedEventHandler(rbuttonreportEXcelMAXF_Click);

            RibbonButton rbuttonreportElecExcel = new RibbonButton();
            rbuttonreportElecExcel.Text = "گزارش الکتریکی";
            rbuttonreportElecExcel.Id = "btnReportElec";
            rbuttonreportElecExcel.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonreportElecExcel.Size = RibbonItemSize.Standard;
            rbuttonreportElecExcel.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\ExcelElec.png", UriKind.RelativeOrAbsolute));
            rbuttonreportElecExcel.ShowText = true;
            rbuttonreportElecExcel.ShowImage = true;
            rbuttonreportElecExcel.Click += new System.Windows.RoutedEventHandler(rbuttonreportElecExcel_Click);

            RibbonButton rbuttonreportGISExcel = new RibbonButton();
            rbuttonreportGISExcel.Text = "GIS گزارش";
            rbuttonreportGISExcel.Id = "btnReportGISExcel";
            rbuttonreportGISExcel.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbuttonreportGISExcel.Size = RibbonItemSize.Standard;
            rbuttonreportGISExcel.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Equip32.png", UriKind.RelativeOrAbsolute));
            rbuttonreportGISExcel.ShowText = true;
            rbuttonreportGISExcel.ShowImage = true;
            rbuttonreportGISExcel.Click += new System.Windows.RoutedEventHandler(rbuttonreportGISExcel_Click);

            //RibbonButton rbuttonremark = new RibbonButton();
            //rbuttonremark.Text = "توضیحات";
            //rbuttonremark.Id = "btnRemark";
            //rbuttonremark.Orientation = System.Windows.Controls.Orientation.Vertical;
            //rbuttonremark.Size = RibbonItemSize.Standard;
            //rbuttonremark.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\RemarkReport.png", UriKind.RelativeOrAbsolute));
            //rbuttonremark.ShowText = true;
            //rbuttonremark.ShowImage = true;
            //rbuttonremark.Click += new System.Windows.RoutedEventHandler(rbuttonremark_Click);

            RibbonButton btnStatusReport01 = new RibbonButton();
            btnStatusReport01.Text = "صدور دستور کار";
            btnStatusReport01.Id = "btnStatusReport";
            btnStatusReport01.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnStatusReport01.Size = RibbonItemSize.Standard;
            btnStatusReport01.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\StatuseReport1.png", UriKind.RelativeOrAbsolute));
            btnStatusReport01.ShowText = true;
            btnStatusReport01.ShowImage = true;
            btnStatusReport01.Click += new System.Windows.RoutedEventHandler(btnStatusReport_Click);

            RibbonButton btnBook = new RibbonButton();
            btnBook.Text = "تولید دفترچه";
            btnBook.Id = "btnBook";
            btnBook.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnBook.Size = RibbonItemSize.Standard;
            btnBook.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\PDF.png", UriKind.RelativeOrAbsolute));
            btnBook.ShowText = true;
            btnBook.ShowImage = true;
            btnBook.Click += new System.Windows.RoutedEventHandler(btnBook_Click);

            RibbonButton btnViewReport = new RibbonButton();
            btnViewReport.Text = "مشاهده گزارشات";
            btnViewReport.Id = "btnViewReport";
            btnViewReport.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnViewReport.Size = RibbonItemSize.Standard;
            btnViewReport.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\ViewReport.png", UriKind.RelativeOrAbsolute));
            btnViewReport.ShowText = true;
            btnViewReport.ShowImage = true;
            btnViewReport.Click += new System.Windows.RoutedEventHandler(btnViewReport_Click);

            //RibbonRow rbEquipment = new RibbonRow();
            //rbEquipment.Title = "محاسبات مکانیکی";
            //rbEquipment.Items.Add(rbutton);
            //rbEquipment.Items.Add(rbuttonMaxF);
            //rbEquipment.Items.Add(rbutton1);
            //rbEquipment.Items.Add(rbuttonSetDefault);
            //rbEquipment.Items.Add(rbuttonTest);

            RibbonRow rbReport = new RibbonRow();
            rbReport.Title = "ATEND ";
            rbReport.Items.Add(rbuttonreport);
            rbReport.Items.Add(rbuttonreportElec);
            rbReport.Items.Add(rbuttonreportGIS);
            //rbReport.Items.Add(rbuttonremark);


            RibbonRow rbReportEXCEl = new RibbonRow();
            rbReportEXCEl.Title = "EXCEL";
            rbReportEXCEl.Items.Add(rbuttonreportEXcelUTS);
            rbReportEXCEl.Items.Add(rbuttonreportEXcelMAXF);
            rbReportEXCEl.Items.Add(rbuttonreportElecExcel);
            rbReportEXCEl.Items.Add(rbuttonreportGISExcel);
            rbReportEXCEl.Items.Add(btnStatusReport01);



            RibbonRow rbBook = new RibbonRow();
            rbBook.Title = "دفترچه";
            rbBook.Items.Add(btnBook);
            rbBook.Items.Add(btnViewReport);

            RibbonPanelSource rpsReport = new RibbonPanelSource();
            rpsReport.Title = "ATEND";
            rpsReport.Rows.Add(rbReport);

            RibbonPanelSource rpsReportEXCEL = new RibbonPanelSource();
            rpsReportEXCEL.Title = " EXCEL";
            rpsReportEXCEL.Rows.Add(rbReportEXCEl);

            RibbonPanelSource rpsBook = new RibbonPanelSource();
            rpsBook.Title = "دفترچه طرح";
            rpsBook.Rows.Add(rbBook);


            RibbonPanel rPanel2 = new RibbonPanel();
            rPanel2.Source = rpsReport;


            RibbonPanel rPanel3 = new RibbonPanel();
            rPanel3.Source = rpsReportEXCEL;

            RibbonPanel rPanel4 = new RibbonPanel();
            rPanel4.Source = rpsBook;


            RibbonTab rTab = new RibbonTab();
            rTab.Title = "گزارشات";
            rTab.Id = "ID_Reporting";
            rTab.IsContextualTab = false;
            rTab.Panels.Add(rPanel2);
            rTab.Panels.Add(rPanel3);
            rTab.Panels.Add(rPanel4);

            RibbonControl rControl = Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl;
            rControl.Tabs.Add(rTab);
            //rControl.ActiveTab = rTab;

        }

        static void btnBook_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Report.frmSelectReports FrmSel = new Atend.Report.frmSelectReports();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmSel) == DialogResult.OK)
                { }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        public static void DesignRibbon()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("fulladdress:{0} \n", Atend.Control.Common.fullPath);
            RibbonButton btnNewDesign;
            btnNewDesign = new RibbonButton();
            btnNewDesign.Text = "طرح جدید";
            btnNewDesign.Id = "btnNewDesign";
            btnNewDesign.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnNewDesign.Size = RibbonItemSize.Standard;
            btnNewDesign.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\New1.png", UriKind.RelativeOrAbsolute));
            btnNewDesign.ShowText = true;
            btnNewDesign.ShowImage = true;
            btnNewDesign.Click += new System.Windows.RoutedEventHandler(btnNewDesign_Click);

            RibbonButton btnOpenDesign;
            btnOpenDesign = new RibbonButton();
            btnOpenDesign.Text = "بازکردن طرح";
            btnOpenDesign.Id = "btnOpenDesign";
            btnOpenDesign.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnOpenDesign.Size = RibbonItemSize.Standard;
            btnOpenDesign.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Open1.png", UriKind.RelativeOrAbsolute));
            btnOpenDesign.ShowText = true;
            btnOpenDesign.ShowImage = true;
            btnOpenDesign.Click += new System.Windows.RoutedEventHandler(btnOpenDesign_Click);


            RibbonButton btnSaveDesign = new RibbonButton();
            btnSaveDesign.Text = "ذخیره سازی طرح";
            btnSaveDesign.Id = "btnSaveDesign";
            btnSaveDesign.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnSaveDesign.Size = RibbonItemSize.Standard;
            btnSaveDesign.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Save32.png", UriKind.RelativeOrAbsolute));
            btnSaveDesign.ShowText = true;
            btnSaveDesign.ShowImage = true;
            //btnSaveDesign.IsEnabled = false;
            btnSaveDesign.Click += new System.Windows.RoutedEventHandler(btnSaveDesign_Click);

            RibbonButton btnSaveasDesign = new RibbonButton();
            btnSaveasDesign.Text = "ذخیره سازی طرح در";
            btnSaveasDesign.Id = "btnSaveasDesign";
            btnSaveasDesign.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnSaveasDesign.Size = RibbonItemSize.Standard;
            btnSaveasDesign.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\filesaveas1.png", UriKind.RelativeOrAbsolute));
            btnSaveasDesign.ShowText = true;
            btnSaveasDesign.ShowImage = true;
            btnSaveasDesign.Click += new System.Windows.RoutedEventHandler(btnSaveAsDesign_Click);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            RibbonButton btnSelectDesign;
            btnSelectDesign = new RibbonButton();
            btnSelectDesign.Text = " انتخاب طرح از سرور";
            btnSelectDesign.Id = "btnSelectDesign";
            btnSelectDesign.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnSelectDesign.Size = RibbonItemSize.Standard;
            btnSelectDesign.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\OpenServer.png", UriKind.RelativeOrAbsolute));
            btnSelectDesign.ShowText = true;
            btnSelectDesign.ShowImage = true;
            btnSelectDesign.Click += new System.Windows.RoutedEventHandler(btnSelectDesign_Click);

            RibbonButton btnSaveDesignOnServer = new RibbonButton();
            btnSaveDesignOnServer.Text = "ذخیره سازی طرح روی سرور";
            btnSaveDesignOnServer.Id = "btnSaveDesignOnServer";
            btnSaveDesignOnServer.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnSaveDesignOnServer.Size = RibbonItemSize.Standard;
            btnSaveDesignOnServer.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\SaveServer.png", UriKind.RelativeOrAbsolute));
            btnSaveDesignOnServer.ShowText = true;
            btnSaveDesignOnServer.ShowImage = true;
            btnSaveDesignOnServer.Click += new System.Windows.RoutedEventHandler(btnSaveDesignOnServer_Click);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


            RibbonButton rbutton2 = new RibbonButton();
            rbutton2.Text = "شرایط آب و هوایی";
            rbutton2.Id = "btnWeatherCondition";
            rbutton2.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbutton2.Size = RibbonItemSize.Standard;
            rbutton2.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\weather32.png", UriKind.RelativeOrAbsolute));
            rbutton2.ShowText = true;
            rbutton2.ShowImage = true;
            rbutton2.Click += new System.Windows.RoutedEventHandler(btnWeatherCondition_Click);



            RibbonButton rbutton5 = new RibbonButton();
            rbutton5.Text = "مدیریت مقیاس هر تجهیز";
            rbutton5.Id = "btnManageLayer";
            rbutton5.Orientation = System.Windows.Controls.Orientation.Vertical;
            rbutton5.Size = RibbonItemSize.Standard;
            rbutton5.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Scale32.png", UriKind.RelativeOrAbsolute));
            rbutton5.ShowText = true;
            rbutton5.ShowImage = true;
            rbutton5.Click += new System.Windows.RoutedEventHandler(btnManageLayer_Click);

            RibbonButton btnDesignProfile = new RibbonButton();
            btnDesignProfile.Text = "مشخصات طرح";
            btnDesignProfile.Id = "btnDesignProfile";
            btnDesignProfile.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnDesignProfile.Size = RibbonItemSize.Standard;
            btnDesignProfile.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Profile1.png", UriKind.RelativeOrAbsolute));
            btnDesignProfile.ShowText = true;
            btnDesignProfile.ShowImage = true;
            btnDesignProfile.Click += new System.Windows.RoutedEventHandler(btnDesignProfile_Click);

            //RibbonButton btnTransferBProduct = new RibbonButton();
            //btnTransferBProduct.Text = "به روز رسانی لیست فهرست بها";
            //btnTransferBProduct.Id = "btnTransferProduct";
            //btnTransferBProduct.Orientation = System.Windows.Controls.Orientation.Vertical;
            //btnTransferBProduct.Size = RibbonItemSize.Standard;
            //btnTransferBProduct.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Update.png", UriKind.RelativeOrAbsolute));
            //btnTransferBProduct.ShowText = true;
            //btnTransferBProduct.ShowImage = true;
            //btnTransferBProduct.Click += new System.Windows.RoutedEventHandler(btnTransferBProduct_Click);

            RibbonButton btnGetFromServer = new RibbonButton();
            btnGetFromServer.Text = "دریافت اطلاعات از سرور";
            btnGetFromServer.Id = "btnGetFromServer";
            btnGetFromServer.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnGetFromServer.Size = RibbonItemSize.Standard;
            btnGetFromServer.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\RecieveData.png", UriKind.RelativeOrAbsolute));
            btnGetFromServer.ShowText = true;
            btnGetFromServer.ShowImage = true;
            btnGetFromServer.IsVisible = true;
            btnGetFromServer.Click += new System.Windows.RoutedEventHandler(btnGetFromServer_Click);


            RibbonButton btnStatusReport = new RibbonButton();
            btnStatusReport.Text = "صدور دستور کار";
            btnStatusReport.Id = "btnStatusReport";
            btnStatusReport.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnStatusReport.Size = RibbonItemSize.Standard;
            btnStatusReport.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\StatuseReport.png", UriKind.RelativeOrAbsolute));
            btnStatusReport.ShowText = true;
            btnStatusReport.ShowImage = true;
            btnStatusReport.Click += new System.Windows.RoutedEventHandler(btnStatusReport_Click);

            RibbonButton btnPlan = new RibbonButton();
            btnPlan.Text = "طرح نهایی";
            btnPlan.Id = "btnPlan";
            btnPlan.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnPlan.Size = RibbonItemSize.Standard;
            btnPlan.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\FinalDesign.png", UriKind.RelativeOrAbsolute));
            btnPlan.ShowText = true;
            btnPlan.ShowImage = true;
            btnPlan.Click += new System.Windows.RoutedEventHandler(btnPlan_Click02);


            RibbonButton btnLoadBack = new RibbonButton();
            btnLoadBack.Text = "بارگذاری زمینه";
            btnLoadBack.Id = "btnLoadBack";
            btnLoadBack.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnLoadBack.Size = RibbonItemSize.Standard;
            btnLoadBack.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\BackGround.png", UriKind.RelativeOrAbsolute));
            btnLoadBack.ShowText = true;
            btnLoadBack.ShowImage = true;
            btnLoadBack.Click += new System.Windows.RoutedEventHandler(btnLoadBack_Click);

            RibbonButton btnRemark = new RibbonButton();
            btnRemark.Text = "توضیحات";
            btnRemark.Id = "btnRemark";
            btnRemark.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnRemark.Size = RibbonItemSize.Standard;
            btnRemark.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Remark1.png", UriKind.RelativeOrAbsolute));
            btnRemark.ShowText = true;
            btnRemark.ShowImage = true;
            btnRemark.Click += new System.Windows.RoutedEventHandler(btnRemark_Click);

            RibbonButton btnFrame = new RibbonButton();
            btnFrame.Text = "کادر";
            btnFrame.Id = "btnFrame";
            btnFrame.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnFrame.Size = RibbonItemSize.Standard;
            btnFrame.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Frame.png", UriKind.RelativeOrAbsolute));
            btnFrame.ShowText = true;
            btnFrame.ShowImage = true;
            btnFrame.Click += new System.Windows.RoutedEventHandler(btnFrame_Click);

            RibbonButton btnShowDescription = new RibbonButton();
            btnShowDescription.Text = "نمایش مشخصات اضافی";
            btnShowDescription.Id = "btnShowDescription";
            btnShowDescription.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnShowDescription.Size = RibbonItemSize.Standard;
            btnShowDescription.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\Info.png", UriKind.RelativeOrAbsolute));
            btnShowDescription.ShowText = true;
            btnShowDescription.ShowImage = true;
            btnShowDescription.Click += new System.Windows.RoutedEventHandler(btnShowDescription_Click);


            RibbonButton btnGIS = new RibbonButton();
            btnGIS.Text = "GIS";
            btnGIS.Id = "btnGIS";
            btnGIS.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnGIS.Size = RibbonItemSize.Standard;
            btnGIS.Image = new BitmapImage(new Uri(Atend.Control.Common.fullPath + @"\Icon\GIS.png", UriKind.RelativeOrAbsolute));
            btnGIS.ShowText = true;
            btnGIS.ShowImage = true;
            btnGIS.Click += new System.Windows.RoutedEventHandler(btnGIS_Click);


            //------------------------------------------

            RibbonRow RibbonRowFile;
            RibbonRowFile = new RibbonRow();
            RibbonRowFile.Title = "RibbonRowFile";
            RibbonRowFile.Items.Add(btnNewDesign);
            RibbonRowFile.Items.Add(btnOpenDesign);
            RibbonRowFile.Items.Add(btnSaveDesign);
            RibbonRowFile.Items.Add(btnSaveasDesign);
            RibbonRowFile.Items.Add(btnSelectDesign);
            RibbonRowFile.Items.Add(btnSaveDesignOnServer);


            RibbonRow RibbonRowDesignSetting;
            RibbonRowDesignSetting = new RibbonRow();
            RibbonRowDesignSetting.Title = "RibbonRowDesignSetting";
            RibbonRowDesignSetting.Items.Add(rbutton2);
            RibbonRowDesignSetting.Items.Add(rbutton5);
            RibbonRowDesignSetting.Items.Add(btnDesignProfile);
            RibbonRowDesignSetting.Items.Add(btnPlan);
            RibbonRowDesignSetting.Items.Add(btnGetFromServer);
            RibbonRowDesignSetting.Items.Add(btnLoadBack);
            RibbonRowDesignSetting.Items.Add(btnRemark);
            RibbonRowDesignSetting.Items.Add(btnFrame);
            RibbonRowDesignSetting.Items.Add(btnShowDescription);

            RibbonRow RibbonRowGIS;
            RibbonRowGIS = new RibbonRow();
            RibbonRowGIS.Title = "RibbonRowGIS";
            RibbonRowGIS.Items.Add(btnGIS);


            //---------------------------------------

            RibbonPanelSource RibbonPanelSourceGIS;
            RibbonPanelSourceGIS = new RibbonPanelSource();
            RibbonPanelSourceGIS.Title = "GIS";
            RibbonPanelSourceGIS.Rows.Add(RibbonRowGIS);

            RibbonPanelSource RibbonPanelSourceFile;
            RibbonPanelSourceFile = new RibbonPanelSource();
            RibbonPanelSourceFile.Title = "فایل";
            RibbonPanelSourceFile.Rows.Add(RibbonRowFile);

            RibbonPanelSource RibbonPanelSourceDesignSetting;
            RibbonPanelSourceDesignSetting = new RibbonPanelSource();
            RibbonPanelSourceDesignSetting.Title = "تنظیمات طرح";
            RibbonPanelSourceDesignSetting.Rows.Add(RibbonRowDesignSetting);


            //---------------------------------
            RibbonPanel RibbonPanelGIS;
            RibbonPanelGIS = new RibbonPanel();
            RibbonPanelGIS.Source = RibbonPanelSourceGIS;


            RibbonPanel RibbonPanelFile;
            RibbonPanelFile = new RibbonPanel();
            RibbonPanelFile.Source = RibbonPanelSourceFile;

            RibbonPanel RibbonPanelDesignSetting;
            RibbonPanelDesignSetting = new RibbonPanel();
            RibbonPanelDesignSetting.Source = RibbonPanelSourceDesignSetting;
            //----------------------------------

            RibbonTab rTab;
            rTab = new RibbonTab();
            rTab.Title = "طرح";
            rTab.Id = "ID_Design";
            rTab.IsContextualTab = false;
            rTab.Panels.Add(RibbonPanelFile);
            rTab.Panels.Add(RibbonPanelDesignSetting);
            rTab.Panels.Add(RibbonPanelGIS);

            RibbonControl rControl = Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl;
            rControl.Tabs.Add(rTab);
            rControl.ActiveTab = rTab;


        }

        static void btnGIS_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {

                Atend.Global.Acad.DrawEquips.AcDrawGIS.DrawNewBlock(new Point3d(10, 10, 0));
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnShowDescription_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                System.Data.OleDb.OleDbConnection _Conection = new System.Data.OleDb.OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
                _Conection.Open();
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;
                try
                {
                    RibbonButton _RibbonButton = sender as RibbonButton;
                    using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
                    {
                        if (_RibbonButton != null && _RibbonButton.IsSelected == false)
                        {
                            _RibbonButton.IsSelected = true;
                            PromptSelectionResult psr = ed.SelectAll();
                            ObjectId[] ids = psr.Value.GetObjectIds();
                            foreach (ObjectId oi in ids)
                            {
                                Atend.Base.Acad.AT_INFO info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                                switch ((Atend.Control.Enum.ProductType)info1.NodeType)
                                {
                                    case Atend.Control.Enum.ProductType.AirPost:
                                        Atend.Global.Acad.DrawEquips.AcDrawAirPost.ShowDescription(oi, _Conection);
                                        break;

                                    case Atend.Control.Enum.ProductType.GroundPost:
                                        Atend.Global.Acad.DrawEquips.AcDrawGroundPost.ShowDescription(oi, _Conection);
                                        break;

                                    case Atend.Control.Enum.ProductType.Pole:
                                        Atend.Global.Acad.DrawEquips.AcDrawPole.ShowDescription(oi, _Conection);
                                        break;

                                    case Atend.Control.Enum.ProductType.Consol:
                                        Atend.Global.Acad.DrawEquips.AcDrawConsol.ShowDescription(oi, _Conection);
                                        break;
                                }
                            }
                        }
                        else if (_RibbonButton != null && _RibbonButton.IsSelected == true)
                        {
                            _RibbonButton.IsSelected = false;
                            TypedValue[] tvs = new TypedValue[] { new TypedValue((int)DxfCode.LayerName, Atend.Control.Enum.AutoCadLayerName.DESCRIPTION.ToString()) };
                            SelectionFilter sf = new SelectionFilter(tvs);
                            PromptSelectionResult psr = ed.SelectAll(sf);
                            ObjectId[] ids = psr.Value.GetObjectIds();
                            foreach (ObjectId oi in ids)
                            {
                                Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi);
                            }

                            Atend.Global.Acad.AcadLayer _AcadLayer = new Atend.Global.Acad.AcadLayer();
                            _AcadLayer.Delete(Atend.Control.Enum.AutoCadLayerName.DESCRIPTION.ToString());

                        }
                    }
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("ERROR DESC:{0} \n", ex.Message);
                }
                _Conection.Close();
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnFrame_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //throw new System.Exception("The method or operation is not implemented.");

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawFrame _frmDrawFrame = new Atend.Design.frmDrawFrame();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmDrawFrame) == DialogResult.OK)
                {
                    Atend.Base.Acad.AcadGlobal.FrameData.Height = _frmDrawFrame._Height;
                    Atend.Base.Acad.AcadGlobal.FrameData.Width = _frmDrawFrame._Width;
                    Atend.Base.Acad.AcadGlobal.FrameData.Products = _frmDrawFrame.dt_Products;
                    Atend.Base.Acad.AcadGlobal.FrameData.HaveDescription = _frmDrawFrame.HaveDescription;
                    Atend.Base.Acad.AcadGlobal.FrameData.HaveInformation = _frmDrawFrame.HaveInformation;
                    Atend.Base.Acad.AcadGlobal.FrameData.HaveSign = _frmDrawFrame.HaveSign;

                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_Frame ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void btnLoadBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                System.Windows.Forms.OpenFileDialog _OpenFileDialog = new OpenFileDialog();
                _OpenFileDialog.Title = "انتخاب زمینه";
                _OpenFileDialog.Filter = "DWG files|*.DWG";
                if (_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && _OpenFileDialog.FileName != string.Empty)
                {
                    try
                    {
                        System.IO.FileInfo _FileInfo = new FileInfo(_OpenFileDialog.FileName);
                        System.IO.File.Copy(_OpenFileDialog.FileName, string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, _FileInfo.Name), true);
                        Atend.Control.Common.DesignBackGroundName = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, _FileInfo.Name);
                        Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                        doc.SendStringToExecute("_LoadBackGround ", true, false, false);
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void rbuttonreportEXcelUTS_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
                int day = calender.GetDayOfMonth(DateTime.Today);
                int Month = calender.GetMonth(DateTime.Today);
                int Year = calender.GetYear(DateTime.Today);

                string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
                string NameFlash = "نتیجه محاسبات مکانیکی به روش UTS" + date + ".xls";
                Atend.Global.Utility.UReport.CreateExcelFinalMechanical(NameFlash, true);

                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "گزارش مکانیکی";
                notification.Msg = "گزارش محاسبات مکانیکی تولید شد";
                notification.infoCenterBalloon();

            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonreportEXcelMAXF_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
                int day = calender.GetDayOfMonth(DateTime.Today);
                int Month = calender.GetMonth(DateTime.Today);
                int Year = calender.GetYear(DateTime.Today);

                string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
                string NameFlash = "نتیجه محاسبات مکانیکی به روش MAXF" + date + ".xls";
                Atend.Global.Utility.UReport.CreateExcelFinalMechanical(NameFlash, false);

                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "گزارش مکانیکی";
                notification.Msg = "گزارش محاسبات مکانیکی تولید شد";
                notification.infoCenterBalloon();


            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }


        }

        static void rbuttonRudSurface_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmRudSurface rudSurface = new Atend.Calculating.frmRudSurface();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(rudSurface) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonreportElecExcel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
                int day = calender.GetDayOfMonth(DateTime.Today);
                int Month = calender.GetMonth(DateTime.Today);
                int Year = calender.GetYear(DateTime.Today);

                string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
                string Name = "گزارش محاسبات الکتریکی " + date + Atend.Control.Common.DesignName + ".xls";

                Atend.Global.Utility.UReport.CreateExcelReportFinalElectrical(Name);
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "گزارش الکتریکی";
                notification.Msg = "گزارش محاسبات الکتریکی تولید شد";
                notification.infoCenterBalloon();
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void rbuttonreportGISExcel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Report.frmGISSelectReport frm = new Atend.Report.frmGISSelectReport();
                frm.IsPDF = false;

                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void btnBSetting_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0)
            {
                Atend.Base.frmBSetting BSetting = new Atend.Base.frmBSetting();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(BSetting) == DialogResult.OK)
                {

                }
            }

        }

        static void btnRemark_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {

                Atend.UControls.Design.frmWordRemark remark = new Atend.UControls.Design.frmWordRemark();
                remark.FullPath = Atend.Control.Common.fullPath;
                remark.DesignFullAddress = Atend.Control.Common.DesignFullAddress;
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(remark) == DialogResult.OK)
                {
                    //Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    //doc.SendStringToExecute("_StreetBox ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
            ///////////////////////
            //if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            //{
            //    Atend.Design.frmRemark _frmRemark = new Atend.Design.frmRemark();
            //    _frmRemark.Show();
            //}
            //else
            //{
            //    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
            //    notification.Title = "انتخاب طرح";
            //    notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
            //    notification.ShowStatusBarBalloon();
            //}
        }

        static void btnProjectCode_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Base.frmProjectCode _frmProjectCode = new Atend.Base.frmProjectCode();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmProjectCode) == DialogResult.OK)
                { }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
            }
        }

        static void btnIsExist_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Base.frmEquipStatus _frmEquipStatus = new Atend.Base.frmEquipStatus();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmEquipStatus) == DialogResult.OK)
                { }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
            }
        }

        static void btnPlan_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
                {
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_FINALDESIGN ", true, false, false);

                }
                else
                {
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "انتخاب طرح";
                    notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                    notification.ShowStatusBarBalloon();
                    return;
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERRO RIBBON : {0} \n", ex.Message);
            }

        }

        static void btnPlan_Click02(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                RibbonButton _RibbonButton = sender as RibbonButton;
                using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    if (_RibbonButton != null && _RibbonButton.IsSelected == false)
                    {
                        _RibbonButton.IsSelected = true;
                        Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                        doc.SendStringToExecute("_FINALDESIGN ", true, false, false);


                    }
                    else if (_RibbonButton != null && _RibbonButton.IsSelected == true)
                    {
                        _RibbonButton.IsSelected = false;
                        TypedValue[] tvs = new TypedValue[] { new TypedValue((int)DxfCode.LayerName, Atend.Control.Enum.AutoCadLayerName.SHELL.ToString()) };
                        SelectionFilter sf = new SelectionFilter(tvs);
                        PromptSelectionResult psr = ed.SelectAll(sf);
                        ObjectId[] ids = psr.Value.GetObjectIds();
                        foreach (ObjectId oi in ids)
                        {
                            Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi);
                        }

                        Atend.Global.Acad.AcadLayer _AcadLayer = new Atend.Global.Acad.AcadLayer();
                        _AcadLayer.Delete(Atend.Control.Enum.AutoCadLayerName.SHELL.ToString());

                    }
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERRO RIBBON : {0} \n", ex.Message);
            }

        }

        static void rbuttonSetDefault_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmSetDefaultMec SetDefaultMec = new Atend.Calculating.frmSetDefaultMec();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(SetDefaultMec) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnTransferBProduct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (!Atend.Base.Base.BProduct.GetFromServer())
                {
                    throw new System.Exception("Atend.Base.Base.BProduct.GetFromServer failed");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("*** ERROR :{0} \n", ex.Message);
            }

        }

        static void rbuttonreportElec_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Report.frmChoiceReportElec elec = new Atend.Report.frmChoiceReportElec();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(elec) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

            //Atend.Report.frmChoiceReportElec elec = new Atend.Report.frmChoiceReportElec();
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(elec);
        }

        static void btnGetFromServer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Atend.Design.frmGetFromServer stb = new Atend.Design.frmGetFromServer();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(stb);
        }

        static void btnServer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Atend.Design.frmSettingServer setServer = new Atend.Design.frmSettingServer();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(setServer);
        }

        //static void btnSupport_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    Atend.Base.frmSettingSupport support = new Atend.Base.frmSettingSupport();
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(support);
        //}

        //static void btnMafsal_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
        //    {
        //        Atend.Design.XXfrmDrawMafsal02 FrmDrawMafsal = new Atend.Design.XXfrmDrawMafsal02();
        //        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDrawMafsal) == System.Windows.Forms.DialogResult.OK)
        //        {

        //        }

        //    }
        //    else
        //    {

        //        Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
        //        notification.Title = "انتخاب طرح";
        //        notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
        //        notification.ShowStatusBarBalloon();
        //        return;
        //    }
        //}

        static void rEditCellStatus_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //throw new System.Exception("The method or operation is not implemented.");
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {

                Atend.Design.frmChangeCellStatus FCCS = new Atend.Design.frmChangeCellStatus();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FCCS) == DialogResult.OK)
                {

                }


            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnSelfKeeper_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //Atend.Control.Common.EquipmentType = Atend.Control.Enum.EquipmentType.Branch;
            //ed.WriteMessage("Show Draw Branch Form \n");


            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {


                Atend.Design.frmDrawSelfKeeper02 FDSK = new Atend.Design.frmDrawSelfKeeper02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FDSK) == DialogResult.OK)
                {
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_SelfKeeper ", true, false, false);

                }



            }
            else
            {

                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void btnMiddleJackPanel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //if (Atend.Control.Common.userCode != 0)
            //{
            //    Atend.Design.frmDrawMiddleJackPanel02 frmMiddle = new Atend.Design.frmDrawMiddleJackPanel02();
            //    if (frmMiddle.ShowDialog() == DialogResult.OK)
            //    {
            //        ed.WriteMessage("Form OK \n");
            //        Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            //        doc.SendStringToExecute("_MiddleJackPanel ", true, false, false);

            //    }

            //}
            //else
            //{
            //    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
            //    notification.Title = "انتخاب طرح";
            //    notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
            //    notification.ShowStatusBarBalloon();
            //}

        }

        static void btnCell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;


            //Atend.Design.frmDrawCell FRMdrawCell = new Atend.Design.frmDrawCell();

            //if (FRMdrawCell.ShowDialog() == DialogResult.OK)
            //{
            //    switch (FRMdrawCell.CellType)
            //    {
            //        case 1:
            //            break;
            //        case 2:

            //            break;
            //        case 3:
            //            doc.SendStringToExecute("_Secsionercell ", true, false, false);

            //            break;
            //        case 4:
            //            doc.SendStringToExecute("_dezhangtorCell ", true, false, false);
            //            break;
            //        case 5:
            //            break;
            //        case 6:
            //            break;
            //    }
            //}
        }

        static void rKhazanTipbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //throw new System.Exception("The method or operation is not implemented.");

            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawKhazan02 khazan = new Atend.Design.frmDrawKhazan02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(khazan) == DialogResult.OK)
                {
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_KHAZAN ", true, false, false);

                }


            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }


            //Atend.Design.frmDrawKhazan FrmDrawKhazan = new Atend.Design.frmDrawKhazan();
            //if (FrmDrawKhazan.ShowDialog() == DialogResult.OK)
            //{




            //    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            //    doc.SendStringToExecute("_Khazan ", true, false, false);



            //}

        }

        static void rGroundCablebutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawGroundCabelTip03 cable = new Atend.Design.frmDrawGroundCabelTip03();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(cable) == DialogResult.OK)
                {
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_GroundCabel ", true, false, false);

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;

            }
        }

        static void btnSaveDesignOnServer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (Atend.Control.Common.DesignName != "" && Atend.Control.Common.userCode != 0)
            {
                doc.SendStringToExecute("_QSAVE ", true, false, false);
                Atend.Design.frmDesignSaveServer02 _frmDesignSaveServer = new Atend.Design.frmDesignSaveServer02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmDesignSaveServer) == DialogResult.OK)
                {
                    doc.SendStringToExecute("_QSAVE ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;

            }



            //Atend.Design.frmDesignSave _frmdesignSave = new Atend.Design.frmDesignSave();
            //if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmdesignSave) == DialogResult.OK)
            //{
            //    ed.WriteMessage("form ok\n");
            //    Atend.Base.Design.DDesignProfile _DDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            //    if (_DDesignProfile.Code == 0)
            //    {
            //        Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
            //        notification.Title = "مشخصات طرح";
            //        notification.Msg = "لطفاً مشخصات طرح را مشخص نمایید";
            //        notification.ShowStatusBarBalloon();

            //    }
            //    else
            //    {
            //        ed.WriteMessage("profile found\n");
            //        int DesignId = 0;
            //        Atend.Base.Design.DDesign _DDesign = Atend.Base.Design.DDesign.SelectByAtendCode(_DDesignProfile.DesignCode);
            //        if (_DDesign.Code != -1)
            //        {
            //            ed.WriteMessage("file exist in design \n");
            //            DesignId = _DDesign.Id;
            //        }
            //        else
            //        {
            //            ed.WriteMessage("file was not exist\n");
            //            _DDesign.ArchiveNo = "-1";
            //            _DDesign.Code = 0;
            //            //_DDesign.Region_Code = 0;
            //            _DDesign.Title = _DDesignProfile.DesignName;
            //            //_DDesign.AtendCoed = _DDesignProfile.DesignCode;
            //            if (_DDesign.Insert())
            //            {
            //                DesignId = _DDesign.Id;
            //            }
            //        }

            //        if (DesignId != 0)
            //        {
            //            ed.WriteMessage("design id found : {0}\n", DesignId);
            //            //Atend.Base.Design.DDesignFile _DDesignFile = new Atend.Base.Design.DDesignFile();
            //            FileStream fs;
            //            fs = File.Open(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
            //            BinaryReader br = new BinaryReader(fs);
            //            Atend.Base.Design.DDesignFile dDesignFile = new Atend.Base.Design.DDesignFile();
            //            dDesignFile.DesignId = DesignId;
            //            dDesignFile.FileSize = Convert.ToInt64(br.BaseStream.Length);
            //            dDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
            //            if (!dDesignFile.Insert())
            //            {
            //                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
            //                notification.Title = "ذخیره سازی";
            //                notification.Msg = "خطا در زمان ثبت طرح روی سرور";
            //                notification.ShowStatusBarBalloon();

            //            }
            //        }

            //    }
            //}
        }

        static void btnSelectDesign_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (Atend.Control.ConnectionString.ConnectionValidation(Atend.Control.ConnectionString.ServercnString) && Atend.Control.ConnectionString.ConnectionValidation(Atend.Control.ConnectionString.LocalcnString))
                {
                    try
                    {
                        if (Atend.Control.Common.userCode != 0)
                        {
                            Atend.Design.frmDesignSearch01 _frmdesignSearch = new Atend.Design.frmDesignSearch01();
                            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmdesignSearch) == DialogResult.OK)
                            {
                                try
                                {
                                    FileInfo fi = new FileInfo(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")));
                                    string Destination = fi.Directory.FullName;
                                    Atend.Global.Acad.DrawinOperation dOperation = new Atend.Global.Acad.DrawinOperation();
                                    dOperation.GetFileFromAtendFile(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), Destination);
                                    string NewName = fi.FullName.Replace(".ATNX", ".DWG");
                                    Atend.Control.Common.DesignFullAddress = Destination;
                                    Atend.Control.Common.DesignName = fi.Name.Replace(".ATNX", ".DWG");
                                    Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, fi.Name.Replace(".ATNX", ".MDB"));
                                    DocumentCollection _DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                                    if (File.Exists(NewName))
                                    {
                                        if (_DocumentCollection.Open(NewName, false) != null)
                                        {
                                            Atend.Base.Acad.AT_COUNTER.ReadAll();
                                        }
                                    }
                                    else
                                    {
                                        ed.WriteMessage("File was not exist : {0} \n", NewName);
                                    }

                                }
                                catch (System.Exception ex)
                                {
                                    ed.WriteMessage("OPEN ERROR : {0} \n", ex.Message);
                                }
                            }
                        }
                        else
                        {
                            Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                            notification.Title = "انتخاب طرح";
                            notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                            notification.ShowStatusBarBalloon();
                            return;
                        }

                    }
                    catch (System.Exception ex1)
                    {
                        ed.WriteMessage("ERROR02 : {0} \n", ex1.Message);
                    }
                }
                else
                {
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "ارتباط با سرور";
                    notification.Msg = "ارتباط با سرور تنظیم نمی باشد";
                    notification.ShowStatusBarBalloon();
                }
            }
            catch (System.Exception ex)
            {
                //ed.WriteMessage("Error01 : {0} \n", ex.Message);

                return;
            }
        }

        static void btnSaveDesign_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.DesignName != "" && Atend.Control.Common.userCode != 0)
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                doc.SendStringToExecute("_QSAVE ", true, false, false);
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

            ////if (Atend.Control.Common.DesignName != "" && Atend.Control.Common.userCode != 0)
            ////{
            ////    Atend.Base.Acad.AT_COUNTER.SaveAll();
            ////    Atend.Design.frmDesignSave _frmDesignSave = new Atend.Design.frmDesignSave();
            ////    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmDesignSave) == System.Windows.Forms.DialogResult.OK)
            ////    {
            ////        //if (Atend.Control.Common.DesignName != "" && Atend.Control.Common.userCode != 0)
            ////        //{
            ////        //    Atend.Design.frmDesignSave _frmDesignSave = new Atend.Design.frmDesignSave();
            ////        //    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmDesignSave) == DialogResult.OK)
            ////        //    {


            ////        //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ////        //Atend.Global.Acad.DrawinOperation dOperation = new Atend.Global.Acad.DrawinOperation();

            ////        //string DWGName = Atend.Control.Common.DesignName;
            ////        //string ATNXName = Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX");
            ////        //string MDBName = Atend.Control.Common.DesignName.Replace(".DWG", ".MDB");

            ////        //ed.WriteMessage("{0}\\{1}\n", Atend.Control.Common.DesignFullAddress, ATNXName);
            ////        //ed.WriteMessage("{0}\\{1}\n", Atend.Control.Common.DesignFullAddress, DWGName);
            ////        //ed.WriteMessage("{0}\\{1}\n", Atend.Control.Common.DesignFullAddress, MDBName);

            ////        //dOperation.AddFileToAtendFile(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, ATNXName),
            ////        //                              string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, DWGName));

            ////        //dOperation.AddFileToAtendFile(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, ATNXName),
            ////        //                              string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, MDBName));


            ////        //if (Atend.Control.Common.DesignName != "" && Atend.Control.Common.userCode != 0)
            ////        //{
            ////        //    Atend.Base.Acad.AT_COUNTER.SaveAll();
            ////        //}

            ////    }
            ////}//

        }

        static void btnSaveAsDesign_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.DesignName != "" && Atend.Control.Common.userCode != 0)
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                doc.SendStringToExecute("_QSAVE ", true, false, false);


                Atend.Design.frmDesignSaveAs saveas = new Atend.Design.frmDesignSaveAs();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(saveas) == DialogResult.OK)
                {
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnOpenDesign_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //hrow new System.Exception("The method or operation is not implemented.");
            if (Atend.Control.Common.userCode != 0)
            {
                //using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
                //{

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                Atend.Design.frmOpenDesign02 frmOpenDesign = new Atend.Design.frmOpenDesign02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmOpenDesign) == DialogResult.OK)
                {
                    try
                    {
                        FileInfo fi = new FileInfo(frmOpenDesign.filePath);
                        string Destination = fi.Directory.FullName;
                        Atend.Global.Acad.DrawinOperation dOperation = new Atend.Global.Acad.DrawinOperation();
                        dOperation.GetFileFromAtendFile(frmOpenDesign.filePath, Destination);
                        string NewName = fi.FullName.Replace(".ATNX", ".DWG");
                        Atend.Control.Common.DesignFullAddress = Destination;
                        Atend.Control.Common.DesignName = fi.Name.Replace(".ATNX", ".DWG");
                        Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, fi.Name.Replace(".ATNX", ".MDB"));

                        DocumentCollection _DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                        if (File.Exists(NewName))
                        {
                            _DocumentCollection.Open(NewName, false);
                        }
                        else
                        {
                            ed.WriteMessage("File was not exist : {0} \n", NewName);
                        }
                        Atend.Base.Acad.AT_COUNTER.ReadAll();
                    }
                    catch (System.Exception ex)
                    {
                        ed.WriteMessage("OPEN ERROR : {0} \n", ex.Message);
                    }
                }
                else
                {
                    //ed.WriteMessage("DR={0}\n", frmOpenDesign.DialogResult);
                }
            }
        }

        static void btnNewDesign_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                Atend.Design.frmNewDesign02 frmNewDesin = new Atend.Design.frmNewDesign02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmNewDesin) == DialogResult.OK)
                {
                    Atend.Base.Acad.AT_COUNTER.ReadAll();
                    //ed.WriteMessage("Pole:{0}", Atend.Control.Common.Counters.PoleCounter);
                    //ed.WriteMessage("Consol:{0}", Atend.Control.Common.Counters.ConsolCounter);
                    //ed.WriteMessage("Clamp:{0}", Atend.Control.Common.Counters.ClampCounter);
                    //ed.WriteMessage("HeaderCabel:{0}\n", Atend.Control.Common.Counters.HeadercableCounter);
                }
            }
        }

        static void btnViewReport_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.DesignName != "" && Atend.Control.Common.userCode != 0)
            {
                Atend.Report.frmViewReport ViewReport = new Atend.Report.frmViewReport();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(ViewReport) == DialogResult.OK)
                { }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnStatusReport_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Atend.Calculating.frmTest T = new Atend.Calculating.frmTest();
            //T.ShowDialog();
            //return;

            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //////////Atend.Calculating.frmTest frmt = new Atend.Calculating.frmTest();
                //////////frmt.Show();
                Atend.Global.Utility.UReport Report = new Atend.Global.Utility.UReport();
                System.Data.DataTable Table = Report.CreateExcelStatus();
                ed.WriteMessage("ST Report2 - table Row Count = " + Table.Rows.Count.ToString() + "\n");
                //____________________________________________________
                Dictionary<string, string> Dic = new Dictionary<string, string>();
                Dic.Add("Code", "كد كالا");
                Dic.Add("Name", "نام كالا");
                Dic.Add("Count", "تعداد");
                Dic.Add("Exist", "نوع صورت وضعیت");
                Dic.Add("ProjectCode", "کد دستور کار تجهیز");
                Dic.Add("Unit", "واحد كالا");
                Dic.Add("Price", "قیمت واحد");
                Dic.Add("ExecutePrice", "قیمت اجرا");
                Dic.Add("WagePrice", "قیمت دستمزد");
                Dic.Add("ProjectName1", "شرح دستور کار");
                Atend.Base.Design.DDesignProfile DP = Atend.Base.Design.DDesignProfile.AccessSelect();
                System.Data.DataTable UnitTable = Atend.Base.Base.BUnit.SelectAll();
                try
                {
                    for (int i = 0; i < Table.Rows.Count; i++)
                    {
                        if (Atend.Control.NumericValidation.Int32Converter(Table.Rows[i]["Unit"].ToString()))
                        {
                            DataRow[] drs = UnitTable.Select(string.Format("Code={0}", Convert.ToInt32(Table.Rows[i]["Unit"])));
                            if (drs.Length > 0)
                            {
                                Table.Rows[i]["Unit"] = drs[0]["Name"];
                            }
                        }

                        //Atend.Base.Design.DDesignProfile DP = Atend.Base.Design.DDesignProfile.AccessSelect();
                        //if (DP.DesignId <= 0)
                        //{
                        //    Atend.Base.Design.DDesign Des = new Atend.Base.Design.DDesign();
                        //    Des.Title = DP.DesignName;
                        //    Des.ArchiveNo = DP.DesignCode;
                        //    Des.PRGCode = 0;
                        //    Des.RequestType = 0;
                        //    Des.Region = 0;
                        //    Des.IsAtend = true;
                        //    Des.Address = DP.Address;

                        //    if (Des.Insert())
                        //    {
                        //        DP.DesignId = Des.Id;
                        //        if (!DP.AccessUpdate())
                        //            ed.WriteMessage("\nError In D_DesignProfile Access Update\n");

                        //    }
                        //    else
                        //        ed.WriteMessage("\nError In D_Design Server Insert\n");

                        //}


                        //Atend.Base.Design.DStatusReport.DeleteByDesignId(DP.DesignId);

                        //Atend.Base.Design.DStatusReport STR = new Atend.Base.Design.DStatusReport();
                        //STR.DesignId = DP.DesignId;
                        //if (Atend.Control.NumericValidation.Int32Converter(Table.Rows[i]["Code"].ToString()))
                        //{
                        //    STR.ProductCode = Convert.ToInt32(Table.Rows[i]["Code"].ToString());
                        //}
                        //else
                        //    STR.ProductCode = 0;
                        //STR.Existance = Convert.ToInt32(Table.Rows[i]["Exist"].ToString());
                        ////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Convert.ToInt32(Table.Rows[i]["ProjectCode"].ToString())).AdditionalCode;
                        //int pc = Convert.ToInt32(Table.Rows[i]["ProjectCode"].ToString());
                        //STR.ProjectCode = pc;
                        //Table.Rows[i]["ProjectCode"] = pc;
                        //Table.Rows[i]["Count"] = Math.Round(Convert.ToDouble(Table.Rows[i]["Count"].ToString()), 2);
                        //STR.Count = Convert.ToDouble(Table.Rows[i]["Count"].ToString());
                        //if (!STR.Insert())
                        //{
                        //    ed.WriteMessage("\nError In D_StatusReport Insertion\n");
                        //}
                        Table.Rows[i]["Exist"] = Atend.Base.Base.BEquipStatus.SelectByACode(Convert.ToInt32(Table.Rows[i]["Exist"])).Name;

                        //if (Table.Rows[i]["Exist"].ToString() == "0")
                        //    Table.Rows[i]["Exist"] = "موجود - موجود";
                        //else
                        //    if (Table.Rows[i]["Exist"].ToString() == "1")
                        //        Table.Rows[i]["Exist"] = "موجود - مستعمل";
                        //    else
                        //        if (Table.Rows[i]["Exist"].ToString() == "2")
                        //            Table.Rows[i]["Exist"] = "موجود - اسقاط";
                        //        else
                        //            if (Table.Rows[i]["Exist"].ToString() == "3")
                        //                Table.Rows[i]["Exist"] = "موجود - جابجایی";
                        //            else
                        //                if (Table.Rows[i]["Exist"].ToString() == "4")
                        //                    Table.Rows[i]["Exist"] = "پیشنهادی - نو";
                        //                else
                        //                    if (Table.Rows[i]["Exist"].ToString() == "5")
                        //                        Table.Rows[i]["Exist"] = "پیشنهادی - جابجایی";
                        //ed.WriteMessage("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                        //ed.WriteMessage("Code = " + Table.Rows[i]["Code"].ToString() + "\n");
                        //ed.WriteMessage("Name = " + Table.Rows[i]["Name"].ToString() + "\n");
                        //ed.WriteMessage("Count = " + Table.Rows[i]["Count"].ToString() + "\n");
                        //ed.WriteMessage("Unit = " + Table.Rows[i]["Unit"].ToString() + "\n");
                        //ed.WriteMessage("Price = " + Table.Rows[i]["Price"].ToString() + "\n");
                        //ed.WriteMessage("ProjectCode = " + Table.Rows[i]["ProjectCode"].ToString() + "\n");
                    }
                    try
                    {

                        Atend.Global.Utility.UReport.CreateExcelReaportForStatus("صورت وضعیت.xls", Table, 1);
                        Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                        notification.Title = "صدور دستورکار";
                        notification.Msg = "گزارش دستورکار تولید شد";
                        notification.infoCenterBalloon();



                    }
                    catch (System.Exception ex1)
                    {
                        ed.WriteMessage(string.Format("Error Exel : {0} \n", ex1.Message));
                    }
                }
                catch (System.Exception ex2)
                {
                    ed.WriteMessage(string.Format("Error : {0} \n", ex2.Message));
                }//
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void btnDesignProfile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //throw new System.Exception("The method or operation is not implemented.");
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Base.frmDesignProfile desifnProile = new Atend.Base.frmDesignProfile();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(desifnProile) == DialogResult.OK)
                {
                    Atend.Global.Acad.DrawEquips.AcDrawFrame _AcDrawFrame = new Atend.Global.Acad.DrawEquips.AcDrawFrame();
                    _AcDrawFrame.UpdateFrame();
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
            }
        }

        static void btnDefineUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0)
            {
                Atend.Design.frmUser FrmDesign = new Atend.Design.frmUser();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDesign);
            }
        }

        static void btnShowAccess_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0)
            {
                Atend.Design.frmUserAccessibility frmuser = new Atend.Design.frmUserAccessibility();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmuser);
            }
            //throw new System.Exception("The method or operation is not implemented.");


        }

        static void btnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                //ed.WriteMessage("LocalString={0}\n", Atend.Control.ConnectionString.LocalcnString);
                //if (Atend.Control.ConnectionString.ConnectionValidation(Atend.Control.ConnectionString.LocalcnString))
                //{
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                doc.SendStringToExecute("_ATLOGIN ", true, false, false);
                //}
                //else
                //{
                //    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                //    notification.Title = "پایگاه داده محلی";
                //    notification.Msg = "اتصال به پایگاه داده محلی برقرار نشد";
                //    notification.infoCenterBalloon();
                //}
            }
            catch
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "پایگاه داده محلی";
                notification.Msg = "اتصال به پایگاه داده محلی برقرار نشد";
                notification.infoCenterBalloon();
                return;
            }
        }

        static void btnGetUserAccess_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            if (Atend.Control.Common.userCode == 0)//&& Atend.Control.Common.DesignName == "")
            {
                if (Atend.Base.Design.DUser.GetFromServer())
                {
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "";
                    notification.Msg = "دریافت اطلاعات با موفقیت انجام شد";
                    notification.infoCenterBalloon();
                }
                else
                {
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "";
                    notification.Msg = "خطا در عملیات دریافت اطلاعات";
                    notification.infoCenterBalloon();
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "خطا";
                notification.Msg = "عدم دریافت اطلاعات به علت ورود به نرم افزار";
                notification.infoCenterBalloon();
            }
        }

        //static void btnServer_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        static void btnLocal_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Atend.Design.frmSetting FRMSetting = new Atend.Design.frmSetting();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FRMSetting);
            }
            catch (System.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("ERROR :{0} \n", ex.Message);
            }

        }

        static void btnManageLayer_Click(object Sender, System.Windows.RoutedEventArgs e)
        {

            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {

                Atend.Design.frmEquipLayer02 _frmEquipLayer = new Atend.Design.frmEquipLayer02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmEquipLayer) == DialogResult.OK)
                { }

            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();

            }
        }

        static void btnPoleNumbering_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //////throw new System.Exception("The method or operation is not implemented.");

            ////Guid startPole, endPole;

            ////Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            ////PromptEntityOptions peo = new PromptEntityOptions("Select Start Pole: \n");
            ////PromptEntityResult per = ed.GetEntity(peo);
            ////if (per.Status == PromptStatus.OK)
            ////{

            ////    Atend.Acad.AcadCommands acadCommands = new AcadCommands();
            ////    if (acadCommands.IsPole(per.ObjectId))
            ////    {

            ////        startPole = acadCommands.NodeGuid;

            ////        per = ed.GetEntity(peo);
            ////        if (per.Status == PromptStatus.OK)
            ////        {
            ////            if (acadCommands.IsPole(per.ObjectId))
            ////            {
            ////                endPole = acadCommands.NodeGuid;

            ////                // at this point both pole were selected correctly
            ////                if (acadCommands.Iteration(startPole, endPole))
            ////                {
            ////                    ed.WriteMessage("Iteration Done. \n");

            ////                    //foreach (Atend.Acad.AcadCommands.NodeListInformation nl in acadCommands.NodeList)
            ////                    //{

            ////                    //    ed.WriteMessage("{0} \n",nl.NodeCode);

            ////                    //}

            ////                }

            ////            }
            ////            else
            ////            {
            ////                ed.WriteMessage("Selected Entity was not pole. \n");
            ////            }
            ////        }

            ////    }
            ////    else
            ////    {
            ////        ed.WriteMessage("Selected Entity was not pole. \n");
            ////    }

            ////}

            //Atend.Design.frmNumberingStondard FRMnumbering = new Atend.Design.frmNumberingStondard();
            //FRMnumbering.Show();

        }

        static void btnOutputReport_Click(object sender, System.Windows.RoutedEventArgs e)
        {


        }

        static void rbutton1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmNetWorkCross frmNetWorkCross = new Atend.Calculating.frmNetWorkCross();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmNetWorkCross) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonMaxF_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmOptimalSagTensionMaxF02 FrmOptionalSagTensionMaxF = new Atend.Calculating.frmOptimalSagTensionMaxF02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmOptionalSagTensionMaxF) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

        }

        static void rbuttonElectrical_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmElecterical electrical = new Atend.Calculating.frmElecterical();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(electrical) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonCalcCrossSectionArea_Click(object Sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmCrossSection crossSection = new Atend.Calculating.frmCrossSection();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(crossSection) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonCalcShortCircuit_Click(object Sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmShortCircuit shortcircuit = new Atend.Calculating.frmShortCircuit();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(shortcircuit) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonCalcTransCapacity_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmTransCapacity frmTrans = new Atend.Calculating.frmTransCapacity();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmTrans) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonLoad_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmLoad load = new Atend.Calculating.frmLoad();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(load) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonDefine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmLoadFactor loadFactor = new Atend.Calculating.frmLoadFactor();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(loadFactor) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbutton_Click(object sender, System.Windows.RoutedEventArgs e)//OPTIMALSAGTENSIONUTS
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmOptimalSagTensionVTS02 FrmOptionalSagTensionVTS = new Atend.Calculating.frmOptimalSagTensionVTS02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmOptionalSagTensionVTS) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonreport_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Report.frmChoiceReportMec frm = new Atend.Report.frmChoiceReportMec();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }


            //if (Atend.Control.Common.userCode != 0)
            //{
            //    Atend.Report.frmChoiceReportMec frm = new Atend.Report.frmChoiceReportMec();
            //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);
            //}
            //else
            //{
            //    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
            //    notification.Title = "انتخاب طرح";
            //    notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
            //    notification.ShowStatusBarBalloon();
            //}


        }

        static void rbuttonreportGIS_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Report.frmGISSelectReport frm = new Atend.Report.frmGISSelectReport();
                frm.IsPDF = true;

                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm) == DialogResult.OK)
                {

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        static void rbuttonremark_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Report.frmRemarkReport RemarkReport = new Atend.Report.frmRemarkReport();
                //RemarkReport.ShowDialog();
                //Atend.Report.frmSelectReports f = new Atend.Report.frmSelectReports();
                //f.ShowDialog();
                //Atend.Report.frmGroundPostDiagramReport f = new Atend.Report.frmGroundPostDiagramReport();
                //f.ShowDialog();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(RemarkReport) == DialogResult.OK) { }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
            }
        }

        static void rbuttonTest_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Calculating.frmOptimalSagTensionVTSTest FrmOptionalSagTensionVTS = new Atend.Calculating.frmOptimalSagTensionVTSTest();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmOptionalSagTensionVTS);
                //FrmOptionalSagTensionVTS.Show();
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
            }


        }

        static void rDisconnectorbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                ed.WriteMessage("Ribbon\n");
                Atend.Design.frmDrawDisconnector02 FrmDrawDisConnector = new Atend.Design.frmDrawDisconnector02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDrawDisConnector) == System.Windows.Forms.DialogResult.OK)
                {
                    ed.WriteMessage("GoTo Draw\n");
                    doc.SendStringToExecute("_DisConnector ", true, false, false);

                }

            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }


            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //PromptPointOptions p = new PromptPointOptions("لطفا نقطه کاشت سکسیونر راانتخاب نمایید:");
            //PromptPointResult r = ed.GetPoint(p);
            //if (r.Status == PromptStatus.OK)
            //{
            //    if (Atend.Control.Common.SelectedDesignCode == 0)
            //    {
            //        System.Windows.Forms.MessageBox.Show("اطفاً ابتدا طرح مورد نظر را انتخاب نمایید");
            //        return;
            //    }
            //    ed.WriteMessage(r.Value.ToString() + "\n");
            //    //Design.frmDrawPole01 frmdrawPole = new Atend.Design.frmDrawPole01(r.Value);
            //    //frmdrawPole.ShowDialog();
            //    Atend.Design.frmDrawDisconnector frmdrawDisconnector = new Atend.Design.frmDrawDisconnector();
            //    if (frmdrawDisconnector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {


            //        //    Draw node Code ///
            //        Atend.Acad.AcadDrawNode myAcadDrawNode = new Atend.Acad.AcadDrawNode();
            //        //myAcadDrawNode.MyNodeInformation = MyNodeInformation;
            //        myAcadDrawNode.MyNodeInformation.X = r.Value.X;
            //        myAcadDrawNode.MyNodeInformation.Y = r.Value.Y;
            //        ed.WriteMessage("i am going to myAcadDrawNode.SaveNodeInformation \n");
            //        if (myAcadDrawNode.SaveNodeInformation())
            //        {
            //            ed.WriteMessage("Node inserted On screen and database correctly \n");
            //        }
            //        else
            //        {
            //            ed.WriteMessage("Node insertion On screen and database failed \n");
            //        }
            //    }
            //    ////////////////////////

            //}


        }

        static void rCatOutbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                //ed.WriteMessage("Ribbon\n");
                Atend.Design.frmDrawCatOut02 FrmDrawCatOut = new Atend.Design.frmDrawCatOut02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDrawCatOut) == System.Windows.Forms.DialogResult.OK)
                {
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_CatOut ", true, false, false);

                }

            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;


            }
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //PromptPointOptions p = new PromptPointOptions("لطفا نقطه کاشت کت اوت راانتخاب نمایید:");
            //PromptPointResult r = ed.GetPoint(p);
            //if (r.Status == PromptStatus.OK)
            //{
            //    if (Atend.Control.Common.SelectedDesignCode == 0)
            //    {
            //        System.Windows.Forms.MessageBox.Show("اطفاً ابتدا طرح مورد نظر را انتخاب نمایید");
            //        return;
            //    }
            //    else
            //    {
            //        ed.WriteMessage(r.Value.ToString() + "\n");
            //        //Design.frmDrawPole01 frmdrawPole = new Atend.Design.frmDrawPole01(r.Value);
            //        //frmdrawPole.ShowDialog();
            //        Atend.Design.frmDrawCatOut frmdrawCatOut = new Atend.Design.frmDrawCatOut();
            //        if (frmdrawCatOut.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {


            //            //    Draw node Code ///
            //            Atend.Acad.AcadDrawNode myAcadDrawNode = new Atend.Acad.AcadDrawNode();
            //            //myAcadDrawNode.MyNodeInformation = MyNodeInformation;
            //            myAcadDrawNode.MyNodeInformation.X = r.Value.X;
            //            myAcadDrawNode.MyNodeInformation.Y = r.Value.Y;
            //            ed.WriteMessage("i am going to myAcadDrawNode.SaveNodeInformation \n");
            //            if (myAcadDrawNode.SaveNodeInformation())
            //            {
            //                ed.WriteMessage("Node inserted On screen and database correctly \n");
            //            }
            //            else
            //            {
            //                ed.WriteMessage("Node insertion On screen and database failed \n");
            //            }
            //        }
            //    }
            //    ////////////////////////
            //}
        }

        static void rAirPostbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                if (Atend.Control.Common.Demo)
                {
                    if (Atend.Base.Design.DPost.AccessSelectAll().Rows.Count > 1)
                    {
                        return;
                    }
                }
                Atend.Design.frmDrawAirPost02 FrmDrawAirPost = new Atend.Design.frmDrawAirPost02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDrawAirPost) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.SendStringToExecute("_AirPost ", true, false, false);

                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;


            }





        }

        static void rRodbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawRod02 FrmDrawRod = new Atend.Design.frmDrawRod02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDrawRod) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.SendStringToExecute("_ROD ", true, false, false);
                }




            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;

            }


        }

        static void rGroundPostbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;


            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                if (Atend.Control.Common.Demo)
                {
                    if (Atend.Base.Design.DPost.AccessSelectAll().Rows.Count > 1)
                    {
                        return;
                    }

                }
                Atend.Design.frmDrawGroundPost03 FrmDrawGrounPost = new Atend.Design.frmDrawGroundPost03();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDrawGrounPost) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.SendStringToExecute("_GroundPost ", true, false, false);

                }

            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;


            }




        }

        static void rJumperbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {


            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                doc.SendStringToExecute("_Jumper ", true, false, false);
            }
            else
            {

                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();

            }



        }

        //static void rOffLinebutton_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("YES");
        //    string path = Atend.Control.Common.fullPath + "\\Database\\Atend.mdb";
        //    SaveFileDialog save = new SaveFileDialog();
        //    Boolean transferTrue = true;
        //    if (save.ShowDialog() == DialogResult.OK)
        //    {
        //        //int index=save.FileName.LastIndexOf("\\");
        //        //int lenght=save.FileName.Length-index;
        //        //string folderName = save.FileName.Substring(index, lenght);
        //        //MessageBox.Show("FolderName= "+folderName);
        //        //string driver = save.FileName.Substring(0, index);
        //        Directory.CreateDirectory(save.FileName);
        //        ed.WriteMessage("Created Directory\n");
        //        System.IO.File.Copy(path, save.FileName + "\\Atend.mdb", true);
        //        ed.WriteMessage("Copy Directory\n");
        //        Atend.Control.Common.AccessPath = save.FileName + "\\Atend.mdb";

        //        ed.WriteMessage("airpost\n");
        //        if (Atend.Base.Equipment.EAirPost.AccessInsert())
        //        {
        //            //MessageBox.Show("Transfer Successful");
        //        }
        //        else
        //        {
        //            transferTrue = false;
        //            MessageBox.Show("Transfer Error");
        //        }
        //        //ed.WriteMessage("AutoKey\n");
        //        //if (Atend.Base.Equipment.EAutoKey_3p.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Autokey_3p Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Autokey_3p Error");
        //        //}
        //        ////ed.WriteMessage("Breaker\n");
        //        //if (Atend.Base.Equipment.EBreaker.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Breaker Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Breaker Error");
        //        //}
        //        ////ed.WriteMessage("BreakerType\n");
        //        //if (Atend.Base.Equipment.EBreakerType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer BreakerType Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer BreakerType Error");
        //        //}
        //        ////ed.WriteMessage("Bus\n");
        //        //if (Atend.Base.Equipment.EBus.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Bus Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Bus Error");
        //        //}
        //        ////ed.WriteMessage("BusMaterialType\n");
        //        //if (Atend.Base.Equipment.EBusMaterialType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer BusMaterialType Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer BusMaterialType Error");
        //        //}
        //        ////ed.WriteMessage("BusType\n");

        //        //if (Atend.Base.Equipment.EBusType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer BusType Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer BusType Error");
        //        //}
        //        ////ed.WriteMessage("CabelType\n");
        //        //if (Atend.Base.Equipment.ECabelType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer CabelTypeType Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer CabelType Error");
        //        //}
        //        ////ed.WriteMessage("Catout\n");
        //        //if (Atend.Base.Equipment.ECatOut.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer CatOut Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer CatOut Error");
        //        //}
        //        ////ed.WriteMessage("CatOutType\n");
        //        //if (Atend.Base.Equipment.ECatOutType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer CatOutType Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer CatOutType Error");
        //        //}
        //        ////ed.WriteMessage("conductor\n");
        //        //if (Atend.Base.Equipment.EConductor.AccessInsert())
        //        //{
        //        //    //    MessageBox.Show("Transfer Conductor successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Conductor Error");
        //        //}
        //        ////ed.WriteMessage("ConductorDamperType\n");
        //        //if (Atend.Base.Equipment.EConductorDamperType.AccessInsert())
        //        //{
        //        //    //    MessageBox.Show("Transfer ConductorDamperType successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer ConductorDamperType Error");
        //        //}
        //        ////ed.WriteMessage("ConductorMaterialType\n");
        //        //if (Atend.Base.Equipment.EConductorMaterialType.AccessInsert())
        //        //{
        //        //    //    MessageBox.Show("Transfer ConductorMaterialType successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer ConductorMaterialType Error");
        //        //}
        //        ////ed.WriteMessage("Consol\n");
        //        //if (Atend.Base.Equipment.EConsol.AccessInsert())
        //        //{
        //        //    //    MessageBox.Show("Transfer Consol successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  Consol Error");
        //        //}
        //        ////ed.WriteMessage("ContainerPackage\n");
        //        //if (Atend.Base.Equipment.EContainerPackage.AccessInsert())
        //        //{
        //        //    //    MessageBox.Show("Transfer ContainerPackage successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer containerpackage Error");
        //        //}
        //        ////ed.WriteMessage("Countor\n");

        //        //if (Atend.Base.Equipment.ECountor.AccessInsert())
        //        //{
        //        //    //    MessageBox.Show("Transfer Countor successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Countor Error");
        //        //}
        //        ////ed.WriteMessage("CT\n");
        //        //if (Atend.Base.Equipment.ECT.AccessInsert())
        //        //{
        //        //    //    MessageBox.Show("Transfer CT successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer CT Error");
        //        //}
        //        ////ed.WriteMessage("DB\n");
        //        //if (Atend.Base.Equipment.EDB.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer DB successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer DB Error");
        //        //}
        //        ////ed.WriteMessage("Disconnector\n");
        //        //if (Atend.Base.Equipment.EDisconnector.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Disconnector successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Disconnector Error");
        //        //}
        //        ////ed.WriteMessage("Disconnector Type\n");
        //        //if (Atend.Base.Equipment.EDisconnectorType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer DisconnectorType successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer DisconnectorType Error");
        //        //}
        //        ////ed.WriteMessage("GroundPost\n");
        //        //if (Atend.Base.Equipment.EGroundPost.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer GroundPost successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer GroundPost Error");
        //        //}
        //        ////ed.WriteMessage("GroundPostCell\n");
        //        //if (Atend.Base.Equipment.EGroundPostCell.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer GroundPostCell successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer GroundPostCell Error");
        //        //}
        //        ////ed.WriteMessage("HeaderCabel\n");
        //        //if (Atend.Base.Equipment.EHeaderCabel.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer HeaderCabel successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer HeaderCabel Error");
        //        //}
        //        ////ed.WriteMessage("HeaderCabelMaterial\n");

        //        //if (Atend.Base.Equipment.EHeaderCabelMaterial.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer HeaderCabelMaterial successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer HeaderCabelMaterial Error");
        //        //}
        //        ////ed.WriteMessage("HeaderCabelType\n");
        //        //if (Atend.Base.Equipment.EHeaderCabelType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer HeaderCabelType successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer HeaderCabelType Error");
        //        //}
        //        ////ed.WriteMessage("Insulator\n");
        //        //if (Atend.Base.Equipment.EInsulator.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Insulator successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Insulator Error");
        //        //}
        //        ////ed.WriteMessage("InsulatorType\n");
        //        //if (Atend.Base.Equipment.EInsulatorType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer InsulatorMaterial successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer InsulatorMaterial Error");
        //        //}
        //        ////ed.WriteMessage("JAckPanel\n");
        //        //if (Atend.Base.Equipment.EJAckPanel.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer JackPanel successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;
        //        //    MessageBox.Show("Transfer JackPanel Error");
        //        //}
        //        ////ed.WriteMessage("JackPanelCell\n");
        //        //if (Atend.Base.Equipment.EJackPanelCell.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer JackPanelCell successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer JackPanelCell Error");
        //        //}

        //        //if (Atend.Base.Equipment.EJackPanelWeek.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer JackPanelWeek Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer JackPanelWeek Error");
        //        //}
        //        //if (Atend.Base.Equipment.EJackPanelWeekCell.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer JackPanelWeekCell Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer JackPanelWeekCell Error");
        //        //}

        //        ////ed.WriteMessage("Jumper\n");
        //        //if (Atend.Base.Equipment.EJumper.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Jumper successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Jumper Error");
        //        //}
        //        ////ed.WriteMessage("Khazan\n");
        //        //if (Atend.Base.Equipment.EKhazan.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Khazan successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Khazan Error");
        //        //}
        //        ////ed.WriteMessage("Mafsal\n");
        //        //if (Atend.Base.Equipment.EMafsal.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Mafsal successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Mafsal Error");
        //        //}
        //        ////ed.WriteMessage("MafsalType\n");
        //        //if (Atend.Base.Equipment.EMafsalType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer MafsalType successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer MafsalType Error");
        //        //}
        //        ////ed.WriteMessage("MiddleGroundCabel");
        //        //if (Atend.Base.Equipment.EMiddleGroundCabel.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer MiddleGroundCabel successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer MiddleGroundCabel Error");
        //        //}
        //        //ed.WriteMessage("MiniatorKey\n");
        //        //if (Atend.Base.Equipment.EMiniatorKey.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer MiniatorKey Successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer MiniatorKey Error");
        //        //}
        //        ////ed.WriteMessage("Operation\n");
        //        //if (Atend.Base.Equipment.EOperation.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Operation successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Operation Error");
        //        //}
        //        ////ed.WriteMessage("Package\n");
        //        //if (Atend.Base.Equipment.EPackage.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Package successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Package Error");
        //        //}
        //        ////ed.WriteMessage("PackageAll\n");
        //        //if (Atend.Base.Equipment.EPackageAll.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer PackageAll successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer PackageAll Error");
        //        //}
        //        ////ed.WriteMessage("PhotoCell\n");
        //        //if (Atend.Base.Equipment.EPhotoCell.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer PhotoCell successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer PhotoCell Error");
        //        //}
        //        ////ed.WriteMessage("Phuse\n");
        //        //if (Atend.Base.Equipment.EPhuse.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Phuse successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer Phuse Error");
        //        //}
        //        //ed.WriteMessage("PhuseKey\n");

        //        //if (Atend.Base.Equipment.EPhuseKey.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer PhuseKey successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  PhuseKey Error");
        //        //}
        //        ////ed.WriteMessage("PhusePole\n");
        //        //if (Atend.Base.Equipment.EPhusePole.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer PhusePole successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  PhusePole Error");

        //        //}
        //        ////ed.WriteMessage("PhuseType\n");
        //        //if (Atend.Base.Equipment.EPhuseType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer PhuseType successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  PhuseType Error");
        //        //}
        //        ////ed.WriteMessage("Pole\n");
        //        //if (Atend.Base.Equipment.EPole.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Pole successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  Pole Error");
        //        //}
        //        ////if (Atend.Base.Equipment.EProductPackage.AccessInsert())
        //        ////{
        //        ////    MessageBox.Show("Transfer Post successful");
        //        ////}
        //        ////else
        //        ////{
        //        ////    MessageBox.Show("Transfer  Post Error");
        //        ////}
        //        ////ed.WriteMessage("ProductPackge\n");
        //        //if (Atend.Base.Equipment.EProductPackage.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer ProductPackage successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  ProductPackage Error");
        //        //}
        //        ////ed.WriteMessage("PT\n");
        //        //if (Atend.Base.Equipment.EPT.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer PT successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  PT Error");
        //        //}
        //        ////ed.WriteMessage("RECloser\n");
        //        //if (Atend.Base.Equipment.EReCloser.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer ReCloser successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  ReCloser Error");
        //        //}
        //        ////ed.WriteMessage("ReCloserType\n");
        //        //if (Atend.Base.Equipment.EReCloserType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer ReCloserType successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  ReCloserType Error");
        //        //}
        //        ////ed.WriteMessage("Rod\n");
        //        //if (Atend.Base.Equipment.ERod.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Rod successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  Rod Error");
        //        //}
        //        ////ed.WriteMessage("sectionLizer\n");
        //        //if (Atend.Base.Equipment.ESectionLizer.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer SectionLizer successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  SectionLizer Error");
        //        //}
        //        ////ed.WriteMessage("StreetBox\n");
        //        //if (Atend.Base.Equipment.EStreetBox.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer StreetBox successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  StreetBox Error");
        //        //}

        //        ////ed.WriteMessage("Transformer\n");
        //        //if (Atend.Base.Equipment.ETransformer.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer Transformer successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  Transformer Error");
        //        //}
        //        ////ed.WriteMessage("TransformerType\n");
        //        //if (Atend.Base.Equipment.ETransformerType.AccessInsert())
        //        //{
        //        //    //MessageBox.Show("Transfer TransformerType successful");
        //        //}
        //        //else
        //        //{
        //        //    transferTrue = false;

        //        //    MessageBox.Show("Transfer  TransformerType Error");
        //        //}
        //        if (transferTrue)
        //        {
        //            MessageBox.Show("انتقال اطلاعات به درستی انجام شد");
        //        }
        //        else
        //        {
        //            MessageBox.Show("انتقال اطلاعات به درستی انجام نشد");

        //        }

        //    }
        //}

        //public static void btnProductBindBlock_Click(object sender, EventArgs e)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument();
        //    Atend.Base.frmProdutBindBlock _frmProductBindBlock = new Atend.Base.frmProdutBindBlock();
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmProductBindBlock);
        //    dlock.Dispose();

        //}

        public static void btnPole_Click(object sender, EventArgs e)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {

                if (Atend.Control.Common.Demo)
                {
                    if (Atend.Base.Design.DNode.AccessSelectAll().Rows.Count > 15)
                    {
                        return;
                    }
                }
                Design.frmDrawPole03 frmdrawPole = new Atend.Design.frmDrawPole03();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmdrawPole) == DialogResult.OK)
                {
                    //ed.WriteMessage("DrawPole\n");
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    if (frmdrawPole.shape == 0)
                    {
                        //ed.WriteMessage("Circle\n");
                        doc.SendStringToExecute("_POLECircle ", true, false, false);
                    }
                    if (frmdrawPole.shape == 1)
                    {
                        if (frmdrawPole.Type == 2)//pertic
                        {
                            //ed.WriteMessage("PolyGon\n");
                            doc.SendStringToExecute("_PolePolygon ", true, false, false);
                        }
                        if (frmdrawPole.Type == 3)
                        {
                            //ed.WriteMessage("Pole\n");
                            doc.SendStringToExecute("_POLE ", true, false, false);
                        }
                    }

                }
                //}

            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("اطفاً ابتدا طرح مورد نظر را انتخاب نمایید");
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }//


        }

        public static void btnConsol_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawConsol02 consol = new Atend.Design.frmDrawConsol02();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(consol) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.SendStringToExecute("_CONSOL ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }

            //Atend.Design.frmDrawConsol02 consol = new Atend.Design.frmDrawConsol02();
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(consol);
        }

        public static void btnPoleTip_Click(object sender, EventArgs e)
        {


            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {

                Design.frmDrawPoleTip03 frmdrawPole = new Atend.Design.frmDrawPoleTip03();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmdrawPole) == DialogResult.OK)
                {
                    //ed.WriteMessage("DrawPoleTIP,Shape={0},Type={1}\n", frmdrawPole.shape, frmdrawPole.Type);
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    if (frmdrawPole.shape == 0)
                    {
                        //ed.WriteMessage("CircleTIP\n");
                        doc.SendStringToExecute("_POLECircleTip ", true, false, false);
                    }
                    if (frmdrawPole.shape == 1)
                    {
                        if (frmdrawPole.Type == 2)//pertic
                        {
                            //ed.WriteMessage("PolyGonTIP\n");
                            doc.SendStringToExecute("_PolePolygonTip ", true, false, false);
                        }
                        if (frmdrawPole.Type == 3)
                        {
                            //ed.WriteMessage("Pole\n");
                            doc.SendStringToExecute("_POLETip ", true, false, false);
                        }
                        else
                        {
                            ed.WriteMessage("Pole\n");
                            doc.SendStringToExecute("_POLETip ", true, false, false);

                        }
                    }

                }


            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("اطفاً ابتدا طرح مورد نظر را انتخاب نمایید");
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }//


        }

        public static void btnCounductor_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmDrawBranch01 FrmDrawBranch = new Atend.Design.frmDrawBranch01();
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmDrawBranch) == System.Windows.Forms.DialogResult.OK)
                {
                    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    doc.SendStringToExecute("_CONDUCTOR ", true, false, false);
                }
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        public static void rEditbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode == 0 && Atend.Control.Common.DesignName == "")
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
                //System.Windows.Forms.MessageBox.Show("اطفاً ابتدا طرح مورد نظر را انتخاب نمایید");
                //return;
            }
            else
            {

                PromptEntityOptions entity = new PromptEntityOptions("لظفاً تجهیز مورد نظر را انتخاب نمایید\n");
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                entity.AllowNone = false;

                PromptEntityResult result = ed.GetEntity(entity);
                ed.WriteMessage(result.Status.ToString());
                if (result.Status == PromptStatus.OK)
                {
                    ObjectId idCollect = Atend.Global.Acad.UAcad.GetEntityGroup(result.ObjectId);
                    Atend.Base.Acad.AT_INFO at_Info;
                    if (idCollect != ObjectId.Null)
                    {
                        at_Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(idCollect);
                    }
                    else
                    {
                        at_Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(result.ObjectId);
                    }

                    //if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                    //{
                    //    ed.WriteMessage("I Am In The If \n");
                    //    ed.WriteMessage("at_Info.NodeCode=" + at_Info.NodeCode + "\n");

                    //    Atend.Design.frmEditSingleBranch EditBranch = new Atend.Design. frmEditSingleBranch();
                    //    ed.WriteMessage("aa\n");
                    //    EditBranch.ShowDialog();
                    //}


                    #region Edit Kablsho Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho))
                    {
                        Atend.Design.frmEditDrawKablsho02 EditKablsho = new Atend.Design.frmEditDrawKablsho02();
                        EditKablsho.NodeCode = new Guid(at_Info.NodeCode);
                        EditKablsho.ObjID = result.ObjectId;
                        //ed.WriteMessage("NodeCode={0}\n", at_Info.NodeCode);
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditKablsho) == DialogResult.OK)
                        {
                        }
                    }
                    #endregion

                    #region Edit Clamp Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
                    {
                        Atend.Design.frmEditDrawClamp02 EditClamp = new Atend.Design.frmEditDrawClamp02();
                        EditClamp.NodeCode = new Guid(at_Info.NodeCode);
                        EditClamp.objID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditClamp) == DialogResult.OK)
                        {
                        }
                    }
                    #endregion

                    #region Edit HeaderCabel Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel))
                    {
                        Atend.Design.frmEditDrawHeaderCable02 EditHeaderCabel = new Atend.Design.frmEditDrawHeaderCable02();
                        EditHeaderCabel.NodeCode = new Guid(at_Info.NodeCode);
                        EditHeaderCabel.objID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditHeaderCabel) == DialogResult.OK)
                        {
                        }
                    }
                    #endregion

                    #region Edit HeaderCableInWeekJackPanel Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))
                    {
                        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(result.ObjectId);
                        ObjectIdCollection _Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
                        foreach (ObjectId _collect in _Collection)
                        {
                            Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
                            if (_Info.ParentCode != "NONE" && (_Info.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel && _Info.SelectedObjectId == result.ObjectId))
                            {
                                if (_Info.ProductCode != 0)
                                {
                                    Atend.Design.frmEditDrawHeaderCable02 EditHeaderCabel2 = new Atend.Design.frmEditDrawHeaderCable02();
                                    EditHeaderCabel2.NodeCode = new Guid(_Info.NodeCode);
                                    EditHeaderCabel2.objID = result.ObjectId;
                                    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditHeaderCabel2) == DialogResult.OK)
                                    {
                                    }
                                }
                                else
                                {
                                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                    notification.Title = "خطا";
                                    notification.Msg = "سرکابل فاقد کابل کشی می باشد";
                                    notification.infoCenterBalloon();
                                }
                                break;
                            }
                        }
                    }
                    #endregion

                    #region Edit HeaderCableInTransformer Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))
                    {
                        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(result.ObjectId);
                        ObjectIdCollection _Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
                        foreach (ObjectId _collect in _Collection)
                        {
                            Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
                            if (_Info.ParentCode != "NONE" && (_Info.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel && _Info.SelectedObjectId == result.ObjectId))
                            {
                                if (_Info.ProductCode != 0)
                                {
                                    Atend.Design.frmEditDrawHeaderCable02 EditHeaderCabel2 = new Atend.Design.frmEditDrawHeaderCable02();
                                    EditHeaderCabel2.NodeCode = new Guid(_Info.NodeCode);
                                    EditHeaderCabel2.objID = result.ObjectId;
                                    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditHeaderCabel2) == DialogResult.OK)
                                    {
                                    }
                                }
                                else
                                {
                                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                    notification.Title = "خطا";
                                    notification.Msg = "سرکابل فاقد کابل کشی می باشد";
                                    notification.infoCenterBalloon();
                                }
                                break;
                            }
                        }
                    }
                    #endregion

                    #region Edit HeaderCableInMiddleJackPanel Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel))
                    {
                        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(result.ObjectId);
                        ObjectIdCollection _Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
                        foreach (ObjectId _collect in _Collection)
                        {
                            Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
                            if (_Info.ParentCode != "NONE" && (_Info.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel && _Info.SelectedObjectId == result.ObjectId))
                            {
                                if (_Info.ProductCode != 0)
                                {
                                    Atend.Design.frmEditDrawHeaderCable02 EditHeaderCabel2 = new Atend.Design.frmEditDrawHeaderCable02();
                                    EditHeaderCabel2.NodeCode = new Guid(_Info.NodeCode);
                                    EditHeaderCabel2.objID = result.ObjectId;
                                    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditHeaderCabel2) == DialogResult.OK)
                                    {
                                    }
                                }
                                else
                                {
                                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                    notification.Title = "خطا";
                                    notification.Msg = "سرکابل فاقد کابل کشی می باشد";
                                    notification.infoCenterBalloon();
                                }
                                break;
                            }
                        }
                    }
                    #endregion

                    #region Edit Rod Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Rod))
                    {
                        Atend.Design.frmEditDrawRod02 EditRod = new Atend.Design.frmEditDrawRod02();
                        //EditRod.DpakageCode = new Guid(at_Info.NodeCode);
                        EditRod.NodeCode = new Guid(at_Info.NodeCode);
                        EditRod.ObjID = idCollect;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditRod) == DialogResult.OK)
                        {
                            //Atend.Base.Acad.AT_INFO a = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(result.ObjectId);
                            //Atend.Base.Equipment.ERod rod = Atend.Base.Equipment.ERod.SelectByXCode(EditRod.);
                            //EXTRA
                            //if (a.ParentCode != "NONE")
                            //if (a.ParentCode != String.Empty)
                            //{
                            //    //a.ProductCode = EditRod.XCode;
                            //    a.Insert();
                            //    ed.WriteMessage("result.ObjectID={0},Rod.Comment={1}\n", result.ObjectId, rod.Comment);
                            //    Atend.Global.Acad.UAcad.ChangeMText(result.ObjectId, rod.Comment);
                            //}
                        }

                    }
                    #endregion

                    #region Edit GroundPost Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost))
                    {
                        Atend.Design.frmEditDrawGroundPost02 EditGroundPost = new Atend.Design.frmEditDrawGroundPost02();
                        EditGroundPost.NodeCode = new Guid(at_Info.NodeCode);
                        EditGroundPost.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditGroundPost) == DialogResult.OK)
                        {
                            //Atend.Base.Acad.AcadGlobal.GroundPostData._AcDrawGroundPost = EditGroundPost.GroundPost;
                            //Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                            //doc.SendStringToExecute("_GroundPostUpdate ", true, false, false);

                            //if (EditGroundPost.GroundPost.UpdateGroundPostData(new Guid(at_Info.NodeCode)))
                            //{
                            //    EditGroundPost.GroundPost.DrawGroundPost();
                            //}
                            //else
                            //{
                            //    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                            //    notification.Title = "خطا";
                            //    notification.Msg = "خطا در ویرایش پست زمینی";
                            //    notification.ShowStatusBarBalloon();
                            //}
                        }
                    }
                    #endregion

                    #region Edit AirPost Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost))
                    {
                        Atend.Design.frmEditDrawAirPost02 EditAirPost = new Atend.Design.frmEditDrawAirPost02();
                        EditAirPost.NodeCode = new Guid(at_Info.NodeCode);
                        EditAirPost.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditAirPost) == DialogResult.OK)
                        {
                            //if (EditAirPost.AirPost.UpdateAirPostData(new Guid(at_Info.NodeCode)))
                            //{
                            //    EditAirPost.AirPost.DrawAirPost();
                            //}
                            //else
                            //{
                            //    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                            //    notification.Title = "خطا";
                            //    notification.Msg = "خطا در ویرایش پست هوایی";
                            //    notification.ShowStatusBarBalloon();
                            //}
                        }
                    }
                    #endregion

                    #region Edit Breaker Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker))
                    {
                        Atend.Design.frmEditDrawBreaker02 EditDrawBreaker = new Atend.Design.frmEditDrawBreaker02();
                        EditDrawBreaker.NodeCode = new Guid(at_Info.NodeCode);
                        EditDrawBreaker.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditDrawBreaker) == DialogResult.OK)
                        {
                        }

                    }
                    #endregion

                    #region Edit CatOut Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut))
                    {
                        Atend.Design.frmEditDrawCatout03 EditCatOut = new Atend.Design.frmEditDrawCatout03();
                        EditCatOut.NodeCode = new Guid(at_Info.NodeCode);
                        EditCatOut.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditCatOut) == DialogResult.OK)
                        {
                        }

                    }
                    #endregion

                    #region Edit DB Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.DB))
                    {
                        Atend.Design.frmEditDrawDB03 EditDB = new Atend.Design.frmEditDrawDB03();
                        EditDB.NodeCode = new Guid(at_Info.NodeCode);
                        EditDB.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditDB) == DialogResult.OK)
                        {
                        }

                    }
                    #endregion

                    #region Edit MeasuredJackPanel Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel))
                    {
                        Atend.Design.frmEditDrawMeasuredJackPanel02 EditDrawMeasuredJackPanel = new Atend.Design.frmEditDrawMeasuredJackPanel02();
                        EditDrawMeasuredJackPanel.NodeCode = new Guid(at_Info.NodeCode);
                        EditDrawMeasuredJackPanel.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditDrawMeasuredJackPanel) == DialogResult.OK)
                        {
                        }
                    }
                    #endregion

                    #region Edit Light Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Light))
                    {
                        Atend.Design.frmEditDrawLight02 EditDrawLight = new Atend.Design.frmEditDrawLight02();
                        EditDrawLight.NodeCode = new Guid(at_Info.NodeCode);
                        EditDrawLight.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditDrawLight) == DialogResult.OK)
                        {
                        }
                    }
                    #endregion

                    #region Edit Ground Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Ground))
                    {
                        Atend.Design.frmEditDrawGround02 EditDrawGround = new Atend.Design.frmEditDrawGround02();
                        EditDrawGround.NodeCode = new Guid(at_Info.NodeCode);
                        EditDrawGround.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditDrawGround) == DialogResult.OK)
                        {
                        }
                    }
                    #endregion

                    #region Edit DisConnector Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector))
                    {
                        Atend.Design.frmEditDrawDisconnector02 EditDisconnector = new Atend.Design.frmEditDrawDisconnector02();
                        EditDisconnector.NodeCode = new Guid(at_Info.NodeCode);
                        EditDisconnector.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditDisconnector) == DialogResult.OK)
                        {
                        }

                    }
                    #endregion

                    #region Edit StreetBox Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox))
                    {
                        Atend.Design.frmEditDrawStreetBox02 EditStreetBox = new Atend.Design.frmEditDrawStreetBox02();
                        EditStreetBox.NodeCode = new Guid(at_Info.NodeCode);
                        EditStreetBox.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditStreetBox) == DialogResult.OK)
                        {
                        }

                    }
                    #endregion

                    #region Edit SelfKeeper Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
                    {
                        ed.WriteMessage("I Am In selfKeeper If \n");
                        ed.WriteMessage("at_Info.NodeCode=" + at_Info.NodeCode + "\n");

                        //Atend.Design.frmEditDrawBranch EditBranch = new Atend.Design.frmEditDrawBranch(at_Info);
                        //ed.WriteMessage("aa\n");
                        //EditBranch.ShowDialog();
                        Atend.Design.frmEditDrawSelfKeeper EditSK = new Atend.Design.frmEditDrawSelfKeeper();
                        ed.WriteMessage("aa\n");
                        EditSK.SKTCode = at_Info.ProductCode;
                        EditSK.BranchCode = new Guid(at_Info.NodeCode);
                        EditSK.BranchObj = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditSK) == DialogResult.OK)
                        {
                            Atend.Base.Acad.AT_INFO a = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(result.ObjectId);
                            //EXTRA
                            //if (a.ParentCode != "NONE")
                            if (a.ParentCode != String.Empty)
                            {
                                a.ProductCode = EditSK.SKTCode;
                                a.Insert();
                            }

                        }

                    }
                    #endregion

                    #region Edit Cabel Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel))
                    {
                        ed.WriteMessage("I Am In GroundCable If \n");
                        ed.WriteMessage("at_Info.NodeCode= " + at_Info.NodeCode + "\n");

                        //Atend.Design.frmEditDrawBranch EditBranch = new Atend.Design.frmEditDrawBranch(at_Info);
                        //ed.WriteMessage("aa\n");
                        //EditBranch.ShowDialog();
                        Atend.Design.frmEditDrawGroundCable02 EditGC = new Atend.Design.frmEditDrawGroundCable02();
                        ed.WriteMessage("aa\n");
                        EditGC.GCCode = at_Info.ProductCode;
                        EditGC.BranchCode = new Guid(at_Info.NodeCode);
                        EditGC.BranchObj = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditGC) == DialogResult.OK)
                        {
                            //Atend.Base.Acad.AT_INFO a = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(result.ObjectId);
                            //if (a.ParentCode != "NONE")
                            //{
                            //    a.ProductCode = EditGC.GCCode;
                            //    a.Insert();
                            //}

                        }

                    }
                    #endregion

                    #region Edit BankKhazan Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan))
                    {
                        ed.WriteMessage("I Am In Bank Khazan If \n");
                        //ed.WriteMessage("at_Info.NodeCode=" + at_Info.NodeCode + "\n");

                        Atend.Design.frmEditDrawKhazan02 EditKhazan = new Atend.Design.frmEditDrawKhazan02();
                        //ed.WriteMessage("aa\n");
                        //EditKhazan.Code = at_Info.ProductCode;
                        EditKhazan.NodeCode = new Guid(at_Info.NodeCode);
                        EditKhazan.ObjID = idCollect;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditKhazan) == DialogResult.OK)
                        {
                            //Atend.Base.Acad.AT_INFO a = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(result.ObjectId);
                            //if (a.ParentCode != "NONE")
                            //{
                            //    a.ProductCode = EditKhazan.pro;
                            //    a.Insert();
                            //}

                        }

                    }
                    #endregion

                    #region Edit Khazan Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan))
                    {
                        ed.WriteMessage("I Am In Khazan If \n");
                        Atend.Design.frmEditDrawKhazan02 EditKhazan = new Atend.Design.frmEditDrawKhazan02();
                        //EditKhazan.Code = at_Info.ProductCode;
                        EditKhazan.NodeCode = new Guid(at_Info.NodeCode);
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditKhazan) == DialogResult.OK)
                        {
                            //Atend.Base.Acad.AT_INFO a = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(result.ObjectId);
                            //if (a.ParentCode != "NONE")
                            //{
                            //    //a.ProductCode = EditKhazan.Code;
                            //    a.ProductCode = at_Info.ProductCode;
                            //    a.Insert();
                            //}

                        }

                    }
                    #endregion

                    #region Edit Pole Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Pole))
                    {
                        //ed.WriteMessage("I Am In Pole If \n");
                        //ed.WriteMessage("at_Info.NodeCode=" + at_Info.NodeCode + "\n");
                        Atend.Design.frmEditDrawPole02 EditPole = new Atend.Design.frmEditDrawPole02();
                        EditPole.NodeCode = new Guid(at_Info.NodeCode);
                        EditPole.ObjID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditPole) == DialogResult.OK)
                        {
                            //Atend.Global.Acad.AcadUpdate acadUpdate = new Atend.Global.Acad.AcadUpdate();
                            //acadUpdate.UpdatePole(result.ObjectId);

                        }

                    }
                    #endregion

                    #region Edit PoleTip Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip))
                    {
                        //ed.WriteMessage("I Am In Pole If \n");
                        //ed.WriteMessage("at_Info.NodeCode=" + at_Info.NodeCode + "\n");
                        Atend.Design.frmEditDrawPoleTip02 EditPoleTip = new Atend.Design.frmEditDrawPoleTip02();
                        EditPoleTip.NodeCode = new Guid(at_Info.NodeCode);
                        EditPoleTip.objID = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditPoleTip) == DialogResult.OK)
                        {
                            //Atend.Global.Acad.AcadUpdate acadUpdate = new Atend.Global.Acad.AcadUpdate();
                            //acadUpdate.UpdatePole(result.ObjectId);

                        }

                    }
                    #endregion

                    #region Edit Conductor Information
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                    {
                        //ed.WriteMessage("I Am In Conductor If \n");
                        //ed.WriteMessage("at_Info.NodeCode=" + at_Info.NodeCode + "\n");

                        Atend.Design.frmEditSingleBranch02 EditBranch = new Atend.Design.frmEditSingleBranch02();
                        //ed.WriteMessage("aa\n");
                        EditBranch.NodeCode = new Guid(at_Info.NodeCode);
                        EditBranch.obj = result.ObjectId;
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditBranch) == DialogResult.OK)
                        {
                            //Atend.Base.Acad.AT_INFO atInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(result.ObjectId);
                            //if (a.ParentCode != "NONE")
                            //{
                            //    a.ProductCode = EditBranch.Code;
                            //    a.Insert();
                            //}

                        }

                    }
                    #endregion

                    #region Consol
                    if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                    {
                        Atend.Design.frmEditConsol EditConsol = new Atend.Design.frmEditConsol();
                        EditConsol.NodeCode = new Guid(at_Info.NodeCode);
                        EditConsol.obj = at_Info.SelectedObjectId;
                        ed.WriteMessage("NodeCode={0}\n", at_Info.NodeCode.ToString());
                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(EditConsol) == DialogResult.OK)
                        {
                        }
                    }


                    #endregion

                    //if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost))
                    //{
                    //    ed.WriteMessage("I Am In GroundPost If \n");
                    //    ed.WriteMessage("at_Info.NodeCode=" + at_Info.NodeCode + "\n");

                    //    //Atend.Design.frmEditDrawBranch EditBranch = new Atend.Design.frmEditDrawBranch(at_Info);
                    //    //ed.WriteMessage("aa\n");
                    //    //EditBranch.ShowDialog();

                    //    Atend.Design.frmEditDrawGroundPost EditGP = new Atend.Design.frmEditDrawGroundPost();
                    //    ed.WriteMessage("aa\n");
                    //    EditGP.SelectedPostCode = at_Info.ProductCode;
                    //    EditGP.DPCode = new Guid(at_Info.NodeCode);

                    //    if (EditGP.ShowDialog() == DialogResult.OK)
                    //    {
                    //        Atend.Base.Acad.AT_INFO a = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(result.ObjectId);
                    //        if (a.ParentCode != "NONE")
                    //        {
                    //            a.ProductCode = EditGP.SelectedPostCode;
                    //            a.Insert();
                    //        }

                    //    }

                    //}

                    ////////////#region Edit GroundPost Information
                    ////////////if (at_Info.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost))
                    ////////////{
                    ////////////    Atend.Design.frmEditDrawGroundPost01 groundPost = new Atend.Design.frmEditDrawGroundPost01();
                    ////////////    Acad.AcadCommands command = new AcadCommands();
                    ////////////    groundPost.GPCode = new Guid(at_Info.NodeCode);
                    ////////////    groundPost.PostCode = at_Info.ProductCode;

                    ////////////    if (groundPost.ShowDialog() == DialogResult.OK)
                    ////////////    {
                    ////////////        //bool Answ=Atend.Global.Acad.AcadRemove.DeleteGroundPost(result.ObjectId);
                    ////////////        //ed.WriteMessage("Delete :{0}\n", Answ);
                    ////////////        if (Atend.Global.Acad.AcadRemove.DeleteGroundPost(result.ObjectId))
                    ////////////        {

                    ////////////            Entity ent = command.DrawGroundPost(groundPost.GroundPostProductCode);
                    ////////////            if (ent != null)
                    ////////////            {
                    ////////////                //ed.WriteMessage("=====================>GroundPostWasDrawn\n");
                    ////////////                //ed.WriteMessage("WeekJackPanel.Count:{0}\n", groundPost.arweekJackPanel.Count);
                    ////////////                for (int i = 0; i < groundPost.WeekJackPAnelCount; i++)
                    ////////////                {
                    ////////////                    Atend.Base.Equipment.EJackPanelWeek jackPAnelWeek = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(Convert.ToInt32(groundPost.arweekJackPanel[i]));
                    ////////////                    //ed.WriteMessage("=====================>Go For Draw JackPanelWeek\n");
                    ////////////                    command.DrawWeekJackPanel(ent, jackPAnelWeek.FeederCount, Convert.ToInt32(groundPost.arweekJackPanel[i]));
                    ////////////                }
                    ////////////                //ed.WriteMessage("Transformer.Count:{0}\n", groundPost.transformerCount);
                    ////////////                for (int i = 0; i < groundPost.transformerCount; i++)
                    ////////////                {
                    ////////////                    //ed.WriteMessage("=====================>Go For Draw Transformer\n");
                    ////////////                    command.DrawTransformer(ent, Convert.ToInt32(groundPost.arTransformer[i]));
                    ////////////                }


                    ////////////                //foreach (Atend.Base.Acad.AcadGlobal.PostEquips p in Atend.Base.Acad.AcadGlobal.PostEquipInserted)
                    ////////////                //{
                    ////////////                //ed.WriteMessage("Parent:{0} , ProductCode:{1} , ProductType:{2} , Code:{3} \n", p.ParentCode, p.ProductCode, p.ProductType, p.CodeGuid);
                    ////////////                //ed.WriteMessage("-------------------------------------------------------\n");
                    ////////////                //}

                    ////////////                //ed.WriteMessage("MiddleJackPanel.Count:{0}\n", groundPost.arMiddleJAckPAnel.Count);
                    ////////////                for (int i = 0; i < groundPost.MiddlejackPanelCount; i++)
                    ////////////                {
                    ////////////                    System.Data.DataTable Cells = Atend.Base.Equipment.EJackPanelCell.SelectByJackPanelCode(Convert.ToInt32(groundPost.arMiddleJAckPAnel[i]));

                    ////////////                    Atend.Base.Acad.AcadGlobal.MiddleJackPanelData.eJackPanelCells.Clear();
                    ////////////                    foreach (DataRow dr in Cells.Rows)
                    ////////////                    {
                    ////////////                        Atend.Base.Equipment.EJackPanelCell Cell = new Atend.Base.Equipment.EJackPanelCell();
                    ////////////                        Cell.Code = Convert.ToInt32(dr["Code"]);
                    ////////////                        Cell.JackPanelCode = Convert.ToInt32(dr["jackPanelCode"]);
                    ////////////                        Cell.Num = Convert.ToByte(dr["CellNum"]);
                    ////////////                        Cell.ProductCode = Convert.ToInt32(dr["ProductCode"]);
                    ////////////                        Cell.ProductType = Convert.ToByte(dr["ProductType"]);
                    ////////////                        Atend.Base.Acad.AcadGlobal.MiddleJackPanelData.eJackPanelCells.Add(Cell);

                    ////////////                    }

                    ////////////                    //ed.WriteMessage("=====================>Go For Draw MiddleJackPanel with cell count {0}\n", Atend.Base.Acad.AcadGlobal.eJackPanelCells.Count);
                    ////////////                    if (Atend.Base.Acad.AcadGlobal.MiddleJackPanelData.eJackPanelCells.Count != 0)
                    ////////////                    {
                    ////////////                        //ed.WriteMessage("Middle jack panel productcode:{0}\n", Convert.ToInt32(groundPost.arMiddleJAckPAnel[i]));
                    ////////////                        command.DrawMiddleJackPanel(ent, Convert.ToInt32(groundPost.arMiddleJAckPAnel[i]));
                    ////////////                    }
                    ////////////                }

                    ////////////                //Atend.Base.Acad.AT_INFO a = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(result.ObjectId);
                    ////////////                //ed.WriteMessage("\nEDIT POST11    " + groundPost.PostCode.ToString() + "\n");
                    ////////////                //if (a.ParentCode != "NONE")
                    ////////////                //{
                    ////////////                //    ed.WriteMessage("\nEDIT POST    " + groundPost.PostCode.ToString() + "\n");
                    ////////////                //    a.ProductCode = groundPost.PostCode;
                    ////////////                //    a.Insert();
                    ////////////                //}

                    ////////////            }
                    ////////////            //____________________________________________________
                    ////////////        }
                    ////////////    }
                    ////////////}
                    ////////////#endregion




                }
                else
                {
                    ed.WriteMessage("Entity not found \n");
                }
            }
        }

        public static void rEditbuttonForConductor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmEditDrawBranch01 editBranch = new Atend.Design.frmEditDrawBranch01();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(editBranch);
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        //public static void btnLayer_Click(object sender, EventArgs e)
        //{
        //    if (Atend.Control.Common.userCode != 0)
        //    {

        //        //Atend.Design.frmLayer02 Frmlayer = new Atend.Design.frmLayer();
        //        //Frmlayer.ShowDialog();

        //        //Atend.Acad.AcadLayer.Insert();
        //    }
        //    else
        //    {
        //        Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
        //        notification.Title = "انتخاب طرح مورد نظر";
        //        //notification.Hlink = "HLINK";
        //        //notification.Htext = "HTEXT";
        //        notification.Msg = "لطفا طرح مورد نظر خود را انتخاب نمایید";
        //        //notification.Msg2 = "MESSAGE2";

        //        notification.ShowStatusBarBalloon();
        //    }
        //}

        public static void btnWeatherCondition_Click(object sender, EventArgs e)
        {
            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                Atend.Design.frmWeather FrmWeather = new Atend.Design.frmWeather();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmWeather);
            }
            else
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "انتخاب طرح";
                notification.Msg = "لطفاً ابتدا طرح مورد نظر را انتخاب نمایید";
                notification.ShowStatusBarBalloon();
                return;
            }
        }

        public static void rConductorbuttonWeek_Click(object sender, EventArgs e)
        {
        }

    }
}