using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class Subcategory
    {
        public int subcategoryID { get; set; }
        public int categoryID { get; set; }

        [Required(ErrorMessage = "Please enter Subcategory name")]

        [Display(Name = "Subcategory name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Enter Subcategory name between 1 to 200 character")]
        public string subcategoryName { get; set; }
        public bool status { get; set; }
    }
}