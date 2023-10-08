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
    
    public partial class tblProjectDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblProjectDetail()
        {
            this.tblOperations = new HashSet<tblOperation>();
            this.tblPRFs = new HashSet<tblPRF>();
            this.tblProjectManagements = new HashSet<tblProjectManagement>();
            this.tblProjectSiteVisits = new HashSet<tblProjectSiteVisit>();
            this.tblProjectUploadFiles = new HashSet<tblProjectUploadFile>();
            this.tblTaskAssigns = new HashSet<tblTaskAssign>();
            this.tblAmountReceives = new HashSet<tblAmountReceive>();
            this.tblProjectDetailItems = new HashSet<tblProjectDetailItem>();
        }
    
        public long projectID { get; set; }
        public System.DateTime dt { get; set; }
        public int clientID { get; set; }
        public string projectType { get; set; }
        public string projectLevel { get; set; }
        public string package { get; set; }
        public string plotSize { get; set; }
        public int amount { get; set; }
        public string status { get; set; }
        public string remark { get; set; }
        public string projectname { get; set; }
        public string projectlocation { get; set; }
        public Nullable<long> finalizeAmount { get; set; }
        public string service { get; set; }
    
        public virtual tblClient tblClient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOperation> tblOperations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPRF> tblPRFs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProjectManagement> tblProjectManagements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProjectSiteVisit> tblProjectSiteVisits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProjectUploadFile> tblProjectUploadFiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTaskAssign> tblTaskAssigns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAmountReceive> tblAmountReceives { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProjectDetailItem> tblProjectDetailItems { get; set; }
    }
}
