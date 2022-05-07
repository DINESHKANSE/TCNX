using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TCNX.commonFunction;
using TCNX.Models;
using TCNX.Models.DBModel;
using CurrentContext = System.Web.HttpContext;

namespace TCNX.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        string Message = string.Empty;

        List<UserLoginModels> loginModel = new List<UserLoginModels>();
        DataTable dt = new DataTable();
        SqlDblayer dal = new SqlDblayer();
        UserLoginModels _loginModel = new UserLoginModels();
        private UserInfo userInfo = Common.CurrentUserInfo;
        private const string key = "Danny81!@@)(*87KANSE";
        private readonly ApplicationDBContext _dbContext;
        private string Query = "";
        public static string refid = "TCNX";
        public UserController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public IActionResult Index()
        {
            string rtnText = "";


            //UserLoginModels _Logmodel = new UserLoginModels();
            //tblregistration tblregistration1 = _dbContext.tblregistration.Where(x => x.tron_add == "TN4P1vaEyripiaVbFPc2fN1tTQoQDLmJyM").FirstOrDefault();
            //if (tblregistration1 != null)
            //{
            //    if (tblregistration1.activate == true)
            //    {
            //        _Logmodel.username = tblregistration1.mid;
            //        _Logmodel.password = tblregistration1.passwordHash;
            //    }
            //    else
            //    {
            //        rtnText = "False-Invalid username or password.";
            //        return Json(rtnText, System.Web.Mvc.JsonRequestBehavior.AllowGet);

            //    }
            //}
            //UserInfo userInfo = new UserInfo(_Logmodel.username, _Logmodel.password);

            try
            {
                refid = CurrentContext.Current.Session.GetString("refid");
            }
            catch
            {

            }
            if (userInfo == null)
            {
                //return RedirectToAction("Index\?refid=TCNX", "Login");
                return RedirectToAction("Index", "Login", new { refid = refid });
            }
            //userInfo.username = "Dinesh";
            UserDashboardDetails userDashboardDetails = new UserDashboardDetails();
            tblregistration tblregistration = _dbContext.tblregistration.Where(x => x.mid.ToLower() == userInfo.username.ToLower()).FirstOrDefault();
            userDashboardDetails.WalletAddress = tblregistration.tron_add;
            var mydirect = _dbContext.tblregistration.Where(x => x.sid.ToLower() == userInfo.username.ToLower()).Count();
            var mfprice = _dbContext.tblsetting.FirstOrDefault();
            var userincome = _dbContext.tblincome.Where(x => x.mid.ToLower() == userInfo.username.ToLower()).FirstOrDefault();
            //var mfstock = _dbContext.tblregistration.Where(x => x.mid == userInfo.username).FirstOrDefault().mfqty;
            userDashboardDetails.MyPackage = tblregistration.packageamt != null ? Convert.ToDecimal(tblregistration.packageamt) : 0 ;
            userDashboardDetails.CoinValue = mfprice.MF_PRICE != null ? Convert.ToDecimal(mfprice.MF_PRICE) : 0 ;
            userDashboardDetails.MyDirect = mydirect;
            userDashboardDetails.Levelincome = Convert.ToDecimal(userincome.level) - Convert.ToDecimal(userincome.levelwd);
            userDashboardDetails.GrowthIncome = Convert.ToDecimal(userincome.growth) - Convert.ToDecimal(userincome.growthwd);
            userDashboardDetails.BiddingIncome = Convert.ToDecimal(userincome.bidding) - Convert.ToDecimal(userincome.biddingwd);
            userDashboardDetails.TotalIncome = Convert.ToDecimal(userincome.withdraw) - Convert.ToDecimal(userincome.withdrawwd);
            userDashboardDetails.SponsorIncome = Convert.ToDecimal(userincome.sponcer) - Convert.ToDecimal(userincome.sponcerwd);
            userDashboardDetails.UserID = userInfo.username.ToUpper();
            Query = "select sum(tamount) from tbltranshistory where ttype=" + (int)TrHistoryEnum.DPPurchase + " and givemid ='" + (userInfo.username.ToUpper()) + "'";
            try
            {
                userDashboardDetails.MyInvestment = Convert.ToDecimal(dal.ExecuteScaler(Query, ref Message));
            }
            catch(Exception ex)
            {

            }
            



            try
            {
                Query = "WITH recursiveBOM  (MID,PID,Mname,packageamt,Position,EDATE,sid,activate,actdate) AS" +
                                    "(SELECT parent.MID,Parent.PID,Parent.Mname,Parent.packageamt,parent.Position,Parent.EDATE,Parent.sid ,Parent.activate,Parent.actdate FROM TBLREGISTRATION parent " +
                                    "WHERE parent.MID='" + (userInfo.username.ToUpper()) + "'" +
                                             " UNION ALL " +
                                    "SELECT child.Mid,child.Pid,child.MNAME,child.packageamt,child.Position,child.EDATE,child.sid,child.activate,child.actdate FROM recursiveBOM parent, TBLREGISTRATION child" +
                                    " WHERE child.sid = parent.Mid ) " +
                                    "SELECT ROW_NUMBER()  OVER (ORDER BY  edate) As Sr_No,MID User_ID,Mname Name,SID Sponsor_Name, packageamt Amount,actdate,EDATE Joining_Date,actdate,case activate when '1' then 'Active' when '-1' then 'Blocked' else 'Inactive' end as Status FROM recursiveBOM where mid != '" + (userInfo.username.ToUpper()) + "' option (maxrecursion 0);";

                DataTable dt = dal.GetTable(Query, ref Message);
                userDashboardDetails.MyTeam = dt.Rows.Count;
            }
            catch(Exception ex)
            {

            }
            return View(userDashboardDetails);
        }

        //public IActionResult LogOff()
        //{
        //    var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Index","Login");
        //}
        public IActionResult Refferal()
        {
            if (userInfo == null)
            {
                //return RedirectToAction("Index\?refid=TCNX", "Login");
                return RedirectToAction("Index", "Login", new { refid = refid });
            }
            if (userInfo != null)
            {
                dal.ClearParameters();
                dal.AddParameter("@USERNAME", userInfo.username, "IN");
                dal.AddParameter("@MODE", "DIRECTREFERRALS", "IN");
                DataTable dt = dal.GetTable("SP_REPORTS", ref Message);
                ViewData["DIRECTREFERRALS"] = dt;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
           
        }

        public IActionResult Wallet()
        {
            if (userInfo == null)
            {
                //return RedirectToAction("Index\?refid=TCNX", "Login");
                return RedirectToAction("Index", "Login", new { refid = refid });
            }
            UserDashboardDetails userDashboardDetails = new UserDashboardDetails();
            tblregistration tblregistration = _dbContext.tblregistration.Where(x => x.mid.ToLower() == userInfo.username.ToLower()).FirstOrDefault();
            userDashboardDetails.WalletAddress = tblregistration.tron_add;
            var mydirect = _dbContext.tblregistration.Where(x => x.sid.ToLower() == userInfo.username.ToLower()).Count();
            var mfprice = _dbContext.tblsetting.FirstOrDefault();
            var userincome = _dbContext.tblincome.Where(x => x.mid.ToLower() == userInfo.username.ToLower()).FirstOrDefault();
            //var mfstock = _dbContext.tblregistration.Where(x => x.mid == userInfo.username).FirstOrDefault().mfqty;
            userDashboardDetails.MyPackage = tblregistration.packageamt != null ? Convert.ToDecimal(tblregistration.packageamt) : 0;
            userDashboardDetails.CoinValue = mfprice.MF_PRICE != null ? Convert.ToDecimal(mfprice.MF_PRICE) : 0;
            userDashboardDetails.MyDirect = mydirect;
            userDashboardDetails.Levelincome = Convert.ToDecimal(userincome.level) - Convert.ToDecimal(userincome.levelwd);
            userDashboardDetails.GrowthIncome = Convert.ToDecimal(userincome.growth) - Convert.ToDecimal(userincome.growthwd);
            userDashboardDetails.BiddingIncome = Convert.ToDecimal(userincome.bidding) - Convert.ToDecimal(userincome.biddingwd);
            userDashboardDetails.TotalIncome = Convert.ToDecimal(userincome.withdraw) - Convert.ToDecimal(userincome.withdrawwd);
            userDashboardDetails.SponsorIncome = Convert.ToDecimal(userincome.sponcer) - Convert.ToDecimal(userincome.sponcerwd);
            userDashboardDetails.RechargeIncome = Convert.ToDecimal(userincome.recharge) - Convert.ToDecimal(userincome.recharge);
            userDashboardDetails.UserID = userInfo.username.ToUpper();
            return View(userDashboardDetails);
        }


        public IActionResult LevelTeam()
        {

            if (userInfo != null)
            {
                Query = "WITH recursiveBOM  (MID,PID,Mname,packageamt,Position,EDATE,sid,activate,actdate) AS" +
                    "(SELECT parent.MID,Parent.PID,Parent.Mname,Parent.packageamt,parent.Position,Parent.EDATE,Parent.sid ,Parent.activate,Parent.actdate FROM TBLREGISTRATION parent " +
                    "WHERE parent.MID='" + (userInfo.username.ToUpper()) + "'" +
                             " UNION ALL " +
                    "SELECT child.Mid,child.Pid,child.MNAME,child.packageamt,child.Position,child.EDATE,child.sid,child.activate,child.actdate FROM recursiveBOM parent, TBLREGISTRATION child" +
                    " WHERE child.sid = parent.Mid ) " +
                    "SELECT ROW_NUMBER()  OVER (ORDER BY  edate) As Sr_No,MID User_ID,Mname Name,SID Sponsor_Name, packageamt Amount,actdate,EDATE Joining_Date,actdate,case activate when '1' then 'Active' when '-1' then 'Blocked' else 'Inactive' end as Status FROM recursiveBOM where mid != '" + (userInfo.username.ToUpper()) + "' option (maxrecursion 0);";

                DataTable dt = dal.GetTable(Query, ref Message);
                ViewData["LevelTeam"] = dt;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login", new { refid = refid });
            }
        }

        public IActionResult Report()
        {

            if (userInfo != null)
            {

                List<tbltranshistory> tbltranshistory = _dbContext.tbltranshistory.Where(x => x.givemid.ToUpper() == userInfo.username).OrderBy(x => x.edate).ToList();
                return View(tbltranshistory);
            }
            else
            {
                return RedirectToAction("Index", "Login", new { refid = refid });
            }
        }

        public IActionResult LogOff()
        {
            string spon_code = "TCNX";
            try
            {
                if (userInfo != null)
                {


                    spon_code = userInfo.spon_code;
                    userInfo = null;
                    Common.CurrentUserInfo = userInfo;
                    Common.CookieUserID = "";
                    var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Login", new { refid = spon_code });
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { refid = refid });
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Login", new { refid = refid });
            }
        }


        [HttpPost]
        public ActionResult WalletBalance(IFormCollection frm)
        {
            if (userInfo == null)
            {
                return RedirectToAction("Index", "Login", new { refid = refid });
            }

            Decimal txtamt = Convert.ToDecimal(frm["txtwidraamt"]);
            if (txtamt < 50)
            {
                TempData["Msg"] = "0";
                TempData["ErrMsg"] = "Please enter valid amount.";
                return RedirectToAction("Wallet", "User");
            }
            tblincome varincome = _dbContext.tblincome.Where(x => x.mid.ToUpper() == userInfo.username.ToUpper()).FirstOrDefault();
            Decimal totalincome = Convert.ToDecimal(varincome.withdraw) - Convert.ToDecimal(varincome.withdrawwd);
            if (txtamt > totalincome)
            {
                TempData["Msg"] = "0";
                TempData["ErrMsg"] = "Insufficient balance.Please enter valid amount.";
                return RedirectToAction("Wallet", "User");
            }

            var regcnt = _dbContext.tblregistration.Where(x => x.mid.ToUpper() == userInfo.username.ToUpper()).FirstOrDefault();
            if (regcnt.activate == false)
            {
                TempData["Msg"] = "0";
                TempData["ErrMsg"] = "Please activate id to get withdrawal.";
                return RedirectToAction("Wallet", "User");
            }
            varincome.withdrawwd = varincome.withdrawwd + txtamt;
            //Query = "update tblincome set Withdrawwd=Withdrawwd+" & Val(widrawal) & " where mid='" & Session("MID") & "'"
            //dal.ExecuteQuery(Query)

            //Query = "Insert into tblTransHistory values (" & tid & ", CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') AS DATETIME) ,'" & UCase(Session("MID")) & "','" & UCase(Session("MID")) & "'," & Val(widrawal) & ",0,'Main Wallet Withdrawal amt : $" & Val(widrawal / 73) & " (₹" & Val(widrawal) & ") ',27,0,1,'dkimg/slip.png'," & Val(Val(widrawal) * 0.1) & "," & Val(Val(widrawal) - Val(Val(widrawal) * 0.1)) & ")"
            // Conn.ExecuteQuery(Query)

            tbltranshistory transhistory = new tbltranshistory();
            
            transhistory.orderid = "WD" + @DateTime.Now.ToBinary().ToString().Replace("-", "");
            //transhistory.order_description = NPDPResponse.order_description;
            //transhistory.invoice_id = NPDPResponse.id;
            transhistory.givemid = userInfo.username;
            transhistory.takemid = userInfo.username;
            transhistory.tamount = txtamt;
            transhistory.status = "pending";
            transhistory.currency = "TCNX";
            transhistory.approvedstatus = 0;
            transhistory.ttype = (int)TrHistoryEnum.Withdrawal;
            transhistory.imgname = "~/dkimg/slip.png";
            transhistory.approved_date = Convert.ToDateTime(userInfo.Get_date());
            transhistory.edate = Convert.ToDateTime(userInfo.Get_date());
            _dbContext.tbltranshistory.Add(transhistory);

            _dbContext.SaveChanges();

            UserDashboardDetails userDashboardDetails = new UserDashboardDetails();
            tblregistration tblregistration = _dbContext.tblregistration.Where(x => x.mid.ToLower() == userInfo.username.ToLower()).FirstOrDefault();
            userDashboardDetails.WalletAddress = tblregistration.tron_add;
            var mydirect = _dbContext.tblregistration.Where(x => x.sid.ToLower() == userInfo.username.ToLower()).Count();
            var mfprice = _dbContext.tblsetting.FirstOrDefault();
            var userincome = _dbContext.tblincome.Where(x => x.mid.ToLower() == userInfo.username.ToLower()).FirstOrDefault();
            //var mfstock = _dbContext.tblregistration.Where(x => x.mid == userInfo.username).FirstOrDefault().mfqty;
            userDashboardDetails.MyPackage = tblregistration.packageamt != null ? Convert.ToDecimal(tblregistration.packageamt) : 0;
            userDashboardDetails.CoinValue = mfprice.MF_PRICE != null ? Convert.ToDecimal(mfprice.MF_PRICE) : 0;
            userDashboardDetails.MyDirect = mydirect;
            userDashboardDetails.Levelincome = Convert.ToDecimal(userincome.level) - Convert.ToDecimal(userincome.levelwd);
            userDashboardDetails.GrowthIncome = Convert.ToDecimal(userincome.growth) - Convert.ToDecimal(userincome.growthwd);
            userDashboardDetails.BiddingIncome = Convert.ToDecimal(userincome.bidding) - Convert.ToDecimal(userincome.biddingwd);
            userDashboardDetails.TotalIncome = Convert.ToDecimal(userincome.withdraw) - Convert.ToDecimal(userincome.withdrawwd);
            userDashboardDetails.SponsorIncome = Convert.ToDecimal(userincome.sponcer) - Convert.ToDecimal(userincome.sponcerwd);
            userDashboardDetails.RechargeIncome = Convert.ToDecimal(userincome.recharge) - Convert.ToDecimal(userincome.recharge);
            userDashboardDetails.UserID = userInfo.username.ToUpper();
            return RedirectToAction("Wallet", "User");
        }

        public IActionResult TopupPackage()
        {
            if (userInfo == null)
            {
                return RedirectToAction("Index", "Login", new { refid = refid });
            }
            //TempData["Msg"] = "1";
            //TempData["ErrMsg"] = "Please enter valid amount.";
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult TopupPackage(IFormCollection frm)
        {

            string rtnText = "";
            if (userInfo == null)
            {
                return RedirectToAction("Index", "Login", new { refid = refid });
            }

            Decimal txtamt = Convert.ToDecimal(frm["txtamt"]);
            string transid = frm["transid"];
            string deposittype = frm["cmbdeposittype"];
            if(deposittype=="1")
            {
                deposittype = "TCNX";
            }
            else
            {
                deposittype = "TRX";
            }
            var transidcnt = _dbContext.tbltranshistory.Where(x => transid == x.txthash).FirstOrDefault();
            if (transidcnt != null)
            {
                if (transidcnt.txthash.Count() > 0)
                {
                    rtnText = "False-Duplicate transaction.This Transaction ID already available in the system.";
                    return Ok(rtnText);
                }

            }

            tbltranshistory transhistory = new tbltranshistory();

            transhistory.orderid = "Invest-T" + @DateTime.Now.ToBinary().ToString().Replace("-", "");
            //transhistory.order_description = NPDPResponse.order_description;
            //transhistory.invoice_id = NPDPResponse.id;
            transhistory.givemid = userInfo.username;
            transhistory.takemid = userInfo.username;
            transhistory.tamount = txtamt;
            transhistory.status = "Investment " + txtamt;
            transhistory.currency = "TCNX";
            transhistory.approvedstatus = 1;
            transhistory.ttype = (int)TrHistoryEnum.DPPurchase;
            transhistory.imgname = "~/dkimg/slip.png";
            transhistory.approved_date = Convert.ToDateTime(userInfo.Get_date());
            transhistory.edate = Convert.ToDateTime(userInfo.Get_date());
            transhistory.payment_id = transid;
            _dbContext.tbltranshistory.Add(transhistory);

            _dbContext.SaveChanges();
            rtnText = "True-Investment Done Successfully.";
            return Ok(rtnText);
            //return RedirectToAction("TopupPackage", "User");
        }


        [HttpPost]
        public void SetTempData(string tempDataValue,string dataflag)
        {
            // Set your TempData key to the value passed in
            TempData["Msg"] = dataflag;
            TempData["ErrMsg"] = tempDataValue;
        }
    }
    }
