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

    public  class single_leg_income
    {
        [Key]
        public int id { get; set; }
        public int stage { get; set; }
        public decimal Team { get; set; }
        public decimal Teamshow { get; set; }
        public string mid { get; set; }
        public int sid { get; set; }
        public int direct { get; set; }
        public decimal single_leg_income1 { get; set; }
        public decimal sponcer_income { get; set; }
        public int status { get; set; }
        public Nullable<System.DateTime> edate { get; set; }
        public decimal dailyincome { get; set; }
        public decimal days { get; set; }
        public decimal reentry { get; set; }
        public decimal nonworking { get; set; }
    }
}
