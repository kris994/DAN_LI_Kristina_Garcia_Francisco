//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAN_LI_Kristina_Garcia_Francisco.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSickLeave
    {
        public int SickLeaveID { get; set; }
        public System.DateTime SickLeaveDate { get; set; }
        public string Reason { get; set; }
        public string CompanyName { get; set; }
        public bool EmergencyCase { get; set; }
        public int UserID { get; set; }
    
        public virtual tblUser tblUser { get; set; }
    }
}
