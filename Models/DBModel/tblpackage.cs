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

    public  class tblpackage
    {
        [Key]
        public int id { get; set; }
        public int packageid { get; set; }
        public decimal packageamt { get; set; }
        public string packagetext { get; set; }
        public bool status { get; set; }
    }
}
