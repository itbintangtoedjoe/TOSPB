using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Controllers;

namespace WebApplication1.Scripts.CheckSession
{
    public class SessionValidation : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["Username"] == null || HttpContext.Current.Session["Username"].ToString() == "")
            {
                //filterContext.HttpContext.Response.StatusCode = 403;
                //filterContext.Result = new JsonResult { Data = "LogOut" };
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                }
                //if (filterContext.HttpContext.Request.IsAjaxRequest())
                //{
                //    var model = new
                //    {
                //        AjaxFailed = "true",
                //        RedirectURL = "localhost/home/index"
                //    };
                //    //filterContext.Result = new JsonResult { Data = "LogOut" };
                //}
                else
                {
                    //filterContext.Result = new RedirectResult("~/Login/Session_Error");
                    //filterContext.HttpContext.Response.StatusCode = 403;
                    if (filterContext.Controller is LoginController == false)
                    {
                        filterContext.Result = new RedirectToRouteResult(
                          new RouteValueDictionary {
                                        { "Controller", "Login" },
                                        { "Action", "Session_Error" }
                                      });
                    }
                }
                //filterContext.Result = new RedirectToRouteResult(
                //   new RouteValueDictionary {
                //                { "Controller", "Login" },
                //                { "Action", "Session_Error" }
                //               });
                base.OnActionExecuting(filterContext);
            }
        }
    }
}