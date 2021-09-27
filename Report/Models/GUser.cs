using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Report.Models
{
    public class GUser
    {
        public static bool NewUser (Utilisateur u)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof;INSERT INTO [utilisateur] VALUES ('" + u.Pseudo + "','" + u.Pwd + "','" + u.prof + "'); ", con);
            ins.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("SELECT  * FROM [PortailSof].[dbo].[utilisateur] where Pseudo ='" + u.Pseudo + "';", con);
            cmd.ExecuteNonQuery();
            verif = true;
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            return verif;
        }
        public static bool DelUser(int id)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof;delete from [utilisateur] where UserId='" + id + "'; ", con);
            ins.ExecuteNonQuery();
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            verif = true;
            return verif;
        }
        public static DataTable ListUser()
        {
            DataTable dt = new DataTable();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT  * FROM [PortailSof].[dbo].[utilisateur] ;", con);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public static Utilisateur getUser( int id)
        {
            Utilisateur u = new Utilisateur();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT  * FROM [PortailSof].[dbo].[utilisateur] where UserId ='"+id+"';", con);
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        u.id = (int)reader["UserId"];
                        u.Pseudo = (string)reader["Pseudo"];
                        u.Pwd = (string)reader["Pwd"];
                    }
                }
            }
            con.Close();
            return u;
        }
        public static bool verifLogin(Utilisateur login)
        {
            bool ok = false;
            Utilisateur u = new Utilisateur();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT  * FROM [PortailSof].[dbo].[utilisateur] where Pseudo ='" + login.Pseudo + "';", con);
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        u.id = (int)reader["UserId"];
                        u.Pseudo = (string)reader["Pseudo"];
                        u.Pwd = (string)reader["Pwd"];
                    }
                }
            }
            if(login.Pseudo == u.Pseudo && login.Pwd == u.Pwd)
            {
                ok = true;
            }
            con.Close();
            return ok;
        }
        public static bool Priv(int idU , int idP)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
                SqlCommand ins = new SqlCommand("use PortailSof update utilisateur set idProf = '" + idP + "' where UserId ='" + idU + "'; ", con);
                ins.ExecuteNonQuery();
            verif = true;
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            return verif;
        }
        public static int getProfil (int id)
        {
            int prof = 0;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT  * FROM [PortailSof].[dbo].[utilisateur] where UserId ='" + id + "';", con);
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prof = (int)reader["idProf"];
                    }
                }
            }
            return prof;
        }
        public static int getUserId(string p)
        {
            int u = 0;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT  * FROM [PortailSof].[dbo].[utilisateur] where Pseudo ='" + p + "';", con);
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        u = (int)reader["UserId"];
                    }
                }
            }
            con.Close();
            return u;
        }
    }
}