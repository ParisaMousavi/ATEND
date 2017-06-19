using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

using System.Collections;
using System.Data.OleDb;

namespace Atend.Calculating
{
    public partial class frmLoad : Form
    {


        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        double Amper = 0, Voltage = 0;
        //ArrayList ConsolGuidList = new ArrayList();
        int SelectedLoadCode;
        int Mode;
        double P = 0, Q = 0, PF = 0;
        string p1, q1, pf1;
        bool fp = false, fq1 = false, fq2 = false, fpf = false;
        bool ForceToClose = false;
        int Countc;
        Guid nodeCode;
        public frmLoad()
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

        private void chkP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkP.Checked)
            {
                gbP.Enabled = true;
            }
            else
            {
                gbP.Enabled = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void chkI_CheckedChanged(object sender, EventArgs e)
        {
            if (chkI.Checked)
            {
                gbI.Enabled = true;
            }
            else
            {
                gbI.Enabled = false;
            }
        }

        private void ChkJ_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkJ.Checked)
            {
                gb.Enabled = true;
            }
            else
            {
                gb.Enabled = false;
            }
        }

        private void chkZ_CheckedChanged(object sender, EventArgs e)
        {
            if (chkZ.Checked)
            {
                gbZ.Enabled = true;
            }
            else
            {
                gbZ.Enabled = false;
            }
        }
        public void BindDataToList()
        {
            lstJ.DisplayMember = "Name";
            lstJ.ValueMember = "Code";
            lstJ.DataSource = Atend.Base.Calculating.CDloadFactor.AccessSelectAll();
        }


        public void BindDataToForm()
        {
            Guid Code = nodeCode;
            Atend.Base.Design.DPackage PAckage = Atend.Base.Design.DPackage.AccessSelectByCode( Code );
            ed.WriteMessage("***"+PAckage.Code.ToString() + "    " + PAckage.LoadCode.ToString() + "\n");
            
            Atend.Base.Calculating.CLoadFactor Load = Atend.Base.Calculating.CLoadFactor.AccessSelectByCode(PAckage.LoadCode);
            ed.WriteMessage("Load.ModeNumber={0}\n",Load.ModeNumber);
            if (Load.Code == 0)
                return;

            Reset();

            
            //if (Load.Q != 0)
            //{
            //    txtq.Text = Load.Q.ToString();
            //    //txtQ1.Text = Load.Q.ToString();
            //}
            //if (Load.P != 0)
            //{
            //    txtP.Text = Load.P.ToString();
            //    txtP1.Text = Load.P.ToString();
            //}
            //if (Load.PF != 0)
            //{
            //    txtPF1.Text = Load.PF.ToString();
            //    txtPF2.Text = Load.PF.ToString();
            //}
            ed.WriteMessage("GoToSet\n");
            SelectedLoadCode = Load.Code;

            if (Load.ModeNumber.IndexOf('1') != -1)
            {
                cboMode.SelectedIndex = 0;
                txtP.Text = Load.P.ToString();
                txtQ1.Text = Load.Q.ToString();
                txtQ2.Text = Load.Q.ToString();
                txtPF.Text = Load.PF.ToString();
            }



            if (Load.I2 != 0)
                txtI2.Text = Load.I2.ToString();
            if (Load.PF2 != 0)
                txtPF3.Text = Load.PF2.ToString();
            if (Load.R != 0)
                txtR.Text = Load.R.ToString();
            if (Load.X != 0)
                txtX.Text = Load.X.ToString();


            //ed.WriteMessage(" in bind to form \n");


            SetObjects(Load.ModeNumber);


            if (Load.TypeCode == 1)
                rdoP.Checked = true;

            if (Load.TypeCode == 2)
                rdoI.Checked = true;

            if (Load.TypeCode == 3)
                rdoZ.Checked = true;

            if (Load.ModeNumber.IndexOf('4') != -1)
            {
                DataTable LoadPDT = Atend.Base.Calculating.CPackageLoad.AccessSelectByLoadFactorCode(Load.Code);

                for (int i = 0; i < LoadPDT.Rows.Count; i++)
                {
                    //ed.WriteMessage(LoadPDT.Rows[i][2].ToString());
                    Atend.Base.Calculating.CDloadFactor DL = Atend.Base.Calculating.CDloadFactor.AccessSelectByCode(Convert.ToInt32(LoadPDT.Rows[i][2].ToString()));
                    gvJ.Rows.Add();
                    gvJ.Rows[gvJ.Rows.Count - 1].Cells[0].Value = DL.Code;
                    gvJ.Rows[gvJ.Rows.Count - 1].Cells[1].Value = DL.Name;
                    gvJ.Rows[gvJ.Rows.Count - 1].Cells[2].Value = DL.Amper;
                    gvJ.Rows[gvJ.Rows.Count - 1].Cells[3].Value = DL.PhaseCount;
                    gvJ.Rows[gvJ.Rows.Count - 1].Cells[4].Value = DL.FactorPower;
                    gvJ.Rows[gvJ.Rows.Count - 1].Cells[5].Value = DL.FactorConcurency;
                    gvJ.Rows[gvJ.Rows.Count - 1].Cells[6].Value = DL.Voltage;
                    gvJ.Rows[gvJ.Rows.Count - 1].Cells[7].Value = LoadPDT.Rows[i][3];

                }

            }
            if(SelectedLoadCode != 0)
                toolStripLabel3.Enabled = true;
            else
                toolStripLabel3.Enabled = false;


            //if (!(string.IsNullOrEmpty(txtP.Text)) && !(string.IsNullOrEmpty(txtQ1.Text)))
            //{
            //    if (Convert.ToDouble(txtQ1.Text) > 1)
            //        PF = Convert.ToDouble(txtQ1.Text) / 100;
            //    else
            //        PF = Convert.ToDouble(txtQ1.Text);
            //    Q = 0;
            //    P = Convert.ToDouble(txtP.Text);

            //}

        }

        private void frmLoad_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            BindDataToList();
            SetGroupBoxEnableFalse();
            cboMode.SelectedIndex = 0;
            Mode = 0;
            SelectedLoadCode = 0;
            p1 = string.Empty;
            q1 = string.Empty;
            pf1 = string.Empty;
            Countc = 0;

            toolStripLabel3.Enabled = false;
        }

        public void Save()
        {
            //ed.WriteMessage("\naaaaaaaaaaaaaaa\n");

            string stringType = "";
            Atend.Base.Calculating.CLoadFactor loadFActor = new Atend.Base.Calculating.CLoadFactor();
            if (chkP.Checked)
            {
                stringType = "1";
            }

            if (chkI.Checked)
            {
                stringType = stringType + "2";
            }
            if (chkZ.Checked)
            {
                stringType = stringType + "3";
            }

            if (ChkJ.Checked)
            {
                stringType = stringType + "4";
            }

            
            //**********Extra
            //loadFActor.DesignCode = Atend.Control.Common.SelectedDesignCode;

            if (chkP.Checked)
            {
                loadFActor.P = Convert.ToDouble(txtP.Text);

                if (Mode == 0 || Mode == 1)
                    loadFActor.Q = Convert.ToDouble(txtQ1.Text);
                else
                    if (Mode == 2)
                        loadFActor.Q = Convert.ToDouble(txtQ2.Text);

                
                loadFActor.PF = Convert.ToDouble(txtPF.Text);

                //if (Mode == 0)
                //{
                //    loadFActor.P = P;
                //    double a = P * Math.Tan(Math.Acos(PF));
                //    Q = Math.Abs(Math.Round(a, 2));

                //    loadFActor.Q = Q;
                //    loadFActor.PF = PF;
                //}

                //if (Mode == 1)
                //{

                //    loadFActor.P = P;
                //    double a = Math.Cos(Math.Atan(Q / P));
                //    PF = Math.Abs(Math.Round(a, 2));

                //    loadFActor.Q = Q;
                //    loadFActor.PF = PF;
                //}

                //if (Mode == 2)
                //{


                //    double a = Q / Math.Tan(Math.Acos(PF));
                //    P = Math.Abs(Math.Round(a, 2));

                //    loadFActor.P = P;
                //    loadFActor.Q = Q;
                //    loadFActor.PF = PF;
                //}



                Amper = (loadFActor.P) / (Math.Sqrt(3) * Math.Abs(Voltage) * Math.Cos(Math.Atan(loadFActor.Q / loadFActor.P)));
            }
            else
            {
                loadFActor.P = 0;
                loadFActor.Q = 0;
                loadFActor.PF = 0;
            }

            if (chkI.Checked)
            {
                ed.WriteMessage("PF2\n");
                loadFActor.PF2 = Convert.ToDouble(txtPF3.Text);
                loadFActor.I2 = Convert.ToDouble(txtI2.Text);
                Amper = loadFActor.I2;
            }
            else
            {
                loadFActor.PF2 = 0;
                loadFActor.I2 = 0;
            }
            if (chkZ.Checked)
            {
                loadFActor.R = Convert.ToDouble(txtR.Text);
                loadFActor.X = Convert.ToDouble(txtX.Text);
            }
            else
            {
                loadFActor.R = 0;
                loadFActor.X = 0;
            }

            loadFActor.ModeNumber = stringType;
            loadFActor.Name = string.Empty;

            if (ChkJ.Checked)
            {
                if (rdoP.Checked)
                    loadFActor.TypeCode = 1;

                if (rdoI.Checked)
                    loadFActor.TypeCode = 2;

                if (rdoZ.Checked)
                    loadFActor.TypeCode = 3;
            }


            if (SelectedLoadCode == 0)
            {
                if (loadFActor.AccessInsert())
                {
                    ed.WriteMessage("Insert LoadFActor\n");
                    Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByCode(nodeCode);

                    //for (int i = 0; i < ConsolGuidList.Count; i++)
                    //{
                    Package.LoadCode = loadFActor.Code;
                        //Guid Code = new Guid(ConsolGuidList[i].ToString());
                        //Consol.DesignCode = Atend.Control.Common.SelectedDesignCode;
                        //Package.Code = ConsolCode;

                        //Package.LoadCode = loadFActor.Code;
                        Package.AccessUpdate();

                    //}

                    for (int i = 0; i < gvJ.RowCount; i++)
                    {
                        Atend.Base.Calculating.CPackageLoad PL = new Atend.Base.Calculating.CPackageLoad();
                        PL.LoadFactorCode = loadFActor.Code;
                        PL.DLoadFactorCode = Convert.ToInt32(gvJ.Rows[i].Cells[0].Value.ToString());
                        PL.Count = Convert.ToInt16(gvJ.Rows[i].Cells[7].Value.ToString());
                        PL.AccessInsert();
                    }


                    //Atend.Global.Acad.UAcad.WriteLoadForPole(nodeCode);
                    //MessageBox.Show("عملیات با موفقیت انجام شد\n");
                }
                else
                {
                    MessageBox.Show("عملیات با موفقیت انجام نشد\n");
                }
            }
            else
                if (SelectedLoadCode > 0)
                {
                    //Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByCode(nodeCode);

                    //for (int i = 0; i < ConsolGuidList.Count; i++)
                    //{
                    //Package.LoadCode = loadFActor.Code;
                    //Guid Code = new Guid(ConsolGuidList[i].ToString());
                    //Consol.DesignCode = Atend.Control.Common.SelectedDesignCode;
                    //Package.Code = ConsolCode;

                    //Package.LoadCode = loadFActor.Code;
                    //Package.AccessUpdate();

                    //}
                    ed.WriteMessage("Update\n");
                    if (gvJ.RowCount > 0)
                    {
                        Atend.Base.Calculating.CPackageLoad.AccessDeleteByLoadCode(SelectedLoadCode);

                        for (int i = 0; i < gvJ.RowCount; i++)
                        {
                            Atend.Base.Calculating.CPackageLoad PL = new Atend.Base.Calculating.CPackageLoad();
                            PL.LoadFactorCode = SelectedLoadCode;
                            PL.DLoadFactorCode = Convert.ToInt32(gvJ.Rows[i].Cells[0].Value.ToString());
                            PL.Count = Convert.ToInt16(gvJ.Rows[i].Cells[7].Value.ToString());
                            PL.AccessInsert();
                        }
                    }

                    //Atend.Global.Acad.UAcad.WriteLoadForPole(ConsolGuidList);
                    //MessageBox.Show("عملیات با موفقیت انجام شد\n");

                    loadFActor.Code = SelectedLoadCode;
                    ed.WriteMessage("SelectedLoadFActor={0}\n",SelectedLoadCode);
                    if (loadFActor.AccessUpdate())
                    {
                    }
                    else
                    {
                        MessageBox.Show("بار انتخاب شده با موفقيت ويرايش نشد");
                    }
                }
        }
        
        public bool Validation()
        {

            if (gbP.Enabled == true)
            {
                if (string.IsNullOrEmpty(txtP.Text) || string.IsNullOrEmpty(txtQ1.Text) || string.IsNullOrEmpty(txtPF.Text))
                {
                    MessageBox.Show("لطفاً مقادير مربوط به توان ثابت را وارد كنيد", "خطا");
                    if(Mode == 0 || Mode == 1 )
                        txtP.Focus();
                    if (Mode == 2)
                        txtQ2.Focus();
                    return false;
                }

                if ((Mode == 0 || Mode == 1) && !Atend.Control.NumericValidation.DoubleConverter(txtP.Text))
                {
                    MessageBox.Show("لطفاً توان حقیقی را با فرمت مناسب وارد نمایید", "خطا");
                    txtP.Focus();
                    txtP.Select(0, txtP.Text.Length);
                    return false;
                }

                if ((Mode == 0 || Mode == 2) && !Atend.Control.NumericValidation.DoubleConverter(txtPF.Text))
                {
                    MessageBox.Show("لطفاً درصد ضریب توان را با فرمت مناسب وارد نمایید", "خطا");
                    txtPF.Focus();
                    txtPF.Select(0, txtPF.Text.Length);
                    return false;
                }

                if ((Mode == 1) && !Atend.Control.NumericValidation.DoubleConverter(txtQ1.Text))
                {
                    MessageBox.Show("لطفاً توان مجازی را با فرمت مناسب وارد نمایید", "خطا");
                    txtQ1.Focus();
                    txtQ1.Select(0, txtQ1.Text.Length);
                    return false;
                }

                if ((Mode == 2) && !Atend.Control.NumericValidation.DoubleConverter(txtQ2.Text))
                {
                    MessageBox.Show("لطفاً توان مجازی را با فرمت مناسب وارد نمایید", "خطا");
                    txtQ2.Focus();
                    txtQ2.Select(0, txtQ2.Text.Length);
                    return false;
                }

            }



            if (gbZ.Enabled == true)
            {

                if (string.IsNullOrEmpty(txtR.Text) || string.IsNullOrEmpty(txtX.Text))
                {
                    MessageBox.Show("لطفاً مقادير مربوط به امپدانس ثابت را وارد كنيد", "خطا");
                    txtR.Focus();
                    return false;
                }

                if (!Atend.Control.NumericValidation.DoubleConverter(txtR.Text))
                {
                    MessageBox.Show("لطفاً امپدانس را با فرمت مناسب وارد نمایید", "خطا");
                    txtR.Focus();
                    txtR.Select(0, txtR.Text.Length);
                    return false;
                }

                if (!Atend.Control.NumericValidation.DoubleConverter(txtX.Text))
                {
                    MessageBox.Show("لطفاً امپدانس را با فرمت مناسب وارد نمایید", "خطا");
                    txtX.Focus();
                    txtX.Select(0, txtX.Text.Length);
                    return false;
                }
            }

            if (gbI.Enabled == true)
            {
                if (string.IsNullOrEmpty(txtI2.Text) || string.IsNullOrEmpty(txtPF3.Text))
                {
                    MessageBox.Show("لطفاً مقادير مربوط به جريان ثابت را وارد كنيد", "خطا");
                    txtI2.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(txtI2.Text))
                {
                    MessageBox.Show("لطفاً امپدانس را با فرمت مناسب وارد نمایید", "خطا");
                    txtI2.Focus();
                    txtI2.Select(0, txtI2.Text.Length);
                    return false;
                }

                if (!Atend.Control.NumericValidation.DoubleConverter(txtPF3.Text))
                {
                    MessageBox.Show("لطفاً ضریب توان را با فرمت مناسب وارد نمایید", "خطا");
                    txtPF3.Focus();
                    txtPF3.Select(0, txtPF3.Text.Length);
                    return false;
                }
            }

            if (gb.Enabled == true)
            {
                if (gvJ.RowCount == 0)
                {
                    MessageBox.Show("لطفاً موارد مربوط به انشعاب را در جدول وارد كنيد", "خطا");
                    btnAdd.Focus();
                    return false;
                }

                for (int i = 0; i < gvJ.RowCount; i++)
                {
                    if (string.IsNullOrEmpty(gvJ.Rows[i].Cells[6].Value.ToString()))
                    {
                        MessageBox.Show("لطفاً تعداد عنصر مربوطه را در جدول وارد كنيد", "خطا");
                        return false;
                    }

                    if (!Atend.Control.NumericValidation.Int32Converter(gvJ.Rows[i].Cells[6].Value.ToString()))
                    {
                        MessageBox.Show("لطفاً تعداد را با فرمت مناسب وارد نمایید", "خطا");
                        gvJ.Focus();
                        return false;
                    }
                }

                if (rdoP.Checked == false && rdoI.Checked == false && rdoZ.Checked == false)
                {
                    MessageBox.Show("لطفاً حالت انشعاب را انتخاب كنيد", "خطا");
                    rdoP.Focus();
                    return false;
                }

            }

            if (nodeCode == Guid.Empty)
            {
                MessageBox.Show("لطفاً گره مورد نظر را انتخاب كنيد");
                btnSelect.Focus();
                return false;
            }

            return true;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Mode == 0 || Mode == 1)
            {
                LeaveP();
            }

            if (Mode == 2)
            {
                LeaveQ2();
            }

            //ed.WriteMessage("aa\n");
            if (Validation())
            {
                //ed.WriteMessage("bb\n");
                Save();
                Reset();
            }
        }
        public void SetGroupBoxEnableFalse()
        {
            gb.Enabled = false;
            gbI.Enabled = false;
            gbP.Enabled = false;
            gbZ.Enabled = false;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void gbIAndPF_Enter(object sender, EventArgs e)
        {

        }

        private void gbPAndPF_Enter(object sender, EventArgs e)
        {

        }

        private void gbPAndQ_Enter(object sender, EventArgs e)
        {

        }

        private void gbQAndPF_Enter(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            int PoleCounter = 0;
            this.Hide();


            //ConsolGuidList.Clear();
            PromptEntityOptions pso = new PromptEntityOptions("\nSelect Consol:");
            PromptEntityResult psr = ed.GetEntity(pso);
            if (psr.Status == PromptStatus.OK)
            {
                Atend.Base.Acad.AT_INFO info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(psr.ObjectId);
                if (info.ParentCode != "NONE" && info.NodeType == (int)Atend.Control.Enum.ProductType.Consol)
                {
                    PoleCounter++;
                    //ed.WriteMessage("PoleGuid is : {0} \n", info.NodeCode);
                    nodeCode=new Guid(info.NodeCode);
                }
                if (info.ParentCode != "NONE" && info.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
                {
                    PoleCounter++;
                    //ed.WriteMessage("PoleGuid is : {0} \n", info.NodeCode);
                   nodeCode=new Guid(info.NodeCode);
                }
                if (info.ParentCode != "NONE" && info.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
                {
                    PoleCounter++;
                    //ed.WriteMessage("PoleGuid is : {0} \n", info.NodeCode);
                    nodeCode = new Guid(info.NodeCode);
                }

            }

            this.Show();
            lblCount.Text = PoleCounter.ToString();
            if (nodeCode!= Guid.Empty)
            {
                //ed.WriteMessage("bind to form \n");
                BindDataToForm();
                
            }


        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void SetObjects(string cmd)
        {
            if (cmd.IndexOf('1') != -1)
            {
                gbP.Enabled = true;
                chkP.Checked = true;
            }


            if (cmd.IndexOf('2') != -1)
            {
                gbI.Enabled = true;
                chkI.Checked = true;
            }

            if (cmd.IndexOf('3') != -1)
            {
                gbZ.Enabled = true;
                chkZ.Checked = true;
            }

            if (cmd.IndexOf('4') != -1)
            {
                gb.Enabled = true;
                ChkJ.Checked = true;
            }
        }

        private void Reset()
        {
            txtQ1.Text = string.Empty;
            txtP.Text = string.Empty;
            txtPF.Text = string.Empty;
            //txtP1.Text = string.Empty;
            //txtQ1.Text = string.Empty;
            //txtPF1.Text = string.Empty;
            //txtPF2.Text = string.Empty;
            txtI2.Text = string.Empty;
            txtPF3.Text = string.Empty;
            txtR.Text = string.Empty;
            txtX.Text = string.Empty;

            SetGroupBoxEnableFalse();
            chkZ.Checked = false;
            chkI.Checked = false;
            ChkJ.Checked = false;
            chkP.Checked = false;
            gvJ.Rows.Clear();
        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Reset();
            SelectedLoadCode = 0;
        }

        private void txtI2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPF3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (lstJ.Items.Count > 0)
            //    if (Convert.ToInt16(lstJ.SelectedValue.ToString()) > 0)
            //    {

            //        Atend.Base.Calculating.CDloadFactor DL = Atend.Base.Calculating.CDloadFactor.SelectByCode(Convert.ToInt16(lstJ.SelectedValue.ToString()));
            //        gvJ.Rows.Add();
            //        gvJ.Rows[gvJ.Rows.Count - 1].Cells[0].Value = DL.Code;
            //        gvJ.Rows[gvJ.Rows.Count - 1].Cells[1].Value = DL.Name;
            //        gvJ.Rows[gvJ.Rows.Count - 1].Cells[2].Value = DL.Amper;
            //        gvJ.Rows[gvJ.Rows.Count - 1].Cells[3].Value = DL.PhaseCount;
            //        gvJ.Rows[gvJ.Rows.Count - 1].Cells[4].Value = DL.FactorPower;
            //        gvJ.Rows[gvJ.Rows.Count - 1].Cells[5].Value = DL.FactorConcurency;
            //        gvJ.Rows[gvJ.Rows.Count - 1].Cells[6].Value = 0;


            //    }

            if (lstJ.Items.Count > 0)
            {
                Countc = 0;
                Atend.Calculating.frmLoadCount frmCount = new frmLoadCount();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmCount);
                Countc = frmCount.Count;

                if (Countc != 0)
                    if (Convert.ToInt16(lstJ.SelectedValue.ToString()) > 0)
                    {
                        //if(gvJ.Rows
                        int i;
                        for (i = 0; (i < gvJ.Rows.Count) && (Convert.ToInt16(gvJ.Rows[i].Cells[0].Value.ToString()) != Convert.ToInt16(lstJ.SelectedValue.ToString() )); i++);

                        if (i < gvJ.Rows.Count)
                            gvJ.Rows[i].Cells[7].Value = Convert.ToInt32(gvJ.Rows[i].Cells[7].Value.ToString()) + Countc;
                        else
                        {
                            Atend.Base.Calculating.CDloadFactor DL = Atend.Base.Calculating.CDloadFactor.AccessSelectByCode(Convert.ToInt16(lstJ.SelectedValue.ToString()));
                            gvJ.Rows.Add();
                            gvJ.Rows[gvJ.Rows.Count - 1].Cells[0].Value = DL.Code;
                            gvJ.Rows[gvJ.Rows.Count - 1].Cells[1].Value = DL.Name;
                            gvJ.Rows[gvJ.Rows.Count - 1].Cells[2].Value = DL.Amper;
                            gvJ.Rows[gvJ.Rows.Count - 1].Cells[3].Value = DL.PhaseCount;
                            gvJ.Rows[gvJ.Rows.Count - 1].Cells[4].Value = DL.FactorPower;
                            gvJ.Rows[gvJ.Rows.Count - 1].Cells[5].Value = DL.FactorConcurency;
                            gvJ.Rows[gvJ.Rows.Count - 1].Cells[6].Value = DL.Voltage;
                            gvJ.Rows[gvJ.Rows.Count - 1].Cells[7].Value = Countc;
                        }
                            
                    }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ((gvJ.Rows.Count > 0) && (gvJ.SelectedRows.Count > 0))
            {
                gvJ.Rows.RemoveAt(gvJ.SelectedRows[0].Index);
            }
        }

        private void LeaveP()
        {
            fp = true;
            if (/*fq1 == true &&*/ Mode == 1 && !string.IsNullOrEmpty(txtP.Text) && !string.IsNullOrEmpty(txtQ1.Text))
            {
                Q = Math.Round(Convert.ToDouble(txtQ1.Text), 2);
                P = Math.Round(Convert.ToDouble(txtP.Text), 2);

                double a = Math.Round(Math.Atan(Q / P), 2);
                a = Math.Cos(a);
                txtPF.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)) * 100);
                fp = false;
                fq1 = false;
                fq2 = false;
            }
            else
                if (/*fpf == true &&*/ Mode == 0 && !string.IsNullOrEmpty(txtP.Text) && !string.IsNullOrEmpty(txtPF.Text))
                {
                    P = Math.Round(Convert.ToDouble(txtP.Text), 2);
                    PF = Math.Round(Convert.ToDouble(txtPF.Text), 2);

                    if (PF > 1)
                        PF = PF / 100;

                    double a = P * Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
                    txtQ1.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
                    txtQ2.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
                    fp = false;
                    fpf = false;
                    ed.WriteMessage("\naaaaaaaaaaaaaaa\n");
                }
        }

        private void txtP_TextChanged(object sender, EventArgs e)
        {

            LeaveP();
            //fp = true;
            //if (/*fq1 == true &&*/ Mode == 1 && !string.IsNullOrEmpty(txtP.Text) && !string.IsNullOrEmpty(txtQ1.Text))
            //{
            //    Q = Math.Round(Convert.ToDouble(txtQ1.Text), 2);
            //    P = Math.Round(Convert.ToDouble(txtP.Text), 2);

            //    double a = Math.Round(Math.Atan(Q / P), 2);
            //    a = Math.Cos(a);
            //    txtPF.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)) * 100);
            //    fp = false;
            //    fq1 = false;
            //    fq2 = false;
            //}
            //else
            //    if (/*fpf == true &&*/ Mode == 0 && !string.IsNullOrEmpty(txtP.Text) && !string.IsNullOrEmpty(txtPF.Text))
            //    {
            //        P = Math.Round(Convert.ToDouble(txtP.Text), 2);
            //        PF = Math.Round(Convert.ToDouble(txtPF.Text), 2);

            //        if (PF > 1)
            //            PF = PF / 100;

            //        double a = P * Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
            //        txtQ1.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
            //        txtQ2.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
            //        fp = false;
            //        fpf = false;

            //    }

            //if (Mode == 0)
            //{
            //    P = Convert.ToDouble(txtP.Text);
            //    Q = 0;
            //}


            //if (Mode == 1)
            //{
            //    P = Convert.ToDouble(txtP.Text);
            //    PF = 0;
            //}


            //if (Mode == 2)
            //{
            //    Q = Convert.ToDouble(txtP.Text);
            //    P = 0;
            //}

        }

        private void LeaveQ1()
        {
            fq1 = true;
            fq2 = true;

            //if(txtQ1.Text 
            txtQ2.Text = txtQ1.Text;

            if (/*fp == true && */Mode == 1 && !string.IsNullOrEmpty(txtP.Text) && !string.IsNullOrEmpty(txtQ1.Text))
            {
                Q = Math.Round(Convert.ToDouble(txtQ1.Text), 2);
                P = Math.Round(Convert.ToDouble(txtP.Text), 2);

                double a = Math.Round(Math.Atan(Q / P), 2);
                a = Math.Cos(a);
                txtPF.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)) * 100);
                fq1 = false;
                fq2 = false;
                fp = false;

            }

 
        }

        private void txtq_TextChanged(object sender, EventArgs e)
        {
            LeaveQ1();
            //fq1 = true;
            //fq2 = true;
            
            ////if(txtQ1.Text 
            //txtQ2.Text = txtQ1.Text;

            ////if (/*fpf == true &&*/ Mode == 2 && !string.IsNullOrEmpty(txtPF.Text) && !string.IsNullOrEmpty(txtQ2.Text))
            ////{
            ////    Q = Math.Round(Convert.ToDouble(txtQ2.Text), 2);
            ////    PF = Math.Round(Convert.ToDouble(txtPF.Text), 2);

            ////    if (PF > 1)
            ////        PF = PF / 100;

            ////    double a = Q / Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
            ////    txtP.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
            ////    fq1 = false;
            ////    fq2 = false;
            ////    fpf = false;


            ////}
            ////else
            //if (/*fp == true && */Mode == 1 && !string.IsNullOrEmpty(txtP.Text) && !string.IsNullOrEmpty(txtQ1.Text))
            //{
            //    Q = Math.Round(Convert.ToDouble(txtQ1.Text), 2);
            //    P = Math.Round(Convert.ToDouble(txtP.Text), 2);

            //    double a = Math.Round(Math.Atan(Q / P), 2);
            //    a = Math.Cos(a);
            //    txtPF.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)) * 100);
            //    fq1 = false;
            //    fq2 = false;
            //    fp = false;

            //}



            //if (Mode == 0)
            //{
            //    if (Convert.ToDouble(txtq.Text) > 1)
            //        PF = Convert.ToDouble(txtq.Text) / 100;
            //    else
            //        PF = Convert.ToDouble(txtq.Text);
            //    Q = 0;
            //}

            //if (Mode == 1)
            //{
            //    Q = Convert.ToDouble(txtq.Text);
            //    PF = 0;
            //}

            //if (Mode == 2)
            //{
            //    if (Convert.ToDouble(txtq.Text) > 1)
            //        PF = Convert.ToDouble(txtq.Text) / 100;
            //    else
            //        PF = Convert.ToDouble(txtq.Text);
            //    P = 0;
            //}

            




        }

        private void LeaveQ2()
        {

            fq1 = true;
            fq2 = true;

            //if(txtQ1.Text 
            txtQ1.Text = txtQ2.Text;

            if (/*fpf == true &&*/Mode == 2 && !string.IsNullOrEmpty(txtPF.Text) && !string.IsNullOrEmpty(txtQ2.Text))
            {
                Q = Math.Round(Convert.ToDouble(txtQ2.Text), 2);
                PF = Math.Round(Convert.ToDouble(txtPF.Text), 2);

                if (PF > 1)
                    PF = PF / 100;

                double a = Q / Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
                txtP.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
                fq1 = false;
                fq2 = false;
                fpf = false;

            }
        }


        private void txtQ2_Leave(object sender, EventArgs e)
        {
            LeaveQ2();
            //fq1 = true;
            //fq2 = true;

            ////if(txtQ1.Text 
            //txtQ1.Text = txtQ2.Text;

            //if (/*fpf == true &&*/Mode == 2 && !string.IsNullOrEmpty(txtPF.Text) && !string.IsNullOrEmpty(txtQ2.Text))
            //{
            //    Q = Math.Round(Convert.ToDouble(txtQ2.Text), 2);
            //    PF = Math.Round(Convert.ToDouble(txtPF.Text), 2);

            //    if (PF > 1)
            //        PF = PF / 100;

            //    double a = Q / Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
            //    txtP.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
            //    fq1 = false;
            //    fq2 = false;
            //    fpf = false;

            //}
            //else
            //if (/*fp == true &&*/ !string.IsNullOrEmpty(txtP.Text) && !string.IsNullOrEmpty(txtQ1.Text))
            //{
            //    Q = Math.Round(Convert.ToDouble(txtQ1.Text), 2);
            //    P = Math.Round(Convert.ToDouble(txtP.Text), 2);

            //    double a = Math.Round(Math.Atan(Q / P), 2);
            //    a = Math.Cos(a);
            //    txtPF.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)) * 100);
            //    fq1 = false;
            //    fq2 = false;
            //    fp = false;

            //}
        }

        private void LeavePF()
        {
            fpf = true;

            if (/*fp == true &&*/Mode == 0 && !string.IsNullOrEmpty(txtP.Text) && !string.IsNullOrEmpty(txtPF.Text))
            {
                P = Math.Round(Convert.ToDouble(txtP.Text), 2);
                PF = Math.Round(Convert.ToDouble(txtPF.Text), 2);

                if (PF > 1)
                    PF = PF / 100;

                //ed.WriteMessage("\n P = " + P.ToString() + "\n");
                //ed.WriteMessage("\n PF = " + PF.ToString() + "\n");

                double a = P * Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);

                //ed.WriteMessage("\n Q = " + Math.Acos(PF).ToString() + "\n");
                txtQ1.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
                txtQ2.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
                //ed.WriteMessage("\n Q = " + a.ToString() + "\n");
                fpf = false;
                fp = false;
            }
            else
                if (/*fpf == true && */ Mode == 2 && !string.IsNullOrEmpty(txtPF.Text) && !string.IsNullOrEmpty(txtQ2.Text))
                {
                    Q = Math.Round(Convert.ToDouble(txtQ2.Text), 2);
                    PF = Math.Round(Convert.ToDouble(txtPF.Text), 2);

                    if (PF > 1)
                        PF = PF / 100;

                    double a = Q / Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
                    txtP.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
                    fpf = false;
                    fq1 = false;
                }
        }

        private void txtPF_TextChanged(object sender, EventArgs e)
        {

            LeavePF();
            //fpf = true;

            //if (/*fp == true &&*/Mode == 0 && !string.IsNullOrEmpty(txtP.Text) && !string.IsNullOrEmpty(txtPF.Text))
            //{
            //    P = Math.Round(Convert.ToDouble(txtP.Text), 2);
            //    PF = Math.Round(Convert.ToDouble(txtPF.Text), 2);
                
            //    if (PF > 1)
            //        PF = PF / 100;
                
            //    //ed.WriteMessage("\n P = " + P.ToString() + "\n");
            //    //ed.WriteMessage("\n PF = " + PF.ToString() + "\n");

            //    double a = P * Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);

            //    //ed.WriteMessage("\n Q = " + Math.Acos(PF).ToString() + "\n");
            //    txtQ1.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
            //    txtQ2.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
            //    //ed.WriteMessage("\n Q = " + a.ToString() + "\n");
            //    fpf = false;
            //    fp = false;
            //}
            //else
            //    if (/*fpf == true && */ Mode == 2 && !string.IsNullOrEmpty(txtPF.Text) && !string.IsNullOrEmpty(txtQ2.Text))
            //    {
            //        Q = Math.Round(Convert.ToDouble(txtQ2.Text), 2);
            //        PF = Math.Round(Convert.ToDouble(txtPF.Text), 2);

            //        if (PF > 1)
            //            PF = PF / 100;

            //        double a = Q / Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
            //        txtP.Text = Convert.ToString(Math.Abs(Math.Round(a, 2)));
            //        fpf = false;
            //        fq1 = false;
            //    }

        }



        private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboMode.SelectedIndex == 0)
            {
                label9.Text = "(KW)";
                label10.Text = "(%)";

                label1.Text = ":P";
                label2.Text = ":PF";
                Mode = 0;
                txtP.Visible = true;
                txtPF.Visible = true;
                txtQ1.Visible = false;
                txtQ2.Visible = false;
                fq1 = false;
                fq2 = false;
            }

            if (cboMode.SelectedIndex == 1)
            {
                label9.Text = "(KW)";
                label10.Text = "(KVar)";

                Mode = 1;
                label1.Text = ":P";
                label2.Text = ":Q";

                txtP.Visible = true;
                txtQ1.Visible = true;
                txtPF.Visible = false;
                txtQ2.Visible = false;
                fpf = false;
            }

            if (cboMode.SelectedIndex == 2)
            {
                label9.Text = "(KVar)";
                label10.Text = "(%)";

                Mode = 2;
                label1.Text = ":Q";
                label2.Text = ":PF";

                txtQ2.Visible = true;
                txtPF.Visible = true;
                txtP.Visible = false;
                txtQ1.Visible = false;
                fp = false;
            }



            /*
            if (cboMode.SelectedIndex == 0)
            {
                label9.Text = "(KW)";
                label10.Text = "(%)";

                label1.Text = ":P";
                label2.Text = ":PF";

                Mode = 0;//P & PF

                if (P != 0 && Q != 0)
                {
                    Q = Math.Round(Q, 2);
                    P = Math.Round(P, 2);

                    double a = Math.Round(Math.Atan(Q / P),2);
                    a = Math.Cos(a);
                    PF = Math.Abs(Math.Round(a, 2)) * 100;
                }
                else
                    if (PF != 0 && Q != 0)
                    {
                        Q = Math.Round(Q, 2);
                        PF = Math.Round(PF, 2);

                        double a = Q / Math.Round(Math.Tan(Math.Round(Math.Acos(PF),2)) , 2);
                        P = Math.Abs(Math.Round(a, 2));
                    }

                if (PF < 1)
                    PF *= 100;

                txtP.Text = P.ToString();
                txtq.Text = PF.ToString();




                P = Convert.ToDouble(txtP.Text);
                if (Convert.ToDouble(txtq.Text) > 1)
                    PF = Convert.ToDouble(txtq.Text) / 100;
                else
                    PF = Convert.ToDouble(txtq.Text);
                Q = 0;


                //MessageBox.Show("P = " + p1 + " Q = " + q1 + " PF = " + pf1);

                //if (p1 != string.Empty && q1 != string.Empty)
                //{
                //    Q = Convert.ToDouble(q1);
                //    P = Convert.ToDouble(p1);

                //    double a = Math.Cos(Math.Atan(Q / P));
                //    PF = Math.Abs(Math.Round(a, 2)) * 100;
                //    txtP.Text = p1;
                //    txtq.Text = PF.ToString();
                //}
                //else
                //    if (pf1 != string.Empty && q1 != string.Empty)
                //    {
                //        Q = Convert.ToDouble(q1);
                //        PF = Convert.ToDouble(pf1);

                //        double a = Q / Math.Tan(Math.Acos(PF));
                //        P = Math.Abs(Math.Round(a, 2));

                //        txtP.Text = P.ToString();
                //        txtq.Text = pf1;
                //    }




            }


            if (cboMode.SelectedIndex == 1)
            {
                label9.Text = "(KW)";
                label10.Text = "(KVar)";

                label1.Text = ":P";
                label2.Text = ":Q";

                Mode = 1;// P & Q


                if (PF != 0 && Q != 0)
                {
                    //double a = Q / Math.Tan(Math.Acos(PF));
                    //P = Math.Abs(Math.Round(a, 2));

                    Q = Math.Round(Q, 2);
                    PF = Math.Round(PF, 2);

                    double a = Q / Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
                    P = Math.Abs(Math.Round(a, 2));
                }
                else
                    if (PF != 0 && P != 0)
                    {
                        P = Math.Round(P, 2);
                        PF = Math.Round(PF, 2);

                        double a = P * Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
                        Q = Math.Abs(Math.Round(a, 2));
                    }

                if (PF < 1)
                    PF *= 100;


                txtP.Text = P.ToString();
                txtq.Text = Q.ToString();




                P = Convert.ToDouble(txtP.Text);
                Q = Convert.ToDouble(txtq.Text);
                PF = 0;

                //MessageBox.Show("P = " + p1 + " Q = " + q1 + " PF = " + pf1);

                //if (pf1 != string.Empty && q1 != string.Empty)
                //{
                //    Q = Convert.ToDouble(q1);
                //    PF = Convert.ToDouble(pf1);

                //    double a = Q / Math.Tan(Math.Acos(PF));
                //    P = Math.Abs(Math.Round(a, 2));
                //    txtP.Text = P.ToString();
                //    txtq.Text = q1;
                //}
                //else
                //    if (pf1 != string.Empty && p1 != string.Empty)
                //    {
                //        P = Convert.ToDouble(p1);
                //        PF = Convert.ToDouble(pf1);

                //        double a = P * Math.Tan(Math.Acos(PF));
                //        Q = Math.Abs(Math.Round(a, 2));
                //        txtP.Text = p1;
                //        txtq.Text = Q.ToString();
                //    }

            }


            if (cboMode.SelectedIndex == 2)
            {
                label9.Text = "(KVar)";
                label10.Text = "(%)";

                label1.Text = ":Q";
                label2.Text = ":PF";

                Mode = 2;//Q & PF

                if (PF != 0 && P != 0)
                {
                    P = Math.Round(P, 2);
                        PF = Math.Round(PF, 2);

                        double a = P * Math.Round(Math.Tan(Math.Round(Math.Acos(PF), 2)), 2);
                    Q = Math.Abs(Math.Round(a, 2));
                }
                else
                    if (P != 0 && Q != 0)
                    {
                        Q = Math.Round(Q, 2);
                        P = Math.Round(P, 2);

                        double a = Math.Round(Math.Atan(Q / P), 2);
                        a = Math.Cos(a);
                        PF = Math.Abs(Math.Round(a, 2)) * 100;                                               
                    }

                if (PF < 1)
                    PF *= 100;

                txtP.Text = Q.ToString();
                txtq.Text = PF.ToString();




                Q = Convert.ToDouble(txtP.Text);
                if (Convert.ToDouble(txtq.Text) > 1)
                    PF = Convert.ToDouble(txtq.Text) / 100;
                else
                    PF = Convert.ToDouble(txtq.Text);
                P = 0;


                //MessageBox.Show("P = " + p1 + " Q = " + q1 + " PF = " + pf1);

                //if (pf1 != string.Empty && p1 != string.Empty)
                //{
                //    P = Convert.ToDouble(p1);
                //    PF = Convert.ToDouble(pf1);

                //    double a = P * Math.Tan(Math.Acos(PF));
                //    Q = Math.Abs(Math.Round(a, 2));
                //    txtP.Text = Q.ToString();
                //    txtq.Text = pf1;
                //}
                //else
                //    if (p1 != string.Empty && q1 != string.Empty)
                //    {
                //        P = Convert.ToDouble(p1);
                //        Q = Convert.ToDouble(q1);

                //        double a = Math.Cos(Math.Atan(Q / P));
                //        PF = Math.Abs(Math.Round(a, 2)) * 100;
                //        txtP.Text = q1;
                //        txtq.Text = PF.ToString();
                //    }

            }



            */
        }

        private void txtP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Mode == 0)
            {
                p1 += e.KeyChar.ToString();
                q1 = string.Empty;
            }

            if (Mode == 1)
            {
                p1 += e.KeyChar.ToString();
                pf1 = string.Empty;
            }

            if (Mode == 2)
            {
                q1 += e.KeyChar.ToString();
                p1 = string.Empty;
            }

        }

        private void txtq_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Mode == 0)
            {
                pf1 += e.KeyChar.ToString();
                q1 = string.Empty;
            }

            if (Mode == 1)
            {
                q1 += e.KeyChar.ToString();
                pf1 = string.Empty;
            }

            if (Mode == 0)
            {
                pf1 += e.KeyChar.ToString();
                p1 = string.Empty;
            }

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (SelectedLoadCode > 0)
            {
                if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Atend.Base.Design.DPackage Pack = Atend.Base.Design.DPackage.AccessSelectByCode(nodeCode);
                    Pack.LoadCode = 0;
                    Pack.AccessUpdate();
                    //for (int i = 0; i < ConsolGuidList.Count; i++)
                    //{
                    //    Guid ConsolCode = new Guid(ConsolGuidList[i].ToString());
                    //   // Consol.DesignCode = Atend.Control.Common.SelectedDesignCode;
                    //    Consol.Code = ConsolCode;

                    //    Consol.LoadCode = 0;// SelectedLoadCode;
                    //    Consol.AccessUpdate_LoadCode();

                    //}

                    if (gvJ.RowCount > 0)
                    {
                        Atend.Base.Calculating.CPackageLoad.AccessDeleteByLoadCode(SelectedLoadCode);

                        //for (int i = 0; i < gvJ.RowCount; i++)
                        //{
                        //    Atend.Base.Calculating.CPackageLoad PL = new Atend.Base.Calculating.CPackageLoad();
                        //    PL.LoadFactorCode = SelectedLoadCode;
                        //    PL.DLoadFactorCode = Convert.ToInt32(gvJ.Rows[i].Cells[0].Value.ToString());
                        //    PL.Count = Convert.ToInt16(gvJ.Rows[i].Cells[7].Value.ToString());
                        //    PL.Insert();
                        //}


                    }

                    //Atend.Global.Acad.UAcad.WriteLoadForPole(ConsolGuidList);
                    //MessageBox.Show("عملیات با موفقیت انجام شد\n");

                    //loadFActor.Code = SelectedLoadCode;
                    if (Atend.Base.Calculating.CLoadFactor.AccessDelete(SelectedLoadCode))
                    {
                        //MessageBox.Show("بار از روی گره مورد نظر حذف شد");
                        //this.Close();
                    }
                    else
                    {
                        MessageBox.Show("عملیات با موفقیت انجام نشد","خطا");
                    }
                }
            }
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Mode == 0 || Mode == 1)
            {
                LeaveP();
            }

            if (Mode == 2)
            {
                LeaveQ2();
            }

            //ed.WriteMessage("aa\n");
            if (Validation())
            {
                //ed.WriteMessage("bb\n");
                Save();
            }
            Close();
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        

    }
}