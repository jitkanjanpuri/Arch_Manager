using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class SiteVisitModel : StaffModel
    {
        public int projectID { get; set; }
        public string remark { get; set; }


    }
}