using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


 

namespace MvcSDesign.Models
{
    public class clientModel
    {
        public int sno { get; set; }
        public int clientID { get; set; }

        [Required(ErrorMessage = "Please enter client name")]

        [Display(Name = "Client name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Enter client name between 1 to 200 character")]

        [Remote("CheckClientExists", "Client", ErrorMessage = "Client name already exists!")]
        public string clientName { get; set; }
        public string orgName { get; set; }
        public string address { get; set; }

        [Required(ErrorMessage = "Please enter city name")]
        public string city { get; set; }
        public string state { get; set; }

        [Required(ErrorMessage = "Please enter mobile no")]
        public string mobile { get; set; }
        public string phone { get; set; }

        [Required(ErrorMessage = "Please enter EMail ID")]
        [Display(Name = "Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email ID")]
        [EmailAddress]
        [Remote("ClientEmailValidation","Client", ErrorMessage="Client Email-ID already exists!")]
        public string emailID { get; set; }
 
        public IEnumerable<SelectListItem> statelist { get; set; }
         
    }
}