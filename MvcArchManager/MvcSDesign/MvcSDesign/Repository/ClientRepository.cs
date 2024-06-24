using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSDesign.Models;
using MvcSDesign.EF;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Globalization;

using System.Threading;
using System.Data.SqlClient;

namespace MvcSDesign.Repository
{
    public class ClientRepository :IClient
    {
        ArchManagerDBEntities _dbContext;
        AdminRepository _IAdmn;

        public ClientRepository(ArchManagerDBEntities dEntity)
        {
            _dbContext = dEntity;
            _IAdmn = new AdminRepository(new ArchManagerDBEntities());
        }



        public  void InsertData(clientModel cnt)
        {
            tblClient obj = new tblClient();
            try
            {
                if (cnt.clientID == 0)
                {
                    var res = _dbContext.tblCompanyProfiles.FirstOrDefault();
                    int companyID = 0;
                    if (res != null)
                    {
                        companyID = res.companyID;
                    }
                    obj.companyID = companyID;
                    obj.clientName = cnt.clientName.Trim();
                    obj.orgName = cnt.orgName == null ?"-": cnt.orgName.Trim();
                    obj.address = cnt.address.Trim();
                    obj.city = cnt.city.Trim();
                    obj.phone = cnt.phone;
                    obj.mobile = cnt.mobile;
                    obj.emailID = cnt.emailID.Trim();
                    obj.state = cnt.state;
                    obj.remark = "-";
                    _dbContext.tblClients.Add(obj);
                }
                else
                {
                    

                    var res = _dbContext.tblClients.Where(x => x.clientID == cnt.clientID).FirstOrDefault();
                    if (res != null)
                    {
                        res.clientName = cnt.clientName.Trim();
                        res.orgName = cnt.orgName == null ?"-": cnt.orgName.Trim();
                        res.address = cnt.address.Trim();
                        res.city = cnt.city.Trim();
                        res.phone = cnt.phone;
                        res.mobile = cnt.mobile;
                        res.emailID = cnt.emailID.Trim();
                        res.state = cnt.state;
                        res.remark = "-";
                    }
                }
                _dbContext.SaveChanges();
            }
            catch (SqlException ex) {
                string ch = ex.Message;
            }
        }
        //public long InsertQuotation(quotation qtn)
        //{
        //    long pid = 0;
        //    try
        //    {
        //        tblProjectDetail obj = new tblProjectDetail();
        //        pid = getProjectID();
        //        obj.projectID = pid;
        //        obj.clientID = int.Parse(qtn.clientID);
        //        obj.dt = DateTime.Now;
        //        obj.projectname = qtn.projectname;
        //        obj.projectType = qtn.projectType;
        //        obj.projectLevel = qtn.projectLevel;
        //        obj.package = qtn.package;
        //        obj.plotSize = qtn.plotSize;
        //        obj.amount = qtn.amount;
        //        obj.finalizeAmount = 0;
        //        obj.status = "request";
        //        obj.remark = qtn.remark;

        //        _dbContext.tblProjectDetails.Add(obj);
        //        _dbContext.SaveChanges();
        //    }
        //    catch (Exception ex) { }
        //    return pid;
        //}

         
        //public long getProjectID()
        //{
        //   long projectID = 0;
             
        //    try
        //    {
        //       // projectID =  _dbContext.tblProjectDetails.Max(u => u.projectID);
               
        //    }
        //    catch (Exception ex) { }
        //    if (projectID == 0)
        //    {
        //       var res = _dbContext.tblAdminSettings.FirstOrDefault();
        //       if(res !=null)
        //        {
        //            projectID = res.projectID +1;

        //        }
        //    }
        //    else
        //    {
        //        projectID++;
        //    }
        //    return projectID;
        //}

       
         


        //public string SaveQuotation(operation qtn, string empdata, ref long pid)
        //{
        //    using (var context = new ArchManagerDBEntities())
        //    {
        //        using (var dbTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                tblProjectDetail obj = new tblProjectDetail();
        //                tblProjectDetailItem objItem;//= new tblProjectDetailItem();
                         
        //                var serializeData = JsonConvert.DeserializeObject<List<ProjectItemModel>>(empdata);

        //                int amount = 0;
        //                foreach (var data in serializeData)
        //                {
        //                    if ((data.amount != 0) && (data.amount != null))
        //                    {
        //                        amount += data.amount;
        //                    }
        //                }


        //                pid = getProjectID();
        //                obj.projectID = pid;
        //                obj.clientID = qtn.clientID;
        //                obj.dt = DateTime.Now;
        //                obj.service = qtn.service.Trim();
        //                obj.projectname = qtn.projectName.Trim();
        //                obj.projectType = "-";
        //                obj.projectLevel = "-";
        //                obj.package = "-";
        //                obj.plotSize = "-";
        //                obj.amount = amount;
        //                obj.finalizeAmount = amount;
        //                obj.status = "request";
        //                obj.remark = qtn.remark;

        //                _dbContext.tblProjectDetails.Add(obj);
        //                _dbContext.SaveChanges();

        //                foreach (var data in serializeData)
        //                {
        //                    objItem = new tblProjectDetailItem();

        //                    objItem.projectID = pid;
        //                    objItem.projectDetail = data.projectDetails.Trim();
        //                    objItem.service = data.services.Trim();
        //                    objItem.rate = data.rate;
        //                    objItem.area = data.area.Trim();
        //                    objItem.amount = data.amount;

        //                    context.tblProjectDetailItems.Add(objItem);
        //                    context.SaveChanges();
        //                }
        //                dbTransaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                dbTransaction.Rollback();
        //                return ex.Message;
        //            }
        //        }

        //    }
        //    return "";
        //}

        public string SaveQuotation(operation qtn, List<QuotationItemModel> li, ref int quotationNo)
        {
            using (var context = new ArchManagerDBEntities())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        tblQuotation obj = new tblQuotation();
                        tblQuotationItem objItem;//= new tblProjectDetailItem();
                       
                        int amount =li.Sum(x=>x.amount);
                        
                        quotationNo = GetQuotationNo();
                        obj.quotationNo = quotationNo;
                        obj.clientID = qtn.clientID;
                        obj.dt = DateTime.Now;
                        obj.services = qtn.service.Trim();
                        obj.projectName = qtn.projectName.Trim();
                        obj.total = amount;
                        obj.status = "request";
                        obj.remark = qtn.remark;

                        _dbContext.tblQuotations.Add(obj);
                        _dbContext.SaveChanges();

                        foreach (var item in li)
                        {
                            objItem = new tblQuotationItem();
                            objItem.quotationID = obj.quotationID;
                            objItem.projectDescription = item.projectDetails.Trim();
                            objItem.service = item.services.Trim();
                            objItem.hsn = item.hsn ==null?"-":item.hsn.Trim();
                            objItem.qty = item.qty;
                            objItem.prjUnit = item.unit;
                            objItem.rate = item.rate;
                            objItem.amount = item.rate * item.qty;

                            context.tblQuotationItems.Add(objItem);
                            context.SaveChanges();
                        }
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
        public int GetQuotationNo()
        {
           int quotationNo = 0;
             
            try
            {
                quotationNo =  _dbContext.tblQuotations.Max(u => u.quotationNo);
               
            }
            catch (Exception ex) { }
            if (quotationNo == 0)
            {
               var res = _dbContext.tblAdminSettings.FirstOrDefault();
               if(res !=null)
                {
                    quotationNo = Convert.ToInt32(res.quotationID)  ;
                }
               else
                {
                    quotationNo = 1;
                }
            }
            else
            {
                quotationNo++;
            }
            return quotationNo;
        }

        public string UpdateQuotation(operation obj, List<QuotationItemModel> itemlist)
        {
            using (var context = new ArchManagerDBEntities())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        tblQuotationItem objItem;//= new tblProjectDetailItem();
                        int amount =itemlist.Sum(x=>x.amount);
                        var res = context.tblQuotations.SingleOrDefault(x => x.quotationID == obj.quotationID);//.FirstOrDefault();
                        if (res != null)
                        {
                            res.services = obj.service.Trim();
                            res.projectName = obj.projectName.Trim();
                            res.total = amount;
                            context.Entry(res).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        }

                        var del = context.tblQuotationItems.Where(x => x.quotationID == obj.quotationID).ToList();
                        if (del != null)
                        {
                            context.tblQuotationItems.RemoveRange(del);
                            context.SaveChanges();
                        }
                        foreach (var item in itemlist)
                        {
                            objItem = new tblQuotationItem();
                            objItem.quotationID = obj.quotationID;
                            objItem.projectDescription = item.projectDetails.Trim();
                            objItem.service = item.services.Trim();
                            objItem.hsn =item.hsn == null ?"-":item.hsn;
                            objItem.qty = item.qty;
                            objItem.prjUnit = item.unit;
                            objItem.rate = item.rate;
                            objItem.amount = item.amount;
                            context.tblQuotationItems.Add(objItem);
                            context.SaveChanges();



                        }

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


         public string SaveProject(operation qtn, List<QuotationItemModel> li, ref long pid)
        {
            using (var context = new ArchManagerDBEntities())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //tblProjectDetail obj = new tblProjectDetail();
                        //tblProjectDetailItem objItem;//= new tblProjectDetailItem();

                        //var serializeData = JsonConvert.DeserializeObject<List<ProjectItemModel>>(empdata);

                        int amount = 0;
                        foreach (var data in li)
                        {
                            //if ((data.amount != 0) && (data.amount != null))
                            //{
                            //    amount += data.amount;
                            //}
                            amount += data.amount;
                        }


                        //pid = getProjectID();
                        //obj.projectID = pid;
                        //obj.clientID = qtn.clientID;
                        //obj.dt = DateTime.Now;
                        //obj.service = qtn.service.Trim();
                        //obj.projectname = qtn.projectName.Trim();
                        //obj.projectType = "-";
                        //obj.projectLevel = "-";
                        //obj.package = "-";
                        //obj.plotSize = "-";
                        //obj.amount = amount;
                        //obj.finalizeAmount = amount;
                        //obj.status = "request";
                        //obj.remark = qtn.remark;

                        //_dbContext.tblProjectDetails.Add(obj);
                        //_dbContext.SaveChanges();

                        foreach (var data in li)
                        {
                          //  objItem = new tblProjectDetailItem();

                            //objItem.projectID = pid;
                            //objItem.projectDetail = data.projectDetails.Trim();
                            //objItem.service = data.services.Trim();
                            //objItem.rate = data.rate;
                            //objItem.area = data.area.Trim();
                            //objItem.amount = data.amount;

                            //context.tblProjectDetailItems.Add(objItem);
                            context.SaveChanges();
                        }
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

        public bool ClientEmailValidation(string mailID)
        {
            bool flag = true;
            try
            {
                tblClient obj = _dbContext.tblClients.SingleOrDefault(x => x.emailID == mailID);
                string ch = obj.clientName;
                flag = false;
            }
            catch (Exception ex) { }
            return flag;
             
        }
       
        public string ClientNameValidation(clientModel obj)
        {
            //string name, string emailID
            try
            {
                var res = _dbContext.tblClients.Where(x => ((x.clientName == obj.clientName)&&(x.clientID != obj.clientID))).FirstOrDefault();
                if (res != null)
                {
                    return "Client name already exist";
                }
                var res1 = _dbContext.tblClients.Where(x => ((x.emailID == obj.emailID) && (x.clientID != obj.clientID))).FirstOrDefault();
                if (res1 != null)
                {
                    return "Client's emailID already exist";
                }
            }
            catch (Exception ex) { return ex.Message; }
            return "";
        }
        
        public IEnumerable<clientModel> SearchClientByName(string name)
        {
            List<clientModel> lst = new List<clientModel>();
            try
            {
                var res = (from item in _dbContext.tblClients
                           where (item.clientName.Contains(name))
                           select new clientModel
                           {
                               clientID = item.clientID,
                               clientName = item.clientName,
                               orgName = item.orgName,
                               address = item.address,
                               city = item.city,
                               mobile = item.mobile,
                               phone = item.phone,
                               emailID = item.emailID,
                               state = item.state
                           }).ToList();
                lst = res.ToList();
            }
            catch (Exception ex) { }
            return lst;
        }


        public IEnumerable<clientModel> SearchClientByNameOrPID(string opt, string name, string pid, string projectName)
        { 
            List<clientModel> lst = new List<clientModel>();
            try
            {
                if (opt == "name")
                { 
                    var res = ( from item in _dbContext.tblClients
                                where (item.clientName.Contains(name))
                                select new clientModel
                                   {
                                       clientID = item.clientID,
                                       clientName = item.clientName,
                                       orgName = item.orgName,
                                       address = item.address,
                                       city = item.city,
                                       mobile = item.mobile,
                                       phone = item.phone,
                                       emailID = item.emailID,
                                       state = item.state
                                   }).ToList();
                      lst = res.ToList();
                }

                else if (opt == "projectID")
                {
                    int varpid = int.Parse(pid);
                    //var res = ( from item in _dbContext.tblClients
                    //            join pd in _dbContext.tblProjectDetails
                    //            on item.clientID equals pd.clientID
                    //            where (pd.projectID == varpid)
                    //            select new clientModel
                    //            {
                    //               clientID = item.clientID,
                    //               clientName = item.clientName,
                    //               orgName = item.orgName,
                    //               address = item.address,
                    //               city = item.city,
                    //               mobile = item.mobile,
                    //               phone = item.phone,
                    //               emailID = item.emailID,
                    //               state = item.state

                    //           }).ToList();
                    //lst = res.ToList();
                }
                else if (opt == "projectName")
                {
                    //var res = (from item in _dbContext.tblClients
                    //           join pd in _dbContext.tblProjectDetails
                    //           on item.clientID equals pd.clientID
                    //           where (pd.projectname.Contains(projectName))
                    //           select new clientModel
                    //           {
                    //               clientID = item.clientID,
                    //               clientName = item.clientName,
                    //               orgName = item.orgName,
                    //               address = item.address,
                    //               city = item.city,
                    //               mobile = item.mobile,
                    //               phone = item.phone,
                    //               emailID = item.emailID,
                    //               state = item.state

                    //           }).ToList();
                    //lst = res.ToList();

                }


            }
            catch (Exception ex) { }
            return lst;
        }


        public IEnumerable<clientModel> GetState()
        {
            List<clientModel> lst = new List<clientModel>();
            try
            {
                lst = (from st in _dbContext.tblStates
                       select new clientModel
                       {
                           state = st.stateName
                       }).ToList();

            }
            catch (Exception ex) { }
            return lst;
        }
        public IEnumerable<tblClient> getByName(string name)
        {
            return _dbContext.tblClients.Where(s => s.clientName.Contains(name)).ToList();
        }

        public IEnumerable<clientModel> GetAll()
        {
            List<clientModel> lst = new List<clientModel>();
            try
            {
                 lst = ( from cm in _dbContext.tblClients
                         orderby cm.clientID descending
                         select new clientModel
                            {
                                clientID = cm.clientID,
                                clientName = cm.clientName,
                                orgName = cm.orgName,
                                address = cm.address,
                                city = cm.city,
                                mobile = cm.mobile,
                                phone = cm.phone,
                                emailID = cm.emailID,
                                state = cm.state
                            }).Take(10).ToList();
               
            }
            catch (Exception ex) { }

            return lst;

        }

        public IEnumerable<clientModel> getClient_PromMail(string name, string city)
        {

            List<clientModel> clist = new List<clientModel>();
            try
            {
               
                if ((name != null) && (city != null))
                {
                    var varlist = (from cl in _dbContext.tblClients
                                   where ((cl.clientName.Contains(name)) && (cl.city.Contains(city)))
                                   select new clientModel
                                   {
                                       clientID = cl.clientID,
                                       clientName = cl.clientName,
                                       city = cl.city,
                                       state = cl.state,
                                       mobile = cl.mobile,
                                       emailID = cl.mobile
                                   }).ToList();

                    clist = varlist.ToList();
                }
                else if (name != null)
                {
                    var varlist = (from cl in _dbContext.tblClients
                                   where (cl.clientName.Contains(name))
                                   select new clientModel
                                   {
                                       clientID = cl.clientID,
                                       clientName = cl.clientName,
                                       city = cl.city,
                                       state = cl.state,
                                       mobile = cl.mobile,
                                       emailID = cl.mobile
                                   }).ToList();

                    clist = varlist.ToList();
                }
                else if (city != null)
                {
                    var varlist = (from cl in _dbContext.tblClients
                                   where (cl.city.Contains(city))
                                   select new clientModel
                                   {
                                       clientID = cl.clientID,
                                       clientName = cl.clientName,
                                       city = cl.city,
                                       state = cl.state,
                                       mobile = cl.mobile,
                                       emailID = cl.mobile
                                   }).ToList();

                    clist = varlist.ToList();
                }

            }
            catch (Exception ex) { }

            return clist;
        }

        
        public clientModel GetClient(int clientID)
        {
            clientModel clist = new clientModel();
            try
            {
                var cnt = _dbContext.tblClients.Where(x => x.clientID == clientID).FirstOrDefault();
                if (cnt != null)
                {
                    clist.clientName = cnt.clientName;
                    clist.orgName = cnt.orgName;
                    clist.address = cnt.address;
                    clist.city = cnt.city;
                    clist.state = cnt.state;
                    clist.phone = cnt.phone;
                    clist.mobile = cnt.mobile;
                    clist.emailID = cnt.emailID;
                }
            }
            catch (Exception ex) { }
           return clist;
        }

        public string Update(clientModel cnt)
       {
           try
           {
                var obj = _dbContext.tblClients.SingleOrDefault(x => x.clientID == cnt.clientID);
                obj.clientName = cnt.clientName;
                obj.orgName = cnt.orgName;
                obj.address = cnt.address;
                obj.city = cnt.city;
                obj.state = cnt.state;
                obj.phone = cnt.phone;
                obj.mobile = cnt.mobile;
                obj.emailID = cnt.emailID;

                _dbContext.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
            }
           catch (Exception ex) { return ex.Message; }
           return "";

       }

       public string DeleteClient(int clientID)
       {
           try
           {
                var obj = _dbContext.tblClients.SingleOrDefault(x => x.clientID == clientID);
                _dbContext.tblClients.Remove(obj);
                _dbContext.SaveChanges();

            }
           catch (Exception ex) { return ex.Message; }

           return "";

       }

        public IEnumerable<operation>  getMonthQuotation( string ptype)
        {
            int mnt = DateTime.Now.Month;
            int yr = DateTime.Now.Year;
            int i = 1;
            List<operation> rec = new List<operation>();
            string varstatus = "";
            try
            {
                
                if (ptype == "All")
                {
                    //var prj = from pd in _dbContext.tblProjectDetails
                    //          join cl in _dbContext.tblClients
                    //          on pd.clientID equals cl.clientID
                    //          where ((pd.dt.Month == mnt) && (pd.dt.Year == yr))
                    //          select new
                    //          {
                    //              clientid = cl.clientID,
                    //              clientName = cl.clientName,
                    //              projectName = pd.projectname,
                    //              projectID = pd.projectID,
                    //              projectType = pd.projectType,
                    //              package = pd.package,
                    //              projectLevel = pd.projectLevel,
                    //              plotSize = pd.plotSize,
                    //              amount = pd.amount,
                    //              remark = pd.remark,
                    //              status = pd.status

                    //          };
                    //foreach (var item in prj)
                    //{
                    //    //
                    //    if (item.status == "request")
                    //        varstatus = "Pending";
                    //    else
                    //        varstatus = "Confirm";
                    //    rec.Add(new operation
                    //    {
                    //        sno = i,
                    //        clientID = item.clientid,
                    //        projectID = item.projectID,
                    //        clientName = item.clientName,
                    //        projectName = item.projectName,
                    //        projectType = item.projectType,
                    //        package = item.package,
                    //        projectLevel = item.projectLevel,
                    //        plotSize = item.plotSize,
                    //        amount = item.amount,
                    //        status = varstatus,
                    //        remark = item.remark

                    //    });
                    //    i++;
                    //}

                }
                else
                {
                    //var prj = from pd in _dbContext.tblProjectDetails
                    //          join cl in _dbContext.tblClients
                    //          on pd.clientID equals cl.clientID
                    //          where ((pd.dt.Month == mnt) && (pd.dt.Year == yr) && (pd.projectType == ptype))
                    //          select new
                    //          {
                    //              clientid = cl.clientID,
                    //              clientName = cl.clientName,
                    //              projectName = pd.projectname,
                    //              projectID = pd.projectID,
                    //              projectType = pd.projectType,
                    //              package = pd.package,
                    //              projectLevel = pd.projectLevel,
                    //              plotSize = pd.plotSize,
                    //              amount = pd.amount,
                    //              remark = pd.remark,
                    //              status = pd.status

                    //          };
                    //foreach (var item in prj)
                    //{
                    //    if (item.status == "request")
                    //        varstatus = "Pending";
                    //    else
                    //        varstatus = "Confirm";
                    //    rec.Add(new operation
                    //    {
                    //        sno = i,
                    //        clientID = item.clientid,
                    //        projectID = item.projectID,
                    //        clientName = item.clientName,
                    //        projectName = item.projectName,
                    //        projectType = item.projectType,
                    //        package = item.package,
                    //        projectLevel = item.projectLevel,
                    //        plotSize = item.plotSize,
                    //        amount = item.amount,
                    //        status = varstatus,
                    //        remark = item.remark

                    //    });
                        i++;
                   // }
                }
            }
            catch (Exception ex) { }

            return rec;
        }
        public IEnumerable<operation> Dashboard_getMonthQuotation()
        {
            List<operation> rec = new List<operation>();
            DateTime dt = DateTime.Now.AddDays(-6);

            try
            {
                //var prj = (from pd in _dbContext.tblProjectDetails
                //           join cl in _dbContext.tblClients
                //           on pd.clientID equals cl.clientID
                //           where (pd.dt >= dt)
                //           select new operation
                //           {
                //               amount = pd.amount,
                //               status = pd.status
                //           }).ToList();
                //rec = prj.ToList();
            }
            catch (Exception ex) { }

            return rec;
        }
        public operation GetQuotationDetail(int pid)
        {
           operation reqlist = new operation();
            try
            {
                 
                reqlist = (from pd in _dbContext.tblQuotations
                           join cl in _dbContext.tblClients
                           on pd.clientID equals cl.clientID
                           where ( pd.quotationID == pid)
                           select new operation
                           {
                               dt = pd.dt,
                               clientID = cl.clientID,
                               clientName = cl.clientName,
                               emailID = cl.emailID,
                               address = cl.address,
                               city = cl.city + " , " + cl.state,
                               projectName = pd.projectName,
                               amount = pd.total,
                               remark = pd.remark,
                               status = pd.status,
                               service = pd.services,
                               quotationID = pd.quotationID,
                               quotationNo = pd.quotationNo
                           }).FirstOrDefault();

            }
            catch (Exception ex) { }
            return reqlist;
        }
        public IEnumerable<QuotationItemModel> GetQuotationDetailItem(int quNo)
        {
            List<QuotationItemModel> reqlist = new List<QuotationItemModel>();
            try
            {
                reqlist = (from qt in _dbContext.tblQuotations
                           join pd in _dbContext.tblQuotationItems
                           on qt.quotationID equals pd.quotationID
                           where (qt.quotationNo == quNo)
                           select new QuotationItemModel
                           {
                               itemID = pd.quotationItemID,
                               projectDetails = pd.projectDescription,
                               hsn = pd.hsn,
                               qty = pd.qty,
                               unit = pd.prjUnit,
                               rate = pd.rate,
                               amount = (int)pd.amount,
                               services = pd.service,
                           }).ToList();

            }
            catch (Exception ex) { }
            return reqlist;
        }


    }
}