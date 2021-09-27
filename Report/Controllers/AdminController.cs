using Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Report.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Admin()
        {
            if (Session["Pseudo_user"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult Login(string login, String password)
        {
            if (Session["Pseudo_user"] != null)
            {
                return RedirectToAction("Login");
            }
            Utilisateur users = new Utilisateur();
            users.Pseudo = login;
            users.Pwd = password;
            if (GUser.verifLogin(users))
            {
                Session["Pseudo_user"] = users.Pseudo.ToString();
                Session["Password_user"] = users.Pwd.ToString();

                if (Session["Pseudo_user"].ToString() == "admin")
                {

                    //RedirectResult("Admin");
                    return RedirectToAction("Admin", "Admin", Session["Pseudo_user"]);
                }
                else
                {

                    return RedirectToAction("Index", "Home" , new { pseudo  = Session["Pseudo_user"].ToString() } );

                }

            }
            else
            {
                TempData["erreur"] = "Connexion échoué veuillez verifier vos identifiants !!";
                return RedirectToAction("Login", "Home" , TempData["erreur"]);
            }
        }
        public ActionResult Deconnecter()
        {
            Session.Clear();
            Session.Abandon();
            TempData["erreur"] = "Au revoir :) !!";
            return RedirectToAction("Login", "Home", TempData["erreur"]);
        }
    }
}