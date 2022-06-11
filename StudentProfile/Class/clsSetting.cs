using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Collections.Generic;
using System.Globalization;
using MyDBManager;
using BusinessLayer;
using System.Data.OleDb;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for clsSetting
/// </summary>
public class clsSetting
{
    public static string _ServerNameCnn = string.Empty;
    public static string _UserNameCnn = string.Empty;
    public static string _PasswordCnn = string.Empty;
    public static string _DatabaseNameCnn = string.Empty;
    

    public static string _DataSourceUserIDName = "  User Id=DBUserName;Password=DBPassword;Persist Security Info=True;Initial Catalog=";

    public clsSetting()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetConnectionString()
    {
        string cnnstring = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        return cnnstring;

        //string cnnstring = "Data Source=" + clsSetting._ServerNameCnn + ";";

        //string _Dbconnectionname = _DataSourceUserIDName;
        //_Dbconnectionname = _Dbconnectionname.Replace("DBPassword", _dbPassword);
        //_Dbconnectionname = _Dbconnectionname.Replace("DBUserName", _dbUserName);

        //return cnnstring += _Dbconnectionname + _dbname;
    }
    public static string GetConnectionString_Master()
    {
        //string cnn = System.Configuration.ConfigurationManager.AppSettings["ConnectionString_Master"].ToString();
        string cnn = "Data Source=" + clsSetting._ServerNameCnn + ";User Id=" + clsSetting._UserNameCnn + ";Password=" + clsSetting._PasswordCnn + ";Initial Catalog=" + clsSetting._DatabaseNameCnn + ";Persist Security Info=True";
        return cnn;
    }
   

    public static string GetConnectionStringManual(string _dbname, string _dbUserName, string _dbPassword)
    {
        string cnnstring = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();

        string _Dbconnectionname = _DataSourceUserIDName;
        _Dbconnectionname = _Dbconnectionname.Replace("DBPassword", _dbPassword);
        _Dbconnectionname = _Dbconnectionname.Replace("DBUserName", _dbUserName);

        return cnnstring += _Dbconnectionname + _dbname;
    }


    #region CheckRefrence
    public static Boolean CheckRefrence(string Tablename, string condition)
    {
        string Query = "Select count(*) from " + Tablename + " where 1=1 " + condition;
        DBManager ObjDbmanager = new DBManager();
        ObjDbmanager.InitConnectionObject(GetConnectionString());
        if (Convert.ToInt32(ObjDbmanager.ExecuteMyScalar(Query)) > 0)
        {
            ObjDbmanager = null;
            return true;
        }
        else
        {
            ObjDbmanager = null;
            return false;
        }
    }
    #endregion


}
