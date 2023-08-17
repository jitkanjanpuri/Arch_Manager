using MvcSDesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MvcSDesign.Repository
{
    interface IUser
    {

        //string userVarifiction(string username, string pwd, ref int regID, ref string name);
        List<TaskListModel> GetTaskAssign(int regID);
        string ChangeCredential(int staffID, string pwd);

        string UploadDesignerTask(int pmID, int taskID, List<HttpPostedFileBase> fileToUpload );
        int fillGraphMonthlyPerformance(int regID);
        List<operation> getWeeklyPerformance(int regID);




    }
}

