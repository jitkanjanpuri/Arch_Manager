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

        public ClientRepository(ArchManagerDBEntities dEntity)
        {
            _dbContext = dEntity;
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
                    obj.orgName = cnt.orgName.Trim();
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
                    //var res = _dbContext.tblCompanyProfiles.FirstOrDefault();
                    //int companyID = 0;
                    //if (res != null)
                    //{
                    //    companyID = res.companyID;
                    //}
                    //obj.companyID = companyID;

                    var res = _dbContext.tblClients.Where(x => x.clientID == cnt.clientID).FirstOrDefault();
                    if (res != null)
                    {
                        res.clientName = cnt.clientName.Trim();
                        res.orgName = cnt.orgName.Trim();
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
        public long InsertQuotation(quotation qtn)
        {
            long pid = 0;
            try
            {
                tblProjectDetail obj = new tblProjectDetail();
                pid = getProjectID();
                obj.projectID = pid;
                obj.clientID = int.Parse(qtn.clientID);
                obj.dt = DateTime.Now;
                obj.projectname = qtn.projectname;
                obj.projectType = qtn.projectType;
                obj.projectLevel = qtn.projectLevel;
                obj.package = qtn.package;
                obj.plotSize = qtn.plotSize;
                obj.amount = qtn.amount;
                obj.finalizeAmount = 0;
                obj.status = "request";
                obj.remark = qtn.remark;

                _dbContext.tblProjectDetails.Add(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) { }
            return pid;
        }

         
        public long getProjectID()
        {
            long projectID = 1000;
             
            try
            {
                projectID = _dbContext.tblProjectDetails.Max(u => u.projectID);
                projectID++;
            }
            catch (Exception ex) { }

            return projectID;
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
        //public string SendPromoMail(string title, string msg, string[] clientList)
        //{

        //    string gMailAccount, password, clientname, clientMailId = "",str;
        //    try
        //    {
        //        CultureInfo CInfo = Thread.CurrentThread.CurrentCulture;
        //        //get gmail account for send mail
        //        var rec = _dbContext.tblGmailAccount.FirstOrDefault ();
        //        gMailAccount =  rec.gmailID;
        //        password =  rec.pwd;
        //        NetworkCredential loginInfo = new NetworkCredential(gMailAccount, password);
        //        MailMessage objMail = new MailMessage();
        //        int i,id;
        //        clientname = "";
        //        clientMailId = "";

        //        for (i= 0;i< clientList.Length;i++)
        //        {
        //            id = int.Parse(clientList[i]);
        //            var tmp = _dbContext.tblClient.Where(x => x.clientID == id).FirstOrDefault<tblClient>();
        //            if(tmp !=null)
        //            {
        //                clientname = tmp.clientName;
        //                clientMailId = tmp.emailID;
        //            }
        //        }

        //        objMail.To.Add(new MailAddress(clientMailId));
            
        //        TextInfo TInfo = CInfo.TextInfo;
        //        clientname = TInfo.ToTitleCase(clientname);

        //        objMail.Subject = title;

        //        str = "<br /><br />Dear " + clientname + "  <br /> <br /> <br /> <br />";
                 
        //        str +=  msg;

        //        str += "<br /><br /> Thanks & Regards <br /> ";

        //        str += " <br />  Satyam Design "  ;
        //        str += "<br /><br />    Tel   : +91-731-4977407 , <br /> Cell : +91-96919-06670" +
        //              "<br /><br />         Email : design.satyam@gmail.com";
               

              
        //        objMail.From = new MailAddress(gMailAccount);
        //        objMail.IsBodyHtml = true;
        //        objMail.Priority = MailPriority.High;

               

        //        objMail.Body = str;

        //        SmtpClient client = new SmtpClient("smtp.gmail.com");
        //        client.EnableSsl = true;
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = loginInfo;
        //        client.Send(objMail);

        //    }
        //    catch (Exception ex) { return ex.Message; }
        //    return "";
        //}

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
                    var res = ( from item in _dbContext.tblClients
                                join pd in _dbContext.tblProjectDetails
                                on item.clientID equals pd.clientID
                                where (pd.projectID == varpid)
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
                else if (opt == "projectName")
                {
                    var res = (from item in _dbContext.tblClients
                               join pd in _dbContext.tblProjectDetails
                               on item.clientID equals pd.clientID
                               where (pd.projectname.Contains(projectName))
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
                    var prj = from pd in _dbContext.tblProjectDetails
                              join cl in _dbContext.tblClients
                              on pd.clientID equals cl.clientID
                              where ((pd.dt.Month == mnt) && (pd.dt.Year == yr))
                              select new
                              {
                                  clientid = cl.clientID,
                                  clientName = cl.clientName,
                                  projectName = pd.projectname,
                                  projectID = pd.projectID,
                                  projectType = pd.projectType,
                                  package = pd.package,
                                  projectLevel = pd.projectLevel,
                                  plotSize = pd.plotSize,
                                  amount = pd.amount,
                                  remark = pd.remark,
                                  status = pd.status

                              };
                    foreach (var item in prj)
                    {
                        //
                        if (item.status == "request")
                            varstatus = "Pending";
                        else
                            varstatus = "Confirm";
                        rec.Add(new operation
                        {
                            sno = i,
                            clientID = item.clientid,
                            projectID = item.projectID,
                            clientName = item.clientName,
                            projectName = item.projectName,
                            projectType = item.projectType,
                            package = item.package,
                            projectLevel = item.projectLevel,
                            plotSize = item.plotSize,
                            amount = item.amount,
                            status = varstatus,
                            remark = item.remark

                        });
                        i++;
                    }

                }
                else
                {
                    var prj = from pd in _dbContext.tblProjectDetails
                              join cl in _dbContext.tblClients
                              on pd.clientID equals cl.clientID
                              where ((pd.dt.Month == mnt) && (pd.dt.Year == yr) && (pd.projectType == ptype))
                              select new
                              {
                                  clientid = cl.clientID,
                                  clientName = cl.clientName,
                                  projectName = pd.projectname,
                                  projectID = pd.projectID,
                                  projectType = pd.projectType,
                                  package = pd.package,
                                  projectLevel = pd.projectLevel,
                                  plotSize = pd.plotSize,
                                  amount = pd.amount,
                                  remark = pd.remark,
                                  status = pd.status

                              };
                    foreach (var item in prj)
                    {
                        if (item.status == "request")
                            varstatus = "Pending";
                        else
                            varstatus = "Confirm";
                        rec.Add(new operation
                        {
                            sno = i,
                            clientID = item.clientid,
                            projectID = item.projectID,
                            clientName = item.clientName,
                            projectName = item.projectName,
                            projectType = item.projectType,
                            package = item.package,
                            projectLevel = item.projectLevel,
                            plotSize = item.plotSize,
                            amount = item.amount,
                            status = varstatus,
                            remark = item.remark

                        });
                        i++;
                    }
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
        public IEnumerable<operation> getProjectDetail(int projectID)
        {
            List<operation> reqlist = new List<operation>();
            try
            {
                reqlist = (from pd in _dbContext.tblProjectDetails
                          join cl in _dbContext.tblClients
                          on pd.clientID equals cl.clientID
                          where (pd.projectID == projectID)
                          select new operation
                          {
                              dt = pd.dt,
                              clientID = cl.clientID,
                              clientName = cl.clientName,
                              emailID = cl.emailID,
                              address = cl.address,
                              city = cl.city + " , " + cl.state,
                              projectID = pd.projectID,
                              projectName = pd.projectname,
                              projectType = pd.projectType,
                              package = pd.package,
                              projectLevel = pd.projectLevel,
                              plotSize = pd.plotSize,
                              amount = pd.amount,
                              remark = pd.remark,
                              finalizeAmount = (long) pd.finalizeAmount,
                              status = pd.status
                          }).ToList();


                 
                //foreach (var item in prj)
                //{
                //    reqlist.Add(new operation
                //    {
                //        dt = item.dt,
                //        clientID = item.clientid,
                //        clientName = item.clientname,
                //        emailID = item.emailID,
                //        designerName = item.address,
                //        rowcolor = item.city,
                //        projectID = item.projectID,
                //        projectName = item.projectname,
                //        projectType = item.projectType,
                //        package = item.package,
                //        projectLevel = item.projectLevel,
                //        plotSize = item.plotSize,
                //        amount = item.amount,
                //        remark = item.remark,
                //        finalizeAmount = (long)item.finalizeAmount,
                //        status = it
                //    });

                //}

            }
            catch (Exception ex) { }
            return reqlist;
        }
         
      
    }
}