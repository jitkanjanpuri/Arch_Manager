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
    
    public partial class tblTaskAssign
    {
        public int taskID { get; set; }
        public System.DateTime dt { get; set; }
        public string tm { get; set; }
        public long projectID { get; set; }
        public int designerID { get; set; }
        public string category { get; set; }
        public string subcatorgy { get; set; }
        public string techRemark { get; set; }
        public Nullable<System.DateTime> submitDate { get; set; }
        public string submitTime { get; set; }
        public Nullable<int> submitDesignerID { get; set; }
        public Nullable<int> pmID { get; set; }
    
        public virtual tblProjectDetail tblProjectDetail { get; set; }
    }
}
