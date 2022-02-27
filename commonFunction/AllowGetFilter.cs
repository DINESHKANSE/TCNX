using System;
using System.Web.Mvc;

namespace TCNX.commonFunction
{
    //public class AllowJsonGetAttribute : ActionFilterAttribute
    //{
    //    public override void OnResultExecuting(ResultExecutingContext filterContext)
    //    {
    //        var jsonResult = filterContext.Result as JsonResult;

    //        if (jsonResult == null)
    //            throw new ArgumentException("Action does not return a JsonResult,attribute AllowJsonGet is not allowed");
    
    //        jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

    //        base.OnResultExecuting(filterContext);
    //    }
    //}

  
public sealed class AllowJsonGetAttribute : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            var jsonResult = context.Result as JsonResult;
            if (jsonResult == null) return;

            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var jsonResult = filterContext.Result as JsonResult;
            if (jsonResult == null) return;

            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            base.OnResultExecuting(filterContext);
        }
    }
}
