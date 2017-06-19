using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Design
{
    public class DAccess
    {

        int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_Access_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        //public static bool ShareUserAccessOnServer()
        //{

        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    SqlConnection Serverconnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlTransaction Servertransaction;

        //    try
        //    {
        //        Serverconnection.Open();
        //        Servertransaction = Serverconnection.BeginTransaction();

        //        try
        //        {
        //            DataTable LocalUserTbl = Atend.Base.Design.DUser.SelectAll();

        //            foreach (DataRow LocalUserRow in LocalUserTbl.Rows)
        //            {
        //                Atend.Base.Design.DUser User = Atend.Base.Design.DUser.ServerSelectByUsernameAndPassword(Servertransaction, Serverconnection, LocalUserRow["UserName"].ToString(), LocalUserRow["PassWord"].ToString());

        //                if (User.Code != -1)
        //                {
        //                    if (!Atend.Base.Design.DUser.ServerDelete(Servertransaction, Serverconnection, User.Code))
        //                    {
        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        return false;
        //                    }
        //                }

        //                User.Name = LocalUserRow["Name"].ToString();
        //                User.Family = LocalUserRow["Family"].ToString();
        //                User.Password = LocalUserRow["Password"].ToString();
        //                User.UserName = LocalUserRow["UserName"].ToString();

        //                DataTable AccessForUser = DUserAccess.SelectByUserId(Convert.ToInt32(LocalUserRow["Code"].ToString()));
        //                foreach (DataRow dr in AccessForUser.Rows)
        //                {
        //                    User.AccessList.Add(Convert.ToInt32(dr["IdAccess"]));
        //                }

        //                if (!User.insert(Servertransaction, Serverconnection))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    return false;
        //                }


        //            }

        //            Servertransaction.Commit();
        //            Serverconnection.Close();
        //            return true;

        //        }
        //        catch (System.Exception exp)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction of ShareUserAccessOnServer:{0}\n", exp.Message));
        //            Servertransaction.Rollback();
        //            Serverconnection.Close();

        //            return false;
        //        }

        //    }
        //    catch (System.Exception exp)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR DACCESS.ShareUserAccessOnServer {0}\n", exp.Message));

        //        Serverconnection.Close();
        //        return false;
        //    }

        //    return true;

        //}

        public static bool GetUserAccessFromServer()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlTransaction Localtransaction;

            try
            {
                Localconnection.Open();
                Localtransaction = Localconnection.BeginTransaction();

                try
                {
                    DataTable ServerUserTbl = Atend.Base.Design.DUser.ServerSelectAll();

                    foreach (DataRow ServerUserRow in ServerUserTbl.Rows)
                    {
                        Atend.Base.Design.DUser User = Atend.Base.Design.DUser.SelectByUsernameAndPassword(Localtransaction, Localconnection, ServerUserRow["UserName"].ToString(), ServerUserRow["PassWord"].ToString());

                        if (User.Code != -1)
                        {
                            if (!Atend.Base.Design.DUser.Delete(Localtransaction, Localconnection, User.Code))
                            {
                                Localtransaction.Rollback();
                                Localconnection.Close();
                                return false;
                            }
                        }

                        User.Name = ServerUserRow["Name"].ToString();
                        User.Family = ServerUserRow["Family"].ToString();
                        User.Password = ServerUserRow["Password"].ToString();
                        User.UserName = ServerUserRow["UserName"].ToString();

                        DataTable AccessForUser = DUserAccess.ServerSelectByUserId(Convert.ToInt32(ServerUserRow["Code"].ToString()));
                        foreach (DataRow dr in AccessForUser.Rows)
                        {
                            User.AccessList.Add(Convert.ToInt32(dr["IdAccess"]));
                        }

                        if (!User.insertX(Localtransaction, Localconnection))
                        {
                            Localtransaction.Rollback();
                            Localconnection.Close();
                            return false;
                        }


                    }

                    Localtransaction.Commit();
                    Localconnection.Close();
                    return true;
                }
                catch (System.Exception exp)
                {
                    ed.WriteMessage(string.Format("Error In TransAction of GetUserAccessFromServer:{0}\n", exp.Message));
                    Localtransaction.Rollback();
                    Localconnection.Close();

                    return false;
                }

            }
            catch (System.Exception exp)
            {
                ed.WriteMessage(string.Format(" ERROR DACCESS.GetUserAccessFromServer {0}\n", exp.Message));

                Localconnection.Close();
                return false;
            }

            return true;


        }


    }
}
