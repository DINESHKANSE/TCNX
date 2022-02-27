using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCNX.Models
{
    public class NPDeposit
    {
        public decimal price_amount { get; set; } = 0.01M;
        public string price_currency { get; set; }
        public string order_id { get; set; }
        public string order_description { get; set; }
        public string ipn_callback_url { get; set; }
        public string success_url { get; set; }
        public string cancel_url { get; set; }
      
      ////"price_amount": 1,
      ////"price_currency": "usd",
      ////"order_id": "RGDBP-21314",
      ////"order_description": "Apple Macbook Pro 2019 x 1",
      ////"ipn_callback_url": "https://nowpayments.io",
      ////"success_url": "https://nowpayments.io",
      ////"cancel_url": "https://nowpayments.io"

    }

    public class NPDepositResponse
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string order_description { get; set; }
        public decimal price_amount { get; set; } = 0.01M;
        public string price_currency { get; set; }
        public string pay_currency { get; set; }
        public string ipn_callback_url { get; set; }
        public string success_url { get; set; }
        public string cancel_url { get; set; }

        public string invoice_url { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }


    }
}