using EBR.Web.Helpers;
using EBR.Web.Services;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace EBR.Web.Filters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class SessionExpireFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!TTools.User.IsAuth)
			{
				if (filterContext.HttpContext.Request.IsAjaxRequest())
				{
					filterContext.HttpContext.Response.StatusCode = 403;
					filterContext.Result = new JsonNetResult("Session timeout, Please login again.");
				}
				else
				{
					filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                        { "Controller", "Home" },
                        { "Action", "Index" }
                    });
				}
			}

			base.OnActionExecuting(filterContext);
		}
	}
}