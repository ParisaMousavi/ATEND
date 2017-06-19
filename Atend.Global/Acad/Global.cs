using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.IO.Packaging;


using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.AcInfoCenterConn;


//update from tehran 7/15
namespace Atend.Global.Acad
{
    public class Global
    {


        public static ObjectId MakeGroup(string GroupName, ObjectIdCollection OIC)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Group group = new Group(GroupName, true);
            ObjectId objectId = ObjectId.Null;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {

                    DBDictionary dict = (DBDictionary)tr.GetObject(db.GroupDictionaryId, OpenMode.ForWrite, true);
                    //ed.WriteMessage("~~~>>>~~~GON : {0}", GroupName);
                    objectId = dict.SetAt(GroupName, group);
                    tr.AddNewlyCreatedDBObject(group, true);
                    tr.Commit();
                    AddObjectToGroup(objectId, OIC);
                }
            }
            return objectId;

        }

        public static void AddObjectToGroup(ObjectId GroupId, ObjectIdCollection OIC)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    //ed.WriteMessage("~~~>>>~~~GOI : {0}",GroupId);
                    Group group = tr.GetObject(GroupId, OpenMode.ForWrite, true) as Group;
                    //PromptSelectionResult psr = ed.GetSelection();
                    //SelectionSet ss = psr.Value;
                    //ObjectId[] idarray = ObjectIdArray;
                    if (group != null)
                    {
                        ObjectIdCollection _OIC = OIC;
                        foreach (ObjectId id in OIC)
                        {

                            //Entity entity = (Entity)tr.GetObject(id, OpenMode.ForRead);
                            group.Append(id);

                        }
                    }
                    tr.Commit();

                }
            }
            //ed.WriteMessage("add to group finished \n");
        }

        //public static Entity WriteNote(string Text, Point3d LineStartPoint, Point3d LineEndPoint)
        //{

        //    DBText dbText = new DBText();
        //    dbText.Position = new LineSegment3d(LineStartPoint, LineEndPoint).MidPoint;
        //    dbText.TextString = Text;
        //    dbText.Height = 2;
        //    //ed.WriteMessage(string.Format("Angle is : {0} \n", ((Math.Atan((conductorInformation.Point1.Y - conductorInformation.Point2.Y) / (conductorInformation.Point1.X - conductorInformation.Point2.X))) * Math.PI) / 180));
        //    dbText.Rotation = new Point2d(LineStartPoint.X, LineStartPoint.Y).GetVectorTo(new Point2d(LineEndPoint.X, LineEndPoint.Y)).Angle;
        //    return dbText;
        //}

        //public static Entity WriteNote(string Text, Point3d TextPosition)
        //{

        //    DBText dbText = new DBText();
        //    dbText.Position = TextPosition;
        //    dbText.TextString = Text;
        //    dbText.Height = 2;
        //    //ed.WriteMessage(string.Format("Angle is : {0} \n", ((Math.Atan((conductorInformation.Point1.Y - conductorInformation.Point2.Y) / (conductorInformation.Point1.X - conductorInformation.Point2.X))) * Math.PI) / 180));
        //    dbText.Rotation = 0;

        //    return dbText;
        //}

        //public static Entity WriteNoteMText(string Text, Point3d TextPosition, double Scale)
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    MText mTxt = new MText();
        //    mTxt.Location = TextPosition;
        //    mTxt.Contents = Text;
        //    mTxt.Rotation = 0;
        //    mTxt.Height = 10;

        //    Entity ent = mTxt;
        //    ed.WriteMessage("******SCALE : {0} :{1} \n", Scale, TextPosition);
        //    Matrix3d trans1 = Matrix3d.Scaling(Scale, mTxt.Location);
        //    ent.TransformBy(trans1);

        //    return ent;

        //}

        public static void ChangeScale(ObjectId EOI, double Scale, Point3d CenterPoint)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {

                    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                    Entity ent = tr.GetObject(EOI, OpenMode.ForWrite) as Entity;
                    if (ent != null)
                    {
                        Matrix3d trans1 = Matrix3d.Scaling(Scale, CenterPoint);
                        ent.TransformBy(trans1);

                    }
                    tr.Commit();


                }
            }
        }

        public static ObjectId GetEntityGroup(ObjectId CurrentEntityOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //Database db = HostApplicationServices.WorkingDatabase;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            ObjectId CurrentGroupOI = ObjectId.Null;

            using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    DBDictionary dict = (DBDictionary)tr.GetObject(db.GroupDictionaryId, OpenMode.ForRead, true);
                    foreach (DBDictionaryEntry dbdic in dict)
                    {
                        //ed.WriteMessage("dictionary list {0} . \n", dbdic.Key);
                        ObjectId groupId = dict.GetAt(dbdic.Key);
                        Group group = (Group)tr.GetObject(groupId, OpenMode.ForRead, true);
                        ObjectId[] objs = group.GetAllEntityIds();
                        foreach (ObjectId id in objs)
                        {

                            //ed.WriteMessage("sub entity id {0} \n", id);
                            if (id == CurrentEntityOI)
                            {
                                CurrentGroupOI = groupId;
                            }

                        }

                    }

                    //tr.Commit();
                }
            }
            return CurrentGroupOI;
        }


        public static System.Data.DataTable  ReadAllEntity()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            return dt;
        }

        //public static ObjectIdCollection GetGroupSubEntities(ObjectId GroupOI)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //Database db = HostApplicationServices.WorkingDatabase;
        //    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //    ObjectIdCollection OIC = new ObjectIdCollection();

        //    using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
        //    {

        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {
        //            DBDictionary dict = (DBDictionary)tr.GetObject(db.GroupDictionaryId, OpenMode.ForRead, true);
        //            //foreach (DBDictionaryEntry dbdic in dict)
        //            //{
        //            //ed.WriteMessage("dictionary list {0} . \n", dbdic.Key);
        //            //ObjectId groupId = dict.GetAt(dbdic.Key);
        //            //if (groupId == GroupOI)
        //            //{
        //            Group group = (Group)tr.GetObject(GroupOI, OpenMode.ForRead, true);
        //            ObjectId[] objs = group.GetAllEntityIds();
        //            foreach (ObjectId id in objs)
        //            {

        //                OIC.Add(id);
        //            }

        //            //}
        //            //}

        //            //tr.Commit();
        //        }
        //    }
        //    return OIC;

        //}

        public static void WriteGroup(List<Entity> Entities)
        {
            DocumentCollection dm = Application.DocumentManager;
            Editor ed = dm.MdiActiveDocument.Editor;
            Database destDb = dm.MdiActiveDocument.Database;
            Database sourceDb = new Database(false, true);
            try
            {

                // Get name of DWG from which to copy blocks
                // Read the DWG into a side database
                // Create a variable to store the list of block identifiers

                //findtab("ID_Equipments");

                //System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
                //string fullPath = m.FullyQualifiedName;
                //try
                //{
                //    fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
                //}
                //catch
                //{
                //}


                string fullPath = Atend.Control.Common.fullPath + "\\GROUP.dwg";
                Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(fullPath + "\n");


                //sourceFileName = ed.GetString("\nEnter the name of the source drawing: ");
                sourceDb.ReadDwgFile(fullPath, System.IO.FileShare.Read, true, "2009");
                ObjectIdCollection blockIds = new ObjectIdCollection();
                Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = sourceDb.TransactionManager;
                using (Transaction myT = tm.StartTransaction())
                {
                    // Open the block table
                    // Check each block in the block table
                    // No need to commit the transaction

                    BlockTable bt = (BlockTable)tm.GetObject(sourceDb.BlockTableId, OpenMode.ForRead, false);
                    foreach (ObjectId btrId in bt)
                    {
                        BlockTableRecord btr = (BlockTableRecord)tm.GetObject(btrId, OpenMode.ForWrite, false);
                        if (!btr.IsAnonymous && !btr.IsLayout)
                        {
                            // Only add named & non-layout blocks to the copy list
                            // Attribute Definition
                            AttributeDefinition text = new AttributeDefinition(new Point3d(0, 2, 0), "NoName", "Name:", "Enter Name", destDb.Textstyle);
                            text.ColorIndex = 0;
                            btr.AppendEntity(text);
                            myT.AddNewlyCreatedDBObject(text, true);
                            blockIds.Add(btrId);
                        }
                        btr.Dispose();
                    }
                    bt.Dispose();
                    myT.Dispose();
                }
                // Copy blocks from source to destination database
                IdMapping mapping = new IdMapping();
                sourceDb.WblockCloneObjects(blockIds, destDb.BlockTableId, mapping, DuplicateRecordCloning.Replace, false);
                //ed.WriteMessage("\nCopied " + blockIds.Count.ToString() + " block definitions from Source file to the current drawing.\n");
            }// end of try
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                ed.WriteMessage("\nError during copy: " + ex.Message + "\n");
            }
            sourceDb.Dispose();
        }

        //public static ObjectId DrawEntityOnScreen(Entity entity)
        //{

        //    ObjectId oi;


        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //ed.WriteMessage("I aM in DrawEntityOnScreen\n");
        //    //Database db = HostApplicationServices.WorkingDatabase;
        //    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //    using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
        //    {


        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {

        //            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);

        //            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

        //            oi = btr.AppendEntity(entity);

        //            tr.AddNewlyCreatedDBObject(entity, true);

        //            tr.Commit();


        //        }
        //    }
        //    //ed.WriteMessage("End Of DrawEntityOnScreen\n");

        //    return oi;

        //}

        //public static ObjectId DrawEntityOnScreen(Entity entity, string LayerName)
        //{

        //    ObjectId oi;

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //ed.WriteMessage("I aM in DrawEntityOnScreen\n");
        //    //Database db = HostApplicationServices.WorkingDatabase;
        //    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //    using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
        //    {


        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {

        //            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);

        //            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);


        //            entity.LayerId = AcadLayer.GetLayerById(LayerName);

        //            oi = btr.AppendEntity(entity);

        //            tr.AddNewlyCreatedDBObject(entity, true);

        //            tr.Commit();


        //        }
        //    }
        //    //ed.WriteMessage("End Of DrawEntityOnScreen\n");

        //    return oi;

        //}

        public static System.Data.DataTable PointInsideWhichEntity(Point3d SelectedPoint)
        {

            //ArrayList PointContainer = new ArrayList();
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("with out transaction PIWI 1\n");
            System.Data.DataColumn dc0 = new System.Data.DataColumn("ObjectId");
            System.Data.DataColumn dc1 = new System.Data.DataColumn("Type");
            System.Data.DataColumn dc2 = new System.Data.DataColumn("CenterPoint");

            System.Data.DataTable PointContainerList = new System.Data.DataTable();
            PointContainerList.Columns.Add(dc0);
            PointContainerList.Columns.Add(dc1);
            PointContainerList.Columns.Add(dc2);

            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                    foreach (ObjectId oi in btr)
                    {

                        //TypedValue[] tvs = new TypedValue[] { new TypedValue((int)DxfCode.Start, "Polyline") };
                        //SelectionFilter sf = new SelectionFilter(tvs);
                        //PromptSelectionResult psr = ed.SelectAll(sf);
                        //ObjectId[] ids = psr.Value.GetObjectIds();
                        //foreach (ObjectId oi in ids)
                        //{

                        Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        if (at_info.ParentCode != "NONE")
                        {
                            try
                            {
                                Polyline CurrentPoly = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Polyline;
                                if (CurrentPoly != null)
                                {
                                    Curve c = CurrentPoly as Curve;
                                    if (c != null)
                                    {
                                        if (Atend.Global.Acad.UAcad.IsInsideCurve(c, SelectedPoint))
                                        {
                                            System.Data.DataRow newRow = PointContainerList.NewRow();
                                            newRow["ObjectId"] = oi.ToString().Substring(1, oi.ToString().Length - 2);
                                            newRow["Type"] = at_info.NodeType;
                                            newRow["CenterPoint"] = Atend.Global.Acad.UAcad.CenterOfEntity(CurrentPoly);
                                            PointContainerList.Rows.Add(newRow);
                                        }
                                    }
                                }
                                Circle CurrentCircle = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Circle;
                                if (CurrentCircle != null)
                                {
                                    Curve c = CurrentCircle as Curve;
                                    if (c != null)
                                    {
                                        if (Atend.Global.Acad.UAcad.IsInsideCurve(c, SelectedPoint))
                                        {
                                            System.Data.DataRow newRow = PointContainerList.NewRow();
                                            newRow["ObjectId"] = oi.ToString().Substring(1, oi.ToString().Length - 2);
                                            newRow["Type"] = at_info.NodeType;
                                            newRow["CenterPoint"] = Atend.Global.Acad.UAcad.CenterOfEntity(CurrentCircle);
                                            PointContainerList.Rows.Add(newRow);
                                        }
                                    }
                                }

                            }
                            catch (System.Exception es)
                            {
                                ed.WriteMessage("ERROR POINT INSIDE 1: {0} \n", es.Message);
                            }
                        }// if none
                        //}
                    }//end foreach
                }
            }

            foreach (System.Data.DataRow dr in PointContainerList.Rows)
            {
                ed.WriteMessage("1:pC:{0}\n", dr["Type"]);
            }
            ed.WriteMessage("with out transaction PIWI 1 END\n");

            return PointContainerList;
        }

        public static System.Data.DataTable __PointInsideWhichEntity(Point3d SelectedPoint, bool BringSubEntities)
        {

            //ArrayList PointContainer = new ArrayList();
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("One transaction 02 \n");

            System.Data.DataColumn dc0 = new System.Data.DataColumn("ObjectId");
            System.Data.DataColumn dc1 = new System.Data.DataColumn("Type");
            System.Data.DataColumn dc2 = new System.Data.DataColumn("CenterPoint");

            System.Data.DataTable PointContainerList = new System.Data.DataTable();
            PointContainerList.Columns.Add(dc0);
            PointContainerList.Columns.Add(dc1);
            PointContainerList.Columns.Add(dc2);

            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    //    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                    //    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                    //    foreach (ObjectId oi in btr)
                    //    {

                    //ed.WriteMessage("^^^^^^^^^^^^^^^^^^ ^^^^^^^^^^^^^^^^^^^^^^^^^^\n");
                    TypedValue[] tvs = new TypedValue[] { new TypedValue((int)DxfCode.Start, "polyline") };
                    SelectionFilter sf = new SelectionFilter(tvs);
                    PromptSelectionResult psr = ed.SelectAll(sf);
                    ObjectId[] ids = psr.Value.GetObjectIds();
                    foreach (ObjectId oi in ids)
                    {


                        Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        if (at_info.ParentCode != "NONE")
                        {
                            try
                            {
                                Polyline CurrentPoly = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Polyline;
                                if (CurrentPoly != null)
                                {
                                    Curve c = CurrentPoly as Curve;
                                    if (c != null)
                                    {
                                        if (Atend.Global.Acad.UAcad.IsInsideCurve(c, SelectedPoint))
                                        {
                                            System.Data.DataRow newRow = PointContainerList.NewRow();
                                            newRow["ObjectId"] = oi.ToString().Substring(1, oi.ToString().Length - 2);
                                            newRow["Type"] = at_info.NodeType;
                                            newRow["CenterPoint"] = Atend.Global.Acad.UAcad.CenterOfEntity(CurrentPoly);
                                            PointContainerList.Rows.Add(newRow);
                                        }
                                    }
                                }
                                Circle CurrentCircle = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Circle;
                                if (CurrentCircle != null)
                                {
                                    Curve c = CurrentCircle as Curve;
                                    if (c != null)
                                    {
                                        if (Atend.Global.Acad.UAcad.IsInsideCurve(c, SelectedPoint))
                                        {
                                            System.Data.DataRow newRow = PointContainerList.NewRow();
                                            newRow["ObjectId"] = oi.ToString().Substring(1, oi.ToString().Length - 2);
                                            newRow["Type"] = at_info.NodeType;
                                            newRow["CenterPoint"] = Atend.Global.Acad.UAcad.CenterOfEntity(CurrentCircle);
                                            PointContainerList.Rows.Add(newRow);
                                        }
                                    }
                                }

                            }
                            catch (System.Exception ex)
                            {
                                ed.WriteMessage("ERROR PointInsideWhich : {0} \n", ex.Message);
                            }
                        }// if none
                    }
                    //    }//end foreach

                    try
                    {
                        if (BringSubEntities)
                        {
                            foreach (System.Data.DataRow dr in PointContainerList.Rows)
                            {
                                Atend.Base.Acad.AT_SUB SubEntity = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(new ObjectId(new IntPtr(int.Parse(dr["ObjectId"].ToString()))));
                                foreach (ObjectId oi in SubEntity.SubIdCollection)
                                {
                                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                                    System.Data.DataRow newRow = PointContainerList.NewRow();
                                    newRow["ObjectId"] = oi.ToString().Substring(1, oi.ToString().Length - 2);
                                    newRow["Type"] = at_info.NodeType;
                                    newRow["CenterPoint"] = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
                                    PointContainerList.Rows.Add(newRow);

                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }

            foreach (System.Data.DataRow dr in PointContainerList.Rows)
            {
                ed.WriteMessage("2:pC:{0}\n", dr["Type"]);
            }
            ed.WriteMessage("END PIWI \n");
            return PointContainerList;
        }

        //public static bool IsInsideCurve(Curve cur, Point3d testPt)
        //{

        //    if (!cur.Closed)

        //        // Cannot be inside

        //        return false;



        //    Polyline2d poly2d = cur as Polyline2d;

        //    if (poly2d != null &&

        //        poly2d.PolyType != Poly2dType.SimplePoly)

        //        // Not supported

        //        return false;



        //    Point3d ptOnCurve =

        //      cur.GetClosestPointTo(testPt, false);



        //    if (Tolerance.Equals(testPt, ptOnCurve))

        //        return true;



        //    // Check it's planar



        //    Plane plane = cur.GetPlane();

        //    if (!cur.IsPlanar)

        //        return false;



        //    // Make the test ray from the plane



        //    Vector3d normal = plane.Normal;

        //    Vector3d testVector =

        //      normal.GetPerpendicularVector();

        //    Ray ray = new Ray();

        //    ray.BasePoint = testPt;

        //    ray.UnitDir = testVector;



        //    Point3dCollection intersectionPoints =

        //      new Point3dCollection();



        //    // Fire the ray at the curve

        //    cur.IntersectWith(

        //      ray,

        //      Intersect.OnBothOperands,

        //      intersectionPoints,

        //      0, 0

        //    );



        //    ray.Dispose();


        //    int numberOfInters =

        //      intersectionPoints.Count;

        //    if (numberOfInters == 0)

        //        // Must be outside

        //        return false;



        //    int nGlancingHits = 0;

        //    double epsilon = 2e-6; // (trust me on this)

        //    for (int i = 0; i < numberOfInters; i++)
        //    {

        //        // Get the first point, and get its parameter

        //        Point3d hitPt = intersectionPoints[i];

        //        double hitParam =

        //          cur.GetParameterAtPoint(hitPt);

        //        double inParam = hitParam - epsilon;

        //        double outParam = hitParam + epsilon;



        //        IncidenceType inIncidence =

        //          CurveIncidence(cur, inParam, testVector, normal);

        //        IncidenceType outIncidence =

        //          CurveIncidence(cur, outParam, testVector, normal);



        //        if ((inIncidence == IncidenceType.ToRight &&

        //            outIncidence == IncidenceType.ToLeft) ||

        //            (inIncidence == IncidenceType.ToLeft &&

        //            outIncidence == IncidenceType.ToRight))

        //            nGlancingHits++;

        //    }

        //    return ((numberOfInters + nGlancingHits) % 2 == 1);

        //}

        //private static IncidenceType CurveIncidence(Curve cur, double param, Vector3d dir, Vector3d normal)
        //{

        //    Vector3d deriv1 =

        //      cur.GetFirstDerivative(param);

        //    if (deriv1.IsParallelTo(dir))
        //    {

        //        // Need second degree analysis



        //        Vector3d deriv2 =

        //          cur.GetSecondDerivative(param);

        //        if (deriv2.IsZeroLength() ||

        //            deriv2.IsParallelTo(dir))

        //            return IncidenceType.ToFront;

        //        else

        //            if (deriv2.CrossProduct(dir).

        //                DotProduct(normal) < 0)

        //                return IncidenceType.ToRight;

        //            else

        //                return IncidenceType.ToLeft;

        //    }



        //    if (deriv1.CrossProduct(dir).

        //        DotProduct(normal) < 0)

        //        return IncidenceType.ToLeft;

        //    else

        //        return IncidenceType.ToRight;

        //}

        //private enum IncidenceType
        //{

        //    ToLeft = 0,

        //    ToRight = 1,

        //    ToFront = 2,

        //    Unknown

        //};
        //-------------------------------

        public static Point3d PoleCommentPosition(Entity PoleEntity)
        {
            Polyline _PoleEntity = PoleEntity as Polyline;
            Point3d CommentPosition = Point3d.Origin;
            if (_PoleEntity != null)
            {
                //calculate comment position
                switch (_PoleEntity.NumberOfVertices)
                {
                    case 5:
                        CommentPosition = _PoleEntity.GetPoint3dAt(0);
                        break;
                    case 7:
                        CommentPosition = _PoleEntity.GetPoint3dAt(0);
                        break;
                }
            }
            else
            {
                Circle PoleEntityCircle = PoleEntity as Circle;
                if (PoleEntityCircle != null)
                {
                    //calculate pole comment position
                    Curve _Curve = PoleEntityCircle as Curve;
                    if (_Curve != null)
                    {
                        Point3dCollection p3c = new Point3dCollection();
                        Ray _Ray = new Ray();
                        Point3d po = Atend.Global.Acad.UAcad.CenterOfEntity(PoleEntityCircle);
                        _Ray.BasePoint = po;
                        _Ray.SecondPoint = new Point3d(_Ray.StartPoint.X + 10, _Ray.StartPoint.Y, 0);
                        _Curve.IntersectWith(_Ray, Intersect.OnBothOperands, p3c, 0, 0);
                        if (p3c.Count > 0)
                        {
                            CommentPosition = p3c[0];
                        }

                    }
                }
            }
            return CommentPosition;
        }

        /// <summary>
        /// work with drawing file
        /// </summary>
        /// <returns></returns>
        public static System.Data.DataTable FindAllEquips()
        {
            List<int> ProductList = new List<int>();
            System.Data.DataTable dt_ProductList = new System.Data.DataTable();

            dt_ProductList.Columns.Add(new System.Data.DataColumn("Type", Type.GetType("System.Int32")));
            dt_ProductList.Columns.Add(new System.Data.DataColumn("ProductCode", Type.GetType("System.Int32")));


            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                //ed.WriteMessage("1\n");
                TypedValue[] tvs = new TypedValue[] { new TypedValue((int)DxfCode.Start, "POLYLINE") };
                //ed.WriteMessage("2\n");
                SelectionFilter sf = new SelectionFilter(tvs);
                //ed.WriteMessage("3\n");
                PromptSelectionResult psr = ed.SelectAll();
                //ed.WriteMessage("4\n");
                try
                {
                    if (psr.Value.Count > 0)
                    {
                        ObjectId[] ids = psr.Value.GetObjectIds();
                        //ed.WriteMessage("5 ID:{0}\n", psr.Value.Count);
                        foreach (ObjectId oi in ids)
                        {
                            Atend.Base.Acad.AT_INFO hInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("YES : {0} \n", hInfo.NodeType);
                            switch ((Atend.Control.Enum.ProductType)hInfo.NodeType)
                            {
                                case Atend.Control.Enum.ProductType.Pole:
                                    Atend.Base.Equipment.EPole _EPole = Atend.Base.Equipment.EPole.AccessSelectByCode(hInfo.ProductCode);
                                    if (_EPole.Shape == 0)
                                    {
                                        if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.PoleCircle))
                                        {
                                            ProductList.Add((int)Atend.Control.Enum.FrameProductType.PoleCircle);
                                            System.Data.DataRow ndr = dt_ProductList.NewRow();
                                            ndr["Type"] = Atend.Control.Enum.FrameProductType.PoleCircle;
                                            ndr["ProductCode"] = hInfo.ProductCode;
                                            dt_ProductList.Rows.Add(ndr);
                                        }

                                    }
                                    else
                                    {
                                        if (_EPole.Type == 2)
                                        {
                                            if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.PolePolygon))
                                            {
                                                ProductList.Add((int)Atend.Control.Enum.FrameProductType.PolePolygon);
                                                System.Data.DataRow ndr = dt_ProductList.NewRow();
                                                ndr["Type"] = Atend.Control.Enum.FrameProductType.PolePolygon;
                                                ndr["ProductCode"] = hInfo.ProductCode;
                                                dt_ProductList.Rows.Add(ndr);
                                            }


                                        }
                                        else
                                        {
                                            if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.Pole))
                                            {
                                                ProductList.Add((int)Atend.Control.Enum.FrameProductType.Pole);
                                                System.Data.DataRow ndr = dt_ProductList.NewRow();
                                                ndr["Type"] = Atend.Control.Enum.FrameProductType.Pole;
                                                ndr["ProductCode"] = hInfo.ProductCode;
                                                dt_ProductList.Rows.Add(ndr);
                                            }


                                        }
                                    }
                                    break;

                                case Atend.Control.Enum.ProductType.PoleTip:
                                    Atend.Base.Equipment.EPoleTip _EPoleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(hInfo.ProductCode);
                                    Atend.Base.Equipment.EPole _EPole1 = Atend.Base.Equipment.EPole.AccessSelectByCode(_EPoleTip.PoleCode);
                                    if (_EPole1.Shape == 0)
                                    {
                                        if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.PoleCircle))
                                        {
                                            ProductList.Add((int)Atend.Control.Enum.FrameProductType.PoleCircle);
                                            System.Data.DataRow ndr = dt_ProductList.NewRow();
                                            ndr["Type"] = Atend.Control.Enum.FrameProductType.PoleCircle;
                                            ndr["ProductCode"] = _EPole1.Code;
                                            dt_ProductList.Rows.Add(ndr);
                                        }

                                    }
                                    else
                                    {
                                        if (_EPole1.Type == 2)
                                        {
                                            if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.PolePolygon))
                                            {
                                                ProductList.Add((int)Atend.Control.Enum.FrameProductType.PolePolygon);
                                                System.Data.DataRow ndr = dt_ProductList.NewRow();
                                                ndr["Type"] = Atend.Control.Enum.FrameProductType.PolePolygon;
                                                ndr["ProductCode"] = _EPole1.Code;
                                                dt_ProductList.Rows.Add(ndr);
                                            }


                                        }
                                        else
                                        {
                                            if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.Pole))
                                            {
                                                ProductList.Add((int)Atend.Control.Enum.FrameProductType.Pole);
                                                System.Data.DataRow ndr = dt_ProductList.NewRow();
                                                ndr["Type"] = Atend.Control.Enum.FrameProductType.Pole;
                                                ndr["ProductCode"] = _EPole1.Code;
                                                dt_ProductList.Rows.Add(ndr);
                                            }


                                        }
                                    }
                                    break;


                                case Atend.Control.Enum.ProductType.GroundPost:
                                    Atend.Base.Equipment.EGroundPost _EGroundPost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(hInfo.ProductCode);
                                    if (_EGroundPost.AdvanceType == 2)
                                    {
                                        if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.GroundPostKiusk))
                                        {
                                            ProductList.Add((int)Atend.Control.Enum.FrameProductType.GroundPostKiusk);
                                            System.Data.DataRow ndr = dt_ProductList.NewRow();
                                            ndr["Type"] = Atend.Control.Enum.FrameProductType.GroundPostKiusk;
                                            ndr["ProductCode"] = hInfo.ProductCode;
                                            dt_ProductList.Rows.Add(ndr);
                                        }
                                    }
                                    else
                                    {
                                        if (_EGroundPost.GroundType == 0)
                                        {
                                            if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.GroundPostOn))
                                            {
                                                ProductList.Add((int)Atend.Control.Enum.FrameProductType.GroundPostOn);
                                                System.Data.DataRow ndr = dt_ProductList.NewRow();
                                                ndr["Type"] = Atend.Control.Enum.FrameProductType.GroundPostOn;
                                                ndr["ProductCode"] = hInfo.ProductCode;
                                                dt_ProductList.Rows.Add(ndr);
                                            }

                                        }
                                        else
                                        {
                                            if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.GroundPostUnder))
                                            {
                                                ProductList.Add((int)Atend.Control.Enum.FrameProductType.GroundPostUnder);
                                                System.Data.DataRow ndr = dt_ProductList.NewRow();
                                                ndr["Type"] = Atend.Control.Enum.FrameProductType.GroundPostUnder;
                                                ndr["ProductCode"] = hInfo.ProductCode;
                                                dt_ProductList.Rows.Add(ndr);
                                            }

                                        }
                                    }
                                    break;

                                case Atend.Control.Enum.ProductType.Disconnector:
                                    if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.Disconnector))
                                    {
                                        ProductList.Add((int)Atend.Control.Enum.FrameProductType.Disconnector);
                                        System.Data.DataRow ndr = dt_ProductList.NewRow();
                                        ndr["Type"] = Atend.Control.Enum.FrameProductType.Disconnector;
                                        ndr["ProductCode"] = hInfo.ProductCode;
                                        dt_ProductList.Rows.Add(ndr);
                                    }


                                    break;

                                case Atend.Control.Enum.ProductType.Breaker:
                                    if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.Breaker))
                                    {
                                        ProductList.Add((int)Atend.Control.Enum.FrameProductType.Breaker);
                                        System.Data.DataRow ndr = dt_ProductList.NewRow();
                                        ndr["Type"] = Atend.Control.Enum.FrameProductType.Breaker;
                                        ndr["ProductCode"] = hInfo.ProductCode;
                                        dt_ProductList.Rows.Add(ndr);
                                    }


                                    break;

                                case Atend.Control.Enum.ProductType.HeaderCabel:
                                    if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.HeaderCable))
                                    {
                                        ProductList.Add((int)Atend.Control.Enum.FrameProductType.HeaderCable);
                                        System.Data.DataRow ndr = dt_ProductList.NewRow();
                                        ndr["Type"] = Atend.Control.Enum.FrameProductType.HeaderCable;
                                        ndr["ProductCode"] = hInfo.ProductCode;
                                        dt_ProductList.Rows.Add(ndr);
                                    }


                                    break;


                                case Atend.Control.Enum.ProductType.Kalamp:
                                    if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.Klamp))
                                    {
                                        ProductList.Add((int)Atend.Control.Enum.FrameProductType.Klamp);
                                        System.Data.DataRow ndr = dt_ProductList.NewRow();
                                        ndr["Type"] = Atend.Control.Enum.FrameProductType.Klamp;
                                        ndr["ProductCode"] = hInfo.ProductCode;
                                        dt_ProductList.Rows.Add(ndr);
                                    }


                                    break;

                                case Atend.Control.Enum.ProductType.KablSho:
                                    if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.Kablsho))
                                    {
                                        ProductList.Add((int)Atend.Control.Enum.FrameProductType.Kablsho);
                                        System.Data.DataRow ndr = dt_ProductList.NewRow();
                                        ndr["Type"] = Atend.Control.Enum.FrameProductType.Kablsho;
                                        ndr["ProductCode"] = hInfo.ProductCode;
                                        dt_ProductList.Rows.Add(ndr);
                                    }


                                    break;

                                case Atend.Control.Enum.ProductType.Conductor:
                                    if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.Conductor))
                                    {
                                        ProductList.Add((int)Atend.Control.Enum.FrameProductType.Conductor);
                                        System.Data.DataRow ndr = dt_ProductList.NewRow();
                                        ndr["Type"] = Atend.Control.Enum.FrameProductType.Conductor;
                                        ndr["ProductCode"] = hInfo.ProductCode;
                                        dt_ProductList.Rows.Add(ndr);
                                    }
                                    break;

                                case Atend.Control.Enum.ProductType.GroundCabel:
                                    if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.GroundCable))
                                    {
                                        ProductList.Add((int)Atend.Control.Enum.FrameProductType.GroundCable);
                                        System.Data.DataRow ndr = dt_ProductList.NewRow();
                                        ndr["Type"] = Atend.Control.Enum.FrameProductType.GroundCable;
                                        ndr["ProductCode"] = hInfo.ProductCode;
                                        dt_ProductList.Rows.Add(ndr);
                                    }
                                    break;

                                case Atend.Control.Enum.ProductType.SelfKeeper:
                                    if (!ProductList.Contains((int)Atend.Control.Enum.FrameProductType.SelfKeeper))
                                    {
                                        ProductList.Add((int)Atend.Control.Enum.FrameProductType.SelfKeeper);
                                        System.Data.DataRow ndr = dt_ProductList.NewRow();
                                        ndr["Type"] = Atend.Control.Enum.FrameProductType.SelfKeeper;
                                        ndr["ProductCode"] = hInfo.ProductCode;
                                        dt_ProductList.Rows.Add(ndr);
                                    }
                                    break;


                            }
                        }
                        //foreach (int i in ProductList)
                        //{
                        //    ed.WriteMessage("-- {0} --\n", i);
                        //}
                        //ed.WriteMessage("6\n");

                        foreach (System.Data.DataRow dr in dt_ProductList.Rows)
                        {
                            ed.WriteMessage("aaa:{0}\n", dr["Type"]);
                        }
                    }
                }
                catch
                {
                }
            }
            return dt_ProductList;
        }

        public static ObjectId CreateWhiteBack(ObjectId oi)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Transaction tr = db.TransactionManager.StartTransaction();
            ObjectId hatId = ObjectId.Null;

            Entity ent = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);
            Polyline pl = ent as Polyline;

            //Point2dCollection p2 = new Point2dCollection();
            Point2dCollection pts = new Point2dCollection(); //p2;
            for (int i = 0; i < pl.NumberOfVertices; i++)
            {
                Point2d p = pl.GetPoint2dAt(i);
                double a = p.X * 1;
                double b = p.Y * 1;
                //ed.WriteMessage("x:{0},{1} \n", a, b);
                pts.Add(new Point2d(a, b));
            }

            try
            {
                BlockTable bt = (BlockTable)tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                //ed.WriteMessage("11\n");
                //Hatch hat = new Hatch();
                //hat.SetDatabaseDefaults();
                //hat.HatchObjectType = HatchObjectType.GradientObject;
                //hat.SetGradient(GradientPatternType.PreDefinedGradient, "LINEAR");
                //hat.GradientOneColorMode = false;
                //GradientColor[] gcs = new GradientColor[2];
                //gcs[0] = new GradientColor(Color.FromRgb(255, 255, 255), 0);
                //gcs[1] = new GradientColor(Color.FromRgb(255, 255, 255), 1);
                //hat.SetGradientColors(gcs);
                //hatId = btr.AppendEntity(hat);
                //tr.AddNewlyCreatedDBObject(hat, true);
                //ObjectIdCollection ids = new ObjectIdCollection();
                //ids.Add(oi);
                //hat.Associative = true;
                //hat.AppendLoop(HatchLoopTypes.Default, ids);
                //hat.EvaluateHatch(true);




                //ed.WriteMessage("12\n");
                //pts.Add(new Point2d(0.0, 0.0));
                //pts.Add(new Point2d(100.0, 0.0));
                //pts.Add(new Point2d(100.0, 120.0));
                //pts.Add(new Point2d(0.0, 100.0));
                //pts.Add(new Point2d(0.0, 0.0));


                Wipeout wo = new Wipeout();
                //ed.WriteMessage("13\n");
                //wo.SetDatabaseDefaults(db);
                wo.SetFrom(pts, new Vector3d(0.0, 0.0, 0.1));
                wo.LayerId = Atend.Global.Acad.UAcad.GetLayerById(Atend.Control.Enum.AutoCadLayerName.SHELL.ToString());
                //ed.WriteMessage("14\n");
                Point2dCollection p2dc = wo.GetClipBoundary();
                //ed.WriteMessage("15\n");
                btr.AppendEntity(wo);
                //ed.WriteMessage("16\n");
                tr.AddNewlyCreatedDBObject(wo, true);
                //ed.WriteMessage("17\n");
                tr.Commit();
                //ed.WriteMessage("18\n");



            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR Wipeout : {0} \n", ex.Message);
            }
            return hatId;
        }

        public static ObjectId CreateWhiteBack(Point2dCollection AllPoints)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("ÔÑæÚ" + "\n");
            //ed.WriteMessage("WHITE \n");
            ObjectId hatId = ObjectId.Null;
            Document doc = Application.DocumentManager.MdiActiveDocument;

            Database db = doc.Database;

            Transaction tr = db.TransactionManager.StartTransaction();

            using (tr)
            {

                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead, false);

                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite, false);

                Point2dCollection pts = AllPoints; //new Point2dCollection(5);

                //pts.Add(AllPoints[0]);
                //pts.Add(AllPoints[1]);
                //pts.Add(AllPoints[2]);
                //pts.Add(AllPoints[3]);
                //pts.Add(AllPoints[0]);

                //pts.Add(new Point2d(0.0, 0.0));

                //pts.Add(new Point2d(100.0, 0.0));

                //pts.Add(new Point2d(100.0, 100.0));

                //pts.Add(new Point2d(0.0, 100.0));

                //pts.Add(new Point2d(0.0, 0.0));

                Wipeout wo = new Wipeout();

                wo.LayerId = Atend.Global.Acad.UAcad.GetLayerById(Atend.Control.Enum.AutoCadLayerName.SHELL.ToString());

                wo.SetDatabaseDefaults(db);

                wo.SetFrom(pts, new Vector3d(0.0, 0.0, 0.1));

                btr.AppendEntity(wo);

                tr.AddNewlyCreatedDBObject(wo, true);

                tr.Commit();

            }
            return hatId;
        }


    }

    public class Notification
    {

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string hlink;

        public string Hlink
        {
            get { return hlink; }
            set { hlink = value; }
        }

        private string htext;

        public string Htext
        {
            get { return htext; }
            set { htext = value; }
        }

        private string msg;

        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        private string msg2;

        public string Msg2
        {
            get { return msg2; }
            set { msg2 = value; }
        }



        [CommandMethod("NotificationBalloon")]
        public void ShowStatusBarBalloon()
        {

            const string appName = "ATEND SOFTWARE";

            Document doc = Application.DocumentManager.MdiActiveDocument;

            TrayItem ti = new TrayItem();

            ti.ToolTipText = appName;

            //ti.Icon = doc.StatusBar.TrayItems[0].Icon;

            Application.StatusBar.TrayItems.Add(ti);

            TrayItemBubbleWindow bw = new TrayItemBubbleWindow();

            bw.Title = Title;

            bw.HyperText = Htext;

            bw.HyperLink = Hlink;

            bw.Text = Msg;

            bw.Text2 = Msg2;

            bw.IconType = IconType.Information;

            ti.ShowBubbleWindow(bw);

            Application.StatusBar.Update();

            bw.Closed += delegate(object o, TrayItemBubbleWindowClosedEventArgs args)
              {

                  try
                  {

                      Application.StatusBar.TrayItems.Remove(ti);

                      Application.StatusBar.Update();

                  }
                  catch
                  {
                  }

              };

        }

        public void infoCenterBalloon()
        {

            InfoCenterManager icm = AcInfoCenterConn.InfoCenterManager;
            Autodesk.InfoCenter.PaletteMgr pm = icm.PaletteManager;
            pm.ShowBalloon(Title, Msg, null, new System.Uri("http://www.moshiran-co.com"), 5, 1);

        }



    }

    public class DrawinOperation
    {
        public void AddFileToAtendFile(string zipFilename, string fileToAdd)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("ZipFileName:{0} FileToAdd:{1} \n", zipFilename, fileToAdd);
            using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
            {
                string destFilename = ".\\" + Path.GetFileName(fileToAdd);
                //ed.WriteMessage("DESFILE:{0} \n", destFilename);
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                if (zip.PartExists(uri))
                {
                    //ed.WriteMessage("Part Exit\n");
                    zip.DeletePart(uri);
                }
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);
                using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
                {
                    //ed.WriteMessage("fileToAdd:{0} \n", fileToAdd);
                    using (Stream dest = part.GetStream())
                    {

                        CopyStream(fileStream, dest);
                        //ed.WriteMessage("write again \n");
                    }
                }
            }
        }

        public void GetFileFromAtendFile(string zipFilename, string Destination)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
            {
                FileInfo AtnxFile = new FileInfo(zipFilename);
                bool MainDWGFound = false;
                foreach (PackagePart prt in zip.GetParts())
                {
                    Stream outputFile = prt.GetStream(FileMode.Open, FileAccess.Read);
                    //ed.WriteMessage("~~~~~~~~~~ file name: {0} ~~~~~~~~~~~~~~\n", prt.Uri.ToString());
                    //prt.Package 
                    FileStream fs = null;
                    if (prt.Uri.ToString().ToUpper().LastIndexOf(".DWG") >= 0)
                    {
                        //ed.WriteMessage("DDD \n");
                        if (MainDWGFound == false)
                        {
                            fs = new FileStream(string.Format(@"{0}\{1}", Destination, AtnxFile.Name.Replace(".ATNX", ".DWG") /*prt.Uri.ToString()*/), FileMode.Create);
                            MainDWGFound = true;
                        }
                        else
                        {
                            fs = new FileStream(string.Format(@"{0}\{1}", Destination, prt.Uri.ToString()), FileMode.Create);
                            //ed.WriteMessage(string.Format(@"{0}\{1}", Destination, prt.Uri.ToString()));
                        }
                    }
                    else if (prt.Uri.ToString().ToUpper().LastIndexOf(".MDB") >= 0)
                    {
                        //ed.WriteMessage("MMM \n");
                        fs = new FileStream(string.Format(@"{0}\{1}", Destination, AtnxFile.Name.Replace(".ATNX", ".MDB") /*prt.Uri.ToString()*/), FileMode.Create);
                    }
                    if (fs != null)
                    {
                        long bufferSize = outputFile.Length < BUFFER_SIZE ? outputFile.Length : BUFFER_SIZE;
                        byte[] buffer = new byte[bufferSize];
                        int bytesRead = 0;
                        long bytesWritten = 0;
                        while ((bytesRead = outputFile.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            fs.Write(buffer, 0, bytesRead);
                            bytesWritten += bufferSize;
                        }
                        fs.Dispose();
                    }
                    //ed.WriteMessage("~~~~~~~~ 1: {0} ~~~~~~~~~~~~ \n", AtnxFile.FullName);
                    //FileInfo outPutFile = new FileInfo(string.Format(@"{0}\{1}", Destination, prt.Uri.ToString()));
                    //ed.WriteMessage(@"~~~~~~~~ 2: {0}:::{1} ~~~~~~~~~~~~ \n", AtnxFile.DirectoryName, AtnxFile.Name);

                }
            }
        }

        private void CopyStream(System.IO.FileStream inputStream, System.IO.Stream outputStream)
        {
            long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            long bytesWritten = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                bytesWritten += bufferSize;
            }
        }

        public byte[] NewAtendFile(string Path, string Name)
        {
            byte[] content = null;


            string DesignFile = string.Format(@"{0}\DesignFile\AtendEmpty.dwg", Atend.Control.Common.fullPath);
            string DatabaseFile = string.Format(@"{0}\DatabaseFile\AtendLocal.mdb", Atend.Control.Common.fullPath);
            string Destination = string.Format(@"{0}\{1}", Path, Name);

            if (File.Exists(DesignFile) && File.Exists(DatabaseFile))
            {
                if (!Directory.Exists(Destination))
                {
                    Directory.CreateDirectory(Destination);

                    File.Copy(DesignFile, string.Format(@"{0}\{1}.DWG", Destination, Name));
                    File.Copy(DatabaseFile, string.Format(@"{0}\{1}.MDB", Destination, Name));

                    Atend.Global.Acad.DrawinOperation dOperation = new Atend.Global.Acad.DrawinOperation();
                    dOperation.AddFileToAtendFile(string.Format(@"{0}\{1}.ATNX", Destination, Name), string.Format(@"{0}\{1}.DWG", Destination, Name));
                    dOperation.AddFileToAtendFile(string.Format(@"{0}\{1}.ATNX", Destination, Name), string.Format(@"{0}\{1}.MDB", Destination, Name));


                    FileStream fs;
                    fs = File.Open(string.Format(@"{0}\{1}.ATNX", Destination, Name), FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    content = br.ReadBytes((Int32)br.BaseStream.Length);
                    fs.Dispose();

                }

            }


            return content;
        }

        public bool LoadAtendFile(byte[] Content, string Path, string Name)
        {
            Stream st = new MemoryStream();
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format("@{0}/{1}", Path, Name));
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);

                    if (!Directory.Exists(string.Format(@"{0}\{1}", Path, Name)))
                    {
                        Directory.CreateDirectory(string.Format(@"{0}\{1}", Path, Name));
                    }
                }


                st = File.Create(string.Format(@"{0}\{1}\{2}.ATNX", Path, Name, Name));
                BinaryWriter binWriter = new BinaryWriter(st);
                binWriter.Write((byte[])Content);
                binWriter.Close();

                string FilePath = string.Format(@"{0}\{1}\{2}.ATNX", Path, Name, Name);

                FileInfo fi = new FileInfo(FilePath);
                string Destination = fi.Directory.FullName;
                Atend.Global.Acad.DrawinOperation dOperation = new Atend.Global.Acad.DrawinOperation();
                dOperation.GetFileFromAtendFile(FilePath, Destination);
                string NewName = fi.FullName.Replace(".ATNX", ".DWG");
                Atend.Control.Common.DesignFullAddress = Destination;
                Atend.Control.Common.DesignName = fi.Name.Replace(".ATNX", ".DWG");
                Atend.Control.Common.AccessPath = string.Format(@"{0}\{1}", Atend.Control.Common.DesignFullAddress, fi.Name.Replace(".ATNX", ".MDB"));
                System.Diagnostics.Process.Start(NewName);


            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in LoadAtendFile={0}\n", ex.Message);
                return false;
            }
            return true;


            return false;
        }

        private const long BUFFER_SIZE = 4096;

    }

}
