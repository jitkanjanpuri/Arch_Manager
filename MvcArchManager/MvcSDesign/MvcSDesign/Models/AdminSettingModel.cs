using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class AdminSettingModel
    {
        
            [Required(ErrorMessage ="Please enter project ID")]
            public long projectID { get; set; }
         
            [Required (ErrorMessage ="Please enter project ID prefix")]
            public string prefixPrejectID { get;set;}
         
            [Required(ErrorMessage ="Enter quotation ID start")]
            public int quotationID { get;set;}

            [Required(ErrorMessage ="Enter quotation prefix")]
            public string prefixQuotationID { get;set;}

            
    }
}