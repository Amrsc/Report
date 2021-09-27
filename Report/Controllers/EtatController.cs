using Report.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;

namespace Report.Controllers
{
    public class EtatController : Controller
    {
        // GET: Etat
        public ActionResult EtatListe()
        {

            if (Session["Pseudo_user"] != null)
            {
                
            DataTable x = GEtat.ListEtat();
            List<Etat> Elist = new List<Etat>();
            for (int i = 0; i < x.Rows.Count; i++)
            {
                Etat u = new Etat();
                u.EtatId = Convert.ToInt32(x.Rows[i]["EtatId"]);
                u.NomEtat = x.Rows[i]["NomEtat"].ToString();
                u.DesEtat = x.Rows[i]["DesEtat"].ToString();
                u.Form = x.Rows[i]["Form"].ToString();
                u.CatId = Convert.ToInt32(x.Rows[i]["CatId"]);
                Elist.Add(u);
            }
            var model = Elist;
               
                return View(model);
            }
            return RedirectToAction("Login", "Home");
        }
        // GET: User
        public ActionResult DelEtat( int id)
        {
            bool x = false;
            x = GEtat.DelEtat(id);
            if (x)
            {
                TempData["ok"] = "Etat supprimé avec succée";
            }
            else
            {
                TempData["ok"] = "Erreur de suppression de l'Etat";
            }
            return RedirectToAction("EtatListe");
        }
        public ActionResult EtatNew()
        {
            if (Session["Pseudo_user"] != null)
            {
                
                List<Cat> lc = new List<Cat> ();
                DataTable dt = new DataTable();
                dt = GCat.ListCat();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cat c = new Cat();
                    c.CatId = Convert.ToInt32(dt.Rows[i]["CatId"]);
                    c.NomCat = dt.Rows[i]["NomCat"].ToString();
                    lc.Add(c);
                }

                var model = lc;
                return View(model);
            }
            return RedirectToAction("Login", "Home");
        }
        // GET: User
        [HttpPost]
        public ActionResult EtatNew(string NomEtat , string DesEtat , string np1 ,string tp1 , string np2, string tp2, string np3, string tp3, string np4 , string tp4, int Cat , int commun =0 )
        {
            string parametre = "";
            Etat etat = new Etat();
            etat.NomEtat = SecurityElement.Escape(NomEtat);
            etat.DesEtat = SecurityElement.Escape(DesEtat);
            etat.CatId = Cat;
            if(commun == 1)
            {
                etat.Commun = commun;
            }
            if(np1.Length != 0)
            {
                parametre = SecurityElement.Escape(np1) + ',' + tp1;
            }
            if (np2.Length != 0)
            {
                parametre = parametre +',' + SecurityElement.Escape(np2) + ',' + tp2;
            }
            if (np3.Length != 0)
            {
                parametre = parametre + ',' + SecurityElement.Escape(np3) + ',' + tp3;
            }
            if (np4.Length != 0)
            {
                    parametre = parametre + ',' + SecurityElement.Escape(np4) + ',' + tp4;
            }
            etat.Form = parametre;
            bool x = false;
            x = GEtat.NewEtat(etat);
            if (x)
            {
                TempData["ok"] = "Etat enregistrer avec succée";
            }
            else
            {
                TempData["ok"] = "Erreur d'enregistrement de l'Etat";
            }
            return RedirectToAction("EtatNew");
        }
       /* [HttpGet]
        public ActionResult ListQuery()
        {

            if (Session["Pseudo_user"] != null)
            {
                DataTable x = GEtat.ListGetQuery();
                List<Query> qlist = new List<Query>();
                for (int i = 0; i < x.Rows.Count; i++)
                {
                    Query q = new Query();
                    q.id = Convert.ToInt32(x.Rows[i]["id"]);
                    q.NomQuery = x.Rows[i]["NomQuery"].ToString();
                    q.query = x.Rows[i]["Query"].ToString();
                    qlist.Add(q);
                }
                var model = qlist;
                return View(model);
            }
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult DelQuery(int id)
        {
            bool x = false;
            x = GEtat.delQuery(id);
            if (x)
            {
                TempData["ok"] = "Requete supprimé avec succée";
            }
            else
            {
                TempData["ok"] = "Erreur de supprission de la requete";
            }
            return RedirectToAction("ListQuery");
        }
        [HttpPost]
        public ActionResult UpdateQuery(int id , string q)
        {
            bool x = false;
            x = GEtat.UpdateQuery(q, id);
            if (x)
            {
                TempData["ok"] = "Requete mise a jour avec succée";
            }
            else
            {
                TempData["ok"] = "Erreur de mise ajour de la requete";
            }
            return RedirectToAction("ListQuery");
        }
       */
    }
}