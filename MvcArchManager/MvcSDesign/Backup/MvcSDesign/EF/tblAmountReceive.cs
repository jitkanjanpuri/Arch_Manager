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
    
    public partial class tblAmountReceive
    {
        public int amountReceiveID { get; set; }
        public int dt { get; set; }
        public int clientID { get; set; }
        public int amount { get; set; }
        public string remark { get; set; }
    
        public virtual tblClient tblClient { get; set; }
    }
}
