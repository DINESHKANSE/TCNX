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

    public  class nowpayment_invoice_history
    {
        [Key]
        public int inv_id { get; set; }
        public string id { get; set; }
        public string order_id { get; set; }
        public string order_description { get; set; }
        public Nullable<decimal> price_amount { get; set; }
        public string price_currency { get; set; }
        public string ipn_callback_url { get; set; }
        public string invoice_url { get; set; }
        public string success_url { get; set; }
        public string cancel_url { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public string invoice_status { get; set; }
        public string username { get; set; }
    }
}