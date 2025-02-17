using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBR.Web.Helpers;
using EBR.Web.Models;
using RfqEbr.Data.Contracts;
using RfqEbr.Models.IT.StockDeviceIT;

namespace EBR.Web.Controllers
{
    public class OutStockDeviceITController : Controller
    {
        private readonly IUnitOfWork _uow;

        public OutStockDeviceITController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // ReceiveStockIT
        // Return View To ReceiveStockIT.cshtml
        public ActionResult ViewOutStockIT(int EmpNo, string CodeId)
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
                    return View("~/Views/Home/StockDevice_IT/Out/OutStockIT.cshtml", EmpUser);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }

        public ActionResult ViewFormOutStockIT(int EmpNo, string CodeId)
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
                    return View("~/Views/Home/StockDevice_IT/Out/FormOutStockIT.cshtml", EmpUser);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }

        // API to get data with paging
        public ActionResult GetOutStockData()
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

        [HttpPost]
        public ActionResult OutItemData(StockTransactionIT[] transactionITs)
        {
            DateTime currentDate = DateTime.Now;

            // แยกปีและเดือน
            int year = currentDate.Year;
            int month = currentDate.Month;

            // แปลงปีและเดือนเป็นสตริง
            string yearString = year.ToString();
            string monthString = month.ToString("00"); // แสดงเดือนเป็น 2 หลัก (เช่น 01, 02, ..., 12)

            // กำหนดตัวแปรเพื่อเก็บหมายเลขเอกสารใหม่
            string newDocNo = string.Empty;

            // get ข้อมูลทั้งหมดใน Transaction
            var dataList = _uow.StockTransactionITs.GetAll().ToList();
            var locationList = _uow.MasterLocations.GetAll().ToList();
            // เอาข้อมูล DocNo ล่าสุดที่เก็บอยู่ใน TransactionList
            var lastDocNo = dataList
                .Where(t => t.DocNo.StartsWith(yearString + monthString)) // กรองเฉพาะเอกสารที่เป็นเดือนนี้
                .Select(t => t.DocNo)
                .OrderByDescending(doc => doc) // เรียงลำดับตาม DocNo
                .FirstOrDefault(); // เอาเอกสารล่าสุด

            if (!string.IsNullOrEmpty(lastDocNo))
            {
                // ถ้ามีเอกสารในเดือนนี้, เราจะหาหมายเลขเอกสารสูงสุด
                string docNoSuffix = lastDocNo.Substring(6); // เลือกเฉพาะ 4 หลักท้าย
                int lastDocNumber = int.Parse(docNoSuffix); // แปลงเป็นตัวเลข

                // เพิ่มหมายเลขเอกสาร
                newDocNo = yearString + monthString + (lastDocNumber + 1).ToString("0000");
            }
            else
            {
                // ถ้าไม่มีเอกสารในเดือนนี้, ให้เริ่มจากหมายเลข 0001
                newDocNo = yearString + monthString + "0001";
            }

            // แสดงผล DocNo ใหม่
            Console.WriteLine("DocNo ใหม่: " + newDocNo);

            // Create new StockTransactionIT objects for each transaction
            if (transactionITs == null)
            {
                return HttpNotFound("transactionITs not found.");
            }
            else
            {
                foreach (var transaction in transactionITs)
                {
                    var location = locationList.FirstOrDefault(l => l.Location == "IT Room");
                    var locCode = location?.LocationCode ?? "";

                    var newTransaction = new StockTransactionIT
                    {
                        DocNo = newDocNo,
                        Code = transaction.Code,
                        LocationCode = locCode,
                        TrxDate = DateTime.Now.Date,
                        TrxType = transaction.TrxType,
                        TrxName = transaction.TrxName,
                        DeptOut = transaction.DeptOut,
                        DeviceName = transaction.DeviceName,
                        UserName = transaction.UserName,
                        DeviceId = transaction.DeviceId,
                        Qty = transaction.Qty,
                        CreateBy = transaction.CreateBy,
                        CreateDate = DateTime.UtcNow,
                        EditBy = transaction.CreateBy,
                        EditDate = DateTime.UtcNow
                    };

                    // Add the new transaction
                    _uow.StockTransactionITs.Add(newTransaction);

                    var existingDataStockInQtys = _uow.StockDeviceITInQtys.GetAll().Where(p => p.LocationCode == locCode && p.Code == transaction.Code)
                        .FirstOrDefault();

                    if (existingDataStockInQtys != null)
                    {
                        // Update existing entry
                        existingDataStockInQtys.Qty -= transaction.Qty;
                        existingDataStockInQtys.Editby = transaction.CreateBy;
                        existingDataStockInQtys.EditDate = DateTime.UtcNow;
                        _uow.StockDeviceITInQtys.Update(existingDataStockInQtys);
                    }

                    // Update StockIT
                    var existingDataStockIT = _uow.StockDeviceITs.GetAll().Where(p => p.Code == transaction.Code).FirstOrDefault();
                    if (existingDataStockIT != null)
                    {
                        existingDataStockIT.QtyBalance = existingDataStockInQtys.Qty;
                        existingDataStockIT.EditBy = transaction.CreateBy;
                        existingDataStockIT.EditDate = DateTime.UtcNow;
                        _uow.StockDeviceITs.Update(existingDataStockIT);
                    }
                }
                _uow.Commit();
                // Commit all changes at once
            }
            // ส่งค่ากลับไป
            return new JsonNetResult("Success");
        }
    }
}