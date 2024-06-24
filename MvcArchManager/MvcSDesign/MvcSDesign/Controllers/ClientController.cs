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

        //public string SendPromoMail( string title, string msg, string[] clientList)
        //{
        //    string ch = "";
        //    try
        //    {
        //        msg = msg.Replace("\n", "<br />");
        //        //ch =  IClnt.SendPromoMail(title, msg, clientList);
        //    }
        //    catch (Exception ex) { }
        //    return ch;
           
        //} 
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


        public JsonResult ShowQuotationPdf(string quoID)
        {
            string pth = "";
            try
            {
                operation prj = IClnt.GetQuotationDetail(int.Parse(quoID));
                quotation obj1 = new quotation();
                pth = CreateQuotationPDF(prj , ref pth);
                pth = Path.GetFileName(pth);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return Json(pth, JsonRequestBehavior.AllowGet);
        }

      


        public JsonResult SendClientQuotation(string pid, string projectType)
        {
            string ch = "";
            try
            {
               // var prj = IClnt.getProjectDetail(int.Parse(pid));
                if (projectType == "Interior")
                {
                    operation obj1 = new operation();
                    //foreach (var item in prj)
                    //{
                    //    obj1.clientID = item.clientID;
                    //    obj1.clientName = item.clientName;
                    //    obj1.emailID = item.status;
                    //    obj1.package = item.package;
                    //    obj1.projectLevel = item.projectLevel;
                    //    obj1.projectType = item.projectType;
                    //    obj1.plotSize = item.plotSize;
                    //    obj1.amount = item.amount;
                    //    obj1.emailID = item.status;
                    //    obj1.projectName = item.projectName;
                    //    obj1.city = item.rowcolor;
                    //    obj1.dt = item.dt;
                    //}
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

                    //foreach (var item in prj)
                    //{
                    //    obj.clientID = item.clientID.ToString();
                    //    obj.clientname = item.clientName;
                    //    obj.emailID = item.status;
                    //    obj.package = item.package;
                    //    obj.projectLevel = item.projectLevel;
                    //    obj.projectType = item.projectType;
                    //    obj.plotSize = item.plotSize;
                    //    obj.amount = item.amount;
                    //    obj.emailID = item.status;
                    //    obj.projectname = item.projectName;
                    //    obj.address = item.designerName;
                    //    obj.city = item.rowcolor;
                    //    obj.dt = item.dt;
                    //    obj.finalizeAmount = 0;
                    //}
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
            Session["quotationItem"] = null;
            return View();
        }

        public JsonResult Quotation_AddItem(QuotationItemModel objItem)
        {

            if (Session["quotationItem"] == null)
            {
                List<QuotationItemModel> li = new List<QuotationItemModel>();
                QuotationItemModel obj = new QuotationItemModel();
                 
                obj.projectDetails = objItem.projectDetails.Trim();
                obj.services = objItem.services;
                obj.hsn = objItem.hsn;
                obj.qty = objItem.qty;
                obj.unit = objItem.unit;
                obj.rate = objItem.rate;
                obj.amount = objItem.qty * objItem.rate;
                
                li.Add(obj);
                Session["quotationItem"] = li;
                return Json(li, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<QuotationItemModel> li = (List<QuotationItemModel>)Session["quotationItem"];
                var itm = li.Where(x => ((x.projectDetails == objItem.projectDetails) && (x.services == objItem.services))).FirstOrDefault();
                if (itm == null)
                {

                    QuotationItemModel obj = new QuotationItemModel();
                    obj.projectDetails = objItem.projectDetails.Trim();
                    obj.services = objItem.services;
                    obj.hsn = objItem.hsn;
                    obj.qty = objItem.qty;
                    obj.unit = objItem.unit;
                    obj.rate = objItem.rate;
                    obj.amount = objItem.qty * objItem.rate;
                    li.Add(obj);
                    Session["quotationItem"] = li;
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Quotation_RemoveItem(string  prjDetail, string service)
        {
            List<QuotationItemModel> li = (List<QuotationItemModel>)Session["quotationItem"];
           var itm = li.Where(x => ((x.projectDetails == prjDetail) && (x.services == service))).FirstOrDefault();
            if (itm != null)
            {
                li.Remove(itm);
            }
            return Json(li, JsonRequestBehavior.AllowGet);
        }
           

        public string SaveQuotation(string cid, string projectname, string service, string remark)
        {
            operation obj = new operation();
            List<QuotationItemModel> li = (List<QuotationItemModel>)Session["quotationItem"];
            int quotationNo = 0;
            obj.clientID = int.Parse(cid);
            obj.projectName = projectname.Trim();
            obj.service = service.Trim();
            obj.remark =remark == null?"-": remark.Trim();

            string ch = IClnt.SaveQuotation(obj, li, ref quotationNo);

            if(ch =="") ch  = " Quotation successfully generated , quotation No is " + quotationNo;
            return ch;
        }


        public ActionResult RevisedQuotation()
        {
            try
            {
                
                Session["revQuotation"] = null;
            }
            catch (Exception ex)
            {
                //ViewBag.Message = "";

            }
            return View();
        }

        public JsonResult GetRevisedQuotation(string qID)
        {

            var res = IClnt.GetQuotationDetailItem(int.Parse(qID));
            List<QuotationItemModel> li = new List<QuotationItemModel>();
            QuotationItemModel obj ;

            foreach(var item in res)
             { 
                obj = new QuotationItemModel();
                obj.projectDetails = item.projectDetails;
                obj.services = item.services;
                obj.hsn = item.hsn;
                obj.qty = item.qty;
                obj.unit = item.unit;
                obj.rate = item.rate;
                obj.amount = item.qty * item.rate;
                
                li.Add(obj);
             }
            Session["revQuotation"] = li;
            return Json(li, JsonRequestBehavior.AllowGet);

            
        }
        public JsonResult UpdateQuotation(string quotationID, string projectDetail, string service)
        {
            List<QuotationItemModel> li = (List<QuotationItemModel>)Session["revQuotation"];
            operation obj = new operation();
           
            obj.quotationID = int.Parse(quotationID);
            obj.projectName = projectDetail.Trim();
            obj.service = service.Trim();

            return Json(IClnt.UpdateQuotation(obj, li), JsonRequestBehavior.AllowGet);
        }



        public JsonResult addProjectItem(string projectDetail, string services, string hsn, string qty , string unit,string rate)
        {
         
            if (Session["revQuotation"] == null)
            {
                List<QuotationItemModel> li = new List<QuotationItemModel>();
                QuotationItemModel obj = new QuotationItemModel();
                obj.projectDetails = projectDetail;
                obj.services = services;
                obj.hsn = hsn;
                obj.qty = int.Parse(qty);
                obj.unit = unit == null ?"-":unit; 
                obj.rate = int.Parse(rate);
                obj.amount = int.Parse(qty) * int.Parse(rate);
                li.Add(obj);
                Session["revQuotation"] = li;
                return Json(li, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<QuotationItemModel> li = (List<QuotationItemModel>)Session["revQuotation"];
                QuotationItemModel obj = new QuotationItemModel();
                var pdt = li.Where(x => ((x.projectDetails == projectDetail) && (x.services == services))).FirstOrDefault();
                if (pdt == null)
                {
                     obj.projectDetails = projectDetail;
                     obj.services = services;
                     obj.hsn = hsn;
                     obj.qty = int.Parse(qty);
                     obj.unit =  unit == null ?"-":unit; 
                     obj.rate = int.Parse(rate);
                     obj.amount = int.Parse(qty) * int.Parse(rate);
                     li.Add(obj);
                     Session["revQuotation"] = li;
                }
                else
                 {
                     obj.projectDetails = projectDetail;
                     pdt.services = services;
                     pdt.hsn = hsn;
                     pdt.qty = int.Parse(qty);
                     pdt.unit =  unit == null ?"-":unit; 
                     pdt.rate = int.Parse(rate);
                     pdt.amount = int.Parse(qty) * int.Parse(rate);
                     Session["revQuotation"] = li;
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

            
        }

        public JsonResult removeRevQuotationItem(string projectDetail, string service)
        {
            List<QuotationItemModel> li = (List<QuotationItemModel>)Session["revQuotation"];
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


                pdfname ="";// CreateQuotationPDF(qut, projectID, ref amountInWord);



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
         
        public string CreateQuotationPDF(operation qut, ref string amountInWord)
        {
            Paragraph para1 = new Paragraph();
            Phrase ph1 = new Phrase();
            iTextSharp.text.Font fnt = new iTextSharp.text.Font();

            PdfPTable table1 = new PdfPTable(2);
            PdfPCell pdfcell;// As PdfPCell
            string logofile = "";
            amountInWord = ""; 
            string pdfname = HostingEnvironment.MapPath("~//PDF_Files//quatation_" + qut.quotationNo + ".pdf");
            var resProfile = IAdn.GetCompanyProfile();

             
            if (resProfile ==null)
            {
                return "";
            }

            var settingRes = IAdn.GetAdminSetting();
           
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

            MemoryStream ms = new MemoryStream();


            Document doc = new Document();
            PdfWriter pw = PdfWriter.GetInstance(doc, new FileStream(pdfname, FileMode.Create));
            pw.PageEvent = new HeaderFooter();

            doc.Open();
             
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

            
            ph1 = new Phrase(Environment.NewLine);
            para1.Add(ph1);


            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            ph1 = new Phrase(new Phrase(new Chunk(  "To : " + qut.clientName.ToUpper() +" " + qut.city, fnt)));
            para1.Add(ph1);
            ph1 = new Phrase(Environment.NewLine + Environment.NewLine);
            para1.Add(ph1);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            ph1 = new Phrase(new Phrase(new Chunk("Quotation No : " + settingRes.prefixQuotationID +" " + qut.quotationNo  , fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(Environment.NewLine + Environment.NewLine );
            para1.Add(ph1);
           
            ph1 = new Phrase(new Phrase(new Chunk("Dear Sir, ", fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(Environment.NewLine + Environment.NewLine);
            para1.Add(ph1);

            ph1 = new Phrase(new Phrase(new Chunk("We are pleased to offer you firm consultancy for architectural and allied services for the following project at fees stated below: ", fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(Environment.NewLine+ Environment.NewLine + Environment.NewLine);
            para1.Add(ph1);

            ph1 = new Phrase(new Phrase(new Chunk("Project : "+ qut.projectName, fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(Environment.NewLine  );
            para1.Add(ph1);

            ph1 = new Phrase(new Phrase(new Chunk("Services :  " + qut.service, fnt)));
            para1.Add(ph1);


            ph1 = new Phrase(Environment.NewLine );
            para1.Add(ph1);
 


            table1 = new PdfPTable(8);
            int[] cellWidthPercentage1 = new int[] {5, 22, 22, 12, 10, 10,10,10 };
            table1.WidthPercentage = 100;
            table1.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.SetWidths(cellWidthPercentage1);
            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            pdfcell = null;
            pdfcell = new PdfPCell(new Phrase(new Chunk("Sno", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthLeft = 1;
            pdfcell.BorderWidthRight = 1;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            pdfcell.FixedHeight = 20;
            pdfcell.BackgroundColor = new BaseColor(229, 225, 225);
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Project Description", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
          //  pdfcell.FixedHeight = 20;
            pdfcell.BackgroundColor = new BaseColor(229, 225, 225);
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Services", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            pdfcell.BackgroundColor = new BaseColor(229, 225, 225);
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("HSN", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            pdfcell.BackgroundColor =new BaseColor(229, 225, 225);
            table1.AddCell(pdfcell);


            pdfcell = new PdfPCell(new Phrase(new Chunk("Qty ", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            pdfcell.BackgroundColor = new BaseColor(229, 225, 225);
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("Unit ", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            pdfcell.BackgroundColor = new BaseColor(229, 225, 225);
            table1.AddCell(pdfcell);


            pdfcell = new PdfPCell(new Phrase(new Chunk("Rate ", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            pdfcell.BackgroundColor =new BaseColor(229, 225, 225);
            table1.AddCell(pdfcell);

            
            pdfcell = new PdfPCell(new Phrase(new Chunk("Amount", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            pdfcell.BackgroundColor = new BaseColor(229, 225, 225);
            table1.AddCell(pdfcell);

            var resItem = IClnt.GetQuotationDetailItem(qut.quotationNo);
            int sno =1;

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            foreach (var item in resItem)
            {

                pdfcell = new PdfPCell(new Phrase(new Chunk(sno.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.BorderWidthBottom = 1;
                pdfcell.FixedHeight = 20;
                table1.AddCell(pdfcell);
                 

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.projectDetails, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.BorderWidthBottom = 1;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.services, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.BorderWidthBottom = 1;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.hsn, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.BorderWidthBottom = 1;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.qty.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.BorderWidthBottom = 1;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.unit, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.BorderWidthBottom = 1;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(item.rate.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                pdfcell.BorderWidthBottom = 1;
                table1.AddCell(pdfcell);


                pdfcell = new PdfPCell(new Phrase(new Chunk(item.amount.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                pdfcell.BorderWidthBottom = 1;
                table1.AddCell(pdfcell);

                sno++;

            }

            // Net Total

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            //pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            pdfcell.FixedHeight = 20;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthBottom = 1;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            // pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            //  pdfcell.FixedHeight = 20;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            // pdfcell.BorderWidthTop = 1;
            pdfcell.BorderWidthBottom = 1;
            //    pdfcell.FixedHeight = 20;
            table1.AddCell(pdfcell); 

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthBottom = 1;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk("", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfcell.BorderWidthBottom = 1;
            table1.AddCell(pdfcell);

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            pdfcell = new PdfPCell(new Phrase(new Chunk("Total", fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.BorderWidthBottom = 1;
            table1.AddCell(pdfcell);

            pdfcell = new PdfPCell(new Phrase(new Chunk(qut.amount.ToString(), fnt)));
            pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            pdfcell.BorderWidthBottom = 1;
            table1.AddCell(pdfcell); 

            para1.Add(table1); 

            fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            ph1 = new Phrase(new Phrase(new Chunk(qut.remark, fnt)));
            para1.Add(ph1);

            ph1 = new Phrase(String.Format(  Environment.NewLine+  Environment.NewLine + " Govt. taxes shall be paid separately."), fnt);
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
         

            //fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC, BaseColor.BLUE);
            //ph1 = new Phrase(String.Format(  Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "                                              G-2, Nursery Circle, Vaishali Nagar, Jaipur - 302 021. Rajasthan (INDIA) "), fnt);
            //para1.Add(ph1);


            ////fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC, BaseColor.BLUE);
            ////ph1 = new Phrase(String.Format(Environment.NewLine  +"                            Cont: +91 88888 59911, E - mail : vrarchitects.in @gmail.com, website: www.vrarchitects.in "), fnt);
            ////para1.Add(ph1);

             

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



        class HeaderFooter  : PdfPageEventHelper
        {
            iTextSharp.text.Font fnt;// = new iTextSharp.text.Font();
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC, BaseColor.BLUE);
        
                PdfPTable tbFooter = new PdfPTable(1);
                tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbFooter.DefaultCell.Border =0;

                tbFooter.AddCell(new Paragraph());
                tbFooter.AddCell(new Paragraph());
                PdfPCell _cell = new PdfPCell(new Phrase(new Chunk("G-2, Nursery Circle, Vaishali Nagar, Jaipur - 302 021. Rajasthan (INDIA)", fnt)));
                _cell.HorizontalAlignment =Element.ALIGN_CENTER;
                _cell.Border =0;
                tbFooter.AddCell(_cell);
                tbFooter.AddCell(new Paragraph());

                _cell = new PdfPCell(new Phrase(new Chunk("Cont: +91 88888 59911, E - mail : vrarchitects.in @gmail.com, website: www.vrarchitects.in", fnt)));
                _cell.HorizontalAlignment =Element.ALIGN_CENTER;
                _cell.Border =0;
                tbFooter.AddCell(_cell);
                tbFooter.AddCell(new Paragraph());
                 
                tbFooter.WriteSelectedRows(0,-1,document.LeftMargin + 10, writer.PageSize.GetBottom(document.BottomMargin) +10, writer.DirectContent);

            }



        }

  
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["user"] = "";
            Session["username"] = "";
            return RedirectToAction("Index", "Login");
        } 
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
        
    }
}
