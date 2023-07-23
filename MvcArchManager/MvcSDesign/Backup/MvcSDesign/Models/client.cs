using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


 

namespace MvcSDesign.Models
{
    public class client
    {
        public int clientID { get; set; }

        [Required(ErrorMessage = "Please enter client name")]
        [Display(Name = "Client name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Enter client name between 1 to 200 character")]
        public string clientname { get; set; }

        [Required(ErrorMessage = "Please enter Organization name")]
        [Display(Name = "Organization name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Enter organization name between 1 to 200 character")]
        public string organizationName { get; set; }

     
      
 

        [Required(ErrorMessage = "Please enter address")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Please enter city  ")]
        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }

      
        public string mobile { get; set; }

 
        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Please enter EMail ID")]
        [Display(Name = "Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email ID")]
        [EmailAddress]
        public string emailID { get; set; }


        public IEnumerable<SelectListItem> statelist { get; set; }
        public IEnumerable<SelectListItem> workgrouplist { get; set; }
    }
}