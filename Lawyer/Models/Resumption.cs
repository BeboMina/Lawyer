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
    
    public partial class Resumption
    {
        public long ID_Resumption { get; set; }
        public string Tybe { get; set; }
        public string Notes { get; set; }
        public Nullable<long> Number_Of_Session { get; set; }
        public string Circle { get; set; }
        public Nullable<long> ID_Case { get; set; }
        public Nullable<long> ID_Jadge { get; set; }
        public string Resumption_Number { get; set; }
    
        public virtual Case Case { get; set; }
    }
}
