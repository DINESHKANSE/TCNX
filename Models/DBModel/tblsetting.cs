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

    public  class tblsetting
    {
        [Key]
        public int id { get; set; }
        public int status { get; set; }
        public int login { get; set; }
        public int linkstatus { get; set; }
        public string commonpass { get; set; }
        public string adminpass { get; set; }
        public string smskey { get; set; }
        public string senderid { get; set; }
        public string SMSURL { get; set; }
        public string BTC_ADD { get; set; }
        public string ETH_ADD { get; set; }
        public string TRON_ADD { get; set; }
        public string BTT_ADD { get; set; }
        public string BNB_ADD { get; set; }
        public Nullable<decimal> MF_PRICE { get; set; }
        public string USDT_ADD { get; set; }
    }
}