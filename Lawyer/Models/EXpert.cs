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
    
    public partial class EXpert
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> Date1 { get; set; }
        public Nullable<System.DateTime> Date2 { get; set; }
        public Nullable<System.DateTime> Date3 { get; set; }
        public Nullable<long> Case_ID { get; set; }
    
        public virtual Case Case { get; set; }
    }
}