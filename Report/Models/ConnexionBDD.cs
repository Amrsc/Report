using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using System.Web;

namespace Report.Models
{
    public static class ConnexionBDD
    {
        public static SqlConnection GetDBConnection()
        {
            //
            // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
            //
            //string connString = @"Data Source=LAPTOP-ALIK-AMR\REPORT;Initial Catalog=PortailSof;Persist Security Info=True;User ID=sa;Password=saa";


            string connString = ConfigurationManager.ConnectionStrings["Connexionbdd"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }
        public static OracleConnection GetDBConnectionOracle()
        {

            string connString = ConfigurationManager.ConnectionStrings["ConnexionORACLE"].ConnectionString;
            OracleConnection conn = new OracleConnection(connString);

            return conn;
        }


    }
}