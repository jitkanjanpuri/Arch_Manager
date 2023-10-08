using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class CategoryModel
    {
        public int categoryID { get; set; }

        [Required(ErrorMessage = "Please enter category name")]

        [Display(Name = "Category name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Enter category name between 1 to 200 character")]
        public string categoryName { get; set; }
        public bool status { get; set; }
    }
}