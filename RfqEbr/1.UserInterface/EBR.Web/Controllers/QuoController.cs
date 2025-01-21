 using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EBR.Web.Helpers;
using EBR.Web.Models;
using EBR.Web.Services;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using OfficeOpenXml.Style;
using RfqEbr.Data.Contracts;
using RfqEbr.Models.Table;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Schema;
using Context = RfqEbr.Data.Context;

namespace EBR.Web.Controllers
{
    public class QuoController : Controller
    {
        private readonly IUnitOfWork _uow;
        public QuoController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public ActionResult Index()
        {
            return RedirectToAction("YMTGroup", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            Services.TTools.UserClear();
            Session.Clear();
            Session.Abandon(); //Kill session;
            return RedirectToAction("Login", "Home");
        }

        public ActionResult GetEmployee(int EmployeeNo)
        {
            var getEmpDetail = _uow.YMTGUsers.GetAll().Where(x => x.Id == EmployeeNo).FirstOrDefault();
            return new JsonNetResult(getEmpDetail);
        }

        //YMTG CheckUser
        public ActionResult checkUser(string Emp_EmpNo, string Emp_Pass)
        {
            var CheckRole = _uow.YMTGUsers.GetAll().Where(t => t.YPTUser == Emp_EmpNo && t.YPTPass == Emp_Pass).FirstOrDefault();

            if (CheckRole == null)
            {
                return new JsonNetResult(CheckRole);
            }
            else
            {
                Session["EmpNo"] = CheckRole.Id;
                return new JsonNetResult(CheckRole);
            }
        }



        public ActionResult GetEmpUser(string EmployeeNo)
        {
            var getEmpDetail = _uow.EmployeeLogins.GetAll().Where(x => x.EmpNo == EmployeeNo).FirstOrDefault();
            return new JsonNetResult(getEmpDetail);
        }



        
    }
}