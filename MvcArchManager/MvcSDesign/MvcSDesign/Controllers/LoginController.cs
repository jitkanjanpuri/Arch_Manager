using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Security;
using MvcSDesign.EF;
using MvcSDesign.Models;
using MvcSDesign.Repository;

 

namespace MvcSDesign.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        
        IAdmin _IAdn;
        public LoginController()
        {
            _IAdn = new AdminRepository(new ArchManagerDBEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(logincls lgn)
        {
            try
            {
                if (Session["user"].ToString() == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {

                    return RedirectToAction("operation", "Render");
                }

            }
            catch (Exception ex) { }
 

            return View();
        }

        public string userVarification(logincls lgn)
        {
            
            StaffModel st = new StaffModel();
            st = _IAdn.getLogin(lgn);

            if (st != null)
            {
                Session["username"] = st.name;
                Session["staffID"] = st.staffID;
                if (st.rolltype == "Admin")
                {
                    Session["IsAdmin"] = true;
                    Session["user"] = "Admin";
                    FormsAuthentication.SetAuthCookie(lgn.username, false);
                }
                else if (st.rolltype == "User")
                {
                    Session["IsUser"] = true;
                    Session["user"] = "User";
                    FormsAuthentication.SetAuthCookie(lgn.username, false);
                }
                return "Sucess";

            }

            return "";
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["user"] = "";
            Session["username"] = "";
            return RedirectToAction("Index", "Login");
        }

    }
}
