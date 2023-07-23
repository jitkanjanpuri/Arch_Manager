using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcSDesign.Models
{
    public class registration
    {
        public int registrationID { get; set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy})")]
        DateTime dt { get; set; }
         
        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "Enter name")]
        public string name { get; set; }

        public string companyname { get; set; }

        public string profession { get; set; }


        [Required(ErrorMessage = "Please enter email ID")]
        [Display(Name = "Enter email ID")]
        public string emailID { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength (maximumLength:10,ErrorMessage ="Minimum 4 character required passward",  MinimumLength =4)]
        [Display(Name = "Enter password")]
        public string pwd { get; set; }

        [Required(ErrorMessage = "Please enter mobile no")]
        [StringLength(maximumLength: 10, ErrorMessage = "10 digit mobile no", MinimumLength = 10)]
        [Display(Name = "Enter mobile no*")]
        public string mobileno { get; set; }

        public IEnumerable<SelectListItem> professionlist { get; set; }

         
    }
}