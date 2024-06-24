using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class QuotationItemModel
    {
        public int itemID { get; set; }
        public string projectDetails { get;set;}
        public string services { get;set;}
        public string hsn { get;set;}
        public int qty { get;set;}
        public string unit { get;set;}
        public int rate { get;set;}
        public int amount { get;set;}

 
    }
}