using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBR.Web.Helpers;
using EBR.Web.Models;
using RFIDReaderAPI;
using RfqEbr.Data.Contracts;

namespace EBR.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _uow;

        public DashboardController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public ActionResult Dashboard(int EmpNo, string CodeId)
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
                    return View("~/Views/Home/Dashboard/Dashboard.cshtml", EmpUser);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }
        public ActionResult GetEmployee()
        {
            var getEmpDetail = _uow.YMTGUsers.GetAll();
            return new JsonNetResult(getEmpDetail);
        }

        public ActionResult CreateEmployee(int EmpNo, string CodeId)
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
                    return View(EmpUser);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }


        }
    }
}