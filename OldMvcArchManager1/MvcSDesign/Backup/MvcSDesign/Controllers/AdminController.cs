using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSDesign.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult ProjectManagement()
        {

            return View();
        }
        public ActionResult Operation()
        {

            return View();
        }

        	
    }
}
