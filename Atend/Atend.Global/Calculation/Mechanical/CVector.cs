using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections;


namespace Atend.Global.Calculation.Mechanical
{
  public  class CVector
    {
        private double absolute;

        public double Absolute
        {
            get { return absolute; }
            set { absolute = value; }
        }

        private double angle;

        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }
      
      public CVector()
      {
          Absolute = 0;
          Angle = 0;
      }
      Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
      public CVector Add(CVector a, CVector b)
      {
          CVector res = new CVector();
          double xRes=a.X()+b.X();
          double yRes = a.Y() + b.Y();
          res.Absolute = Math.Sqrt(xRes * xRes + yRes * yRes);
          res.Angle = Math.Atan2(yRes, xRes)*180/Math.PI;
          return res;
      }

      public CVector Mines(CVector a, CVector b)
      {
          CVector res=new CVector();
          double xRes = a.X()- b.X();
          double yRes = a.Y() - b.Y();
          res.absolute = Math.Sqrt(xRes * xRes + yRes * yRes);
          res.Angle = Math.Atan2(yRes, xRes)*180/Math.PI;
          return res;
      }


      public double X()
      {
          return Math.Abs(Absolute) * Math.Cos(Angle * Math.PI / 180);

      }
      public double Y()
      {
          return Math.Abs(Absolute) * Math.Sin(Angle*Math.PI/180);
      }

    }
}
