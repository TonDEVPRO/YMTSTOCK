using System.Web.Mvc;
using EBR.Web.Filters;

namespace EBR.Web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new AjaxHandleErrorAttribute());
		}
	}
}
