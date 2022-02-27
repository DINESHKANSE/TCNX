using TCNX.commonFunction;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;


public static class GlobalVariable
{
    public static string URL = "www.tcnxcoin.com";
    public static string loginURL = "https://tcnxcoin.com/Login/Login";
    public static string HomeUrl = "https://tcnxcoin.com";
    public static string Name = "AI Universe";
    public static string Logo_URL = "http://www.tcnxcoin.com/Content/Home/images/logo.png";
    public static string Support_Addr = "support@tcnxcoin.com";
    public static string SendEmailid = "support@tcnxcoin.com";
    public static string SendEmailpassword = "Support@987";
    public static string SendEmailhost = "webmail.tcnxcoin.com";

    public static string GetApiKey()
    {
        SqlDblayer dal = new SqlDblayer();
        string Message = string.Empty;
        dal.ClearParameters();
        string Key = dal.ExecuteScaler("SELECT TOP 1 API_KEY FROM ADMINPWD", ref Message).ToString();
        return Key;
    }
}

public class BitcoinAddress
{
    SqlDblayer dal = new SqlDblayer();
    string Message = string.Empty;
    private UserInfo userInfo = Common.CurrentUserInfo;
    ErrorResponse Res = new ErrorResponse();
    string Apikey = GlobalVariable.GetApiKey();

    //******************* TOPUP/ACTIVATION ADDRESS ***************************
    public string TopupBtcAddress(string MembCode, int deposit_amt = 0, string package = "0")
    {
        string rtnValue = "Error";
        string txtMembCode = MembCode;
        string blockioLable = txtMembCode.ToString() + "_Act" + RandomString(8);
        string cmbpackage = package;

        if (string.IsNullOrEmpty(MembCode) || deposit_amt == 0)
        {
            return "Error";
        }

        string deposit_btc = "0";
        try
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new WebClient())
            {
                var xxx = client.DownloadString("https://blockchain.info/tobtc?currency=USD&value=" + deposit_amt);
                deposit_btc = string.Format("{0:N8}", Convert.ToDouble(xxx));
            }
        }
        catch
        {

        }
        try
        {
            if (!string.IsNullOrEmpty(txtMembCode) && Convert.ToDouble(cmbpackage) > 0 && Convert.ToDouble(deposit_amt) > 0)
            {
                string str = "https://block.io/api/v2/get_new_address/?api_key=" + Apikey + "&label=" + blockioLable;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(str);
                httpWebRequest.Method = WebRequestMethods.Http.Get;
                httpWebRequest.Accept = "application/json";
                HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Newtonsoft.Json.Linq.JObject rss = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                try
                {
                    string query = " INSERT INTO BLOCKCHAIN_WITH_STR(MEMB_CODE,REQ_SRNO,RSS_STR,TTYPE)";
                    query += "  VALUES('" + txtMembCode + "','99999','" + rss + "','DEPOSIT')";
                    dal.ClearParameters();
                    int aa = dal.ExecuteNonQuery(query, ref Message);
                }
                catch (Exception ex)
                {
                    dal.ClearParameters();
                    dal.ExecuteNonQuery("INSERT INTO ERROR_MSG(PAGENAME,ERROR,MEMB_CODE) VALUES('DEPOSIT','" + ex.Message + "','" + txtMembCode + "')", ref Message);
                }

                string status = (string)rss["status"];
                Object data = (Object)rss["data"];
                string network = (string)rss["data"]["network"];
                int user_id = (int)rss["data"]["user_id"];
                string address = (string)rss["data"]["address"];
                string label = (string)rss["data"]["label"];

                if (!string.IsNullOrEmpty(address) && address.Length > 5 && status == "success")
                {
                    dal.ClearParameters();
                    dal.AddParameter("@DEPOSIT_AC", address, "IN");
                    dal.AddParameter("@MEMB_CODE", txtMembCode, "IN");
                    dal.AddParameter("@AMOUNT_USD", deposit_amt, "IN");
                    dal.AddParameter("@AMOUNT_BTC", deposit_amt, "IN");
                    dal.AddParameter("@BLOCKIO_LABLE", blockioLable, "IN");
                    dal.AddParameter("@APIKEY", Apikey, "IN");
                    dal.AddParameter("@ETYPE", "PUR", "IN");
                    dal.AddParameter("@MODE", "INSERT", "IN");
                    int x = dal.ExecuteNonQuery("SP_BTC_PURCHASE_REQUEST", ref Message);
                    if (x > 0)
                    {
                        string commit_no = "100";
                        bool clb = SetCallBack(address, commit_no, "TOPUP", txtMembCode);
                        if (clb == true)
                        {
                            return address;
                        }
                    }
                    else
                    {
                        return "Error";
                    }
                }
                else
                {
                    return "Error";
                }
            }
            return "Error";
        }
        catch
        {
            return "Error";
        }
    }

    //******************* POOL TOPUP ADDRESS ***************************

    public string PoolTopupBtcAddress(string MembCode, int deposit_amt = 0, string package = "0", string poolName = "")
    {
        string rtnValue = "Error";
        string txtMembCode = MembCode;
        string blockioLable = txtMembCode.ToString() + "_Act" + RandomString(8);
        string cmbpackage = package;

        if (string.IsNullOrEmpty(MembCode) || deposit_amt == 0)
        {
            return "Error";
        }

        string deposit_btc = "0";
        try
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new WebClient())
            {
                var xxx = client.DownloadString("https://blockchain.info/tobtc?currency=USD&value=" + deposit_amt);
                deposit_btc = string.Format("{0:N8}", Convert.ToDouble(xxx));
            }
        }
        catch
        {
            return "Error";
        }
        try
        {
            if (!string.IsNullOrEmpty(txtMembCode) && Convert.ToDouble(cmbpackage) > 0 && Convert.ToDouble(deposit_amt) > 0)
            {
                dal.ClearParameters();
                dal.AddParameter("@APIKEY", Apikey, "IN");
                dal.AddParameter("@MEMB_CODE", txtMembCode, "IN");
                dal.AddParameter("@MODE", "GET_DEPOSIT_ADDRESS", "IN");
                string addressx = dal.ExecuteScaler("SP_BTC_PURCHASE_REQUEST", ref Message).ToString();
                if (!string.IsNullOrEmpty(addressx) || addressx.Length > 3)
                {

                    //dal.ClearParameters();
                    //dal.AddParameter("@MEMB_CODE", userInfo.Memb_code, "IN");
                    //dal.AddParameter("@AMOUNT", cmbpackage, "IN");
                    //dal.AddParameter("@DONATE_THROUGH", addressx, "IN");
                    //dal.AddParameter("@POOL_NAME", poolName, "IN");
                    //dal.AddParameter("@MODE", "POOL_TOPUP", "IN");
                    //int x01 = dal.ExecuteNonQuery("SP_PAY_BC_GET_BC", ref Message);
                    //if (x01 > 0)
                    //{
                        return addressx;
                    //}
                    //else
                    //{
                    //    return "Error";
                    //}

                }
                if (string.IsNullOrEmpty(addressx) || addressx.Length < 3)
                {
                    if (Convert.ToDouble(deposit_btc) > 0 && Convert.ToDouble(deposit_btc) > 0)
                    {

                        string str = "https://block.io/api/v2/get_new_address/?api_key=" + Apikey + "&label=" + blockioLable;
                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(str);
                        httpWebRequest.Method = WebRequestMethods.Http.Get;
                        httpWebRequest.Accept = "application/json";
                        HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        Newtonsoft.Json.Linq.JObject rss = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                        try
                        {
                            string query = " INSERT INTO BLOCKCHAIN_WITH_STR(MEMB_CODE,REQ_SRNO,RSS_STR,TTYPE)";
                            query += "  VALUES('" + txtMembCode + "','99999','" + rss + "','DEPOSIT')";
                            dal.ClearParameters();
                            int aa = dal.ExecuteNonQuery(query, ref Message);
                        }
                        catch (Exception ex)
                        {
                            dal.ClearParameters();
                            dal.ExecuteNonQuery("INSERT INTO ERROR_MSG(PAGENAME,ERROR,MEMB_CODE) VALUES('DEPOSIT','" + ex.Message + "','" + txtMembCode + "')", ref Message);
                        }
                        string status = (string)rss["status"];
                        Object data = (Object)rss["data"];
                        string network = (string)rss["data"]["network"];
                        int user_id = (int)rss["data"]["user_id"];
                        string address = (string)rss["data"]["address"];
                        string label = (string)rss["data"]["label"];
                        if (!string.IsNullOrEmpty(address) && address.Length > 5 && status == "success")
                        {
                            dal.ClearParameters();
                            dal.AddParameter("@DEPOSIT_AC", address, "IN");
                            dal.AddParameter("@MEMB_CODE", txtMembCode, "IN");
                            dal.AddParameter("@AMOUNT_USD", deposit_amt, "IN");
                            dal.AddParameter("@AMOUNT_BTC", deposit_btc, "IN");
                            dal.AddParameter("@BLOCKIO_LABLE", blockioLable, "IN");
                            dal.AddParameter("@APIKEY", Apikey, "IN");
                            dal.AddParameter("@ETYPE", "PUR", "IN");
                            dal.AddParameter("@MODE", "INSERT", "IN");
                            int x = dal.ExecuteNonQuery("SP_BTC_PURCHASE_REQUEST", ref Message);
                            if (x > 0)
                            {
                                bool clb = SetCallBack(address, "0", "POOL", txtMembCode.ToString());
                                if (clb == true)
                                {
                                    //dal.ClearParameters();
                                    //dal.AddParameter("@MEMB_CODE", userInfo.Memb_code, "IN");
                                    //dal.AddParameter("@AMOUNT", cmbpackage, "IN");
                                    //dal.AddParameter("@DONATE_THROUGH", address, "IN");
                                    //dal.AddParameter("@POOL_NAME", poolName, "IN");
                                    //dal.AddParameter("@MODE", "POOL_TOPUP", "IN");
                                    //int x01 = dal.ExecuteNonQuery("SP_PAY_BC_GET_BC", ref Message);
                                    //if (x01 > 0)
                                    //{
                                        return address;
                                    //}
                                }
                                else
                                {
                                    return "Error";
                                }
                            }
                            else
                            {
                                return "Error";
                            }
                        }
                        else
                        {
                            return "Error";
                        }
                    }
                    return "Error";
                }
            }
            return "Error";
        }
        catch { return "Error"; }
    }

    public bool SetCallBack(string addr, string commitno, string ttypex, string memb_code)
    {
        bool strReturn = false;
        string strCallBack = "https://block.io/api/v2/create_notification/?api_key=" + Apikey + "&type=address&address=" + addr + "&url=http://world.globalcharityworld.com";
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(strCallBack);
        httpWebRequest.Method = WebRequestMethods.Http.Get;
        httpWebRequest.Accept = "application/json";
        HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;
        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        Newtonsoft.Json.Linq.JObject rss = Newtonsoft.Json.Linq.JObject.Parse(responseString);

        try
        {
            string query = " INSERT INTO BLOCKCHAIN_WITH_STR(MEMB_CODE,REQ_SRNO,RSS_STR,TTYPE)";
            query += "  VALUES('" + memb_code + "','787878','" + rss + "','GENERATE_CLBK')";
            dal.ClearParameters();
            int aa = dal.ExecuteNonQuery(query, ref Message);
        }
        catch (Exception ex)
        {
            dal.ClearParameters();
            dal.ExecuteNonQuery("INSERT INTO ERROR_MSG(PAGENAME,ERROR,MEMB_CODE) VALUES('787878','" + ex.Message + "','" + memb_code + "')", ref Message);
        }

        string status = (string)rss["status"];
        Object data = (Object)rss["data"];
        string network = (string)rss["data"]["network"];
        string notification_id = (string)rss["data"]["notification_id"];
        string type = (string)rss["data"]["type"];
        bool enabled = (bool)rss["data"]["enabled"];
        string url = (string)rss["data"]["url"];
        if (status == "success")
        {
            dal.ClearParameters();
            dal.AddParameter("@NOTIFICATION_ID", notification_id, "IN");
            dal.AddParameter("@MEMB_CODE", memb_code, "IN");
            dal.AddParameter("@NETWORK", network, "IN");
            dal.AddParameter("@TYPE", type, "IN");
            dal.AddParameter("@DATA", url, "IN");
            dal.AddParameter("@DEPOSIT_AC", addr, "IN");
            dal.AddParameter("@MODE", "UPDATE_CALLBACK", "IN");
            int x = dal.ExecuteNonQuery("SP_BTC_PURCHASE_REQUEST", ref Message);
            if (x > 0)
            {
                strReturn = true;
            }
            else
            {
                strReturn = false;
            }
        }
        else
        {
            strReturn = false;
        }
        return strReturn;
    }

    Random rand = new Random();
    private string RandomString(int Size)
    {
        string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder builder = new StringBuilder();
        char ch;
        for (int i = 0; i < Size; i++)
        {
            ch = input[rand.Next(0, input.Length)];
            builder.Append(ch);
        }
        return builder.ToString();
    }

}

