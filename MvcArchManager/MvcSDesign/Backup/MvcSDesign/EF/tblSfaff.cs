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
    
    public partial class tblSfaff
    {
        public tblSfaff()
        {
            this.tblOperation = new HashSet<tblOperation>();
            this.tblStaffPaid = new HashSet<tblStaffPaid>();
        }
    
        public int staffID { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string dist { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string emailID { get; set; }
    
        public virtual ICollection<tblOperation> tblOperation { get; set; }
        public virtual ICollection<tblStaffPaid> tblStaffPaid { get; set; }
    }
}
