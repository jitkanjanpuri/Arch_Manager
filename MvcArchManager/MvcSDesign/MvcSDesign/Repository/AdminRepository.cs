using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using MvcSDesign.EF;
using MvcSDesign.Models;
using MvcSDesign.Repository;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace MvcSDesign.Repository
{
    public class AdminRepository :IAdmin
    {
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
                    rec.phoneno = model.phone.Trim();
                    rec.emailID = model.emailID.Trim();
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


        public void InsertRegistration(staff st)
        {
            try
            {
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
                _dbContext.tblStaffs.Add(tsf);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public IEnumerable<tblStaff> SearchRegistration(string name)
        {
            return _dbContext.tblStaffs.Where(s => s.name.Contains(name)).ToList();
        }

        public staff getLogin(logincls lgn)
        {
            staff obj = new staff();
            try
            {
                var rec = _dbContext.tblStaffs.Where(st => (st.username == lgn.username) && (st.password == lgn.pwd)).FirstOrDefault();
                if (rec == null)
                    return null;
                obj.name = rec.name;
                obj.rolltype = rec.rolltype;
                obj.staffID = rec.staffID;

            }
            catch (Exception ex) { }
            return obj;
        }

        public string RegistrationUpdate(staff obj)
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
                        pd.amount = famount;
                        pd.status = "estimated";
                        pd.projectlocation = projectlocation;
                        cid = pd.clientID;


                        context.Entry(pd).State = System.Data.Entity.EntityState.Modified;
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


        public string SavePRF(PRFModel model)
        {
            tblPRF obj = new tblPRF();
            try
            {
                obj.projectID = model.projectID;
                obj.workingStatus =model.workingStatus;
                obj.slabdetail = "-";
                obj.slabheight =model.slabheight;
                obj.plinthheight =model.plinthheight;
                obj.porchheight = model.porchheight;
                obj.elevationpattern = model.elevationpattern;
                //obj.totalfloor = "-";
                obj.towerroom = model.towerroom;
                obj.cornerplotplan = model.cornerplotplan;
                obj.plotside = model.plotside;
                obj.boundrywall = model.boundrywall;
                obj.doorlintel = model.doorlintel;
                obj.windowsill =model.windowsill;
                obj.windowlintel =model.windowlintel;
                obj.anyother = model.anyother;

                _dbContext.tblPRFs.Add(obj);
                _dbContext.SaveChanges();
 
            }
            catch(SqlException ex)
            {
                string ch= ex.Message;
                return ch;
            }
            
            
            return "";
        }

        public IEnumerable<tblStaff> getOperationDesigner()
        {
            return _dbContext.tblStaffs.ToList().OrderBy(x => x.name);
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

        public IEnumerable<operation> getCurrentWorkingList(string dname)
        {
            List<operation> prjlist = new List<operation>();
            string diffDt, color = "";
            int l;
            try
            {
                if ((dname == null) || (dname == "0"))
                {

                    var query = from pl in _dbContext.tblProjectDetails
                                join cl in _dbContext.tblClients on pl.clientID equals cl.clientID
                                join cw in _dbContext.tblOperations on pl.projectID equals cw.projectID
                                join sf in _dbContext.tblStaffs on cw.staffID equals sf.staffID
                                where (cw.status == "WRC")
                                select new
                                {
                                    dt = cw.dt,
                                    clientID = cl.clientID,
                                    clientName = cl.clientName,
                                    projectID = pl.projectID,
                                    projectType = pl.projectType,
                                    projectCategory = cw.projectCategory,
                                    package = pl.package,
                                    projectLevel = pl.projectLevel,
                                    plotSize = pl.plotSize,
                                    amount = pl.amount,
                                    name = sf.name,
                                    remark = cw.remark,
                                    operationID = cw.operationID
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
                            operationID = item.operationID,
                            clientID = item.clientID,
                            projectID = item.projectID,
                            clientName = item.clientName,
                            projectType = item.projectType,
                            package = item.package,
                            projectLevel = item.projectLevel,
                            projectCategory = item.projectCategory,
                            plotSize = item.plotSize,
                            amount = item.amount,
                            designerName = item.name,
                            remark = item.remark,
                            rowcolor = color

                        });
                        i++;
                    }

                }
                else
                {

                    int sid = int.Parse(dname);
                    var query = from pl in _dbContext.tblProjectDetails
                                join cl in _dbContext.tblClients on pl.clientID equals cl.clientID
                                join cw in _dbContext.tblOperations on pl.projectID equals cw.projectID
                                join sf in _dbContext.tblStaffs on cw.staffID equals sf.staffID
                                where ((cw.status == "WRC") && (cw.staffID == sid))
                                select new
                                {
                                    dt = cw.dt,
                                    clientID = cl.clientID,
                                    clientName = cl.clientName,
                                    projectID = pl.projectID,
                                    projectType = pl.projectType,
                                    projectCategory = cw.projectCategory,
                                    package = pl.package,
                                    projectLevel = pl.projectLevel,
                                    plotSize = pl.plotSize,
                                    amount = pl.amount,
                                    name = sf.name,
                                    remark = cw.remark,
                                    operationID = cw.operationID
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
                            clientID = item.clientID,
                            projectID = item.projectID,
                            clientName = item.clientName,
                            projectType = item.projectType,
                            package = item.package,
                            projectLevel = item.projectLevel,
                            projectCategory = item.projectCategory,
                            plotSize = item.plotSize,
                            amount = item.amount,
                            designerName = item.name,
                            remark = item.remark,
                            rowcolor = color,
                            operationID = item.operationID,

                        });
                        i++;
                    }
                   }
                }
            catch (Exception ex) { }
            return prjlist;
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

        public string SaveNewProject(int projectID, string pcategory, string dname, int amount)
        {
            tblOperation objOptn = new tblOperation();
            try
            {
                int tmp = 0;
                // check for project already in queue for same category

                //(cw.status.ToLower() != "complete")
                var rec = from cw in _dbContext.tblOperations.Where(cw => (cw.projectID == projectID) && (cw.projectCategory == pcategory) && (cw.status.ToLower() != "complete"))
                          select new
                          {
                              operationID = cw.operationID
                          };

                try
                {
                    foreach (var item in rec)
                    {
                        tmp = item.operationID;
                    }
                }
                catch (Exception ex) { }
                if (tmp != 0) return "Project already in queue for same category";

                objOptn.dt = DateTime.Now;
                objOptn.projectID = projectID;
                objOptn.staffID = int.Parse(dname);
                objOptn.designerAmount = amount;
                objOptn.projectCategory = pcategory;
                objOptn.status = "WRC";
                objOptn.payStatus = "pending";

                _dbContext.tblOperations.Add(objOptn);
                _dbContext.SaveChanges();

            }
            catch (Exception ex) { return ex.Message; }

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
                        operationID = item.operationID,
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


        //Amount receive from Client
        public string AmountReceive(int cid, string amount, string remark)
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
                        obj.clientID = cid;
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

                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                        return ex.Message;
                    }
                }
            }

            return "Success";
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
        public IEnumerable<client> RptClientLedger(string cname)
        {
            List<client> obj = new List<client>();
            int i = 1;
            try
            {
                int balance = 0;
                if (cname == null) cname = "";
                CultureInfo cinfo = new CultureInfo("en-US");

                if ((cname == null) || (cname.Trim() == ""))
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
                    //    balance = 0;
                    //    try
                    //    {
                    //        tblClientLedger cl = _dbContext.tblClientLedgers.Where(x => x.clientID == item.clientID).OrderByDescending(x => x.clientLadgerID).First();
                    //        balance = cl.balance;
                    //    }
                    //    catch (Exception ex) { }

                    //    if (balance != 0)
                    //    {
                    //        obj.Add(new client
                    //        {
                    //            sno = i,
                    //            clientID = item.clientID,
                    //            clientName = item.clientName,
                    //            city = item.city,
                    //            state = item.state,
                    //            mobile = balance.ToString()
                    //        });
                    //        i++;
                    //    }
                    //}
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
                    //    balance = 0;
                    //    try
                    //    {
                    //        tblClientLedger cl = _dbContext.tblClientLedgers.Where(x => x.clientID == item.clientID).OrderByDescending(x => x.clientLadgerID).First();
                    //        balance = cl.balance;
                    //    }
                    //    catch (Exception ex) { }

                    //    obj.Add(new client
                    //    {
                    //        sno = i,
                    //        clientID = item.clientID,
                    //        clientName = item.clientName,
                    //        city = item.city,
                    //        state = item.state,
                    //        mobile = balance.ToString()
                    //    });
                    //    i++;

                    //}
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

                //var query = from cl in _dbContext.tblClientLedgers
                //            join pd in _dbContext.tblProjectDetails on cl.projectID equals pd.projectID into pdetail
                //            from pd in pdetail.DefaultIfEmpty()
                //            where ((cl.dt >= dt1) && (cl.dt <= dt2) && (cl.clientID == clientID))
                //            select new
                //            {
                //                clientID = cl.clientID,

                //                projectID = (pd == null ? 0 : pd.projectID),
                //                dt = cl.dt,
                //                projectname = pd.projectname,
                //                projectType = pd.projectType,
                //                package = pd.package,
                //                amount = cl.prjAmount,
                //                receive = cl.receivedAmount,
                //                balance = cl.balance,
                //                remark = cl.remark
                //            };

                //clientDetailRecord = new DataTable();
                //clientDetailRecord.Columns.Add("projectID");
                //clientDetailRecord.Columns.Add("dt");
                //clientDetailRecord.Columns.Add("pname");
                //clientDetailRecord.Columns.Add("ptype");
                //clientDetailRecord.Columns.Add("package");
                //clientDetailRecord.Columns.Add("amount");
                //clientDetailRecord.Columns.Add("receive");
                //clientDetailRecord.Columns.Add("balance");
                //clientDetailRecord.Columns.Add("remark");
                //foreach (var item in query)
                //{
                //    dr = clientDetailRecord.NewRow();
                //    obj.Add(new operation
                //    {
                //        projectID = item.projectID,
                //        status = item.dt.ToString("dd-MMM-yyyy"),
                //        projectName = item.projectname,
                //        projectType = item.projectType,
                //        package = item.package,
                //        amount = item.amount,
                //        receivedAmount = item.receive,
                //        balance = item.balance,
                //        remark = item.remark


                //    });
                //    dr[0] = item.projectID;
                //    dr[1] = item.dt;
                //    dr[2] = item.projectname;
                //    dr[3] = item.projectType;
                //    dr[4] = item.package;
                //    dr[5] = item.amount;
                //    dr[6] = item.receive;
                //    dr[7] = item.balance;
                //    dr[8] = item.remark;
                //    clientDetailRecord.Rows.Add(dr);
                //}



            }
            catch (Exception ex) { }
            return obj;

        }



        public IEnumerable<operation> RptClientReceive(string cname, string fromDt, string toDt)
        {
            List<operation> obj = new List<operation>();
            try
            {
                CultureInfo cinfo = new CultureInfo("en-US");
                DateTime dt1 = DateTime.Parse(fromDt, cinfo);
                DateTime dt2 = DateTime.Parse(toDt, cinfo);
                dt2 = dt2.AddDays(1);


                int amount = 0, i = 1, amt = 0;
                if ((cname == null) || (cname == ""))
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
                        amount = 0;
                        try
                        {
                            var cl = _dbContext.tblClientLedgers.Where(x => ((x.clientID == item.clientID) && ((x.dt >= dt1) && (x.dt <= dt2)))).ToList();
                            foreach (var item1 in cl)
                            {
                                amount += item1.receivedAmount;
                            }
                        }
                        catch (Exception ex) { }

                        if (amount != 0)
                        {
                            amt += amount;
                            obj.Add(new operation
                            {
                                sno = i,
                                clientID = item.clientID,
                                clientName = item.clientName,
                                city = item.city,
                                status = item.state,
                                receivedAmount = amount,
                                balance = amt
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
                        amount = 0;
                        try
                        {
                            var cl = _dbContext.tblClientLedgers.Where(x => ((x.clientID == item.clientID) && ((x.dt >= dt1) && (x.dt <= dt2)))).ToList();
                            foreach (var item1 in cl)
                            {
                                amount += item1.receivedAmount;
                            }
                        }
                        catch (Exception ex) { }

                        if (amount != 0)
                        {
                            amt += amount;
                            obj.Add(new operation
                            {
                                sno = i,
                                clientID = item.clientID,
                                clientName = item.clientName,
                                city = item.city,
                                status = item.state,
                                receivedAmount = amount,
                                balance = amt
                            });
                            i++;
                        }
                    }
                }

            }
            catch (Exception ex) { }
            return obj.ToList();

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


        public IEnumerable<staff> RptDesignerLedger(string dname)
        {
            List<staff> obj = new List<staff>();
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
                        obj.Add(new staff
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
                        obj.Add(new staff
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
            List<operation> prjType = new List<operation>();
            try
            {
                DateTime dt2 = DateTime.Today;
                DateTime dt1 = dt2.AddDays(-4);

                dt2 = DateTime.Today.AddDays(1);
                string[] arr = new string[] { "" };
                string projectTypeItems = "", allQty = "";
                bool flag = false;


                string name;
                arr = new string[] { "Elevation", "Revised Elevation", "Dratf View", "Revised View", "Final View", "Interior", "3D Floor Plan" };
                projectTypeItems = "Elevation, Revised Elevation, Dratf View, Revised View, Final View, Interior,3D Floor Plan";


                var res = (from st in _dbContext.tblStaffs
                           where (st.rolltype == "User")
                           select new staff
                           {
                               staffID = st.staffID,
                               name = st.name,
                           }).ToList();

                foreach (var item in res)
                {
                    var res1 = (from p in _dbContext.tblOperations
                                where ((p.staffID == item.staffID) && ((p.dt >= dt1) && (p.dt <= dt2)))
                                group p by p.projectCategory into g
                                select new operation
                                {
                                    sno = g.Count(),
                                    projectCategory = g.Key
                                }).ToList();
                    name = item.name;
                    allQty = "";
                    for (int i = 0; i < arr.Length; i++)
                    {

                        flag = false;
                        foreach (var itemOp in res1)
                        {
                            if (arr[i] == itemOp.projectCategory)
                            {
                                if (allQty.Length == 0)
                                    allQty = itemOp.sno.ToString();
                                else
                                    allQty = allQty + "," + itemOp.sno.ToString();

                                flag = true;
                            }
                        }
                        if (allQty.Length == 0)
                            allQty = "0";
                        else
                            allQty = allQty + "," + "0";


                    }

                    prjType.Add(
                                new operation
                                {
                                    clientName = name,
                                    projectType = projectTypeItems,
                                    package = allQty,

                                }
                           );
                }

            }
            catch (Exception ex) { }

            return prjType;

        }


        public List<staff> getTopPerformers()
        {
            List<staff> obj = new List<staff>();

            try
            {
                System.DateTime dt2 = System.DateTime.Today;
                System.DateTime dt1 = dt2.AddDays(-30);// 30 days 
                dt2 = System.DateTime.Now.AddDays(1);


                var res = (from st in _dbContext.tblStaffs
                           where (st.rolltype == "User")
                           select new staff
                           {
                               staffID = st.staffID,
                               name = st.name,
                           }).ToList();



                foreach (var item in res)
                {

                    var res1 = (from p in _dbContext.tblOperations
                                where ((p.staffID == item.staffID) && ((p.dt >= dt1) && (p.dt <= dt2)) && (p.status == "complete"))
                                select new operation
                                {
                                    projectID = p.projectID
                                }).ToList();


                    if (res1.Count > 0)
                    {
                        obj.Add(
                              new staff
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

        public IEnumerable<operation> RptQuotation(string ptype, string dt1, string dt2, string searchOpt, string projectID, string cname)
        {
            List<operation> reqlist = new List<operation>();
            CultureInfo cinfo = new CultureInfo("en-US");
            DateTime fromDt = DateTime.Parse(dt1, cinfo);
            DateTime toDt = DateTime.Parse(dt2, cinfo);
            toDt = toDt.AddDays(1);
            int amt = 0;
            try
            {
                if (searchOpt == "projectType")
                {
                    if (ptype == "All")
                    {

                        var prj = from pd in _dbContext.tblProjectDetails
                                  join cl in _dbContext.tblClients
                                  on pd.clientID equals cl.clientID
                                  where ((pd.dt >= fromDt) && (pd.dt <= toDt))
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
                                      remark = pd.remark,
                                  };

                        int i = 1;

                        foreach (var item in prj)
                        {
                            amt += item.amount;
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
                                remark = item.remark,
                                balance = amt,
                            });
                            i++;
                        }



                    }

                    else
                    {
                        var prj = from pd in _dbContext.tblProjectDetails
                                  join cl in _dbContext.tblClients
                                  on pd.clientID equals cl.clientID
                                  where ((pd.projectType == ptype) && ((pd.dt >= fromDt) && (pd.dt <= toDt)))
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
                            amt += item.amount;
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
                                remark = item.remark,
                                balance = amt,

                            });
                            i++;
                        }
                    }
                }
                else if (searchOpt == "projectID")
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
                                  package = pd.package,
                                  projectLevel = pd.projectLevel,
                                  plotSize = pd.plotSize,
                                  amount = pd.amount,
                                  remark = pd.remark
                              };
                    int i = 1;

                    foreach (var item in prj)
                    {
                        amt += item.amount;
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
                            remark = item.remark,
                            balance = amt
                        });
                        i++;
                    }
                }

                else if (searchOpt == "clientName")
                {
                    var prj = from pd in _dbContext.tblProjectDetails
                              join cl in _dbContext.tblClients
                              on pd.clientID equals cl.clientID
                              where (cl.clientName.Contains(cname))
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
                        amt += item.amount;
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
                            remark = item.remark,
                            balance = amt
                        });
                        i++;
                    }

                }


            }
            catch (Exception ex) { }

            return reqlist.ToList();

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

        public void UpdateGMailPassword(GMail obj)
        {
            try
            {
                tblGmailAccount res = _dbContext.tblGmailAccounts.Where(x => x.id == 2).FirstOrDefault();
                if (res != null)
                {
                    res.pwd = obj.pwd;
                    _dbContext.SaveChanges();
                }

            }
            catch (Exception ex) { }

        }



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