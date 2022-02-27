using System;
using System.Data;


namespace TCNX.commonFunction
{

    public class UserInfo
    {
        SqlDblayer dal = new SqlDblayer();
        string Message = string.Empty;

        //FLAG,  Spon_Title,  Plac_Title,  spon_code,  plac_code,  placement, reg_date,  Reg_time
        //   ,  Memb_code,  membname_f,  memb_name,  m_status,  m_country,  email,  reg_amt,  rv_code,
        //    mpwd,  prod_cost,  authrised,  mposition,  tempf,  lpoint,  rpoint,  clientip,  username,  last_log_in

        public string UserID = "";
        public string FLAG = "";
        public string Spon_Title = "";
        public string Plac_Title = "";
        public string spon_code = "";
        public string plac_code = "";
        public string placement = "";
        public string reg_date = "";
        public string Reg_time = "";
        public string Memb_code = "";
        public string membname_f = "";
        public string memb_name = "";
        public string m_status = "";
        public string m_country = "";
        public string email = "";

        public string package_amt { get; private set; }

        public string reg_amt = "";
        public string transactionPassword = "";
        public string mpwd = "";
        public string passwordHash = "";
        public string prod_cost = "";
        public string authrised = "";
        public string mposition = "";
        public string tempf = "";
        public string lpoint = "";
        public string rpoint = "";
        public string clientip = "";
        public string username = "";
        public string last_log_in = "";
        public string ActiveStatus = "";
        private bool _isAuthenticated = false;
        public DataTable _userPages;
        private bool IsActive = false;
        public string IsActive2FA = "N";
        public string BANKAC = "";
        public string BTC_ADD = "";
        public string ETH_ADD = "";
        public string TRX_ADD = "";
        public string mobile = "";
        public string MAC_ADD = "";
        public string MASTER_PIN = "";
        public string REF_ID = "";
        public string ACCOUNT_NAME = "";
        public string BANKNAME = "";
        public string BANKBRANCH = "";
        public string IFSC = "";
        public string vcardno = "";
        public string PANNO_FORMNO = "";
        public string ACTIVE2FA = "";



        public DateTime Get_date()
        {
            dal.ClearParameters();
            var sysdate = dal.ExecuteScaler("select CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') AS DATETIME)", ref Message);
            return Convert.ToDateTime(sysdate);
        }
        public bool isAuthenticate
        {
            get { return _isAuthenticated; }

        }

        public UserInfo(string username)
        {
            if (IsSuperUser(username))
            {
                SetSuperValues(username);
            }
            else
            {

                dal.ClearParameters();
                dal.AddParameter("@MODE", "GetUserByID", "IN");
                dal.AddParameter("@username", username, "IN");


                DataTable dt = dal.GetTable("SP_REGISTER_LOGIN", ref Message);
                if (dt != null && dt.Rows.Count > 0)
                    SetValues(dt.Rows[0]);
            }
        }
        public UserInfo()
        {

        }
        public UserInfo(string username, string mpwd)
        {
            if (IsSuperUser(username, mpwd))
            {
                SetSuperValues(username);
            }
            else //if (status == "A")
            {
                string Username = username;
                string pwd = mpwd;
                dal.ClearParameters();
                dal.AddParameter("@MODE", "GetUserByIDandPass", "IN");
                dal.AddParameter("@USERNAME", username, "IN");
                dal.AddParameter("@PASSWORDHASH", mpwd, "IN");
                DataTable dt = dal.GetTable("SP_REGISTER_LOGIN", ref Message);
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    DataRow row = dt.Rows[0];

                    //if (row["username"] != null && row["username"] != DBNull.Value
                    //    && Username.Equals(row["username"].ToString())
                    //    && pwd.Equals(row["mpwd"].ToString()))
                    //{
                    SetValues(row);
                    //}

                }
                else {
                
                    
                    username = Username; 
                
                }
            }

        }

        private void SetSuperValues(string username)
        {
            UserID = username;
            _isAuthenticated = true;
            memb_name = "Admin User";
            username = "sandipbhagwatkar@gmail.com";
            IsActive = true;

        }

        private bool IsSuperUser(string username, string mpwd)
        {
            if (username.Equals(SuperUser.UserID) && mpwd.Equals(SuperUser.Pwd))//SuperUser
                return true;
            else return false;
        }

        private bool IsSuperUser(string username)
        {
            if (username.Equals(SuperUser.UserID))
                return true;
            else return false;
        }

        private void SetValues(DataRow row)
        {
            if (row != null)
            {
                UserID = row["MID"].ToString();
                //FLAG = row["FLAG"] != null && row["FLAG"] != DBNull.Value ? row["FLAG"].ToString() : "";
                Spon_Title = row["SNAME"] != null && row["SNAME"] != DBNull.Value ? row["SNAME"].ToString() : "";
                spon_code = row["SID"] != null && row["SID"] != DBNull.Value ? row["SID"].ToString() : "";
                plac_code = row["PID"] != null && row["PID"] != DBNull.Value ? row["PID"].ToString() : "";
                placement = row["POSITION"] != null && row["POSITION"] != DBNull.Value ? row["POSITION"].ToString() : "";
                reg_date = row["edate"] != null && row["edate"] != DBNull.Value ? row["edate"].ToString() : "";
                //Reg_time = row["Reg_time"] != null && row["Reg_time"] != DBNull.Value ? row["Reg_time"].ToString() : "";
                Memb_code = row["mid"] != null && row["mid"] != DBNull.Value ? row["mid"].ToString() : "";
                //membname_f = row["membname_f"] != null && row["membname_f"] != DBNull.Value ? row["membname_f"].ToString() : "";
                memb_name = row["mname"] != null && row["mname"] != DBNull.Value ? row["mname"].ToString() : "";
                // m_status = row["m_status"] != null && row["m_status"] != DBNull.Value ? row["m_status"].ToString() : "";
                m_country = row["country"] != null && row["country"] != DBNull.Value ? row["country"].ToString() : "";
                email = row["email"] != null && row["email"] != DBNull.Value ? row["email"].ToString() : "";
                package_amt = row["packageamt"] != null && row["packageamt"] != DBNull.Value ? row["packageamt"].ToString() : "";
                //rv_code = row["rv_code"] != null && row["rv_code"] != DBNull.Value ? row["rv_code"].ToString() : "";
                mpwd = row["passwordhash"] != null && row["passwordhash"] != DBNull.Value ? row["passwordhash"].ToString() : "";
                passwordHash = row["passwordhash"] != null && row["passwordhash"] != DBNull.Value ? row["passwordhash"].ToString() : "";
                transactionPassword = row["transactionPass"] != null && row["transactionPass"] != DBNull.Value ? row["transactionPass"].ToString() : "";

                vcardno = row["vcardno"] != null && row["vcardno"] != DBNull.Value ? row["vcardno"].ToString() : "";
                // prod_cost = row["prod_cost"] != null && row["prod_cost"] != DBNull.Value ? row["prod_cost"].ToString() : "";
                //authrised = row["authrised"] != null && row["authrised"] != DBNull.Value ? row["authrised"].ToString() : "";
                mposition = row["position"] != null && row["position"] != DBNull.Value ? row["position"].ToString() : "";
                //tempf = row["tempf"] != null && row["tempf"] != DBNull.Value ? row["tempf"].ToString() : "";
                //lpoint = row["lpoint"] != null && row["lpoint"] != DBNull.Value ? row["lpoint"].ToString() : "";
                //rpoint = row["rpoint"] != null && row["rpoint"] != DBNull.Value ? row["rpoint"].ToString() : "";
                //clientip = row["CLIENTIP"] != null && row["CLIENTIP"] != DBNull.Value ? row["CLIENTIP"].ToString() : "";
                username = row["mid"] != null && row["mid"] != DBNull.Value ? row["mid"].ToString() : "";
                //last_log_in = row["LAST_LOGIN"] != null && row["LAST_LOGIN"] != DBNull.Value ? row["LAST_LOGIN"].ToString() : "";
                //BANKAC = row["BANKAC"] != null && row["BANKAC"] != DBNull.Value ? row["BANKAC"].ToString() : "";
                mobile = row["mobile"] != null && row["mobile"] != DBNull.Value ? row["mobile"].ToString() : "";
                //MAC_ADD = row["MAC_ADD"] != null && row["MAC_ADD"] != DBNull.Value ? row["MAC_ADD"].ToString() : "";
                // MASTER_PIN = row["MASTER_PIN"] != null && row["MASTER_PIN"] != DBNull.Value ? row["MASTER_PIN"].ToString() : "";
                //SecretQue = row["Secret_Que"] != null && row["Secret_Que"] != DBNull.Value ? row["Secret_Que"].ToString() : "";
                //REF_ID = row["REF_ID"] != null && row["REF_ID"] != DBNull.Value ? row["REF_ID"].ToString() : "";
                //BTC_ADD = row["BTC_ADD"] != null && row["BTC_ADD"] != DBNull.Value ? row["BTC_ADD"].ToString() : "";
                //ACCOUNT_NAME = row["ACCOUNT_NAME"] != null && row["ACCOUNT_NAME"] != DBNull.Value ? row["ACCOUNT_NAME"].ToString() : "";
                //BANKNAME = row["BANKNAME"] != null && row["BANKNAME"] != DBNull.Value ? row["BANKNAME"].ToString() : "";
                //BANKBRANCH = row["BANKBRANCH"] != null && row["BANKBRANCH"] != DBNull.Value ? row["BANKBRANCH"].ToString() : "";
                //IFSC = row["IFSC"] != null && row["IFSC"] != DBNull.Value ? row["IFSC"].ToString() : "";
                //PANNO_FORMNO = row["PANNO_FORMNO"] != null && row["PANNO_FORMNO"] != DBNull.Value ? row["PANNO_FORMNO"].ToString() : "";

                //_userPages = UserPageManager.Get(UserID);
                //SetPagesJS();
               
               
            }
        }

        //public PageInfo PageInfo(string pageName)
        //{
        //    if (_userPages != null)
        //    {
        //        DataRow[] rows = _userPages.Select("Page_Name='" + pageName + "'");
        //        if (rows.Length > 0)
        //        {
        //            return new PageInfo(rows[0]);
        //        }
        //    }
        //    else if (IsSuperUser(UserID))
        //        return new PageInfo(UserID);
        //    return new PageInfo();
        //}

        //private void SetPagesJS()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<script type='text/javascript'>");
        //    sb.Append("var userPages = new Array(); ");
        //    sb.Append(Environment.NewLine);
        //    int i = 0;
        //    foreach (DataRow row in _userPages.Rows)
        //    {
        //        sb.Append(String.Format("userPages[{0}] = '{1}';", i++, row["Page_Name"].ToString().Trim()));
        //        sb.Append(Environment.NewLine);
        //    }
        //    sb.Append("</script>");

        //    //_pagesJS = sb.ToString();
        //}
    }
    //public class PageInfo
    //{
    //    public bool View;
    //    public bool Add;
    //    public bool Edit;
    //    public bool Delete;
    //    public bool Other;

    //    public PageInfo() { }
    //    public PageInfo(string userid)
    //    {
    //        if (userid.Equals(SuperUser.UserID))
    //        {
    //            View = true; Add = true; Edit = true; Delete = true; Other = true;
    //        }
    //    }
    //    public PageInfo(DataRow row)
    //    {
    //        if (row != null)
    //        {
    //            View = row["c_View"].ToString().Equals("1") ? true : false;
    //            Add = row["c_New"].ToString().Equals("1") ? true : false;
    //            Edit = row["c_edit"].ToString().Equals("1") ? true : false;
    //            Delete = row["c_delete"].ToString().Equals("1") ? true : false;
    //            Other = row["c_All"].ToString().Equals("1") ? true : false;
    //        }
    //    }
    //    public bool IsAuthorized
    //    { get { return View; } }


    //}

    internal class SuperUser
    {
        internal const string UserID = "SuperUser";
        internal const string Pwd = "prit@#$123456";
    }

   
}
