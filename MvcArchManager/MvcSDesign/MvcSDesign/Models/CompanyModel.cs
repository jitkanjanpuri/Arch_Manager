using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class CompanyModel
    {

        public int companyID { get; set; }

        [Required(ErrorMessage = "Enter Company Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Enter organization Name")]
        public string orgName { get; set; }

         
        public string gstNo { get; set; }

        [Required(ErrorMessage = "Enter address")]
        public string address { get; set; }


        public string address2 { get; set; }
        [Required(ErrorMessage = "Enter City")]
        public string city { get; set; }

        [Required(ErrorMessage = "Enter State")]
        public string state { get; set; }

        [Required(ErrorMessage = "Enter Pincode")]
        [MaxLength(6)]
        [MinLength(6)]
        public string pincode { get; set; }
        
        [Required(ErrorMessage = "Enter Mobile")]
        [MaxLength(10)]
        [MinLength(10)]
        public string mobile { get; set; }

       
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string emailID { get; set; }
        public string phone { get; set; }

        bool active { get; set; }
    }
}