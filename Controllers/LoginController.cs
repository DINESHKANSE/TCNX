
using TCNX.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TCNX.Models.DBModel;
using TCNX.commonFunction;

using System;
using CurrentContext = System.Web.HttpContext;
using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace TCNX.Controllers
{
 
    public class LoginController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        SqlDblayer dal = new SqlDblayer();
        private string Query = "";
        private string Message = "";
        decimal levelamt = 0;
        int level = 0;
        public LoginController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IActionResult Index(string refid)   
        {
            string id1 = "TCNX";
            try
            {
                if(!string.IsNullOrEmpty(refid))
                {
                    if (refid.ToString().Trim().Length > 3)
                    {
                        var refferal = _dbContext.tblregistration.Where(x => x.mid.ToLower() == refid.ToLower()).FirstOrDefault();
                        if(refferal!=null)
                        {
                            TempData["spname"] = refferal.mname;
                        }
                        
                        CurrentContext.Current.Session.SetString("refid", refid.ToString());
                        id1 = refid;
                    }
                    else
                    {
                        CurrentContext.Current.Session.SetString("refid", "TCNX");
                    }
                }
               
            }
            catch
            {
                CurrentContext.Current.Session.SetString("refid", "TCNX");
            }
            ////@ViewBag.Id = id1;
            TempData["refid"] = id1;
            
            //UserInfo userInfo = new UserInfo(_Logmodel.username, _Logmodel.password);
            return View();
        }

        public IActionResult Register(string refid)
        {
            string id1 = "TCNX";
            try
            {
                if (!string.IsNullOrEmpty(refid))
                {
                    if (refid.ToString().Trim().Length > 3)
                    {
                        CurrentContext.Current.Session.SetString("refid", refid.ToString());
                        id1 = refid;
                    }
                    else
                    {
                        CurrentContext.Current.Session.SetString("refid", "TCNX");
                    }
                }

            }
            catch
            {
                CurrentContext.Current.Session.SetString("refid", "TCNX");
            }
            ////@ViewBag.Id = id1;
            TempData["refid"] = id1;

            //UserInfo userInfo = new UserInfo(_Logmodel.username, _Logmodel.password);
            return View();
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetSponcer(string SponsorID)
        {
            string rtnText = "";
            var spdetails = _dbContext.tblregistration.Where(x => x.mid.ToLower() == SponsorID.ToLower()).FirstOrDefault();
            if (spdetails != null)
            {
                if (!string.IsNullOrEmpty(spdetails.mname))
                {
                    rtnText = "True-" + spdetails.mname;
                      return Ok(rtnText);
                }

                rtnText = "False-Sponsor Id Is Not Valid ..!";
                  return Ok(rtnText);
            }
            else
            {
                rtnText = "False-Sponsor Id Is Not Valid ..!";
                  return Ok(rtnText);
            }

          
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public  IActionResult LoginAjax(IFormCollection frm)
        {
            string rtnText = "";
            UserLoginModels _Logmodel = new UserLoginModels();
            string walletaddress = frm["txtwalletaddress"].ToString();
            if (string.IsNullOrEmpty(walletaddress))
            {
                rtnText = "Unauthorized-Access.Please enter valid Address.";
                return Ok(rtnText);
                
            }

            tblregistration tblregistration =   _dbContext.tblregistration.Where(x => x.tron_add == walletaddress).FirstOrDefault();
            if (tblregistration == null)
            {
                rtnText = "Register-User Not Register.";
                return Ok(rtnText);

            }
            if (tblregistration !=null )
            {
                if(tblregistration.activate == true)
                {
                    _Logmodel.username = tblregistration.mid;
                    _Logmodel.password = tblregistration.passwordHash;
                }
                else
                {
                    rtnText = "False-Invalid username or password.";
                    return Ok(rtnText);

                }
            }
           
            //_Logmodel.username = frm["txtusername"];
            //_Logmodel.password = frm["txtpassword"];
          


            try
            {
                if (!string.IsNullOrEmpty(_Logmodel.username) && !string.IsNullOrEmpty(_Logmodel.password))
                {


                    // Set UserInfo
                    var userdetails =  _dbContext.tblregistration.Where(x => x.mid.ToLower() == _Logmodel.username.ToLower() && x.passwordHash == _Logmodel.password).FirstOrDefault();

                    //dt = dal.GetTable("select *", ref Message);
                    if (userdetails != null)
                    {
                        //dal.ClearParameters();
                        //string ACTIVE_STATUS = dal.ExecuteScaler("SELECT ACTIVE_STATUS FROM ENTRY WHERE USERNAME='" + _Logmodel.username + "'", ref Message).ToString();
                        //if (ACTIVE_STATUS=="N")
                        //{
                        //    rtnText = "False-Please verify your email id, Get Verification link by Forgot Password";
                        //    return Ok(rtnText, System.Web.Mvc.JsonRequestBehavior.AllowGet);
                        //}

                        if (userdetails.blocked == true)
                        {
                            rtnText = "False-Your account blocked, Please contact support@turbotraining.live.";
                            return Ok(rtnText);
                        }

                        UserInfo userInfo = new UserInfo(_Logmodel.username, _Logmodel.password);
                        if (String.IsNullOrEmpty(userInfo.username))
                        {
                            rtnText = "False-Invalid User";
                            return Ok(rtnText);
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(userdetails.mid))
                            {
                                CookieOptions option = new CookieOptions();

                                System.Web.HttpContext.Current.Response.Cookies.Append("username", _Logmodel.username, option);
                                System.Web.HttpContext.Current.Response.Cookies.Append("password", _Logmodel.password, option);
                                
                                
                                // set cookie expiration in a month
                                //Response.Cookies["username"].Expires = DateTime.Now.AddMonths(1);
                                //Response.Cookies["password"].Expires = DateTime.Now.AddMonths(1);

                                Common.CurrentUserInfo = userInfo;
                                Common.CookieUserID = String.Empty;
                                string pcName = System.Environment.MachineName;


                                ////dal.ClearParameters();
                                string ipaddress;
                                //ipaddress = System.Web.HttpContext.Current.Request.se.ServerVariables["HTTP_X_FORWARDED_FOR"];
                                ipaddress = System.Web.HttpContext.Current.Response.HttpContext.Connection.RemoteIpAddress.ToString();
                                if (ipaddress == "" || ipaddress == null)
                                    ipaddress = HttpContext.Connection.RemoteIpAddress.ToString();
                                Common.ipaddress = ipaddress;

                                Common.devicename = pcName;
                                Common.location = Common.GetUserCountryByIp(ipaddress);

                                //dal.AddParameter("@CLIENTIP", ipaddress, "IN");
                                //dal.AddParameter("@MEMB_CODE", userInfo.Memb_code, "IN");
                                //dal.AddParameter("@MODE", "UPDATEIP", "IN");
                                //dal.ExecuteNonQuery("SP_REGISTER_LOGIN", ref Message);

                                CurrentContext.Current.Session.SetString("NEW_CLIENTIP", ipaddress.FirstOrDefault().ToString());

                                //FormsAuthentication.SetAuthCookie(_Logmodel.username, false);
                                var identity = new ClaimsIdentity(new[] {
                                    new Claim(ClaimTypes.Name, _Logmodel.username)
                                }, CookieAuthenticationDefaults.AuthenticationScheme);

                                var principal = new ClaimsPrincipal(identity);

                                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                                //SetLog(userInfo);

                                CurrentContext.Current.Session.SetString("MID",userdetails.mid);

                                //Session["XPUB"] = "";
                                //dal.ClearParameters();
                                //string XPUB = dal.ExecuteScaler("SELECT TOP 1 XPUB FROM ADMINPWD", ref Message).ToString();
                                //if (!string.IsNullOrEmpty(XPUB))
                                //{
                                //    Session["XPUB"] = XPUB;
                                //}

                                //if (dt.Rows[0]["W_RULE"].ToString().Trim() == "N")
                                //{
                                //    var rnd = new Random(DateTime.Now.Millisecond);
                                //    int ticks = rnd.Next(1000, 9000);
                                //    Session["OPT"] = Convert.ToString(ticks);

                                //    dal.ClearParameters();
                                //    string url = dal.ExecuteScaler("select url from sms", ref Message).ToString();
                                //    string url1 = url;

                                //    url = url.Replace("XXMOBXX", userInfo.mobile);
                                //    string msg = "Your OTP is " + ticks + " for login to the requested application. www.ORANGE_CITY_FUN_AND_EARN.com";
                                //    url = url.Replace("XXMESSAGEXX", msg);

                                //    using (var client = new WebClient())
                                //    {
                                //        var xxx = client.DownloadString(url);
                                //    }
                                //    return RedirectToAction("Verify", "Login");
                                //}


                                bool status = false;
                                string message = "";
                                //userInfo.ACTIVE2FA = "N";

                                //dal.ClearParameters();
                                //dal.AddParameter("@MEMB_CODE", userInfo.Memb_code, "IN");
                                //dal.AddParameter("@MODE", "CHECK_2FA", "IN");
                                //string fa2 = dal.ExecuteScaler("SP_REGISTER_LOGIN", ref Message).ToString();
                                //if (fa2 == "on")
                                //{
                                //    status = true; // show 2FA form
                                //    message = "2FA Verification";
                                //    userInfo.ACTIVE2FA = "Y";
                                //    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                                //    string UserUniqueKey = (userInfo.username.ToLower() + key); //as Its a demo, I have done this way. But you should use any encrypted value here which will be unique value per user.
                                //    Session["UserUniqueKey"] = UserUniqueKey;
                                //    var setupInfo = tfa.GenerateSetupCode("Day2dayhelps", userInfo.username.ToLower(), UserUniqueKey, 300, 300);
                                //    ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                                //    ViewBag.SetupCode = setupInfo.ManualEntryKey;
                                //    ViewBag.Message = message;
                                //    ViewBag.Status = status;
                                //    rtnText = "True2FA-" + userInfo.username;
                                //    return Ok(rtnText, System.Web.Mvc.JsonRequestBehavior.AllowGet);
                                //}
                                //else
                                //{
                                rtnText = "True-Success";
                                // return Ok(rtnText, System.Web.Mvc.JsonRequestBehavior.AllowGet);
                                //var get1 = new Newtonsoft.Json.JsonSerializerSettings();

                                return Ok(rtnText);
                                //}
                            }
                            else
                            {
                                rtnText = "False-Invalid username or password.";
                                //return Ok(rtnText, System.Web.Mvc.JsonRequestBehavior.AllowGet);
                                return Ok(rtnText);
                            }
                        }
                    }
                    else
                    {

                       
                        rtnText = "False-Invalid username or password.";
                        //return Ok(rtnText, System.Web.Mvc.JsonRequestBehavior.AllowGet);
                        return Ok(rtnText);

                    }



                     
                }
                else
                {
                    rtnText = "False-Please enter valid login details.";
                    return Ok(rtnText);
                    //return Ok(rtnText, System.Web.Mvc.JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                rtnText = "False-Please enter valid login details.";
                return Ok(rtnText);
            }
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult UserRegister(string walletaddress)
        {
            try
            {
                String rtnText = "";
                var chkusr = _dbContext.tblregistration.Where(x => x.tron_add == walletaddress);
                if (chkusr != null && chkusr.Count() > 0)
                {
                    string mid = chkusr.FirstOrDefault().mid;

                    if (!string.IsNullOrEmpty(mid.ToString()))
                    {
                        rtnText = "True-Welcome to TCNX.";
                        return Ok(rtnText);
                    }
                }
               
                    rtnText = "False-Trx";
                    return Ok(rtnText);
                
            }
            catch(Exception ex)
            {
              
                return Ok("False-Trx");
            }
           
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult RegisterAjax(IFormCollection frm)
        {
            int cntchk = 0;

            string txtName = "TCNX";
            string txtuserID = "TCNX";

            //string cmbcountry = frm.Get("cmbcountry");
            string walletaddress = frm["txtwalletaddress"].ToString();
            string cmbcountry = "India";
            int countrycode = Convert.ToInt32(91);
            var countrydetails = "India";
            string cmbcountry_code ="91";
            cmbcountry = "India";

            string cmbstate = "IND";
            string txtcity = "Ind";

            string txtState = "IND";

            string txtMobileNo = "9999999999";
            string txtSP_Code = frm["txtsponsor"].ToString();
            string OptLeft = "Left";
            OptLeft = frm["cmbposition"].ToString();
            string package = frm["cmbpackage"].ToString();
            string packagetype = frm["cmbpackagetype"].ToString();
            switch (package)
            {
                case "50":
                    packagetype = "1";
                    break;
                case "100":
                    packagetype = "2";
                    break;
                case "200":
                    packagetype = "3";
                    break;
                
            }

          
               
            string cmbGender = "MALE";
            string spon_name = frm["lblsponsor"].ToString();
            string txtspname = frm["lblsponsor"].ToString();
            string transid = frm["transid"].ToString();
            string txtemail = "user@gmail.com";
            //string txtRemail = frm.Get("txtRemail");
            string txtTranpass = RandomString(6); // frm.Get("txtTrpass"); // RandomString(6); // frm.Get("txtTranpass");
            string txtpass = "Pass@123";  //"EG" + RandomString(4); //frm.Get("txtpass");
            string txtCpass = "Pass@123";

            string rtnText = "";



            //if (txtpass != txtCpass)
            //{
            //    rtnText = "False-Please enter valid pasword/confirm password not match";
            //      return Ok(rtnText);
            //}
            if (string.IsNullOrEmpty(txtpass))
            {
                rtnText = "False-Please enter password..!";
                return Ok(rtnText);
            }
            if (string.IsNullOrEmpty(txtTranpass))
            {
                rtnText = "False-Please enter transaction password..!";
                return Ok(rtnText);
            }

            if (string.IsNullOrEmpty(cmbcountry))
            {
                rtnText = "False-Please select your country..!";
                return Ok(rtnText);
            }
            if (string.IsNullOrEmpty(txtcity.Trim()) || txtcity.Length < 3)
            {
                rtnText = "False-Please enter valid city..!";
                return Ok(rtnText);
            }
            if (string.IsNullOrEmpty(txtState.Trim()) || txtState.Length < 3)
            {
                rtnText = "False-Please enter valid state..!";
                return Ok(rtnText);
            }

            if (OptLeft == "0")
            {
                rtnText = "False-Please select placement..!";
                return Ok(rtnText);
            }
            //if (IsEmail(txtemail) == false)
            //{
            //    rtnText = "False-Please enter valid email address..!";
            //    return Ok(rtnText);
            //}
            if (string.IsNullOrEmpty(txtuserID))
            {
                rtnText = "False-Please enter valid username id..!";
                return Ok(rtnText);
            }
            //if (string.IsNullOrEmpty(txtspname))
            //{
            //    rtnText = "False-Please enter sponsor id ..!";
            //    return Ok(rtnText);
            //}
            if (string.IsNullOrEmpty(txtuserID))
            {
                rtnText = "False-Please enter username ..!";
                return Ok(rtnText);
            }
            if (string.IsNullOrWhiteSpace(txtuserID))
            {
                rtnText = "False-Space not allowd in username..!";
                return Ok(rtnText);
            }
            if (string.IsNullOrEmpty(txtName))
            {
                rtnText = "False-Please enter name ..!";
                return Ok(rtnText);
            }
            if (string.IsNullOrEmpty(txtMobileNo))
            {
                rtnText = "False-Please enter mobile number ..!";
                  return Ok(rtnText);
            }
            if (IsNumeric(txtMobileNo) == false)
            {
                rtnText = "False-Please enter valid mobile no..!";
                return Ok(rtnText);
            }

            if (!string.IsNullOrEmpty(txtMobileNo))
            {
                //dal.ClearParameters();
                //dal.AddParameter("@MOBILE_NO", txtMobileNo, "IN");
                //dal.AddParameter("@MODE", "CHECK_MOBILE", "IN");
                var mobchk = _dbContext.tblregistration.Where(x => x.mobile == txtMobileNo).Count();
                //if ( mobchk > 0)
                //{


                //        rtnText = "False-Mobile Number Already Registered ..!";
                //        return Json(rtnText, JsonRequestBehavior.AllowGet);
                //}
            }

            if (!string.IsNullOrEmpty(txtemail))
            {
                //var emailchk = dbAIUniverseEntities.tblregistrations.Where(x=> x.email == txtemail).Count();
                //if (emailchk > 0)
                //{ 
                //        rtnText = "False-Email ID Is Already Registered ..!";
                //        return Json(rtnText, JsonRequestBehavior.AllowGet);

                //}
            }
            var transidcnt = _dbContext.tbltranshistory.Where(x => transid==x.txthash).FirstOrDefault();
            if(transidcnt != null)
            {
                if(transidcnt.txthash.Count() > 0)
                {
                    rtnText = "False-Duplicate transaction.This Transaction ID already available in the system.";
                    return Ok(rtnText);
                }
                
            }
            var packageid = _dbContext.tblpackage.Where(x => x.packageamt == Convert.ToDecimal(package)).FirstOrDefault();


            if (!string.IsNullOrEmpty(txtSP_Code) && !string.IsNullOrEmpty(txtName) && !string.IsNullOrEmpty(txtuserID) && !string.IsNullOrEmpty(txtpass))
            {
                //dal.ClearParameters();
                //dal.AddParameter("@USERNAME", spon_name, "IN");
                //dal.AddParameter("@MODE", "CheckSponser", "IN");
                var spdetails = _dbContext.tblregistration.Where(x => x.mid.ToLower() == txtSP_Code.ToLower()).FirstOrDefault();
                if (spdetails != null && !string.IsNullOrEmpty(spdetails.mname))
                {
                    if (spdetails.mname == null)
                    {
                        rtnText = "False-Sponsor Id Is Not Valid ..!";
                        return Ok(rtnText);
                    }

                }
                else
                {
                    rtnText = "False-Sponsor Id Is Not Valid ..!";
                    return Ok(rtnText);
                }

                

                //var chkusr = _dbContext.tblregistration.Where(x => x.mid.ToLower() == txtuserID.ToLower());
                //if (chkusr != null && chkusr.Count() > 0)
                //{
                //    string mid = chkusr.FirstOrDefault().mid;

                //    if (!string.IsNullOrEmpty(mid.ToString()))
                //    {
                //        rtnText = "False-Username Is Already Registered OR Not Valid ..!";
                //          return Ok(rtnText);
                //    }
                //}
                var chkusr = _dbContext.tblregistration.Where(x => x.tron_add ==walletaddress);
                if (chkusr != null && chkusr.Count() > 0)
                {
                    string mid = chkusr.FirstOrDefault().mid;

                    if (!string.IsNullOrEmpty(mid.ToString()))
                    {
                        rtnText = "False-Trx Address Is Already Registered OR Not Valid ..!";
                        return Ok(rtnText);
                    }
                }
                string ipaddress = "";
                try
                {
                    //ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                catch (Exception ex)
                {

                }
                
                string Message = "";
                if (ipaddress == "" || ipaddress == null)
                    ipaddress = HttpContext.Connection.RemoteIpAddress.ToString();
                dal.ClearParameters();
                dal.AddParameter("@PASSWORDHASH", txtpass, "IN");
                dal.AddParameter("@TRANSACTIONPASS", txtTranpass, "IN");
                dal.AddParameter("@USERNAME", txtuserID, "IN");
                dal.AddParameter("@SID", txtSP_Code, "IN");
                dal.AddParameter("@EMAIL", txtemail, "IN");
                dal.AddParameter("@MNAME", txtName, "IN");
                dal.AddParameter("@MOBILE", txtMobileNo, "IN");
                dal.AddParameter("@City", txtcity, "IN");
                dal.AddParameter("@STATE", txtState, "IN");
                //dal.AddParameter("@CLIENTIP", ipaddress, "IN");
                dal.AddParameter("@PACKAGEID", packageid.packageid, "IN");
                dal.AddParameter("@PACKAGEAMT", Convert.ToString(packageid.packageamt), "IN");
                dal.AddParameter("@PLANTYPE", Convert.ToInt32(packagetype), "IN");
                dal.AddParameter("@LINKAMT", 0, "IN");
                dal.AddParameter("@MEMB_CODEX", "", "OUT");
                dal.AddParameter("@COUNTRY", cmbcountry, "IN");
                dal.AddParameter("@TRON_ADD",walletaddress, "IN");
                dal.AddParameter("@COUNTRY_CODE", cmbcountry, "IN");
                //dal.AddParameter("@MAC_ADD", GetMACAddress(), "IN");
                dal.AddParameter("@ACTIVATE", 1, "IN");
                dal.AddParameter("@POSITION", OptLeft, "IN");
                dal.AddParameter("@MODE", "REGISTERDAPP", "IN");
                cntchk = dal.ExecuteNonQuery("SP_REGISTER_LOGIN", ref Message);
            }
            if (cntchk > 0)
            {
                //TempData["msg"] = "Registration Successfully" + Convert.ToInt32(dal.ReturnParameter);
                CurrentContext.Current.Session.SetString("MID", dal.ReturnParameter);
                string regid = Convert.ToString(dal.ReturnParameter);
                TempData["MID"] = regid;

                levelamt = Convert.ToDecimal(packageid.packageamt) ;
                var mySid = _dbContext.tblregistration.Where(x => x.mid.ToLower() == regid.ToLower()).FirstOrDefault();
                dal.ClearParameters();

                Query = "Insert into tblTransHistory (edate,givemid,takemid,tamount,approvedstatus,status,ttype,bid,type,imgname,deduction,final_amount,txthash,payment_id) values (" + "CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') AS DATETIME),'" + (regid.ToUpper()) + "','" + (regid.ToUpper()) + "'," + Convert.ToDecimal(levelamt) + ",1,' Purchase :" + Convert.ToDecimal(levelamt) + "'," + (int)TrHistoryEnum.InActive + ",1,1,'dkimg/slip.png',0,0,'" + transid + "','" + transid + "')";
                dal.ExecuteNonQuery(Query, ref Message);

                dal.ClearParameters();
                Query = "Update tblincome set sponcer=CONVERT(float, sponcer)+" + (Convert.ToDecimal(levelamt) * Convert.ToDecimal(0.1)) + ",withdraw=CONVERT(float, withdraw)+" + (levelamt) + " where Mid='" + mySid.sid + "'";
                dal.ExecuteNonQuery(Query, ref Message);

                int tid = 1;
                try
                {
                    tid = Convert.ToInt32(dal.ExecuteScaler("select max(tid) from tbltranshistory", ref Message));
                }
                catch (Exception ex)
                {

                }
                
                tid = tid + 1;

                Query = "Insert into tblTransHistory (edate,givemid,takemid,tamount,approvedstatus,status,ttype,bid,type,imgname,deduction,final_amount) values (" + "CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') AS DATETIME),'" + mySid.sid + "','" + (regid.ToUpper()) + "'," + Convert.ToDecimal(Convert.ToDecimal(levelamt) * Convert.ToDecimal(0.1)) + ",1,' Refferal Income :" + Convert.ToDecimal(Convert.ToDecimal(levelamt) * Convert.ToDecimal(0.1)) + "'," + (int)TrHistoryEnum.SponsorIncome + ",0,1,'dkimg/slip.png',0,0)";
                dal.ExecuteNonQuery(Query, ref Message);


                try
                {
                    level = 1;
                    calculatelevelincome(regid.ToUpper(), Convert.ToDecimal(levelamt), regid.ToUpper());
                }
                catch (Exception ex)
                {

                }
                rtnText = "True-Registration Successfully";

                string txtemail_msg = string.Empty;

                //Common x = new Common();
                //string urlx = "http://5050cfhands.com/Login/Verify?keyx=" + Server.UrlEncode(HttpUtility.UrlEncode(x.Encrypt(dt.Rows[0]["REF_ID"].ToString().Trim())));

                //txtemail_msg = "<html><body> <p style='center'>";
                //txtemail_msg = txtemail_msg + "<strong> AI Universe </strong> <br> <br>";
                //txtemail_msg = txtemail_msg + " Sign up successfully done. ";
                //txtemail_msg = txtemail_msg + "<br>";
                //txtemail_msg = txtemail_msg + " Dear Sir/Madam  <br>";
                //txtemail_msg = txtemail_msg + "<br>Thank you for join with us.please login to system using below credentials :-<br>";
                //txtemail_msg = txtemail_msg + "<br>  <a class='btn btn-success-outline' href='" + "https://aiuniverse.live/Login/Login" + "'> Login </a> for user :" + txtuserID;
                //txtemail_msg = txtemail_msg + "<br > <table  class='table table-bordered table - responsive'> <tr> <td> Name : </td> <td> " + txtName + " </td></tr>";
                //txtemail_msg = txtemail_msg + " <tr> <td> Username : </td> <td> " + txtuserID + " </td></tr>";
                //txtemail_msg = txtemail_msg + " <tr> <td> password : </td> <td> " + txtpass + " </td></tr>";
                //txtemail_msg = txtemail_msg + " <tr> <td> Transaction Pass : </td> <td> " + txtTranpass + " </td></tr>";
                //txtemail_msg = txtemail_msg + "<br> Powered By : AI universe ";
                //txtemail_msg = txtemail_msg + "<br> ";
                //if (dt.Rows[0]["ACTIVE_STATUS"].ToString().Trim() == "N")
                //{
                //    txtemail_msg = txtemail_msg + "<p>Email Verification Link : <a href='" + urlx + "'> " + urlx + "</a> </p>";
                //}
                txtemail_msg = txtemail_msg + "  <p>  If you have any query, contact us on " + GlobalVariable.Support_Addr + "</p><br />";
                txtemail_msg = txtemail_msg + "<br>--<br>";
                txtemail_msg = txtemail_msg + "Regards <br>" + GlobalVariable.Name + " <br>" + GlobalVariable.URL + " </p></body></html>";


                ///======================================
                ////SendMail(GlobalVariable.loginURL, "", txtemail, txtemail_msg, "AIUniverse signup successfully done");
                //RedirectToAction("RegSuccess", new { id = 0 });
                //return Json(null);

                  return Ok(rtnText);
            }
            else
            {
                rtnText = "False-Please enter valid information..!";
                return Ok(rtnText);
            }
        }

        Random rand = new Random();
        private string RandomString(int Size)
        {
            string input = "DINESH06041991KANSE19011989";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = input[rand.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }
        public static bool IsNumeric(string str)
        {

            string strRegex = @"\d{10}";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(str))
                return (true);
            else
                return (false);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult GetVideoMappingsAsync(IFormCollection frm)
        {
            String rtnText = "True-Success";

            //using (var httpClient = new HttpClient())
            //{
            //    //var json = JsonConvert.SerializeObject(frm);
            //    //var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            //    //using (var response = await httpClient.PostAsync("https://localhost:44360/Login/LoginAjax/", stringContent, System.Threading.CancellationToken.None))
            //    //{
            //    //    //string apiResponse = await response.Content.ReadAsStringAsync();
            //    //}
            //}

            //return RedirectToAction("Index");
            return Ok(rtnText);
        }

        [System.Web.Http.HttpPost]
        public JsonResult PostMethod(string name)
        {
            PersonModel person = new PersonModel
            {
                Name = name,
                DateTime = DateTime.Now.ToString()
            };
            return Json(person);
        }
        public IActionResult GetList()
        {
            List<tblregistration> lst =  _dbContext.tblregistration.ToList();
            return View(lst);

        }

        public IActionResult LogOut()
        {

            return RedirectToAction("Login", "Index");
        }



        private async Task<bool> calculatelevelincome(string mid, decimal calamt, string regmid)
        {

            var stopGenerateIncome = _dbContext.tblsetting.FirstOrDefault();
            if (stopGenerateIncome.status == 0)
            {
                return false;
            }
            DataTable dt, dt1, dt2, dt3 = new DataTable();
            decimal pamt = 0;
            levelamt = 0;
            int direcrrequired = 1;
            Query = "Select sid,packageamt from tblRegistration where Mid='" + mid + "'";
            dt1 = dal.GetTable(Query, ref Message);
            if (dt1.Rows.Count > 0)
            {

                decimal amt = calamt;
                level += 1;
                switch (level)
                {
                    case 1:
                        levelamt = 0;
                        break;
                    //levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.10);
                    //direcrrequired = 1;
                    //break;
                    case 2:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.05);
                        direcrrequired = 2;
                        break;
                    case 3:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.05);
                        direcrrequired = 3;
                        break;
                    case 4:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.05);
                        direcrrequired = 4;
                        break;
                    case 5:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.05);
                        direcrrequired = 5;
                        break;
                    case 6:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.01);
                        direcrrequired = 6;
                        break;
                    case 7:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.01);
                        direcrrequired = 7;
                        break;
                    case 8:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.01);
                        direcrrequired = 8;
                        break;
                    case 9:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.01);
                        direcrrequired = 9;
                        break;
                    case 10:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.01);
                        direcrrequired = 10;
                        break;
                    case 11:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                    case 12:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                    case 13:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                    case 14:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                    case 15:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                    case 16:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                    case 17:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                    case 18:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                    case 19:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                    case 20:
                        levelamt = Convert.ToDecimal(calamt) * Convert.ToDecimal(0.005);
                        direcrrequired = 10;
                        break;
                }
                Query = "select count(*) from tbltranshistory where ttype=" + (int)TrHistoryEnum.LevelIncome + " and format(edate,'yyyy-MM-dd')=FORMAT((select growthdate from tbllastpay),'yyyy-MM-dd') and givemid='" + dt1.Rows[0][0].ToString().ToUpper() + "'";
                int incomecredited = Convert.ToInt32(dal.ExecuteScaler(Query, ref Message));

                // var incomecredited = _dbContext.tbltranshistories.Where(x => x.givemid.ToUpper() == mfacc.mid.ToUpper() && x.ttype == (int)TrHistoryEnum.RechargeIncome && Convert.ToDateTime(x.edate).ToString("yyyy-MM-dd") == Convert.ToDateTime(userInfo.Get_date())).FirstOrDefault();
                if (levelamt > 0 && incomecredited == 0)
                {
                    Query = "Select Count(*) from tblRegistration where activate=1 and upper(Mid)='" + dt1.Rows[0][0].ToString().ToUpper() + "'";
                    int actcnt = Convert.ToInt32(dal.ExecuteScaler(Query, ref Message));

                    Query = "Select Count(*) from tblRegistration where activate=1 and upper(sid)='" + dt1.Rows[0][0].ToString().ToUpper() + "'";
                    int sidactcnt = Convert.ToInt32(dal.ExecuteScaler(Query, ref Message));

                    var mfprice = _dbContext.tblsetting.FirstOrDefault().MF_PRICE;
                    var mfacc = _dbContext.tblregistration.Where(x => x.activate == true && x.packageamt >= 50 && x.mid.ToUpper() == dt1.Rows[0][0].ToString().ToUpper()).FirstOrDefault();
                    if (mfacc != null)
                    {
                        decimal totalminingamount = Convert.ToDecimal(mfacc.miningamt);
                        if (actcnt > 0 && sidactcnt >= direcrrequired && totalminingamount >=50)
                        {
                            Query = "Update tblincome set level=CONVERT(float, bidding)+" + (levelamt) + ",withdraw=CONVERT(float, withdraw)+" + (levelamt) + " where Mid='" + dt1.Rows[0][0] + "'";

                            dal.ExecuteNonQuery(Query, ref Message);
                            int tid = 0;
                            tid = _dbContext.tbltranshistory.Max(x => x.tid);
                            tid = tid + 1;

                            Query = "Insert into tblTransHistory (edate,givemid,takemid,tamount,approvedstatus,status,ttype,bid,type,imgname,deduction,final_amount) values (" + "CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') AS DATETIME),'" + dt1.Rows[0][0] + "','" + (regmid.ToUpper()) + "'," + Convert.ToDecimal(levelamt) + ",1,' Farm Buying Level Income :" + Convert.ToDecimal(levelamt) + "'," + (int)TrHistoryEnum.LevelIncome + ",0,1,'dkimg/slip.png',0,0)";
                            dal.ExecuteNonQuery(Query, ref Message);
                        }

                        dt = new DataTable();

                        Query = "Select sid from tblRegistration where Mid='" + dt1.Rows[0][0] + "'";
                        dt = dal.GetTable(Query, ref Message);
                        if (dt.Rows.Count > 0)
                        {
                            calculatelevelincome(Convert.ToString(dt1.Rows[0][0]), calamt, regmid);
                        }
                    }



                }

                if (level >= 20)
                {
                    return false;
                }
            }

            return true;

        }

    }
}


