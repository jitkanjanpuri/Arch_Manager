using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MvcSDesign.EF;
using MvcSDesign.Models;
using MvcSDesign.Repository;

namespace MvcSDesign.Controllers
{
    public class ArchGallaryController : Controller
    {
        //
        // GET: /ArchGallary/

        registration regObj = new registration();
        IAdmin IAdn;
        public ArchGallaryController()
        {
            //IAdn = new AdminRepository(new SDesignEntities());
        }

        public ActionResult ArchLogin()
        {

            return View();
        }

        public ActionResult Index()
        {
            ViewBag.selectedItem = "Archect";
            regObj.professionlist = getProfessionList();
            return View(regObj);
        }

        public IEnumerable<SelectListItem> getProfessionList()
        {
            var lst = new List<SelectListItem>();

            lst.Add(new SelectListItem
            {
                Text = "Architect",
                Value = "Architect"
            });

            lst.Add(new SelectListItem
            {
                Text = "Designer",
                Value = "Designer"
            });


            lst.Add(new SelectListItem
            {
                Text = "Engineer",
                Value = "Engineer"
            });

            lst.Add(new SelectListItem
            {
                Text = "PR Agency",
                Value = "PR Agency"
            });
            



            return lst;
        }

        [HttpPost]
        public ActionResult Index(registration tmpObj)
        {

            string ch = "";
            if (ModelState.IsValid)
            {
                ch = "";// IAdn.SaveRegistration(tmpObj);

                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("Err", "Error occurred in save operation");

            }

            ViewBag.selectedItem = "Archect";
            regObj.professionlist = getProfessionList();
            return View(regObj);

            //return View();
        }

        public ActionResult projectUpload()
        {

            return View();
        }



       

    }
}
