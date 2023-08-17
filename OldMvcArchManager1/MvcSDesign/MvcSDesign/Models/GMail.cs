using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcSDesign.Models
{
    public class GMail
    {

        public int id { get; set; }
        public string gmailID { get; set; }
        [Required (ErrorMessage ="Please enter password")]
        public string pwd { get; set; }
    }
}