﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcSDesign.EF;
using MvcSDesign.Models;
using MvcSDesign.Repository;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Globalization;
using System.Web.Hosting;
using System.Drawing;
using System.IO;
 

namespace MvcSDesign.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public static DataTable clientDetailRecord;//= new DataTable();
        IAdmin _IAmn;
        StaffModel obj = new StaffModel();
       

        public AdminController()
        {
            _IAmn = new AdminRepository (new ArchManagerDBEntities());
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

        public ActionResult ProjectManagement()
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
        public ActionResult CompanyProfile()
        {
            //try
            //{
            //     string ch = Session["user"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    FormsAuthentication.SignOut();
            //    FormsAuthentication.SetAuthCookie("", true);

            //    return RedirectToAction("Index", "Login");
            //}
            CompanyModel obj = _IAmn.GetCompanyProfile();
            return View(obj);
        }
        public ActionResult PRF()
        {
            //try
            //{
            //     string ch = Session["user"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    FormsAuthentication.SignOut();
            //    FormsAuthentication.SetAuthCookie("", true);

            //    return RedirectToAction("Index", "Login");
            //}
             
            return View();
        }


        public ActionResult Category()
        {
            //try
            //{
            //     string ch = Session["user"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    FormsAuthentication.SignOut();
            //    FormsAuthentication.SetAuthCookie("", true);

            //    return RedirectToAction("Index", "Login");
            //}

            return View();
        }
        [HttpPost]
        public ActionResult Category(CategoryModel obj)
        {

            if (obj.categoryID == 0) ModelState.Remove("categoryID");
            if (ModelState.IsValid)
            {
                string ch = _IAmn.SaveCategory(obj);
                if (ch == "")
                {
                    ViewBag.message = "Record successfully saved";
                }
                else
                    ViewBag.message = ch;
                return RedirectToAction("Category");
            }
            // var errors = ModelState.Values.SelectMany(v => v.Errors);
            var res = ModelState.Values.SelectMany(v => v.Errors);
            return View();
        }

        public JsonResult GetAllCategory()
        {
            return Json(_IAmn.GetAllCategory(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdminSetting()
        {
            AdminSettingModel obj = new AdminSettingModel();
            obj.projectID = GetStartProjectID();
            ViewBag.message = "";
            return View(obj);
        }
        [HttpPost]
        public ActionResult AdminSetting(AdminSettingModel obj)
        {

            if (ModelState.IsValid)
            {
                string ch = _IAmn.SaveAdminSetting(obj);
                if (ch == "")
                {
                    ViewBag.message = "Record successfully saved";
                }
                else
                    ViewBag.message = ch;

            }
                

            
            AdminSettingModel obj1 = new AdminSettingModel();
            obj1.projectID = GetStartProjectID();
            return View(obj);
        }
       
        long GetStartProjectID()
        {
            return _IAmn.GetStartProjectID();

        }
        public JsonResult GetPRFByPrjectID(string projectID)
        {
            return Json(_IAmn.GetPRFByPrjectID(int.Parse(projectID)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavePRF(PRFModel obj)
        {
            return Json(_IAmn.SavePRF(obj), JsonRequestBehavior.AllowGet);
        }

         
       [HttpPost]
        public ActionResult CompanyProfile(CompanyModel obj, HttpPostedFileBase logofile)
        {
            
            try
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (logofile != null)
                        {
                            string[] deletepth = Directory.GetFiles(HostingEnvironment.MapPath("~//Images//logoimage//"));
                            foreach (string fpth in deletepth)
                            {
                                System.IO.File.Delete(fpth);
                            }
                           
                            string fpath = HostingEnvironment.MapPath("~//Images//logoimage//") +"logo" + Path.GetExtension(logofile.FileName);
                             
                            logofile.SaveAs(fpath);
                        }
                         
                    }
                    catch (Exception ex) { }

                    string str = _IAmn.SaveCompanyProfile(obj);
                    if (str == "")
                    {
                        ModelState.Clear();
                        ViewData["result"] = "Data succesfully saved";

                        return RedirectToAction("CompanyProfile");
                    }

                }
                //var errors = ModelState.Values.SelectMany(v => v.Errors);

            }
            catch (Exception ex)
            {

                
            }
            return RedirectToAction("CompanyProfile");
        }

        public ActionResult RepTechnical(CompanyModel obj, HttpPostedFileBase logofile)
        {
            return View();
        }


        public JsonResult getProjectQuotation()
        {
            var prj = _IAmn.getProjectQuotation();
            return Json(prj, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getQuotation()
        {
            return Json(_IAmn.getQuotation(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveProjectManagement(string clientid, string level, string projectID, string finalAmount)
        {
            string res = "";
            try
            {
               
                    string pth = HostingEnvironment.MapPath("~//ProjectLocation//");
                    string subfolder = "";
 
                    pth = pth + "//client_" + clientid;
                    //client folder check
                    if (!Directory.Exists(pth))
                    {
                        Directory.CreateDirectory(pth);
                    }
                    //project folder create
                    pth = pth + "//proj_" + projectID;
                    Directory.CreateDirectory(pth);

                    subfolder = pth + "//Site Photo";
                    Directory.CreateDirectory(subfolder);

                    subfolder = pth + "//Presentation Drawing";
                    Directory.CreateDirectory(subfolder);

                        string ch = subfolder + "//Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Floor Plans Ground To Terrace";
                        Directory.CreateDirectory(ch);

                    subfolder = pth + "//Structure Drawing";
                    Directory.CreateDirectory(subfolder);

                        ch = subfolder + "//Center Line Plan";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Column Footing Column and Footing Design";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Plinth Beam plan and Design";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//UG Tank Detail Septic Tank Fire Tank Rain Water Tank";
                        Directory.CreateDirectory(ch);

                    //Ground
                    subfolder = pth + "//Ground Floor Drawing";
                    Directory.CreateDirectory(subfolder);

                        ch = subfolder + "//Working Drawing";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Door and Window Schedule";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Lintel Beam";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//StairCase Detail Drawing";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Wall Electrical";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Roof Electrial";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Ground Floor Shuttering";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Ground Floor Beam and Slab Design";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//2D Elevation";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//2D Elevation Electrical";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Plumbing Drainage and Rain Water Drawing";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Toilet Plan and Detail Niche and Other Working Drawing";
                        Directory.CreateDirectory(ch);

                        ch = subfolder + "//Compound Wall";
                        Directory.CreateDirectory(ch);

                //First Floor
                if (level == "Ground") goto Here;
                subfolder = pth + "//First Floor Drawing";
                Directory.CreateDirectory(subfolder);

                    ch = subfolder + "//Working Drawing";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Door and Window Schedule";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Lintel Beam";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//StairCase Detail Drawing";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Wall Electrical";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Roof Electrial";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//First Floor Shuttering";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//First Floor Beam and Slab Design";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//2D Elevation";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//2D Elevation Electrical";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Plumbing Drainage and Rain Water";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Toilet Plan and Detail Niche and Other Working Drawing";
                    Directory.CreateDirectory(ch);
                 


                if (level == "G+1") goto Here;
                subfolder = pth + "//Second Floor Drawing";
                Directory.CreateDirectory(subfolder);

                    ch = subfolder + "//Working Drawing";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Door and Window Schedule";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Lintel Beam";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//StairCase Detail Drawing";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Wall Electrical";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Roof Electrial";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//First Floor Shuttering";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//First Floor Beam and Slab Design";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//2D Elevation";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//2D Elevation Electrical";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Plumbing Drainage and Rain Water";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Toilet Plan and Detail Niche and Other Workinge";
                    Directory.CreateDirectory(ch);

                    ch = subfolder + "//Perapet Wall";
                    Directory.CreateDirectory(ch);


                Here:
                   res =  _IAmn.UpdateQuotation(int.Parse(projectID), int.Parse(finalAmount), pth);
               
            }
            catch (Exception ex)
            {
                //FormsAuthentication.SignOut();
                // FormsAuthentication.SetAuthCookie("", true);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }



        public JsonResult QuotationDelete(string projectID)
        {
            string ch = "";
            try
            {
                ch = _IAmn.QuotationDelete(int.Parse(projectID));
            }
            catch (Exception ex) { }
            return Json(ch, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Operation()
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

        public ActionResult CurrentWorking()
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
        public ActionResult SiteVisit()
        {
            SiteVisitModel obj = new SiteVisitModel();
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
            obj.designerList = GetAllStaff();
            return View(obj);
        }

        IEnumerable<SelectListItem> GetAllStaff()
        {
            var obj = new List<SelectListItem>();
            try
            {
                var res = _IAmn.GatAllRegistration();
                foreach (var item in res)
                {
                    obj.Add(
                        new SelectListItem
                        {
                            Text = item.name,
                            Value = item.staffID.ToString()
                        });
                }

            }
            catch (Exception ex) { }

            return obj;


        }



        [HttpPost]
        public ActionResult SiteVisit(SiteVisitModel obj,string name ,string projectID, List<HttpPostedFileBase> sitevisitPhoto)
        {
            FileCompress fc = new FileCompress();
            try
            {
                var res = _IAmn.GetProjectInfo(obj.projectID);
                string fname = "", ext, fpath, filename ="";

                foreach (HttpPostedFileBase item in sitevisitPhoto)
                {
                //    if (sitevisitPhoto != null)
                //{
                    ext = Path.GetExtension(item.FileName);
                    fname = "SitePhoto_" + obj.projectID + "_" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                    fpath = HostingEnvironment.MapPath("~/ProjectLocation/client_" + res.clientID + "/proj_" + projectID + "/Site Photo/"+ fname + ext);
                    //fname = fname + ext;
                    //fpath +=  fname;
                    //sitevisitPhoto.SaveAs(fpath);
                   // string f = sitevisitPhoto.
                    string ch = fc.Save(item, fpath);
                    if (filename == "")
                        filename = fname + ext;
                    else
                        filename = filename + "," + fname+ ext;
                }
                ext =_IAmn.SaveSiteVisit(obj.projectID, int.Parse(name), filename, obj.remark);
            }
            catch (Exception ex) { }
            return RedirectToAction("SiteVisit");
        }

        public JsonResult SearchSiteVisitByNameOrProjectID(string opt, string projectID, string cname, string pname)
        {
            return Json(_IAmn.SearchSiteVisitByNameOrProjectID(opt, projectID, cname, pname), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchByProjectIDOrName(string opt, string projectID, string cname)
        {
            List<tblClient> obj = new List<tblClient>();
            var lst = _IAmn.SearchByProjectIDOrName(opt, projectID, cname);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchCurrentWorking(string dname, string category,   string subcategory)
        {
            return Json(_IAmn.GetCurrentWorking(dname, category, subcategory), JsonRequestBehavior.AllowGet);
        }
        public FileResult DownloadDWG(string filename, string pmID, string projectID)
        {
            string ext = Path.GetExtension(filename);
            string fpath= _IAmn.GetFilePath(int.Parse(pmID),  filename);
            string contentType = "application/octet-stream";
            return File(fpath, contentType, filename);
        }



        public JsonResult TaskSendToClient(string pmID, string pid, string[] filelist, string gmail, string whatsApp)
        {
            string uploadedFileName = "", whatsAppFilepath = "", whatsAppno = "", socialmedia = "";
            
            string ch = _IAmn.SendTaskMailToClien(int.Parse(pmID), int.Parse(pid), filelist, out uploadedFileName, gmail);
            if (ch == "Y")
            {
                _IAmn.SaveStatus("Start Delete Project Management");
                ch = _IAmn.DeleteProjectManagement(int.Parse(pmID), uploadedFileName);
                _IAmn.SaveStatus("Status : " + ch);

            }
            List<SelectListItem> para = new List<SelectListItem>();
            para.Add(new SelectListItem { Text = ch, Value = ch });
            para.Add(new SelectListItem { Text = socialmedia, Value = socialmedia });


            //if ((whatsAppno != "") && ((projectCategory == "Elevation") || (projectCategory == "Revised Elevation") || (projectCategory == "Final View") || (projectCategory == "Draft View") || (projectCategory == "Revised Draft")))
            //{
            //    string[] arr = whatsAppFilepath.Split('#');
            //    for (int i = 0; i < arr.Length; i++)
            //    {
            //        //string url = "http://173.248.151.174:89/WhatsAppMsg/" + arr[i];
            //        //var client = new RestClient("https://panel.rapiwha.com/send_message.php?apikey=0UFVPJN62DTQFVRBUQFN&number=91" + whatsAppno + "&text=" + url + " ");
            //        //var request = new RestRequest(Method.GET);
            //        //IRestResponse response = client.Execute(request);



            //    }

            //    para.Add(new SelectListItem { Text = "WhatsApp", Value = "WhatsApp" });
            //}
            //else
            //{
            //    para.Add(new SelectListItem { Text = "No", Value = "No" });
            //}

            return Json(para, JsonRequestBehavior.AllowGet);
        }



        public JsonResult AddSearchProject_ClientName(string pid)
        {
            return Json(_IAmn.AddSearchProject_ClientName(int.Parse(pid)), JsonRequestBehavior.AllowGet);
        }
        public FileResult DownloadPRF(string projectID, string location)
        {
            string pdfpath = _IAmn.DownloadPRF(projectID, location);
            string filename = System.IO.Path.GetFileName(pdfpath);
            string contentType = "application/pdf";

            return File(pdfpath, contentType, filename);
        }

        public FileResult DownloadSiteVisit(string projectID,  string filename)
        {
            string pdfpath = _IAmn.DownloadSiteVist(int.Parse(projectID), filename);
            filename = System.IO.Path.GetFileName(pdfpath);
            string contentType = "application/pdf";

            return File(pdfpath, contentType, filename);
        }

        public FileResult DownloadUploadFile(string projectID, string uploafFileID,string filename)
        {
            string pdfpath = _IAmn.DownloadUploadFile(int.Parse(projectID), int.Parse(uploafFileID), filename);
            filename = System.IO.Path.GetFileName(pdfpath);
            string contentType = "application/pdf";

            return File(pdfpath, contentType, filename);
        }


        public ActionResult ReportProjectHistory()
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
        public ActionResult ReportSiteVisit()
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

        public ActionResult ReportTechnical()
        {
            try
            {
                //string ch = Session["user"].ToString();
            }
            catch (Exception ex)
            {
            }


            return View();
        }

        public ActionResult Registration()
        {
            StaffModel obj = new StaffModel();
            try
            {
                //string ch = Session["user"].ToString();
                ViewBag.message = "";
            }
            catch (Exception ex)
            {
                //FormsAuthentication.SignOut();
                //FormsAuthentication.SetAuthCookie("", true);

                //return RedirectToAction("Index", "Login");
            }
            if (ViewBag.message !="success")
             {
                 ViewBag.message = "";
             }
            obj.designerList = GetDesignerTypeList();
            obj.rollList = GetRollList();
            return View(obj);
        }

      
        IEnumerable<SelectListItem> GetDesignerTypeList()
        {
            var obj = new List<SelectListItem>();
             
            obj.Add(new SelectListItem
            {
                Text ="Admin",
                Value="Admin"
            });
            obj.Add(new SelectListItem
            {
                Text = "Tech",
                Value = "Tech"
            });

            obj.Add(new SelectListItem
            {
                Text = "BDM",
                Value = "BDM"
            });

            obj.Add(new SelectListItem
            {
                Text = "Sales Executive",
                Value = "Sales Executive"
            });
           
            
            return obj;

        }

        IEnumerable<SelectListItem> GetRollList()
        {
            var obj = new List<SelectListItem>();

            obj.Add(new SelectListItem
            {
                Text = "Admin",
                Value = "Admin"
            });
            obj.Add(new SelectListItem
            {
                Text = "User",
                Value = "User"
            });
             
            return obj;

        }


        [HttpPost]
        public ActionResult Registration(StaffModel st)
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
            ViewBag.message = "";
            
            if (ModelState.IsValid)
            {
                string ch = _IAmn.InsertRegistration(st);
                if (ch != "")
                {
                    ViewBag.message = ch;
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.message = "";
                    return RedirectToAction("Registration");
                }

            }
            StaffModel obj = new StaffModel();
            obj.designerList = GetDesignerTypeList();
            obj.rollList = GetRollList();
            return View(obj);
        }

        public JsonResult CheckDesignertNameExists(string name)
        {
            bool UserExists = _IAmn.DesignerNameValidation(name);
            return Json(UserExists, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DesignerEmailValidation(string emailID)
        {
            bool emailExist =  _IAmn.DesignerEmailValidation(emailID);
            return Json(emailExist, JsonRequestBehavior.AllowGet);

        }
        public JsonResult SearchRegistration(string name)
        {
            //List<tblStaff> obj = new List<tblStaff>();
            //if (name == null) name = "";
            //var lst = .OrderBy(x => x.name);

            

            return Json(_IAmn.SearchRegistration(name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GatAllRegistration()
        {
            return Json(_IAmn.GatAllRegistration(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAddProject(string projectID)
        {
            operation obj = new operation();
            obj = _IAmn.SearchAddProject(int.Parse(projectID));
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveNewProject(operation obj)
        {
           return Json(_IAmn.SaveNewProject(obj), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrationUpdate(string staffID, string name, string designation, string address, string city, string phone, string mobile, string emailID, string user, string password)
        {
            obj.staffID = int.Parse(staffID);
            obj.name = name.Trim();
            if (designation == null) designation = "-";
            if (address == null) address = "-";
            if (phone == null) phone = "-";

            obj.designation = designation.Trim();
            obj.address = address.Trim();
            obj.city = city.Trim();
            obj.phone = phone;
            obj.mobile = mobile;
            obj.emailID = emailID;
            obj.username = user;
            obj.password = password;
            string ch = _IAmn.RegistrationUpdate(obj);
            return Json(ch, JsonRequestBehavior.AllowGet);

        }
        public JsonResult RegistrationDelete(string sid)
        {
            string ch = _IAmn.RegistrationDelete(int.Parse(sid));
            return Json(ch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getOperationDesigner()
        {
            return Json(_IAmn.getOperationDesigner(), JsonRequestBehavior.AllowGet);
        }
         
        public JsonResult getProjectAssignList()
        {
            var prj = _IAmn.getProjectAssign();
            return Json(prj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProjectAssigning(operation op)
        {
            string whatsAppno = "", designername = "";

            string ch =  _IAmn.ProjectAssigning(op);
            //if ((ch == "Success") && (whatsAppno != ""))
            //{
                designername = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(designername.ToLower());
                //url = "Dear " + designername + " \n\n";
                //switch (op.category)
                //{
                //    case "Elevation":
                //        url += "🏚 You are requested to provide Elevation of Project ID " + op.projectID + "__" + op.option + ".";
                //        break;
                //    case "Revised Elevation":
                //        url += "🏚 You are requested to provide Revised Elevation of Project ID " + op.projectID + "__" + op.option + ".";
                //        break;
                //    case "3D Model":
                //        url += "🏚 You are requested to provide Model of Project ID " + op.projectID + "__" + op.option + ".";
                //        break;
                //    case "Draft View":
                //        url += "🏡 You are requested to provide Draft view of Project ID " + op.projectID + "__" + op.option + ".";
                //        break;
                //    case "Revised Draft":
                //        url += "🏡 You are requested to provide Revise Draft of Project ID " + op.projectID + "__" + op.option + ".";
                //        break;
                //    case "Final View":
                //        url += "🏡 You are requested to provide Finalize render of Project ID " + op.projectID + "__" + op.option + ".";
                //        break;
                //}
                //url += "\n\n Thanks \n";
                //url += " Team Arch Manager Technical";
                ////var client = new RestClient("https://panel.rapiwha.com/send_message.php?apikey=0UFVPJN62DTQFVRBUQFN&number=91" + whatsAppno + "&text=" + url + " ");
                //var client = new RestClient("https://panel.rapiwha.com/send_message.php?apikey=0UFVPJN62DTQFVRBUQFN&number=" + whatsAppno + "&text=" + url + " ");


                //var request = new RestRequest(Method.GET);
                //IRestResponse response = client.Execute(request);
            //}

            return Json(ch, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProjectRollBack(string pmID)
        {
             
            string ch = _IAmn.ProjectRollBack(int.Parse(pmID));
            //if ((ch == "Y") && (whatsAppno != ""))
            //{
            //    //string url = "We rolled back " + category + " of  project ID " + projectID + " option " + opt;
            //    //var client = new RestClient("https://panel.rapiwha.com/send_message.php?apikey=0UFVPJN62DTQFVRBUQFN&number=" + whatsAppno + "&text=" + url + " ");
            //    //var request = new RestRequest(Method.GET);
            //    //IRestResponse response = client.Execute(request);
            //}
            return Json(ch, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveProjectAssigned(string projectID, string staffID, string projectCategory, string designerAmount)
        {
            obj.name = _IAmn.SaveProjectAssigned(projectID, staffID, projectCategory, designerAmount);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getCurrentWorkingList(string dname)
        //{
        //    var lst = _IAmn.getCurrentWorkingList(dname);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

  
        public JsonResult CurrentWorkingRemarkUpdate(string opID, string remark)
        {
            string ch = _IAmn.CurrentWorkingRemarkUpdate(int.Parse(opID) ,  remark);
            return Json(ch, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompleteCurrentWorking(string opID)
        {

            obj.name =  _IAmn.CompleteCurrentWorking(int.Parse(opID));
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCurrentWorking(string opID)
        {
            obj.name = _IAmn.DeleteCurrentWorking(opID);
            return Json(obj, JsonRequestBehavior.AllowGet);
        } 
        public ActionResult PayDesigner()
        {
            return View();
        }
        public ActionResult Receipt()
        {
            return View();
        }


        [HttpPost]
        public ActionResult PayDesigner(HttpPostedFileBase planfile)
        {
            if(planfile ==null)
            {
                string pth = Server.MapPath("~/ProjectUpload/prj54");
                System.IO.Directory.CreateDirectory(pth);
            }
            else
            {
                string pth = Server.MapPath("~/ProjectUpload/prj54/");
                pth += planfile.FileName;

                planfile.SaveAs(pth);
            }
            return View();
        }
       
        public JsonResult SavePayDesigner(string sid, string amount,  string remark)
        {
            string ch = _IAmn.SavePayDesigner(int.Parse(sid), int.Parse (amount), remark);
            return Json(ch, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveAmountReceive(string clientID, string projectID, string amount, string remark, string gmail)
        {
            return Json(_IAmn.SaveAmountReceive(int.Parse(clientID), int.Parse(projectID), amount, remark, gmail), JsonRequestBehavior.AllowGet);  
        }
        
        public JsonResult getDesignerProjectAmount(string designerID)
        {
            var lst = _IAmn.getDesignerProjectAmount(int.Parse(designerID));
            return Json(lst, JsonRequestBehavior.AllowGet);
        } 
        public JsonResult DesignerAmountCancel(string operationID)
        {
            obj.name = _IAmn.DesignerAmountCancel(int.Parse(operationID));
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PromoMail()
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

        public ActionResult CreateGmailAccount()
        {

            string ch = Session.Timeout.ToString();
            return View(); 
        }


     
        public JsonResult SaveBalanceAdjust( string clientID , string balance, string amount, string remark)
        {
            string ch = _IAmn.SaveBalanceAdjust(int.Parse(clientID), int.Parse(balance), int.Parse(amount), remark);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report()
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
        public ActionResult ClientLedgerUpdate()
        {
            ViewBag.clientName = Session["clientName"].ToString();
           return View();
        }


        public JsonResult GetClientLedger()
        {
            string clientID = Session["clientID"].ToString();
            string fromDt = Session["fromDt"].ToString();

            List<operation> res = _IAmn.GetClientLedger(int.Parse(clientID), fromDt);

            Session["clientLedger"] = res;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateLedger(int id, int amount, int receivedAmount, int newAmount, string remark)
        {
            return Json(_IAmn.UpdateLedger(id, amount, receivedAmount, newAmount, remark), JsonRequestBehavior.AllowGet); ;
        }


        public JsonResult RptClientLedger( string cname)
        {
            var rep =_IAmn.RptClientLedger(cname);
            return Json(rep, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PreviousAmount(string cid, string fromDt)
        {
            return Json(_IAmn.ClientPreviousBalance(cid, fromDt), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RptClientLedgerDetail(string clientName, string clientID, string fromDt, string toDt)
        {
            Session["clientID"] = clientID;
            Session["fromDt"] = fromDt;
            Session["clientName"] = clientName;
            var rep = _IAmn.RptClientLedgerDetail(int.Parse(clientID), fromDt, toDt ,ref clientDetailRecord);
            Session["ClientLedger"] = rep;
            return Json(rep, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RptClientReceive(string cname, string fromDt, string toDt)
        {
            var rep = _IAmn.RptClientReceive(cname, fromDt, toDt );
            return Json(rep, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RptClientReceivePrint(string recID)
        {
            string ch = _IAmn.SendReceipt(int.Parse(recID), "Y");
            ch = Path.GetFileName(ch);
            return Json(ch, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RptOutstanding(string cname, string fromDt, string toDt)
        {
            return Json(_IAmn.RptOutstanding(cname, fromDt, toDt), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RptTechnical(string designerID, string fromDt, string toDt)
        {
            return Json(_IAmn.RptTechnical(designerID, fromDt, toDt), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClientLedgerPDF(string cid, string cname, string addr, string fromDt, string toDt)
        {
            string ch = createClientLedgerPDF(clientDetailRecord, cid, cname , addr , fromDt, toDt);
            return Json(ch, JsonRequestBehavior.AllowGet);
        }

        public string createClientLedgerPDF(DataTable dt, string cid, string cname, string addr , string fromDt , string toDt)
        {

            Document doc = new Document();
            Paragraph para1 = new Paragraph();
            Phrase ph1 = new Phrase();
            iTextSharp.text.Font fnt = new iTextSharp.text.Font();

            PdfPTable table1 = new PdfPTable(2);
            PdfPCell pdfcell;// As PdfPCell
            DataRow dr;
            string logofile = "";
            string balance = "", pdfname, fname ,tmpDt;
            int i,oldBalance;
            DateTime pdt; 
            fname =  "p_" +System.DateTime.Now.Hour.ToString() +System.DateTime.Now.Minute.ToString()+ System.DateTime.Now.Second.ToString() +".pdf";
            pdfname = HostingEnvironment.MapPath("~//PDF_Files//" + fname);
             
            try
            {
                string[] deletepth = Directory.GetFiles(HostingEnvironment.MapPath("~//PDF_Files//"));
                foreach (string fpth in deletepth)
                {
                    System.IO.File.Delete(fpth);
                }
            }
            catch (Exception ex) { }

            var dir1 = new DirectoryInfo(HostingEnvironment.MapPath("~//Images/logoimage"));
            try
            {
                foreach (var file in dir1.GetFiles())
                {
                    logofile = file.Name;
                }
            }
            catch (Exception ex) { }


            oldBalance = _IAmn.ClientPreviousBalance(cid, fromDt);
            var resProfile = _IAmn.GetCompanyProfile();


            PdfWriter.GetInstance(doc, new FileStream(pdfname, FileMode.Create));
            doc.Open();
             

            para1.Alignment = Element.ALIGN_LEFT;
            para1.SetLeading(0.0F, 1.0F);

            //pdfcell = null;
            //ph1 = new Phrase(Environment.NewLine);
            //para1.Add(ph1);

            //fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            //table1 = new PdfPTable(2);
            //int[] cellWidthPercentage = new int[] { 50, 50 };
            //table1.WidthPercentage = 100;
            //table1.HorizontalAlignment = Element.ALIGN_CENTER;
            //table1.SetWidths(cellWidthPercentage);

            //pdfcell = null;
            //pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            //pdfcell.Border = 0;
            //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //table1.AddCell(pdfcell);

            //pdfcell = new PdfPCell(new Phrase(new Chunk("301-H, Pushparatna solintare", fnt)));
            //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //pdfcell.Border = 0;
            //table1.AddCell(pdfcell);


            //pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //pdfcell.Border = 0;
            //table1.AddCell(pdfcell);

            //pdfcell = new PdfPCell(new Phrase(new Chunk("New Palasia, AB Road, Indore - 452001 MP", fnt)));
            //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //pdfcell.Border = 0;
            //table1.AddCell(pdfcell);

            //pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //pdfcell.Border = 0;
            //table1.AddCell(pdfcell);

            //pdfcell = new PdfPCell(new Phrase(new Chunk("Tel: +91-731-4977407 , Cell : +91-96919-06670", fnt)));
            //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //pdfcell.Border = 0;
            //table1.AddCell(pdfcell);

            //pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //pdfcell.Border = 0;
            //table1.AddCell(pdfcell);

            //pdfcell = new PdfPCell(new Phrase(new Chunk(" Email : design.satyam@gmail.com ", fnt)));
            //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //pdfcell.Border = 0;
            //table1.AddCell(pdfcell);

            //para1.Add(table1);

            table1 = new PdfPTable(2);
            int[] cellWidthPercentage = new int[] { 50, 50 };
            table1.WidthPercentage = 100;
            table1.HorizontalAlignment = Element.ALIGN_CENTER;

            table1.SetWidths(cellWidthPercentage);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.Border = 0;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            table1.AddCell(pdfcell);


            pdfcell = new PdfPCell(new Phrase(new Chunk(resProfile.orgName, fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.Border = 0;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk(resProfile.address, fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);


            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk(resProfile.city + " - " + resProfile.pincode + " " + resProfile.state, fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);



            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Tel: " + resProfile.phone + " , Cell : " + resProfile.mobile, fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk(" Email : " + resProfile.emailID, fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            para1.Add(table1);
            table1 = new PdfPTable(2);
            int[] cellWidthPercentage2 = new int[] { 70, 30 };
            table1.WidthPercentage = 100;

            table1.SetWidths(cellWidthPercentage2);

            pdfcell = null;
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            pdfcell = new PdfPCell(new Phrase(new Chunk(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cname.ToLower()), fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthLeft = 0;
            pdfcell.BorderWidthRight = 0;
            pdfcell.BorderWidthBottom = 0;

            table1.AddCell(pdfcell);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            pdfcell = new PdfPCell(new Phrase(new Chunk(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase("Date " + DateTime.Today.Date.Day + "-" + DateTime.Today.Date.Month + "-" + DateTime.Today.Date.Year), fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthLeft = 0;
            pdfcell.BorderWidthRight = 0;
            pdfcell.BorderWidthBottom = 0;

            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk(addr, fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            pdfcell.Border = 0;

            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            pdfcell.Border = 0;

            table1.AddCell(pdfcell);
            para1.Add(table1);

            pdt = Convert.ToDateTime(fromDt);
            fromDt = pdt.ToString("dd-MM-yyyy");

            pdt = Convert.ToDateTime(toDt);
            toDt = pdt.ToString("dd-MM-yyyy");
             

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            ph1 = new Phrase(Environment.NewLine + "                                                                 Statement From " + fromDt +" To " + toDt , fnt);
            para1.Add(ph1);


            table1 = new PdfPTable(2);
            cellWidthPercentage = new int[] {  20,10 };
            table1.WidthPercentage = 30;
            table1.HorizontalAlignment = 0;
            table1.SetWidths(cellWidthPercentage);

            pdfcell = null;
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            
            pdfcell = new PdfPCell(new Phrase(new Chunk(" Previous Balance", fnt)));
            pdfcell.Border = 0;
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk(oldBalance.ToString(), fnt)));
            pdfcell.Border = 0;
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);
            para1.Add(table1);

            ph1 = new Phrase(Environment.NewLine);
            para1.Add(ph1);

            table1 = new PdfPTable(10);
            cellWidthPercentage = new int[] { 5, 10,20, 12, 15, 10,10,15,10,10};
            table1.WidthPercentage = 100;
            table1.HorizontalAlignment = 0;
            table1.SetWidths(cellWidthPercentage);

            pdfcell = null;
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Sno", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Project ID", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Project name", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Date", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Project type", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);
             
            pdfcell = new PdfPCell(new Phrase(new Chunk("Package", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Amount", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);


            pdfcell = new PdfPCell(new Phrase(new Chunk("Amount Receive", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Balance", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);


            pdfcell = new PdfPCell(new Phrase(new Chunk("Remark", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            table1.AddCell(pdfcell);
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            int cnt = 1;
    
            for (i=0;i<dt.Rows.Count;i++)
            {
                dr = dt.Rows[i];
                pdfcell = new PdfPCell(new Phrase(new Chunk( cnt.ToString() , fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(dr["projectID"].ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(dr["pname"].ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);


                pdt = Convert.ToDateTime (dr["dt"].ToString());
                tmpDt = pdt.ToString("dd-MMM-yyyy");

                pdfcell = new PdfPCell(new Phrase(new Chunk(tmpDt, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

               
                pdfcell = new PdfPCell(new Phrase(new Chunk(dr["ptype"].ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(dr["package"].ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(dr["amount"].ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(dr["receive"].ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);
                 
                balance = dr["balance"].ToString();
                pdfcell = new PdfPCell(new Phrase(balance, fnt));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(dr["remark"].ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);
                 
                cnt++;

            }
            para1.Add(table1);

            ph1 = new Phrase(Environment.NewLine);
            para1.Add(ph1);

            table1 = new PdfPTable(2);
            cellWidthPercentage = new int[] { 20, 10 };
            table1.WidthPercentage = 30;
            table1.HorizontalAlignment = 0;
            table1.SetWidths(cellWidthPercentage);

            pdfcell = null;
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            pdfcell = new PdfPCell(new Phrase(new Chunk(" Total Due Balance", fnt)));
            pdfcell.Border = 0;
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk(balance.ToString(), fnt)));
            pdfcell.Border = 0;
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

            para1.Add(table1);

           

            ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "     For " + resProfile.orgName.ToUpper()), fnt);
            para1.Add(ph1);


            ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + "         " + resProfile.orgName), fnt);
            para1.Add(ph1);


            ph1 = new Phrase(String.Format(Environment.NewLine + "     Authorized signatory"), fnt);
            para1.Add(ph1);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);
            ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "                                                                     Please note that this is a system generated mail and does not require signature."), fnt);
            para1.Add(ph1);

           

            iTextSharp.text.Image jpg;
            jpg = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~//Images/logoimage/" + logofile));

            jpg.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
            jpg.ScaleToFit(90.0F, 90.0F);
            jpg.SetAbsolutePosition(doc.PageSize.Width - 560, 740.0F);
            doc.Add(jpg);



            doc.Add(para1);
            doc.Close();
            doc.Dispose();
            doc = null;


            return fname;
        }


        public JsonResult RptDesignerLedger( string dname)
        {
            var rep = _IAmn.RptDesignerLedger(dname);
            return Json(rep, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RptDesignerLedgerDetail(string sid, string dt1, string dt2)
        {
            var rep = _IAmn.RptDesignerLedgerDetail(int.Parse(sid), dt1, dt2);
            return Json(rep, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RptQuotation(string dt1 , string dt2 , string searchOpt, string projectID , string cname, string pname)
        {
            return Json(_IAmn.RptQuotation(dt1, dt2, searchOpt, projectID, cname, pname), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RptSiteVisit(string opt, string projectID, string cname, string pname)
        {
            return Json(_IAmn.RptSiteVisit(opt, projectID, cname, pname), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RptProjectHistory(string opt, string projectID, string cname, string pname)
        {
            return Json(_IAmn.RptProjectHistory(opt, projectID, cname, pname), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowBalanceAdjust(string dt1, string dt2, string cname)
        {
            IEnumerable<operation> bal = _IAmn.ShowBalanceAdjust(dt1, dt2, cname);
            return Json(bal, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GMailSetting()
        {
            //GMail obj= _IAmn.getGMailAccount();
            //ViewBag.sessionTimeOut = Session.Timeout.ToString();
            //return View(obj);
            return View();
        }

        [HttpPost]
        public ActionResult GMailSetting(GMail obj)
        {
            
            //_IAmn.UpdateGMailPassword(obj);
            return RedirectToAction("GMailSetting");
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult ShowStaff()
        {
            return View();
        }
        public ActionResult ReportClientReceive()
        {

            return View();
        }
        public ActionResult ReportOutstanding()
        {

            return View();
        }
        public ActionResult ReportDesignerLedger()
        {

            return View();
        }

        public ActionResult ReportQuotation()
        {

            return View();
        }
        public ActionResult ReportBalanceAdjust()
        {

            return View();
        }
        public ActionResult ReportClient()
        {
            return View();
        }

        public ActionResult ReportClientLedger()
        {
          
            return View();
        }

        public ActionResult ReportMonthQuotation()
        {
            return View();
        }
        public ActionResult Test()
        {
            ClientInheritanceModel obj = new ClientInheritanceModel();
            obj.clientName = "Abc";
            obj.amount = 58;
            return View(obj);
        }
        public JsonResult TestData()
        {
            List<ClientInheritanceModel> lst = new List<ClientInheritanceModel>();
            lst.Add(
                   new ClientInheritanceModel
                   {
                       clientName = "Abc",
                       address = "Gulab vihar",
                       city = "Rau",
                       amount = 150000
                   }
                 );

            lst.Add(
                   new ClientInheritanceModel
                   {
                       clientName = "Ravi",
                       address = "Gulab vihar",
                       city = "Rau",
                       amount = 200000
                   }
                 );
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult DashBoard_getProjectType()
        {
            return Json(_IAmn.DashBoard_getProjectType(), JsonRequestBehavior.AllowGet);
           
        }

        public JsonResult getTopPerformers()
        {
            return Json(_IAmn.getTopPerformers(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Dashboard_getMonthQuotation()
        {
            return Json(_IAmn.Dashboard_getMonthQuotation(), JsonRequestBehavior.AllowGet);
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
