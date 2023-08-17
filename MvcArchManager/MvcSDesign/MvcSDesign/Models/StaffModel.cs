using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
 
  
namespace MvcSDesign.Models
{
    public class StaffModel
    {

        public int sno { get; set; }
        public int staffID { get; set; }

        [Display(Name = "Designer name")]
        [Required(ErrorMessage = "Please enter Designer name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Enter Designer name between 1 to 200 character")]
        [Remote("CheckDesignertNameExists", "Admin", ErrorMessage = "Staff name already exists!")]
        public string name { get; set; }
             

        public string designation { get; set; }
        public string address { get; set; }
        public string city { get; set; }

        [Required(ErrorMessage = "Please enter client name")]
        [Display(Name = "Enter moble  ")]
        public string mobile { get; set; }
        public string phone { get; set; }

        [Required(ErrorMessage = "Please enter EMail ID")]

        [Remote("CheckDesignerEmailID", "Admin", ErrorMessage = "Email ID already exists!")]
        public string emailID { get; set; }

        [Required(ErrorMessage = "Please enter user name")]
        [StringLength(52, MinimumLength = 1, ErrorMessage = "Enter user name between 1 to 50 character")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(52, MinimumLength = 1, ErrorMessage = "Enter password between 1 to 10 character")]
        public string password { get; set; }

        public string rolltype { get; set; }
        public bool active { get; set; }
        public int prjAmount { get; set; }
        public int paidAmount { get; set; }
        public int balance { get; set; }
        public IEnumerable<SelectListItem> designerList { get; set; }

        public IEnumerable<SelectListItem> rollList { get; set; }
    }
}