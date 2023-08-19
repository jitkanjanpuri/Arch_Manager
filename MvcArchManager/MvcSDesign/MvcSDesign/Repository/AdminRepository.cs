using iTextSharp.text;
using iTextSharp.text.pdf;
using MvcSDesign.EF;
using MvcSDesign.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Security.Cryptography;
using static iTextSharp.text.pdf.AcroFields;

namespace MvcSDesign.Repository
{
    public class AdminRepository :IAdmin
    {
        ConvertNumberToWord objNTW = new ConvertNumberToWord();
        ArchManagerDBEntities _dbContext;
        CultureInfo cinfo = new CultureInfo("en-US");
        public AdminRepository(ArchManagerDBEntities sd)
        {
            _dbContext = sd;
        }

        public bool DesignerNameValidation(string name)
        {
            bool flag = true;
            try
            {
                tblStaff obj = _dbContext.tblStaffs.FirstOrDefault(x => x.name == name);
                string ch = obj.name;
                flag = false;
            }
            catch (Exception ex) { }
            return flag;
        }

        public string SaveCompanyProfile(CompanyModel model)
        {
            try
            {
                var rec = _dbContext.tblCompanyProfiles.FirstOrDefault();
                if (rec == null)
                {
                    tblCompanyProfile obj = new tblCompanyProfile();
                    obj.name = model.name.Trim();
                    obj.orgName = model.orgName.Trim();
                    obj.gstNo = model.gstNo.Trim();
                    obj.address = model.address.Trim();
                    obj.city = model.city.Trim();
                    obj.state = model.state.Trim();
                    obj.pincode = model.pincode;
                    obj.mobileno = model.mobile.Trim();
                    obj.phoneno = model.phone == null ? "-" : model.phone;
                    obj.emailID = model.emailID.Trim();
                    obj.pwd = model.password;
                    _dbContext.tblCompanyProfiles.Add(obj);
                }
                else
                {
                    rec.name = model.name.Trim();
                    rec.orgName = model.orgName.Trim();
                    rec.gstNo = model.gstNo.Trim();
                    rec.address = model.address.Trim();
                    rec.city = model.city.Trim();
                    rec.state = model.state.Trim();
                    rec.pincode = model.pincode;
                    rec.mobileno = model.mobile.Trim();
                    rec.phoneno = model.phone == null ? "-" : model.phone;
                    rec.emailID = model.emailID.Trim();
                    rec.pwd = model.password;
                }
                _dbContext.SaveChanges();


            }
            catch (Exception ex) { }
            return "";
        }

        public CompanyModel GetCompanyProfile()
        {
            CompanyModel obj = new CompanyModel();
            try
            {
                var rec = _dbContext.tblCompanyProfiles.FirstOrDefault();//.Where(st => (st.username == lgn.username) && (st.password == lgn.pwd)).FirstOrDefault();
                if (rec == null)
                    return null;

                obj.name = rec.name.Trim();
                obj.orgName = rec.orgName.Trim();
                obj.gstNo = rec.gstNo.Trim();
                obj.address = rec.address.Trim();
                obj.city = rec.city.Trim();
                obj.state = rec.state.Trim();
                obj.pincode = rec.pincode;
                obj.mobile = rec.mobileno.Trim();
                obj.phone = rec.phoneno.Trim();
                obj.emailID = rec.emailID.Trim();

            }
            catch (Exception ex) { }
            return obj;
        }

       
        public bool DesignerEmailValidation(string mailID)
        {
            bool flag = true;
            try
            {
                tblStaff obj = _dbContext.tblStaffs.SingleOrDefault(x => x.emailID == mailID);
                string ch = obj.name;
                flag = false;
            }
            catch (Exception ex) { }
            return flag;
        }


        public string InsertRegistration(StaffModel st)
        {
            try
            {
                if (st.staffID == 0)
                {
                    var res = _dbContext.tblStaffs.Where(x => x.name == st.name.Trim()).FirstOrDefault();
                    if (res != null)
                    {
                        return "Staff name already exist";
                    }
                    var res1 = _dbContext.tblStaffs.Where(x => x.username == st.username.Trim()).FirstOrDefault();
                    if (res != null)
                    {
                        return "User name is already exist";
                    }
                    tblStaff tsf = new tblStaff();
                    tsf.name = st.name;
                    tsf.designation = st.designation;
                    tsf.address = st.address;
                    tsf.city = st.city;
                    tsf.phone = st.phone;
                    tsf.mobile = st.mobile;
                    tsf.emailID = st.emailID;
                    tsf.username = st.username;
                    tsf.password = st.password;
                    tsf.active = true;
                    tsf.rolltype = st.rolltype;
                    _dbContext.tblStaffs.Add(tsf);
                }
                else
                {
                    var obj = _dbContext.tblStaffs.Where(x => x.staffID == st.staffID).FirstOrDefault();
                    
                    obj.name = st.name.Trim();
                    obj.designation = (st.designation == null) ? "-" : st.designation.Trim();
                    obj.address = (st.address == null) ? "-" : st.address.Trim();
                    obj.city = st.city.Trim();
                    obj.phone = (st.phone == null) ? "-" : st.phone.Trim();
                    obj.mobile = st.mobile.Trim();
                    obj.rolltype = st.rolltype;
                    obj.emailID = st.emailID.Trim();
                    obj.username = st.username;
                    obj.password = st.password;
                   // _dbContext.Entry(st).State = System.Data.Entity.EntityState.Modified;
                }
                _dbContext.SaveChanges();
                return "";
            }
            catch (Exception ex) { return ex.Message; }
        }

        public IEnumerable<StaffModel> SearchRegistration(string name)
        {
            List<StaffModel> lst = new List<StaffModel>();
            try
            {
                var res = ( from item in _dbContext.tblStaffs
                            where item.name.Contains(name)
                            select new StaffModel
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
                               password = item.password,
                               rolltype = item.rolltype,
                               active =(bool) item.active ,
                           }).ToList();
                lst = res.ToList();
            }
            catch (Exception ex)
            { }
             
            return lst;
        }


        public IEnumerable<StaffModel> GatAllRegistration()
        {
            List<StaffModel> lst = new List<StaffModel>();
            try
            {
                var res = (from item in _dbContext.tblStaffs
                          
                           select new StaffModel
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
                               password = item.password,
                               rolltype = item.rolltype,
                               active = (bool)item.active,
                           }).Take(10).ToList();
                lst = res.ToList();
            }
            catch (Exception ex)
            { }

            return lst;
        }


        public StaffModel getLogin(logincls lgn)
        {
            StaffModel obj = new StaffModel();
            try
            {
                var rec = _dbContext.tblStaffs.Where(st => (st.username == lgn.username) && (st.password == lgn.pwd)).FirstOrDefault();
                if (rec == null)
                    return null;
                obj.name = rec.name;
                obj.rolltype = rec.rolltype;
                obj.designation = rec.designation;
                obj.staffID = rec.staffID;

            }
            catch (Exception ex) { }
            return obj;
        }

        public string RegistrationUpdate(StaffModel obj)
        {
            try
            {
                tblStaff st = _dbContext.tblStaffs.Find(obj.staffID);

                st.name = obj.name.Trim();
                st.designation = (obj.designation == null) ? "-" : obj.designation.Trim();
                st.address = (obj.address == null) ? "-" : obj.address.Trim();
                st.city = obj.city.Trim();
                st.phone = (obj.phone == null) ? "-" : obj.phone.Trim();
                st.mobile = obj.mobile.Trim();
                st.rolltype = "User";
                st.emailID = obj.emailID.Trim();
                st.username = st.username;
                st.password = obj.password;
                _dbContext.Entry(st).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();

            }
            catch (Exception ex) { return ex.Message; }

            return "";

        }

        //public string SaveGMail(string gmailID, string pwd)
        //{
        //    try
        //    {
        //        var rec = _dbcontext.tblGmailAccount.Where(x => x.gmailID == gmailID).FirstOrDefault();
        //        if(rec ==null)
        //        {
        //            tblGmailAccount obj = new tblGmailAccount();
        //            obj.gmailID = gmailID;
        //            obj.pwd = pwd;
        //            _dbcontext.tblGmailAccount.Add(obj);
        //            _dbcontext.SaveChanges();
        //        }
        //        else
        //        {
        //            return "This Gmail ID already exist";
        //        }
        //    }
        //    catch (Exception ex) { return ex.Message; }

        //    return "";
        //}
        //public List<GMail> getGmail()
        //{
        //    List<GMail> obj = new List<GMail>();
        //    try
        //    {
        //        var rec = (from cl in _dbcontext.tblGmailAccount
        //                   select new GMail
        //                   {
        //                       id = cl.id,
        //                       gmailID = cl.gmailID,
        //                       pwd = cl.pwd
        //                   }).ToList();
        //        obj = rec.ToList();
        //    }
        //    catch (Exception ex) { }

        //    return obj;
        //}

        //public string RemoveGMailAccount(int id)
        //{
        //    try
        //    {
        //        var rec = _dbcontext.tblGmailAccount.Where(x => x.id == id).FirstOrDefault();
        //        if (rec != null)
        //        {
        //            _dbcontext.tblGmailAccount.Remove(rec);
        //            _dbcontext.SaveChanges();
        //        }
        //        else
        //        {
        //            return "No record found";
        //        }
        //    }
        //    catch (Exception ex) { return ex.Message; }

        //    return "success";
        //}
        public string RegistrationDelete(int staffID)
       {
           try
           {
                var obj = _dbContext.tblStaffs.SingleOrDefault(x => x.staffID == staffID);
                _dbContext.tblStaffs.Remove(obj);
                _dbContext.SaveChanges();

            }
           catch (Exception ex) { return ex.Message; }
           return "";
       }
       public string UpdateQuotation(int pid, int famount,  string projectlocation)
       {
            using (var context = new ArchManagerDBEntities())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int cid, balance = 0;
                        tblProjectDetail pd = context.tblProjectDetails.Find(pid);
                        tblClientLedger obj = new tblClientLedger();
                        pd.dt = DateTime.Now;
                        pd.finalizeAmount = famount;
                        pd.status = "estimated";
                        pd.projectlocation = projectlocation;
                        cid = pd.clientID;


                        context.Entry(pd).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                        //Entry in tblProjectmanagement
                        tblProjectManagement pmObj = new tblProjectManagement();
                        pmObj.dt = DateTime.Today;
                        pmObj.projectID = pid;
                        pmObj.category = "Presentation Drawing";
                        pmObj.subcategory = "Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank";
                        pmObj.projectstatus = "Normal";
                        pmObj.status = "WRC";
                        pmObj.remark = "";
                        context.tblProjectManagements.Add(pmObj);
                        context.SaveChanges();


                        try
                        {
                            tblClientLedger cl = context.tblClientLedgers.Where(x => x.clientID == cid).OrderByDescending(x => x.clientLadgerID).First();
                            balance = cl.balance;
                        }
                        catch (Exception ex) { }


                        obj.dt = DateTime.Now;
                        obj.clientID = cid;
                        obj.projectID = pid;
                        obj.prjAmount = famount;
                        obj.receivedAmount = 0;
                        obj.balance = balance + famount;
                        obj.remark = "-";
                        context.tblClientLedgers.Add(obj);
                        context.SaveChanges();

                        dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                        return ex.Message;
                    }
                }
            }
            return "success";
       }

        public IEnumerable<operation> getProjectQuotation()
        {
            List<operation> reqlist = new List<operation>();
            try
            {

                string str = "request";
                var prj = from pd in _dbContext.tblProjectDetails
                          join cl in _dbContext.tblClients
                          on pd.clientID equals cl.clientID
                          where (pd.status == str)
                          select new
                          {
                              clientid = cl.clientID,
                              clientname = cl.clientName,
                              projectID = pd.projectID,
                              projectType = pd.projectType,
                              package = pd.package,
                              projectLevel = pd.projectLevel,
                              plotSize = pd.plotSize,
                              amount = pd.amount,
                              remark = pd.remark
                          };
                int i = 1;

                foreach (var item in prj)
                {
                    reqlist.Add(new operation
                    {
                        sno = i,
                        clientID = item.clientid,
                        projectID = item.projectID,
                        clientName = item.clientname,
                        projectType = item.projectType,
                        package = item.package,
                        projectLevel = item.projectLevel,
                        plotSize = item.plotSize,
                        amount = item.amount,
                        remark = item.remark

                    });
                    i++;
                }

            }
            catch (Exception ex) { }

            return reqlist.ToList();
        }

        public PRFModel GetPRFByPrjectID(int projectID)
        {
            PRFModel obj = new PRFModel();
            try
            {

                var res = _dbContext.tblPRFs.Where(x => x.projectID == projectID).FirstOrDefault();
                if (res != null)
                {
                    obj.workingStatus = res.workingStatus;
                    obj.slabdetail = "-";
                    obj.slabheight = res.slabheight;
                    obj.plinthheight = res.plinthheight;
                    obj.porchheight = res.porchheight;
                    obj.elevationpattern = res.elevationpattern;
                    //obj.totalfloor = "-";
                    obj.towerroom = res.towerroom;
                    obj.cornerplotplan = res.cornerplotplan;
                    obj.plotside = res.plotside;
                    obj.boundrywall = res.boundrywall;
                    obj.doorlintel = res.doorlintel;
                    obj.windowsill = res.windowsill;
                    obj.windowlintel = res.windowlintel;
                    obj.anyother = res.anyother;
                }
            }
            catch (Exception ex) { }

            return obj;
        }

        public string SavePRF(PRFModel model)
        {
           
            try
            {
                 
                var res = _dbContext.tblPRFs.Where(x => x.projectID == model.projectID).FirstOrDefault();
                if (res != null)
                {
                    res.workingStatus = model.workingStatus;
                    res.slabdetail = "-";
                    res.slabheight = model.slabheight;
                    res.plinthheight = model.plinthheight;
                    res.porchheight = model.porchheight;
                    res.elevationpattern = model.elevationpattern;
                    //obj.totalfloor = "-";
                    res.towerroom = model.towerroom;
                    res.cornerplotplan = model.cornerplotplan;
                    res.plotside = model.plotside;
                    res.boundrywall = model.boundrywall;
                    res.doorlintel = model.doorlintel;
                    res.windowsill = model.windowsill;
                    res.windowlintel = model.windowlintel;
                    res.anyother = model.anyother;
                }
                else
                {
                    tblPRF obj = new tblPRF();
                    obj.projectID = model.projectID;
                    obj.workingStatus = model.workingStatus;
                    obj.slabdetail = "-";
                    obj.slabheight = model.slabheight;
                    obj.plinthheight = model.plinthheight;
                    obj.porchheight = model.porchheight;
                    obj.elevationpattern = model.elevationpattern;
                    //obj.totalfloor = "-";
                    obj.towerroom = model.towerroom;
                    obj.cornerplotplan = model.cornerplotplan;
                    obj.plotside = model.plotside;
                    obj.boundrywall = model.boundrywall;
                    obj.doorlintel = model.doorlintel;
                    obj.windowsill = model.windowsill;
                    obj.windowlintel = model.windowlintel;
                    obj.anyother = model.anyother;

                    _dbContext.tblPRFs.Add(obj);
                }
                _dbContext.SaveChanges();
 
            }
            catch(SqlException ex)
            {
                string ch= ex.Message;
                return ch;
            }
            
            
            return "";
        }

        
        public IEnumerable<QuotationModel> SearchByProjectIDOrName(string opt, string projectID  , string name )
        {
            List<QuotationModel> lst = new List<QuotationModel>();
            //if ((chkName == "true") && (chkcity == "true"))
            //    return _dbContext.tblClients.Where(x => (x.clientName.Contains(name)) && (x.city.Contains(cityname)));
            //else if (chkcity == "true")
            //    return _dbContext.tblClients.Where(x => (x.city.Contains(cityname)));


            return lst;// (s => s.clientName.Contains(name)).ToList();
        }




        public operation AddSearchProject_ClientName(int projectID)
        {
            operation obj = new operation();
            try
            {
                
                var res = (from pd in _dbContext.tblProjectDetails
                           join c in _dbContext.tblClients on pd.clientID equals c.clientID
                           where (pd.projectID == projectID)
                           select new
                           {
                               clientName = c.clientName,
                               status = pd.status
                           }).ToList();
               foreach(var item in res)
                {
                    obj.clientName =  item.clientName;
                    obj.status = item.status;
                }
            }
            catch (Exception ex) { }
            return obj;
        }

       





        public string SaveSiteVisit(int projectID, string fname, string remark)
        {
            tblProjectSiteVisit obj = new tblProjectSiteVisit();
            try
            {
                obj.dt = DateTime.Today.Date;
                obj.projectID = projectID;
                obj.sitePhotoFile = fname;
                obj.remark = remark == null ? "-" : remark.Trim();

                _dbContext.tblProjectSiteVisits.Add(obj);
                _dbContext.SaveChanges();
            }
            catch(Exception ex) { return ex.Message; }

            return "";
        }

        public IEnumerable<operation> SearchSiteVisitByNameOrProjectID(string opt, string projectID, string name, string pname)
        {
            List<operation> lst = new List<operation>();
            try
            {
                if (opt == "projectID")
                {
                    int pid = int.Parse(projectID);
                    lst = (from pl in _dbContext.tblProjectDetails
                           join cl in _dbContext.tblClients on pl.clientID equals cl.clientID
                           where (pl.projectID == pid)
                           select new operation
                           {
                               dt = pl.dt,
                               clientID = cl.clientID,
                               clientName = cl.clientName,
                               projectName = pl.projectname,
                               projectID = pl.projectID,
                               projectType = pl.projectType,
                               package = pl.package,
                               projectLevel = pl.projectLevel,
                               plotSize = pl.plotSize,
                               amount = pl.amount,
                               projectlocation = pl.projectlocation,
                               status = pl.status
                               
                           }).ToList();


                }
                else if (opt == "name")
                {
                    lst = (from pl in _dbContext.tblProjectDetails
                           join cl in _dbContext.tblClients on pl.clientID equals cl.clientID
                           where (cl.clientName.Contains(name))
                           select new operation
                           {
                               dt = pl.dt,
                               clientID = cl.clientID,
                               clientName = cl.clientName,
                               projectName = pl.projectname,
                               projectID = pl.projectID,
                               projectType = pl.projectType,
                               package = pl.package,
                               projectLevel = pl.projectLevel,
                               plotSize = pl.plotSize,
                               amount = pl.amount,
                               projectlocation = pl.projectlocation,
                               status = pl.status

                           }).ToList();

                }

                else 
                {
                    lst = (from pl in _dbContext.tblProjectDetails
                           join cl in _dbContext.tblClients on pl.clientID equals cl.clientID
                           where (pl.projectname.Contains(pname))
                           select new operation
                           {
                               dt = pl.dt,
                               clientID = cl.clientID,
                               clientName = cl.clientName,
                               projectName = pl.projectname,
                               projectID = pl.projectID,
                               projectType = pl.projectType,
                               package = pl.package,
                               projectLevel = pl.projectLevel,
                               plotSize = pl.plotSize,
                               amount = pl.amount,
                               projectlocation = pl.projectlocation,
                               status = pl.status

                           }).ToList();

                }
            }
            catch (Exception ex) { }
            
            return lst; 
        }

       public operation GetProjectInfo(int projectID)
        {
            operation op = new operation();
            try
            {
                var res = (from pd in _dbContext.tblProjectDetails
                           where pd.projectID == projectID
                           select new operation
                           {
                               clientID = pd.clientID,
                               projectlocation = pd.projectlocation,

                           }).ToList();
                foreach(var item in res)
                {
                    op.clientID = item.clientID;
                    op.projectlocation = item.projectlocation;
                }
            }
            catch (Exception ex) { }
            return op;
        }

        public IEnumerable<SelectListItem> getOperationDesigner()
        {
            var obj = new List<SelectListItem>();
            obj.Add(new SelectListItem
            {
                Text = "NA",
                Value = "0",

            });
            try
            {
                var res = _dbContext.tblStaffs.Where(x => x.designation == "Tech").ToList();
                foreach (var item in res)
                {
                    obj.Add(new SelectListItem
                    {
                        Text = item.name,
                        Value = item.staffID.ToString(),

                    });
                }
            }
            catch (Exception ex) { }
            return obj;

        }
        public string ProjectAssigning(operation op)
        {
           
            try
            {
                tblTaskAssign taObj = new tblTaskAssign();
                DateTime dt = DateTime.Today.Date;
                string tm = DateTime.Now.Hour + ":" + DateTime.Now.Minute;// +":" + DateTime.Now.Second;
                var res = _dbContext.tblProjectManagements.Where(x => x.pmID == op.pmID).FirstOrDefault();
                res.projectstatus = "Assigned";

                taObj.dt = dt;
                taObj.tm = tm;
                taObj.projectID = op.projectID;
                taObj.designerID = int.Parse(op.designerName);
                taObj.category = res.category;
                taObj.subcatorgy = res.subcategory;
                taObj.techRemark = op.techremark == null ? "" : op.techremark.Trim();
                taObj.pmID = op.pmID;

                _dbContext.tblTaskAssigns.Add(taObj);
                _dbContext.SaveChanges();

                return "Success";
            }
            catch (Exception ex) { return ex.Message; }

        }
        public string ProjectRollBack(int pmID)
        {
            try
            {
                
                var res = _dbContext.tblProjectManagements.Where(x => x.pmID == pmID).FirstOrDefault();

                if (res != null)
                {
                    res.projectstatus = "Normal";
                    res.User_UploadedFileName = "";
                }

                var res1 = _dbContext.tblTaskAssigns.Where(x => x.pmID == pmID).ToList();

                foreach (var item in res1)
                {
                    item.designerID = 0;
                }
                _dbContext.SaveChanges();

                return "Y";
            }
            catch (SqlException ex)
            {
                string ch = ex.Message;
                return ch;
            }

        }
        public string ProjectRollBack(string projectmanagementID, ref string whatsAppno, ref string opt, ref string category, ref string projectID)
        {
            try
            {
                string designerID = "";
                whatsAppno = ""; opt = ""; category = ""; projectID = "";

                //if (conDlb.State == ConnectionState.Closed) conDlb.Open();
                //if (conAmtConfirm.State == ConnectionState.Closed) conAmtConfirm.Open();

                //str = " Select designerID , opt, projectCategory, projectID From  " +
                //      " tblDesignerTaskAssign Where projectmanagementID = " + projectmanagementID;
                //cmd = new SqlCommand(str, conDlb);
                //try
                //{
                //    SqlDataReader rd = cmd.ExecuteReader();
                //    if (rd.Read())
                //    {
                //        designerID = rd["designerID"].ToString();
                //        opt = rd["opt"].ToString();
                //        projectID = rd["projectID"].ToString();
                //        category = rd["projectCategory"].ToString();
                //    }
                //    rd.Close();
                //}
                //catch (Exception ex) { }


                //str = " Update tblDesignerTaskAssign SET designerID = 0 " +
                //     "  Where projectmanagementID = " + projectmanagementID;
                //cmd = new SqlCommand(str, conDlb);
                //cmd.ExecuteNonQuery();

                //str = " Update tblProjectManagement SET projectStatus ='Normal', rowcolor  ='Normal'," +
                //      " User_UploadedFileName='', User_UploadedReferenceFile='', " +
                //      " User_pdfUpload ='', User_conceptImagUpload ='' Where " +
                //      " projectmanagementID =" + projectmanagementID;
                //cmd = new SqlCommand(str, conAmtConfirm);
                //cmd.ExecuteNonQuery();


                //str = " Select whatsappno From tblRegistration Where registrationID =" + designerID;
                //cmd = new SqlCommand(str, conAmtConfirm);
                //try
                //{
                //    whatsAppno = cmd.ExecuteScalar().ToString();
                //}
                //catch (Exception ex) { }

                return "Y";
            }
            catch (Exception ex) { return ex.Message; }

        }

        public IEnumerable<operation> getProjectAssign()
        {
            List<operation> prjlist = new List<operation>();
            try
            {

                var prj = from pl in _dbContext.tblProjectDetails
                          join cl in _dbContext.tblClients
                          on pl.clientID equals cl.clientID
                          where (pl.status == "estimated")
                          select new
                          {
                              clientID = cl.clientID,
                              clientName = cl.clientName,
                              projectID = pl.projectID,
                              projectType = pl.projectType,
                              package = pl.package,
                              projectLevel = pl.projectLevel,
                              plotSize = pl.plotSize,
                              amount = pl.amount,
                              remark = pl.remark
                          };
                int i = 1;


                foreach (var item in prj)
                {
                    prjlist.Add(new operation
                    {
                        sno = i,
                        clientID = item.clientID,
                        projectID = item.projectID,
                        clientName = item.clientName,
                        projectType = item.projectType,
                        package = item.package,
                        projectLevel = item.projectLevel,
                        plotSize = item.plotSize,
                        amount = item.amount,
                        remark = item.remark

                    });
                    i++;
                }

            }
            catch (Exception ex) { }
            return prjlist;
        }

        public string SaveProjectAssigned(string projectID, string staffID, string projectCategory, string designerAmount)
        {
            tblOperation objOptn = new tblOperation();
            tblStaffLedger sl = new tblStaffLedger();
            using (var context = new ArchManagerDBEntities())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int balance = 0;
                        int sid = int.Parse(staffID);
                        int pid = int.Parse(projectID);
                        int amount = int.Parse(designerAmount);
                        objOptn.dt = DateTime.Now;
                        objOptn.projectID = pid;
                        objOptn.staffID = sid;
                        objOptn.designerAmount = amount;
                        objOptn.projectCategory = projectCategory;
                        objOptn.status = "WRC";// "WRC"; //corrent working
                        objOptn.payStatus = "pending";

                        context.tblOperations.Add(objOptn);
                        context.SaveChanges();

                        tblProjectDetail objPD = context.tblProjectDetails.Find(int.Parse(projectID));
                        objPD.status = "WRC";

                        context.Entry(objPD).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                        try
                        {
                            tblStaffLedger tmp = context.tblStaffLedgers.Where(x => x.staffID == sid).OrderByDescending(x => x.staffLedgerID).FirstOrDefault();
                            balance = tmp.balance;
                        }
                        catch (Exception ex) { }

                        sl.dt = DateTime.Now;
                        sl.staffID = sid;
                        sl.projectID = pid;
                        sl.prjAmount = amount;
                        sl.balance = balance + amount;
                        sl.remark = "-";
                        context.tblStaffLedgers.Add(sl);
                        context.SaveChanges();

                        dbTransaction.Commit();
                    }
                    catch (Exception ex) { dbTransaction.Rollback(); }
                }
            }
            return "";
        }

        public IEnumerable<operation> GetCurrentWorking(string dname, string category , string subcategory)
        {
            List<operation> prjlist = new List<operation>();
            string  color = "", files = "";
            try
            {
                if ((dname == null) || (dname == "0") || (dname == "NA"))
                {
                    var query = (from pl in _dbContext.tblProjectDetails
                                 join cl in _dbContext.tblClients on pl.clientID equals cl.clientID
                                 join cw in _dbContext.tblProjectManagements on pl.projectID equals cw.projectID
                                 where ((cw.category == category) && (cw.subcategory == subcategory))
                                 select new
                                 {
                                     dt = cw.dt,
                                     clientID = cl.clientID,
                                     clientName = cl.clientName,
                                     projectID = pl.projectID,
                                     projectType = pl.projectType,
                                     category = cw.category,
                                     subcatgory = cw.subcategory,
                                     package = pl.package,
                                     projectLevel = pl.projectLevel,
                                     plotSize = pl.plotSize,
                                     amount = pl.amount,
                                     projectlocation = pl.projectlocation,
                                     remark = cw.remark,
                                     pmID = cw.pmID,
                                     projetStatus = cw.projectstatus,
                                     status = cw.status,
                                     uploadFileName = cw.User_UploadedFileName,
                                     files = cw.User_UploadedFileName,
                                 }).ToList();



                    int i = 1;
                    string dname1 = "", prfFlag = "N";
                    foreach (var item in query)
                    {
                        if (item.projetStatus == "Normal")
                            color = "#FFF";//White;
                        else if (item.projetStatus == "Assigned")
                            color = "#ffa500";//Orange;
                        else
                            color = "#E3FF00";//red;


                        dname1 = "";


                        if (item.projetStatus == "Assigned")
                        {
                            var rec = (from d in _dbContext.tblStaffs
                                       join ta in _dbContext.tblTaskAssigns on d.staffID equals ta.designerID
                                       where (ta.pmID == item.pmID)
                                       select new
                                       {
                                           name = d.name
                                       });
                            foreach (var item1 in rec)
                            {
                                dname1 = item1.name;
                            }
                        }
                        else if (item.projetStatus == "Submit")
                        {
                            var rec = (from d in _dbContext.tblStaffs
                                       join ta in _dbContext.tblTaskAssigns on d.staffID equals ta.submitDesignerID
                                       where (ta.pmID == item.pmID)
                                       select new
                                       {
                                           name = d.name
                                       });
                            foreach (var item1 in rec)
                            {
                                dname1 = item1.name;
                            }
                        }

                        if (item.files == null)
                            files = "";
                        else
                            files = item.files;

                        //Check prf
                        prfFlag = "N";
                        var rec1 = _dbContext.tblPRFs.Where(x => x.projectID == item.projectID).FirstOrDefault();
                        if (rec1 != null) prfFlag = "Y"
 ;
                        prjlist.Add(new operation
                        {
                            sno = i,
                            pmID = item.pmID,
                            clientID = item.clientID,
                            projectID = item.projectID,
                            clientName = item.clientName,
                            projectType = item.projectType,
                            projectlocation = item.projectlocation,
                            package = item.package,
                            projectLevel = item.projectLevel,
                            category = item.category,
                            subcategory = item.subcatgory,
                            plotSize = item.plotSize,
                            amount = item.amount,
                            projectStatus = item.projetStatus,
                            status = item.status,
                            designerName = dname1,
                            uploadFileName = item.uploadFileName == null ? "" : item.uploadFileName.Trim(),
                            remark = item.remark,
                            rowcolor = color,
                            arr = files.ToString().Split(','),
                            filename = files == null ? "" : item.files,// dr["filename"].ToString(),
                            prfFlag = prfFlag
                        });
                        i++;
                    }

                }
                else
                {

                    int sid = int.Parse(dname);
                    var query = from pl in _dbContext.tblProjectDetails
                                join cl in _dbContext.tblClients on pl.clientID equals cl.clientID
                                join pm in _dbContext.tblProjectManagements on pl.projectID equals pm.projectID
                                join ta in _dbContext.tblTaskAssigns on pm.pmID equals ta.pmID
                                join sf in _dbContext.tblStaffs on ta.designerID equals sf.staffID
                                where (ta.designerID == sid)
                                select new
                                {
                                    dt = pm.dt,
                                    clientID = cl.clientID,
                                    clientName = cl.clientName,
                                    projectID = pl.projectID,
                                    projectType = pl.projectType,
                                    category = pm.category,
                                    subcatgory = pm.subcategory,
                                    package = pl.package,
                                    projectLevel = pl.projectLevel,
                                    plotSize = pl.plotSize,
                                    amount = pl.amount,
                                    projectlocation = pl.projectlocation,
                                    remark = pm.remark,
                                    pmID = pm.pmID,
                                    projetStatus = pm.projectstatus,
                                    status = pm.status,
                                    uploadFileName = pm.User_UploadedFileName,
                                };
                    int i = 1;
                    foreach (var item in query)
                    {

                        if (item.projetStatus == "Normal")
                            color = "#FFF";//White;
                        else if (item.projetStatus == "Assigned")
                            color = "#ffa500";//Orange;
                        else
                            color = "#E3FF00";//red;


                        prjlist.Add(new operation
                        {
                            sno = i,
                            pmID = item.pmID,
                            clientID = item.clientID,
                            projectID = item.projectID,
                            clientName = item.clientName,
                            projectType = item.projectType,
                            projectlocation = item.projectlocation,
                            package = item.package,
                            projectLevel = item.projectLevel,
                            category = item.category,
                            subcategory = item.subcatgory,
                            plotSize = item.plotSize,
                            amount = item.amount,
                            projectStatus = item.projetStatus,
                            status = item.status,
                            uploadFileName = item.uploadFileName == null ? "" : item.uploadFileName.Trim(),
                            remark = item.remark,
                            rowcolor = color

                        });
                        i++;
                    }
                }
            }
            catch (Exception ex) { }
            return prjlist;
        }

     
        public string GetFilePath(int pmID, string filename)
        {
            string location = "",  category ="", subcategory ="";
            try
            {
                int clientID = 0;
                long projectID = 0;
                var res = (from pd in _dbContext.tblProjectDetails
                           join pm in _dbContext.tblProjectManagements on pd.projectID equals pm.projectID
                           where (pm.pmID == pmID)
                           select new
                           {
                               projectID = pd.projectID,
                               clientID = pd.clientID,
                               category = pm.category,
                               subcategory = pm.subcategory
                           }).ToList();

                if (res == null)
                {
                    return "";
                }

                foreach (var item in res)
                {
                    clientID = item.clientID;
                    category = item.category;
                    subcategory = item.subcategory;
                    projectID = item.projectID;
                }
                location = HostingEnvironment.MapPath("~//ProjectLocation//client_" + clientID.ToString() + "//proj_" + projectID.ToString() + "//" + category + "//" + subcategory + "//I/" + filename);

            }
            catch (Exception ex) { return ex.Message; }
            return location;
        }

        public string GetProjectSiteVistPath(int pid, string filename)
        {
            string  clientID = "";
            try
            {

                var res = _dbContext.tblProjectDetails.Where(x => x.projectID == pid).FirstOrDefault();
                 
                if (res == null)
                {
                    return "";
                }
                clientID = res.clientID.ToString();
               return  HostingEnvironment.MapPath("~//ProjectLocation//client_" + clientID.ToString() + "//proj_" + pid.ToString() + "//Site Visit//" + filename);

            }
            catch (Exception ex) { return ex.Message; }
            return "";
        }

        public string SendTaskMailToClien(int pmID, int pid,  string[] arrFiles, out string uploadedFileName, string gmail)
        {

            uploadedFileName = "";
            try
            {
                string filename = "", plotSize = "",  ch ,str="", fnamesub="";
                string mainfile = "", gMailAccount, password, clientMailId = "", clientName = "", filepath = "", category ="", subcategory = "";
                int i, j, clientID=0;
                string[] arr = new string[10];
                 
                Attachment at;
                  
                gMailAccount = "";
                password = "";
                var res = ( from pd in _dbContext.tblProjectDetails
                            join cl in _dbContext.tblClients on pd.clientID equals cl.clientID
                            join pm in _dbContext.tblProjectManagements on pd.projectID equals pm.projectID
                            join cp in _dbContext.tblCompanyProfiles on cl.companyID equals cp.companyID
                            where (pm.pmID == pmID)
                            select new
                            {   
                               clientID = pd.clientID,
                               category = pm.category,
                               subcategory = pm.subcategory,
                               clientName = cl.clientName,
                               clientMailID = cl.emailID,
                               plotSize = pd.plotSize,
                               gmailID = cp.emailID,
                               pwd = cp.pwd
                           }).ToList();
                if (res == null)
                {
                    return "Record not found";
                }

                foreach(var item in res)
                {
                    clientID = item.clientID;
                    category = item.category;
                    subcategory = item.subcategory;
                    clientName = item.clientName;
                    clientMailId = item.clientMailID;
                    plotSize = item.plotSize;
                    gMailAccount = item.gmailID;
                    password = item.pwd;

                }
                var profile = _dbContext.tblCompanyProfiles.FirstOrDefault();
                if (profile == null)
                {
                    return "Company profile is not available";
                }
                NetworkCredential loginInfo = new NetworkCredential(gMailAccount, password);
                MailMessage objMail = new MailMessage();

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


                CultureInfo CInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo TInfo = CInfo.TextInfo;
                clientName = TInfo.ToTitleCase(clientName);

                clientName = clientName.Replace(" ", "_");
                str = "Dear " + clientName;

                objMail.From = new MailAddress(gMailAccount);
                objMail.IsBodyHtml = true;
                objMail.Priority = MailPriority.High;

                str += "<br /> <br /> Greetings!";

                objMail.Subject = " 🏚 " + subcategory + "_" + pid + "_" + "_" + clientName + "_" + plotSize;
                str += "<br /> <br /> Kindly find our submission of " + subcategory + " of Project ID  " + pid + ".";


                if (subcategory.Contains("Revised"))
                {
                    fnamesub = "Revised ";
                    subcategory = subcategory.Replace("Revised", "");
                    subcategory = subcategory.Trim();
                }

                if (subcategory.Length >= 60)
                {
                    foreach (var part in subcategory.Split(' '))
                    {
                        fnamesub += part.Substring(0, 1);
                    }
                }
                else
                {
                    fnamesub = subcategory;
                }

                fnamesub = fnamesub.Replace(" ", "_");



                ch = HostingEnvironment.MapPath("~//ProjectLocation//client_" + clientID.ToString() + "//proj_" + pid.ToString() + "//" + category + "//" + subcategory);
                for (i = 0; i < arrFiles.Length; i++)
                {
                    filename = arrFiles[i];
                    string tempfilename = "", ext = "";


                    string filetmp = ch + "/I/" + filename; // check file is available in option of elevation otherwise search in another folder

                    if (File.Exists(filetmp))
                    {
                        ext = Path.GetExtension(filename);
                        tempfilename = ch + "/" + pid + "_" + fnamesub + ext;
                        if (File.Exists(tempfilename))
                        {
                            //check if file already exist in Elevation path
                            tempfilename = ch + "/" + pid + "_" + fnamesub + "_rev_1" + ext;
                            if (File.Exists(tempfilename))
                            {

                                bool flag1 = true;
                                j = 2;
                                while (flag1)
                                {
                                    tempfilename = ch + "/" + pid + "_" + fnamesub + "_rev_" + j.ToString() + ext;
                                    if (!File.Exists(tempfilename))
                                    {
                                        flag1 = false;
                                    }
                                    j++;
                                }
                            }
                        }

                        File.Copy(filetmp, tempfilename); // copy file from test folder to main folder
                        filename = Path.GetFileName(tempfilename);
                        filepath = ch + "/" + filename;
                        mainfile = filename;

                        if (uploadedFileName.Length == 0)
                            uploadedFileName = filename;
                        else
                            uploadedFileName = uploadedFileName + "," + filename;

                        //attach Elevation file 
                        at = new Attachment(tempfilename);
                        at.Name = filename;
                        objMail.Attachments.Add(at);
                    }
                }

                if (gmail == "true")
                {
                    str += " <br /> <br /> Check draft image very carefully for: <br/> ";
                    str += "<ul> ";
                    str += "   <li> Modeling </li>";
                    str += "   <li> Camera Angle </li>";
                    str += "   <li> Colors </li>";
                    str += "   <li> Light </li>";
                    str += "   <li> Materials or Textures </li> </ul> <br/>";

                    str += "<br/><br/> Your feedback is very important to us.  <br/> <br /> Thank you for your business. <br/><br/>";
                    str += " <br /> <br /> Thank you for the opportunity to serve you. We look forward for further communication with you.  ";

                    str += " <br /> <br /> Best Regards <br /><br /> ";
                    str += " " + profile.orgName + "   <br /> ";

                    str += "<br/> <i>Please note that this is a system generated mail and does not require signature. </i> ";
                    objMail.Body = str;
                    //SmtpClient client = new SmtpClient("smtp.gmail.com");
                    //client.EnableSsl = true;
                    //client.UseDefaultCredentials = false;
                    //client.Credentials = loginInfo;
                    //client.Send(objMail);

                    SmtpClient client = new SmtpClient("smtp.hostinger.com");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = loginInfo;
                    client.Port = 587;// 465;
                    client.Send(objMail);
                }
                try
                {
                    filepath = ch + "/I/";
                    Directory.Delete(filepath, true);
                }
                catch (Exception ex) { }
                return "Y";
            }
            catch (Exception ex) { return ex.Message; }

            return "";
        }

        public string DeleteProjectManagement(int pmID, string uploadedFileName)
        {
            try
            {
                string subcategory="" , category = "";
                int  designrID=0;
                long projectID = 0;
                 
                using (var context = new ArchManagerDBEntities())
                {
                    using(var dbTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var res = context.tblTaskAssigns.Where(x => x.pmID == pmID).OrderByDescending(x => x.taskID).FirstOrDefault();
                            if (res != null)
                            {
                                projectID = res.projectID;
                                designrID = res.submitDesignerID == null ? 0 : (int)res.submitDesignerID;
                                category = res.category;
                                subcategory = res.subcatorgy;
                            }

                            var del = context.tblProjectManagements.Where(x => (x.pmID == pmID)).FirstOrDefault();
                            if (del != null)
                            {
                                context.tblProjectManagements.Remove(del);
                                context.SaveChanges();
                            }
                            tblProjectUploadFile objFile = new tblProjectUploadFile();
                            objFile.dt = DateTime.Today.Date;
                            objFile.projectID = projectID;
                            objFile.designerID = designrID;
                            objFile.category = category;
                            objFile.subcategory = subcategory;
                            objFile.filename = uploadedFileName;

                            context.tblProjectUploadFiles.Add(objFile);
                            context.SaveChanges();

                            dbTransaction.Commit();
                            return "Y";
                        }
                        catch(Exception ex)
                        {
                            dbTransaction.Rollback();
                        }
                
                    }
                }
            }
            catch (Exception ex) { return ex.Message; }
            return "";
        }

       
        public string DownloadPRF(string projectID, string filelocation)
        {
            string pdfname = "";

           
            Document doc = new Document();
            Paragraph para1 = new Paragraph();
            Phrase ph1 = new Phrase();
            iTextSharp.text.Font fnt = new iTextSharp.text.Font();
            SqlDataAdapter adsDs = new SqlDataAdapter();
            DataSet ds = new DataSet();
            PdfPTable table1 = new PdfPTable(2);
            PdfPCell pdfcell;// As PdfPCell
            try
            {
                if (!Directory.Exists(HostingEnvironment.MapPath("~//prf")))
                {
                    Directory.CreateDirectory(HostingEnvironment.MapPath("~//prf"));
                }

                pdfname = HostingEnvironment.MapPath("~//prf//prf_" + projectID + ".pdf");
                PdfWriter.GetInstance(doc, new FileStream(pdfname, FileMode.Create));
                doc.Open();
                para1.Alignment = Element.ALIGN_LEFT;
                para1.SetLeading(0.0F, 1.0F);

                //get record of prf
                int pid = int.Parse(projectID);
                var res = _dbContext.tblPRFs.Where(x => x.projectID == pid).FirstOrDefault();  // GetPRF(projectID, ref level, ref sideEle, ref floorOption);
                if(res ==null)
                {
                    return "";
                }

                Phrase p1;
                //iTextSharp.text.Font link;
                //link = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);

                //dr = ds.Tables[0].Rows[0];
                para1.Alignment = Element.ALIGN_LEFT;
                para1.SetLeading(0.0F, 1.0F);

                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                p1 = new Phrase(new Chunk(Environment.NewLine + "Project Details Requirement Form  (" + projectID + ")", fnt));
                para1.Add(p1);
                para1.Add(Environment.NewLine);
                //fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL + iTextSharp.text.Font.ITALIC, BaseColor.BLACK);

                //str = "The purpose of this very brief is to help us serve your needs more effectively.  By understanding where we are " +
                //     "exceeding your expectations, or need to improve, we can allocate our resources to provide better services,  " +
                //     "knowledgeable staff, and executive management.  Our goal is to be proactive in monitoring your satisfaction, " +
                //     "so please provide constructive replies that we can incorporate into our strategy. ";

                //table1.WidthPercentage = 80;
                //table1.HorizontalAlignment = 0;
                //pdfcell = null;
                //pdfcell = new PdfPCell(new Phrase(new Chunk(str, fnt)));
                //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                //pdfcell.BorderWidth = 0;
                //pdfcell.FixedHeight = 80;
                //table1.AddCell(pdfcell);

                //para1.Add(table1);
                //para1.Add(Environment.NewLine);

                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                table1 = new PdfPTable(3);
                table1.WidthPercentage = 100;
                table1.HorizontalAlignment = 0;
                int[] cellWidthPercentage = new int[3];
                cellWidthPercentage[0] = 6;
                cellWidthPercentage[1] = 60;
                cellWidthPercentage[2] = 30;
                table1.SetWidths(cellWidthPercentage);

                pdfcell = null;
                pdfcell = new PdfPCell(new Phrase(new Chunk(" SNo. ", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                pdfcell.FixedHeight = 25;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(" Query", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                pdfcell.FixedHeight = 25;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(" Reply", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                pdfcell.FixedHeight = 25;
                table1.AddCell(pdfcell);


                pdfcell = null;
                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL);
                pdfcell = new PdfPCell(new Phrase(new Chunk(" 1", fnt)));
                pdfcell.FixedHeight = 25;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Project ID or Project Name", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(projectID, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);


                pdfcell = null;
                pdfcell = new PdfPCell(new Phrase(new Chunk(" 2", fnt)));
                pdfcell.FixedHeight = 35;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Specify the actual working status of the site.", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.workingStatus, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                //pdfcell = null;
                //pdfcell = new PdfPCell(new Phrase(new Chunk(" 3", fnt)));
                //pdfcell.FixedHeight = 35;
                //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                //table1.AddCell(pdfcell);

                //pdfcell = new PdfPCell(new Phrase(new Chunk(" Existing site photographs (zip/rar)", fnt)));
                //table1.AddCell(pdfcell);

                //Anchor an = new Anchor(zipfilename, link);
                //an.Reference = zipfileloacation;
                //table1.AddCell(an);


                //pdfcell = null;
                //pdfcell = new PdfPCell(new Phrase(new Chunk(" 4", fnt)));
                //pdfcell.FixedHeight = 35;
                //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                //table1.AddCell(pdfcell);

                //pdfcell = new PdfPCell(new Phrase(new Chunk("Is the Slab is executed ? If yes, mark the slab details in plan", fnt)));
                //table1.AddCell(pdfcell);

                //pdfcell = new PdfPCell(new Phrase(new Chunk(dr["slabdetail"].ToString(), fnt)));
                //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                //table1.AddCell(pdfcell);



                pdfcell = new PdfPCell(new Phrase(new Chunk(" 3", fnt)));
                pdfcell.FixedHeight = 30;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Specify the slab height?(In Feet)", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.slabheight, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);


                pdfcell = new PdfPCell(new Phrase(new Chunk(" 4", fnt)));
                pdfcell.FixedHeight = 35;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Specify the height of the plinth ? (In Feet) ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.plinthheight, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);


                pdfcell = new PdfPCell(new Phrase(new Chunk(" 5", fnt)));
                pdfcell.FixedHeight = 30;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Specify the height of porch ? (In Feet)  ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.porchheight, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(" 6", fnt)));
                pdfcell.FixedHeight = 35;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Specify the desired pattern for the elevation? ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.elevationpattern, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);


                //pdfcell = new PdfPCell(new Phrase(new Chunk(" 9", fnt)));
                //pdfcell.FixedHeight = 35;
                //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                //table1.AddCell(pdfcell);

                //pdfcell = new PdfPCell(new Phrase(new Chunk("No. of floors in the proposed building  ", fnt)));
                //table1.AddCell(pdfcell);

                //pdfcell = new PdfPCell(new Phrase(new Chunk(level, fnt)));
                //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                //table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(" 7", fnt)));
                pdfcell.FixedHeight = 35;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(" Door lintel level ? ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.doorlintel, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);


                pdfcell = new PdfPCell(new Phrase(new Chunk(" 8", fnt)));
                pdfcell.FixedHeight = 35;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Window sill level?   ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.windowsill, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(" 9", fnt)));
                pdfcell.FixedHeight = 35;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Window lintel level?   ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.windowlintel, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);


                pdfcell = new PdfPCell(new Phrase(new Chunk(" 10", fnt)));
                pdfcell.FixedHeight = 30;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Is the tower room is executed at Stairs?    ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.towerroom, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);


                pdfcell = new PdfPCell(new Phrase(new Chunk(" 11", fnt)));
                pdfcell.FixedHeight = 30;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);
                pdfcell = new PdfPCell(new Phrase(new Chunk("Do you want side elevations ?", fnt)));
                table1.AddCell(pdfcell);

               // if (sideEle == "0") sideEle = "";
                pdfcell = new PdfPCell(new Phrase(new Chunk(res.cornerplotplan, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(" 12", fnt)));
                pdfcell.FixedHeight = 30;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Mention the facing side of plot:  ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.plotside, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(" 13", fnt)));
                pdfcell.FixedHeight = 60;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Do you want boundary wall Design ? (if yes Please give us plot boundary detail with entrance and site plan) ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.boundrywall, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);




                //pdfcell = new PdfPCell(new Phrase(new Chunk(" 17", fnt)));
                //// pdfcell.FixedHeight = 60;
                //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                //table1.AddCell(pdfcell);

                //pdfcell = new PdfPCell(new Phrase(new Chunk("Floor", fnt)));
                //table1.AddCell(pdfcell);

                //pdfcell = new PdfPCell(new Phrase(new Chunk(floorOption, fnt)));
                //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                //table1.AddCell(pdfcell);





                //pdfcell = new PdfPCell(new Phrase(new Chunk(" 18", fnt)));
                //pdfcell.FixedHeight = 40;
                //pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                //table1.AddCell(pdfcell);

                //pdfcell = new PdfPCell(new Phrase(new Chunk("If you want any type of Color combination, provide us some concept image before 3d view ", fnt)));
                //table1.AddCell(pdfcell);

                //Anchor an1 = new Anchor(filename, link);
                //an1.Reference = flocation;
                //table1.AddCell(an1);


                pdfcell = new PdfPCell(new Phrase(new Chunk(" 14", fnt)));
                pdfcell.FixedHeight = 30;
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Any Other ", fnt)));
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(res.anyother, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                para1.Add(table1);

                //string imageURL = HostingEnvironment.MapPath("~/PDF_Files/letterhead.jpg");
                //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                //jpg.Alignment = Element.ALIGN_LEFT;
                //jpg.ScaleToFit(400.0F, 900.0F);
                //jpg.SetAbsolutePosition(doc.PageSize.Width - 140.0F, doc.PageSize.Height - 845.0F);
                //doc.Add(jpg);
                doc.Add(para1);
                doc.Close();
                doc.Dispose();
                doc = null;

            }
            catch (Exception ex)
            {
                pdfname = "";
            }
            return pdfname;

        }
        public string DownloadSiteVist(int projectID, string filename)
        {
            try
            {
                var res = _dbContext.tblProjectDetails.Where(x => x.projectID == projectID).FirstOrDefault();
                if(res == null)
                {
                    return ""; 
                }

                return HostingEnvironment.MapPath("~/ProjectLocation/client_" + res.clientID + "/proj_" + projectID + "/Site Photo/" + filename);
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public string DownloadUploadFile(int projectID, int uploafFileID, string filename)
        {
            try
            {
                string clientID="" ,category="",subcategory="";

                var res = (from uf in _dbContext.tblProjectUploadFiles
                           join pd in _dbContext.tblProjectDetails
                           on uf.projectID equals pd.projectID
                           where (uf.uploadfileID == uploafFileID)
                           select new
                           {
                               clientID = pd.clientID,
                               category = uf.category,
                               subcategory = uf.subcategory
                           }).ToList();
                           
                foreach(var item in res)
                {
                    clientID = item.clientID.ToString();
                    category = item.category;
                    subcategory = item.subcategory;

                }
                subcategory = subcategory.Replace("Revised", "").Trim();

                return HostingEnvironment.MapPath("~/ProjectLocation/client_" + clientID + "/proj_" + projectID +"/"+ category +"/" + subcategory+"/"+ filename);
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public IEnumerable<operation> getDesignerWorkingList(int reg)
        {
            List<operation> prjlist = new List<operation>();
            string diffDt, color = "";
            int l;
            try
            {
                var query = from pl in _dbContext.tblProjectDetails
                            join cw in _dbContext.tblOperations on pl.projectID equals cw.projectID
                            where ((cw.status == "WRC") && (cw.staffID == reg))
                            select new
                            {
                                dt = cw.dt,
                                projectID = pl.projectID,
                                projectType = pl.projectType,
                                projectCategory = cw.projectCategory,
                                package = pl.package,
                                projectLevel = pl.projectLevel,
                                plotSize = pl.plotSize,
                                amount = pl.amount,
                                remark = cw.remark
                            };
                int i = 1;
                foreach (var item in query)
                {
                    diffDt = (DateTime.Now.Date - item.dt).TotalDays.ToString();
                    l = diffDt.IndexOf(".");
                    diffDt = diffDt.Substring(0, l);
                    if (diffDt == "-0")
                        color = "#FFF";//White;
                    else if (int.Parse(diffDt) == 0)
                        color = "#ffa500";//Orange;
                    else
                        color = "#ff0000";//red;

                    prjlist.Add(new operation
                    {
                        sno = i,
                       // projectID = item.projectID,
                        projectType = item.projectType,
                        package = item.package,
                        projectLevel = item.projectLevel,
                        projectCategory = item.projectCategory,
                        plotSize = item.plotSize,
                        amount = item.amount,
                        remark = item.remark,
                        rowcolor = color

                    });
                    i++;
                }
            }
            catch (Exception ex) { }


            return prjlist;
        }
        public operation SearchAddProject(int projectID)
        {
            operation obj = new operation();
            try
            {

                var rec = from pd in _dbContext.tblProjectDetails
                          join cl in _dbContext.tblClients on pd.clientID equals cl.clientID
                          where (pd.projectID == projectID)
                          select new
                          {
                              clientName = cl.clientName,
                              projectType = pd.projectType
                          };

                if (rec != null)
                {
                    foreach (var item in rec)
                    {
                        obj.clientName = item.clientName;
                        obj.projectType = item.projectType;
                    }
                }

            }
            catch (Exception ex) { }
            return obj;

        }

        public string SaveNewProject(operation obj)
        {
            using (var context = new ArchManagerDBEntities())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var rec = context.tblProjectDetails.Where(pd=>pd.projectID == obj.projectID).FirstOrDefault();
                        if (rec == null)
                        {
                            return "Project is not available ";
                        }
                        if(rec.status.ToLower() == "request")
                        {
                            return "Request pending in Project Management, please check it";
                        }
                        var rec1 = context.tblProjectManagements.Where(pm => ((pm.projectID == obj.projectID) && (pm.category == obj.category) && (pm.subcategory == obj.subcategory))).FirstOrDefault();
                        if (rec1 != null)
                        {
                            return "Project already in queue for same category";
                        }
                         
                        tblProjectManagement pmObj = new tblProjectManagement();
                        pmObj.dt = DateTime.Today;
                        pmObj.projectID = obj.projectID;
                        pmObj.category = obj.category;
                        pmObj.subcategory = obj.subcategory;
                        pmObj.projectstatus = "Assigned";
                        pmObj.status = "WRC";
                        pmObj.remark = obj.remark == null ? "" : obj.remark.Trim(); 
                        context.tblProjectManagements.Add(pmObj);
                        context.SaveChanges();

                        tblTaskAssign taObj = new tblTaskAssign();
                        taObj.dt = DateTime.Today.Date;
                        taObj.tm = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                        taObj.projectID = obj.projectID;
                        taObj.designerID = int.Parse(obj.designerName);
                        taObj.category = obj.category;
                        taObj.subcatorgy = obj.subcategory;
                        taObj.techRemark = obj.remark == null ? "" : obj.remark.Trim();
                        taObj.pmID = pmObj.pmID;

                        context.tblTaskAssigns.Add(taObj);
                        context.SaveChanges();

                        dbTransaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                        return ex.Message;
                    }

                }
            }


            return "";
        }
        public string CompleteCurrentWorking(int operationID)
        {
            try
            {
                var rec = _dbContext.tblOperations.Where(x => (x.operationID == operationID)).FirstOrDefault();
                if (rec != null)
                {
                    rec.status = "completed";
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";

        }


        public string CurrentWorkingRemarkUpdate(int opID, string remark)
        {
            try
            {
                var rec = _dbContext.tblOperations.Where(x => (x.operationID == opID)).FirstOrDefault();
                if (rec != null)
                {
                    rec.remark = remark.Trim();

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "success";

        }
        public string DeleteCurrentWorking(string opID)
        {
            try
            {
                int pd = int.Parse(opID);
                var del = _dbContext.tblOperations.Where(x => (x.operationID == pd)).FirstOrDefault();
                if (del != null)
                {
                    _dbContext.tblOperations.Remove(del);
                    _dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";

        }

        public IEnumerable<operation> getDesignerProjectAmount(int designerID)
        {
            List<operation> prjlist = new List<operation>();
            try
            {
                string str = "pending";
                var query = from cw in _dbContext.tblOperations.Where(cw => (cw.payStatus == str) && (cw.staffID == designerID))
                            select new
                            {
                                operationID = cw.operationID,
                                projectID = cw.projectID,
                                designerAmount = cw.designerAmount,
                                projectCategory = cw.projectCategory
                            };
                foreach (var item in query)
                {
                    prjlist.Add(new operation
                    {
                        pmID = item.operationID,
                        projectID = item.projectID,
                        projectCategory = item.projectCategory,
                        amount = item.designerAmount.Value,
                    });
                }
            }
            catch (Exception ex) { }
            return prjlist.ToList();
        }


        public string DesignerAmountCancel(int operationID)
        {
            try
            {
                var op = _dbContext.tblOperations.SingleOrDefault(x => x.operationID == operationID);
                op.payStatus = "cancel";
                _dbContext.Entry(op).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Done";
        }


        public void SaveStatus(string ch)
        {
            tblStatu obj = new tblStatu();
            try
            {

                obj.status = ch;

                _dbContext.tblStatus.Add(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public string SaveAmountReceive(int cid , int projectID, string amount, string remark, string flagGmail)
        {
            using (var context = new ArchManagerDBEntities())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        tblAmountReceive obj = new tblAmountReceive();
                        tblClientLedger cl = new tblClientLedger();
                        int balance = 0;
                        obj.dt = DateTime.Now;
                        obj.projectID = projectID;
                        obj.amount = int.Parse(amount);
                        obj.remark = remark.Trim();
                        context.tblAmountReceives.Add(obj);
                        context.SaveChanges();
                        try
                        {
                            tblClientLedger tmp = context.tblClientLedgers.Where(x => x.clientID == cid).OrderByDescending(x => x.clientLadgerID).First();
                            balance = tmp.balance;
                        }
                        catch (Exception ex) { }

                        cl.dt = DateTime.Now;
                        cl.clientID = cid;
                        cl.projectID = 0;
                        cl.prjAmount = 0;
                        cl.receivedAmount = int.Parse(amount);
                        cl.balance = balance - int.Parse(amount);

                        cl.remark = remark.Trim();

                        context.tblClientLedgers.Add(cl);
                        context.SaveChanges();

                        dbTransaction.Commit();
                        if (flagGmail == "true") SendReceipt(obj.amountReceiveID, "N");
                        //if (flagGmail == "true") SendReceipt(4);
                    }


                    catch (System.Data.Entity.Validation.DbEntityValidationException e)
                    {
                        string validationErrors = "DbEntityValidationException ValidationErrors: ";
                        foreach (var k in e.EntityValidationErrors)
                        {
                            foreach (var e1 in k.ValidationErrors)
                            {
                                validationErrors += string.Format("{0} - {1}; ", e1.PropertyName, e1.ErrorMessage);
                            }
                        }
                    }
                }
            }
            return "Success";
        }


        public string SendReceipt(int recID, string prfFlag)
        {
            try
            {
                Document doc = new Document();
                Paragraph para1 = new Paragraph();
                Phrase ph1 = new Phrase();
                iTextSharp.text.Font fnt = new iTextSharp.text.Font();
                SqlDataAdapter adsDs = new SqlDataAdapter();
                DataSet ds = new DataSet();
                PdfPTable table1 = new PdfPTable(2);
                PdfPCell pdfcell;// As PdfPCell

                string str = "", logofile="",  projectName ="", dtStr ="", city ="", clientMailId = "", clientName = "", pdfname = "";
                int i ,   receivedAmount =0;
                string[] arr = new string[10];
                long finalizeAmount =0 , projectID = 0, totalRecAmount =0 ;


                var resProfile = _dbContext.tblCompanyProfiles.FirstOrDefault();
                if(resProfile == null)
                {
                    return "Company profile is not available";
                         
                }

                var res = (from pd in _dbContext.tblProjectDetails
                           join cl in _dbContext.tblClients on pd.clientID equals cl.clientID
                           join amt in _dbContext.tblAmountReceives on pd.projectID equals amt.projectID
                           where (amt.amountReceiveID == recID)
                           select new
                           {
                               dtStr = amt.dt.Day + "-" + amt.dt.Month + "-" + amt.dt.Year,
                               clientName = cl.clientName,
                               clientMailID = cl.emailID,
                               plotSize = pd.plotSize,
                               city = cl.city,
                               projectID = pd.projectID,
                               projectName = pd.projectname,
                               finalizeAmount = pd.finalizeAmount,
                               receivedAmount = amt.amount
                           }).ToList();
                if (res == null)
                {
                    return "Record not found";
                }
                foreach (var item in res)
                {
                    clientName = item.clientName;
                    clientMailId = item.clientMailID;
                    dtStr = item.dtStr;
                    city = item.city;
                    projectName = item.projectName;
                    projectID = item.projectID;
                    finalizeAmount = (long)item.finalizeAmount;
                    receivedAmount = item.receivedAmount;
                }
                
                
                var cl1 = _dbContext.tblAmountReceives.Where(x =>((x.projectID == projectID) && (x.amountReceiveID != recID))) .ToList();
                foreach (var item1 in cl1)
                {
                    totalRecAmount += item1.amount;
                }

                NetworkCredential loginInfo = new NetworkCredential(resProfile.emailID, resProfile.pwd);
                MailMessage objMail = new MailMessage();

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


                CultureInfo CInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo TInfo = CInfo.TextInfo;
                clientName = TInfo.ToTitleCase(clientName);

                clientName = clientName.Replace(" ", "_");
                str = "Dear " + clientName;

                objMail.From = new MailAddress(resProfile.emailID);
                objMail.IsBodyHtml = true;
                objMail.Priority = MailPriority.High;

                str += "<br /> <br /> Greetings!";
                objMail.CC.Add(new MailAddress("jitkanjanpuri@gmail.com"));
                
                pdfname = HostingEnvironment.MapPath("~//PDF_Files//Receipt_" + projectID + ".pdf");
                if (File.Exists(pdfname))
                {
                    try
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        File.Delete(pdfname);
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
                para1.Alignment = Element.ALIGN_LEFT;
                para1.SetLeading(0.0F, 1.0F);

                para1.Alignment = Element.ALIGN_LEFT;
                para1.SetLeading(0.0F, 1.0F);

                pdfcell = null;
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

                pdfcell = new PdfPCell(new Phrase(new Chunk("Tel: " + resProfile.phoneno + " , Cell : " + resProfile.mobileno, fnt)));
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

                pdfcell = new PdfPCell(new Phrase(new Chunk(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(clientName.ToLower()), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                pdfcell.BorderWidthLeft = 0;
                pdfcell.BorderWidthRight = 0;
                pdfcell.BorderWidthBottom = 0;

                table1.AddCell(pdfcell);

                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Date " + dtStr, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                //pdfcell.Border = 1;
                pdfcell.BorderWidthLeft = 0;
                pdfcell.BorderWidthRight = 0;
                pdfcell.BorderWidthBottom = 0;

                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(city, fnt)));
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

                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                ph1 = new Phrase(Environment.NewLine + Environment.NewLine + "Receipt No : " + recID.ToString(), fnt);
                para1.Add(ph1);
                ph1 = new Phrase(Environment.NewLine + "Date : " + DateTime.Today.Date.ToString("dd/MM/yyyy") + Environment.NewLine + Environment.NewLine, fnt);
                para1.Add(ph1);

                table1 = new PdfPTable(7);
                int[] cellWidthPercentage3 = new int[] { 5, 15, 25,20, 20,15,15 };
                table1.WidthPercentage = 100;
                table1.HorizontalAlignment = 0;
                table1.SetWidths(cellWidthPercentage3);

                pdfcell = null;
                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                pdfcell = new PdfPCell(new Phrase(new Chunk("SNo", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);

                     
                pdfcell = new PdfPCell(new Phrase(new Chunk("Project ID", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Project Name", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Amount", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Amount Received", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Old Deposit", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk("Balance", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                 
                pdfcell = new PdfPCell(new Phrase(new Chunk("1", fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table1.AddCell(pdfcell);
                
                pdfcell = new PdfPCell(new Phrase(new Chunk(projectID.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);


                pdfcell = new PdfPCell(new Phrase(new Chunk(projectName, fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(finalizeAmount.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(receivedAmount.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk(totalRecAmount.ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);

                pdfcell = new PdfPCell(new Phrase(new Chunk((finalizeAmount- (totalRecAmount + receivedAmount )).ToString(), fnt)));
                pdfcell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                pdfcell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table1.AddCell(pdfcell);
                 
                para1.Add(table1);

                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                ph1 = new Phrase(String.Format(Environment.NewLine + "Total of amount received in words :  " +  objNTW.ConvertWholeNumber(receivedAmount.ToString()) + " Only"), fnt);
                para1.Add(ph1);

                ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "For " + resProfile.orgName.ToUpper()), fnt);
                para1.Add(ph1);


                ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + Environment.NewLine +  resProfile.orgName), fnt);
                para1.Add(ph1);


                ph1 = new Phrase(String.Format(Environment.NewLine + "Authorized signatory"), fnt);
                para1.Add(ph1);


                fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);
                ph1 = new Phrase(String.Format(Environment.NewLine + Environment.NewLine + Environment.NewLine + "                      Please note that this is a system generated mail and does not require signature."), fnt);
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

                if (prfFlag == "N")
                {
                    Attachment at = new Attachment(pdfname);
                    objMail.Attachments.Add(at);

                    str = "<br/> Dear " + clientName + "  <br />" +
                            "<br/> Greetings! <br />" +
                            "<br  /> Payment for project ID " + projectID + " was successful. Please find attached receipt.<br  /> <br  /> ";

                    str += " Best Regards  <br /><br />  ";
                    str += resProfile.orgName; /*" DesignLAB International, India <br /><br /> ";*/

                    str += "<br /> <br /> <br/> <i>Please note that this is a system generated mail and does not require signature. </i> ";
                    objMail.Subject = "🧾 Receipt_" + projectID;
                    objMail.Body = str;



                    SmtpClient client = new SmtpClient("smtp.hostinger.com");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = loginInfo;
                    client.Port = 587;// 465;
                    client.Send(objMail);
                    return "Y";
                }
                else
                {
                    return pdfname;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    string validationErrors = "DbEntityValidationException ValidationErrors: ";
                    foreach (var k in e.EntityValidationErrors)
                    {
                        foreach (var e1 in k.ValidationErrors)
                        {
                            validationErrors += string.Format("{0} - {1}; ", e1.PropertyName, e1.ErrorMessage);
                        }
                    }
                }
            return "";
        }

       







        public string SavePayDesigner(int sid, int amount, string remark)
        {
            //using (var context = new ArchManagerDBEntities())
            //{
            //    using (var dbTransaction = context.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            tblStaffPaid obj = new tblStaffPaid();
            //            tblStaffLedger sl = new tblStaffLedger();
            //            int balance = 0;

            //            if ((remark == null) || (remark.Trim() == "")) remark = "-";
            //            obj.dt = DateTime.Now;
            //            obj.staffID = sid;
            //            obj.amount = amount;
            //            obj.remark = remark.Trim();



            //            context.tblStaffPaids.Add(obj);
            //            context.SaveChanges();

            //            try
            //            {
            //                tblStaffLedger cl = context.tblStaffLedgers.Where(x => x.staffID == sid).OrderByDescending(x => x.staffLedgerID).First();
            //                balance = cl.balance;
            //            }
            //            catch (Exception ex) { }

            //            sl.dt = DateTime.Now;
            //            sl.staffID = sid;
            //            sl.projectID = 0;
            //            sl.prjAmount = 0;
            //            sl.paidAmount = amount;
            //            if (balance > 0)
            //                sl.balance = balance - amount;
            //            else
            //                sl.balance = -amount;

            //            sl.remark = remark.Trim();

            //            context.tblStaffLedgers.Add(sl);
            //            context.SaveChanges();

            //            dbTransaction.Commit();

            //        }
            //        catch (Exception ex)
            //        {
            //            dbTransaction.Rollback();
            //            return ex.Message;
            //        }
            //    }
            //}
            return "";
        }
        public string QuotationDelete(int projectID)
        {
            try
            {
                //var dl = _dbContext.tblProjectDetails.Find(projectID);
                //_dbContext.tblProjectDetails.Remove(dl);
                //_dbContext.SaveChanges();

            }
            catch (Exception ex) { return ex.Message; }
            return "";
        }
        public IEnumerable<clientModel> RptClientLedger(string cname)
        {
            List<clientModel> obj = new List<clientModel>();
            int i = 1;
            try
            {
                int balance = 0;
                if (cname == null) cname = "";
                CultureInfo cinfo = new CultureInfo("en-US");
                if ((cname == null) || (cname.Trim() == ""))
                {
                    var query = from cl in _dbContext.tblClients
                                orderby (cl.clientName)
                                select new
                                {
                                    clientID = cl.clientID,
                                    clientName = cl.clientName,
                                    city = cl.city,
                                    state = cl.state
                                };



                    foreach (var item in query)
                    {
                        balance = 0;
                        try
                        {
                            tblClientLedger cl = _dbContext.tblClientLedgers.Where(x => x.clientID == item.clientID).OrderByDescending(x => x.clientLadgerID).First();
                            balance = cl.balance;
                        }
                        catch (Exception ex) { }

                        if (balance != 0)
                        {
                            obj.Add(new clientModel
                            {
                                sno = i,
                                clientID = item.clientID,
                                clientName = item.clientName,
                                city = item.city,
                                state = item.state,
                                mobile = balance.ToString()
                            });
                            i++;
                        }
                    }
                }
                else
                {

                    var query = from cl in _dbContext.tblClients
                                where (cl.clientName.Contains(cname.Trim()))
                                orderby (cl.clientName)
                                select new
                                {
                                    clientID = cl.clientID,
                                    clientName = cl.clientName,
                                    city = cl.city,
                                    state = cl.state
                                };

                    foreach (var item in query)
                    {
                        balance = 0;
                        try
                        {
                            tblClientLedger cl = _dbContext.tblClientLedgers.Where(x => x.clientID == item.clientID).OrderByDescending(x => x.clientLadgerID).First();
                            balance = cl.balance;
                        }
                        catch (Exception ex) { }

                        obj.Add(new clientModel
                        {
                            sno = i,
                            clientID = item.clientID,
                            clientName = item.clientName,
                            city = item.city,
                            state = item.state,
                            mobile = balance.ToString()
                        });
                        i++;

                    }
                }
            }
            catch (Exception ex) { }
            return obj;

        }



        public List<operation> GetClientLedger(int clientID, string fromDt)
        {
            List<operation> obj = new List<operation>();
            try
            {
                CultureInfo cinfo = new CultureInfo("en-US");
                DateTime dt1 = DateTime.Parse(fromDt, cinfo);


                //obj = (from cl in _dbContext.tblClientLedgers
                //       join pd in _dbContext.tblProjectDetails on cl.projectID equals pd.projectID into pdetail
                //       from pd in pdetail.DefaultIfEmpty()
                //       where ((cl.dt >= dt1) && (cl.clientID == clientID))
                //       select new operation
                //       {
                //           clientLedgerID = cl.clientLadgerID,
                //           clientID = cl.clientID,
                //           projectID = (pd == null ? 0 : pd.projectID),
                //           dtstr = cl.dt.Day.ToString() + "-" + cl.dt.Month.ToString() + "-" + cl.dt.Year.ToString(),
                //           projectName = pd.projectname,
                //           projectType = pd.projectType,
                //           package = pd.package,
                //           amount = cl.prjAmount,
                //           receivedAmount = (cl.receivedAmount == null ? 0 : cl.receivedAmount),
                //           balance = cl.balance,
                //           remark = cl.remark
                //       }).ToList();



            }
            catch (Exception ex) { }
            return obj;

        }

        public IEnumerable<operation> RptClientLedgerDetail(int clientID, string fromDt, string toDt, ref DataTable clientDetailRecord)
        {
            List<operation> obj = new List<operation>();
            try
            {
                CultureInfo cinfo = new CultureInfo("en-US");
                DateTime dt1 = DateTime.Parse(fromDt, cinfo);
                DateTime dt2 = DateTime.Parse(toDt, cinfo);
                dt1 = dt1.AddDays(-1);
                dt2 = dt2.AddDays(1);
                DataRow dr;

                var query = from cl in _dbContext.tblClientLedgers
                            join pd in _dbContext.tblProjectDetails on cl.projectID equals pd.projectID into pdetail
                            from pd in pdetail.DefaultIfEmpty()
                            where ((cl.dt >= dt1) && (cl.dt <= dt2) && (cl.clientID == clientID))
                            select new
                            {
                                clientID = cl.clientID,

                                projectID = (pd == null ? 0 : pd.projectID),
                                dt = cl.dt,
                                projectname = pd.projectname,
                                projectType = pd.projectType,
                                package = pd.package,
                                amount = cl.prjAmount,
                                receive = cl.receivedAmount,
                                balance = cl.balance,
                                remark = cl.remark
                            };

                clientDetailRecord = new DataTable();
                clientDetailRecord.Columns.Add("projectID");
                clientDetailRecord.Columns.Add("dt");
                clientDetailRecord.Columns.Add("pname");
                clientDetailRecord.Columns.Add("ptype");
                clientDetailRecord.Columns.Add("package");
                clientDetailRecord.Columns.Add("amount");
                clientDetailRecord.Columns.Add("receive");
                clientDetailRecord.Columns.Add("balance");
                clientDetailRecord.Columns.Add("remark");
                foreach (var item in query)
                {
                    dr = clientDetailRecord.NewRow();
                    obj.Add(new operation
                    {
                        projectID = item.projectID,
                        status = item.dt.ToString("dd-MMM-yyyy"),
                        projectName = item.projectname,
                        projectType = item.projectType,
                        package = item.package,
                        amount = item.amount,
                        receivedAmount = item.receive,
                        balance = item.balance,
                        remark = item.remark


                    });
                    dr[0] = item.projectID;
                    dr[1] = item.dt;
                    dr[2] = item.projectname;
                    dr[3] = item.projectType;
                    dr[4] = item.package;
                    dr[5] = item.amount;
                    dr[6] = item.receive;
                    dr[7] = item.balance;
                    dr[8] = item.remark;
                    clientDetailRecord.Rows.Add(dr);
                }
            }
            catch (Exception ex) { }
            return obj;

        }



        public IEnumerable<operation> RptClientReceive(string cname, string fromDt, string toDt)
        {
            List<operation> rec = new List<operation>();
            try
            {
                CultureInfo cinfo = new CultureInfo("en-US");
                DateTime dt1 = DateTime.Parse(fromDt, cinfo);
                DateTime dt2 = DateTime.Parse(toDt, cinfo);
                dt2 = dt2.AddDays(1);
                 
                if ((cname == null) || (cname == ""))
                {
                    //var query = from cl in _dbContext.tblClients
                    //            orderby (cl.clientName)
                    //            select new
                    //            {
                    //                clientID = cl.clientID,
                    //                clientName = cl.clientName,
                    //                city = cl.city,
                    //                state = cl.state
                    //            };



                    //foreach (var item in query)
                    //{
                    //    amount = 0;
                    //    try
                    //    {
                    //        var cl = _dbContext.tblClientLedgers.Where(x => ((x.clientID == item.clientID) && ((x.dt >= dt1) && (x.dt <= dt2)))).ToList();
                    //        foreach (var item1 in cl)
                    //        {
                    //            amount += item1.receivedAmount;
                    //        }
                    //    }
                    //    catch (Exception ex) { }

                    //    if (amount != 0)
                    //    {
                    //        amt += amount;
                    //        obj.Add(new operation
                    //        {
                    //            sno = i,
                    //            clientID = item.clientID,
                    //            clientName = item.clientName,
                    //            city = item.city,
                    //            status = item.state,
                    //            receivedAmount = amount,
                    //            balance = amt
                    //        });
                    //        i++;
                    //    }
                    //}


                    rec = (from op in _dbContext.tblAmountReceives 
                               join pd in _dbContext.tblProjectDetails
                               on op.projectID equals pd.projectID
                               join cl in _dbContext.tblClients
                               on pd.clientID equals cl.clientID
                               where ((op.dt >= dt1) && (op.dt <= dt2))
                               select new operation
                                {
                                    dtstr = op.dt.Day +"-" + op.dt.Month +"-" + op.dt.Year,
                                    pmID = op.amountReceiveID,
                                    clientID = cl.clientID,
                                    clientName = cl.clientName,
                                    projectID = op.projectID,
                                    receivedAmount = op.amount,
                                }).ToList();
                }

                else
                {
                    //var query = from cl in _dbContext.tblClients
                    //            where (cl.clientName.Contains(cname.Trim()))
                    //            orderby (cl.clientName)
                    //            select new
                    //            {
                    //                clientID = cl.clientID,
                    //                clientName = cl.clientName,
                    //                city = cl.city,
                    //                state = cl.state
                    //            };



                    //foreach (var item in query)
                    //{
                    //    amount = 0;
                    //    try
                    //    {
                    //        var cl = _dbContext.tblClientLedgers.Where(x => ((x.clientID == item.clientID) && ((x.dt >= dt1) && (x.dt <= dt2)))).ToList();
                    //        foreach (var item1 in cl)
                    //        {
                    //            amount += item1.receivedAmount;
                    //        }
                    //    }
                    //    catch (Exception ex) { }

                    //    if (amount != 0)
                    //    {
                    //        amt += amount;
                    //        obj.Add(new operation
                    //        {
                    //            sno = i,
                    //            clientID = item.clientID,
                    //            clientName = item.clientName,
                    //            city = item.city,
                    //            status = item.state,
                    //            receivedAmount = amount,
                    //            balance = amt
                    //        });
                    //        i++;
                    //    }
                    //}

                    rec = (from op in _dbContext.tblAmountReceives
                               join pd in _dbContext.tblProjectDetails
                               on op.projectID equals pd.projectID
                               join cl in _dbContext.tblClients
                               on pd.clientID equals cl.clientID
                               where ((cl.clientName.Contains(cname)) && ((op.dt >= dt1) && (op.dt <= dt2)))
                               select new operation
                               {
                                   dtstr = op.dt.Day + "-" + op.dt.Month + "-" + op.dt.Year,
                                   pmID = op.amountReceiveID,
                                   clientID = cl.clientID,
                                   projectID = op.projectID,
                                   clientName = cl.clientName,
                                   receivedAmount = op.amount,
                               }).ToList();

                }

            }
            catch (Exception ex) { }
            return rec;

        }

        public IEnumerable<operation> RptOutstanding(string cname, string fromDt, string toDt)
        {
            List<operation> rec = new List<operation>();
            try
            {
                CultureInfo cinfo = new CultureInfo("en-US");
                DateTime dt1 = DateTime.Parse(fromDt, cinfo);
                DateTime dt2 = DateTime.Parse(toDt, cinfo);
                dt2 = dt2.AddDays(1);
                long amount = 0;
                if ((cname == null) || (cname == ""))
                {
                    var query = (from pd in _dbContext.tblProjectDetails
                                 join cl in _dbContext.tblClients
                                 on pd.clientID equals cl.clientID
                                 where ((pd.dt >= dt1) && (pd.dt <= dt2))
                                 select new
                                 {
                                     dt = pd.dt,
                                     clientID = cl.clientID,
                                     clientName = cl.clientName,
                                     city = cl.city,
                                     state = cl.state,
                                     projectID = pd.projectID,
                                     projectLevel = pd.projectLevel,
                                     projectName = pd.projectname,
                                     finalizeAmount = pd.finalizeAmount

                                 }).ToList();
                    foreach (var item in query)
                    {
                        amount = 0;
                        try
                        {
                            var cl = _dbContext.tblAmountReceives.Where(x => x.projectID == item.projectID).ToList();
                            foreach (var item1 in cl)
                            {
                                amount += item1.amount;
                            }
                        }
                        catch (Exception ex) { }
                        rec.Add(new operation
                        {
                            dtstr = item.dt.Day + "-" + item.dt.Month + "-" + item.dt.Year,
                            clientID = item.clientID,
                            clientName = item.clientName,
                            projectID = item.projectID,
                            projectName = item.projectName,
                            finalizeAmount = (long)item.finalizeAmount,
                            receivedAmount = amount,
                            balance = (long)(item.finalizeAmount - amount)
                        });
                    }

                }

                else
                {
                    var query = (from pd in _dbContext.tblProjectDetails
                                 join cl in _dbContext.tblClients
                                 on pd.clientID equals cl.clientID
                                 where (cl.clientName.Contains(cname))
                                 select new
                                 {
                                     dt = pd.dt,
                                     clientID = cl.clientID,
                                     clientName = cl.clientName,
                                     city = cl.city,
                                     state = cl.state,
                                     projectID = pd.projectID,
                                     projectLevel = pd.projectLevel,
                                     projectName = pd.projectname,
                                     finalizeAmount = pd.finalizeAmount

                                 }).ToList();
                    foreach (var item in query)
                    {
                        amount = 0;
                        try
                        {
                            var cl = _dbContext.tblAmountReceives.Where(x => x.projectID == item.projectID).ToList();
                            foreach (var item1 in cl)
                            {
                                amount += item1.amount;
                            }
                        }
                        catch (Exception ex) { }
                        rec.Add(new operation
                        {
                            dtstr = item.dt.Day + "-" + item.dt.Month + "-" + item.dt.Year,
                            clientID = item.clientID,
                            clientName = item.clientName,
                            projectID = item.projectID,
                            projectName = item.projectName,
                            finalizeAmount = (long)item.finalizeAmount,
                            receivedAmount = amount,
                            balance = (long)(item.finalizeAmount - amount)
                        });
                    }

                }


            }
            catch (Exception ex) { }
            return rec;

        }


        public int ClientPreviousBalance(string clinetID, string dt)
        {
            int cid = int.Parse(clinetID);
            CultureInfo cinfo = new CultureInfo("en-US");
            DateTime dt1 = DateTime.Parse(dt, cinfo);
            int balance = 0;
            try
            {
                var cl = _dbContext.tblClientLedgers.Where(x => ((x.clientID == cid) && (x.dt <= dt1))).OrderByDescending(x => x.clientLadgerID).First();
                balance = cl.balance;
            }
            catch (Exception ex) { }
            return balance;
        }


        public IEnumerable<StaffModel> RptDesignerLedger(string dname)
        {
            List<StaffModel> obj = new List<StaffModel>();
            int i = 1;
            try
            {
                int balance = 0;

                if ((dname == null) || (dname == ""))
                {
                    var query = from d in _dbContext.tblStaffs
                                orderby (d.name)
                                select new
                                {
                                    staffID = d.staffID,
                                    name = d.name,
                                    designation = d.designation,
                                    city = d.city + " , " + d.dist
                                };

                    foreach (var item in query)
                    {
                        obj.Add(new StaffModel
                        {
                            sno = i,
                            staffID = item.staffID,
                            name = item.name,
                            designation = item.designation,
                            city = item.city,
                            balance = balance

                        });
                        i++;
                    }
                }
                else
                {

                    var query = from d in _dbContext.tblStaffs
                                where (d.name.Contains(dname.Trim()))
                                orderby (d.name)
                                select new
                                {
                                    staffID = d.staffID,
                                    name = d.name,
                                    designation = d.designation,
                                    city = d.city + " , " + d.dist
                                };

                    foreach (var item in query)
                    {
                        obj.Add(new StaffModel
                        {
                            sno = i,
                            staffID = item.staffID,
                            name = item.name,
                            designation = item.designation,
                            city = item.city,
                            balance = balance

                        });
                        i++;
                    }
                }
            }
            catch (Exception ex) { }
            return obj;

        }

        public List<operation> DashBoard_getProjectType()
        {
            List<operation> lst = new List<operation>();
            try
            {
                DateTime dt2 = DateTime.Today.AddDays(1);
                DateTime dt1 = dt2.AddDays(-4);

                dt2 = DateTime.Today.AddDays(1);
                string[] arr = new string[] { "" };
                string   allQty = "", projectTypeItems="Task";
                

                var res = (from st in _dbContext.tblStaffs
                           where (st.rolltype == "User")
                           select new StaffModel
                           {
                               staffID = st.staffID,
                               name = st.name,
                           }).ToList();

                foreach (var item in res)
                {
                    var res1 = (from p in _dbContext.tblProjectUploadFiles
                                where ((p.designerID == item.staffID) && ((p.dt >= dt1) && (p.dt <= dt2)))
                                group p by p.designerID into g
                                select new
                                {
                                    sno = g.Count(),

                                }).ToList();

                    foreach (var itemOp in res1)
                    {
                        allQty = itemOp.sno.ToString();
                    }

                    lst.Add(
                                new operation
                                {
                                    clientName = item.name,
                                    projectType = projectTypeItems,
                                    package = allQty,

                                }
                           );
                }

            }
            catch (Exception ex) { }

            return lst;

        }


        public List<StaffModel> getTopPerformers()
        {
            List<StaffModel> obj = new List<StaffModel>();

            try
            {
                System.DateTime dt2 = System.DateTime.Today;
                System.DateTime dt1 = dt2.AddDays(-30);// 30 days 
                dt2 = System.DateTime.Now.AddDays(1);


                var res = (from st in _dbContext.tblStaffs
                           where (st.rolltype == "User")
                           select new StaffModel
                           {
                               staffID = st.staffID,
                               name = st.name,
                           }).ToList();



                foreach (var item in res)
                {
                     
                    var res1 = (from p in _dbContext.tblProjectUploadFiles
                                where ((p.designerID == item.staffID) && ((p.dt >= dt1) && (p.dt <= dt2)))  
                                select new operation
                                {
                                    projectID =(long) p.projectID
                                }).ToList();

                    if (res1.Count > 0)
                    {
                        obj.Add(
                              new StaffModel
                              {
                                  name = item.name,
                                  sno = res1.Count / 30,

                              }
                            );
                    }
                }

                if (obj.Count > 3)
                {
                    obj = obj.OrderByDescending(x => x.sno).Take(3).ToList();
                }
            }
            catch (System.Exception ex) { }

            return obj;
        }

        public List<quotation> getQuotation()
        {
            List<quotation> lst = new List<quotation>();
            try
            {
                DateTime dt2 = DateTime.Today;
                DateTime dt1 = dt2.AddDays(-9);

                dt2 = DateTime.Today.AddDays(1);
                lst = (from p in _dbContext.tblProjectDetails
                       where ((p.dt >= dt1) && (p.dt <= dt2))
                       group p by p.projectType into g
                       select new quotation
                       {
                           sno = g.Count(),
                           projectType = g.Key
                       }).ToList();
            }
            catch (Exception ex) { }

            return lst;

        }

        public IEnumerable<operation> Dashboard_getMonthQuotation()
        {
            List<operation> rec = new List<operation>();
            DateTime dt = DateTime.Now.AddDays(-6);

            try
            {
                var prj = (from pd in _dbContext.tblProjectDetails
                           join cl in _dbContext.tblClients
                           on pd.clientID equals cl.clientID
                           where (pd.dt >= dt)
                           select new operation
                           {
                               amount = pd.amount,
                               status = pd.status
                           }).ToList();
                rec = prj.ToList();
            }
            catch (Exception ex) { }

            return rec;
        }
        public IEnumerable<operation> RptDesignerLedgerDetail(int sid, string fromDt, string toDt)
        {
            List<operation> obj = new List<operation>();
            try
            {

                CultureInfo cinfo = new CultureInfo("en-US");
                DateTime dt1 = DateTime.Parse(fromDt, cinfo);
                DateTime dt2 = DateTime.Parse(toDt, cinfo);
                dt2 = dt2.AddDays(1);
                var query = from sl in _dbContext.tblStaffLedgers
                            join pd in _dbContext.tblProjectDetails on sl.projectID equals pd.projectID into pdetail
                            from pd in pdetail.DefaultIfEmpty()
                            where ((sl.staffID == sid) && ((sl.dt >= dt1) && (sl.dt <= dt2)))
                            select new
                            {
                                projectID = sl.projectID,
                                dt = sl.dt,
                                prjName = pd.projectname,
                                projectType = pd.projectType,
                                package = pd.package,
                                prjAmount = sl.prjAmount,
                                paidAmount = sl.paidAmount,
                                balance = sl.balance,
                            };


                foreach (var item in query)
                {

                    obj.Add(new operation
                    {
                        projectID = item.projectID,
                        status = item.dt.ToString("dd-MMM-yyyy"),
                        projectName = item.prjName,
                        projectType = item.projectType,
                        package = item.package,
                        amount = item.prjAmount,
                        paidAmount = item.paidAmount,
                        balance = item.balance
                    });
                }

            }
            catch (Exception ex) { }


            return obj;
        }

        public IEnumerable<operation> RptQuotation(string dt1, string dt2, string searchOpt, string projectID, string cname, string pname)
        {
            List<operation> reqlist = new List<operation>();
            CultureInfo cinfo = new CultureInfo("en-US");
            DateTime fromDt = DateTime.Parse(dt1, cinfo);
            DateTime toDt = DateTime.Parse(dt2, cinfo);
            toDt = toDt.AddDays(1);
            long amt = 0;
            try
            {
               
                if (searchOpt == "projectID")
                {
                    int pid = int.Parse(projectID);
                    var prj = from pd in _dbContext.tblProjectDetails
                              join cl in _dbContext.tblClients
                              on pd.clientID equals cl.clientID
                              where (pd.projectID == pid)
                              select new
                              {
                                  clientid = cl.clientID,
                                  clientname = cl.clientName,
                                  projectID = pd.projectID,
                                  projectType = pd.projectType,
                                  projectName = pd.projectname,
                                  package = pd.package,
                                  projectLevel = pd.projectLevel,
                                  plotSize = pd.plotSize,
                                  amount = pd.amount,
                                  remark = pd.remark,
                                  status = pd.status,
                                  finalizeAmount = pd.finalizeAmount
                              };
                    int i = 1;

                    foreach (var item in prj)
                    {
                        if (item.status == "request")
                            amt += item.amount;
                        else
                            amt += (long)(item.finalizeAmount);
                        reqlist.Add(new operation
                        {
                            sno = i,
                            clientID = item.clientid,
                            projectID = item.projectID,
                            clientName = item.clientname,
                            projectType = item.projectType,
                            projectName = item.projectName,
                            package = item.package,
                            projectLevel = item.projectLevel,
                            plotSize = item.plotSize,
                            amount = item.amount,
                            remark = item.remark,
                            balance = amt,
                            status = item.status,
                            finalizeAmount =(long) item.finalizeAmount
                        });
                        i++;
                    }
                }

                else if (searchOpt == "clientName")
                {
                    var prj = from pd in _dbContext.tblProjectDetails
                              join cl in _dbContext.tblClients
                              on pd.clientID equals cl.clientID
                              where (((pd.dt>= fromDt)&& (pd.dt <= toDt)) && (cl.clientName.Contains(cname)))
                              select new
                              {
                                  clientid = cl.clientID,
                                  clientname = cl.clientName,
                                  projectID = pd.projectID,
                                  projectType = pd.projectType,
                                  projectName = pd.projectname,
                                  package = pd.package,
                                  projectLevel = pd.projectLevel,
                                  plotSize = pd.plotSize,
                                  amount = pd.amount,
                                  remark = pd.remark,
                                  status = pd.status,
                                  finalizeAmount = pd.finalizeAmount
                              };
                    int i = 1;

                    foreach (var item in prj)
                    {
                        if (item.status == "request")
                            amt += item.amount;
                        else
                            amt += (long)(item.finalizeAmount);
                        reqlist.Add(new operation
                        {
                            sno = i,
                            clientID = item.clientid,
                            projectID = item.projectID,
                            clientName = item.clientname,
                            projectType = item.projectType,
                            projectName = item.projectName,
                            status = item.status,
                            package = item.package,
                            projectLevel = item.projectLevel,
                            plotSize = item.plotSize,
                            amount = item.amount,
                            remark = item.remark,
                            balance = amt,
                            finalizeAmount = (long)item.finalizeAmount
                        });
                        i++;
                    }

                }
                else if (searchOpt == "projectName")
                {
                    var prj = from pd in _dbContext.tblProjectDetails
                              join cl in _dbContext.tblClients
                              on pd.clientID equals cl.clientID
                              where (((pd.dt >= fromDt) && (pd.dt <= toDt)) && (pd.projectname.Contains(pname)))
                              select new
                              {
                                  clientid = cl.clientID,
                                  clientname = cl.clientName,
                                  projectID = pd.projectID,
                                  projectType = pd.projectType,
                                  projectName = pd.projectname,
                                  package = pd.package,
                                  projectLevel = pd.projectLevel,
                                  plotSize = pd.plotSize,
                                  amount = pd.amount,
                                  remark = pd.remark,
                                  status = pd.status,
                                  finalizeAmount = pd.finalizeAmount
                              };
                    int i = 1;

                    foreach (var item in prj)
                    {
                        if (item.status == "request")
                            amt += item.amount;
                        else
                            amt += (long)(item.finalizeAmount);
                        reqlist.Add(new operation
                        {
                            sno = i,
                            clientID = item.clientid,
                            projectID = item.projectID,
                            clientName = item.clientname,
                            projectType = item.projectType,
                            projectName = item.projectName,
                            status = item.status,
                            package = item.package,
                            projectLevel = item.projectLevel,
                            plotSize = item.plotSize,
                            amount = item.amount,
                            remark = item.remark,
                            balance = amt,
                            finalizeAmount = (long)item.finalizeAmount
                        });
                        i++;
                    }

                }

            }
            catch (Exception ex) { }

            return reqlist.ToList();

        }
     
        public IEnumerable<operation> RptSiteVisit(string opt, string projectID, string cname, string pname)
        {
            List<operation> reqlist = new List<operation>();
            try
            {
                if (opt == "projectID")
                {
                    int pid = int.Parse(projectID);
                    reqlist = (from sv in _dbContext.tblProjectSiteVisits
                               join pd in _dbContext.tblProjectDetails
                               on sv.projectID equals pd.projectID
                               join cl in _dbContext.tblClients
                               on pd.clientID equals cl.clientID
                               where (pd.projectID == pid)
                               select new operation
                               {
                                   dtstr = sv.dt.Day +"-"+ sv.dt.Month +"-"+ sv.dt.Year,
                                   clientID = cl.clientID,
                                   clientName = cl.clientName,
                                   projectID = pd.projectID,
                                   projectName = pd.projectname,
                                   projectType = pd.projectType,
                                   package = pd.package,
                                   projectLevel = pd.projectLevel,
                                   plotSize = pd.plotSize,
                                   remark = sv.remark,
                                   filename = sv.sitePhotoFile
                               }).ToList();
                }
                else if(opt =="name")
                {
                    reqlist = (from sv in _dbContext.tblProjectSiteVisits
                              join pd in _dbContext.tblProjectDetails
                              on sv.projectID equals pd.projectID
                              join cl in _dbContext.tblClients
                              on pd.clientID equals cl.clientID
                              where (cl.clientName.Contains(cname))
                              select new operation
                              {
                                  dtstr = sv.dt.Day + "-" + sv.dt.Month + "-" + sv.dt.Year,
                                  clientID = cl.clientID,
                                  clientName = cl.clientName,
                                  projectID = pd.projectID,
                                  projectName = pd.projectname,
                                  projectType = pd.projectType,
                                  package = pd.package,
                                  projectLevel = pd.projectLevel,
                                  plotSize = pd.plotSize,
                                  remark = sv.remark,
                                  filename = sv.sitePhotoFile
                              }).ToList();
                }
                else if (opt == "projectName")
                {
                    reqlist = (from sv in _dbContext.tblProjectSiteVisits
                              join pd in _dbContext.tblProjectDetails
                              on sv.projectID equals pd.projectID
                              join cl in _dbContext.tblClients
                              on pd.clientID equals cl.clientID
                              where (pd.projectname.Contains(pname))
                              select new operation
                              {
                                  dtstr = sv.dt.Day + "-" + sv.dt.Month + "-" + sv.dt.Year,
                                  clientID = cl.clientID,
                                  clientName = cl.clientName,
                                  projectID = pd.projectID,
                                  projectName = pd.projectname,
                                  projectType = pd.projectType,
                                  package = pd.package,
                                  projectLevel = pd.projectLevel,
                                  plotSize = pd.plotSize,
                                  remark = sv.remark,
                                  filename = sv.sitePhotoFile
                              }).ToList();
                }
                    
                  
            }
            catch (Exception ex) { }

            return reqlist;

        }

        public IEnumerable<operation> RptProjectHistory(string opt, string projectID, string cname, string pname)
        {
            List<operation> reqlist = new List<operation>();
            try
            {
                if (opt == "projectID")
                {
                    int pid = int.Parse(projectID);
                    reqlist = (from puf in _dbContext.tblProjectUploadFiles
                               join pd in _dbContext.tblProjectDetails
                               on puf.projectID equals pd.projectID
                               join cl in _dbContext.tblClients
                               on pd.clientID equals cl.clientID
                               where (pd.projectID == pid)
                               select new operation
                               {
                                   pmID = puf.uploadfileID,
                                   dtstr = puf.dt.Day + "-" + puf.dt.Month + "-" + puf.dt.Year,
                                   clientID = cl.clientID,
                                   clientName = cl.clientName,
                                   projectID = pd.projectID,
                                   projectName = pd.projectname,
                                   projectType = pd.projectType,
                                   package = pd.package,
                                   projectLevel = pd.projectLevel,
                                   plotSize = pd.plotSize,
                                   category = puf.category,
                                   subcategory = puf.subcategory,
                                   filename = puf.filename
                               }).ToList();
                }
                else if (opt == "name")
                {
                    reqlist = (from puf in _dbContext.tblProjectUploadFiles
                               join pd in _dbContext.tblProjectDetails
                               on puf.projectID equals pd.projectID
                               join cl in _dbContext.tblClients
                               on pd.clientID equals cl.clientID
                               where (cl.clientName.Contains(cname))
                               select new operation
                               {
                                   pmID = puf.uploadfileID,
                                   dtstr = puf.dt.Day + "-" + puf.dt.Month + "-" + puf.dt.Year,
                                   clientID = cl.clientID,
                                   clientName = cl.clientName,
                                   projectID = pd.projectID,
                                   projectName = pd.projectname,
                                   projectType = pd.projectType,
                                   package = pd.package,
                                   projectLevel = pd.projectLevel,
                                   plotSize = pd.plotSize,
                                   category = puf.category,
                                   subcategory = puf.subcategory,
                                   filename = puf.filename
                               }).ToList();
                }
                else if (opt == "projectName")
                {
                    reqlist = (from puf in _dbContext.tblProjectUploadFiles
                               join pd in _dbContext.tblProjectDetails
                               on puf.projectID equals pd.projectID
                               join cl in _dbContext.tblClients
                               on pd.clientID equals cl.clientID
                               where (pd.projectname.Contains(pname))
                               select new operation
                               {
                                   pmID = puf.uploadfileID,
                                   dtstr = puf.dt.Day + "-" + puf.dt.Month + "-" + puf.dt.Year,
                                   clientID = cl.clientID,
                                   clientName = cl.clientName,
                                   projectID = pd.projectID,
                                   projectName = pd.projectname,
                                   projectType = pd.projectType,
                                   package = pd.package,
                                   projectLevel = pd.projectLevel,
                                   plotSize = pd.plotSize,
                                   category = puf.category,
                                   subcategory = puf.subcategory,
                                   filename = puf.filename
                               }).ToList();
                }

                //int i = 1;

                //foreach (var item in prj)
                //{

                //    reqlist.Add(new operation
                //    {
                //        sno = i,
                //        dtstr = item.dt.Day + "-" + item.dt.Month + "-" + item.dt.Year,
                //        pmID = item.id,
                //        clientID = item.clientid,
                //        projectID = item.projectID,
                //        clientName = item.clientname,
                //        projectType = item.projectType,
                //        projectName = item.projectName,
                //        package = item.package,
                //        projectLevel = item.projectLevel,
                //        plotSize = item.plotSize,
                //        filename = item.filename,
                //        category = item.category,
                //        subcategory = item.subcategory,

                //    });
                //    i++;
                //}

            }
            catch (Exception ex) { }

            return reqlist.ToList();

        }


        public IEnumerable<operation> RptTechnical(string dname, string fromDt, string toDt)
        {
            List<operation> lst = new List<operation>();
            try
            {
                CultureInfo cinfo = new CultureInfo("en-US");
                DateTime dt1 = DateTime.Parse(fromDt, cinfo);
                DateTime dt2 = DateTime.Parse(toDt, cinfo);

                if (dname == "0")
                {
                    var res = _dbContext.tblStaffs.Where(x => x.designation == "Tech").ToList();
                    foreach (var item in res)
                    {
                        int qty = 0;
                        var query = (from up in _dbContext.tblProjectUploadFiles
                                     join st in _dbContext.tblStaffs on up.designerID equals st.staffID
                                     where (((up.dt >= dt1) && (up.dt <= dt2)) && (up.designerID == item.staffID))
                                     group up by up.designerID into g
                                     select new
                                     {
                                         sno = g.Count(),
                                     });
                        foreach (var item1 in query)
                        {
                            qty = item1.sno;
                        }

                        lst.Add(
                                new operation
                                {
                                    clientName = item.name,
                                    projectType = "Elevation",
                                    package = qty.ToString(),
                                });


                    }

                }
                else

                {
                    int staffID = int.Parse(dname);
                    int qty = 0;
                    var query = (from up in _dbContext.tblProjectUploadFiles
                                 join st in _dbContext.tblStaffs on up.designerID equals st.staffID
                                 where (((up.dt >= dt1) && (up.dt <= dt2)) && (up.designerID == staffID))
                                 group up by up.designerID into g
                                 select new
                                 {
                                     sno = g.Count(),
                                 });
                    foreach (var item1 in query)
                    {
                        qty = item1.sno;
                    }

                    lst.Add(
                            new operation
                            {
                                clientName = "A",
                                projectType = "Elevation",
                                package = qty.ToString(),
                            });


                }
                 
            }
            catch (Exception ex) { }

            return lst;

        }
        public string SaveRegistration(registration obj)
        {

            try
            {
                //tblRegistration tmp = new tblRegistration();
                //tmp.name = obj.name.Trim();
                //tmp.companyName = obj.companyname.Trim();
                //tmp.profession = obj.profession;
                //tmp.mobileno = obj.mobileno.Trim();
                //tmp.emailID = obj.emailID.Trim();
                //tmp.password = obj.pwd.Trim();

                //_dbContext.tblRegistration.Add(tmp);
                //_dbContext.SaveChanges();
            }
            catch (Exception ex) { }
            
            return "";
        }

        public string SaveBalanceAdjust(int clientID, int balance, int amount, string remark)
        {
            using (var context = new ArchManagerDBEntities())
            {
                tblClientLedger obj = new tblClientLedger();
                try
                {
                    if (remark == null) remark = "";
                    obj.dt = DateTime.Now;
                    obj.clientID = clientID;
                    obj.projectID = 0;
                    obj.prjAmount = 0;
                    obj.receivedAmount = 0;
                    obj.balance = balance - amount;
                    obj.remark = remark.Trim();
                    obj.oldBalance = balance;
                    obj.adjustBalance = amount;
                    context.tblClientLedgers.Add(obj);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "";
        }

        public IEnumerable<operation> ShowBalanceAdjust(string dt1, string dt2, string cname)
        {
            List<operation> rec = new List<operation>();
            try
            {
                DateTime fromDt = DateTime.Parse(dt1, cinfo);
                DateTime toDt = DateTime.Parse(dt2, cinfo);
                toDt = toDt.AddDays(1);


                if ((cname != null) && (cname.Length != 0))
                {
                    var query = (from x in _dbContext.tblClientLedgers.AsEnumerable()
                                 join cn in _dbContext.tblClients.AsEnumerable()
                                 on x.clientID equals cn.clientID
                                 where (((x.dt >= fromDt) && (x.dt <= toDt)) && (cn.clientName.Contains(cname)) && (x.adjustBalance > 0))
                                 select new operation
                                 {
                                     clientID = x.clientID,
                                     clientName = cn.clientName,
                                     oldBalance = x.oldBalance == null ? 0 : x.oldBalance,
                                     adjustBalance = x.adjustBalance == null ? 0 : x.adjustBalance,
                                     remark = x.remark

                                 }).ToList();
                    rec = query.ToList();
                }
                else
                {
                    var query = (from x in _dbContext.tblClientLedgers.AsEnumerable()
                                 join cn in _dbContext.tblClients.AsEnumerable()
                                 on x.clientID equals cn.clientID
                                 where (((x.dt >= fromDt) && (x.dt <= toDt)) && (x.adjustBalance > 0))
                                 select new operation
                                 {
                                     clientID = x.clientID,
                                     clientName = cn.clientName,
                                     oldBalance = x.oldBalance == null ? 0 : x.oldBalance,
                                     adjustBalance = x.adjustBalance == null ? 0 : x.adjustBalance,
                                     remark = x.remark
                                 }).ToList();
                    rec = query.ToList();
                }
            }
            catch (Exception ex) { }
            return rec.ToList();
        }

        public GMail getGMailAccount()
        {
            GMail obj = new GMail();
            try
            {
                //var res = _dbContext.tblGmailAccounts.OrderBy(x => x.gmailID).FirstOrDefault();

                //if (res != null)
                //{
                //    obj.gmailID = res.gmailID;
                //    obj.pwd = res.pwd;
                //}

            }
            catch (Exception ex) { }
            return obj;

        }

        //public void UpdateGMailPassword(GMail obj)
        //{
        //    try
        //    {
        //        tblGmailAccount res = _dbContext.tblGmailAccounts.Where(x => x.id == 2).FirstOrDefault();
        //        if (res != null)
        //        {
        //            res.pwd = obj.pwd;
        //            _dbContext.SaveChanges();
        //        }

        //    }
        //    catch (Exception ex) { }

        //}



        public string UpdateLedger(int clientLedgerID, int amount, int receivedAmount, int newAmount, string remark)
        {
            try
            {
                int bal = 0, clientID = 0;
                var res = _dbContext.tblClientLedgers.Where(x => x.clientLadgerID == clientLedgerID).FirstOrDefault();
                if (res != null)
                {
                    clientID = res.clientID;
                    if (res.prjAmount != 0)
                    {
                        bal = res.balance - res.prjAmount;
                        res.prjAmount = newAmount;
                        res.balance = bal + newAmount;
                    }
                    else
                    {
                        bal = res.balance + res.receivedAmount;
                        res.receivedAmount = newAmount;
                        res.balance = bal - newAmount;
                    }
                    res.remark = remark.Trim();
                    bal = res.balance;
                }

                var res1 = (from cl in _dbContext.tblClientLedgers
                            where ((cl.clientLadgerID > clientLedgerID) && (cl.clientID == clientID))
                            select new operation
                            {
                                clientLedgerID = cl.clientLadgerID,
                            }).ToList();
                foreach (var item in res1)
                {
                    var obj = _dbContext.tblClientLedgers.Where(x => x.clientLadgerID == item.clientLedgerID).FirstOrDefault();
                    if (obj != null)
                    {

                        if (obj.prjAmount != 0)
                        {
                            obj.balance = bal + obj.prjAmount;
                        }
                        else
                        {
                            obj.balance = bal - obj.receivedAmount;
                        }
                        bal = obj.balance;
                    }
                }

                _dbContext.SaveChanges();


            }
            catch (Exception ex) { }
            return "";

        }


    }
}