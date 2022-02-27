using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TCNX.Models
{
    public class UserLoginModels
    {
        public string Spon_Title { get; set; }
        public string Spon_Code { get; set; }
        public string Plac_Title { get; set; }
        public string Plac_Code { get; set; }
        public string Placement { get; set; }
        public string Reg_Date { get; set; }
        public string Reg_Time { get; set; }
        public int    Memb_Code { get; set; }
        public string Memb_Title { get; set; }
        public string MembName_F { get; set; }
        public string MembName_M { get; set; }
        public string MembName_L { get; set; }
        public string Memb_Name { get; set; }
        public string Gender { get; set; }
        public string M_Status { get; set; }
        public string Birth_Date { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }
        public string Nomi_Title { get; set; }
        public string NomiName_F { get; set; }
        public string NomiName_M { get; set; }
        public string NomiName_L { get; set; }
        public string R_Nominee { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }

        public string country { get; set; }

        //[Required(ErrorMessage = "Please enter Password")]
        public string C_code { get; set; }
        public string M_COUNTRY { get; set; }
        public string Pin_Code { get; set; }
        public string Country_Code { get; set; }
        public string Mobile_No { get; set; }
        public string Phone_No { get; set; }
        public string EMail { get; set; }
        public string Pan_Form { get; set; }
        public string PanNo_FormNo { get; set; }
        public string Bank_Name { get; set; }
        public string B_City { get; set; }
        public string B_Branch { get; set; }
        public string Account_No { get; set; }
        public string MICR_Code { get; set; }
        public string Reg_Amt { get; set; }
        public string Payment_by { get; set; }
        public string DD_No { get; set; }
        public string DD_Date { get; set; }
        public string DD_BankName { get; set; }
        public string RV_Code { get; set; }
        public string POLC_CLR { get; set; }
        public string POLC_NO { get; set; }
        public string REMARK { get; set; }
        public string polc_date { get; set; }
        public string Boc_Code { get; set; }
        public string Boc_Date { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string password { get; set; }

        public string PIN_ID { get; set; }
        public string SERIALNO { get; set; }
        public string REG_BYID { get; set; }
        public string BANKAC { get; set; }
        public string BANKNAME { get; set; }
        public string BANKBRANCH { get; set; }
        public string prod_cost { get; set; }
        public string authrised { get; set; }
        public string auth_remark { get; set; }
        public string MPOSITION { get; set; }
        public string COUNT_POSITTION { get; set; }
        public string FLAG { get; set; }
        public string club { get; set; }
        public string card_print { get; set; }
        public string freeID { get; set; }
        public string prod_code { get; set; }
        public string TEMPF { get; set; }
        public string LPOINT { get; set; }
        public string RPOINT { get; set; }
        public string smsqty { get; set; }
        public string tds { get; set; }
        public string process { get; set; }
        public string jcode { get; set; }
        public string clientip { get; set; }
        public string capping_amt { get; set; }
        public string security_ans { get; set; }
        public string security_qu { get; set; }
        public string master_pin { get; set; }
        public string partner { get; set; }
        public string partner_income { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Username is not valid")]
        [Required(ErrorMessage = "Please enter email")]
        public string username { get; set; }

        public string inv_amt { get; set; }
        public string real_inv { get; set; }
        public string security_code { get; set; }
        public string client_ip { get; set; }
        public string account_name { get; set; }
        public string cardno { get; set; }
        public string lu_user { get; set; }
        public string lu_ac { get; set; }
        public string wopt { get; set; }
        public string last_log_in { get; set; }
        public string w_rule { get; set; }
        public string sp_fb_id { get; set; }
        public string sp_sk_id { get; set; }
        public string spon_dedu { get; set; }

        [Required(ErrorMessage = "Please enter captcha")]
        public string CaptchaCode { get; set; }

        [Required(ErrorMessage = "Please Select Country")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Select State")]
        public string Name { get; set; }
    }
}