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
    public partial class frmDesignSaveServer : Form
    {

        bool AllowToClose = true;
        bool ForceToClose = false;


        public frmDesignSaveServer()
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
                gvDesign.AutoGenerateColumns = false;
                gvDesign.DataSource = dt;

                ///this.Text = Atend.Control.Common.DesignId.ToString();
            }
            else
            {
                MessageBox.Show("design id was -1");
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

                try
                {


                    if (_NodeTransaction.CreateTransactionAndConnection(out sTransaction, out sConnection, out aTransaction, out aConnection, false))
                    {

                        Atend.Control.Common.DesignId = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection).DesignId;
                        if (Atend.Control.Common.DesignId == 0)
                        {
                            if (MessageBox.Show("آیا طرح در پشتیبان تعریف شده است", "ذخیره سازی طرح", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                //طرح کد نداره ولی در پشتیبان است  پس به پشتیبان اختصاص می دهیم
                                ed.WriteMessage("~~~ No design Id  and is Poshtiban file ~~~\n");
                                if (gvDesign.SelectedRows.Count > 0)
                                {
                                    if (chkIsComplement.Checked)
                                    {
                                        //Is Postiban and is complement
                                        ed.WriteMessage("~~~ is complement ~~~\n");
                                        Atend.Base.Design.DDesign currentDessign = Atend.Base.Design.DDesign.SelectByID(sTransaction, sConnection, Convert.ToInt32(gvDesign.SelectedRows[0].Cells["Id"].Value));
                                        if (currentDessign.Id != -1)
                                        {
                                            DataTable Designs = Atend.Base.Design.DDesign.SelectByCode(sTransaction, sConnection, currentDessign.Code);
                                            if (Designs.Rows.Count > 0)
                                            {
                                                DataView dv = new DataView(Designs);
                                                dv.Sort = "RequestType";
                                                Atend.Base.Design.DDesign NewDesign = Atend.Base.Design.DDesign.SelectByID(sTransaction, sConnection, Convert.ToInt32(dv.Table.Rows[0]["Id"].ToString()));
                                                if (NewDesign.Id != -1)
                                                {
                                                    NewDesign.RequestType++;
                                                    if (!NewDesign.Insert(sTransaction, sConnection))
                                                    {
                                                        throw new System.Exception("NewDesign.Insert 2");
                                                    }

                                                    Atend.Base.Design.DDesignProfile currentProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
                                                    if (currentProfile.DesignId != -1)
                                                    {
                                                        currentProfile.DesignId = NewDesign.Id;
                                                        if (!currentProfile.AccessUpdate(aTransaction, aConnection))
                                                        {
                                                            throw new System.Exception("currentProfile.AccessUpdate 2");
                                                        }

                                                        Atend.Base.Design.DDesignFile NewDesignFile = new Atend.Base.Design.DDesignFile();
                                                        NewDesignFile.DesignId = NewDesign.Id;
                                                        NewDesignFile.File = null;
                                                        FileStream fs;
                                                        fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
                                                        BinaryReader br = new BinaryReader(fs);
                                                        NewDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
                                                        fs.Dispose();
                                                        if (!NewDesignFile.Insert(sTransaction, sConnection))
                                                        {
                                                            throw new System.Exception("NewDesignFile.Insert 2");
                                                        }

                                                        if (!currentProfile.Insert(sTransaction, sConnection))
                                                        {
                                                            throw new System.Exception("currentProfile.Insert 2");
                                                        }
                                                        MessageBox.Show("Well done 2");
                                                    }


                                                }
                                            }
                                        }////////
                                    }
                                    else
                                    {
                                        //Is Poshtiban and is not complement
                                        ed.WriteMessage("~~~ is not complement ~~~\n");
                                        Atend.Base.Design.DDesignFile CurrentDesignFile = Atend.Base.Design.DDesignFile.SelectByDesignId(sTransaction, sConnection, Convert.ToInt32(gvDesign.SelectedRows[0].Cells["Id"].Value));
                                        if (CurrentDesignFile.Id != -1)
                                        {
                                            ed.WriteMessage("~~~  have file ~~~\n");
                                            Atend.Base.Design.DDesignProfile currentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
                                            if (currentDesignProfile.DesignId != -1)
                                            {
                                                currentDesignProfile.DesignId = Convert.ToInt32(gvDesign.SelectedRows[0].Cells["Id"].Value);
                                                if (!currentDesignProfile.AccessUpdate(aTransaction, aConnection))
                                                {
                                                    throw new System.Exception("currentDesignProfile.AccessUpdate 6");
                                                }

                                                CurrentDesignFile.File = null;
                                                FileStream fs;
                                                fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
                                                BinaryReader br = new BinaryReader(fs);
                                                CurrentDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
                                                fs.Dispose();
                                                if (!CurrentDesignFile.Update(sTransaction, sConnection))
                                                {
                                                    throw new System.Exception("CurrentDesignFile.Update 6");
                                                }


                                                if (!currentDesignProfile.UpdateByDesignId(sTransaction, sConnection))
                                                {
                                                    throw new System.Exception("currentDesignProfile.Update 6");
                                                }
                                                MessageBox.Show("well done 6");
                                            }
                                        }
                                        else
                                        {
                                            ed.WriteMessage("~~~ does not have file ~~~\n");
                                            Atend.Base.Design.DDesignProfile currentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
                                            if (currentDesignProfile.DesignId != -1)
                                            {
                                                currentDesignProfile.DesignId = Convert.ToInt32(gvDesign.SelectedRows[0].Cells["Id"].Value);
                                                if (!currentDesignProfile.AccessUpdate(aTransaction, aConnection))
                                                {
                                                    throw new System.Exception("currentDesignProfile.AccessUpdate 5");
                                                }

                                                Atend.Base.Design.DDesignFile NewDesignFile = new Atend.Base.Design.DDesignFile();
                                                NewDesignFile.DesignId = Convert.ToInt32(gvDesign.SelectedRows[0].Cells["Id"].Value);
                                                NewDesignFile.File = null;
                                                FileStream fs;
                                                fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
                                                BinaryReader br = new BinaryReader(fs);
                                                NewDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
                                                fs.Dispose();
                                                if (!NewDesignFile.Insert(sTransaction, sConnection))
                                                {
                                                    throw new System.Exception("NewDesignFile.Insert 5");
                                                }


                                                if (!currentDesignProfile.Insert(sTransaction, sConnection))
                                                {
                                                    throw new System.Exception("currentDesignProfile.Insert 5");
                                                }

                                                MessageBox.Show("well done 5");
                                            }
                                        }

                                    }
                                }
                            }
                            else /////////////////////////////////////////////////////////////////////////////////
                            {
                                //طرح کد نداره و در پشتیبان طرحی نداره پی یک طرح جدید تولید می کنیم
                                ed.WriteMessage("~~~ No design Id  and is atend file ~~~\n");
                                Atend.Base.Design.DDesignProfile currentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
                                Atend.Base.Design.DDesign currentDesign = new Atend.Base.Design.DDesign();
                                currentDesign.Code = 0;
                                currentDesign.Title = currentDesignProfile.DesignName;
                                currentDesign.ArchiveNo = currentDesignProfile.DesignCode;
                                currentDesign.PRGCode = 0;
                                currentDesign.RequestType = 0;
                                currentDesign.AdditionalCode = 0;
                                if (!currentDesign.Insert(sTransaction, sConnection))
                                {
                                    throw new System.Exception("currentDesign.Insert 1");
                                }

                                currentDesignProfile.DesignId = currentDesign.Id;
                                if (!currentDesignProfile.AccessUpdate(aTransaction, aConnection))
                                {
                                    throw new System.Exception("currentDesignProfile.AccessUpdate 1");
                                }

                                Atend.Base.Design.DDesignFile currentDesignFile = new Atend.Base.Design.DDesignFile();
                                currentDesignFile.DesignId = currentDesign.Id;
                                currentDesignFile.File = null;
                                FileStream fs;
                                fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
                                BinaryReader br = new BinaryReader(fs);
                                currentDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
                                fs.Dispose();
                                if (!currentDesignFile.Insert(sTransaction, sConnection))
                                {
                                    throw new System.Exception("currentDesignFile.Insert 1");
                                }

                                if (!currentDesignProfile.Insert(sTransaction, sConnection))
                                {
                                    throw new System.Exception("currentDesignProfile.Insert 1");
                                }

                                MessageBox.Show("Well Done 1");
                            }
                        }
                        else////////////////////////////////////// ALL DESIGN HAVE DESIGN ID ///////////////////////////////////////
                        {
                            //means Design has DesignId
                            MessageBox.Show(Atend.Control.Common.DesignId.ToString());
                            if (chkIsComplement.Checked)
                            {
                                //means design is complement
                                Atend.Base.Design.DDesign currentDessign = Atend.Base.Design.DDesign.SelectByID(sTransaction, sConnection, Atend.Control.Common.DesignId);
                                if (currentDessign.Id != -1)
                                {
                                    DataTable Designs = Atend.Base.Design.DDesign.SelectByCode(sTransaction, sConnection, currentDessign.Code);
                                    if (Designs.Rows.Count > 0)
                                    {
                                        DataView dv = new DataView(Designs);
                                        dv.Sort = "RequestType";
                                        Atend.Base.Design.DDesign NewDesign = Atend.Base.Design.DDesign.SelectByID(sTransaction, sConnection, Convert.ToInt32(dv.Table.Rows[0]["Id"].ToString()));
                                        if (NewDesign.Id != -1)
                                        {
                                            NewDesign.RequestType++;
                                            if (!NewDesign.Insert(sTransaction, sConnection))
                                            {
                                                throw new System.Exception("NewDesign.Insert 3");
                                            }

                                            Atend.Base.Design.DDesignProfile currentProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
                                            if (currentProfile.DesignId != -1)
                                            {
                                                currentProfile.DesignId = NewDesign.Id;
                                                if (!currentProfile.AccessUpdate(aTransaction, aConnection))
                                                {
                                                    throw new System.Exception("currentProfile.AccessUpdate 3");
                                                }

                                                Atend.Base.Design.DDesignFile NewDesignFile = new Atend.Base.Design.DDesignFile();
                                                NewDesignFile.DesignId = NewDesign.Id;
                                                NewDesignFile.File = null;
                                                FileStream fs;
                                                fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
                                                BinaryReader br = new BinaryReader(fs);
                                                NewDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
                                                fs.Dispose();
                                                if (!NewDesignFile.Insert(sTransaction, sConnection))
                                                {
                                                    throw new System.Exception("NewDesignFile.Insert 3");
                                                }

                                                if (!currentProfile.Insert(sTransaction, sConnection))
                                                {
                                                    throw new System.Exception("currentProfile.Insert 3");
                                                }
                                                MessageBox.Show("Well done 3");
                                            }


                                        }
                                    }
                                }////////
                            }
                            else
                            {
                                //means design is not complement
                                Atend.Base.Design.DDesignFile CurrentDesignFile = Atend.Base.Design.DDesignFile.SelectByDesignId(sTransaction, sConnection, Atend.Control.Common.DesignId);
                                if (CurrentDesignFile.Id != -1)
                                {
                                    ed.WriteMessage("~~~  have file ~~~\n");
                                    Atend.Base.Design.DDesignProfile currentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
                                    if (currentDesignProfile.DesignId != -1)
                                    {
                                        CurrentDesignFile.File = null;
                                        FileStream fs;
                                        fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
                                        BinaryReader br = new BinaryReader(fs);
                                        CurrentDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
                                        fs.Dispose();
                                        if (!CurrentDesignFile.Update(sTransaction, sConnection))
                                        {
                                            throw new System.Exception("CurrentDesignFile.Update 4");
                                        }

                                        if (!currentDesignProfile.UpdateByDesignId(sTransaction, sConnection))
                                        {
                                            throw new System.Exception("currentDesignProfile.Update 4");
                                        }
                                        MessageBox.Show("well done 4");
                                    }
                                    else
                                    {
                                        throw new System.Exception("DDesignProfile access record was not found");
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("~~~ does not have file ~~~\n");
                                    Atend.Base.Design.DDesignProfile currentDesignProfile = Atend.Base.Design.DDesignProfile.AccessSelect(aTransaction, aConnection);
                                    if (currentDesignProfile.DesignId != -1)
                                    {
                                    

                                        Atend.Base.Design.DDesignFile NewDesignFile = new Atend.Base.Design.DDesignFile();
                                        NewDesignFile.DesignId = Atend.Control.Common.DesignId;
                                        NewDesignFile.File = null;
                                        FileStream fs;
                                        fs = new FileStream(string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, Atend.Control.Common.DesignName.Replace(".DWG", ".ATNX")), FileMode.Open);
                                        BinaryReader br = new BinaryReader(fs);
                                        NewDesignFile.File = br.ReadBytes((Int32)br.BaseStream.Length);
                                        fs.Dispose();
                                        if (!NewDesignFile.Insert(sTransaction, sConnection))
                                        {
                                            throw new System.Exception("NewDesignFile.Insert 5");
                                        }

                                        if (!currentDesignProfile.Insert(sTransaction, sConnection))
                                        {
                                            throw new System.Exception("currentDesignProfile.Insert 5");
                                        }

                                        MessageBox.Show("well done 4-2");
                                    }


                                }
                            }
                        }
                    }

                    aTransaction.Commit();
                    sTransaction.Commit();

                    aConnection.Close();
                    sConnection.Close();
                }
                catch
                {

                    aTransaction.Rollback();
                    sTransaction.Rollback();

                    aConnection.Close();
                    sConnection.Close();


                    ed.WriteMessage("ERROR WHILE SAVE FILE ON SERVER");
                    //AllowToClose = false;
                }
                DataTable dt = Atend.Base.Design.DDesign.SelectAll();
                gvDesign.AutoGenerateColumns = false;
                gvDesign.DataSource = dt;

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
    }
}