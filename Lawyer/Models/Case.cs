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
    
    public partial class Case
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Case()
        {
            this.Client_Case = new HashSet<Client_Case>();
            this.Resumptions = new HashSet<Resumption>();
            this.vetoes = new HashSet<veto>();
        }
    
        public long ID { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public Nullable<long> Number_of_Session { get; set; }
        public string Circle { get; set; }
        public Nullable<long> ID_jadge { get; set; }
        public Nullable<bool> Lock { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client_Case> Client_Case { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resumption> Resumptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<veto> vetoes { get; set; }
    }
}
