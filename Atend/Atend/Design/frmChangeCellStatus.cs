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
    public partial class frmChangeCellStatus : Form
    {
        Guid NodeCode;
        System.Data.DataTable dtCell = new System.Data.DataTable();
        System.Data.DataColumn COL1 = new System.Data.DataColumn("CellGuid");
        System.Data.DataColumn COL2 = new System.Data.DataColumn("CellObjectId");
        System.Data.DataColumn COL3 = new System.Data.DataColumn("KeyObjectId");
        System.Data.DataTable CellAndKey = new System.Data.DataTable();
        Dictionary<Guid, ObjectId> MyDic = new Dictionary<Guid, ObjectId>();
        bool ForceToClose = false;

        public frmChangeCellStatus()
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

            CellAndKey.Columns.Add(COL1);
            CellAndKey.Columns.Add(COL2);
            CellAndKey.Columns.Add(COL3);


        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("I Am In The Ok\n");
            btnSelect.Focus();
            for (int i = 0; i < gvCellJAckPAnel.Rows.Count; i++)
            {
                //ed.WriteMessage("Code=" + gvCellJAckPAnel.Rows[i].Cells[0].Value + "\n");
                DataRow[] dr = dtCell.Select(" CellCode= '" + gvCellJAckPAnel.Rows[i].Cells[0].Value.ToString() + "'");
                //ed.WriteMessage("dr.Lenght= " + dr.Length + "\n");
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvCellJAckPAnel.Rows[i].Cells[2];

                ed.WriteMessage("~~~~~{0}\n", Convert.ToBoolean(chk.Value));
                //if (chk.Value.ToString() == "1" || chk.Value.ToString() == "-1")
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    //ed.WriteMessage("True\n");
                    //dr[0]["IsClosed"] = false;
                    dr[0]["IsClosed"] = 1;
                }
                else
                {
                    //ed.WriteMessage("False\n");
                    dr[0]["IsClosed"] = 0;
                    //dr[0]["IsClosed"] = true;
                }
                //ed.WriteMessage("After IsClose\n");
            }

            //foreach (DataRow dr in CellAndKey.Rows)
            //{
            //    ed.WriteMessage("cellguid:{0} , cellobjectid:{1} , keyobjectid:{2} \n", dr["CellGuid"], dr["CellObjectId"], dr["KeyObjectId"]);
            //}


            foreach (DataRow dr in dtCell.Rows)
            {
                Atend.Base.Design.DCellStatus cellStatus = new Atend.Base.Design.DCellStatus();
                cellStatus.CellCode = new Guid(dr["CellCode"].ToString());
                cellStatus.JackPanelCode = new Guid(NodeCode.ToString());
                //ed.WriteMessage("------- {0} \n",Convert.ToBoolean(dr["IsClosed"]));
                cellStatus.IsClosed = Convert.ToBoolean(dr["IsClosed"]);
                //ed.WriteMessage("Befor Update\n");
                ed.WriteMessage("&&&&& :{0}\n", cellStatus.IsClosed);
                if (cellStatus.AccessUpdate())
                {
                    //ed.WriteMessage("CellCode={0}\n", cellStatus.CellCode);
                    System.Data.DataRow[] drs = CellAndKey.Select(string.Format("CellGuid like '{0}'", cellStatus.CellCode));
                    if (drs.Length != 0 && drs[0]["KeyObjectId"].ToString() != "")
                    {
                        //ed.WriteMessage("***DRS={0}\n", drs[0]["KeyObjectId"].ToString());
                        Atend.Global.Acad.UAcad.ChangeKeyStatus(new ObjectId(new IntPtr(Convert.ToInt32(drs[0]["KeyObjectId"].ToString()))), cellStatus.IsClosed);
                    }
                }
                else
                {
                }
            }
            ed.WriteMessage("Finish\n");
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            #region Select
            this.Hide();
            PromptEntityOptions peo = new PromptEntityOptions("\nSelect MiddleJackPanel:\n");
            PromptEntityResult per = ed.GetEntity(peo);
            if (per.Status == PromptStatus.OK)
            {


                ObjectId GroupOI = Atend.Global.Acad.Global.GetEntityGroup(per.ObjectId);
                if (GroupOI != ObjectId.Null)
                {

                    Atend.Base.Acad.AT_INFO GroupInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(GroupOI);
                    if (GroupInfo.ParentCode != "NONE" && GroupInfo.NodeType == (int)Atend.Control.Enum.ProductType.MiddleJackPanel)
                    {


                        ObjectIdCollection OIC = Atend.Global.Acad.UAcad.GetGroupSubEntities(GroupOI);
                        foreach (ObjectId oi in OIC)
                        {
                            Atend.Base.Acad.AT_INFO SubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (SubInfo.ParentCode != "NONE" && SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Cell)
                            {
                                //ed.WriteMessage("NodeCode:{0} , CellOI:{1}\n", SubInfo.NodeCode, oi);
                                //MyDic.Add(new Guid(SubInfo.NodeCode), oi);
                                System.Data.DataRow NewRow1 = CellAndKey.NewRow();
                                NewRow1["CellGuid"] = new Guid(SubInfo.NodeCode);
                                NewRow1["CellObjectId"] = oi.ToString().Substring(1, oi.ToString().Length - 2);

                                Atend.Base.Acad.AT_SUB CellSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
                                foreach (ObjectId oii in CellSub.SubIdCollection)
                                {
                                    Atend.Base.Acad.AT_INFO KeyInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);
                                    if (KeyInfo.ParentCode != "NONE" && KeyInfo.NodeType == (int)Atend.Control.Enum.ProductType.Key)
                                    {
                                        NewRow1["KeyObjectId"] = oii.ToString().Substring(1, oii.ToString().Length - 2);
                                    }

                                }
                                CellAndKey.Rows.Add(NewRow1);

                            }
                        }

                        //foreach (System.Data.DataRow dr in CellAndKey.Rows)
                        //{

                        //    ed.WriteMessage("CG:{0},\nCOI:{1},\nKOI:{2}\n", dr["CellGuid"], dr["CellObjectId"], dr["KeyObjectId"]);

                        //}

                        NodeCode = new Guid(GroupInfo.NodeCode);
                        dtCell = Atend.Base.Design.DCellStatus.AccessSelectByJackPanelCode(NodeCode);
                        System.Data.DataColumn dcName = new System.Data.DataColumn("Name");
                        dtCell.Columns.Add(dcName);
                        foreach (DataRow dr in dtCell.Rows)
                        {
                            //ed.WriteMessage("CellCode:{0}\n", dr["CellCode"]);
                            Atend.Base.Design.DPackage package = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dr["CellCode"].ToString()));// SelectByCode(new Guid(dr["CellCode"].ToString()));
                            Atend.Base.Equipment.ECell cell = Atend.Base.Equipment.ECell.AccessSelectByCode(package.ProductCode);
                            dr["Name"] = cell.Name;
                        }
                        gvCellJAckPAnel.AutoGenerateColumns = false;
                        gvCellJAckPAnel.DataSource = dtCell;

                        //Atend.Calculating.frmTest t = new Atend.Calculating.frmTest();
                        //t.dataGridView5.DataSource = dtCell;
                        //t.ShowDialog();


                    }
                }
            }

            this.Show();
            #endregion


        }

        private void frmChangeCellStatus_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

        }
    }
}