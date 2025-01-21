using RfqEbr.Data.Contracts;
using RfqEbr.Models.Custom;
using EBR.Web.Helpers;
using EBR.Web.Models;
using EBR.Web.Services;
using RfqEbr.Models.Table;
using EBR.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;
using Context = RfqEbr.Data.Context;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using EISAuthorize.Common.Dto;

namespace EBR.Web.Controllers
{
	public class AccountController : Controller
	{
		public ActionResult Index()
		{
			return View();

			//return RedirectToAction("EbrData", "Home", new { EbrId = 0 });

			//if (Tools.User.IsAuth)
			//{
			//             return View();


			//         }
			//else
			//{
			//	return RedirectToAction("Login", "Account");
			//}
		}

		public ActionResult Login()
		{

			var returnUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0];
			return Redirect(string.Format("http://f5-farm.tlnw.magnecomp.com/EISAuthorize/Login?url={0}&systemId=EBR_RFQ&type=EN", returnUrl));
		}

		public ActionResult Permission()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(string result)
		{
			var jObject = JObject.Parse(result);
			var employee = ((dynamic)jObject).Employee;

			var user = new User
			{
				En = employee.EmployeeNo.Value,
				Name = employee.EngName.Value
			};

			var systems = ((dynamic)jObject).Systems;

			foreach (var system in systems)
			{
				foreach (var role in system.Roles)
				{
					user.RoleNames.Add(role.RoleName.Value);
				}
			}

			if (user.En == null || user.Name == null)
			{
				return RedirectToAction("Login");
			}

			if (user.RoleNames.Count == 0 && false) //Force login
			{
				return View("Permission");
			}
			else
			{
				var userLogin = result != null ? new JavaScriptSerializer().Deserialize<EisUserDto>(result) : null;
				var userSession = new UserModel(userLogin);

				Session.Timeout = 1440;
				TTools.User = userSession;
				return RedirectToAction("Index", "Home");
			}

		}

		public ActionResult LogOff()
		{
			TTools.UserClear();
			Session.Abandon(); //Kill session;
			return RedirectToAction("Login", "Account");
		}
	}
}