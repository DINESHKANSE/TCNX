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

    public  class tblcoinlist
    {
        [Key]
        public int id { get; set; }
        public string coinsymbol { get; set; }
        public string description { get; set; }
        public int priority { get; set; }
        public int cointype { get; set; }
        public int category { get; set; }
        public bool status { get; set; }
    }
}
