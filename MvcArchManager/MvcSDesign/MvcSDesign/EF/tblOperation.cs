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
    
    public partial class tblOperation
    {
        public int operationID { get; set; }
        public System.DateTime dt { get; set; }
        public int staffID { get; set; }
        public Nullable<int> designerAmount { get; set; }
        public string status { get; set; }
        public string remark { get; set; }
        public string payStatus { get; set; }
        public string projectCategory { get; set; }
        public long projectID { get; set; }
    
        public virtual tblProjectDetail tblProjectDetail { get; set; }
        public virtual tblStaff tblStaff { get; set; }
    }
}
