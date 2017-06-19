using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;

namespace Atend.Global.Calculation.Mechanical
{
    public class CAutoPoleInstallation02
    {

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~ properties ~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        Point3dCollection SpanPoints = new Point3dCollection();
        Point3dCollection SectionPoints = new Point3dCollection();

        Atend.Base.Equipment.EPole selectedPole;
        public Atend.Base.Equipment.EPole SelectedPole
        {
            get { return selectedPole; }
            set { selectedPole = value; }
        }

        Atend.Base.Design.DBranch selectedBranch;
        public Atend.Base.Design.DBranch SelectedBranch
        {
            get { return selectedBranch; }
            set { selectedBranch = value; }
        }

        /// <summary>
        /// برای این خصوصیت عبوری اختصاص داده می شود
        /// </summary>
        Atend.Base.Equipment.EConsol selectedConsol;
        public Atend.Base.Equipment.EConsol SelectedConsol
        {
            get { return selectedConsol; }
            set { selectedConsol = value; }
        }

        Atend.Base.Equipment.EConsol selectedConsolTension;
        public Atend.Base.Equipment.EConsol SelectedConsolTension
        {
            get { return selectedConsolTension; }
            set { selectedConsolTension = value; }
        }

        /// <summary>
        /// برای این خصوصیت عبوری اختصاص داده می شود
        /// </summary>
        Atend.Base.Equipment.EClamp selectedClamp;
        public Atend.Base.Equipment.EClamp SelectedClamp
        {
            get { return selectedClamp; }
            set { selectedClamp = value; }
        }

        Atend.Base.Equipment.EClamp selectedClampTension;
        public Atend.Base.Equipment.EClamp SelectedClampTension
        {
            get { return selectedClampTension; }
            set { selectedClampTension = value; }
        }

        /// <summary>
        /// طول اسپن
        /// </summary>
        double se;
        public double Se
        {
            get { return se; }
            set { se = value; }
        }

        /// <summary>
        /// یو تی اس درصد
        /// </summary>
        double uTS;
        public double UTS
        {
            get { return uTS; }
            set { uTS = value; }
        }

        /// <summary>
        /// کد محل عبور شبکه
        /// </summary>
        int netCrossCode;
        public int NetCrossCode
        {
            get { return netCrossCode; }
            set { netCrossCode = value; }
        }

        /// <summary>
        /// حداکثر طول سکشن
        /// </summary>
        double maxSectionLength;
        public double MaxSectionLength
        {
            get { return maxSectionLength; }
            set { maxSectionLength = value; }
        }

        /// <summary>
        /// درصد مجاز تغییرات طول اسپن
        /// </summary>
        double changePercent;
        public double ChangePercent
        {
            get { return changePercent; }
            set { changePercent = value; }
        }

        /// <summary>
        /// حاشیه اطمینان
        /// </summary>
        double relibility;
        public double Relibility
        {
            get { return relibility; }
            set { relibility = value; }
        }

        Line branchEntity;
        public Line BranchEntity
        {
            get { return branchEntity; }
            set { branchEntity = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~ methods ~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool PoleInstallationWithoutForbiddenArea(Guid SelectedPoleDpackageCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            bool Answer = true;
            List<ObjectIdCollection> _AllConsols = new List<ObjectIdCollection>();
            ObjectIdCollection _AllClamps = new ObjectIdCollection();

            double _SectionLength = 0;
            double _SpanLength = 0;
            int _SectionCount = SectionCalculation(out _SectionLength);
            int _SpanCount = SpanCalculation(_SectionLength, out _SpanLength);
            Atend.Base.Acad.AT_INFO CurrentBranchInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(BranchEntity.ObjectId);
            ObjectIdCollection t = new ObjectIdCollection();

            try
            {
                Atend.Global.Acad.DrawEquips.AcDrawPole _AcDrawPole = null;
                Atend.Global.Acad.DrawEquips.AcDrawCirclePole _AcDrawCirclePole = null;
                Atend.Global.Acad.DrawEquips.AcDrawPolygonPole _AcDrawPolygonPole = null;

                if (_SpanLength != -1)
                {

                    CalculateSectionPoints(branchEntity.StartPoint, branchEntity.EndPoint, _SectionCount);
                    Atend.Base.Design.DPackage PolePack = Atend.Base.Design.DPackage.AccessSelectByNodeCode(SelectedPoleDpackageCode);

                    //Start Pole consols or clamp
                    if (SelectedBranch.ProductType == (int)Atend.Control.Enum.ProductType.Conductor)
                    {
                        t.Add(GetStartConsolOrClamp());
                        _AllConsols.Add(t);
                    }
                    else
                    {
                        _AllClamps.Add(GetStartConsolOrClamp());
                    }


                    for (int SectionCounter = 0; SectionCounter < _SectionCount; SectionCounter++)
                    {
                        SpanPoints.Clear();
                        CalculateSpanPoints(SectionPoints[SectionCounter], SectionPoints[SectionCounter + 1], _SpanCount);


                        #region Put data in AcadGlobal
                        if (SelectedPole.Shape == 0)
                        {
                            //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~POLECircle~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                            #region POLECircle
                            Atend.Base.Acad.AcadGlobal.CirclePoleData.UseAccess = true;
                            Atend.Base.Acad.AcadGlobal.CirclePoleData.ePole = SelectedPole;
                            Atend.Base.Acad.AcadGlobal.CirclePoleData.ProjectCode = PolePack.ProjectCode;
                            Atend.Base.Acad.AcadGlobal.CirclePoleData.Existance = Convert.ToByte(PolePack.IsExistance);
                            Atend.Base.Acad.AcadGlobal.CirclePoleData.Height = 0;

                            //Atend.Base.Acad.AcadGlobal.CirclePoleData.dPoleInfo = PoleInfo;

                            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsols.Add(SelectedConsol);
                            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolCount.Add(2);
                            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));
                            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolProjectCode.Add(PolePack.ProjectCode);
                            Atend.Base.Acad.AcadGlobal.CirclePoleData.eConsolUseAccess.Add(false);

                            Atend.Base.Acad.AcadGlobal.CirclePoleData.eHalterCount = 0;

                            //if (!_AcDrawCirclePole.DrawPoleCircle(SpanPoints[SpanCounter], GetStartPoleAngle(), out t))
                            //{
                            //    throw new System.Exception("failor while drawing pole");
                            //}
                            //_AllConsols.Add(t);
                            #endregion
                        }
                        else if (SelectedPole.Shape == 1)
                        {
                            if (SelectedPole.Type == 2)//pertic
                            {
                                //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~PolePolygon~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                                _AcDrawPolygonPole = new Atend.Global.Acad.DrawEquips.AcDrawPolygonPole();
                                #region PolePolygon
                                _AcDrawPolygonPole.UseAccess = true;
                                _AcDrawPolygonPole.ePole = SelectedPole;
                                _AcDrawPolygonPole.ProjectCode = PolePack.ProjectCode;
                                _AcDrawPolygonPole.Existance = Convert.ToByte(PolePack.IsExistance);

                                //_AcDrawPolygonPole.dPoleInfo = PoleInfo;

                                _AcDrawPolygonPole.eConsols.Add(SelectedConsol);
                                _AcDrawPolygonPole.eConsolCount.Add(2);
                                _AcDrawPolygonPole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));
                                _AcDrawPolygonPole.eConsolProjectCode.Add(PolePack.ProjectCode);
                                _AcDrawPolygonPole.eConsolUseAccess.Add(false);

                                _AcDrawPolygonPole.eHalterCount = 0;

                                //if (!_AcDrawPolygonPole.DrawPolePolygon(SpanPoints[SpanCounter], GetStartPoleAngle(), out t))
                                //{
                                //    throw new System.Exception("failor while drawing pole");
                                //}
                                //_AllConsols.Add(t);
                                #endregion
                            }
                            else
                            {
                                //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~Pole~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                                _AcDrawPole = new Atend.Global.Acad.DrawEquips.AcDrawPole();
                                #region Pole
                                _AcDrawPole.UseAccess = true;
                                _AcDrawPole.ePole = SelectedPole;
                                _AcDrawPole.ProjectCode = PolePack.ProjectCode;
                                _AcDrawPole.Existance = Convert.ToByte(PolePack.IsExistance);

                                //_AcDrawPole.dPoleInfo = PoleInfo;

                                _AcDrawPole.eConsols.Add(SelectedConsol);
                                _AcDrawPole.eConsolCount.Add(2);
                                _AcDrawPole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));
                                _AcDrawPole.eConsolProjectCode.Add(PolePack.ProjectCode);
                                _AcDrawPole.eConsolUseAccess.Add(false);

                                _AcDrawPole.eHalterCount = 0;

                                //if (!_AcDrawPole.DrawPole(SpanPoints[SpanCounter], GetStartPoleAngle(), out t))
                                //{
                                //    throw new System.Exception("failor while drawing pole");
                                //}
                                //_AllConsols.Add(t);
                                #endregion
                            }
                        }
                        #endregion


                        for (int SpanCounter = 1; SpanCounter < _SpanCount; SpanCounter++)
                        {
                            Atend.Base.Design.DPoleInfo PoleInfo = new Atend.Base.Design.DPoleInfo();
                            PoleInfo.HalterCount = 0;
                            PoleInfo.HalterType = 0;
                            PoleInfo.Factor = 0;
                            if (SelectedBranch.ProductType == (int)Atend.Control.Enum.ProductType.Conductor)
                            {
                                #region Counductor
                                //ed.WriteMessage("~~~~~~~~{0}~~~~~~~~~~~~~~GO TO SHAPE~~~~~~~~~~~{1}~~~~~~~~~~~~~~\n", SelectedPole.Shape, SelectedPole.Type);
                                if (SelectedPole.Shape == 0)
                                {
                                    //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~TWO~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                                    _AcDrawCirclePole = new Atend.Global.Acad.DrawEquips.AcDrawCirclePole();
                                    #region POLECircle
                                    _AcDrawCirclePole.UseAccess = true;
                                    _AcDrawCirclePole.ePole = SelectedPole;
                                    _AcDrawCirclePole.ProjectCode = PolePack.ProjectCode;
                                    _AcDrawCirclePole.Existance = Convert.ToByte(PolePack.IsExistance);

                                    _AcDrawCirclePole.dPoleInfo = PoleInfo;

                                    _AcDrawCirclePole.eConsols.Add(SelectedConsol);
                                    _AcDrawCirclePole.eConsolCount.Add(2);
                                    _AcDrawCirclePole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));
                                    _AcDrawCirclePole.eConsolProjectCode.Add(PolePack.ProjectCode);
                                    _AcDrawCirclePole.eConsolUseAccess.Add(false);

                                    _AcDrawCirclePole.eHalterCount = 0;

                                    if (!_AcDrawCirclePole.DrawPoleCircle(SpanPoints[SpanCounter], GetStartPoleAngle(), out t))
                                    {
                                        throw new System.Exception("failor while drawing pole");
                                    }
                                    _AllConsols.Add(t);
                                    #endregion
                                }
                                else if (SelectedPole.Shape == 1)
                                {
                                    if (SelectedPole.Type == 2)//pertic
                                    {
                                        //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~THREE~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                                        _AcDrawPolygonPole = new Atend.Global.Acad.DrawEquips.AcDrawPolygonPole();
                                        #region PolePolygon
                                        _AcDrawPolygonPole.UseAccess = true;
                                        _AcDrawPolygonPole.ePole = SelectedPole;
                                        _AcDrawPolygonPole.ProjectCode = PolePack.ProjectCode;
                                        _AcDrawPolygonPole.Existance = Convert.ToByte(PolePack.IsExistance);

                                        _AcDrawPolygonPole.dPoleInfo = PoleInfo;

                                        _AcDrawPolygonPole.eConsols.Add(SelectedConsol);
                                        _AcDrawPolygonPole.eConsolCount.Add(2);
                                        _AcDrawPolygonPole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));
                                        _AcDrawPolygonPole.eConsolProjectCode.Add(PolePack.ProjectCode);
                                        _AcDrawPolygonPole.eConsolUseAccess.Add(false);

                                        _AcDrawPolygonPole.eHalterCount = 0;

                                        if (!_AcDrawPolygonPole.DrawPolePolygon(SpanPoints[SpanCounter], GetStartPoleAngle(), out t))
                                        {
                                            throw new System.Exception("failor while drawing pole");
                                        }
                                        _AllConsols.Add(t);
                                        #endregion
                                    }
                                    else
                                    {
                                        //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~ONE~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                                        _AcDrawPole = new Atend.Global.Acad.DrawEquips.AcDrawPole();
                                        #region Pole
                                        _AcDrawPole.UseAccess = true;
                                        _AcDrawPole.ePole = SelectedPole;
                                        _AcDrawPole.ProjectCode = PolePack.ProjectCode;
                                        _AcDrawPole.Existance = Convert.ToByte(PolePack.IsExistance);

                                        _AcDrawPole.dPoleInfo = PoleInfo;

                                        _AcDrawPole.eConsols.Add(SelectedConsol);
                                        _AcDrawPole.eConsolCount.Add(2);
                                        _AcDrawPole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));
                                        _AcDrawPole.eConsolProjectCode.Add(PolePack.ProjectCode);
                                        _AcDrawPole.eConsolUseAccess.Add(false);

                                        _AcDrawPole.eHalterCount = 0;

                                        if (!_AcDrawPole.DrawPole(SpanPoints[SpanCounter], GetStartPoleAngle(), out t))
                                        {
                                            throw new System.Exception("failor while drawing pole");
                                        }
                                        _AllConsols.Add(t);
                                        #endregion
                                    }
                                }
                                #endregion

                            }//end of if (SelectedBranch.ProductType == (int)Atend.Control.Enum.ProductType.Conductor)
                            else if (SelectedBranch.ProductType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                            {
                                #region Selfkeeper
                                if (SelectedPole.Shape == 0)
                                {
                                    //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~TWO~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                                    _AcDrawCirclePole = new Atend.Global.Acad.DrawEquips.AcDrawCirclePole();
                                    #region POLECircle
                                    _AcDrawCirclePole.UseAccess = true;
                                    _AcDrawCirclePole.ePole = SelectedPole;
                                    _AcDrawCirclePole.ProjectCode = PolePack.ProjectCode;
                                    _AcDrawCirclePole.Existance = Convert.ToByte(PolePack.IsExistance);

                                    _AcDrawCirclePole.dPoleInfo = PoleInfo;

                                    _AcDrawCirclePole.eConsols = new List<Atend.Base.Equipment.EConsol>();
                                    _AcDrawCirclePole.eConsolCount = new ArrayList();
                                    _AcDrawCirclePole.eConsolExistance = new ArrayList();
                                    _AcDrawCirclePole.eConsolProjectCode = new ArrayList();
                                    _AcDrawCirclePole.eConsolUseAccess = new ArrayList();

                                    _AcDrawCirclePole.eHalterCount = 0;

                                    if (!_AcDrawCirclePole.DrawPoleCircle(SpanPoints[SpanCounter], GetStartPoleAngle(), out t))
                                    {
                                        throw new System.Exception("failor while drawing pole");
                                    }
                                    //_AllClamps.Add(t[0]);
                                    #endregion
                                }
                                else if (SelectedPole.Shape == 1)
                                {
                                    if (SelectedPole.Type == 2)//pertic
                                    {
                                        //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~THREE~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                                        _AcDrawPolygonPole = new Atend.Global.Acad.DrawEquips.AcDrawPolygonPole();
                                        #region PolePolygon
                                        _AcDrawPolygonPole.UseAccess = true;
                                        _AcDrawPolygonPole.ePole = SelectedPole;
                                        _AcDrawPolygonPole.ProjectCode = PolePack.ProjectCode;
                                        _AcDrawPolygonPole.Existance = Convert.ToByte(PolePack.IsExistance);

                                        _AcDrawPolygonPole.dPoleInfo = PoleInfo;

                                        _AcDrawPolygonPole.eConsols = new List<Atend.Base.Equipment.EConsol>();
                                        _AcDrawPolygonPole.eConsolCount = new ArrayList();
                                        _AcDrawPolygonPole.eConsolExistance = new ArrayList();
                                        _AcDrawPolygonPole.eConsolProjectCode = new ArrayList();
                                        _AcDrawPolygonPole.eConsolUseAccess = new ArrayList();

                                        _AcDrawPolygonPole.eHalterCount = 0;

                                        if (!_AcDrawPolygonPole.DrawPolePolygon(SpanPoints[SpanCounter], GetStartPoleAngle(), out t))
                                        {
                                            throw new System.Exception("failor while drawing pole");
                                        }
                                        //_AllClamps.Add(t[0]);
                                        #endregion
                                    }
                                    else
                                    {
                                        //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~ONE~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                                        _AcDrawPole = new Atend.Global.Acad.DrawEquips.AcDrawPole();
                                        #region Pole
                                        _AcDrawPole.UseAccess = true;
                                        _AcDrawPole.ePole = SelectedPole;
                                        _AcDrawPole.ProjectCode = PolePack.ProjectCode;
                                        _AcDrawPole.Existance = Convert.ToByte(PolePack.IsExistance);

                                        _AcDrawPole.dPoleInfo = PoleInfo;

                                        _AcDrawPole.eConsols = new List<Atend.Base.Equipment.EConsol>();
                                        _AcDrawPole.eConsolCount = new ArrayList();
                                        _AcDrawPole.eConsolExistance = new ArrayList();
                                        _AcDrawPole.eConsolProjectCode = new ArrayList();
                                        _AcDrawPole.eConsolUseAccess = new ArrayList();

                                        _AcDrawPole.eHalterCount = 0;

                                        if (!_AcDrawPole.DrawPole(SpanPoints[SpanCounter], GetStartPoleAngle(), out t))
                                        {
                                            throw new System.Exception("failor while drawing pole");
                                        }
                                        //_AllClamps.Add(t[0]);
                                        #endregion
                                    }
                                }
                                #endregion


                                Atend.Global.Acad.DrawEquips.AcDrawKalamp _AcDrawKalamp = new Atend.Global.Acad.DrawEquips.AcDrawKalamp();
                                _AcDrawKalamp.eClamp = SelectedClamp;
                                _AcDrawKalamp.Existance = PolePack.IsExistance;
                                _AcDrawKalamp.ProjectCode = PolePack.ProjectCode;
                                _AcDrawKalamp.UseAccess = false;
                                _AllClamps.Add(_AcDrawKalamp.DrawKalamp(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(t[0])), t[0]));

                            }//end of else if (SelectedBranch.ProductType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                        }//end of span


                        if (SectionCounter + 1 != _SectionCount)
                        {
                            //ed.WriteMessage("~~~~~~~~~~~~<<<SECTION>>>~~~~~~~~~~~~~\n");
                            if (_AcDrawPole != null)
                            {
                                //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~ONE~~~~~~~~~~~~~~~~~~~~~~~~~\n");


                                if (CurrentBranchInfo.NodeType != (int)Atend.Control.Enum.ProductType.SelfKeeper)
                                {
                                    _AcDrawPole.eConsols = new List<Atend.Base.Equipment.EConsol>();
                                    _AcDrawPole.eConsols.Add(SelectedConsolTension);
                                    _AcDrawPole.eConsols.Add(SelectedConsolTension);

                                    _AcDrawPole.eConsolCount = new ArrayList();
                                    _AcDrawPole.eConsolCount.Add(2);
                                    _AcDrawPole.eConsolCount.Add(2);

                                    _AcDrawPole.eConsolExistance = new ArrayList();
                                    _AcDrawPole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));
                                    _AcDrawPole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));

                                    _AcDrawPole.eConsolProjectCode = new ArrayList();
                                    _AcDrawPole.eConsolProjectCode.Add(PolePack.ProjectCode);
                                    _AcDrawPole.eConsolProjectCode.Add(PolePack.ProjectCode);

                                    _AcDrawPole.eConsolUseAccess = new ArrayList();
                                    _AcDrawPole.eConsolUseAccess.Add(false);
                                    _AcDrawPole.eConsolUseAccess.Add(false);


                                }
                                else
                                {
                                    _AcDrawPole.eConsols = new List<Atend.Base.Equipment.EConsol>();
                                    _AcDrawPole.eConsolCount = new ArrayList();
                                    _AcDrawPole.eConsolExistance = new ArrayList();
                                    _AcDrawPole.eConsolProjectCode = new ArrayList();
                                    _AcDrawPole.eConsolUseAccess = new ArrayList();
                                }


                                _AcDrawPole.eHalterCount = 0;
                                if (!_AcDrawPole.DrawPole(SectionPoints[SectionCounter + 1], GetStartPoleAngle(), out t))
                                {
                                    throw new System.Exception("failor while drawing pole");
                                }
                            }
                            else if (_AcDrawCirclePole != null)
                            {
                                //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~TWO~~~~~~~~~~~~~~~~~~~~~~~~~\n");

                                if (CurrentBranchInfo.NodeType != (int)Atend.Control.Enum.ProductType.SelfKeeper)
                                {
                                    _AcDrawCirclePole.eConsols = new List<Atend.Base.Equipment.EConsol>();
                                    _AcDrawCirclePole.eConsols.Add(SelectedConsolTension);
                                    _AcDrawCirclePole.eConsols.Add(SelectedConsolTension);

                                    _AcDrawCirclePole.eConsolCount = new ArrayList();
                                    _AcDrawCirclePole.eConsolCount.Add(2);
                                    _AcDrawCirclePole.eConsolCount.Add(2);

                                    _AcDrawCirclePole.eConsolExistance = new ArrayList();
                                    _AcDrawCirclePole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));
                                    _AcDrawCirclePole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));

                                    _AcDrawCirclePole.eConsolProjectCode = new ArrayList();
                                    _AcDrawCirclePole.eConsolProjectCode.Add(PolePack.ProjectCode);
                                    _AcDrawCirclePole.eConsolProjectCode.Add(PolePack.ProjectCode);

                                    _AcDrawCirclePole.eConsolUseAccess = new ArrayList();
                                    _AcDrawCirclePole.eConsolUseAccess.Add(false);
                                    _AcDrawCirclePole.eConsolUseAccess.Add(false);
                                }
                                else
                                {
                                    _AcDrawCirclePole.eConsols = new List<Atend.Base.Equipment.EConsol>();
                                    _AcDrawCirclePole.eConsolCount = new ArrayList();
                                    _AcDrawCirclePole.eConsolExistance = new ArrayList();
                                    _AcDrawCirclePole.eConsolProjectCode = new ArrayList();
                                    _AcDrawCirclePole.eConsolUseAccess = new ArrayList();
                                }

                                _AcDrawCirclePole.eHalterCount = 0;
                                if (!_AcDrawCirclePole.DrawPoleCircle(SectionPoints[SectionCounter + 1], GetStartPoleAngle(), out t))
                                {
                                    throw new System.Exception("failor while drawing pole");
                                }
                            }
                            else if (_AcDrawPolygonPole != null)
                            {
                                //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~THREE~~~~~~~~~~~~~~~~~~~~~~~~~\n");


                                if (CurrentBranchInfo.NodeType != (int)Atend.Control.Enum.ProductType.SelfKeeper)
                                {
                                    _AcDrawPolygonPole.eConsols = new List<Atend.Base.Equipment.EConsol>();
                                    _AcDrawPolygonPole.eConsols.Add(SelectedConsolTension);
                                    _AcDrawPolygonPole.eConsols.Add(SelectedConsolTension);

                                    _AcDrawPolygonPole.eConsolCount = new ArrayList();
                                    _AcDrawPolygonPole.eConsolCount.Add(2);
                                    _AcDrawPolygonPole.eConsolCount.Add(2);

                                    _AcDrawPolygonPole.eConsolExistance = new ArrayList();
                                    _AcDrawPolygonPole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));
                                    _AcDrawPolygonPole.eConsolExistance.Add(Convert.ToByte(PolePack.IsExistance));

                                    _AcDrawPolygonPole.eConsolProjectCode = new ArrayList();
                                    _AcDrawPolygonPole.eConsolProjectCode.Add(PolePack.ProjectCode);
                                    _AcDrawPolygonPole.eConsolProjectCode.Add(PolePack.ProjectCode);

                                    _AcDrawPolygonPole.eConsolUseAccess = new ArrayList();
                                    _AcDrawPolygonPole.eConsolUseAccess.Add(false);
                                    _AcDrawPolygonPole.eConsolUseAccess.Add(false);
                                }
                                else
                                {
                                    _AcDrawPolygonPole.eConsols = new List<Atend.Base.Equipment.EConsol>();
                                    _AcDrawPolygonPole.eConsolCount = new ArrayList();
                                    _AcDrawPolygonPole.eConsolExistance = new ArrayList();
                                    _AcDrawPolygonPole.eConsolProjectCode = new ArrayList();
                                    _AcDrawPolygonPole.eConsolUseAccess = new ArrayList();
                                }

                                ////ed.WriteMessage("### BBB ### \n");
                                _AcDrawPolygonPole.eHalterCount = 0;
                                if (!_AcDrawPolygonPole.DrawPolePolygon(SectionPoints[SectionCounter + 1], GetStartPoleAngle(), out t))
                                {
                                    throw new System.Exception("failor while drawing pole");
                                }
                                //ed.WriteMessage("### GGG ### \n");
                            }

                            if (CurrentBranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                            {
                                _AllConsols.Add(t);
                            }
                            else if (CurrentBranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                            {

                                Atend.Global.Acad.DrawEquips.AcDrawKalamp _AcDrawKalamp = new Atend.Global.Acad.DrawEquips.AcDrawKalamp();
                                _AcDrawKalamp.eClamp = SelectedClampTension;
                                _AcDrawKalamp.Existance = PolePack.IsExistance;
                                _AcDrawKalamp.ProjectCode = PolePack.ProjectCode;
                                _AcDrawKalamp.UseAccess = false;
                                _AllClamps.Add(_AcDrawKalamp.DrawKalamp(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(t[0])), t[0]));

                            }
                            _AcDrawCirclePole = null;
                            _AcDrawPole = null;
                            _AcDrawPolygonPole = null;

                            // ed.WriteMessage("~~~~~~~~~~~~<<<NOT SECTION>>>~~~~~~~~~~~~~\n");
                        }//change section
                    }
                    //End Pole consols

                    if (CurrentBranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                    {

                        t = new ObjectIdCollection();
                        t.Add(GetEndConsolOrClamp());
                        _AllConsols.Add(t);
                    }
                    else if (CurrentBranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                    {
                        //_AllClamps.Add(t[0]);
                        _AllClamps.Add(GetEndConsolOrClamp());

                    }

                    #region Do Branch Operation Here

                    if (CurrentBranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                    {
                        #region Conductor Operation
                        //delete conductor
                        if (Atend.Global.Acad.DrawEquips.AcDrawConductor.DeleteConductorData(branchEntity.ObjectId))
                        {
                            Atend.Global.Acad.DrawEquips.AcDrawConductor.DeleteConductor(branchEntity.ObjectId);
                        }
                        //ed.WriteMessage("~~~~~~ start sim keshi ~~~~~~~~~\n");
                        ObjectId LastConsol = ObjectId.Null, StartConsol = ObjectId.Null;
                        for (int ii = 0; ii < _AllConsols.Count - 1; ii++)
                        {


                            //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~~~\n");
                            ObjectIdCollection o1 = _AllConsols[ii];
                            //foreach (ObjectId oii in o1)
                            //{
                            //    ed.WriteMessage("oi:{0}\n", oii);
                            //}


                            if (o1.Count == 1)
                            {
                                StartConsol = o1[0];
                            }
                            else
                            {
                                if (o1[1] == LastConsol)
                                {
                                    StartConsol = o1[0];
                                }
                                else
                                {
                                    StartConsol = o1[1];
                                }
                            }
                            ObjectIdCollection o2 = _AllConsols[ii + 1];
                            LastConsol = o2[0];
                            //ed.WriteMessage("Start:{0} , End:{1} \n", StartConsol, LastConsol);
                            Atend.Global.Acad.DrawEquips.AcDrawConductor _AcDrawConductor = new Atend.Global.Acad.DrawEquips.AcDrawConductor();
                            _AcDrawConductor.UseAccess = true;
                            //ed.WriteMessage("111 \n");



                            _AcDrawConductor.eConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(CurrentBranchInfo.ProductCode);
                            //ed.WriteMessage("112 \n");


                            ///////////_AcDrawConductor.eConductors.Add(Atend.Base.Equipment.EConductor.AccessSelectByCode(_AcDrawConductor.eConductorTip.NeutralProductCode));
                            //ed.WriteMessage("113 \n");


                            //////////_AcDrawConductor.eConductors.Add(Atend.Base.Equipment.EConductor.AccessSelectByCode(5));
                            //ed.WriteMessage("114 \n");


                            /////////_AcDrawConductor.eConductors.Add(Atend.Base.Equipment.EConductor.AccessSelectByCode(5));
                            //ed.WriteMessage("115 \n");


                            Atend.Base.Design.DBranch _DBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(CurrentBranchInfo.NodeCode));
                            if (_DBranch.Code != Guid.Empty)
                            {

                                _AcDrawConductor.Existance = _DBranch.IsExist;
                                //ed.WriteMessage("116 \n");


                                _AcDrawConductor.ProjectCode = _DBranch.ProjectCode;
                                //ed.WriteMessage("i will go to draw sim \n");
                            }
                            else
                            {
                                _AcDrawConductor.Existance = 0;
                                //ed.WriteMessage("116 \n");


                                _AcDrawConductor.ProjectCode = 0;
                                //ed.WriteMessage("i will go to draw sim \n");

                            }

                            _AcDrawConductor.DrawConductor(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(StartConsol)), Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConsol)),
                                StartConsol, LastConsol, _SpanLength);
                            //ed.WriteMessage("Start:{0} , End:{1} \n", StartConsol, LastConsol);
                        }
                        //ed.WriteMessage("~~~~~~ end sim keshi ~~~~~~~~~\n");
                        #endregion
                    }
                    else if (CurrentBranchInfo.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                    {
                        #region SelfKeeper Operation


                        if (Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper.DeleteSelfKeeperData(branchEntity.ObjectId))
                        {
                            //ed.WriteMessage("delete data io{0}\n", branchEntity.ObjectId);
                            if (Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper.DeleteSelfKeeper(branchEntity.ObjectId, true))
                            {
                                //ed.WriteMessage("delete graphic \n");

                                for (int ClampCounter = 0; ClampCounter < _AllClamps.Count - 1; ClampCounter++)
                                {
                                    //ed.WriteMessage("draw self keeper \n");

                                    //ed.WriteMessage("ANSWER={0} \n", oi);
                                    Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper _AcDrawSelfKeeper = new Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper();
                                    _AcDrawSelfKeeper.UseAccess = true;
                                    _AcDrawSelfKeeper.eSelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(SelectedBranch.ProductCode);
                                    _AcDrawSelfKeeper.Existance = SelectedBranch.IsExist;
                                    _AcDrawSelfKeeper.ProjectCode = SelectedBranch.ProjectCode;

                                    _AcDrawSelfKeeper.DrawSelfKeeper(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(_AllClamps[ClampCounter])), Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(_AllClamps[ClampCounter + 1])), _AllClamps[ClampCounter], _AllClamps[ClampCounter + 1], _SpanLength);
                                }

                            }
                            else
                            {
                                throw new System.Exception("while deleting self keeper");
                            }
                        }
                        else
                        {
                            throw new System.Exception("while deleting self keeper");
                        }

                        #endregion
                    }
                    #endregion

                }
                else
                {
                    ed.WriteMessage("طول اسپن قابل محاسبه نمی باشد" + "\n");
                    Answer = false;
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error While Pole installation : {0} \n", ex.Message);
                Answer = false;
            }
            return Answer;
        }

        private ObjectId GetStartConsolOrClamp()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ObjectId FoundOne = ObjectId.Null;
            Atend.Base.Acad.AT_SUB BranchSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(BranchEntity.ObjectId);
            foreach (ObjectId oi in BranchSub.SubIdCollection)
            {
                Atend.Base.Acad.AT_INFO SubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                if (SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Consol)
                {
                    if (Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi)) == branchEntity.StartPoint)
                    {
                        FoundOne = oi;
                    }

                }
                else if (SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
                {
                    if (Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi)) == branchEntity.StartPoint)
                    {
                        FoundOne = oi;
                    }
                }
            }
            ed.WriteMessage("Start node: {0} \n", FoundOne);
            return FoundOne;
        }

        private ObjectId GetEndConsolOrClamp()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ObjectId FoundOne = ObjectId.Null;
            Atend.Base.Acad.AT_SUB BranchSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(BranchEntity.ObjectId);
            foreach (ObjectId oi in BranchSub.SubIdCollection)
            {

                Atend.Base.Acad.AT_INFO SubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                if (SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Consol)
                {
                    if (Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi)) == branchEntity.EndPoint)
                    {
                        FoundOne = oi;
                    }

                }
                else if (SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
                {
                    if (Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi)) == branchEntity.EndPoint)
                    {
                        FoundOne = oi;
                    }
                }
            }
            ed.WriteMessage("End node: {0} \n", FoundOne);
            return FoundOne;
        }

        /// <summary>
        /// محاسبه تعداد اسپن و طول هر اسپن
        /// spanCount will return from method
        /// </summary>
        /// <param name="SelectedPathLength"></param>
        /// <param name="SpanLength"></param>
        /// <param name="MaxSpanLength"></param>
        private int SpanCalculation(double RealSectionLength, out double SpanLength)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SpanLength = -1;
            //ed.WriteMessage("--^^--ChangePercent:{0}\n", ChangePercent);
            double _MaxSpanLength = Math.Abs(Se + (Se / 100 * ChangePercent));
            double _MinSpanLength = Math.Abs(Se - (Se / 100 * ChangePercent));
            ed.WriteMessage("--^^--_MaxSpanLength:{0}\n", _MaxSpanLength);
            ed.WriteMessage("--^^--_MinSpanLength:{0}\n", _MinSpanLength);
            double SpanL = _MaxSpanLength;
            bool SpanLengthFound = false;

            while ((SpanL >= _MinSpanLength && SpanL <= _MaxSpanLength) && !SpanLengthFound)
            {
                //call mechanical calculation for suitable SpanLength
                //assign SpanLength here
                double _Clereance = 0;
                int _Voltage = 0;

                if (SelectedBranch.ProductType == (int)Atend.Control.Enum.ProductType.Conductor)
                {
                    _Voltage = SelectedConsol.VoltageLevel;
                }
                else if (SelectedBranch.ProductType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                {
                    _Voltage = SelectedClamp.VoltageLevel;
                }

                Atend.Base.Calculating.CNetWorkCross _CNetWorkCross = Atend.Base.Calculating.CNetWorkCross.AccessSelectByCode(NetCrossCode);
                if (_Voltage == 400)
                {
                    _Clereance = _CNetWorkCross.V380;
                }
                if (_Voltage == 11000)
                {
                    _Clereance = _CNetWorkCross.KV11;
                }
                if (_Voltage == 20000)
                {
                    _Clereance = _CNetWorkCross.KV20;
                }
                if (_Voltage == 33000)
                {
                    _Clereance = _CNetWorkCross.KV32;
                }


                //ed.WriteMessage("--^^--_Voltage:{0}\n", _Voltage);
                //ed.WriteMessage("--^^--SpanL:{0}\n", SpanL);
                //ed.WriteMessage("--^^--_Clereance:{0}\n", _Clereance);
                //ed.WriteMessage("--^^--Relibility:{0}\n", Relibility);
                //ed.WriteMessage("--^^--UTS:{0}\n", UTS);
                Atend.Global.Calculation.Mechanical.CalcOptimalSagTension _CalcOptimalSagTension = new CalcOptimalSagTension();
                if (_CalcOptimalSagTension.CalSagTension02(SelectedBranch, SpanL, SelectedConsol, SelectedClamp, SelectedPole, _Clereance, Relibility, UTS))
                {
                    SpanLength = SpanL;
                    SpanLengthFound = true;
                    //ed.WriteMessage("--------------\n");
                }
                _CalcOptimalSagTension.CloseConnection();
                SpanL = SpanL - 1;
            }
            int SpanCount = Convert.ToInt32(Math.Ceiling(RealSectionLength / SpanLength));
            //ed.WriteMessage("--^^-- BEFORE SpanLength:{0}\n", SpanLength);
            //SpanLength = RealSectionLength / SpanCount;
            ed.WriteMessage("--^^--SpanLength:{0}\n", SpanLength);
            ed.WriteMessage("--^^--SpanCount:{0}\n", SpanCount);
            return SpanCount;
        }

        /// <summary>
        /// محاسبه تعداد سکشن و طول هر سکشن
        /// sectionCount will return from method
        /// maxSectionLength will return from output parameter
        /// </summary>
        /// <param name="SelectedPathLength"></param>
        /// <param name="MaxSectionLength"></param>
        private int SectionCalculation(out double RealSectionLength)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            RealSectionLength = MaxSectionLength;
            int SectionCount = Convert.ToInt32(Math.Ceiling(SelectedBranch.Lenght / MaxSectionLength));

            if (SelectedBranch.Lenght > MaxSectionLength)
            {
                RealSectionLength = SelectedBranch.Lenght / SectionCount;
            }
            else
            {
                RealSectionLength = SelectedBranch.Lenght;
            }
            ed.WriteMessage("~~~>>>~~~ RealSectionLength:{0}\n", RealSectionLength);
            ed.WriteMessage("~~~>>>~~~ Count:{0}\n", SectionCount);
            return SectionCount;
        }

        private void CalculateSpanPoints(Point3d StartPoint, Point3d EndPoint, int SpanCount)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Vector3d Vect1 = EndPoint - StartPoint, norm1 = Vect1.GetNormal();
            double Length = Vect1.Length / SpanCount;

            //ed.WriteMessage("Length:{0}\n", Length);

            SpanPoints.Add(StartPoint);
            for (int i = 1; i < SpanCount; i++)
            {
                Point3d anotherPoint = StartPoint + (norm1 * Length * i);
                SpanPoints.Add(anotherPoint);
            }
            SpanPoints.Add(EndPoint);


        }

        private void CalculateSectionPoints(Point3d StartPoint, Point3d EndPoint, int SectionCount)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Vector3d Vect1 = EndPoint - StartPoint, norm1 = Vect1.GetNormal();
            double Length = Vect1.Length / SectionCount;

            //ed.WriteMessage("Length:{0}\n", Length);

            SectionPoints.Add(StartPoint);
            for (int i = 1; i < SectionCount; i++)
            {
                Point3d anotherPoint = StartPoint + (norm1 * Length * i);
                SectionPoints.Add(anotherPoint);
            }
            SectionPoints.Add(EndPoint);
        }

        private double GetStartPoleAngle()
        {
            double Angle = 0;
            Atend.Base.Acad.AT_SUB BranchSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(BranchEntity.ObjectId);
            foreach (ObjectId oi in BranchSub.SubIdCollection)
            {
                Atend.Base.Acad.AT_INFO SubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                if (SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Consol || SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
                {
                    Atend.Base.Acad.AT_SUB ConsolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
                    foreach (ObjectId oii in ConsolSub.SubIdCollection)
                    {
                        Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);
                        if (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole)
                        {
                            Angle = poleInfo.Angle;
                        }
                        else if (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip)
                        {
                            Angle = poleInfo.Angle;
                        }
                    }
                }
            }

            return Angle;
        }

    }
}
