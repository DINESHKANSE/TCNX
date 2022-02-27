using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using TCNX.commonFunction;
using TCNX.Models;
using System.Net;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace TCNX.commonFunction
{

    public enum TrHistoryEnum
    {
        Activate = 0,
        Activate1 = 1,
        DPPurchase = 2,
        MFSell = 3,
        HelpLink = 5,

        SponsorIncome = 7,
        LevelIncome = 8,
        GrowthIncome = 9,
        GrowthSponsorBonus = 10,
        RepurchaseIncome = 11,
        Withdrawal = 20

    }

    public class Common
    {
        public static string gSuspendID;
        public static string ipaddress;
        public static string location;
        public static string devicename;
        public static string TCurrency="INR";
        public static SessionUtility sessionUtility = new SessionUtility();
        private static  IHttpContextAccessor _contextAccessor;
        public Common(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            sessionUtility = new SessionUtility(contextAccessor);
        }
        public static string CurrentPageName
        {
            get
            {
                string sPath = System.Web.HttpContext.Current.Request.Path;
                System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string sRet = oInfo.Name.ToLower();
                return sRet;
            }
        }

        public static bool IsLoginPage
        {
            get
            {
                string pageId = CurrentPageName.Replace(".aspx", "");
                return pageId.Equals("login") ? true : false;
            }
        }
      
        public static string CookieUserID
        {
            get
            {
                return System.Web.HttpContext.Current.Request.Cookies["Email"] != null ?
                    System.Web.HttpContext.Current.Request.Cookies["Email"].FirstOrDefault().ToString() : "";

            }
            set
            {
                CookieOptions option = new CookieOptions();

                System.Web.HttpContext.Current.Response.Cookies.Append("UserId", value, option);
      
            }
        }

        public   string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:X2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }




        public   string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }


        public   string Decrypt(string cipherText="")
        {
            string EncryptionKey = "9579012525";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public   string Encrypt(string clearText)
        {
            string EncryptionKey = "9579012525";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }

        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        } 


        private string alphanums = "9049332677SANDIPBHAGWATKAR16039878";
        private const int codeLen = 6; //Length of coded string. Must be at least 4

        public string EncodeNumber(string numx)
        {
            int num = Convert.ToInt32(numx);

            if (num < 1 || num > 999999) //or throw an exception
                return "";
            int[] nums = new int[codeLen];
            int pos = 0;

            while (!(num == 0))
            {
                nums[pos] = num % alphanums.Length;
                num /= alphanums.Length;
                pos += 1;
            }

            string result = "";
            foreach (int numIndex in nums)
                result = alphanums[numIndex].ToString() + result;

            return result;
        }

        public int DecodeNumber(string str)
        {
            //Check for invalid string
            if (str.Length != codeLen) //Or throw an exception
                return -1;
            long num = 0;

            foreach (char ch in str)
            {
                num *= alphanums.Length;
                num += alphanums.IndexOf(ch);
            }

            //Check for invalid number
            if (num < 1 || num > 999999) //or throw exception
                return -1;
            return System.Convert.ToInt32(num);
        }

        public static UserInfo CurrentUserInfo
        {
            get
            {
                //UserInfo userInfo = (UserInfo)sessionUtility.GetSession("UserInfo");
               // var info = System.Web.HttpContext.Current.Session.GetObjectFromJson("UserInfo");
                UserInfo userInfo = System.Web.HttpContext.Current.Session.GetObjectFromJson<UserInfo>("UserInfo");
                if (userInfo == null && !String.IsNullOrEmpty(CookieUserID))
                {
                    userInfo = new UserInfo(CookieUserID);
                    CurrentUserInfo = userInfo;
                }
                return userInfo;
            }
            set
            {
                System.Web.HttpContext.Current.Session.SetObjectAsJson("UserInfo", value);
            }
        }

      
        public string ConvertNumbertoWords(int number)
        {
            if (number == 0) return "Zero";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " Lakh ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " Hundred ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "And ";
                var unitsMap = new[]   
        {  
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"  
        };
                var tensMap = new[]   
        {  
            "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"  
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public string DataTableToJSon(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public static string filter(string x)
        {
            x = x.Replace("http:", "_");
            x = x.Replace("-1", "_");
            x = x.Replace("-1;", "_");
            x = x.Replace("+", "_");
            x = x.Replace("-", "_");
            x = x.Replace("'", "_");
            x = x.Replace("?", "_");
            x = x.Replace(";", "_");
            x = x.Replace(" or ", "_");
            x = x.Replace("select", "_");
            x = x.Replace("delete", "_");
            x = x.Replace("update", "_");
            x = x.Replace("union", "_");
            x = x.Replace("response.write", "_");
            x = x.Replace("pg_sleep", "_");
            x = x.Replace("windowswin.ini", "_");
            x = x.Replace("print(", "_");
            x = x.Replace("md5", "_");
            x = x.Replace(".ini", "_");
            x = x.Replace("windowswin.ini", "_");
            x = x.Replace("../", "_");
            x = x.Replace("boot.ini", "_");
            x = x.Replace("WEB-INF", "_");
            x = x.Replace("web.xml", "_");
            x = x.Replace("windows", "_");
            x = x.Replace(".CONFIG", "_");
            x = x.Replace(".ASP", "_");
            x = x.Replace(".PHP", "_");
            x = x.Replace(".ASPX", "_");
            x = x.Replace("/", "_");
            x = x.Replace("script", "_");
            x = x.Replace("<", "_");
            x = x.Replace(">", "_");
            x = x.Replace("sysobjects", "_");
            x = x.Replace("EXEC", "_");
            x = x.Replace("DROP", "_");
            x = x.Replace("<%", "_");
            x = x.Replace("%>", "_");
            x = x.Replace("createobject", "_");
            x = x.Replace("(", "_");
            x = x.Replace(")", "_");
            x = x.Replace("WScript", "_");
            x = x.Replace("Shell", "_");
            x = x.Replace("exec", "_");
            x = x.Replace("UTL_INADDR", "_");
            x = x.Replace("GET_HOST_ADDRESS", "_");
            x = x.Replace("isnull", "_");
            x = x.Replace("concat", "_");
            x = x.Replace("||", "_");

            return x;
        }

        public static string GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }

            return ipInfo.Country;
        }

       
    }
}