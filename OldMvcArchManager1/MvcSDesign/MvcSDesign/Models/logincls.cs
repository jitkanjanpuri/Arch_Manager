using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
 
using System.ComponentModel.DataAnnotations;

namespace MvcSDesign.Models
{
    public class logincls
    {
        [Required(ErrorMessage = "Please enter login")]
        [Display(Name = "Enter user name")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [Display(Name = "Enter password")]
        [DataType(DataType.Password)]
        public string pwd { get; set; }
    }
}