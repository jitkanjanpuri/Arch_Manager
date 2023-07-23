using System;
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
        staff obj = new staff();
       

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


        public ActionResult Testss()
        {

            return View();
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
                    }

                }
                //var errors = ModelState.Values.SelectMany(v => v.Errors);

            }
            catch (Exception ex)
            {

                
            }
            return RedirectToAction("CompanyProfile");
        }




        public JsonResult getProjectQuotation()
        {
            var prj = _IAmn.getProjectQuotation();
            return Json(prj, JsonRequestBehavior.AllowGet);
        }

        
       public JsonResult getCCWidget()
        {
            List<quotation> prjType = new List<quotation>();
            try
            {
                DataSet ds = new DataSet();
              
                prjType.Add(
                        new quotation
                        {
                            projectType = "5-01-2023",
                            sno = 7
                        });
                prjType.Add(
                        new quotation
                        {
                            projectType = "6-01-2023",
                            sno = 4
                        });
                prjType.Add(
                        new quotation
                        {
                            projectType = "7-01-2023",
                            sno = 9
                        });

            }
            catch (Exception ex) { }
            return Json(prjType, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getHighestMonthSale()
        {
            List<quotation> prjType = new List<quotation>();
            try
            {
                DataTable ds = new DataTable();
                prjType.Add(
                             new quotation
                             {
                                 clientname = "Rajiv",
                                 targetAmount =100,
                                 receiveAmount = 70 ,
                                 amount =200,
                                 depositAmount = 100,
                                 dtstr ="Jan"
                             }
                        );

                prjType.Add(
                          new quotation
                          {
                              clientname = "Manoj",
                              targetAmount = 150,
                              receiveAmount = 110,
                              amount =200,
                              depositAmount = 100,
                              dtstr = "Jan"
                          }
                     );
                prjType.Add(
                         new quotation
                         {
                             clientname = "Sanjiv",
                             targetAmount = 50,
                             receiveAmount = 20,
                             amount = 200,
                             depositAmount = 100,
                             dtstr = "Jan"
                         }
                    );

            }
            catch (Exception ex) { }
            return Json(prjType, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getQuotation()
        {
            return Json(_IAmn.getQuotation(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveProjectManagement(string projectID, string finalAmount)
        {
            string ch = "";
            try
            {
                //ch = Session["user"].ToString();
                ch =  _IAmn.UpdateQuotation(int.Parse(projectID), int.Parse(finalAmount));
            }
            catch (Exception ex)
            {
                //FormsAuthentication.SignOut();
                // FormsAuthentication.SetAuthCookie("", true);
            }
            return Json(ch, JsonRequestBehavior.AllowGet);
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
        public ActionResult Registration()
        {
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
            return View();
        }

        [HttpPost]
        public ActionResult Registration(staff st)
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
                _IAmn.InsertRegistration(st);
                ModelState.Clear();
                ViewBag.message = "success";
            }
            return View();
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
            List<tblStaff> obj = new List<tblStaff>();
            if (name == null) name = "";
            var lst = _IAmn.SearchRegistration(name).OrderBy(x => x.name);

            foreach (var item in lst)
            {
                obj.Add(new tblStaff
                {
                    staffID = item.staffID,
                    name = item.name,
                    designation = item.designation,
                    address = item.address,
                    city = item.city,
                    mobile = item.mobile,
                    phone = item.phone,
                    emailID = item.emailID,
                    username = item.username,
                    password = item.password

                });
            }


            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAddProject(string projectID)
        {
            operation obj = new operation();
            obj = _IAmn.SearchAddProject(int.Parse(projectID));
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveNewProject(string projectID, string pcategory, string dname, string amount)
        {
            operation obj = new operation();
            obj.clientName =   _IAmn.SaveNewProject(int.Parse(projectID), pcategory , dname, int.Parse(amount));
            return Json(obj, JsonRequestBehavior.AllowGet);

           
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
            var obj = new List<SelectListItem>();
            var dsn = _IAmn.getOperationDesigner();
            obj.Add(new SelectListItem
            {    
                Text = "All",
                Value = "0",
                
            });
            foreach (var item in dsn)
            {
                obj.Add(new SelectListItem
                {
                    Text = item.name,
                    Value = item.staffID.ToString(),

                });
            }
            
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
         
        public JsonResult getProjectAssignList()
        {
            var prj = _IAmn.getProjectAssign();
            return Json(prj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveProjectAssigned(string projectID, string staffID, string projectCategory, string designerAmount)
        {
            obj.name = _IAmn.SaveProjectAssigned(projectID, staffID, projectCategory, designerAmount);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCurrentWorkingList(string dname)
        {
            var lst = _IAmn.getCurrentWorkingList(dname);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

  
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


        public JsonResult AmountReceive(string clientID, string amount, string remark)
        {
            string ch =  _IAmn.AmountReceive(int.Parse(clientID), amount, remark);
            return Json(ch, JsonRequestBehavior.AllowGet);  
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

        public JsonResult SaveGMail(string gmailID, string pwd)
        {
            //return Json(_IAmn.SaveGMail(gmailID.Trim(), pwd.Trim()) , JsonRequestBehavior.AllowGet);

            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult getGmailAccount()
        {
            IEnumerable<GMail> obj1;//= _IAmn.getGmail();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveGMailAccount(string id)
        {
            string name = "";// _IAmn.RemoveGMailAccount(int.Parse(id));
            return Json(name, JsonRequestBehavior.AllowGet);
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

            oldBalance = _IAmn.ClientPreviousBalance(cid, fromDt);
             
            PdfWriter.GetInstance(doc, new FileStream(pdfname, FileMode.Create));
            doc.Open();
             

            para1.Alignment = Element.ALIGN_LEFT;
            para1.SetLeading(0.0F, 1.0F);

            pdfcell = null;
            ph1 = new Phrase(Environment.NewLine);
            para1.Add(ph1);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            table1 = new PdfPTable(2);
            int[] cellWidthPercentage = new int[] { 50, 50 };
            table1.WidthPercentage = 100;
            table1.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.SetWidths(cellWidthPercentage);

            pdfcell = null;
            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.Border = 0;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("301-H, Pushparatna solintare", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);


            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("New Palasia, AB Road, Indore - 452001 MP", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Tel: +91-731-4977407 , Cell : +91-96919-06670", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk(" Email : design.satyam@gmail.com ", fnt)));
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
             

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "For SATYAM DESIGN  " ), fnt);
            para1.Add(ph1);
         
            ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + "Satyam Design"), fnt);
            para1.Add(ph1);

            
            ph1 = new Phrase(String.Format(Environment.NewLine + "Authorized Signatory"), fnt);
            para1.Add(ph1);
             
            iTextSharp.text.Image jpg;
            jpg = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~//Images//Logo.png"));

            jpg.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
            jpg.ScaleToFit(90.0F, 90.0F);
            jpg.SetAbsolutePosition(doc.PageSize.Width - 560, 735.0F);
            doc.Add(jpg);
            
            jpg = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~//Images//logoname.jpeg"));

            jpg.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
            jpg.ScaleToFit(100.0F, 100.0F);
            jpg.SetAbsolutePosition(doc.PageSize.Width - 145,782.0F);
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


        
        public JsonResult RptQuotation( string ptype, string dt1 , string dt2 , string searchOpt, string projectID , string cname)
        {
            var prj = _IAmn.RptQuotation(ptype, dt1, dt2, searchOpt, projectID, cname);
            return Json(prj, JsonRequestBehavior.AllowGet);
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
            clsReport obj = new clsReport();
            obj.dt = DateTime.Today;
            return View(obj);
        }

        public ActionResult ReportMonthQuotation()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
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
