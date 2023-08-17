using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSDesign.Models
{
    public class QuotationModel
    {

        public int sno { get; set; }
        public int clientID { get; set; }

        [Required(ErrorMessage = "Please enter client name")]

        [Display(Name = "Client name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Enter client name between 1 to 200 character")]

       
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
        public string emailID { get; set; }

        
    }
}