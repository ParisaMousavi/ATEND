using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Design
{
    public partial class frmDesignSaveServer02 : Form
    {

        bool AllowToClose = true;
        bool ForceToClose = false;

        public frmDesignSaveServer02()
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

        private void frmDesignSaveServer_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Atend.Base.Design.DDesignProfile CurrentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (CurrentDesignProfile.DesignId != -1)
            {
                txtDesignName.Text = Atend.Control.Common.DesignName;
                txtDesignFullAddress.Text = Atend.Control.Common.DesignFullAddress;
                txtTitle.Text = CurrentDesignProfile.DesignName;
                Atend.Control.Common.DesignId = CurrentDesignProfile.DesignId;

                DataTable dt = Atend.Base.Design.DDesign.SelectAll();
                gvDesign1.AutoGenerateColumns = false;
                gvDesign1.DataSource = dt;


                Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Id", new object[1] { CurrentDesignProfile.DesignId }, dt, gvDesign1, this);
                AssignImageToGrid();

            }
            else
            {
                //MessageBox.Show("design id was -1");
            }

        }

        private bool Validation()
        {
            if (txtDesignName.Text == "")
            {
                MessageBox.Show("لطفا نام فایل طراحی را مشخص نمایید");
                return false;
            }

            if (txtDesignFullAddress.Text == "")
            {
                MessageBox.Show("لطفا مسیر فایل طراحی را مشخص نمایید");
                return false;
            }


            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Validation())
            {
                Atend.Base.Design.NodeTransaction _NodeTransaction = new Atend.Base.Design.NodeTransaction();

                SqlConnection sConnection = null;
                SqlTransaction sTransaction = null;

                OleDbTransaction aTransaction = null;
                OleDbConnection aConnection = null;

                if (_NodeTransaction.CreateTransactionAndConnection(out sTransaction, out sConnection, out aTransaction, out aConnection, false))
                {
                    Atend.Control.Common.DesignId = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection).DesignId;
                    if (Atend.Control.Common.DesignId == 0)//یعنی طرح جدید باز شده است
                    {
                        if (MessageBox.Show("آیا طرح در پشتیبان تعریف شده است", "ذخیره سازی طرح", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            ed.WriteMessage("_______ 1\n");
                            NewDesignWithPoshtiban(aConnection, aTransaction, sConnection, sTransaction);
                            ed.WriteMessage("_______ 1.1\n");
                        }
                        else /////////////////////////////////////////////////////////////////////////////////
                        {
                            ed.WriteMessage("_______ 2\n");
                            NewDesignWithoutPoshtiban(aConnection, aTransaction, sConnection, sTransaction);
                            ed.WriteMessage("_______ 2.2\n");
                        }
                    }
                    else////////////////////////////////////// ALL DESIGN HAVE DESIGN ID ///////////////////////////////////////
                    {
                        ed.WriteMessage("_______ 3\n");
                        PoshtibanDesign(aConnection, aTransaction, sConnection, sTransaction);
                    }
                }
                ed.WriteMessage("_______ 4\n");
                aTransaction.Commit();
                sTransaction.Commit();

                aConnection.Close();
                sConnection.Close();


                //#region DDesignProfile

                //Atend.Global.Utility.UOtherOutPut Output = new Atend.Global.Utility.UOtherOutPut();
                //Atend.Report.dsSagAndTension ds = Output.FillStatusReport();
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
                //for (int i = 0; i < ds.Tables["StatusReport"].Rows.Count; i++)
                //{

                //    Atend.Base.Design.DStatusReport STR = new Atend.Base.Design.DStatusReport();
                //    STR.DesignId = DP.DesignId;
                //    if (Atend.Control.NumericValidation.Int32Converter(ds.Tables["StatusReport"].Rows[i]["Code"].ToString()))
                //    {
                //        STR.ProductCode = Convert.ToInt32(ds.Tables["StatusReport"].Rows[i]["Code"].ToString());
                //    }
                //    else
                //        STR.ProductCode = 0;
                //    //MessageBox.Show(i.ToString() +"   " + );
                //    DataTable ExTbl = Atend.Base.Base.BEquipStatus.SelectAllX();
                //    STR.Existance = Convert.ToInt32(ExTbl.Select("Name = '" + ds.Tables["StatusReport"].Rows[i]["Exist"].ToString() + "'")[0]["ACode"].ToString());
                //    int pc = Convert.ToInt32(ds.Tables["StatusReport"].Rows[i]["ProjectCode"]);
                //    //int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Convert.ToInt32(Table.Rows[i]["ProjectCode"].ToString())).AdditionalCode;
                //    STR.ProjectCode = pc;
                //    ds.Tables["StatusReport"].Rows[i]["ProjectCode"] = pc;
                //    ds.Tables["StatusReport"].Rows[i]["Count"] = Math.Round(Convert.ToDouble(ds.Tables["StatusReport"].Rows[i]["Count"].ToString()), 2);
                //    STR.Count = Convert.ToDouble(ds.Tables["StatusReport"].Rows[i]["Count"].ToString());
                //    if (!STR.Insert())
                //    {
                //        ed.WriteMessage("\nError In D_StatusReport Insertion\n");
                //    }
                //}

                //#endregion

                DataTable dt = Atend.Base.Design.DDesign.SelectAll();
                gvDesign1.AutoGenerateColumns = false;
                gvDesign1.DataSource = dt;

            }
            else
            {
                AllowToClose = false;
            }


        }

        private void frmDesignSaveServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToClose)
            {
                e.Cancel = true;
            }
            AllowToClose = true;
        }

        /// <summary>
        /// طرح جدید با پشتیبان
        /// </summary>
        private void NewDesignWithPoshtiban(OleDbConnection aConnection, OleDbTransaction aTransaction, SqlConnection sConnection, SqlTransaction sTransaction)
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                //طرح کد نداره ولی در پشتیبان است  پس به پشتیبان اختصاص می دهیم
                if (gvDesign1.SelectedRows.Count > 0)
                {
                    DataTable dtWorkOrder = Atend.Base.Design.DPackage.AccessWorkOrders(aTransaction, aConnection);
                    int DesignId = Convert.ToInt32(gvDesign1.SelectedRows[0].Cells["Id"].Value);
                    //MessageBox.Show(DesignId.ToString());

                    Atend.Base.Design.DDesignFile _DDesignFile = Atend.Base.Design.DDesignFile.SelectByDesignId(sTransaction, sConnection, DesignId);
                    //MessageBox.Show(_DDesignFile.Id.ToString());

                    Atend.Base.Design.DDesignProfile _DDesignProfile = Atend.Base.Design.DDesignProfile.SelectByDesignID(sTransaction, sConnection, DesignId);
                    //MessageBox.Show(_DDesignProfile.Id.ToString());

                    Atend.Base.Design.DStatusReport.DeleteByDesignId(sTransaction, sConnection, DesignId);
                    Atend.Base.Design.DDesign _DDesign = Atend.Base.Design.DDesign.SelectByID(sTransaction, sConnection, DesignId);
                    Atend.Base.Design.DDesignProfile currentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
                    if (_DDesign.Id != -1)
                    {
                        //MessageBox.Show(_DDesign.Id.ToString());
                        //Atend.Base.Design.DDesignProfile currentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
                        if (currentDesignProfile.DesignId != -1)
                        {
                            currentDesignProfile.DesignId = DesignId;
                            currentDesignProfile.DesignCode = _DDesign.ArchiveNo;
                            currentDesignProfile.DesignName = _DDesign.Title;
                            if (!currentDesignProfile.AccessUpdate(aTransaction, aConnection))
                            {
                                throw new System.Exception("currentDesignProfile.AccessUpdate ");
                            }
                            //MessageBox.Show("currentDesignProfile.AccessUpdate");

                            Atend.Control.Common.DesignId = DesignId;

                            #region design
                            _DDesign.Region = Convert.ToByte(Atend.Base.Base.BRegion.SelectByCodeLoacal(currentDesignProfile.Zone).SecondCode);
                            if (!_DDesign.Update(sTransaction, sConnection))
                            {
                                throw new System.Exception("_DDesign.Update ");
                            }
                            #endregion

                            #region Update DesignProfile On Server
                            if (_DDesignProfile.Id != -1)
                            {
                                if (!currentDesignProfile.UpdateByDesignId(sTransaction, sConnection))
                                {
                                    throw new System.Exception("currentDesignProfile.UpdateByDesignId ");
                                }
                                //MessageBox.Show("currentDesignProfile.UpdateByDesignId");
                            }
                            else
                            {
                                if (!currentDesignProfile.Insert(sTransaction, sConnection))
                                {
                                    throw new System.Exception("currentDesignProfile.UpdateByDesignId ");
                                }
                                //MessageBox.Show("currentDesignProfile.Insert");
                            }
                            #endregion

                        }
                        //currentDesignProfile.DesignId != -1
                    }

                    #region DesignFile
                    _DDesignFile.DesignId = DesignId;
                    _DDesignFile.File = new byte[0];

                    #region ATNX file
                    FileStream fs;
                    fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    _DDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
                    fs.Dispose();
                    #endregion

                    #region PDF file
                    if (txtBookAddress.Text != string.Empty)
                    {
                        fs = new FileStream(txtBookAddress.Text, FileMode.Open);
                        br = new BinaryReader(fs);
                        _DDesignFile.Book = br.ReadBytes((Int32)br.BaseStream.Length);
                        fs.Dispose();
                    }
                    else
                    {
                        _DDesignFile.Book = new byte[0];
                    }
                    #endregion

                    #region DWF file
                    if (txtBookAddress.Text != string.Empty)
                    {
                        fs = new FileStream(txtDWFAddress.Text, FileMode.Open);
                        br = new BinaryReader(fs);
                        _DDesignFile.Image = br.ReadBytes((Int32)br.BaseStream.Length);
                        fs.Dispose();
                    }
                    else
                    {
                        _DDesignFile.Image = new byte[0];
                    }
                    #endregion

                    if (_DDesignFile.Id != -1)
                    {
                        //update last one
                        if (!_DDesignFile.Update(sTransaction, sConnection))
                        {
                            throw new System.Exception("_DDesignFile.Update");
                        }
                        //MessageBox.Show("_DDesignFile.Update");
                    }
                    else
                    {
                        //insert new one
                        if (!_DDesignFile.Insert(sTransaction, sConnection))
                        {
                            throw new System.Exception("_DDesignFile.Update");
                        }
                        //MessageBox.Show("_DDesignFile.Insert");
                    }
                    #endregion


                    #region StatusReport
                    Atend.Global.Utility.UOtherOutPut Output = new Atend.Global.Utility.UOtherOutPut();
                    Atend.Report.dsSagAndTension ds = Output.FillStatusReport();
                    Atend.Base.Design.DStatusReport.DeleteByDesignId(currentDesignProfile.DesignId);
                    for (int i = 0; i < ds.Tables["StatusReport"].Rows.Count; i++)
                    {

                        Atend.Base.Design.DStatusReport STR = new Atend.Base.Design.DStatusReport();
                        STR.DesignId = currentDesignProfile.DesignId;
                        if (Atend.Control.NumericValidation.Int32Converter(ds.Tables["StatusReport"].Rows[i]["Code"].ToString()))
                        {
                            STR.ProductCode = Convert.ToInt32(ds.Tables["StatusReport"].Rows[i]["Code"].ToString());
                        }
                        else
                            STR.ProductCode = 0;

                        DataTable ExTbl = Atend.Base.Base.BEquipStatus.SelectAllX();
                        STR.Existance = Convert.ToInt32(ExTbl.Select("Name = '" + ds.Tables["StatusReport"].Rows[i]["Exist"].ToString() + "'")[0]["ACode"].ToString());
                        int pc = Convert.ToInt32(ds.Tables["StatusReport"].Rows[i]["ProjectCode"]);
                        //int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Convert.ToInt32(Table.Rows[i]["ProjectCode"].ToString())).AdditionalCode;
                        STR.ProjectCode = pc;
                        ds.Tables["StatusReport"].Rows[i]["ProjectCode"] = pc;
                        ds.Tables["StatusReport"].Rows[i]["Count"] = Math.Round(Convert.ToDouble(ds.Tables["StatusReport"].Rows[i]["Count"].ToString()), 2);
                        STR.Count = Convert.ToDouble(ds.Tables["StatusReport"].Rows[i]["Count"].ToString());
                        if (!STR.Insert(sTransaction, sConnection))
                        {
                            ed.WriteMessage("\nError In D_StatusReport Insertion\n");
                        }
                    }
                    #endregion

                    #region WorkOrder
                    if (Atend.Base.Design.DWorkOrder.Delete(DesignId, sTransaction, sConnection))
                    {
                        foreach (DataRow drWorkOrder in dtWorkOrder.Rows)
                        {
                            Atend.Base.Design.DWorkOrder WO = new Atend.Base.Design.DWorkOrder();
                            WO.DesignID = Convert.ToInt32(gvDesign1.SelectedRows[0].Cells["Id"].Value);
                            WO.WorkOrder = Convert.ToInt32(drWorkOrder["WorkOrderCode"].ToString());
                            if (!WO.Insert(sTransaction, sConnection))
                            {
                                throw new System.Exception("WorkOrder.Insert Failed 6");
                            }
                        }
                        //MessageBox.Show("WO.Insert");
                    }
                    #endregion

                }//if (gvDesign.SelectedRows.Count > 0)
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR NewDesignWithPoshtiban : {0} \n", ex1.Message);
            }
        }

        /// <summary>
        /// طرح جدید بدون پشتیبان
        /// </summary>
        private void NewDesignWithoutPoshtiban(OleDbConnection aConnection, OleDbTransaction aTransaction, SqlConnection sConnection, SqlTransaction sTransaction)
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //طرح کد نداره و در پشتیبان طرحی نداره پی یک طرح جدید تولید می کنیم
            ed.WriteMessage("@@@@ 1\n");
            DataTable dtWorkOrder = Atend.Base.Design.DPackage.AccessWorkOrders(aTransaction, aConnection);
            Atend.Base.Design.DDesignProfile currentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
            Atend.Base.Design.DDesign currentDesign = new Atend.Base.Design.DDesign();
            currentDesign.Title = currentDesignProfile.DesignName;
            currentDesign.ArchiveNo = currentDesignProfile.DesignCode;
            currentDesign.PRGCode = 0;
            ed.WriteMessage("@@@@ 2\n");
            currentDesign.RequestType = 0;
            currentDesign.Region = Convert.ToByte(Atend.Base.Base.BRegion.SelectByCodeLoacal(currentDesignProfile.Zone).SecondCode);
            currentDesign.IsAtend = true;
            currentDesign.Address = currentDesignProfile.Address;
            ed.WriteMessage("@@@@ 3\n");
            if (!currentDesign.Insert(sTransaction, sConnection))
            {
                ed.WriteMessage("@@@@ 4\n");
                throw new System.Exception("currentDesign.Insert 1");
            }
            ed.WriteMessage("@@@@ 5\n");
            foreach (DataRow drWorkOrder in dtWorkOrder.Rows)
            {
                Atend.Base.Design.DWorkOrder WO = new Atend.Base.Design.DWorkOrder();
                WO.DesignID = currentDesign.Id;
                WO.WorkOrder = Convert.ToInt32(drWorkOrder["WorkOrderCode"].ToString());
                if (!WO.Insert(sTransaction, sConnection))
                {
                    throw new System.Exception("WorkOrder.Insert Failed 1");
                }
            }

            ed.WriteMessage("@@@@ 6\n");
            currentDesignProfile.DesignId = currentDesign.Id;
            if (!currentDesignProfile.AccessUpdate(aTransaction, aConnection))
            {
                throw new System.Exception("currentDesignProfile.AccessUpdate 1");
            }

            ed.WriteMessage("@@@@ 7\n");
            Atend.Base.Design.DDesignFile currentDesignFile = new Atend.Base.Design.DDesignFile();
            currentDesignFile.DesignId = currentDesign.Id;
            currentDesignFile.File = new byte[0];


            ed.WriteMessage("@@@@ 8\n");
            #region ATNX file
            FileStream fs;
            fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            currentDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
            fs.Dispose();
            #endregion

            ed.WriteMessage("@@@@ 9\n");
            #region PDF file
            if (txtBookAddress.Text != string.Empty)
            {
                fs = new FileStream(txtBookAddress.Text, FileMode.Open);
                br = new BinaryReader(fs);
                currentDesignFile.Book = br.ReadBytes((Int32)br.BaseStream.Length);
                fs.Dispose();
            }
            else
            {
                currentDesignFile.Book = new byte[0];
            }
            #endregion

            ed.WriteMessage("@@@@ 10\n");
            #region DWF file
            if (txtBookAddress.Text != string.Empty)
            {
                fs = new FileStream(txtDWFAddress.Text, FileMode.Open);
                br = new BinaryReader(fs);
                currentDesignFile.Image = br.ReadBytes((Int32)br.BaseStream.Length);
                fs.Dispose();
            }
            else
            {
                currentDesignFile.Image = new byte[0];
            }
            #endregion

            ed.WriteMessage("@@@@ 11\n");

            if (!currentDesignFile.Insert(sTransaction, sConnection))
            {
                throw new System.Exception("currentDesignFile.Insert 1");
            }
            ed.WriteMessage("@@@@ 12\n");
            if (!currentDesignProfile.Insert(sTransaction, sConnection))
            {
                throw new System.Exception("currentDesignProfile.Insert 1");
            }
            ed.WriteMessage("@@@@ 13\n");
            #region StatusReport
            Atend.Global.Utility.UOtherOutPut Output = new Atend.Global.Utility.UOtherOutPut();
            ed.WriteMessage("@@@@ 13.1\n");
            Atend.Report.dsSagAndTension ds = Output.FillStatusReport();
            ed.WriteMessage("@@@@ 13.2\n");
            Atend.Base.Design.DStatusReport.DeleteByDesignId(currentDesignProfile.DesignId);
            ed.WriteMessage("@@@@ 14,{0}\n", ds.Tables["StatusReport"].Rows.Count);
            for (int i = 0; i < ds.Tables["StatusReport"].Rows.Count; i++)
            {

                Atend.Base.Design.DStatusReport STR = new Atend.Base.Design.DStatusReport();
                STR.DesignId = currentDesignProfile.DesignId;
                if (Atend.Control.NumericValidation.Int32Converter(ds.Tables["StatusReport"].Rows[i]["Code"].ToString()))
                {
                    STR.ProductCode = Convert.ToInt32(ds.Tables["StatusReport"].Rows[i]["Code"].ToString());
                }
                else
                    STR.ProductCode = 0;

                DataTable ExTbl = Atend.Base.Base.BEquipStatus.SelectAllX();
                STR.Existance = Convert.ToInt32(ExTbl.Select("Name = '" + ds.Tables["StatusReport"].Rows[i]["Exist"].ToString() + "'")[0]["ACode"].ToString());
                int pc = Convert.ToInt32(ds.Tables["StatusReport"].Rows[i]["ProjectCode"]);
                //int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Convert.ToInt32(Table.Rows[i]["ProjectCode"].ToString())).AdditionalCode;
                STR.ProjectCode = pc;
                ds.Tables["StatusReport"].Rows[i]["ProjectCode"] = pc;
                ds.Tables["StatusReport"].Rows[i]["Count"] = Math.Round(Convert.ToDouble(ds.Tables["StatusReport"].Rows[i]["Count"].ToString()), 2);
                STR.Count = Convert.ToDouble(ds.Tables["StatusReport"].Rows[i]["Count"].ToString());
                if (!STR.Insert(sTransaction, sConnection))
                {
                    ed.WriteMessage("\nError In D_StatusReport Insertion\n");
                }
                ed.WriteMessage("@@@@ 15\n");
            }
            #endregion

        }

        /// <summary>
        /// طرح پشتیبان
        /// </summary>
        private void PoshtibanDesign(OleDbConnection aConnection, OleDbTransaction aTransaction, SqlConnection sConnection, SqlTransaction sTransaction)
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            DataTable dtWorkOrder = Atend.Base.Design.DPackage.AccessWorkOrders(aTransaction, aConnection);
            Atend.Base.Design.DDesignProfile _DDesignProfile = Atend.Base.Design.DDesignProfile.SelectByDesignID(sTransaction, sConnection, Atend.Control.Common.DesignId);
            Atend.Base.Design.DDesign _DDesign = Atend.Base.Design.DDesign.SelectByID(sTransaction, sConnection, Atend.Control.Common.DesignId);
            Atend.Base.Design.DDesignFile _DDesignFile = Atend.Base.Design.DDesignFile.SelectByDesignId(sTransaction, sConnection, Atend.Control.Common.DesignId);
            Atend.Base.Design.DDesignProfile currentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);

            if (_DDesignProfile.Id != -1)
            {
                if (currentDesignProfile.DesignId != -1)
                {
                    if (!currentDesignProfile.UpdateByDesignId(sTransaction, sConnection))
                    {
                        throw new System.Exception("currentDesignProfile.Update 4");
                    }
                }
                else
                {
                    throw new System.Exception("DDesignProfile access record was not found");
                }
            }
            else
            {
                if (currentDesignProfile.DesignId != -1)
                {
                    if (!currentDesignProfile.Insert(sTransaction, sConnection))
                    {
                        throw new System.Exception("currentDesignProfile.Insert 5");
                    }
                }
            }

            #region design
            _DDesign.Region = Convert.ToByte(Atend.Base.Base.BRegion.SelectByCodeLoacal(currentDesignProfile.Zone).SecondCode);
            if (!_DDesign.Update(sTransaction, sConnection))
            {
                throw new System.Exception("_DDesign.Update ");
            }
            #endregion

            #region DesignFile
            _DDesignFile.DesignId = Atend.Control.Common.DesignId;
            _DDesignFile.File = new byte[0];

            #region ATNX file
            FileStream fs;
            fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            _DDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
            fs.Dispose();
            #endregion

            #region PDF file
            if (txtBookAddress.Text != string.Empty)
            {
                fs = new FileStream(txtBookAddress.Text, FileMode.Open);
                br = new BinaryReader(fs);
                _DDesignFile.Book = br.ReadBytes((Int32)br.BaseStream.Length);
                fs.Dispose();
            }
            else
            {
                _DDesignFile.Book = new byte[0];
            }
            #endregion

            #region DWF file
            if (txtBookAddress.Text != string.Empty)
            {
                fs = new FileStream(txtDWFAddress.Text, FileMode.Open);
                br = new BinaryReader(fs);
                _DDesignFile.Image = br.ReadBytes((Int32)br.BaseStream.Length);
                fs.Dispose();
            }
            else
            {
                _DDesignFile.Image = new byte[0];
            }
            #endregion


            if (_DDesignFile.Id != -1)
            {
                if (!_DDesignFile.Update(sTransaction, sConnection))
                {
                    throw new System.Exception("NewDesignFile.Insert 5");
                }
            }
            else
            {
                if (!_DDesignFile.Insert(sTransaction, sConnection))
                {
                    throw new System.Exception("CurrentDesignFile.Update 4");
                }
            }
            #endregion

            #region StatusReport
            Atend.Global.Utility.UOtherOutPut Output = new Atend.Global.Utility.UOtherOutPut();
            Atend.Report.dsSagAndTension ds = Output.FillStatusReport();
            Atend.Base.Design.DStatusReport.DeleteByDesignId(currentDesignProfile.DesignId);
            for (int i = 0; i < ds.Tables["StatusReport"].Rows.Count; i++)
            {

                Atend.Base.Design.DStatusReport STR = new Atend.Base.Design.DStatusReport();
                STR.DesignId = currentDesignProfile.DesignId;
                if (Atend.Control.NumericValidation.Int32Converter(ds.Tables["StatusReport"].Rows[i]["Code"].ToString()))
                {
                    STR.ProductCode = Convert.ToInt32(ds.Tables["StatusReport"].Rows[i]["Code"].ToString());
                }
                else
                    STR.ProductCode = 0;

                DataTable ExTbl = Atend.Base.Base.BEquipStatus.SelectAllX();
                STR.Existance = Convert.ToInt32(ExTbl.Select("Name = '" + ds.Tables["StatusReport"].Rows[i]["Exist"].ToString() + "'")[0]["ACode"].ToString());
                int pc = Convert.ToInt32(ds.Tables["StatusReport"].Rows[i]["ProjectCode"]);
                //int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Convert.ToInt32(Table.Rows[i]["ProjectCode"].ToString())).AdditionalCode;
                STR.ProjectCode = pc;
                ds.Tables["StatusReport"].Rows[i]["ProjectCode"] = pc;
                ds.Tables["StatusReport"].Rows[i]["Count"] = Math.Round(Convert.ToDouble(ds.Tables["StatusReport"].Rows[i]["Count"].ToString()), 2);
                STR.Count = Convert.ToDouble(ds.Tables["StatusReport"].Rows[i]["Count"].ToString());
                if (!STR.Insert(sTransaction, sConnection))
                {
                    ed.WriteMessage("\nError In D_StatusReport Insertion\n");
                }
            }
            #endregion

            #region WorkOrder
            if (Atend.Base.Design.DWorkOrder.Delete(Atend.Control.Common.DesignId, sTransaction, sConnection))
            {
                foreach (DataRow drWorkOrder in dtWorkOrder.Rows)
                {
                    Atend.Base.Design.DWorkOrder WO = new Atend.Base.Design.DWorkOrder();
                    WO.DesignID = Atend.Control.Common.DesignId; ;
                    WO.WorkOrder = Convert.ToInt32(drWorkOrder["WorkOrderCode"].ToString());
                    if (!WO.Insert(sTransaction, sConnection))
                    {
                        throw new System.Exception("WorkOrder.Insert Failed 5");
                    }
                }
            }
            #endregion


        }

        private void Search()
        {
            DataTable dt = gvDesign1.DataSource as DataTable;
            if (dt != null)
            {
                dt.DefaultView.RowFilter = "Title like '%" + txtTitleSearch.Text + "%'";
            }

            AssignImageToGrid();
        }

        private void txtTitleSearch_TextChanged(object sender, EventArgs e)
        {
            //Search();
        }

        private void btnSelectBook_Click(object sender, EventArgs e)
        {

            OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            _OpenFileDialog.Title = "انتخاب دفترچه طرح";
            _OpenFileDialog.Filter = "*.PDF | *.pdf";
            _OpenFileDialog.InitialDirectory = Atend.Control.Common.DesignFullAddress;
            if (_OpenFileDialog.ShowDialog() == DialogResult.OK && _OpenFileDialog.FileName != string.Empty)
            {
                txtBookAddress.Text = _OpenFileDialog.FileName;




            }
        }

        private void btnSelectDWF_Click(object sender, EventArgs e)
        {
            OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            _OpenFileDialog.Title = "انتخاب فایل Dwf";
            _OpenFileDialog.Filter = "*.DWF | *.dwf";
            _OpenFileDialog.InitialDirectory = Atend.Control.Common.DesignFullAddress;
            if (_OpenFileDialog.ShowDialog() == DialogResult.OK && _OpenFileDialog.FileName != string.Empty)
            {
                txtDWFAddress.Text = _OpenFileDialog.FileName;

            }

        }

        private void gvDesign1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void AssignImageToGrid()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            for (int i = 0; i <= gvDesign1.Rows.Count - 1; i++)
            {
                DataGridViewImageCell _IC1 = gvDesign1.Rows[i].Cells["imgFile"] as DataGridViewImageCell;
                DataGridViewImageCell _IC2 = gvDesign1.Rows[i].Cells["imgBook"] as DataGridViewImageCell;
                DataGridViewImageCell _IC3 = gvDesign1.Rows[i].Cells["imgDwf"] as DataGridViewImageCell;
                Atend.Base.Design.DDesignFile dfile = Atend.Base.Design.DDesignFile.SelectByDesignId(Convert.ToInt32(gvDesign1.Rows[i].Cells["ID"].Value.ToString()));
                //ed.WriteMessage("ID={0}\n", dfile.Id);
                
                if (dfile.Id == -1)
                {
                    //ed.WriteMessage("Have Not File\n");
                    _IC1.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Equip16.png");
                    gvDesign1.Refresh();
                    _IC2.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Equip16.png");
                    gvDesign1.Refresh();
                    _IC3.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Equip16.png");
                    gvDesign1.Refresh();
                    gvDesign1.RefreshEdit();
                }
                else
                {

                    if (dfile.File.Length > 0)
                    {

                        _IC1.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\HaveFile.png");//file
                        //ed.WriteMessage("1\n");
                        gvDesign1.Refresh();
                    }
                    else
                    {
                        _IC1.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Equip16.png");
                        //ed.WriteMessage("2\n");

                        gvDesign1.Refresh();
                    }

                    if (dfile.Book.Length > 0)
                    {
                        _IC2.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\PDF.jpg");//book
                        gvDesign1.Refresh();
                    }
                    else
                    {
                        _IC2.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Equip16.png");
                        gvDesign1.Refresh();
                    }


                    if (dfile.Image.Length > 0)
                    {
                        _IC3.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Excel.png");//dwf
                        gvDesign1.Refresh();
                    }
                    else
                    {
                        _IC3.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Equip16.png");
                        gvDesign1.Refresh();
                    }
                    gvDesign1.Refresh();

                }
            }
            gvDesign1.RefreshEdit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        //private void AssignImageToGrid()
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("Assign Image\n");
        //    for (int i = 0; i <= gvDesign1.Rows.Count - 1; i++)
        //    {
        //        DataGridViewImageCell _IC = gvDesign1.Rows[i].Cells["imgFile"] as DataGridViewImageCell;
        //        Atend.Base.Design.DDesignFile dfile = Atend.Base.Design.DDesignFile.SelectByDesignId(Convert.ToInt32(gvDesign1.Rows[i].Cells["ID"].Value.ToString()));
        //        if (dfile.Id == -1)
        //        {
        //            _IC.Value= new Bitmap(Atend.Control.Common.fullPath + @"\Icon\Equip16.png");
        //            gvDesign1.Refresh();
        //            gvDesign1.RefreshEdit();
        //            ed.WriteMessage("Have Not File\n");
        //        }
        //        else
        //        {
        //            _IC.Value = new Bitmap(Atend.Control.Common.fullPath + @"\Icon\HaveFile.png");
        //            gvDesign1.Refresh();
        //        }
        //    }

        //}



    }
}