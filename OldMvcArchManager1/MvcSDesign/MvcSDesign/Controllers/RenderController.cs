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

         
        //public string userVarification(string uname, string pwd)
        //{
        //    int regID = 0;
        //    string name = "", usertype = "", profileImage = "", designation = "", description = "";
        //    string ch = _render.userVarifiction(uname, pwd, ref regID, ref name, ref usertype, ref profileImage, ref designation, ref description);
        //    if (ch == "")
        //    {
        //        Session["username"] = uname;
        //        Session["regID"] = regID;
        //        Session["name"] = name;
        //        Session["profileImage"] = profileImage;
        //        Session["usertype"] = usertype;
        //        Session["designation"] = designation;
        //        Session["description"] = description;
        //    }

        //    return ch;
        //}
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
                 
                ch =  _IUser.UploadDesignerTask(int.Parse(pmid), int.Parse(taskId), fileUpload);
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
        //public JsonResult SalaryGenerate(string mnt, string yr)
        //{

        //    GeneratePDF objPdf = new GeneratePDF();
        //    DataTable dt = new DataTable();
        //    List<clsSalary> sl = new List<clsSalary>();
        //    try
        //    {
        //        string rid = Session["regID"].ToString();
        //        dt = objPdf.CratePDF(mnt, yr, rid);

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            sl.Add(
        //                       new clsSalary
        //                       {
        //                           sno = dr["sno"].ToString(),
        //                           fname = dr["fname"].ToString(),
        //                           fpath = dr["fpath"].ToString()
        //                       }
        //                   );
        //        }
        //    }
        //    catch (Exception ex) { }
        //    return Json(sl, JsonRequestBehavior.AllowGet);

        //}

        public FileResult pdfDownload(string projectID, string location)
        {
            string pdfpath = "";// renderDb.DownloadPRF(projectID, location);
            string filename = System.IO.Path.GetFileName(pdfpath);
            string contentType = "application/pdf";

            return File(pdfpath, contentType, filename);
        }
        //public FileResult Download(string taskID, string projectID, string option, string location, string filename, string type)
        //{
        //    string fpath = "";
        //    string contentType = "application/octet-stream";
        //    location = location.Replace("http://absit.co.in:89/", "");
        //    location = location.Replace("http://173.248.151.174:89/", "");
        //    location = "C://inetpub/wwwroot/DesignLab/" + location;


        //    if (filename == "-") return File("", "", "");
        //    if (type == "plan")
        //    {
        //        fpath = location;
        //    }
        //    else
        //    {
        //        string ch, ftmp;
        //        ch = Path.GetExtension(location).ToLower();
        //        ftmp = Path.GetFileName(location);

        //        filename = filename.Trim();
        //        if ((ch == ".dwg") || (ch == ".zip") || (ch == ".rar"))
        //        {
        //            ch = location.Replace("/Plan/" + ftmp, "");
        //        }
        //        else
        //        {
        //            int l = location.IndexOf("/Plan/");
        //            ch = location.Substring(0, l);
        //        }
        //        if (type == "Elevation")
        //        {
        //            fpath = ch + "/" + type + "/" + filename;
        //            if (!System.IO.File.Exists(fpath)) // check if file  is not available then search in elevation folder
        //            {
        //                fpath = ch + "/" + type + "/I/" + filename;
        //                if (!System.IO.File.Exists(fpath))
        //                {
        //                    fpath = ch + "/" + type + "/II/" + filename;
        //                    if (!System.IO.File.Exists(fpath))
        //                    {
        //                        fpath = ch + "/Concept Image/" + filename;
        //                    }
        //                }

        //            }
        //        }
        //        else if (type == "Supporting Image")
        //        {
        //            fpath = ch + "/" + type + "/" + filename;// check if file  is not support image foler then search in concep image folder
        //            if (!System.IO.File.Exists(fpath))
        //            {
        //                fpath = ch + "/Concept Image/" + filename;
        //            }
        //        }

        //        else if (type == "Draft")
        //        {
        //            fpath = ch + "/Draft View/" + filename;// check if file  is not support image foler then search in concep image folder
        //            if (!System.IO.File.Exists(fpath))
        //            {
        //                fpath = ch + "/Concept Image/" + filename;
        //            }
        //        }
        //        else
        //        {
        //            fpath = ch + "/" + type + "/" + filename.Trim();
        //        }


        //    }
        //    _render.TaskAssignStatusChange(int.Parse(taskID));
        //    return File(fpath, contentType, filename);
        //}

        //public JsonResult getThumbnail(string projectID)
        //{
        //    var ps = new List<SelectListItem>();
        //    string uid = renderDb.getUID(projectID);
        //    string str = "C:/inetpub/wwwroot/DesignLab/ProjectThumbnail/UID_" + uid;
        //    string physicalPath = HostingEnvironment.MapPath("~/Images/uid/");
        //    string urlPath = "/Images/uid/";
        //    try
        //    {
        //        string[] files = Directory.GetFiles(str);//.Where(;
        //        string fname, tmp;

        //        if (files.Length > 20)
        //        {
        //            Array.Reverse(files);
        //        }

        //        foreach (var item in files.Take(20))
        //        {
        //            fname = Path.GetFileName(item);
        //            try
        //            {
        //                tmp = physicalPath + fname;
        //                System.IO.File.Copy(item, tmp);
        //            }
        //            catch (Exception ex) { }
        //            tmp = urlPath + fname;
        //            ps.Add(
        //                 new SelectListItem
        //                 {
        //                     Text = tmp
        //                 }
        //                );
        //        }
        //    }
        //    catch (Exception ex) { }

        //    if (ps.Count == 0)
        //    {
        //        urlPath = "/Images/noimage.jpg";
        //        ps.Add(
        //                new SelectListItem
        //                {
        //                    Text = urlPath
        //                }
        //               );
        //    }
        //    return Json(ps, JsonRequestBehavior.AllowGet);
        //}


        //public JsonResult fillGraph_Elevation()
        //{
        //    int regID = 0;//
        //    try
        //    {
        //        regID = int.Parse(Session["regID"].ToString());
        //    }
        //    catch (Exception ex) { }
        //    return Json(_IUser.fillGraph_Elevation(regID), JsonRequestBehavior.AllowGet);
        //}

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
           // return Json(renderDb.getProjectCategoryPerformance(regID, name), JsonRequestBehavior.AllowGet);
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

                string tmp = @"C:\inetpub\wwwroot\DesignLab\loginImage\" + imgname;
                try
                {
                    System.IO.File.Copy(tmp, pth);
                }
                catch (Exception ex) { return ex.Message; };
            }

            return "Y";
        }




    }
}