using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Global.Acad
{
    public class AcadUpdate
    {
        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //public bool UpdatePole(ObjectId objPole)
        //{
           
        //    Atend.Base.Acad.AcadGlobal.dConsolCode.Clear();
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlTransaction Transaction;
        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();
        //        try
        //        {

        //            ed.WriteMessage("~~~~~~~ Start Update Pole Information ~~~~~~~~\n");

        //            if (Atend.Base.Acad.AcadGlobal.dNode.UpdateProductCode(Transaction, Connection))
        //            {
        //                //ed.writeMessage("Update DNode\n");
        //               objPole=UAcad.ChangePoleShape(objPole, Atend.Base.Acad.AcadGlobal.dNode.ProductCode);
        //                //ed.writeMessage("enter Delete\n");
        //                for (int i = 0; i < Atend.Base.Acad.AcadGlobal.dPackageForDelete.Count; i++)
        //                {
        //                    if (Desig.NodeTransaction.DeleteConsol(new Guid(Atend.Base.Acad.AcadGlobal.dPackageForDelete[i].ToString()), Connection, Transaction))
        //                    {
        //                        //ed.writeMessage("DeleteConsol\n");
        //                    }
        //                    else
        //                    {
        //                        //ed.writeMessage(string.Format("Error NodeTransaction.DeleteConsol :  \n"));
        //                        throw new Exception("while Deleting Consol package information");
        //                    }
        //                }
        //                //ed.writeMessage("@@@@@@@@ DELETE FROM DPACKAGE FINISHED @@@@@@@@@@\n");
                        

        //                //ed.writeMessage("Consol Count For delete:{0}\n", Atend.Base.Acad.AcadGlobal.dPackageForDelete.Count);
        //                for (int i = 0; i < Atend.Base.Acad.AcadGlobal.dPackageForDelete.Count; i++)
        //                {
        //                    //ed.writeMessage("New Pole OI :{0} \nCurrent Consol:{1} \n", objPole, Atend.Base.Acad.AcadGlobal.dPackageForDelete[i].ToString());
        //                    AcadRemove.DeleteConsol(objPole, Atend.Base.Acad.AcadGlobal.dPackageForDelete[i].ToString());
        //                }

        //                //ed.writeMessage("@@@@@@@@ DELETE FROM Graphic FINISHED @@@@@@@@@@\n");

        //                if (Atend.Base.Acad.AcadGlobal.dPoleInfo.Update(Transaction, Connection))
        //                {
        //                    //ed.WriteMessage(string.Format("I am going to insert package \n"));
        //                    //Atend.Base.Acad.AcadGlobal.dPackage.NodeCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                    //myDPackage.ParentCode = new Guid();
        //                    //if (Atend.Base.Acad.AcadGlobal.dPackage.Insert(Transaction, Connection))
        //                    //{
        //                    //ed.WriteMessage("Pole record inserted to dpackage\n");
        //                    //Done Successfully
        //                    //ed.WriteMessage(string.Format("Going to packages iteration. \n"));
        //                    //ed.WriteMessage("number of rows in MyDPackages: " + MyDPackages.Count.ToString() + "\n");
        //                    for (int i = 0; i < Atend.Base.Acad.AcadGlobal.dPackages.Count; i++)
        //                    {
        //                        Atend.Base.Design.DPackage tempPackage = (Atend.Base.Design.DPackage)Atend.Base.Acad.AcadGlobal.dPackages[i];
        //                        //ed.WriteMessage(string.Format("MyDPackages.type : {0}\n", tempPackage.Type));
        //                        //PromptIntegerResult r31 = ed.GetInteger("sss :");
        //                        //tempPackage.ParentCode = Atend.Base.Acad.AcadGlobal.dPackage.Code;
        //                        //ed.WriteMessage(string.Format("I am going to packag insert \n"));
        //                        if (tempPackage.Insert(Transaction, Connection))
        //                        {
        //                            //ed.WriteMessage(string.Format(">>> code: {0} nodecode :{1} parentcode: {2} type:{3} productcode: {4} \n", tempPackage.Code, tempPackage.NodeCode, tempPackage.ParentCode, tempPackage.Type, tempPackage.ProductCode));
        //                            //PromptIntegerResult r2 = ed.GetInteger("dPackage inserted:");
        //                            //save sub equips
        //                            if (tempPackage.Type == (int)Atend.Control.Enum.ProductType.Consol)
        //                            {
        //                                //ed.writeMessage("TepmPackage.Code= " + tempPackage.Code.ToString() + "\n");
        //                                Atend.Base.Acad.AcadGlobal.dConsolCode.Add(tempPackage.Code);
                                        
        //                                Atend.Base.Acad.AcadGlobal.dConsol.Code = tempPackage.Code;
        //                                Atend.Base.Acad.AcadGlobal.dConsol.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //                                Atend.Base.Acad.AcadGlobal.dConsol.IsExistance = tempPackage.IsExistance;
        //                                Atend.Base.Acad.AcadGlobal.dConsol.ParentCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                                Atend.Base.Acad.AcadGlobal.dConsol.ProductCode = tempPackage.ProductCode;
        //                                Atend.Base.Acad.AcadGlobal.dConsol.LoadCode = 0;
        //                                if (Atend.Base.Acad.AcadGlobal.dConsol.Insert(Transaction, Connection))
        //                                {
        //                                    //ed.writeMessage(string.Format("All Information for dConsol saved successfully \n"));
        //                                }
        //                                else
        //                                {
        //                                    //ed.writeMessage(string.Format("Error NodeTransaction.InsertDconsol  \n"));
        //                                    throw new Exception("while saving dConsol package information");
        //                                }

        //                                //PromptIntegerResult r3 = ed.GetInteger("type is consol :");
        //                                //ed.WriteMessage(string.Format("package was a consol and go to sub product \n"));
        //                                //ed.writeMessage("Go to SubProducts. \n");
        //                                if (SubProducts(tempPackage.ProductCode, tempPackage.Type, tempPackage.Code, Transaction, Connection))
        //                                {
        //                                    ed.WriteMessage(string.Format("All Information for Pole saved successfully \n"));
        //                                }
        //                                else
        //                                {
        //                                    //ed.writeMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole sub package information  \n"));
        //                                    throw new Exception("while saving pole sub package information");
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            //ed.writeMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole package information  \n"));
        //                            throw new Exception("while saving pole package information");
        //                        }
        //                    }
                            
        //                    //}
        //                    //else
        //                    //{
        //                    //    ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DPackage for pole information \n");
        //                    //    throw new Exception("while saving DPackage information");
        //                    //}
        //                }
        //                else
        //                {
        //                    //ed.writeMessage("Error NodeTransaction.InsertPole : while saving DPoleInfo information \n");
        //                    throw new Exception("while saving DPoleInfo information");
        //                }

        //            }
        //            else
        //            {
        //                //ed.writeMessage("Error NodeTransaction.InsertPole : while saving DNode information \n");
        //                throw new Exception("while saving DNode information");
        //            }
        //            //}
        //            //else
        //            //{
        //            //    ed.WriteMessage("Error NodeTransaction.DesignSettngUpdate : while updating DesignSetting information \n");
        //            //    throw new Exception("while saving DNode information");
        //            //}

        //            ed.WriteMessage("~~~~~~~End Save Pole Information ~~~~~~~~~\n");


        //        }
        //        catch (System.Exception ex2)
        //        {
        //            ed.WriteMessage(string.Format("Error NodeTransaction.Insert  : {0} \n", ex2.Message));
        //            Transaction.Rollback();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error NodeTransaction.Insert  : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //    Transaction.Commit();
        //    try
        //    {
        //        Atend.Global.Acad.UAcad.insertConsol(objPole, Atend.Base.Acad.AcadGlobal.dConsolCode);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("Error InsertConsol:= "+ex.Message+"\n");
        //    }
        //        Connection.Close();
        //    return true;
        //}

        //public bool SubProducts(int ContainerCode, int Type, Guid ParentCode, SqlTransaction Transaction, SqlConnection Connection)
        //{

        //    System.Data.DataTable productPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerCodeAndType(ContainerCode, Type);
        //    foreach (DataRow row1 in productPackageTable.Rows)
        //    {
        //        Atend.Base.Design.DPackage packageTemp = new Atend.Base.Design.DPackage();
        //        packageTemp.Count = Convert.ToInt32(row1["Count"]);
        //        packageTemp.ParentCode = ParentCode;
        //        packageTemp.Type = Convert.ToInt32(row1["TableType"]);
        //        packageTemp.ProductCode = Convert.ToInt32(row1["ProductCode"]);

        //        if (packageTemp.Insert(Transaction, Connection))
        //        {

        //            SubProducts(Convert.ToInt32(row1["ProductCode"]), Convert.ToInt32(row1["TableType"]), packageTemp.Code, Transaction, Connection);

        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}
