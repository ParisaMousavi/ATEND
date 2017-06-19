using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Control
{
    public class ConnectionString
    {


        private static string localconnection;
        public static string LocalcnString
        {
            get
            {
                localconnection = ReadFromConfig("LocalConnectionString");
                if (localconnection == "NONE")
                    return null;
                return localconnection;
            }
            set
            {

                WriteToConfig("LocalConnectionString", value);
                localconnection = value;
            }
        }

        private static string serverconnection;
        public static string ServercnString
        {
            get
            {
                serverconnection = ReadFromConfig("ServerConnectionString");
                if (serverconnection == "NONE")
                    return null;
                return serverconnection;
            }
            set
            {

                WriteToConfig("ServerConnectionString", value);
                serverconnection = value;
            }
        }

        //private static string supportconnection;
        //public static string Supportconnection
        //{
        //    get
        //    {
        //        supportconnection = ReadFromConfig("SupportConnectionString");
        //        if (supportconnection == "NONE")
        //            return null;
        //        return supportconnection;
        //    }
        //    set
        //    {

        //        WriteToConfig("SupportConnectionString", value);
        //        supportconnection = value;
        //    }
        //}

        private static string accessconnecion;
        public static string AccessCnString
        {
            get
            {
                //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Z:\AccessDataBase\AtendLocal.mdb;Persist Security Info=False;Jet OLEDB:Database Password=321
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //accessconnecion = string.Format("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False;Jet OLEDB:Database Password=321", Atend.Control.Common.AccessPath);
                Atend.Control.Common.AccessPath = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Name.Replace("DWG", "MDB");
                ed.WriteMessage("NAME:{0}\n", Atend.Control.Common.AccessPath);
                accessconnecion = string.Format("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False;Jet OLEDB:Database Password=321", Atend.Control.Common.AccessPath);
                return accessconnecion;
            }
            //set
            //{
            //   accessconnecion = string.Format("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source={0}", Atend.Control.Common.AccessPath);
            //    accessconnecion = value;
            //}
        }

        private static string newAccessconnecion;
        public static string NewAccessconnecion
        {
            get
            {

                //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Z:\AccessDataBase\AtendLocal.mdb;Persist Security Info=False;Jet OLEDB:Database Password=321
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //accessconnecion = string.Format("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False;Jet OLEDB:Database Password=321", Atend.Control.Common.AccessPath);
                Atend.Control.Common.NewAccessPath = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Name.Replace(".DWG", "-NEW.MDB");
                ed.WriteMessage("NAME:{0}\n", Atend.Control.Common.AccessPath);
                ConnectionString.newAccessconnecion = string.Format("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False;Jet OLEDB:Database Password=321", Atend.Control.Common.NewAccessPath);
                return ConnectionString.newAccessconnecion;
            }
            //set { ConnectionString.newAccessconnecion = value; }
        }


        private static string lST;
        public static string LST
        {
            get
            {

                lST = ReadFromConfig("LST");
                if (lST == "NONE")
                    return null;
                return lST;
            }
            set
            {

                WriteToConfig("LST", value);
                lST = value;
            }
        }

        private static bool statusDef;
        public static bool StatusDef
        {
            get
            {
                statusDef = Convert.ToBoolean(ReadFromConfig("StatusDef"));
                return ConnectionString.statusDef;
            }
            set
            {
                ConnectionString.statusDef = value;
                WriteToConfig("StatusDef", value.ToString());
            }
        }


        private static string recentPath;
        public static string RecentPath
        {
            get
            {

                recentPath = ReadFromConfig("RecentPath");
                if (recentPath == "NONE")
                    return null;
                return recentPath;



                //return ConnectionString.recentPath; 
            }
            set
            {
                WriteToConfig("RecentPath", value);
                recentPath = value;

                //ConnectionString.recentPath = value;
            }
        }

        private static string logoname;
        public static string LogoName
        {
            get
            {

                logoname = ReadFromConfig("LogoName");
                if (logoname == "NONE")
                    return null;
                return logoname;

            }
            set
            {
                WriteToConfig("LogoName", value);
                logoname = value;

            }
        }

        private static string logopath;
        public static string LogoPath
        {
            get
            {

                logopath = ReadFromConfig("LogoPath");
                if (logopath == "NONE")
                    return null;
                return logopath;

            }
            set
            {
                WriteToConfig("LogoPath", value);
                logopath = value;

            }
        }

        private static bool followParent;
        public static bool FollowParent
        {
            get
            {
                followParent = Convert.ToBoolean(ReadFromConfig("FollowParent"));
                return ConnectionString.followParent;
            }
            set
            {
                ConnectionString.followParent = value;
                WriteToConfig("FollowParent", value.ToString());

            }
        }

        public static bool ConnectionValidation(string ConnectionString)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string ReadFromConfig(string keyName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            string fullPath = m.FullyQualifiedName;
            try
            {
                fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
            }
            catch
            {
            }
            string xmlPath = fullPath + "\\Atend.dll.config";
            xmlDoc.Load(xmlPath);

            foreach (XmlElement xElement in xmlDoc.DocumentElement)
            {

                if (xElement.Name == "appSettings")
                {

                    foreach (XmlNode Xnode in xElement.ChildNodes)
                    {

                        if (Xnode.Attributes[0].Value == keyName)
                        {
                            return Xnode.Attributes[1].Value;
                        }
                    }
                }
            }
            return "NULL";
        }

        private static void WriteToConfig(string keyName, string newValue)
        {
            System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            string fullPath = m.FullyQualifiedName;
            try
            {
                fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
            }
            catch
            {
            }

            string xmlPath = fullPath + "\\Atend.dll.config";

            //ed.WriteMessage("\nstart of write to config\n");

            XmlDocument xmlDoc = new XmlDocument();
            //string xmlPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            xmlDoc.Load(xmlPath);

            //ed.WriteMessage("\n Path = " + xmlPath + "\n");

            //ed.WriteMessage("\n" + xmlDoc.InnerXml + "\n");
            //ed.WriteMessage("\n" + xmlDoc.InnerText + "\n");

            foreach (XmlElement xElement in xmlDoc.DocumentElement/* int i = 0; ! string.IsNullOrEmpty(xmlDoc.DocumentElement[i].Name); i++ */)
            {
                if (xElement.Name == "appSettings")
                {
                    foreach (XmlNode Xnode in xElement.ChildNodes)
                    {
                        if (Xnode.Attributes[0].Value == keyName)
                        {
                            Xnode.Attributes[1].Value = newValue;
                        }
                    }

                }

            }


            xmlDoc.Save(xmlPath);

            //ed.WriteMessage("\nend of write to config\n");

        }

        private SqlConnection singleSqlConnectionLocal;
        public SqlConnection SingleSqlConnectionLocal
        {
            get
            {
                return singleSqlConnectionLocal;
            }
            //set { singleSqlConnectionLocal = value; }
        }

        public void CloseSingleSqlConnectionLocal()
        {
            if (singleSqlConnectionLocal.State == ConnectionState.Open)
                singleSqlConnectionLocal.Close();
        }

        public void OpenSingleSqlConnectionLocal()
        {
            singleSqlConnectionLocal = new SqlConnection(Atend.Control.ConnectionString.localconnection);
            try
            {
                singleSqlConnectionLocal.Open();
            }
            catch
            {
                singleSqlConnectionLocal.Close();
                singleSqlConnectionLocal = null;
            }
        }

        private OleDbConnection singleConnectionAccess;
        public OleDbConnection SingleConnectionAccess
        {
            get { return singleConnectionAccess; }
            //set { singleSqlConnectionAccess = value; }
        }

        public void CloseSingleConnectionAccess()
        {
            if (singleConnectionAccess.State == ConnectionState.Open)
                singleConnectionAccess.Close();
        }

        public void OpenSingleConnectionAccess()
        {
            singleConnectionAccess = new OleDbConnection(Atend.Control.ConnectionString.accessconnecion);
            try
            {
                singleConnectionAccess.Open();
            }
            catch
            {
                singleConnectionAccess.Close();
                singleSqlConnectionLocal = null;
            }
        }


    }

}
