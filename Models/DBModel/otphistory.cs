//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TCNX.Models.DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class otphistory
    {
        [Key]
        public int id { get; set; }
        public string mid { get; set; }
        public string otp { get; set; }
        public int status { get; set; }
        public int retrycnt { get; set; }
        public System.DateTime edate { get; set; }
    }
}