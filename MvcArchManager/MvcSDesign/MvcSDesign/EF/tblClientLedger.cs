//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcSDesign.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblClientLedger
    {
        public int clientLadgerID { get; set; }
        public System.DateTime dt { get; set; }
        public int clientID { get; set; }
        public int projectID { get; set; }
        public int prjAmount { get; set; }
        public int receivedAmount { get; set; }
        public int balance { get; set; }
        public string remark { get; set; }
        public Nullable<int> oldBalance { get; set; }
        public Nullable<int> adjustBalance { get; set; }
    
        public virtual tblClient tblClient { get; set; }
    }
}
