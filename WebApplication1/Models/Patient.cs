//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient
    {
        public int PatientId { get; set; }
        public string patientName { get; set; }
        public string patientAddress { get; set; }
        public Nullable<int> patientMobile { get; set; }
        public string condition { get; set; }
        public Nullable<System.DateTime> patientDateAdmitted { get; set; }
        public Nullable<int> doctor { get; set; }
    
        public virtual Doctor Doctor1 { get; set; }
    }
}