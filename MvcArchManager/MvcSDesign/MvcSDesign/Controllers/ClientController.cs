using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcSDesign.EF;
using MvcSDesign.Models;
using MvcSDesign.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Hosting;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Data;
 
 
namespace MvcSDesign.Controllers
{
    public class ClientController : Controller
    {
        ConvertNumberToWord objNTW = new ConvertNumberToWord();
        IClient IClnt;
        IAdmin IAdn;
        clientModel obj = new clientModel();
        ClientRepository objCR;//= new ClientRepository();
        public ClientController()
        {
            IClnt = new ClientRepository(new ArchManagerDBEntities());
            IAdn = new AdminRepository(new ArchManagerDBEntities());

        }
        public JsonResult CheckClientExists(clientModel obj)
        {
            string ch = IClnt.ClientNameValidation(obj);
            return Json(ch, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {

            try
            {
                //string ch = Session["user"].ToString();
                obj.statelist = getStateList();
            }
            catch (Exception ex)
            {
                //FormsAuthentication.SignOut();
                //FormsAuthentication.SetAuthCookie("", true);

                //return RedirectToAction("Index", "Login");
            }
           
            return View(obj);
        }




        IEnumerable<SelectListItem> getStateList()
        {
            var st = new List<SelectListItem>();
          
            var res = IClnt.GetState();
            foreach(var item in res)
            {
                st.Add(new SelectListItem
                {
                    Text = item.state
                });
            }

            return st;

        }


        [HttpPost]
        public ActionResult Index(clientModel obj )
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
            if (ModelState.IsValid)
            {
                IClnt.InsertData(obj);
                ModelState.Clear();

                return RedirectToAction("Index");
            }

            obj.statelist = getStateList();
            
            return View(obj);

        }
         
        public JsonResult getClient(string name)
        {
            List<tblClient> obj = new List<tblClient>();
            var lst = IClnt.getByName(name);
            foreach (var item in lst)
            {
                obj.Add(new tblClient
                {
                    clientID = item.clientID,
                    clientName = item.clientName,
                    mobile = item.mobile,
                    address = item.address,
                    city = item.city,
                    emailID = item.emailID,
                    state = item.state
                });
            }

            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public string SendPromoMail( string title, string msg, string[] clientList)
        {
            string ch = "";
            try
            {
                msg = msg.Replace("\n", "<br />");
                //ch =  IClnt.SendPromoMail(title, msg, clientList);
            }
            catch (Exception ex) { }
            return ch;
           
        } 
        public JsonResult getClient_PromMail(string name , string city)
        {
            //var lst = IClnt.getClient_PromMail(name, city);
            //return Json(lst, JsonRequestBehavior.AllowGet);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult getClientList()
        {
            List<SelectListItem> obj = new List<SelectListItem>();
            try
            {
                //var lst = IClnt.getAll();
                //foreach (var item in lst)
                //{
                //    obj.Add(new SelectListItem
                //    {
                //        Text = item.clientName,
                //        Value = item.clientID.ToString()
                //    });
                //}
            }
            catch (Exception ex) { }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getMonthQuotation( string ptype)
        {
            var rec = IClnt.getMonthQuotation(ptype);
            return Json(rec, JsonRequestBehavior.AllowGet);
             
        }

        public JsonResult SearchClient(string name)
        {
            return Json(IClnt.SearchClientByName(name), JsonRequestBehavior.AllowGet);
        }


       
        public JsonResult AllClient()
        {
            return Json(IClnt.GetAll(), JsonRequestBehavior.AllowGet);

        }

         

        public JsonResult UpdateClient(string cid, string name, string orgName , string address, string city ,string state,  string phone , string mobile, string emailID)
        {
            
            obj.clientID = int.Parse(cid);
            obj.clientName = name.Trim();
            obj.orgName =(orgName ==null) ?"-" : orgName.Trim();
            obj.address =  (address == null )?"-" : address.Trim() ;
            obj.city = city.Trim();
            obj.state = state.Trim();
            obj.phone = (phone==null) ?"-": phone.Trim();
            obj.mobile = mobile;
            obj.emailID = emailID;
            string ch = "";// IClnt.Update(obj);
            return Json(ch, JsonRequestBehavior.AllowGet);

        }
        public JsonResult DeleteClient(string cid)
        {
            string ch =  IClnt.DeleteClient(int.Parse(cid));
            return Json(ch, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ShowQuotationPdf(string pid, string projectType)
        {
            string pth = "";
            try
            {
                var prj = IClnt.getProjectDetail(int.Parse(pid));
                quotation obj1 = new quotation();
                foreach (var item in prj)
                {
                    obj1.clientID = item.clientID.ToString();
                    obj1.clientname = item.clientName;
                    obj1.emailID = item.status;
                    obj1.package = item.package;
                    obj1.projectLevel = item.projectLevel;
                    obj1.projectType = item.projectType;
                    obj1.plotSize = item.plotSize;
                    obj1.amount = item.amount;
                    obj1.emailID = item.status;
                    obj1.projectname = item.projectName;
                    obj1.city = item.rowcolor;
                    obj1.dt = item.dt;
                    obj1.finalizeAmount = item.finalizeAmount;
                    obj1.status = item.status;
                    obj1.service = item.service;
                }
                pth = CreateQuotationPDF(obj1, int.Parse(pid), ref pth);
                pth = Path.GetFileName(pth);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return Json(pth, JsonRequestBehavior.AllowGet);
        }

        public string SaveQuotation(string cid, string projectname, string service, string empdata)
        {
            operation obj = new operation();
            DataTable dt = new DataTable();
            long pid = 0;
            obj.clientID = int.Parse(cid);
            obj.projectName = projectname;
            obj.service = service;

            IClnt.SaveQuotation(obj, empdata, ref pid);
            ViewBag.Message = " Quotation successfully generated , project ID is " + pid;
            return "";
        }
        public JsonResult SendClientQuotation(string pid, string projectType)
        {
            string ch = "";
            try
            {
                var prj = IClnt.getProjectDetail(int.Parse(pid));
                if (projectType == "Interior")
                {
                    operation obj1 = new operation();
                    foreach (var item in prj)
                    {
                        obj1.clientID = item.clientID;
                        obj1.clientName = item.clientName;
                        obj1.emailID = item.status;
                        obj1.package = item.package;
                        obj1.projectLevel = item.projectLevel;
                        obj1.projectType = item.projectType;
                        obj1.plotSize = item.plotSize;
                        obj1.amount = item.amount;
                        obj1.emailID = item.status;
                        obj1.projectName = item.projectName;
                        obj1.city = item.rowcolor;
                        obj1.dt = item.dt;
                    }
                    DataTable dt = new DataTable();
                    DataRow dr;

                    dt.Columns.Add("projectDetails");
                    dt.Columns.Add("particular");
                    dt.Columns.Add("unit");
                    dt.Columns.Add("amount");

                }
                else
                {
                    quotation obj = new quotation();

                    foreach (var item in prj)
                    {
                        obj.clientID = item.clientID.ToString();
                        obj.clientname = item.clientName;
                        obj.emailID = item.status;
                        obj.package = item.package;
                        obj.projectLevel = item.projectLevel;
                        obj.projectType = item.projectType;
                        obj.plotSize = item.plotSize;
                        obj.amount = item.amount;
                        obj.emailID = item.status;
                        obj.projectname = item.projectName;
                        obj.address = item.designerName;
                        obj.city = item.rowcolor;
                        obj.dt = item.dt;
                        obj.finalizeAmount = 0;
                    }
                    SendQuotation(obj, int.Parse(pid));
                    ch = "success";
                }
            }

            catch (Exception ex)
            {
                ch = ex.Message;
            }

            return Json(ch, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Quotation()
        {
            try
            {
                if (Session["quotation"].ToString() == "")
                {
                    ViewBag.Message = "";
                }
                else
                {
                    ViewBag.Message = Session["quotation"].ToString();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "";
               
            }
            return View();
        }


        public ActionResult RevisedQuotation()
        {
            try
            {
                //if (Session["RevisedQuotation"].ToString() == "")
                //{
                //    ViewBag.Message = "";
                //}
                //else
                //{
                //    ViewBag.Message = Session["RevisedQuotation"].ToString();
                //}
                Session["revQuotation"] = null;
            }
            catch (Exception ex)
            {
                //ViewBag.Message = "";

            }
            return View();
        }

        public JsonResult GetRevisedQuotation(string projectID)
        {

            var res = IClnt.GetProjectDetailItem(int.Parse(projectID));
            List <ProjectItemModel> li = new List<ProjectItemModel>();
            ProjectItemModel obj = new ProjectItemModel();

            foreach (var item in res)
            {
                obj = new ProjectItemModel();
                obj.projectDetails = item.projectDetails;
                obj.services = item.services;
                obj.area = item.area;
                obj.rate = item.rate;
                obj.amount = item.amount;
                li.Add(obj);
               
            }
            Session["revQuotation"] = li;
            return Json(li, JsonRequestBehavior.AllowGet);

            
        }
        public JsonResult UpdateQuotation(string projectID, string projectDetail, string service)
        {
            List<ProjectItemModel> li = (List<ProjectItemModel>)Session["revQuotation"];
            operation obj = new operation();
           
            obj.projectID = int.Parse(projectID);
            obj.projectName = projectDetail.Trim();
            obj.service = service.Trim();

            string ch = IClnt.UpdateQuotation(obj, li );
            //ViewBag.Message = " Quotation successfully generated , project ID is " + pid;
            return Json(ch, JsonRequestBehavior.AllowGet);
        }



        public JsonResult addProjectItem(string projectDetail, string services, string area,   string rate, string amount)
        {
          
            if (Session["revQuotation"] == null)
            {
                List<ProjectItemModel> li = new List<ProjectItemModel>();
                ProjectItemModel obj = new ProjectItemModel();
                obj.projectDetails = projectDetail;
                obj.services = services;
                obj.area = area;
                obj.rate = int.Parse(rate);
                obj.amount = int.Parse(amount);
                li.Add(obj);
                Session["revQuotation"] = li;
                return Json(li, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ProjectItemModel> li = (List<ProjectItemModel>)Session["revQuotation"];
                var pdt = li.Where(x => ((x.projectDetails == projectDetail) && (x.services == services))).FirstOrDefault();
                if (pdt == null)
                {
                    ProjectItemModel obj = new ProjectItemModel();
                    obj.projectDetails = projectDetail;
                    obj.services = services;
                    obj.area = area;
                    obj.rate = int.Parse(rate);
                    obj.amount = int.Parse(amount);
                    li.Add(obj);
                    Session["revQuotation"] = li;
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

            
        }

        public JsonResult removeRevQuotationItem(string projectDetail, string service)
        {
            List<ProjectItemModel> li = (List<ProjectItemModel>)Session["revQuotation"];
            var pdt = li.Where(x => ((x.projectDetails == projectDetail) && (x.services == service))).FirstOrDefault();
            if (pdt != null)
            {
                li.Remove(pdt);
            }
            return Json(li, JsonRequestBehavior.AllowGet);


        }
          
        void SendQuotation(quotation qut, int projectID)
        {
            string gMailAccount, password, clientMailId = "", str = "", pdfname = "", amountInWord = "";
            int i;
            string[] arr = new string[10];
            GMail obj = new GMail();//  IAdn.getGMailAccount();

            gMailAccount = obj.gmailID;
            password = obj.pwd;
            clientMailId = "office.satyamdesign@gmail.com"; // satyam mail id

            try
            {
                NetworkCredential loginInfo = new NetworkCredential(gMailAccount, password);
                MailMessage objMail = new MailMessage();

                str = "";
                if (clientMailId.IndexOf(',') > 0)
                    arr = clientMailId.Split(',');
                else if (clientMailId.IndexOf(';') > 0)
                    arr = clientMailId.Split(';');
                else if (clientMailId.IndexOf('/') > 0)
                    arr = clientMailId.Split('/');
                else
                    str = clientMailId;

                if (str.Length > 0)
                    objMail.To.Add(new MailAddress(clientMailId));
                else
                {
                    for (i = 0; i < arr.Length; i++)
                    {
                        objMail.To.Add(new MailAddress(arr[i]));
                    }
                }


                pdfname = CreateQuotationPDF(qut, projectID, ref amountInWord);

                CultureInfo CInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo TInfo = CInfo.TextInfo;
                objMail.From = new MailAddress(gMailAccount);
                objMail.IsBodyHtml = true;
                objMail.Priority = MailPriority.High;

                Attachment at = new Attachment(pdfname);
                objMail.Attachments.Add(at);

                 

                str = " <br/><br/> <br/> <br/>           Date: " + qut.dt.ToString("dd/MM/yyyy")+                  
                      "<br/>         Quotation   <br />" +
                      "<br/>           To <br/>            Dear " + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(qut.clientname.ToLower()) + 
                      "<br/>          " + qut.city;

                str += " <br/><br/>               I am pleased to offer you most competitive proposal. I am certain you will find the information  in line," +
                      "  <br/> apt to your needs. The proposal covers our amount.";


                str += " <br/> <br/>  <table  border='1'  style ='border-width :thin; width: 550px; margin-left :50px;'> " +
                      " <tr> <th style=''>  Project name </th>" +
                      " <th style=''>  Project ID </th>" +
                      " <th style=''>  Particulars </th>" +
                      " <th style=''>  Package </th>" +
                      " <th style=''>  Amount </th> </tr>" +
                      " <tr> <td style='text-align:center'> " + qut.projectname + "</td>" +
                      "    <td style='text-align:center'> " + projectID.ToString() + "</td>" +
                      "    <td style='text-align:center'> " + qut.projectType + "</td>" +
                      "    <td style='text-align:center'> " + qut.package + "</td>" +
                      "    <td style='text-align:center'> " + qut.amount.ToString() + "</td></tr>  " +
                     " <tr>  <td> </td> <td> </td>  <td> </td> <td style='text-align:center'><b> Total <b/> </td> <td style='text-align:center'> <b> " + qut.amount.ToString() + " <b/></td></tr> </table>";
                str += "<br/><br/> In words :  " + amountInWord + " Rs. Only";


                str += "<br/><br/> Thank you for the opportunity to serve you. We look forward for further communication with you, " +
                       "<br/> after you have reviewed the proposal.";

                str += "<br/><br/> <br/> For  SATYAM DESIGN <br/><br/>" +
                       " Satyam Design <br/>" +
                       " Authorized signatory ";

                str += "<br/><br/><br/> <br/><br/><br/>          Please note that this is a system generated mail and does not require signature.";

                str += "<br/><br/><br/> <br/>          Tel   : +91-731-4977407 , <br/> Cell : +91-96919-06670" +
                       "<br/>          Email : design.satyam@gmail.com";
                 
                objMail.Subject = "  Quotation for " + qut.projectType +"_"+ projectID.ToString();
                objMail.Body = str;
                SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = loginInfo;
                client.Send(objMail);
                client = null;
                objMail.Dispose();
                
                 
            }
            catch (Exception ex)
            {
               
            }
        }
         
        public string CreateQuotationPDF(quotation qut, int projectID, ref string amountInWord)
        {

            Document doc = new Document();
            Paragraph para1 = new Paragraph();
            Phrase ph1 = new Phrase();
            iTextSharp.text.Font fnt = new iTextSharp.text.Font();

            PdfPTable table1 = new PdfPTable(2);
            PdfPCell pdfcell;// As PdfPCell
            string logofile = "";
            amountInWord = "";

            string pdfname = HostingEnvironment.MapPath("~//PDF_Files//quatation_" + projectID + ".pdf");
            var resProfile = IAdn.GetCompanyProfile();

            
            if(resProfile ==null)
            {
                return "";
            }
            
            if (System.IO.File.Exists(pdfname))
            {
                // code for delete file
                var dir = new DirectoryInfo(HostingEnvironment.MapPath("~//PDF_Files"));
                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                     
                    foreach (var file in dir.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch (IOException ex)
                        {

                        }
                    }
                }
                catch (Exception ex) { }
            }

            
            var dir1 = new DirectoryInfo(HostingEnvironment.MapPath("~//Images/logoimage"));
            try
            {
                foreach (var file in dir1.GetFiles())
                {
                    logofile = file.Name;
                }
            }
            catch (Exception ex) { }
            


            PdfWriter.GetInstance(doc, new FileStream(pdfname, FileMode.Create));
            doc.Open();
            //doc.SetMargins(5f, 5f, 0f, 10f);
            para1.Alignment = Element.ALIGN_LEFT;
            para1.SetLeading(0.0F, 1.0F);

            pdfcell = null;
            table1 = new PdfPTable(2);
            int[] cellWidthPercentage = new int[] { 50, 50 };
            table1.WidthPercentage = 100;
            table1.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.SetWidths(cellWidthPercentage);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, BaseColor.RED);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.Border = 0;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            table1.AddCell(pdfcell);


            pdfcell = new PdfPCell(new Phrase(new Chunk("V R ARCHITECTS ", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.Border = 0;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("AN INNOVATIVE DESIGN STUDIO", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("ARCHITECTURE INTERIORS PROJECT-MANAGEMENT GRAPHICS", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);



             
            para1.Add(table1);

            
            table1 = new PdfPTable(2);
            int[] cellWidthPercentage2 = new int[] { 70,30 };
            table1.WidthPercentage = 100;

            table1.SetWidths(cellWidthPercentage2);

            pdfcell = null;
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Ref. No. ", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            //pdfcell.Border = 1;
            pdfcell.BorderWidthLeft = 0;
            pdfcell.BorderWidthRight = 0;
            pdfcell.BorderWidthBottom = 0;
           
            table1.AddCell(pdfcell);
            pdfcell = new PdfPCell(new Phrase(new Chunk("Date " + qut.dt.Day + "-" + qut.dt.Month + "-" + qut.dt.Year, fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //pdfcell.Border = 1;
            pdfcell.BorderWidthLeft = 0;
            pdfcell.BorderWidthRight = 0;
            pdfcell.BorderWidthBottom = 0;
            table1.AddCell(pdfcell);

            para1.Add(table1);


            table1 = new PdfPTable(1);
            int[] cellWidthPercentage7 = new int[] { 100 };
            table1.WidthPercentage = 100;
            table1.SetWidths(cellWidthPercentage7);
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            pdfcell = new PdfPCell(new Phrase(new Chunk("QUOTATION ", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.Border = 0;
            table1.AddCell(pdfcell);

            para1.Add(table1);



            ph1 = new Phrase(Environment.NewLine+ Environment.NewLine);
            para1.Add(ph1);


            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            ph1 = new Phrase(new Phrase(new Chunk(  "To : " + qut.clientname.ToUpper() +" " + qut.city, fnt)));
            para1.Add(ph1);
            ph1 = new Phrase(Environment.NewLine + Environment.NewLine);
            para1.Add(ph1);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            ph1 = new Phrase(new Phrase(new Chunk("Quotation No : " + "VRA-"+ projectID, fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(Environment.NewLine + Environment.NewLine + Environment.NewLine);
            para1.Add(ph1);
           
            ph1 = new Phrase(new Phrase(new Chunk("Dear Sir, ", fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(Environment.NewLine + Environment.NewLine);
            para1.Add(ph1);

            ph1 = new Phrase(new Phrase(new Chunk("We are pleased to offer you firm consultancy for architectural and allied services for the following project at fees stated below: ", fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(Environment.NewLine+ Environment.NewLine + Environment.NewLine);
            para1.Add(ph1);

            ph1 = new Phrase(new Phrase(new Chunk("Project : "+ qut.projectname, fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(Environment.NewLine + Environment.NewLine );
            para1.Add(ph1);

            ph1 = new Phrase(new Phrase(new Chunk("Services :  " + qut.service, fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(Environment.NewLine + Environment.NewLine);
            para1.Add(ph1);

            ph1 = new Phrase(new Phrase(new Chunk("Fees : "  , fnt)));
            para1.Add(ph1);


            table1 = new PdfPTable(7);
            int[] cellWidthPercentage1 = new int[] {5, 10, 25, 25, 15, 12, 12 };
            table1.WidthPercentage = 100;
            table1.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.SetWidths(cellWidthPercentage1);
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            pdfcell = null;
            pdfcell = new PdfPCell(new Phrase(new Chunk("Sno", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthLeft = 1;
            pdfcell.BorderWidthRight = 1;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            //pdfcell.FixedHeight = 20;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Project Code", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            //pdfcell.BorderWidthLeft = 1;
           // pdfcell.BorderWidthRight = 1;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
          //  pdfcell.FixedHeight = 20;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

             

            pdfcell = new PdfPCell(new Phrase(new Chunk("Project Description", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
          //  pdfcell.FixedHeight = 20;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Services", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
         //   pdfcell.FixedHeight = 20;

            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            pdfcell = new PdfPCell(new Phrase(new Chunk("AREA APPRO. ", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;

         //   pdfcell.FixedHeight = 20;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Rate/SQ.FT. ", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            //pdfcell.FixedHeight = 20;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            pdfcell = new PdfPCell(new Phrase(new Chunk("Amount", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            //pdfcell.FixedHeight = 20;
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            table1.AddCell(pdfcell);

            var resItem = IClnt.GetProjectDetailItem(projectID);

            string sno = "1", varPrj = "VRC-"+ projectID ;

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            foreach (var item in resItem)
            {

                pdfcell = new PdfPCell(new Phrase(new Chunk(sno, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //pdfcell.BorderWidthTop = 1;
                pdfcell.BorderWidthBottom = 1;
             //   pdfcell.FixedHeight = 20;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(varPrj, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
               // pdfcell.BorderWidthTop = 1;
                pdfcell.BorderWidthBottom = 1;
                pdfcell.FixedHeight = 20;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.projectDetails, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
               // pdfcell.BorderWidthTop = 1;
                pdfcell.BorderWidthBottom = 1;
            //    pdfcell.FixedHeight = 20;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.services, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
               // pdfcell.BorderWidthTop = 1;
                pdfcell.BorderWidthBottom = 1;
              //  pdfcell.FixedHeight = 20;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.area, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
               // pdfcell.BorderWidthTop = 1;
                pdfcell.BorderWidthBottom = 1;
            //    pdfcell.FixedHeight = 20;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.rate.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.BorderWidthBottom = 1;
                table1.AddCell(pdfcell);


                pdfcell = new PdfPCell(new Phrase(new Chunk(item.amount.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.BorderWidthBottom = 1;
                table1.AddCell(pdfcell);

                sno = "";
                varPrj = "";

            }

            para1.Add(table1);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            ph1 = new Phrase(String.Format(    Environment.NewLine + " Govt. taxes shall be paid separately."), fnt);
            para1.Add(ph1);

            ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine +  Environment.NewLine + "For " + resProfile.orgName.ToUpper()), fnt);
            para1.Add(ph1);

            ph1 = new Phrase(String.Format(Environment.NewLine+ Environment.NewLine + Environment.NewLine + "Sincerely,"), fnt);
            para1.Add(ph1);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD , BaseColor.BLACK);
            ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine +   resProfile.name), fnt);
            
            para1.Add(ph1);
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            ph1 = new Phrase(String.Format(Environment.NewLine + "Architect"), fnt);
            para1.Add(ph1);

             
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC, BaseColor.BLUE);
            ph1 = new Phrase(String.Format(  Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "                                              G-2, Nursery Circle, Vaishali Nagar, Jaipur - 302 021. Rajasthan (INDIA) "), fnt);
            para1.Add(ph1);
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC, BaseColor.BLUE);
            ph1 = new Phrase(String.Format(Environment.NewLine  +"                            Cont: +91 88888 59911, E - mail : vrarchitects.in @gmail.com, website: www.vrarchitects.in "), fnt);
            para1.Add(ph1);

            iTextSharp.text.Image jpg;
            jpg = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~//Images/logoimage/"+logofile));

            jpg.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
            jpg.ScaleToFit(70.0F, 70.0F);
            jpg.SetAbsolutePosition(doc.PageSize.Width-270, 760.0F);
            doc.Add(jpg);

            doc.Add(para1);
            doc.Close();
            doc.Dispose();
            doc = null;


            return pdfname;
        }

         
         
        //string CreateInteriorQuotationPDF(operation qut, string emailID,string citystate, int projectID, DataTable dt, DateTime reqDate , ref string amountInword)
        //{
        //    string pdfname = "";
        //    int i, amount = 0;
        //    string[] arr = new string[10];
        //    Document doc = new Document();
        //    Paragraph para1 = new Paragraph();
        //    Phrase ph1 = new Phrase();
        //    iTextSharp.text.Font fnt = new iTextSharp.text.Font();

        //    PdfPTable table1 = new PdfPTable(2);
        //    PdfPCell pdfcell;// As PdfPCell
        //    DataRow dr;
        //    amountInword = "";
        //    try
        //    {
                  
        //        CultureInfo CInfo = Thread.CurrentThread.CurrentCulture;
        //        TextInfo TInfo = CInfo.TextInfo;
                 
        //        pdfname = HostingEnvironment.MapPath("~//PDF_Files//quatation_" + projectID + ".pdf");

        //        if (System.IO.File.Exists(pdfname))
        //        {
        //            // code for delete file

        //            var dir = new DirectoryInfo(HostingEnvironment.MapPath("~//PDF_Files"));
        //            try
        //            {
        //                foreach (var file in dir.GetFiles())
        //                {
        //                    try
        //                    {
        //                        file.Delete();
        //                    }
        //                    catch (IOException ex)
        //                    {

        //                    }
        //                }
        //            }
        //            catch (Exception ex) { }
        //        }
        //        PdfWriter.GetInstance(doc, new FileStream(pdfname, FileMode.Create));
        //        doc.Open();
        //        para1.Alignment = Element.ALIGN_LEFT;
        //        para1.SetLeading(0.0F, 1.0F);


        //        para1.Alignment = Element.ALIGN_LEFT;
        //        para1.SetLeading(0.0F, 1.0F);

        //        ph1 = new Phrase(Environment.NewLine);
        //        para1.Add(ph1);

        //        pdfcell = null;
        //        table1 = new PdfPTable(2);
        //        int[] cellWidthPercentage = new int[] { 50, 50 };
        //        table1.WidthPercentage = 100;
        //        table1.HorizontalAlignment = Element.ALIGN_CENTER;

        //        table1.SetWidths(cellWidthPercentage);

        //        fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.Border = 0;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("301-H, Pushparatna solintare", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        pdfcell.Border = 0;
        //        table1.AddCell(pdfcell);


        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        pdfcell.Border = 0;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("New Palasia, AB Road, Indore - 452001 MP", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        pdfcell.Border = 0;
        //        table1.AddCell(pdfcell);



        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        pdfcell.Border = 0;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Tel: +91-731-4977407 , Cell : +91-96919-06670", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        pdfcell.Border = 0;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        pdfcell.Border = 0;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk(" Email : design.satyam@gmail.com ", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        pdfcell.Border = 0;
        //        table1.AddCell(pdfcell);

        //        para1.Add(table1);


        //        table1 = new PdfPTable(2);
        //        int[] cellWidthPercentage2 = new int[] { 70, 30 };
        //        table1.WidthPercentage = 100;

        //        table1.SetWidths(cellWidthPercentage2);

        //        pdfcell = null;
        //        fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                

        //        pdfcell = new PdfPCell(new Phrase(new Chunk(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(qut.clientName.ToLower()), fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
        //        //pdfcell.Border = 1;
        //        pdfcell.BorderWidthLeft = 0;
        //        pdfcell.BorderWidthRight = 0;
        //        pdfcell.BorderWidthBottom = 0;

        //        table1.AddCell(pdfcell);

        //        fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Date " + qut.dt.Day + "-" + qut.dt.Month + "-" + qut.dt.Year, fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        //pdfcell.Border = 1;
        //        pdfcell.BorderWidthLeft = 0;
        //        pdfcell.BorderWidthRight = 0;
        //        pdfcell.BorderWidthBottom = 0;

        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk(qut.city, fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
        //        pdfcell.Border = 0;

        //        table1.AddCell(pdfcell);
        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
        //        pdfcell.Border = 0;

        //        table1.AddCell(pdfcell);
        //        para1.Add(table1);

        //        ph1 = new Phrase(Environment.NewLine);
        //        para1.Add(ph1);





        //        //table1 = new PdfPTable(2);
        //        //int[] cellWidthPercentage2 = new int[] { 70, 30 };
        //        //table1.WidthPercentage = 100;

        //        //table1.SetWidths(cellWidthPercentage2);

        //        //pdfcell = null;
        //        //fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //        //pdfcell = new PdfPCell(new Phrase(new Chunk(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(qut.clientname.ToLower()), fnt)));
        //        //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
        //        ////pdfcell.Border = 1;
        //        //pdfcell.BorderWidthLeft = 0;
        //        //pdfcell.BorderWidthRight = 0;
        //        //pdfcell.BorderWidthBottom = 0;

        //        //table1.AddCell(pdfcell);

        //        //fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //        //pdfcell = new PdfPCell(new Phrase(new Chunk("Date " + qut.dt.Day + "-" + qut.dt.Month + "-" + qut.dt.Year, fnt)));
        //        //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        ////pdfcell.Border = 1;
        //        //pdfcell.BorderWidthLeft = 0;
        //        //pdfcell.BorderWidthRight = 0;
        //        //pdfcell.BorderWidthBottom = 0;

        //        //table1.AddCell(pdfcell);

        //        //pdfcell = new PdfPCell(new Phrase(new Chunk(qut.city, fnt)));
        //        //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
        //        //pdfcell.Border = 0;

        //        //table1.AddCell(pdfcell);



        //        //pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        //pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
        //        //pdfcell.Border = 0;

        //        //table1.AddCell(pdfcell);

        //        //para1.Add(table1);








        //        //pdfcell = null;
        //        //fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //        //ph1 = new Phrase(Environment.NewLine + Environment.NewLine + Environment.NewLine + "                   Date : " + DateTime.Today.Date.ToString("dd/MM/yyyy"), fnt);
        //        //para1.Add(ph1);

        //        //fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        //        //ph1 = new Phrase(Environment.NewLine + "                   Quotation ", fnt);
        //        //para1.Add(ph1);

        //        //fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        //        //ph1 = new Phrase(Environment.NewLine + Environment.NewLine + "                   To " + Environment.NewLine + "                           Dear " + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(qut.clientName.ToLower()), fnt);
        //        //para1.Add(ph1);

        //        //ph1 = new Phrase(Environment.NewLine + "                           " + citystate, fnt);
        //        //para1.Add(ph1);
                 
        //        ph1 = new Phrase(Environment.NewLine + "                                    I am pleased to offer you most competitive proposal. I am certain you will find  the information in line,", fnt);
        //        para1.Add(ph1);
        //        ph1 = new Phrase(Environment.NewLine + "                       apt to your needs. The proposal covers our amount." + Environment.NewLine, fnt);
        //        para1.Add(ph1);


        //        table1 = new PdfPTable(3);
        //        int[] cellWidthPercentage5 = new int[] { 11,30, 25 };
        //        table1.WidthPercentage = 60;
        //        table1.HorizontalAlignment = 0;
        //        table1.SetWidths(cellWidthPercentage5);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.BorderColor = BaseColor.WHITE;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Request Date ", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk(reqDate.ToString("dd/MM/yyyy"), fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.BorderColor = BaseColor.WHITE;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Project ID", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk(projectID.ToString(), fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.BorderColor = BaseColor.WHITE;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Project Name", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk(qut.projectName, fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.BorderColor = BaseColor.WHITE;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Plot Size", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk(qut.plotSize, fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.BorderColor = BaseColor.WHITE;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Level", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk(qut.projectLevel, fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.BorderColor = BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        para1.Add(table1);

        //        ph1 = new Phrase(Environment.NewLine );
        //        para1.Add(ph1);


        //        table1 = new PdfPTable(4);
        //        cellWidthPercentage = new int[] { 26, 26, 15, 12 };
        //        table1.WidthPercentage = 82;
        //        table1.HorizontalAlignment = Element.ALIGN_CENTER;
        //        table1.SetWidths(cellWidthPercentage);

        //        pdfcell = null;
        //        fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Project Details", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Particular", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Unit/Size", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Amount", fnt)));
        //        pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        table1.AddCell(pdfcell);


        //        fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        //        for (i = 0; i < dt.Rows.Count; i++)
        //        {
        //            dr = dt.Rows[i];
        //            pdfcell = new PdfPCell(new Phrase(new Chunk("  " + dr["projectDetails"].ToString(), fnt)));
        //            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //            table1.AddCell(pdfcell);

        //            pdfcell = new PdfPCell(new Phrase(new Chunk("  " + dr["particular"].ToString(), fnt)));
        //            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //            table1.AddCell(pdfcell);

        //            pdfcell = new PdfPCell(new Phrase(new Chunk("  " + dr["unit"].ToString(), fnt)));
        //            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //            table1.AddCell(pdfcell);

        //            pdfcell = new PdfPCell(new Phrase(new Chunk(dr["amount"].ToString(), fnt)));
        //            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        //            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //            table1.AddCell(pdfcell);

        //            amount += int.Parse(dr["amount"].ToString());
        //        }

        //        fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.BorderColorRight = iTextSharp.text.BaseColor.GRAY;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
        //        pdfcell.BorderColorLeft = iTextSharp.text.BaseColor.WHITE;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk("Total", fnt)));
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        table1.AddCell(pdfcell);

        //        pdfcell = new PdfPCell(new Phrase(new Chunk(amount.ToString(), fnt)));
        //        pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //        table1.AddCell(pdfcell);

        //        para1.Add(table1);

        //        amountInword = objNTW.ConvertWholeNumber(amount.ToString());

        //        fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        //        ph1 = new Phrase(String.Format(Environment.NewLine + "                         In words :  " + amountInword + " Rs. Only"), fnt);
        //        para1.Add(ph1);

        //        ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + "                         Thank you for the opportunity to serve you. We look forward for further communication with you,"), fnt);
        //        para1.Add(ph1);


        //        ph1 = new Phrase(String.Format(Environment.NewLine + "                         after you have reviewed the proposal. "), fnt);
        //        para1.Add(ph1);


        //        ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "                         For  SATYAM DESIGN"), fnt);
        //        para1.Add(ph1);


        //        ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + "                         Satyam Design "), fnt);
        //        para1.Add(ph1);


        //        ph1 = new Phrase(String.Format(Environment.NewLine + "                         Authorized signatory"), fnt);
        //        para1.Add(ph1);

        //        fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);
        //        ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "                                                                     Please note that this is a system generated mail and does not require signature."), fnt);
        //        para1.Add(ph1);



        //        //fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
        //        //ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "                                                                    301-H, Pushparatna solintare , New Palasia, AB Road, Indore - 452001  MP, INDIA"), fnt);
        //        //para1.Add(ph1);

        //        //ph1 = new Phrase(String.Format(Environment.NewLine + "                                                                                                     Tel . : +91-731-4977407 , Cell : +91-96919-06670"), fnt);
        //        //para1.Add(ph1);

        //        //ph1 = new Phrase(String.Format(Environment.NewLine + "                                                                                                                 Email : design.satyam@gmail.com"), fnt);
        //        //para1.Add(ph1);
                 
        //        iTextSharp.text.Image jpg;
        //        jpg = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~//Images//Logo.png"));

        //        jpg.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
        //        jpg.ScaleToFit(90.0F, 90.0F);
        //        jpg.SetAbsolutePosition(doc.PageSize.Width - 560, 735.0F);
        //        doc.Add(jpg);

        //        jpg = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~//Images//logoname.jpeg"));

        //        jpg.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
        //        jpg.ScaleToFit(100.0F, 100.0F);
        //        jpg.SetAbsolutePosition(doc.PageSize.Width - 143, 783.0F);
        //        doc.Add(jpg);


        //        doc.Add(para1);
        //        doc.Close();
        //        doc.Dispose();
        //        doc = null;
                 
        //    }
        //    catch (Exception ex)
        //    {
                 
        //    }

        //    return pdfname;
        //}


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["user"] = "";
            Session["username"] = "";
            return RedirectToAction("Index", "Login");
        }
        //private static String ConvertWholeNumber(String Number)
        //{
        //    string word = "";
        //    try
        //    {
        //        bool beginsZero = false;//tests for 0XX
        //        bool isDone = false;//test if already translated
        //        double dblAmt = (Convert.ToDouble(Number));
        //        //if ((dblAmt > 0) && number.StartsWith("0"))
        //        if (dblAmt > 0)
        //        {//test for zero or digit zero in a nuemric
        //            beginsZero = Number.StartsWith("0");

        //            int numDigits = Number.Length;
        //            int pos = 0;//store digit grouping
        //            String place = "";//digit grouping name:hundres,thousand,etc...
        //            switch (numDigits)
        //            {
        //                case 1://ones' range

        //                    word = ones(Number);
        //                    isDone = true;
        //                    break;
        //                case 2://tens' range
        //                    word = tens(Number);
        //                    isDone = true;
        //                    break;
        //                case 3://hundreds' range
        //                    pos = (numDigits % 3) + 1;
        //                    place = " Hundred ";
        //                    break;
        //                case 4://thousands' range
        //                case 5:
        //                case 6:
        //                    pos = (numDigits % 4) + 1;
        //                    place = " Thousand ";
        //                    break;
        //                case 7://millions' range
        //                case 8:
        //                case 9:
        //                    pos = (numDigits % 7) + 1;
        //                    place = " Million ";
        //                    break;
        //                case 10://Billions's range
        //                case 11:
        //                case 12:

        //                    pos = (numDigits % 10) + 1;
        //                    place = " Billion ";
        //                    break;
        //                //add extra case options for anything above Billion...
        //                default:
        //                    isDone = true;
        //                    break;
        //            }
        //            if (!isDone)
        //            {//if transalation is not done, continue...(Recursion comes in now!!)
        //                if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
        //                {
        //                    try
        //                    {
        //                        word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
        //                    }
        //                    catch { }
        //                }
        //                else
        //                {
        //                    word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
        //                }

        //                //check for trailing zeros
        //                //if (beginsZero) word = " and " + word.Trim();
        //            }
        //            //ignore digit grouping names
        //            if (word.Trim().Equals(place.Trim())) word = "";
        //        }
        //    }
        //    catch { }
        //    return word.Trim();
        //}
        //private static String tens(String Number)
        //{
        //    int _Number = Convert.ToInt32(Number);
        //    String name = null;
        //    switch (_Number)
        //    {
        //        case 10:
        //            name = "Ten";
        //            break;
        //        case 11:
        //            name = "Eleven";
        //            break;
        //        case 12:
        //            name = "Twelve";
        //            break;
        //        case 13:
        //            name = "Thirteen";
        //            break;
        //        case 14:
        //            name = "Fourteen";
        //            break;
        //        case 15:
        //            name = "Fifteen";
        //            break;
        //        case 16:
        //            name = "Sixteen";
        //            break;
        //        case 17:
        //            name = "Seventeen";
        //            break;
        //        case 18:
        //            name = "Eighteen";
        //            break;
        //        case 19:
        //            name = "Nineteen";
        //            break;
        //        case 20:
        //            name = "Twenty";
        //            break;
        //        case 30:
        //            name = "Thirty";
        //            break;
        //        case 40:
        //            name = "Fourty";
        //            break;
        //        case 50:
        //            name = "Fifty";
        //            break;
        //        case 60:
        //            name = "Sixty";
        //            break;
        //        case 70:
        //            name = "Seventy";
        //            break;
        //        case 80:
        //            name = "Eighty";
        //            break;
        //        case 90:
        //            name = "Ninety";
        //            break;
        //        default:
        //            if (_Number > 0)
        //            {
        //                name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
        //            }
        //            break;
        //    }
        //    return name;
        //}
        //private static String ones(String Number)
        //{
        //    int _Number = Convert.ToInt32(Number);
        //    String name = "";
        //    switch (_Number)
        //    {

        //        case 1:
        //            name = "One";
        //            break;
        //        case 2:
        //            name = "Two";
        //            break;
        //        case 3:
        //            name = "Three";
        //            break;
        //        case 4:
        //            name = "Four";
        //            break;
        //        case 5:
        //            name = "Five";
        //            break;
        //        case 6:
        //            name = "Six";
        //            break;
        //        case 7:
        //            name = "Seven";
        //            break;
        //        case 8:
        //            name = "Eight";
        //            break;
        //        case 9:
        //            name = "Nine";
        //            break;
        //    }
        //    return name;
        //}


 
        public ActionResult ShowClient()
        {

            return View();
        }
       
         
        public ActionResult EditDesigner(string id)
        {
            ViewBag.message = "";
            return View();
        }

        public ActionResult ReportClientLadger()
        {

            return View();
        }

        public ActionResult Test()
        {

            return View();
        }
        //public ActionResult Dashboard()
        //{
        //    clientModel obj = new clientModel();
        //    return View(obj);
        //}
    }
}
