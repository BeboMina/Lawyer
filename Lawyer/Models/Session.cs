//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lawyer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Session
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Session()
        {
            this.FilesCases = new HashSet<FilesCas>();
            this.Lawery_Session = new HashSet<Lawery_Session>();
        }
    
        public long ID { get; set; }
        public long IDCase { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<System.DateTime> NextDate { get; set; }
        public string Jadge { get; set; }
        public string Notes { get; set; }
        public string Timer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilesCas> FilesCases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lawery_Session> Lawery_Session { get; set; }
        public virtual Session Sessions1 { get; set; }
        public virtual Session Session1 { get; set; }
    }
}
