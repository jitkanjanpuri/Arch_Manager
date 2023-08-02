using MvcSDesign.EF;
using MvcSDesign.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace MvcSDesign.Repository
{
    public class UserRepository : IUser
    {
        
        ArchManagerDBEntities _dbContext;
        public UserRepository(ArchManagerDBEntities db )
        {
            _dbContext = db;
            
        }

        //public string userVarifiction(string username, string pwd, ref int regID, ref string name, ref string usertype, ref string profileImage, ref string designation, ref string description)
        //{

        //    regID = 0;
        //    name = "";
        //    usertype = "";
        //    designation = "";
        //    try
        //    {
        //        var res = _dbContext.tblRegistration.Where(x => ((x.username.ToLower().Trim() == username.ToLower().Trim()) && (x.pwd.ToLower().Trim() == pwd.ToLower().Trim()))).FirstOrDefault();
        //        if (res == null)
        //        {
        //            return "Invalid user name or password";
        //        }
        //        else if (res.active == false)
        //        {
        //            return "User deactived";
        //        }
        //        else if ((res.designerType != "Exterior") || (res.designerDesignation != "3D Designer"))
        //        {
        //            return "Invalid login for exterior or their designation";
        //        }

        //        usertype = res.designerDesignation;
        //        regID = res.registrationID;
        //        name = res.name;
        //        profileImage = res.profileImage == null ? "dummyuser.png" : res.profileImage;
        //        designation = res.designerDesignation == null ? "-" : res.designerDesignation;
        //        description = res.description == null ? "-" : res.description;



        //    }
        //    catch (Exception ex) { return ex.Message; }
        //    return "";
        //}


        public string ChangeCredential(int staffID, string pwd)
        {
            var res = _dbContext.tblStaffs.Where(x => x.staffID == staffID).FirstOrDefault();
            if(res !=null)
            {
                res.password = pwd;
                _dbContext.SaveChanges();
            }

            return "Record successfully updated";
        }


        //public void TaskAssignStatusChange(int taskID)
        //{
        //    var res = _dbDlabContext.tblDesignerTaskAssigns.Where(x => (x.taskID == taskID)).FirstOrDefault();
        //    if (res != null)
        //    {
        //        res.status = "WRKG";
        //        _dbDlabContext.SaveChanges();
        //    }
        //}


        public List<TaskListModel> GetTaskAssign(int regID)
        {
            List<TaskListModel> lst = new List<TaskListModel>();
            try
            {
                string projectlocation = "", prfFlag = "N";
                var res = (from tl in _dbContext.tblTaskAssigns
                           where (tl.designerID == regID)
                           select new TaskListModel
                           {
                               pmID= tl.pmID,
                               taskID = tl.taskID,
                               dt = tl.dt.Day.ToString() + "-" + tl.dt.Month.ToString() + "-" + tl.dt.Year.ToString(),
                               tm = tl.tm,
                               projectID = tl.projectID,
                               designerID = tl.designerID,
                               category = tl.category,
                               subcategory = tl.subcatorgy,
                               techRemark = tl.techRemark,
                           }).ToList();

                foreach (var item in res)
                {

                    // if (previousDesigner == "") previousDesigner = "New";
                    var res1 = _dbContext.tblPRFs.Where(x => x.projectID == item.projectID).FirstOrDefault();

                    prfFlag = "N";
                    if(res1 != null) prfFlag = "Y";
                    
                    lst.Add(
                          new TaskListModel
                          {
                              taskID = item.taskID,
                              projectID = item.projectID,
                              pmID = item.pmID,
                              dt = item.dt,
                              tm = item.tm,
                              status = item.status,
                              projectlocation = projectlocation,
                              subcategory = item.subcategory,
                              category =item.category,
                              prf = prfFlag,
                              techRemark= item.techRemark
                          });
                }
            }
            catch (Exception ex) { }
            return lst;
        }
         
        public string UploadDesignerTask(int pmID, int taskID, List<HttpPostedFileBase> fileToUpload)
        {
            try
            {
                string location, ch, ch1, uploadfile = "", tm = "", category = "", subcatgory = "", temp="";
                DateTime dt = DateTime.Today.Date;
                long pid = 0;
                int clientID = 0, i=1;
                 
                tm = DateTime.Now.Hour + ":" + DateTime.Now.Minute;

                 
                var res = (from pd in _dbContext.tblProjectDetails
                           join pm in _dbContext.tblProjectManagements on pd.projectID equals pm.projectID
                           where (pm.pmID == pmID)
                           select new
                           {
                               clientID = pd.clientID,
                               projectID = pm.projectID,
                               category = pm.category,
                               subcategory = pm.subcategory
                           }).ToList();
                
                foreach (var item in res)
                {
                    pid = item.projectID;
                    category = item.category;
                    subcatgory = item.subcategory;
                    clientID = item.clientID;
                }
                location = "~/ProjectLocation/client_" + clientID.ToString() + "/proj_" + pid.ToString() + "/" + category + "/" + subcatgory+"/I";
                ch = HostingEnvironment.MapPath(location);

                 
                if (Directory.Exists(ch))
                {
                    Directory.Delete(ch, true);
                }
                Directory.CreateDirectory(ch);

                foreach (HttpPostedFileBase item in fileToUpload)
                {
                    temp = pid +  "_tm"+i.ToString() + Path.GetExtension(item.FileName);
                    ch1 = ch +  "/" + temp;
                    if (File.Exists(ch1))
                    {
                         i = 2;
                        bool flag = true;
                        while (flag)
                        {
                            temp = pid + "_tm" + i.ToString() + Path.GetExtension(item.FileName);
                            ch1 = ch + "/" + temp;
                            if (!File.Exists(ch1))
                            {
                                flag = false;
                            }
                            i++;
                        }
                    }
                    if (uploadfile.Length == 0)
                        uploadfile = temp;
                    else
                        uploadfile += "," + temp;

                    item.SaveAs(ch1);

                }
                
                var res1 = _dbContext.tblTaskAssigns.Where(x => x.taskID == taskID).FirstOrDefault();
                if(res1 !=null)
                {
                    res1.designerID = 0;
                    res1.submitTime = tm ;
                    res1.submitDate = dt;
                }

                var res2 = _dbContext.tblProjectManagements.Where(x => x.pmID == pmID).FirstOrDefault();
                if (res2 != null)
                {
                    res2.projectstatus = "Submit";
                    res2.User_UploadedFileName = uploadfile;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex) { return ex.Message; }
            return "Y";
        }


        public int fillGraphMonthlyPerformance(int regID)
        {
            int qty = 0;
            try
            {
                //if (conDlab.State == ConnectionState.Closed) conDlab.Open();
                //System.DateTime dt2 = System.DateTime.Today;
                //System.DateTime dt1 = dt2.AddDays(-30);// 30 days 

                //dt2 = DateTime.Now;
                //str = " Select distinct count(projectID) from tblProjectUploadFiles " +
                //      " Where desinername like '%" + name.Trim() + "%' AND " +
                //      " ((dt >'" + dt1 + "') AND (dt <='" + dt2 + "' ))";
                //try
                //{
                //    cmd = new SqlCommand(str, conDlab);
                //    qty = int.Parse(cmd.ExecuteScalar().ToString());
                //}
                //catch (System.Exception ex) { }
            }
            catch (System.Exception ex) { }
            return qty;
        }



    }
}