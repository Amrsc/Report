using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Report.Models
{
    public class GPrivilege
    {
        public static bool NewPrivilege(int UserId, int EtatId, int CatId)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof IF NOT EXISTS (SELECT * FROM privilege WHERE UserId = '" + UserId + "' AND CatId = '" + CatId + "' AND EtatId = '" + EtatId + "')INSERT INTO privilege VALUES('" + UserId + "','" + EtatId + "','" + CatId + "') ;", con);
            ins.ExecuteNonQuery();
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            verif = true;
            return verif;
        }

        public static bool DelPrivilege(int id)
        {
            bool verif = false;
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlCommand ins = new SqlCommand("USE PortailSof;delete from [privilege] where PrivId='" + id + "'; ", con);
            ins.ExecuteNonQuery();
            Console.WriteLine("Query successfully executed !!!");
            con.Close();
            verif = true;
            return verif;
        }
        public static DataTable ListEtatPrivilege(int id)
        {
            DataTable dt = new DataTable();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("use PortailSof select * from etat right join privilege on etat.EtatId = privilege.EtatId where privilege.UserId = '" + id + "'", con);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public static DataTable ListEtatNPrivilege(int Uid, List<int> Cid)
        {
            string idauth = "";

            DataTable dt = new DataTable();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            if(Cid.Contains(1013))
            {
                    SqlDataAdapter da = new SqlDataAdapter("use PortailSof select distinct e.EtatId , e.NomEtat , e.DesEtat , e.form , e.CatId from etat e except(select distinct e.EtatId, e.NomEtat, e.DesEtat, e.form, e.CatId from etat e right join privilege on e.EtatId = privilege.EtatId where privilege.UserId = "+Uid+" )", con);
                    da.Fill(dt);

            }
            else
            {
                foreach (int i in Cid)
                {
                    SqlDataAdapter da = new SqlDataAdapter("use PortailSof select * from etat where CatId in ('" + i + "') except(select distinct e.EtatId, e.NomEtat, e.DesEtat, e.form, e.CatId , e.Commun from etat e right join privilege on e.EtatId = privilege.EtatId where privilege.UserId = " + Uid + " or e.commun =1 )", con);
                    da.Fill(dt);
                }
                SqlDataAdapter daa = new SqlDataAdapter("use PortailSof select distinct e.EtatId , e.NomEtat , e.DesEtat , e.form , e.CatId from etat e where Commun = 1  except(select distinct e.EtatId, e.NomEtat, e.DesEtat, e.form, e.CatId from etat e right join privilege on e.EtatId = privilege.EtatId where privilege.UserId = " + Uid + " )", con);
                daa.Fill(dt);
            }
            
            con.Close();
            return dt;
        }
        public static DataTable ListProfil()
        {
            DataTable dt = new DataTable();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT  * FROM [PortailSof].[dbo].[prof] order by profil;", con);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public static DataTable getCats(int idp)
        {

            DataTable dt = new DataTable();
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT  * FROM [PortailSof].[dbo].[categorie] where idProf ='" + idp + "' ;", con);
            da.Fill(dt);
            con.Close();

            return dt;
        }
        public void AddProfile(string Nom)
        {
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            string sql = "use PortailSof IF NOT EXISTS( select * from prof where profil ='"+Nom+"') insert into prof  values('" + Nom + "')";
            SqlCommand sc = new SqlCommand(sql, con);
            sc.ExecuteNonQuery();
            sc.Dispose();
            con.Close();
        }
        public void delProfile(string Nom)
        {
            SqlConnection con = ConnexionBDD.GetDBConnection();
            con.Open();
            string sql = "use PortailSof delete from prof where profil ='" + Nom + "'";
            SqlCommand sc = new SqlCommand(sql, con);
            sc.ExecuteNonQuery();
            sc.Dispose();
            con.Close();
        }
    }
}