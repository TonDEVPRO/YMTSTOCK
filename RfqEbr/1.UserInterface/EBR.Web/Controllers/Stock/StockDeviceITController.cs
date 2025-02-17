using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBR.Web.Helpers;
using EBR.Web.Models;
using RfqEbr.Data.Contracts;

namespace EBR.Web.Controllers
{
    public class StockDeviceITController : Controller
    {
        private readonly IUnitOfWork _uow;

        public StockDeviceITController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Return View To StockDeviceIT.cshtml
        public ActionResult ViewStockDeviceIT(int EmpNo, string CodeId)
        {
            int empSession = Convert.ToInt32(Session["EmpNo"]);

            if (empSession != EmpNo)
            {
                return RedirectToAction("Login", "Home");
            }

            var CheckRole = _uow.YMTGUsers.GetAll().Where(t => t.Id == EmpNo).FirstOrDefault();

            if (EmpNo == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (CheckRole.Status == "0" || CheckRole.Status == "2" || CheckRole.Status == "33")
                {
                    var EmpUser = new UserQuo
                    {
                        EmpNo = EmpNo,
                        CodeId = CodeId
                    };
                    return View("~/Views/Home/StockDevice_IT/StockDeviceIT.cshtml", EmpUser);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }

        // ReceiveStockIT
        // Return View To ReceiveStockIT.cshtml
        public ActionResult ViewReceiveStockIT(int EmpNo, string CodeId)
        {
            int empSession = Convert.ToInt32(Session["EmpNo"]);

            if (empSession != EmpNo)
            {
                return RedirectToAction("Login", "Home");
            }

            var CheckRole = _uow.YMTGUsers.GetAll().Where(t => t.Id == EmpNo).FirstOrDefault();

            if (EmpNo == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (CheckRole.Status == "0" || CheckRole.Status == "2" || CheckRole.Status == "33")
                {
                    var EmpUser = new UserQuo
                    {
                        EmpNo = EmpNo,
                        CodeId = CodeId
                    };
                    return View("~/Views/Home/StockDevice_IT/Receive/ReceiveStockIT.cshtml", EmpUser);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }

        // API to get data with paging
        public ActionResult GetStockData()
        {
            var totalItems = _uow.StockDeviceITs.GetAll().Count();
            var GETDATA = _uow.StockDeviceITs.GetAll()
                            .OrderBy(u => u.Code)
                            .ToList();

            var result = new
            {
                items = GETDATA,
                totalPages = (int)Math.Ceiling((double)totalItems / 10)
            };

            return new JsonNetResult(result);
        }
    }
}