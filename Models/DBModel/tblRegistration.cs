namespace TCNX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class tblregistration
    {
        [Key]
        public int regid { get; set; }
        public string mid { get; set; }
        public string pid { get; set; }
        public string sid { get; set; }
        public string mname { get; set; }
        public string mobile { get; set; }
        public bool mobileverify { get; set; }
        public string email { get; set; }
        public bool emailverify { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string pincode { get; set; }
        public string pancard { get; set; }
        public string bankname { get; set; }
        public string account { get; set; }
        public string bankhdname { get; set; }
        public string atmcardno { get; set; }
        public string ifsccode { get; set; }
        public string passwordHash { get; set; }
        public string transactionPass { get; set; }
        public int packageid { get; set; }
        public Nullable<decimal> packageamt { get; set; }
        public System.DateTime edate { get; set; }
        public System.DateTime actdate { get; set; }
        public bool activate { get; set; }
        public bool active_status { get; set; }
        public bool blocked { get; set; }
        public bool locked { get; set; }
        public bool regpermission { get; set; }
        public bool admin { get; set; }
        public Nullable<int> partner { get; set; }
        public string position { get; set; }
        public Nullable<int> planType { get; set; }
        public string nominee { get; set; }
        public string relation { get; set; }
        public string sname { get; set; }
        public string smobile { get; set; }
        public int count { get; set; }
        public int leg { get; set; }
        public Nullable<decimal> mfqty { get; set; }
        public int boosterincome { get; set; }
        public string btc_add { get; set; }
        public string btc_value { get; set; }
        public string eth_add { get; set; }
        public string tron_add { get; set; }
        public string usdt_add { get; set; }
        public string crypto_add { get; set; }
        public string googlepay { get; set; }
        public string phonepe { get; set; }
        public string paytm { get; set; }
        public string token { get; set; }
        public string mkey { get; set; }
        public bool active2fa { get; set; }
        public string active2fa_code { get; set; }
        public string vcardno { get; set; }
        public string ipaddress { get; set; }
        public string devicename { get; set; }
        public string location { get; set; }
        public Nullable<decimal> miningamt { get; set; }
        public Nullable<decimal> mfqty1 { get; set; }
        public Nullable<decimal> mfqty2 { get; set; }
    }
}
