using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Web;

namespace Report.Models
{
    public class GEtat
    {
        public static bool NewEtat(Etat etat)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof;INSERT INTO [etat] VALUES ('" + etat.NomEtat + "','" + etat.DesEtat + "','" + etat.Form + "','" + etat.CatId + "' ,'"+etat.Commun+"' ); ", con);
            ins.ExecuteNonQuery();
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            verif = true;
            return verif;
        }

        public static bool DelEtat(int id)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof;delete from [etat] where EtatId='" + id + "'; ", con);
            ins.ExecuteNonQuery();
            SqlCommand ins2 = new SqlCommand("USE PortailSof;delete from [privilege] where EtatId='" + id + "'; ", con);
            ins2.ExecuteNonQuery();
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            verif = true;
            return verif;
        }
        public static DataTable ListEtat()
        {
            DataTable dt = new DataTable();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT  * FROM [PortailSof].[dbo].[etat] ;", con);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public static Etat getEtat(int id)
        {
            Etat etat = new Etat();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT  * FROM [PortailSof].[dbo].[etat] where EtatId ='" + id + "';", con);
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        etat.EtatId = (int)reader["EtatId"];
                        etat.NomEtat = (string)reader["NomEtat"];
                        etat.DesEtat = (string)reader["DesEtat"];
                        etat.Form = (string)reader["Form"];
                        etat.CatId = (int)reader["CatId"];
                    }
                }
            }
            con.Close();
            return etat;
        }
        /*
        public static void NewQuery(string NomRequet)
        {
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof;if not exists(select * from [query] where NomQuery='" + NomRequet + "')INSERT INTO [query] VALUES ('" + NomRequet + "','' ); ", con);
            ins.ExecuteNonQuery();
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
        }
        public static bool delQuery(int id)
        {
            bool ok = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof; delete from [query] where id =" + id + " ); ", con);
            ins.ExecuteNonQuery();
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            ok = true;
            return ok;
        }
        public static bool UpdateQuery(string query , int id)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof;update [query] set Query ='"+query+"' where id="+id+"  ", con);
            ins.ExecuteNonQuery();
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            verif = true;
            return verif;
        }
        public static DataTable ListGetQuery()
        {
            DataTable dt = new DataTable();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT  * FROM [PortailSof].[dbo].[query] ;", con);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public static DataTable executeQ(string sql)
        {
            DataTable dt = new DataTable();
            return dt;
        }*/
        public static List<Dossier> DosCBMIM()
        {
            List<Dossier> dos1 = new List<Dossier>();
            OracleConnection con = ConnexionBDD.GetDBConnectionOracle();

            con.Open();
            string cmdgeury = "select * from dossier where tpgcode = 'CBMIM'";
            //cmd.CommandText = 
            OracleCommand cmd = new OracleCommand(cmdgeury);

            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Dossier d = new Dossier();
                d.DosNum = reader["DOSNUM"].ToString();
                dos1.Add(d);
            }
            return (dos1);
        }
        public static List<Dossier> Dos()
        {
            List<Dossier> dos1 = new List<Dossier>();
            OracleConnection con = ConnexionBDD.GetDBConnectionOracle();

            con.Open();
            string cmdgeury = "select * from dossier";
            //cmd.CommandText = 
            OracleCommand cmd = new OracleCommand(cmdgeury);

            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Dossier d = new Dossier();
                d.DosNum = reader["DOSNUM"].ToString();
                dos1.Add(d);
            }
            return (dos1);
        }

        public static List<Acteur> act()
        {
            List<Acteur> act1 = new List<Acteur>();
            OracleConnection con = ConnexionBDD.GetDBConnectionOracle();

            con.Open();
            string cmdgeury = "select distinct act.actcode , act.actid , act.actnom FROM ACTEUR ACT, DOSACTEUR DAC, DOSSIER DOS where DOS.DOSID = DAC.DOSID and DAC.DACDTFIN is null AND ACT.ACTID = DAC.ACTID AND DAC.ROLCODE = 'CLIENT' order by 1";
            //cmd.CommandText = 
            OracleCommand cmd = new OracleCommand(cmdgeury);

            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Acteur a1 = new Acteur();
                a1.actid = (int)reader["ACTID"];
                a1.IDAC = reader["ACTCODE"].ToString();
                a1.NomC = reader["ACTNOM"].ToString();
                act1.Add(a1);
            }
            return (act1);
        }
    }
}