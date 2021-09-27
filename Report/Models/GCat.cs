using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Report.Models
{
    public class GCat
    {
        public static bool NewCat(Cat c)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof;if not exists(select * from [categorie] where NomCat='" + c.NomCat + "') INSERT INTO [categorie] VALUES ('" + c.NomCat + "','" + c.idProf + "'); ", con);
            ins.ExecuteNonQuery();
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            verif = true;
            return verif;
        }

        public static DataTable ListCat()
        {
            DataTable dt = new DataTable();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("USE PortailSof select * from categorie join prof on categorie.idProf = prof.idProf order by prof.profil ;", con);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public static bool DelCat(int id)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ctrl = new SqlCommand("use Portailsof ; select * from etat where CatId ='" + id + "' ;",con);
            ctrl.ExecuteNonQuery();
            SqlDataReader reader = ctrl.ExecuteReader();
            
                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                con.Close();
                    return verif;
                
                }
                else
                {
                SqlConnection conn = ConnexionBDD.GetDBConnection();
                conn.Open();
                SqlCommand ins = new SqlCommand("USE PortailSof;delete from [categorie] where CatId='" + id + "'; ", conn);
                    ins.ExecuteNonQuery();
                    SqlCommand ins2 = new SqlCommand("USE PortailSof;delete from [privilege] where CatId='" + id + "'; ", conn);
                    ins2.ExecuteNonQuery();
                    Console.WriteLine("Query successfully executed !!!");
                    conn.Close();
                    verif = true;
                    return verif;
                }   
        }
        public static Cat getCatId(string NomCat)
        {
            Cat c = new Cat();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT  * FROM [PortailSof].[dbo].[categorie] where NomCat ='" + NomCat + "';", con);
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        c.CatId= (int)reader["CatId"];
                        c.NomCat = (string)reader["NomCat"];
                    }
                }
            }
            con.Close();
            return c;
        }
    }
}