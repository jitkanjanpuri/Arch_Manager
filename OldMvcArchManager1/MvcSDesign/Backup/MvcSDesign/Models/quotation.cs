using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcSDesign.Models
{
    public class quotation
    {
        [Display(Name = "Project ID")]
        public int projectID { get; set; }

        [Display(Name = "Client name")]
        public string clientname { get; set; }
        public string orgname { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string phone { get; set; }
        public string emailID { get; set; }

        [Display(Name = "Project Type")]
        public string projectType { get; set; }

        [Display(Name = "Remark")]
        public string remark { get; set; }
        public string rowcolor { get; set; }

        public string package { get; set; }

        public string modelstatus { get; set; }

        
    }
}