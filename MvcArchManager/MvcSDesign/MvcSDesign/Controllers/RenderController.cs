using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSDesign.Models;
using MvcSDesign.Repository;
using MvcSDesign.EF;
using System.Web.Hosting;
using System.IO;

namespace MvcSDesign.Controllers
{
    public class RenderController : Controller
    {
        //
        // GET: /Render/

        IUser _IUser;
        // clsTechnicalDB renderDb = new clsTechnicalDB();

        public RenderController()
        {
            _IUser = new UserRepository(new ArchManagerDBEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult operation()
        {
            List<TaskListModel> ta = new List<TaskListModel>();
            try
            {
                // DataTable dt = new DataTable();
                string regID = "";
                try
                {
                    regID = Session["staffID"].ToString();
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Login", "Render");
                }
                ViewBag.uploadmessage = "";
                ta = _IUser.GetTaskAssign(int.Parse(regID));

            }
            catch (Exception ex) { }
            ViewBag.Message = "";
            if (ta.Count == 0)
            {
                ViewBag.Message = "There is not pending task available for you";
            }
            return View(ta);

        }

        [HttpPost]
        public ActionResult operation(string pmid,  string option, string taskId, List<HttpPostedFileBase> fileUpload)
        {
            string staffID = "0";
            try
            {
                staffID = Session["staffID"].ToString();
            }
            catch (Exception ex) { }
            string ch;
            try
            {

                if (fileUpload.Count == 0)
                {
                    ViewBag.uploadmessage = "Please select file for upload";
                    return RedirectToAction("operation");
                }

                ch = _IUser.UploadDesignerTask(int.Parse(pmid), int.Parse(taskId), fileUpload);
                if (ch == "Y")
                    TempData["message"] = "success";
                else
                {
                    ViewBag.message = ch;
                    return RedirectToAction("operation");
                }
            }
            catch (Exception ex) { ViewBag.message = ex.Message; }

            return RedirectToAction("operation");
        }


        public string ChangeCredential(string pwd)
        {
            int staffID = 0;

            try
            {
                staffID = int.Parse(Session["staffID"].ToString());
            }
            catch (Exception ex)
            {
            }

            return _IUser.ChangeCredential(staffID, pwd);
        }

        public string ChangeProfilePhoto(string desc)
        {
            int regID = 0;
            string profilename = "";
            try
            {
                regID = int.Parse(Session["regID"].ToString());
            }
            catch (Exception ex)
            {

            }

            HttpFileCollectionBase files = Request.Files;
            string username = Session["username"].ToString();
            for (int i = 0; i < files.Count; i++)
            {
                profilename = Path.GetFileName(Request.Files[i].FileName);
                HttpPostedFileBase file = files[i];
                string pth = HostingEnvironment.MapPath("~//profile//");
                profilename = username.Replace(" ", "") + Path.GetExtension(profilename);
                pth += profilename;
                file.SaveAs(pth);
            }
            //username = renderDb.ChangeUserProfile(regID, desc.Trim(), profilename);
            //Session["description"] = desc;

            return username;
        }
       
        public FileResult pdfDownload(string projectID, string location)
        {
            string pdfpath = "";// renderDb.DownloadPRF(projectID, location);
            string filename = System.IO.Path.GetFileName(pdfpath);
            string contentType = "application/pdf";

            return File(pdfpath, contentType, filename);
        }
        public JsonResult fillMonthlyPerformance()
        {
            int regID = 0;//
            try
            {
                regID = int.Parse(Session["staffID"].ToString());
            }
            catch (Exception ex) { }

            return Json(_IUser.fillGraphMonthlyPerformance(regID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getWeeklyPerformance()
        {
            int regID = 0;//
            try
            {
                regID = int.Parse(Session["staffID"].ToString());
            }
            catch (Exception ex) { }
            return Json(_IUser.getWeeklyPerformance(regID), JsonRequestBehavior.AllowGet);

       }
        public JsonResult getProjectCategoryPerformance()
        {
            int regID = 0;//
            string name = "";
            try
            {
                regID = int.Parse(Session["regID"].ToString());
                name = Session["name"].ToString();
            }
            catch (Exception ex) { }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public string CheckLoginImage(string imgname)
        {
            //Common login image path
            string pth = HostingEnvironment.MapPath("~//Images//loginImage//" + imgname);
            if (!System.IO.File.Exists(pth))
            {
                try
                {
                    string tmpPath = HostingEnvironment.MapPath("~//Images//loginImage");
                    string[] files = Directory.GetFiles(tmpPath);
                    foreach (string fl in files)
                    {
                        System.IO.File.Delete(fl);
                    }
                }
                catch (Exception ex) { }

               
            }

            return "Y";
        }




    }
}