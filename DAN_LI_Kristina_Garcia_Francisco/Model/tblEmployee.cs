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
    
    public partial class tblEmployee
    {
        public int EmployeeID { get; set; }
        public Nullable<int> FloorNumber { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string Responsibility { get; set; }
        public string Salary { get; set; }
        public int UserID { get; set; }
    
        public virtual tblUser tblUser { get; set; }
    }
}
