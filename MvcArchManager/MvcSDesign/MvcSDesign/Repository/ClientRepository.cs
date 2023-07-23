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
  
namespace MvcSDesign.Repository
{
    public class ClientRepository :IClient
    {
        ArchManagerDBEntities _dbContext;

        public ClientRepository(ArchManagerDBEntities dEntity)
        {
            _dbContext = dEntity;
        }

        

        public  void InsertData(client cnt)
        {
            tblClient obj = new tblClient();
            try
            {
                obj.clientName = cnt.clientName;
                obj.orgName = cnt.orgName;
                obj.address = cnt.address;
                obj.city = cnt.city;
                obj.phone = cnt.phone;
                obj.mobile = cnt.mobile;
                obj.emailID = cnt.emailID;
                obj.state = cnt.state;

                _dbContext.tblClients.Add(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) { }
        }
        public int InsertQuotation(quotation qtn)
        {
            int pid = 0;
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
                obj.status = "request";
                obj.remark = qtn.remark;

                _dbContext.tblProjectDetails.Add(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) { }
            return pid;
        }


        //public void SaveStatus(string ch)
        //{
        //    try
        //    {
        //        tblStatus obj = new tblStatus();
        //        obj.status = ch;

        //        _dbContext.tblStatus.Add(obj);
        //        _dbContext.SaveChanges();
        //    }
        //    catch (Exception ex) { }

        //}


        public string InsertInteriorQuotation(operation obj, string empdata, ref int pid , ref DataTable dt)
        {
            //using (var context = new SDesignEntities())
            //{
            //    using (var dbTransaction = context.Database.BeginTransaction())
            //    {
            //        try
            //        {
                        //    tblProjectDetail objPD = new tblProjectDetail();
                        //    tblInteriorProjectDetail objInterior = new tblInteriorProjectDetail();
                        //    pid = getProjectID();
                        //    DataRow dr;
                        //    var serializeData = JsonConvert.DeserializeObject<List<InteriorProject>>(empdata);

                        //    int amount = 0;
                        //    foreach (var data in serializeData)
                        //    {
                        //        amount += data.amount;
                        //    }
                        //    objPD.projectID = pid;
                        //    objPD.clientID = obj.clientID;
                        //    objPD.dt = DateTime.Now;
                        //    objPD.projectname = obj.projectName.Trim();
                        //    objPD.projectType = "Interior";
                        //    objPD.projectLevel = obj.projectLevel.Trim();
                        //    objPD.package = "-";
                        //    objPD.plotSize = obj.plotSize;
                        //    objPD.amount = amount;
                        //    objPD.status = "request";

                        //    context.tblProjectDetail.Add(objPD);
                        //    context.SaveChanges();

                        //    foreach (var data in serializeData)
                        //    {
                        //        objInterior.projectID = pid;
                        //        objInterior.projectDetails = data.projectDetails.Trim();
                        //        objInterior.particulars = data.particular.Trim();
                        //        objInterior.unit = data.unit.Trim();
                        //        objInterior.amount = data.amount;

                        //        context.tblInteriorProjectDetail.Add(objInterior);
                        //        context.SaveChanges();

                        //        amount += data.amount;
                        //        dr = dt.NewRow();
                        //        dr[0] = data.projectDetails;
                        //        dr[1] = data.particular;
                        //        dr[2] = data.unit;
                        //        dr[3] = data.amount;

                        //        dt.Rows.Add(dr);

                        //    }
                        //    dbTransaction.Commit();
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
        public int getProjectID()
        {
            int projectID = 1000;
             
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

        public string ClientNameValidation(string name, string emailID)
        {
            
            try
            {
                var res = _dbContext.tblClients.Where(x => x.clientName == name).FirstOrDefault();
                if(res != null)
                {
                    return "Client name already exist";
                }
                var res1 = _dbContext.tblClients.Where(x => x.emailID == emailID).FirstOrDefault();
                if (res1 != null)
                {
                    return "Client's emailID already exist";
                }
            }
            catch (Exception ex) { return ex.Message; }
            return "";
        }

        public IEnumerable<tblClient> SearchByName(string chkName, string name, string chkcity, string cityname)
        {
            if ((chkName == "true") && (chkcity == "true"))
                return _dbContext.tblClients.Where(x => (x.clientName.Contains(name)) && (x.city.Contains(cityname)));
            else if (chkcity == "true")
                return _dbContext.tblClients.Where(x => (x.city.Contains(cityname)));


            return _dbContext.tblClients.Where(s => s.clientName.Contains(name)).ToList();
        }

        public IEnumerable<tblClient> getByName(string name)
        {
            return _dbContext.tblClients.Where(s => s.clientName.Contains(name)).ToList();
        }
         
        public IEnumerable<tblClient> getAll()
        {
            return _dbContext.tblClients.ToList().OrderBy(x => x.clientName);
        }

        public IEnumerable<client> getClient_PromMail(string name, string city)
        {

            

            List<client> clist = new List<client>();
            try
            {
               
                if ((name != null) && (city != null))
                {
                    //var varlist = (from cl in _dbContext.tblClient
                    //               where ((cl.clientName.Contains(name)) && (cl.city.Contains(city)))
                    //               select new client
                    //               {
                    //                   clientID = cl.clientID,
                    //                   clientName = cl.clientName,
                    //                   city = cl.city,
                    //                   state = cl.state,
                    //                   mobile = cl.mobile,
                    //                   emailID = cl.mobile
                    //               }).ToList();

                    //clist = varlist.ToList();
                }
                else if (name != null)
                {
                    //var varlist = (from cl in _dbContext.tblClient
                    //               where (cl.clientName.Contains(name))  
                    //               select new client
                    //               {
                    //                   clientID = cl.clientID,
                    //                   clientName = cl.clientName,
                    //                   city = cl.city,
                    //                   state = cl.state,
                    //                   mobile = cl.mobile,
                    //                   emailID = cl.mobile
                    //               }).ToList();

                    //clist = varlist.ToList();
                }
                else if (city != null)
                {
                    //var varlist = (from cl in _dbContext.tblClient
                    //               where (cl.city.Contains(city))
                    //               select new client
                    //               {
                    //                   clientID = cl.clientID,
                    //                   clientName = cl.clientName,
                    //                   city = cl.city,
                    //                   state = cl.state,
                    //                   mobile = cl.mobile,
                    //                   emailID = cl.mobile
                    //               }).ToList();

                    //clist = varlist.ToList();
                }

            }
            catch (Exception ex) { }



            return clist;
        }

        
        public client GetClient(int clientID)
        {
           client clist = new client();
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

        public string Update(client cnt)
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


                var prj = from pd in _dbContext.tblProjectDetails
                          join cl in _dbContext.tblClients
                          on pd.clientID equals cl.clientID
                          where (pd.projectID == projectID)
                          select new
                          {
                              dt = pd.dt,
                              clientid = cl.clientID,
                              clientname = cl.clientName,
                              emailID = cl.emailID,
                              address = cl.address,
                              city = cl.city + " , " + cl.state,
                              projectID = pd.projectID,
                              projectname = pd.projectname,
                              projectType = pd.projectType,
                              package = pd.package,
                              projectLevel = pd.projectLevel,
                              plotSize = pd.plotSize,
                              amount = pd.amount,
                              remark = pd.remark

                          };


                //status = for cient email

                //designerName = for cient address

                //rowcolor = for city
                foreach (var item in prj)
                            {
                                reqlist.Add(new operation
                                {
                                    dt = item.dt,
                                    clientID = item.clientid,
                                    clientName = item.clientname,
                                    status = item.emailID,
                                    designerName = item.address,
                                    rowcolor = item.city,
                                    projectID = item.projectID,
                                    projectName = item.projectname,
                                    projectType = item.projectType,
                                    package = item.package,
                                    projectLevel = item.projectLevel,
                                    plotSize = item.plotSize,
                                    amount = item.amount,
                                    remark = item.remark

                                });

                            }

            }
            catch (Exception ex) { }
            return reqlist.ToList();
        }


        //public IEnumerable<tblInteriorProjectDetail> getInteriorProjectDetail(int projectID)
        //{
        //    return _dbContext.tblInteriorProjectDetail.Where(x=>x.projectID == projectID).ToList().OrderBy(x => x.interiorID);
        //}



      
    }
}