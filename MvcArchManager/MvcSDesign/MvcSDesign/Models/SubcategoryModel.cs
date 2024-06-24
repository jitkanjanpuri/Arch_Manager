using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSDesign.Models
{
    public class SubcategoryModel 
    {
        public int subcategoryID { get; set; }
        public int categoryID { get; set; }

        public string categoryName { get; set; }

        [Required(ErrorMessage = "Please enter Subcategory name")]

        [Display(Name = "Subcategory name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Enter Subcategory name between 1 to 200 character")]
        public string subcategoryName { get; set; }

        public IEnumerable<SelectListItem> categoryList { get; set; }
    
        public bool status { get; set; }
    }
}