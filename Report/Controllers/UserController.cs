using Report.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Report.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserList()
        {
            if (Session["Pseudo_user"] != null)
            {

                DataTable x = GUser.ListUser();
                List<Utilisateur> Ulist = new List<Utilisateur>();
                for (int i = 0; i < x.Rows.Count; i++)
                {
                    Utilisateur u = new Utilisateur();
                    u.id = Convert.ToInt32(x.Rows[i]["UserId"]);
                    u.Pseudo = x.Rows[i]["Pseudo"].ToString();
                    u.Pwd = x.Rows[i]["pwd"].ToString();
                    if (u.Pseudo != "admin")
                    {
                        Ulist.Add(u);
                    }
                }
                var model = Ulist;
                return View(model);
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult UserNew(string pseudo, string pwd, int cat)
        {
            DataTable y = GPrivilege.ListProfil();
            List<profile> Clist = new List<profile>();
            for (int i = 0; i < y.Rows.Count; i++)
            {
                profile c = new profile();
                c.idProf = Convert.ToInt32(y.Rows[i]["idProf"]);
                c.profil = y.Rows[i]["profil"].ToString();
                Clist.Add(c);
            }
            bool x = false;
            Utilisateur u = new Utilisateur();
            u.Pseudo = pseudo;
            u.Pwd = pwd;
            u.prof = cat;
            x = GUser.NewUser(u);
            if (x)
            {
                TempData["pok"] = "Utilisateur crée avec succès !!";
                return RedirectToAction("UserList", TempData["pok"]);
            }
            else
            {
                TempData["ok"] = "Erreur d'enregistrement de l'utilisateur";
            }
            var model = Clist;
            return View(model);
        }
        [HttpGet]
        public ActionResult UserNew()
        {
            if (Session["Pseudo_user"] != null)
            {
                DataTable y = GPrivilege.ListProfil();
                List<profile> Clist = new List<profile>();
                for (int i = 0; i < y.Rows.Count; i++)
                {
                    profile c = new profile();
                    c.idProf = Convert.ToInt32(y.Rows[i]["idProf"]);
                    c.profil = y.Rows[i]["profil"].ToString();
                    Clist.Add(c);
                }

                var model = Clist;
                return View(model);
            }
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult UserProf(int id)
        {
            if (Session["Pseudo_user"] != null)
            {
                DataTable y = GPrivilege.ListProfil();
                List<profile> Clist = new List<profile>();
                for (int i = 0; i < y.Rows.Count; i++)
                {
                    profile c = new profile();
                    c.idProf = Convert.ToInt32(y.Rows[i]["idProf"]);
                    c.profil = y.Rows[i]["profil"].ToString();
                    Clist.Add(c);
                }
                ViewData["cats"] = Clist;
                Utilisateur u = new Utilisateur();
                u = GUser.getUser(id);
                var model = u;
                return View(model);
            }
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult profil(int idU, int profil)
        {
            bool x = false;
            x = GUser.Priv(idU, profil);
            if (x)
            {
                TempData["pok"] = "Profile attribuer !!!!";
                return RedirectToAction("UserList", TempData["pok"]);
            }
            else
            {
                TempData["eror"] = "erreur d'enregistrement !!!!";
                return RedirectToAction("UserPriv", new { id = idU });
            }

        }

        [HttpGet]
        public ActionResult UserDel(int id)
        {
            bool x = false;
            x = GUser.DelUser(id);
            if (x)
            {
                TempData["pok"] = "Utilisateur supprimer avec succès";
                return RedirectToAction("UserList", TempData["pok"]);
            }
            else
            {
                TempData["ok"] = "Erreure de suppression de l'utilisateur";
                return RedirectToAction("UserList", TempData["ok"]);
            }

        }

        [HttpPost]
        public ActionResult AddProfile(string nom)
        {
            GPrivilege GP = new GPrivilege();
            DataTable y = GPrivilege.ListProfil();
            int i = 0;
            int spy = 0;
            while (i < y.Rows.Count)
            {
                GP.AddProfile(nom);

                return RedirectToAction("UserNew");
            }
        
                return View();

    }

    [HttpPost]
    public ActionResult delProfile(string nom)
    {
        if (nom == "SUPER")
        {
            TempData["msg"] = "impossible de supprimer le profil Super";
            return RedirectToAction("UserNew");
        }
        else
        {
            GPrivilege GP = new GPrivilege();
            GP.delProfile(nom);
            return RedirectToAction("UserNew");
        }

    }

    [HttpGet]
    public ActionResult UserPrivilege(int id)
    {
        int pid;
        List<int> lcs = new List<int>();
        Utilisateur s = new Utilisateur();
        string nom;
        s = GUser.getUser(id);
        pid = GUser.getProfil(id);
        DataTable h = GPrivilege.getCats(pid);
        for (int i = 0; i < h.Rows.Count; i++)
        {
            Cat w = new Cat();
            w.CatId = Convert.ToInt32(h.Rows[i]["CatId"]);
            lcs.Add(w.CatId);
        }

        nom = s.Pseudo;
        DataTable x = GPrivilege.ListEtatPrivilege(id);
        List<privilege> plist = new List<privilege>();
        for (int i = 0; i < x.Rows.Count; i++)
        {
            privilege p = new privilege();
            p.PrivId = Convert.ToInt32(x.Rows[i]["PrivId"]);
            p.EtatId = Convert.ToInt32(x.Rows[i]["EtatId"]);
            p.UserId = Convert.ToInt32(x.Rows[i]["UserId"]);
            p.CatId = Convert.ToInt32(x.Rows[i]["CatId"]);
            p.NomEtat = x.Rows[i]["NomEtat"].ToString();
            plist.Add(p);
        }
        DataTable y = GPrivilege.ListEtatNPrivilege(id, lcs);

        List<privilege> pnlist = new List<privilege>();
        for (int i = 0; i < y.Rows.Count; i++)
        {
            privilege p = new privilege();
            p.EtatId = Convert.ToInt32(y.Rows[i]["EtatId"]);
            p.CatId = Convert.ToInt32(y.Rows[i]["CatId"]);
            p.NomEtat = y.Rows[i]["NomEtat"].ToString();
            p.DesEtat = y.Rows[i]["DesEtat"].ToString();
            pnlist.Add(p);
        }
        ViewData["pnlist"] = pnlist;
        TempData["user"] = nom;
        TempData["userid"] = id;
        TempData["catid"] = pid;
        var model = plist;
        return View(model);
    }
    [HttpGet]
    public ActionResult DelPrivilege(int idp, int uid)
    {
        bool x = false;
        x = GPrivilege.DelPrivilege(idp);
        if (x)
        {
            TempData["ok"] = "Prévilège supprimer avec succès";
        }
        else
        {
            TempData["ok"] = "Erreure de suppression du prévilège";
        }
        return RedirectToAction("UserPrivilege", new { id = uid });
    }

    [HttpGet]
    public ActionResult NewPrivilege(int UserId, string Etat, int CatId)
    {
        bool x = false;
        int EtatId = 0;
        if (Etat != "")
        {
            string[] xetet = Etat.Split(',');
            for (int i = 0; i < xetet.Length; i++)
            {
                EtatId = Int32.Parse(xetet[i]);
                x = GPrivilege.NewPrivilege(UserId, EtatId, CatId);
            }
            if (!x)
            {
                TempData["erreur"] = "operation echouer !!!!";
            }
        }
        return RedirectToAction("UserPrivilege", new { id = UserId });
    }

}
}