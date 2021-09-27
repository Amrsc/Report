using Report.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Report.Controllers
{
    public class CatController : Controller
    {
        // GET: Cat
        public ActionResult CategoriNew( string NomCat , int p)
        {
            bool x = false;
            Cat c = new Cat();
            c.NomCat = NomCat;
            c.idProf = p;
            x = GCat.NewCat(c);
            if (x)
            {
                TempData["ok"] = "Catégorie créer avec succès !! ";
            }
            else
            {
                TempData["eok"] = "Erreure de création de la Catégorie !! ";
            }
            return RedirectToAction("CategoriListe");
        }
        // GET: User
        public ActionResult CategoriListe()
        {
            if (Session["Pseudo_user"] != null)
            {
            DataTable x = GCat.ListCat();
            List<Cat> Clist = new List<Cat>();
            for (int i = 0; i < x.Rows.Count; i++)
            {
                Cat c = new Cat();
                c.CatId = Convert.ToInt32(x.Rows[i]["CatId"]);
                c.NomCat = x.Rows[i]["NomCat"].ToString();
                c.prof = x.Rows[i]["profil"].ToString();
                Clist.Add(c);
            }
                var model = Clist;


                DataTable y = GPrivilege.ListProfil();
                List<profile> prlist = new List<profile>();
                for (int i = 0; i < y.Rows.Count; i++)
                {
                    profile p = new profile();
                    p.idProf = Convert.ToInt32(y.Rows[i]["idProf"]);
                    p.profil = y.Rows[i]["profil"].ToString();
                    prlist.Add(p);
                }

                TempData["lprof"] = prlist;

                return View(model);
            }
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Delete(int CatId)
        {
            bool x = false;
            if(CatId == 1013)
            {
                TempData["eok"] = "Impossible de supprimer cette Catégorie !!";
            }
            else
            {
                x = GCat.DelCat(CatId);
                if (x)
                {
                    TempData["ok"] = "Catégorie supprimé avec succès !!";
                }
                else
                {
                    TempData["eok"] = "Erreur de suppression de cette Catégorie !!";
                }
            }
            
            return RedirectToAction("CategoriListe");
        }
    }
}