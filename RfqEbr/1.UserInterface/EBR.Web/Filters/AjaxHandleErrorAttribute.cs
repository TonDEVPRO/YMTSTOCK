using System.Web;
using EBR.Web.Helpers;

using System.Web.Mvc;

namespace EBR.Web.Filters
{
	public class AjaxHandleErrorAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext context)
		
		{
			if (context.ExceptionHandled || !context.HttpContext.IsCustomErrorEnabled)
			{
				return;
			}

			// if the request is AJAX return JSON else view.
			if (context.HttpContext.Request.IsAjaxRequest()) //IsAjax(filterContext))
			{
				context.Result = new JsonNetResult(context.Exception.Message);
				context.ExceptionHandled = true;
				context.HttpContext.Response.Clear();
				context.HttpContext.Response.StatusCode = 500;
				context.HttpContext.Response.TrySkipIisCustomErrors = true;
			}
			else
			{
				base.OnException(context);
			}

			//if want to get different of the request
			//var currentController = (string)filterContext.RouteData.Values["controller"];
			//var currentActionName = (string)filterContext.RouteData.Values["action"];
		}


	}

	static class test
	{
		public static bool IsAjaxRequest(this HttpRequestBase request)
		{
			if (request["X-Requested-With"] == "XMLHttpRequest")
				return true;
			if (request.Headers != null)
				return request.Headers["X-Requested-With"] == "XMLHttpRequest";
			return false;
		}
	}
}