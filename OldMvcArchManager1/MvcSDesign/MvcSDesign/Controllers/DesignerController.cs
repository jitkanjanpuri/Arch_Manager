using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcSDesign.EF;
using MvcSDesign.Models;
using MvcSDesign.Repository;

namespace MvcSDesign.Controllers
{
    public class DesignerController : Controller
    {
        //
        // GET: /Designer/
        IAdmin _IAdn;

        public DesignerController()
        {
            _IAdn = new AdminRepository(new ArchManagerDBEntities());
        }
         
        public ActionResult Index()
        {
            try
            {
                //string ch = Session["user"].ToString();
                
            }
            catch (Exception ex)
            {
                //FormsAuthentication.SignOut();
                //FormsAuthentication.SetAuthCookie("", true);

                //return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public JsonResult DesignerWorkingList()
        {
            
            int reg = int.Parse(Session["staffID"].ToString());
            var lst = _IAdn.getDesignerWorkingList(reg);
            return Json(lst, JsonRequestBehavior.AllowGet);
             

        }
        public ActionResult DesignerReport()
        {

            return View();
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
