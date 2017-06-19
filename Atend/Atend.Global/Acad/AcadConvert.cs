using System;
using System.Collections.Generic;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;


namespace Atend.Global.Acad
{
    public class AcadConvert
    {


        public class MyCircle:Circle
        {
            private Dictionary<string, object> additionalDictionary = new Dictionary<string, object>();

            public Dictionary<string, object> AdditionalDictionary
            {
                get { return additionalDictionary; }
                set { additionalDictionary = value; }
            }
        }


        public class MyPolyline : Polyline
        {
            private Dictionary<string, object> additionalDictionary = new Dictionary<string, object>();

            public Dictionary<string, object> AdditionalDictionary
            {
                get { return additionalDictionary; }
                set { additionalDictionary = value; }
            }
        }

        public class MyLine : Line
        {
            private Dictionary<string, object> additionalDictionary = new Dictionary<string, object>();

            public Dictionary<string, object> AdditionalDictionary
            {
                get { return additionalDictionary; }
                set { additionalDictionary = value; }
            }
        }



        public class Post
        {
            public List<JackPanel20KV> JackPanel20KVs = new List<JackPanel20KV>();
            public List<JackPanelWeek> JackPanelWeeks = new List<JackPanelWeek>();
            public List<Transformer> Transformers = new List<Transformer>();
            public List<Entity> Entities = new List<Entity>();
        }


        public class JackPanelWeek
        {
            public List<Entity> Entities = new List<Entity>();
        }


        public class JackPanel20KV
        {
            public List<Cell> Cells = new List<Cell>();
            public List<Entity> Entities = new List<Entity>();
        }


        public class Transformer
        {
            public List<Entity> Entities = new List<Entity>();
        }


        public class Cell
        {
            public List<Entity> Entities = new List<Entity>();

        }

        public static Entity ConverToCustomEntity(ObjectId CurrentEntityOI,int ProductCode , int ProductType)
        {
            Entity ent = Atend.Global.Acad.UAcad.GetEntityByObjectID(CurrentEntityOI);

            MyCircle c = ent as MyCircle;
            if (c != null)
            {
                #region Convert to mycircle
                c.AdditionalDictionary.Add("ProductType", ProductType);
                c.AdditionalDictionary.Add("ProductCode", ProductCode);
                ent = c;
                #endregion
            }
            else
            {
                MyPolyline p = ent as MyPolyline;
                if (p != null)
                {
                    #region convert to polyline
                    p.AdditionalDictionary.Add("ProductType", ProductType);
                    p.AdditionalDictionary.Add("ProductCode", ProductCode);
                    ent = p;
                    #endregion
                }
                else
                {
                    MyLine l = ent as MyLine;
                    if (l != null)
                    {
                        #region convert to line
                        l.AdditionalDictionary.Add("ProductType", ProductType);
                        l.AdditionalDictionary.Add("ProductCode", ProductCode);
                        ent = l;

                        #endregion 
                    }
                    else
                    {
                        // it was nothing
                    }
                }
            }
            return ent;

        }


    }
}
