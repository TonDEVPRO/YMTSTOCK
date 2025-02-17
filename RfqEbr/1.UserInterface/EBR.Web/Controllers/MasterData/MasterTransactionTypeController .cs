using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using EBR.Web.Helpers;
using EBR.Web.Models;
using RfqEbr.Data.Contracts;

namespace EBR.Web.Controllers
{
    public class MasterTransactionTypeController : Controller
    {
        private readonly IUnitOfWork _uow;

        public MasterTransactionTypeController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        // Api Get Supplier Data
        public ActionResult GetTransactionTypeData()
        {
            var GETDATA = _uow.MasterTransactionTypes.GetAll().ToList(); ;

            return new JsonNetResult(GETDATA);
        }
    }
}