using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class TaskListModel
    {
        public int taskID { get; set; }
        public int pmID { get; set; }
        public long projectID { get; set; }
        public int designerID { get; set; }
        public string dt { get; set; }
        public string tm { get; set; }
        public string level { get; set; }
        public string techRemark { get; set; }
        public string status { get; set; }
        public string taskDone { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string previousDesigner { get; set; }
        public string projectlocation { get; set; }
        public string prf { get; set; }

    }
}