using Report.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Report.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index( string ?pseudo )
        {
            
            if (Session["Pseudo_user"] != null)
            {
                int x = GUser.getUserId(pseudo);
                IList<Etat> l1 = new List<Etat>();
                DataTable dt = new DataTable();
                dt = GPrivilege.ListEtatPrivilege(x);
                int actts = 0;
                int doss = 0;
                int dosscbmim = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Etat etat = new Etat();
                    etat.EtatId = Convert.ToInt32(dt.Rows[i]["EtatId"]);
                    etat.CatId = Convert.ToInt32(dt.Rows[i]["CatId"]);
                    etat.NomEtat = dt.Rows[i]["NomEtat"].ToString();
                    etat.DesEtat = dt.Rows[i]["DesEtat"].ToString();
                    etat.Form = dt.Rows[i]["Form"].ToString();
                    if (etat.Form.Contains("actor"))
                    {
                        actts = 1;
                    }if (etat.Form.Contains("dos"))
                    {
                        doss = 1;
                    }if (etat.Form.Contains("doscbmim"))
                    {
                        dosscbmim = 1;
                    }
                    
                        l1.Add(etat);
                }
                if (actts == 1)
                {
                    List<Acteur> acts = GEtat.act();
                    TempData["acts"] = acts;
                }
                if (doss == 1)
                {
                    List<Dossier> acts = GEtat.Dos();
                    TempData["dos"] = acts;
                }
                if (dosscbmim == 1)
                {
                    List<Dossier> acts = GEtat.DosCBMIM();
                    TempData["doscbmim"] = acts;
                }
                
                return View(l1);
            }
            return RedirectToAction("Login", "Home");
        }

    }
}