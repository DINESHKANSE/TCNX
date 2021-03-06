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

    public  class nowpayment_history
    {
        [Key]   
        public int pay_id { get; set; }
        public string payment_id { get; set; }
        public string invoice_id { get; set; }
        public string purchase_id { get; set; }
        public string payment_status { get; set; }
        public string pay_address { get; set; }
        public Nullable<decimal> price_amount { get; set; }
        public string price_currency { get; set; }
        public Nullable<decimal> pay_amount { get; set; }
        public Nullable<decimal> actually_paid { get; set; }
        public string pay_currency { get; set; }
        public Nullable<decimal> outcome_amount { get; set; }
        public string outcome_currency { get; set; }
        public string order_id { get; set; }
        public string order_description { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public string username { get; set; }
    }
}
