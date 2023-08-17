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
        public DateTime dt { get; set; }


        [Display(Name = "Client name")]

        public string clientID { get; set; }
        public string clientname { get; set; }
        public string orgname { get; set; }
        public string address { get; set; }

        public string city { get; set; }
        public string state { get; set; }
        public string phone { get; set; }
        public string emailID { get; set; }

        public string projectname { get; set; }

        [Display(Name = "Project Type")]
        public string projectType { get; set; }

        [Display(Name = "Remark")]
        public string remark { get; set; }
        public string projectLevel { get; set; }

        public string package { get; set; }
        
        public string plotSize { get; set; }
        public int amount { get; set; }
        public string status { get; set; }

        public int sno { get; set; }

        public long finalizeAmount { get; set; }


        public int targetAmount { get; set; }
        public int receiveAmount { get; set; }
        
        public int depositAmount { get; set; }
        public string dtstr { get; set; }


    }
}