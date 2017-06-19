using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.Collections;

namespace Atend.Global.Calculation.General
{
    public class General
    {
        // ~~~~Start Create Mechanical Array Here
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        public class AutocadSelectedList
        {

            private Guid productGuid;
            public Guid ProductGuid
            {
                get { return productGuid; }
                set { productGuid = value; }
            }

            private string poleNumber;
            public string PoleNumber
            {
                get { return poleNumber; }
                set { poleNumber = value; }
            }

            private double conductorAngle;
            public double ConductorAngle
            {
                get { return conductorAngle; }
                set { conductorAngle = value; }
            }

            private int productType;
            public int ProductType
            {
                get { return productType; }
                set { productType = value; }
            }


        }

        // ~~~~End Create Mechanical Array Here

        public class AutoCAdConductorList
        {
            private Guid conductorGuid;

            public Guid ConductorGuid
            {
                get { return conductorGuid; }
                set { conductorGuid = value; }
            }

            private ObjectId conductorObjectID;

            public ObjectId ConductorObjectID
            {
                get { return conductorObjectID; }
                set { conductorObjectID = value; }
            }
        }

        //*****************************Calc HAlter*******************************
        public enum ConsolType : byte
        {
            Entehai = 1,
            Kesheshi = 0,
            Oburi = 2,
            DP = 3
        }

        public enum ErrHalter
        {
            NoError,

            MaxHalter
        }

        //***************WithHalter**************
        public System.Data.DataTable CalcHalter(System.Data.DataTable dtForce, double X, double Y, double SaftyFactor, ArrayList _M, ArrayList _Count, int MaxHalterCount, out bool result, double SaftyFactorOburi)
        {
            System.Data.DataTable dtResult = new System.Data.DataTable();
            dtResult = dtForce.Copy();
            System.Data.DataColumn dcPower = new System.Data.DataColumn("Power");
            System.Data.DataColumn dcCount = new System.Data.DataColumn("Count");
            System.Data.DataColumn dcHalterPower = new System.Data.DataColumn("HalterPower");
            System.Data.DataColumn dcHalterCount = new System.Data.DataColumn("HalterCount");
            System.Data.DataColumn dcName = new System.Data.DataColumn("Name");
            System.Data.DataColumn dcHalterXCode = new System.Data.DataColumn("HalterXCode");

            dtResult.Columns.Add(dcPower);
            dtResult.Columns.Add(dcCount);
            dtResult.Columns.Add(dcHalterPower);
            dtResult.Columns.Add(dcHalterCount);
            dtResult.Columns.Add(dcName);
            dtResult.Columns.Add(dcHalterXCode);


            result = true;
            bool chkEntehai = false;
            bool chkKesheshi = false;
            bool chkOburi = false;
            bool chkDP = false;
            int Polecount = 0;

            double Distance = 0;
            double Depth = 0;
            double h1 = 0;
            double h2 = 0;
            double h3 = 0;
            double T = 0;
            double F = 0;
            double M = 0;
            //const double constValue = 60;
            System.Data.DataTable dtHalter = Atend.Base.Equipment.EHalter.SelectAllX();
            try
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    chkEntehai = false;
                    chkKesheshi = false;
                    chkOburi = false;
                    chkDP = false;
                    Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dr["DcPoleGuid"].ToString()));
                    Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dPack.ProductCode);

                    System.Data.DataTable dtConsol = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dPack.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
                    System.Data.DataTable dtCalamp = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dPack.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

                    if (dtConsol.Rows.Count > 0)
                    {
                        foreach (DataRow drConsol in dtConsol.Rows)
                        {
                            Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(drConsol["ProductCode"].ToString()));

                            if (Convert.ToByte(Consol.ConsolType) == Convert.ToByte(ConsolType.Entehai))
                            {
                                chkEntehai = true;

                                ed.WriteMessage("Entehai\n");
                            }
                            if (Convert.ToByte(Consol.ConsolType) == Convert.ToByte(ConsolType.Kesheshi))
                            {
                                chkKesheshi = true;
                                ed.WriteMessage("Kesheshi\n");

                            }

                            if (Convert.ToByte(Consol.ConsolType) == Convert.ToByte(ConsolType.Oburi))
                            {
                                chkOburi = true;
                                ed.WriteMessage("Oburi\n");

                            }
                            if (Convert.ToByte(Consol.ConsolType) == Convert.ToByte(ConsolType.DP))
                            {
                                chkDP = true;
                                ed.WriteMessage("DP\n");

                            }
                            Distance = Consol.DistanceCrossArm;
                            ed.WriteMessage("DistanceCrossArm={0}\n", Consol.DistanceCrossArm);
                        }

                    }

                    foreach (DataRow drCalamp in dtCalamp.Rows)
                    {
                        Atend.Base.Equipment.EClamp Calamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(drCalamp["ProductCode"].ToString()));
                        switch (Calamp.Type)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 6:

                                chkEntehai = true;
                                break;

                            case 5:
                                {
                                    chkOburi = true;
                                    break;
                                }
                            //default :
                            //    {
                            //        chkEntehai = true;
                            //    }


                        }


                        Distance = Calamp.DistanceSupport;
                    }



                    if (chkEntehai)
                    {
                        M = Convert.ToDouble(_M[0].ToString()) * Convert.ToDouble(_Count[0].ToString());
                        Polecount = Convert.ToInt32(_Count[0].ToString());
                        M *= SaftyFactor;
                    }
                    else if (chkKesheshi)
                    {
                        M = Convert.ToDouble(_M[2].ToString()) * Convert.ToDouble(_Count[2].ToString());
                        Polecount = Convert.ToInt32(_Count[2].ToString());
                        M *= SaftyFactor;
                    }
                    else if (chkOburi)
                    {
                        ed.WriteMessage("_M[1]={0}\n", _M[1].ToString());
                        M = Convert.ToDouble(_M[1].ToString()) * Convert.ToDouble(_Count[1].ToString());
                        Polecount = Convert.ToInt32(_Count[1].ToString());
                        M *= SaftyFactorOburi;
                    }
                    else if (chkDP)
                    {
                        M = Convert.ToDouble(_M[3].ToString()) * Convert.ToDouble(_Count[3].ToString());
                        Polecount = Convert.ToInt32(_Count[3].ToString());
                        M *= SaftyFactorOburi;
                    }
                    //ed.WriteMessage("M={0},chk={1}\n",M,chkOburi);
                    //M *= SaftyFactor;
                    //ed.WriteMessage("M saftyFactor={0}\n",M);
                    ed.WriteMessage("M={0}\n", M);
                    F = GetForceOnPole(dr);
                    ed.WriteMessage("F={0}\n", F);
                    Depth = .1 * pole.Height + .6;
                    h1 = pole.Height - (Depth + Distance);
                    ed.WriteMessage("h1={0}\n", h1.ToString());
                    h2 = Y;
                    ed.WriteMessage("h2={0}\n", h2.ToString());
                    h3 = X;
                    ed.WriteMessage("h3={0},Power={1}\n", h3.ToString(), pole.Power);

                    //DataRow drHalter = dtTable.NewRow();
                    T = (((F * h1) / (h1)) - M) * ((h1) / (h2 * Math.Sin(Math.Atan(h3 / h2))));

                    bool Answer = true;
                    DataRow drResult = GetPowerPoleAndHalter(T, M, dr, dtHalter, 0, Polecount, MaxHalterCount, out Answer);
                    if (Answer)
                    {
                        ed.WriteMessage("***HalterPower={0}\n", drResult["HalterPower"].ToString());
                        dr["HalterPower"] = drResult["HalterPower"].ToString();
                        dr["HalterCount"] = drResult["HalterCount"].ToString();
                        dr["Power"] = Convert.ToDouble(drResult["Power"].ToString()) / SaftyFactor;
                        dr["Count"] = drResult["Count"].ToString();
                        dr["HalterXCode"] = drResult["HalterXCode"].ToString();
                    }
                    else
                    {
                        result = false;
                    }
                    //dtTable.Rows.Add(drHalter);
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROOOOOOOOOOOOOOOOR:={0}\n", ex.Message);
            }

            return dtResult;

        }

        public double GetForceOnPole(DataRow drPoleForce)
        {
            double MaxForce = Convert.ToDouble(drPoleForce["DcNorm"].ToString());
            if (Convert.ToDouble(drPoleForce["DcIceHeavy"].ToString()) > MaxForce)
            {
                MaxForce = Convert.ToDouble(drPoleForce["DcIceHeavy"].ToString());
            }
            if (Convert.ToDouble(drPoleForce["DcWindSpeed"].ToString()) > MaxForce)
            {
                MaxForce = Convert.ToDouble(drPoleForce["DcWindSpeed"].ToString());
            }
            if (Convert.ToDouble(drPoleForce["DcMaxTemp"].ToString()) > MaxForce)
            {
                MaxForce = Convert.ToDouble(drPoleForce["DcMaxTemp"].ToString());
            }

            if (Convert.ToDouble(drPoleForce["DcMinTemp"].ToString()) > MaxForce)
            {
                MaxForce = Convert.ToDouble(drPoleForce["DcMinTemp"].ToString());
            }
            if (Convert.ToDouble(drPoleForce["DcWindIce"].ToString()) > MaxForce)
            {
                MaxForce = Convert.ToDouble(drPoleForce["DcWindIce"].ToString());
            }

            return MaxForce;
        }

        private DataRow GetPowerPoleAndHalter(double T, double PowerWithsaftyFactr, DataRow drPoleFoece, System.Data.DataTable dtHalter, int HalterCount, int PoleCount, double MaxHalterCount, out bool Answer)
        {
            //ed.WriteMessage("dtPower.Count={0}\n", dtPower.Rows.Count);
            //int i = 0;
            Answer = true;
            try
            {
                bool Continue = true;
                double HalterPowerWithLoad = 0;
                string HalterName = "";
                Guid HalterXCode = Guid.Empty;
                while (Continue)
                {
                    HalterCount++;
                    foreach (DataRow dr in dtHalter.Rows)
                    {
                        if (HalterCount <= MaxHalterCount)
                        {
                            if (T <= (HalterCount * Convert.ToDouble(dr["Power"].ToString())))
                            {
                                drPoleFoece["Name"] = dr["Name"].ToString();
                                drPoleFoece["HalterPower"] = dr["Power"].ToString();
                                drPoleFoece["HalterCount"] = HalterCount.ToString();
                                drPoleFoece["Power"] = PowerWithsaftyFactr;
                                drPoleFoece["Count"] = PoleCount.ToString();
                                drPoleFoece["HalterXCode"] = dr["XCode"].ToString();
                                Continue = false;
                                return drPoleFoece;
                            }
                        }
                        else
                        {
                            HalterPowerWithLoad = Convert.ToDouble(dr["Power"].ToString());
                            HalterName = dr["Name"].ToString();
                            HalterXCode = new Guid(dr["XCode"].ToString());
                            Continue = false;
                        }
                    }
                }
                Continue = true;
                while (Continue)
                {
                    PoleCount++;

                    if (T <= ((HalterCount * HalterPowerWithLoad) + (PoleCount * PowerWithsaftyFactr)))
                    {
                        drPoleFoece["Power"] = PowerWithsaftyFactr;
                        drPoleFoece["Count"] = PoleCount.ToString();
                        drPoleFoece["HalterPower"] = HalterPowerWithLoad;
                        drPoleFoece["HalterCount"] = HalterCount.ToString();
                        drPoleFoece["Name"] = HalterName;
                        drPoleFoece["HalterXCode"] = HalterXCode;
                        Continue = false;
                        return drPoleFoece;
                        //}
                    }
                    else
                    {
                        Continue = false;
                        Answer = false;
                    }
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("LOOP={0}\n", ex.Message);
            }

            //DataRow drRE = GetPowerPoleAndHalter(T, PowerWithsaftyFactr, drPoleFoece, dtHalter, HalterCount, PoleCount, MaxHalterCount + 1);
            return drPoleFoece;
        }

        //******************EndWithHalter*********************





        //*********************WithOutHalter*********************
        public System.Data.DataTable GetForceOnPole(System.Data.DataTable dtPoleForce, double SaftyFactor, double SaftyFactorOburi)
        {
            System.Data.DataTable dtResult = dtPoleForce.Copy();
            System.Data.DataTable dtPower = Atend.Base.Calculating.CPower.AccessSelect();
            System.Data.DataColumn dcPowerWithSaftyFactor = new System.Data.DataColumn("PowerWithSaftyFactor");
            dtPower.Columns.Add(dcPowerWithSaftyFactor);

            //foreach (DataRow dr in dtPower.Rows)
            //{
            //    dr["PowerWithSaftyFactor"] = Convert.ToDouble(dr["Power"].ToString()) * SaftyFactor;
            //}

            System.Data.DataColumn dcPower = new System.Data.DataColumn("DcPower");
            System.Data.DataColumn dcCount = new System.Data.DataColumn("DcCount");
            dtResult.Columns.Add(dcPower);
            dtResult.Columns.Add(dcCount);
            ed.WriteMessage("dtPower.Rows.Count={0}\n", dtPower.Rows.Count);
            if (dtPower.Rows.Count > 0)
            {
                foreach (DataRow drPoleForce in dtResult.Rows)
                {
                    double MaxForce = GetForceOnPole(drPoleForce);
                    //Convert.ToDouble(drPoleForce["DcNorm"].ToString());
                    //if (Convert.ToDouble(drPoleForce["DcIceHeavy"].ToString()) > MaxForce)
                    //{
                    //    MaxForce = Convert.ToDouble(drPoleForce["DcIceHeavy"].ToString());
                    //}
                    //if (Convert.ToDouble(drPoleForce["DcWindSpeed"].ToString()) > MaxForce)
                    //{
                    //    MaxForce = Convert.ToDouble(drPoleForce["DcWindSpeed"].ToString());
                    //}
                    //if (Convert.ToDouble(drPoleForce["DcMaxTemp"].ToString()) > MaxForce)
                    //{
                    //    MaxForce = Convert.ToDouble(drPoleForce["DcMaxTemp"].ToString());
                    //}

                    //if (Convert.ToDouble(drPoleForce["DcMinTemp"].ToString()) > MaxForce)
                    //{
                    //    MaxForce = Convert.ToDouble(drPoleForce["DcMinTemp"].ToString());
                    //}
                    //if (Convert.ToDouble(drPoleForce["DcWindIce"].ToString()) > MaxForce)
                    //{
                    //    MaxForce = Convert.ToDouble(drPoleForce["DcWindIce"].ToString());
                    //}

                    ed.WriteMessage("****MaxForce={0}\n", MaxForce);
                    DataRow drResult = GetPower(MaxForce, dtPower, drPoleForce, SaftyFactor, SaftyFactorOburi);
                    drPoleForce["DcPower"] = drResult["DcPower"].ToString();
                    drPoleForce["DcCount"] = drResult["DcCount"].ToString();

                }
            }
            return dtResult;

        }

        private DataRow GetPower(double MaxForce, System.Data.DataTable dtPower, DataRow drPoleFoece, double SaftyFactor, double SaftyFactorOburi)
        {
            ed.WriteMessage("dtPower.Count={0}\n", dtPower.Rows.Count);
            int i = 0;
            bool Continue = true;

            bool chkEntehai = false;
            bool chkKesheshi = false;
            bool chkOburi = false;
            bool chkDP = false;
            double SFactor = 0;
            Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(drPoleFoece["DcPoleGuid"].ToString()));
            System.Data.DataTable dtConsol = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dPack.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
            System.Data.DataTable dtCalamp = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dPack.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

            foreach (DataRow drConsol in dtConsol.Rows)
            {
                Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dtConsol.Rows[0]["ProductCode"].ToString()));

                if (Convert.ToByte(Consol.ConsolType) == Convert.ToByte(ConsolType.Entehai))
                {
                    chkEntehai = true;

                    ed.WriteMessage("Entehai\n");
                }
                if (Convert.ToByte(Consol.ConsolType) == Convert.ToByte(ConsolType.Kesheshi))
                {
                    chkKesheshi = true;
                    ed.WriteMessage("Kesheshi\n");

                }

                if (Convert.ToByte(Consol.ConsolType) == Convert.ToByte(ConsolType.Oburi))
                {
                    chkOburi = true;
                    ed.WriteMessage("Oburi\n");

                }
                if (Convert.ToByte(Consol.ConsolType) == Convert.ToByte(ConsolType.DP))
                {
                    chkDP = true;
                    ed.WriteMessage("DP\n");

                }
                //Distance = Consol.DistanceCrossArm;
                ed.WriteMessage("DistanceCrossArm={0}\n", Consol.DistanceCrossArm);
            }
            foreach (DataRow drCalamp in dtCalamp.Rows)
            {
                Atend.Base.Equipment.EClamp Calamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(drCalamp["ProductCode"].ToString()));
                switch (Calamp.Type)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 6:

                        chkEntehai = true;
                        break;

                    case 5:
                        {
                            chkOburi = true;
                            break;
                        }
                    //default :
                    //    {
                    //        chkEntehai = true;
                    //    }


                }


                //Distance = Calamp.DistanceSupport;
            }


            if (chkEntehai || chkKesheshi)
            {
                SFactor = SaftyFactor;
            }
            else if (chkOburi || chkDP)
            {
                SFactor = SaftyFactorOburi;
            }
            ed.WriteMessage("*********SaftyFactor={0}\n", SFactor);
            while (Continue)
            {
                i++;
                foreach (DataRow dr in dtPower.Rows)
                {

                    if (MaxForce <= (i * Convert.ToDouble(dr["Power"].ToString()) * SFactor))
                    {
                        drPoleFoece["DcPower"] = dr["Power"].ToString();
                        drPoleFoece["DcCount"] = i.ToString();
                        Continue = false;
                        return drPoleFoece;
                    }
                }
            }


            return drPoleFoece;
        }

        //***********************WithOutHalter***********************


        //******************************EndOfCalcHalter*****************************


        public double ComputeTotalWeight(Atend.Base.Design.DWeather weather)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByCode(cond.ProductCode);


            //ed.WriteMessage("#Weather.Code = " +weather.Code.ToString() + "\n");

            double iceWeight = Math.Round(913 * Math.PI * weather.IceDiagonal * (weather.IceDiagonal + Atend.Global.Calculation.Mechanical.CCommon.Diagonal) * 1e-6, 2);//Wic=913*3.14*i[i+d]*1e-6
            double windWeight = Math.Round(Math.Pow(weather.WindSpeed, 2) / 16 * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * 1e-3, 2);//Ww=v^2/16*(d+2i)*10^-3
            double totalWeight = Math.Round(Math.Sqrt(Math.Pow(Atend.Global.Calculation.Mechanical.CCommon.WC + iceWeight, 2) + Math.Pow(windWeight, 2)), 2);
            //ed.WriteMessage("###IceWeight= " + iceWeight.ToString() + "\n");
            //ed.WriteMessage("##WindWeight= " + windWeight.ToString() + "\n");
            // ed.WriteMessage("#TotalWeight= " + totalWeight.ToString() + "\n");
            return totalWeight;

        }

        //public double ComputeKe(byte conductorType, Atend.Control.Enum.Netform Form, Atend.Base.Equipment.EConductor conductor)
        //{
        //    double[,] ke ={ 
        //        {0.85,0.65,0.7},
        //        {0.85,0.65,0.7},
        //        {0.75,0.62,0.65},
        //        {0.75,0.62,0.65},
        //        {0.75,0.62,0.65},
        //        {0.85,0.65,0.7},
        //        {0.85,0.65,0.7},
        //        {0.75,0.62,0.65},
        //        {0.75,0.62,0.65}
        //    };
        //    int firstIndex = 0;
        //    int secondIndex = 0;
        //    if (Form == Atend.Control.Enum.Netform.Vertical)
        //        secondIndex = 0;
        //    else if (Form == Atend.Control.Enum.Netform.Horizontal)
        //        secondIndex = 1;
        //    else if (Form == Atend.Control.Enum.Netform.Triangular)
        //        secondIndex = 2;
        //    if (conductorType == 1)//Mesi
        //    {
        //        if (conductor.CrossSectionArea <= 16)
        //            firstIndex = 0;
        //        else if (conductor.CrossSectionArea <= 25)
        //            firstIndex = 1;
        //        else if (conductor.CrossSectionArea <= 35)
        //            firstIndex = 2;
        //        else if (conductor.CrossSectionArea <= 50)
        //            firstIndex = 3;
        //        else if (conductor.CrossSectionArea <= 70)
        //            firstIndex = 4;
        //    }
        //    else if (conductorType == 8)
        //        firstIndex = 5;
        //    else if (conductorType == 9)
        //        firstIndex = 6;
        //    else if (conductorType == 10)
        //        firstIndex = 7;
        //    else if (conductorType == 11)
        //        firstIndex = 8;
        //    return ke[firstIndex, secondIndex];
        //}

        public double Min(double a, double b)
        {
            if (a > b)
                return b;
            else
                return a;
        }

        public double findBiggestRealRoot(double a, double b, double c, double d, double initialGuess, double precision)
        {
            double[] coeff = new double[4] { d, c, b, a };
            double[] roots = new double[3];
            int numOfRoots = find3rdDegreePolyRoots(coeff, 3, roots, initialGuess, precision);

            return arrayMax(roots, numOfRoots);
        }


        public int find3rdDegreePolyRoots(double[] coeff, int degree, double[] roots,
            double initGuess, double precision)
        {
            double[] secondOrder = new double[3];
            roots[0] = findOnePolynomialRoot(coeff, 3, initGuess, precision);
            devidePolyBy(coeff, degree, roots[0], secondOrder);
            return find2ndOrderPoly(secondOrder[2], secondOrder[1], secondOrder[0], roots) + 1;
        }


        public int find2ndOrderPoly(double a, double b, double c, double[] roots)
        {
            double delta = b * b - 4 * a * c;
            if (delta < 0)
                return 0;
            else if (delta == 0)
            {
                roots[1] = -b / (2 * a);
                return 1;

            }
            else
            {
                roots[1] = (-b + Math.Sqrt(delta)) / (2 * a);
                roots[2] = (-b - Math.Sqrt(delta)) / (2 * a);
                return 2;
            }
        }


        public void devidePolyBy(double[] a, int n, double k, double[] b)
        {
            b[n - 1] = a[n];
            for (int i = n - 2; i >= 0; --i)
            {
                b[i] = k * b[i + 1] + a[i + 1];
            }
        }


        public double arrayMax(double[] a, int n)
        {
            if (n < 1)
                return -1;//error
            double m = a[0];
            for (int i = 1; i < n; ++i)
                if (a[i] > m)
                    m = a[i];
            //ed.WriteMessage("");
            return m;
        }


        public double findOnePolynomialRoot(double[] coeff, int degree, double initialGuess, double precision)
        {
            double[] derive = new double[degree];
            getDerivative(coeff, degree, derive);
            double x = initialGuess;
            double newX = 0;
            bool bOk = false;
            while (!bOk)
            {
                newX = x - evalFunction(coeff, degree, x) / evalFunction(derive, degree - 1, x);
                int iter = 1;
                while (iter < 300 && Math.Abs((newX - x) / newX) * 100 > precision)
                {
                    x = newX;
                    newX = x - evalFunction(coeff, degree, x) / evalFunction(derive, degree - 1, x);
                    iter++;
                }
                if (iter >= 300)
                {
                    initialGuess *= 10;
                    x = initialGuess;
                    iter = 0;
                }
                else
                    bOk = true;
            }

            return newX;
        }


        public double evalFunction(double[] coeff, int degree, double x)
        {
            double xpow = 1.00;
            double sum = 0;
            for (int i = 0; i <= degree; ++i)
            {
                sum += xpow * coeff[i];
                xpow *= x;
            }
            return sum;


        }



        public void getDerivative(double[] coeff, int degree, double[] derivative)
        {
            for (int i = 0; i < degree; ++i)
            {
                derivative[i] = coeff[i + 1] * (i + 1);

            }
        }



        public double ComputeTension(Atend.Base.Design.DWeather baseCase, Atend.Base.Design.DWeather newCase,
           double baseH, double baseSpan)
        {

            //ed.WriteMessage("BaseCAs.Code= "+baseCase.Code+"   newCase.Code= "+newCase.Code+"\n");
            double baseTotalWeight = ComputeTotalWeight(baseCase);
            //ed.WriteMessage("BaseToTalWeight= "+baseTotalWeight.ToString()+" BaseH="+baseH+"\n");

            //ed.WriteMessage("CrossSectionAre= " + Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea + " Alastisity= " + Atend.Global.Calculation.Mechanical.CCommon.Alasticity + "  BaseSpan= " + baseSpan + "\n");
            double k1 = Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea * Atend.Global.Calculation.Mechanical.CCommon.Alasticity * Math.Pow(baseSpan, 2) * Math.Pow(baseTotalWeight, 2) /
                (24 * Math.Pow(baseH, 2)) - baseH;
            //ed.WriteMessage("K1= "+k1.ToString()+"\n");
            double k2 = k1 + Atend.Global.Calculation.Mechanical.CCommon.Alpha * Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea * Atend.Global.Calculation.Mechanical.CCommon.Alasticity *
                (newCase.Temp - baseCase.Temp);
            //ed.WriteMessage("K2="+k2.ToString()+"\n");
            double totalWeight = ComputeTotalWeight(newCase);
            //ed.WriteMessage("ToTAlWeight= " + totalWeight.ToString() + "\n");
            double k3 = Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea * Atend.Global.Calculation.Mechanical.CCommon.Alasticity * Math.Pow(baseSpan, 2) * Math.Pow(totalWeight, 2) / 24;
            //ed.WriteMessage("K3= "+k3.ToString()+"\n");
            double H = findBiggestRealRoot(1, k2, 0, -k3, baseH, 0.1);
            //ed.WriteMessage("H= "+H.ToString()+"\n");
            return H;

        }
        public double ComputeTension(Atend.Base.Design.DWeather baseCase, double temp,
           double baseH, double baseSpan)
        {

            //ed.WriteMessage("BaseCAse= " + baseCase.Code +" Span= "+baseSpan+" BaseH ="+baseH+ "\n");
            //ed.WriteMessage("CrossSectionAre= " + conductorSection.CrossSectionArea + " Alastisity= " + conductorSection.Alasticity + "Alpha= "+conductorSection.Alpha+" Temp= "+temp+ "\n");

            double baseTotalWeight = ComputeTotalWeight(baseCase);
            //ed.WriteMessage("BaseToTalWeight= " + baseTotalWeight.ToString() + "\n");
            double k1 = (Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea * Atend.Global.Calculation.Mechanical.CCommon.Alasticity * Math.Pow(baseSpan, 2) * Math.Pow(baseTotalWeight, 2)) /
                (24 * Math.Pow(baseH, 2)) - baseH;
            // ed.WriteMessage("K1= " + k1.ToString() + "\n");
            double k2 = k1 + Atend.Global.Calculation.Mechanical.CCommon.Alpha * Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea * Atend.Global.Calculation.Mechanical.CCommon.Alasticity *
                (temp - baseCase.Temp);
            //ed.WriteMessage("K2=" + k2.ToString() + "\n");
            double totalWeight = ComputeTotalWeight(baseCase);

            double k3 = (Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea * Atend.Global.Calculation.Mechanical.CCommon.Alasticity * Math.Pow(baseSpan, 2) * Math.Pow(totalWeight, 2)) / 24;
            //ed.WriteMessage("K3= " + k3.ToString() + "\n");
            double H = findBiggestRealRoot(1, k2, 0, -k3, baseH, 0.1);
            //ed.WriteMessage("H= " + H.ToString() + "\n");
            return H;

        }



        //internal double[] ComputeTension(Atend.Base.Design.DWeather WeatherBase, Atend.Base.Design.DWeather WeatherSecond, double BaseH, double p)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        /// <summary>
        /// Returns a Datatable that contains all poles which was DDE or DE
        /// </summary>
        /// <returns></returns>

        public static System.Data.DataTable GetDesignPoles()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            System.Data.DataColumn dc1 = new System.Data.DataColumn("PoleGuid");
            System.Data.DataColumn dc2 = new System.Data.DataColumn("PoleOI");

            System.Data.DataTable PolesTable = new System.Data.DataTable();
            PolesTable.Columns.Add(dc1);
            PolesTable.Columns.Add(dc2);



            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                //ed.WriteMessage("1\n");
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    //ed.WriteMessage("2\n");
                    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                    //ed.WriteMessage("3\n");
                    foreach (ObjectId oi in btr)
                    {

                        Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Pole)
                        {
                            //ed.WriteMessage("4\n");
                            //CurrentPoleOi = oi;

                            System.Data.DataTable Consols = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(at_info.NodeCode)).Code, (int)Atend.Control.Enum.ProductType.Consol);
                            foreach (System.Data.DataRow dr in Consols.Rows)
                            {
                                //ed.WriteMessage("5\n");
                                Atend.Base.Equipment.EConsol EC = Atend.Base.Equipment.EConsol.SelectByCode(Convert.ToInt32(dr["ProductCode"]));
                                if (EC.ConsolType == 0 || EC.ConsolType == 1)
                                {
                                    ed.WriteMessage("6\n");
                                    System.Data.DataRow NewR = PolesTable.NewRow();
                                    NewR["PoleGuid"] = at_info.NodeCode;
                                    NewR["PoleOI"] = oi.ToString().Substring(1, oi.ToString().Length - 2);
                                    PolesTable.Rows.Add(NewR);
                                }
                            }
                        }
                    }

                }//end of transaction

            }// end of lock


            foreach (System.Data.DataRow dr in PolesTable.Rows)
            {
                ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                ed.WriteMessage("PG:{0} \nPOI:{1} \n", dr["PoleGuid"], dr["PoleOI"]);
            }
            return PolesTable;
        }


    }
}
