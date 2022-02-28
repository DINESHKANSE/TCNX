using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TCNX.commonFunction;
using TCNX.Models;
using TCNX.Models.DBModel;

namespace TCNX.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        string Message = string.Empty;

        List<UserLoginModels> loginModel = new List<UserLoginModels>();
        DataTable dt = new DataTable();
        UserLoginModels _loginModel = new UserLoginModels();
        private UserInfo userInfo = Common.CurrentUserInfo;
        private const string key = "Danny81!@@)(*87KANSE";
        private readonly ApplicationDBContext _dbContext;
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
            if (userInfo == null)
            {
                //return RedirectToAction("Index\?refid=TCNX", "Login");
                return RedirectToAction("Index", "Login", new { refid = "TCNX" });
            }
            //userInfo.username = "Dinesh";
            UserDashboardDetails userDashboardDetails = new UserDashboardDetails();
            tblregistration tblregistration = _dbContext.tblregistration.Where(x => x.mid.ToLower() == userInfo.username.ToLower()).FirstOrDefault();
            userDashboardDetails.WalletAddress = tblregistration.tron_add;
            var mydirect = _dbContext.tblregistration.Where(x => x.sid.ToLower() == userInfo.username.ToLower()).Count();
            var mfprice = _dbContext.tblsetting.FirstOrDefault();
            var userincome = _dbContext.tblincome.Where(x => x.mid.ToLower() == userInfo.username.ToLower()).FirstOrDefault();
            //var mfstock = _dbContext.tblregistration.Where(x => x.mid == userInfo.username).FirstOrDefault().mfqty;
            userDashboardDetails.CoinValue = mfprice.MF_PRICE != null ? Convert.ToDecimal(mfprice.MF_PRICE) : 0 ;
            userDashboardDetails.MyDirect = mydirect;
            userDashboardDetails.Levelincome = Convert.ToDecimal(userincome.level) - Convert.ToDecimal(userincome.levelwd);
            userDashboardDetails.GrowthIncome = Convert.ToDecimal(userincome.growth) - Convert.ToDecimal(userincome.growthwd);
            userDashboardDetails.BiddingIncome = Convert.ToDecimal(userincome.bidding) - Convert.ToDecimal(userincome.biddingwd);
            userDashboardDetails.TotalIncome = Convert.ToDecimal(userincome.withdraw) - Convert.ToDecimal(userincome.withdrawwd);
            userDashboardDetails.SponsorIncome = Convert.ToDecimal(userincome.sponcer) - Convert.ToDecimal(userincome.sponcerwd);
            userDashboardDetails.UserID = userInfo.username.ToUpper();
            return View(userDashboardDetails);
        }

        //public IActionResult LogOff()
        //{
        //    var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Index","Login");
        //}

        public IActionResult LogOff()
        {
            string spon_code = "TCNX";
            try
            {
                spon_code =userInfo.spon_code;
                userInfo = null;
                Common.CurrentUserInfo = userInfo;
                Common.CookieUserID = "";                
                var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Login", new { refid = spon_code });
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Login", new { refid = spon_code });
            }
        }
    }
}
