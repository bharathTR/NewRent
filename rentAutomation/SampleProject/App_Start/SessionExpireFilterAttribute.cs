using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SampleProject.SessionFilter
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = HttpContext.Current;

            if (context.Session != null)
            {
                //if(context.Session.IsNewSession)
                //{
                    string sessionCookie = context.Request.Headers["Cookie"];
                    if((sessionCookie!=null)&&(sessionCookie.IndexOf("ASP.NET_SessionId")>=0))
                    {
                        if(!string.IsNullOrEmpty(context.Request.RawUrl))
                        {
                            var ctx = filterContext.HttpContext;

                            //if Session == null => Login page
                            if (ctx.Session["Username"] == null)
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Account" }));

                            }

                            base.OnActionExecuting(filterContext);

                        }

                    //}
                }
            }

            
        }
    }
}