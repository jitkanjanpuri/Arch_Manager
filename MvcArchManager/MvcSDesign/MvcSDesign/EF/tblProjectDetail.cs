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
            this.tblAmountReceives = new HashSet<tblAmountReceive>();
            this.tblProjectSiteVisits = new HashSet<tblProjectSiteVisit>();
            this.tblPRFs = new HashSet<tblPRF>();
            this.tblProjectManagements = new HashSet<tblProjectManagement>();
            this.tblTaskAssigns = new HashSet<tblTaskAssign>();
            this.tblProjectUploadFiles = new HashSet<tblProjectUploadFile>();
        }
    
        public long projectID { get; set; }
        public System.DateTime dt { get; set; }
        public string remark { get; set; }
        public string projectlocation { get; set; }
        public Nullable<long> finalizeAmount { get; set; }
        public int quotationID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAmountReceive> tblAmountReceives { get; set; }
        public virtual tblQuotation tblQuotation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProjectSiteVisit> tblProjectSiteVisits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPRF> tblPRFs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProjectManagement> tblProjectManagements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTaskAssign> tblTaskAssigns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProjectUploadFile> tblProjectUploadFiles { get; set; }
    }
}
