using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Xml;
using System.Collections;

namespace Atend.Design
{
    public partial class frmWeather : Form
    {
        bool ForceToClose = false;


        public frmWeather()
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

        private void frmWeather_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
            {
                gvWeather.AutoGenerateColumns = false;

                string[] Type ={ "فوق سنگین", "سنگین", "متوسط", "سبک" };
                cboweatherType.DisplayMember = "Name";
                cboweatherType.ValueMember = "Code";
                //DataTable dtWeatherType = Atend.Base.Design.DWeatherType.SelectByDesignCode(Atend.Control.Common.SelectedDesignCode);
                DataTable dtWeatherType = Atend.Base.Design.DWeatherType.AccessSelectAll();

                if (dtWeatherType.Rows.Count != 0)
                {

                    cboweatherType.DataSource = dtWeatherType;
                    foreach (DataRow dr in dtWeatherType.Rows)
                    {
                        if (Convert.ToBoolean(dr["IsSelected"].ToString()) == true)
                        {
                            cboweatherType.SelectedValue = Convert.ToInt32(dr["Code"].ToString());
                        }
                    }
                }
                else
                {
                    //ed.WriteMessage("Build WeatherType\n");
                    Atend.Base.Design.DWeatherType weatherType = new Atend.Base.Design.DWeatherType();
                    for (int i = 0; i < Type.Length; i++)
                    {
                        //***Extra
                        //weatherType.DesignCode = Atend.Control.Common.SelectedDesignCode;
                        if (i == 2)
                        {
                            weatherType.IsSelected = true;//true
                        }
                        else
                        {
                            weatherType.IsSelected = false;//false
                        }
                        weatherType.Name = Type[i];
                        weatherType.Accessinsert();

                    }
                    //DataTable dt = Atend.Base.Design.DWeatherType.AccessSelectAll();

                    //ed.WriteMessage("Rows.count={0},NAme={1},Code={2}\n", dt.Rows.Count, dt.Rows[0]["Name"].ToString(),dt.Rows[0]["Code"].ToString());
                    cboweatherType.DataSource = Atend.Base.Design.DWeatherType.AccessSelectAll();

                }
                //ed.WriteMessage("SelectWeather\n");
                DataTable selectweather = Atend.Base.Design.DWeather.AccessSelectByType(Convert.ToInt32(cboweatherType.SelectedValue));
                //ed.WriteMessage("ASfterSelectWeather\n");

                if (selectweather.Rows.Count == 0)
                {
                    //ed.WriteMessage("A\n");
                    Atend.Base.Design.DWeather dWeather = new Atend.Base.Design.DWeather();
                    //ed.WriteMessage("AA\n");

                    DataTable dtWeatherCondition = Atend.Base.Design.DWeatherConditionType.AccessSelectAll();


                    foreach (DataRow dr in dtWeatherCondition.Rows)
                    {
                        for (int i = 0; i < cboweatherType.Items.Count; i++)
                        {
                            cboweatherType.SelectedIndex = i;
                            #region FogheSangin
                            if (i == 0)
                            {
                                switch (Convert.ToInt32(dr["Code"]))
                                {
                                    case 1:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code1:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 10;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 18;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 2:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code2:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 30;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -5;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 3:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 15;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 40;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 4:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 5;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 35;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 5:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -30;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 6:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 35;
                                            ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                }
                            }
                            #endregion
                            #region sangin
                            if (i == 1)
                            {
                                switch (Convert.ToInt32(dr["Code"]))
                                {
                                    case 1:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 10;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 18;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 2:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 20;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -5;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 3:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 15;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 40;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 4:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 5;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 40;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 5:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -25;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 6:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 15;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -20;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 20;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                }
                            }
                            #endregion
                            #region Motevaset
                            if (i == 2)
                            {
                                switch (Convert.ToInt32(dr["Code"]))
                                {
                                    case 1:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 10;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 20;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 2:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 15;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -5;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 3:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 15;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 40;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 4:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 5;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 45;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 5:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -20;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 6:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 7;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -10;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 25;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                }
                            }
                            #endregion
                            #region sabok
                            if (i == 3)
                            {
                                switch (Convert.ToInt32(dr["Code"]))
                                {
                                    case 1:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 10;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 25;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 2:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 6;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -5;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 22;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 3:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 0;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 45;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 4:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 5;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 50;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 5:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = -5;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 0;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                    case 6:
                                        {
                                            dWeather.ConditionCode = Convert.ToByte(dr["Code"].ToString());
                                            //ed.WriteMessage("Code:" + dr["Code"].ToString() + "\n");
                                            dWeather.IceDiagonal = 0;
                                            //ed.WriteMessage("dWeather.IceDiagonal " + "\n");
                                            dWeather.SaftyFactor = 3;
                                            //ed.WriteMessage("dWeather.SaftyFactor " + "\n");

                                            dWeather.Temp = 15;
                                            //ed.WriteMessage("dWeather.Temp " + "\n");
                                            //ed.WriteMessage("CBO:" + cboweatherType.ValueMember[i].ToString() + "\n");
                                            dWeather.TypeCode = Convert.ToInt32(cboweatherType.SelectedValue.ToString());
                                            dWeather.WindSpeed = 28;
                                            dWeather.AccessInsert();
                                            break;
                                        }
                                }
                            }
                            #endregion

                        }
                    }
                    ed.WriteMessage("ENDFOr");
                    cboweatherType.SelectedValue = Atend.Base.Design.DWeatherType.AccessSelectBySelectedStatus(true).Code;
                    gvWeather.DataSource = Atend.Base.Design.DWeather.AccessSelectByType(Convert.ToInt32(cboweatherType.SelectedValue.ToString()));
                }
                else
                {
                    ed.WriteMessage("Else={0}\n", cboweatherType.SelectedValue.ToString());
                    cboweatherType.SelectedValue = Atend.Base.Design.DWeatherType.AccessSelectBySelectedStatus(true).Code;
                    gvWeather.DataSource = Atend.Base.Design.DWeather.AccessSelectByType(Convert.ToInt32(cboweatherType.SelectedValue.ToString()));
                }
            }
            else
            {
                MessageBox.Show("لطفا طرح مورد نظر را انتخاب کنید");
                this.Close();
            }


        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboweatherType_Click(object sender, EventArgs e)
        {

        }

        //private void cboweatherType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    //ed.WriteMessage("SelectedValue" + cboweatherType.SelectedValue.ToString()+"\n");
        //    gvWeather.Rows.Clear();
        //                    gvWeather.DataSource = Atend.Base.Design.DWeather.AccessSelectByType(Convert.ToInt32(cboweatherType.SelectedValue.ToString()));

        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click_1(object sender, EventArgs e)
        {
            if (validation())
            {
                save();
            }
        }
        private void save()
        {
            gvWeather.Focus();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("EnterSave"+"\n");
            Atend.Base.Design.DWeather weather = new Atend.Base.Design.DWeather();
            for (int i = 0; i < gvWeather.Rows.Count; i++)
            {
                //ed.WriteMessage("i: "+i.ToString() + "\n");
                //ed.WriteMessage("Cells[0]:" + gvWeather.Rows[i].Cells[0].Value.ToString() + "\n");
                weather.Code = Convert.ToInt32(gvWeather.Rows[i].Cells[0].Value.ToString());
                weather.Temp = Convert.ToDouble(gvWeather.Rows[i].Cells["Column3"].Value.ToString());
                weather.WindSpeed = Convert.ToDouble(gvWeather.Rows[i].Cells["Column5"].Value.ToString());
                weather.IceDiagonal = Convert.ToDouble(gvWeather.Rows[i].Cells["Column4"].Value.ToString());
                weather.SaftyFactor = Convert.ToDouble(gvWeather.Rows[i].Cells["Column6"].Value.ToString()); ;

                //
                if (weather.AccessUpdate())
                {
                    ed.WriteMessage("اطلاعات به درستی به روز رسانی شد");
                }
                else
                {
                    //ed.WriteMessage("اطلاعات به درستی به روز رسانی نشد");
                }

            }
        }
        private bool validation()
        {
            //MessageBox.Show("A");
            for (int i = 0; i < gvWeather.Rows.Count; i++)
            {
                if (!Atend.Control.NumericValidation.DoubleConverter(gvWeather.Rows[i].Cells["Column3"].Value.ToString()))
                {
                    MessageBox.Show("لطفا دما را با فرمت مناسب وارد نمایید", "خطا");
                    gvWeather.Focus();
                    return false;
                }

                if (!Atend.Control.NumericValidation.DoubleConverter(gvWeather.Rows[i].Cells["Column4"].Value.ToString()))
                {
                    MessageBox.Show("لطفا قطر یخ را با فرمت مناسب وارد نمایید", "خطا");
                    gvWeather.Focus();
                    return false;
                }

                if (!Atend.Control.NumericValidation.DoubleConverter(gvWeather.Rows[i].Cells["Column5"].Value.ToString()))
                {
                    MessageBox.Show("لطفا سرعت باد را با فرمت مناسب وارد نمایید", "خطا");
                    gvWeather.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(gvWeather.Rows[i].Cells["Column6"].Value.ToString()))
                {
                    MessageBox.Show("لطفا ضریب اطمینان را با فرمت مناسب وارد نمایید", "خطا");
                    gvWeather.Focus();
                    return false;
                }

            }
            return true;
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboweatherType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            gvWeather.AutoGenerateColumns = false;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("SelectedValue" + cboweatherType.SelectedValue.ToString() + "\n");
            //gvWeather.Rows.Clear();
            gvWeather.DataSource = Atend.Base.Design.DWeather.AccessSelectByType(Convert.ToInt32(cboweatherType.SelectedValue.ToString()));
            Atend.Base.Design.DWeatherType weatherType = Atend.Base.Design.DWeatherType.AccessSelectByCode(
                Convert.ToInt32(cboweatherType.SelectedValue));
            chkSelected.Checked = weatherType.IsSelected;
            //ed.WriteMessage("^^^^^****weatherType.IsSelected={0}\n",weatherType.IsSelected);
        }

        private void gvWeather_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("before validate");
            if (validation())
            {
                //ed.WriteMessage("before save");
                save();
            }
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                save();
            }

        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                save();
                Close();
            }
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkSelected_CheckedChanged(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Current chk status is " + chkSelected.Checked.ToString() + "\n");
            //ed.WriteMessage("Current slectedvalue is " + cboweatherType.SelectedValue.ToString() + "\n");
            //ed.WriteMessage("@@@cboweatherType.SelectedValue={0}\n", cboweatherType.SelectedValue);
            if (chkSelected.Checked)
            {
                Atend.Base.Design.DWeatherType.AccessUpdateSelectedStatus(-1,
                    0);



            }
            int check = 0;
            if (chkSelected.Checked)
            {
                check = 1;
            }
            else
            {
                check = 0;
            }
            Atend.Base.Design.DWeatherType.AccessUpdateSelectedStatus(
                    Convert.ToInt32(cboweatherType.SelectedValue), check);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                save();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}