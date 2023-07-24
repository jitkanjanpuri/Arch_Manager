using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class operation
    {
        
        public int clientLedgerID { get; set; }
        public int operationID { get; set; }
        public DateTime dt { get; set; }

        public string dtstr { get; set; }
        public int sno { get; set; }
        public int clientID { get; set; }
        public long projectID { get; set; }
        public string clientName { get; set; }
        public string designerName { get; set; }
        public string projectName { get; set; }
        public string projectType { get; set; }

        public string projectCategory { get; set; }

        public string projectLevel { get; set; }
        public string package { get; set; }
        public string plotSize { get; set; }
        public int amount { get; set; }
        public int receivedAmount { get; set; }
        public int paidAmount { get; set; }
        public int balance { get; set; }

        public int? oldBalance { get; set; }

        public int? adjustBalance { get; set; }
        public string status { get; set; }
        public string city { get; set; }

        public string emailID { get; set; }
        public string rowcolor { get; set; }
        public string remark { get; set; }
    }
}