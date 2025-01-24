 using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EBR.Web.Helpers;
using EBR.Web.Models;
using EBR.Web.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using OfficeOpenXml.Style;
using Org.BouncyCastle.Asn1.X509;
using RfqEbr.Data.Contracts;
using RfqEbr.Models;
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
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using Tools.Http;
using Context = RfqEbr.Data.Context;
using Font = iTextSharp.text.Font;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using Rectangle = iTextSharp.text.Rectangle;
using RFIDReaderAPI;
using RFIDReaderAPI.Models;
using System.Net.Http.Headers;
using QRCoder;


namespace EBR.Web.Controllers
{
    public class HomeController : Controller , RFIDReaderAPI.Interface.IAsynchronousMessage
    {
        private readonly IUnitOfWork _uow;

        string IPConfig = "192.168.3.248:9090";

        private List<string> displayedEpcs = new List<string>();
        public HomeController(IUnitOfWork uow)
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

        public ActionResult YMTGroup()
        {
            return View();
        }

        public ActionResult GetEmpUser(string EmployeeNo)
        {
            var getEmpDetail = _uow.EmployeeLogins.GetAll().Where(x => x.EmpNo == EmployeeNo).FirstOrDefault();
            return new JsonNetResult(getEmpDetail);
        }

        public ActionResult YMTHome(int EmpNo)
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
                    return View(EmpNo);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }


        public ActionResult YMTNdsStock(int EmpNo)
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
                    return View(EmpNo);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }
        
      public ActionResult POSSystem(int EmpNo)
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
                    return View(EmpNo);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }


        [HttpGet]
        public JsonResult GetData()
        {
 
                var data = _uow.YMTGNDSShopStocks.GetAll()
                                  .Select(e => new
                                  {
                                      e.Id,
                                      e.Style,
                                      e.Description,
                                      e.Color,
                                      e.Size,
                                      e.Cost,
                                      e.Price,
                                      e.Total
                                  })
                                  .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);

        }


        // ** Start **  MenuList StockNDS //
        public ActionResult getStockStyleNDS()
        {
            var MasterStyle = _uow.YMTGNDSShopStocks.GetAll().Where(p => p.Status == 1).GroupBy(p => p.Style).ToList();
            var MasterStyles = MasterStyle.Select(g => g.Key).ToList();
            return new JsonNetResult(MasterStyles);
        }

        public ActionResult GetDescStyle(string Style)
        {
            var ListDescData = _uow.YMTGNDSShopStocks.GetAll().Where(z => z.Style == Style && z.Status == 1).FirstOrDefault();
            return new JsonNetResult(ListDescData);
        }

        public ActionResult GetColors(YMTGNDSShopStock ListStyleColors)
        {
            var MasterStyle = _uow.YMTGNDSShopStocks.GetAll().Where(p => p.Status == 1 && p.Style == ListStyleColors.Style).GroupBy(p => p.Color).ToList();
            var MasterStyles = MasterStyle.Select(g => g.Key).ToList();
            return new JsonNetResult(MasterStyles);
        }

        public ActionResult GetSizeNDS(YMTGNDSShopStock ListSizes)
        {
            var MasterList = _uow.YMTGNDSShopStocks.GetAll().Where(p => p.Status == 1 && p.Style == ListSizes.Style && p.Color == ListSizes.Color).GroupBy(p => p.Size).ToList();
            var MasterLists = MasterList.Select(g => g.Key).ToList();
            return new JsonNetResult(MasterLists);
        }

        public ActionResult GetAllStock(YMTGNDSShopStock ListAllStock)
        {
            var MasterListAllStock = _uow.YMTGNDSShopStocks.GetAll().Where(p => p.Status == 1 &&
            p.Style == ListAllStock.Style && p.Color == ListAllStock.Color && p.Size == ListAllStock.Size).FirstOrDefault();
            return new JsonNetResult(MasterListAllStock);
        }



        //** END** MenuList StockNDS //

        //** START **  MenuList SaveStock //

        public ActionResult SaveStocks(YMTGNDSShopStock ndsShopStocks)
        {
            var existingData = _uow.YMTGNDSShopStocks.GetAll().FirstOrDefault(x => x.Id == ndsShopStocks.Id);
            if (existingData == null)
            {
                return new JsonNetResult(existingData);
            }
            else
            {
                existingData.Style = ndsShopStocks.Style.Trim();
                existingData.Description = ndsShopStocks.Description;
                existingData.Color = ndsShopStocks.Color;
                existingData.Size = ndsShopStocks.Size;
                existingData.Price = ndsShopStocks.Price;
                existingData.Total = ndsShopStocks.Total;
                existingData.Remark = ndsShopStocks.Remark;

                _uow.YMTGNDSShopStocks.Update(existingData);
                _uow.Commit();
                return new JsonNetResult(existingData);
            }

        }



        public ActionResult SaveStockLogs(YMTGNDSShopStockLog ndsShopStocksLogs)
        {
            ndsShopStocksLogs.Remark = "AddStock";
            ndsShopStocksLogs.CreateDate = DateTime.Now;
            _uow.YMTGNDSShopStockLogs.Add(ndsShopStocksLogs);
            _uow.Commit();
            return new JsonNetResult(ndsShopStocksLogs);



        }

        public ActionResult EditStock(YMTGNDSShopStock ListId)
        {
            var getquo = _uow.YMTGNDSShopStocks.GetAll().Where(z => z.Id == ListId.Id).FirstOrDefault();
            return new JsonNetResult(getquo);
        }


        public ActionResult UpdateStocks(YMTGNDSShopStock ndsShopStocks)
        {
            var existingData = _uow.YMTGNDSShopStocks.GetAll().FirstOrDefault(x => x.Id == ndsShopStocks.Id);
            if (existingData == null)
            {
                //var errorstock = $"Data with ID {ndsShopStocks.Id} not found.";

                var errorstock = "Data not found";
                return new JsonNetResult(errorstock);
            } else
            {
                existingData.Style = ndsShopStocks.Style;
                existingData.Description = ndsShopStocks.Description;
                existingData.Color = ndsShopStocks.Color;
                existingData.Size = ndsShopStocks.Size;
                existingData.Price = ndsShopStocks.Price;
                existingData.Total = ndsShopStocks.Total;

                _uow.YMTGNDSShopStocks.Update(existingData);
                _uow.Commit();

                return new JsonNetResult(existingData);
            }
        }


        public ActionResult UpdateStockLogs(YMTGNDSShopStockLog ndsShopStocksLogs)
        {
            ndsShopStocksLogs.Remark = "UpdateStock";
            ndsShopStocksLogs.CreateDate = DateTime.Now;
            _uow.YMTGNDSShopStockLogs.Add(ndsShopStocksLogs);
            _uow.Commit();
            return new JsonNetResult(ndsShopStocksLogs);
        }


        public ActionResult GetAutoColors()
        {
            var MasterColor = _uow.YMTGNDSShopStocks.GetAll().Where(p => p.Status == 1).GroupBy(p => p.Color).ToList();
            var MasterColors = MasterColor.Select(g => g.Key).ToList();
            return new JsonNetResult(MasterColors);
        }

        public ActionResult GetAutoSizes()
        {
            var MasterSize = _uow.YMTGNDSShopStocks.GetAll().Where(p => p.Status == 1).GroupBy(p => p.Size).ToList();
            var MasterSizes = MasterSize.Select(g => g.Key).ToList();
            return new JsonNetResult(MasterSizes);
        }

        public ActionResult CheckMasterStock(YMTGNDSShopStock MasterStocks)
        {
            var errorstatus = "HaveStock";
            var MasterStock = _uow.YMTGNDSShopStocks.GetAll().Where(z => z.Style == MasterStocks.Style && z.Size == MasterStocks.Size && z.Color == MasterStocks.Color).FirstOrDefault();

            if (MasterStock == null)
            {
                return new JsonNetResult(MasterStocks);
            } else
            {
                return new JsonNetResult(errorstatus);
            }



        }

        public ActionResult SaveMasterStock(YMTGNDSShopStock MasterSaveStocks)
        {
            MasterSaveStocks.Status = 1; 
            MasterSaveStocks.CreateDate = DateTime.Now;
            _uow.YMTGNDSShopStocks.Add(MasterSaveStocks);
            _uow.Commit();

            return new JsonNetResult(MasterSaveStocks);
        }



        public ActionResult NDSHolidayStock(int EmpNo)
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
                    return View(EmpNo);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }
        public JsonResult GetHolidayStock()
        {
            var ListStockHoliday = _uow.YMTGNDSShopHolidays.GetAll().Select(e => new
            {
                e.Id,
                e.OrderNo,
                e.Style,
                e.Description,
                e.Color,
                e.Size,
                e.Cost,
                e.Price,
                e.Total
            }).ToList();
            return Json(new { data = ListStockHoliday });
        }


        [HttpGet]
        public JsonResult GetholidayData()
        {

            var dataHoliday = _uow.YMTGNDSShopHolidays.GetAll()
                              .Select(e => new
                              {
                                  e.Id,
                                  e.OrderNo,
                                  e.Style,
                                  e.Description,
                                  e.Color,
                                  e.Size,
                                  e.Cost,
                                  e.Price,
                                  e.Total
                              })
                              .ToList();
            return Json(new { data = dataHoliday }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetStockHolidays()
        {
            var MasterOrder = _uow.YMTGNDSShopHolidays.GetAll().Where(p => p.Status == 1).GroupBy(p => p.OrderNo).ToList();
            var MasterOrders = MasterOrder.Select(g => g.Key).ToList();
            return new JsonNetResult(MasterOrders);
        }

        public ActionResult GetHolidayStyle(string OrderNos)
        {
            var MasterStyle = _uow.YMTGNDSShopHolidays.GetAll().Where(p => p.Status == 1 && p.OrderNo == OrderNos).GroupBy(p => p.Style).ToList();
            var MasterStyles = MasterStyle.Select(g => g.Key).ToList();
            return new JsonNetResult(MasterStyles);
        }

        public ActionResult GetHolidayDescStyle(string OrderNo, string Style)
        {
            var MasterStock = _uow.YMTGNDSShopHolidays.GetAll().Where(p => p.Status == 1 && p.OrderNo == OrderNo && p.Style == Style).FirstOrDefault();
            return new JsonNetResult(MasterStock);
        }

        public ActionResult GetHolidayColor(string OrderNo, string Style)
        {
            var MasterColor = _uow.YMTGNDSShopHolidays.GetAll().Where(p => p.Status == 1 && p.OrderNo == OrderNo && p.Style == Style).GroupBy(p => p.Color).ToList();
            var MasterColors = MasterColor.Select(g => g.Key).ToList();
            return new JsonNetResult(MasterColors);
        }

        public ActionResult GetHolidaySizes(string OrderNo, string Style, string Color)
        {
            var MasterSize = _uow.YMTGNDSShopHolidays.GetAll().Where(p => p.Status == 1 && p.OrderNo == OrderNo && p.Style == Style && p.Color == Color).GroupBy(p => p.Size).ToList();
            var MasterSizes = MasterSize.Select(g => g.Key).ToList();
            return new JsonNetResult(MasterSizes);
        }


        public ActionResult GetHolidayDetailAlls(string OrderNo, string Style, string Color, string Size)
        {
            var MasterAllData = _uow.YMTGNDSShopHolidays.GetAll().Where(p => p.Status == 1 && p.OrderNo == OrderNo && p.Style == Style && p.Color == Color && p.Size == Size).FirstOrDefault();
            return new JsonNetResult(MasterAllData);
        }






        public ActionResult SaveHolidayStocks(YMTGNDSShopSaveHoliday SaveHoliday, string UserId)
        {
            var Descriptions = "";

            if (SaveHoliday.Description == null || SaveHoliday.Description == "")
            {
                Descriptions = "";
            }
            else
            {
                Descriptions = SaveHoliday.Description;
            }

            var getSaveHoliday = _uow.YMTGNDSShopSaveHolidays.GetAll().Where(z => z.OrderNo == SaveHoliday.OrderNo).FirstOrDefault();

            if(getSaveHoliday == null)
            {
                SaveHoliday.Description = Descriptions;
                SaveHoliday.CreateBy = UserId;
                SaveHoliday.CreateDate = DateTime.Now;
                _uow.YMTGNDSShopSaveHolidays.Add(SaveHoliday);
                _uow.Commit();

                return new JsonNetResult(SaveHoliday);
            }
            else
            {
                getSaveHoliday.Price = SaveHoliday.Price;
                getSaveHoliday.Color = SaveHoliday.Color;
                getSaveHoliday.Cost = SaveHoliday.Cost;
                getSaveHoliday.Total = SaveHoliday.Total;
                getSaveHoliday.Description = Descriptions;
                getSaveHoliday.CreateBy = UserId;
                getSaveHoliday.CreateDate = DateTime.Now;

                _uow.YMTGNDSShopSaveHolidays.Update(getSaveHoliday);
                _uow.Commit();

                return new JsonNetResult(getSaveHoliday);
            }
        }





        public ActionResult YMTNdsSystem(int EmpNo)
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
                    return View(EmpNo);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }




        public ActionResult NdsSystemQuotation(int EmpNo)
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
                    return View(EmpNo);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }



        public ActionResult GetdataQuo()
        {

            var dataquo = _uow.YmtgOrderNdss.GetAll()
            .Where(a => a.QuoCancel == 0)
            .OrderByDescending(a => a.Id) // àÃÕÂ§ÅÓ´Ñº Id ¨Ò¡ÁÒ¡ä»¹éÍÂ
            .ToList();


            return new JsonNetResult(dataquo);
        }



        //public ActionResult NdsSystemEditQuotation(string QuotationNumber)
        //{

        //    var dataquoEditOrder = _uow.YmtgOrderNdss.GetAll().Where(z => z.QuotationNumber == QuotationNumber)
        //        .FirstOrDefault();


        //    return new JsonNetResult(dataquoEditOrder);
        //}

        public ActionResult NdsSystemEditQuotation  (string QuotationNumber, int EmpNo )
        {

            //string QuotationNumber, int EmpNo
            // สร้างข้อมูลตัวอย่าง
            var EmpUser = new UserQuo
            {
                EmpNo = EmpNo,
                QuotationNumber = QuotationNumber
            };

            // ส่ง Model ไปที่ View
            return View(EmpUser);
        }






        [HttpGet]
        public JsonResult GetdataquoNew()
        {

            var data = _uow.YmtgOrderNdss.GetAll()
                              .Select(e => new
                              {
                                  e.Id,
                                  e.QuotationNumber,
                                  e.QuoType,
                         
                                  e.CustomerName,
                                  e.QuoLastname,
                                  e.CreateDate,
                                  e.QuoStatus

                              })
                              .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);

        }



        public ActionResult GetdataQuoForEdit(string QuotationNumber, string EmpNos)
        {

            var dataquoEditOrder =  _uow.YmtgOrderNdss.GetAll().Where(z => z.QuotationNumber == QuotationNumber)
                .FirstOrDefault();


            return new JsonNetResult(dataquoEditOrder);
        }


        public ActionResult GetSku()
        {
            var MasterStylesDataGroup =_uow.MasterStyles.GetAll().GroupBy(p => p.Description).ToList();
            var MasterStylesData = MasterStylesDataGroup.Select(g => g.Key).ToList();

            return new JsonNetResult(MasterStylesData);
        }


        public ActionResult GetColorss()
        {
            var colors = new List<object>();
            colors.Add("Black");
            colors.Add("Green");
            colors.Add("Gray");
            colors.Add("Navy");
            colors.Add("White");
            colors.Add("NO COLOR");

            return new JsonNetResult(colors);
        }


        public ActionResult GetSizes()
        {
            var sizes = new List<object>();
            sizes.Add("XS");
            sizes.Add("S");
            sizes.Add("M");
            sizes.Add("L");
            sizes.Add("XL");
            sizes.Add("2XL");
            sizes.Add("3XL");
            sizes.Add("4XL");
            sizes.Add("NO SIZE");

            return new JsonNetResult(sizes);
        }




        public ActionResult GetOrderType()
        {

            var GetOrderTypeLists = _uow.TypeOrderFroms.GetAll().Select(p => p.TypeRecapShow).ToList();
            return new JsonNetResult(GetOrderTypeLists);

        }

        public ActionResult GetSkuCode(MasterStyle style)
        {
            var Nomatching = "No matching SKU Codes found.";
            if (style == null || string.IsNullOrWhiteSpace(style.StyleCode))
            {
                return new JsonNetResult(style);
            }
            var skuCodes = _uow.MasterStyles.GetAll()
                .Where(z => z.Description == style.StyleCode)
                .GroupBy(p => p.Style) // Column ·ÕèàÃÒ Select
                .Select(g => g.Key)
                .FirstOrDefault();

            if (skuCodes == null || !skuCodes.Any())
            {
                return new JsonNetResult(Nomatching);
            }
            return new JsonNetResult(skuCodes);
        }




        [HttpGet]
        public ActionResult GetProvinces()
        {
            var GetProvinceList = _uow.MasterProvinces.GetAll().GroupBy(p => p.Provinces).ToList();
            var GetProvinceLists = GetProvinceList.Select(g => g.Key).ToList();
            return new JsonNetResult(GetProvinceLists);
        }
        [HttpPost]
        public ActionResult GetDistricts(string Provinces)
        {
            var GetDistrict = _uow.MasterProvinces.GetAll().Where(z => z.Provinces == Provinces).GroupBy(p => p.Districts).ToList();
            var GetDistrictLists = GetDistrict.Select(g => g.Key).ToList();
            return new JsonNetResult(GetDistrictLists);
        }

        [HttpPost]
        public ActionResult GetListSubs(string Districts , string Provinces)
        {
            var GetSubDistricts = _uow.MasterProvinces.GetAll().Where(z => z.Districts == Districts && z.Provinces == Provinces)
                .GroupBy(p => p.SubDistricts).ToList();
            var GetSubDistrictsList = GetSubDistricts.Select(g => g.Key).ToList();
            return new JsonNetResult(GetSubDistrictsList);

        }

        [HttpPost]
        public ActionResult GetListZipcode(MasterProvince request)
        {
            var GetZipcode = _uow.MasterProvinces.GetAll()
            .Where(z => z.SubDistricts == request.SubDistricts && z.Districts == request.Districts)
            .GroupBy(p => p.ZipCode).ToList();

            var GetZipcodeList = GetZipcode.Select(g => g.Key).FirstOrDefault();
            return new JsonNetResult(GetZipcodeList);
        }






        [HttpGet]
        public ActionResult GetLoadRemark()
        {
            var GetLoadRemark = _uow.MasterRemarks.GetAll().Select(k => k.RemarkQuo).ToList();

            return new JsonNetResult(GetLoadRemark);
        }




        [HttpPost]
        public ActionResult GetForEditProduct( string QuotationNumber)
        {


            var dataquoEditProduct = _uow.YmtgProductNdss.GetAll().Where(z => z.QuotationNumber == QuotationNumber).ToList();

            return new JsonNetResult(dataquoEditProduct);
        }










        // File
        [HttpGet]
        public ActionResult GetQuotationFiles(string quotationNumber)

        {
            if (string.IsNullOrEmpty(quotationNumber))
            {
                var error = "Quotation number is required.";
                return new JsonNetResult(error);
            }
            var files = _uow.QuotationFiles.GetAll()
                .Where(f => f.QuotationNumber == quotationNumber).ToList();
            return new JsonNetResult(files);

        }



        public ActionResult DeleteFile(string filePaths, string quotationNumber , int DelIds)
        {
            var getdatamain = _uow.QuotationFiles.GetAll().Where(z => z.Id == DelIds).FirstOrDefault();

            if (getdatamain != null)
            {
                // ลบข้อมูลจากฐานข้อมูล
                _uow.QuotationFiles.Delete(getdatamain.Id);
                _uow.Commit();

                // ลบไฟล์ออกจากโฟลเดอร์
                string filePath = getdatamain.FilePath; // สมมติว่ามี FilePath ในข้อมูล
                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        // Log หรือจัดการข้อผิดพลาด
                        Console.WriteLine($"Error deleting file: {ex.Message}");
                    }
                }

                return new JsonNetResult(getdatamain);
            }
            else
            {
                return new JsonNetResult(new { error = "Data not found" });
            }

        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase files, string fileDescription, string quotationNumber)
        {
            // ตรวจสอบว่าไฟล์ถูกส่งมาหรือไม่
            if (files == null || files.ContentLength == 0)
            {
                return Json(new { success = false, message = "No files uploaded." }, JsonRequestBehavior.AllowGet);
            }

            // ตรวจสอบว่าหมายเลข Quotation ถูกส่งมาหรือไม่
            if (string.IsNullOrEmpty(quotationNumber))
            {
                return Json(new { success = false, message = "Quotation number is required." }, JsonRequestBehavior.AllowGet);
            }

            // กำหนดค่าเริ่มต้นให้ fileDescription หากไม่ได้ส่งมา
            if (string.IsNullOrEmpty(fileDescription))
            {
                fileDescription = string.Empty;
            }

            // สร้างโฟลเดอร์สำหรับเก็บไฟล์
            var quotationFolderPath = Path.Combine(Server.MapPath("~/Uploads"), quotationNumber);
            if (!Directory.Exists(quotationFolderPath))
            {
                Directory.CreateDirectory(quotationFolderPath);
            }

            // กำหนดชื่อไฟล์และที่อยู่สำหรับบันทึกไฟล์
            var fileName = Path.GetFileName(files.FileName);
            var filePath = Path.Combine(quotationFolderPath, fileName);

            // ตรวจสอบว่ามีไฟล์ชื่อเดียวกันอยู่แล้วหรือไม่ หากมีให้เปลี่ยนชื่อ
            int counter = 1;
            while (System.IO.File.Exists(filePath))
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                var fileExtension = Path.GetExtension(fileName);
                fileName = $"{fileNameWithoutExtension}_{counter++}{fileExtension}";
                filePath = Path.Combine(quotationFolderPath, fileName);
            }

            // บันทึกไฟล์ลงเซิร์ฟเวอร์
            files.SaveAs(filePath);

            // สร้างข้อมูลไฟล์ใหม่สำหรับบันทึกลงฐานข้อมูล
            var newFile = new QuotationFile
            {
                QuotationNumber = quotationNumber,
                FileName = fileName,
                FilePath = Path.Combine("Uploads", quotationNumber, fileName).Replace("\\", "/"), // ใช้ Path แบบ Relative
                FileDescription = fileDescription,
                CreatedAt = DateTime.Now
            };

            // บันทึกข้อมูลลงฐานข้อมูล
            _uow.QuotationFiles.Add(newFile);
            _uow.Commit();

            // ส่งข้อมูลกลับไปยัง Client
            return Json(new { success = true, data = newFile }, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public ActionResult DownloadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "File path is required.");
            }

            // ตรวจสอบว่า path เริ่มต้นด้วย "Uploads" หรือไม่
            if (filePath.StartsWith("Uploads", StringComparison.OrdinalIgnoreCase))
            {
                filePath = filePath.Substring("Uploads".Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }

            // ระบุ path แบบ absolute
            var absolutePath = Path.Combine(Server.MapPath("~/Uploads"), filePath);

            if (!System.IO.File.Exists(absolutePath))
            {
                return HttpNotFound("File not found.");
            }

            var fileName = Path.GetFileName(absolutePath);
            var fileBytes = System.IO.File.ReadAllBytes(absolutePath);

            // ส่งไฟล์กลับไปให้ผู้ใช้
            return File(fileBytes, "application/octet-stream", fileName);
        }




        


        //Update data
        [HttpPost]
        public ActionResult UpdateQuotations(QuotationUpdateModel updateModel , string EmpNos)
        {


  
            if (updateModel == null)
            {
                var errorcheck = "Invalid data.";

                return new JsonNetResult(errorcheck);

            }


       
            var existingOrder = _uow.YmtgOrderNdss.GetAll().FirstOrDefault(q => q.QuotationNumber == updateModel.QuotationNumber);
            if (existingOrder == null)
            {
                var errorcheck1 = "Quotation not found.";

                return new JsonNetResult(errorcheck1);
            }
            else
            {
                existingOrder.CustomerName = updateModel.CustomerName;
                existingOrder.OrderDate = updateModel.OrderDate;
                existingOrder.ShipDate = updateModel.ShipDate; 
                existingOrder.TotalQty = updateModel.TotalQty;
                existingOrder.TotalPrice = updateModel.TotalPrice;
                existingOrder.QuoRemark = updateModel.Remark;
                existingOrder.CustomerAddress = updateModel.CustomerAddress;
                existingOrder.CustomerPhone = updateModel.CustomerPhone;
                existingOrder.QuoProvince = updateModel.QuoProvince;
                existingOrder.QuoDistricts = updateModel.QuoDistricts;
                existingOrder.QuoSubDistricts = updateModel.QuoSubDistricts;
                existingOrder.QuoZipCode = updateModel.QuoZipCode;
                existingOrder.CreateBy = EmpNos; 
                existingOrder.QuoCompanyName = updateModel.QuoCompanyName;
                existingOrder.QuoLastname = updateModel.QuoLastname;
                existingOrder.QuoTaxID = updateModel.QuoTaxID;
                existingOrder.CustomerEmail = updateModel.CustomerEmail;
                existingOrder.QuoType = updateModel.QuoType;
                existingOrder.QuoLastUpdate = DateTime.Now;
                existingOrder.QuoShippingPrice = updateModel.QuoShippingPrice;
                existingOrder.QuoStatus = updateModel.QuoStatus; 

                _uow.YmtgOrderNdss.Update(existingOrder);
                _uow.Commit();





                //Loop Delete Data 

                var DataProducts = _uow.YmtgProductNdss.GetAll()
               .Where(p => p.QuotationNumber == updateModel.QuotationNumber)
               .ToList();

                foreach(var data in DataProducts)
                {

                    _uow.YmtgProductNdss.Delete(data.Id);
                    _uow.Commit();
                }

                //getdata 

                        var getdataProducts = _uow.YmtgProductNdss.GetAll()
                .Where(p => p.QuotationNumber == updateModel.QuotationNumber)
                .FirstOrDefault();


         

                if (getdataProducts == null)
                {
                    foreach (var entry in updateModel.Entries)
                    {

                        var newpro = new YmtgProductNds();
                        newpro.QuotationNumber = updateModel.QuotationNumber;
                        newpro.OrderNumber = existingOrder.OrderNumber;
                        newpro.ProductName = entry.ProductName;
                        newpro.Qty = entry.Qty;
                        newpro.SKUCode = entry.SKUCodeFull;
                        newpro.SKUCodeFull = entry.SKUCodeFull;
                        newpro.Size = entry.Size;
                        newpro.Price = entry.Price;
                        newpro.Color = entry.Color;
                        newpro.PrintingType = 0;
                        newpro.CreateBy = EmpNos;
                        newpro.CreateDate = DateTime.Now;



                        if (!string.IsNullOrEmpty(entry.SKUCodeFull) && entry.SKUCodeFull.Length > 3)
                        {
                            newpro.SKUCode = entry.SKUCodeFull.Substring(0, entry.SKUCodeFull.Length - 3);
                        }
                        if (!string.IsNullOrEmpty(entry.Sku) && entry.Sku.Length > 3)
                        {
                            string lastThreeChars = entry.Sku.Substring(entry.Sku.Length - 3);
                            if (int.TryParse(lastThreeChars, out int printingType))
                            {
                                newpro.PrintingType = printingType;
                            }
                        }
                        _uow.YmtgProductNdss.Add(newpro);
                        _uow.Commit();
                    }
                }
            }
            return new JsonNetResult(existingOrder);
        }





        public ActionResult NdsSystemCreateQuotation(int EmpNo)
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
                    return View(EmpNo);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }



        [HttpGet]
        public ActionResult PrintPDF(string quotationNumber)
        {

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var FontLinkBold = Path.Combine(baseDirectory, @"fonts\THSarabunPSK\THSarabun Bold.ttf");
            var FontLinkNormal = Path.Combine(baseDirectory, @"fonts\THSarabunPSK\THSarabun.ttf");

            string remarkText = "";
            string accountInfo = "ธนาคารไทยพาณิชย์ จำกัด (มหาชน) ชื่อบัญชี NOBLE DESTRIBUTION S เลขที่บัญชี 278-227978-1";
            string preparedBy = "";

            //var LogoLink = @"wwwroot/images/logo-nds.png";
            string logoLink = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logo-nds.png");

            var GetProductTable = new List<YmtgProductNds>();
            var GetOrderTable = new YmtgOrderNds();
            var GetMasterStyle = new MasterStyle();

            if (quotationNumber != null && quotationNumber != "")
            {
                GetOrderTable = _uow.YmtgOrderNdss.GetAll().Where(z => z.QuotationNumber == quotationNumber).FirstOrDefault();

                GetProductTable = _uow.YmtgProductNdss.GetAll().Where(z => z.QuotationNumber == quotationNumber).ToList();


                remarkText = GetOrderTable.QuoRemark;
                preparedBy = GetOrderTable?.CreateBy ?? "Unknown";
                //GetMasterStyle = _context.MasterStyles

            }


                using (MemoryStream stream = new MemoryStream())
                {
                    // Create a new PDF document
                    Document document = new Document(PageSize.A4, 25, 25, 5, 55); //เดิม 25, 25, 30, 30
                    PdfWriter writer = PdfWriter.GetInstance(document, stream);

                    //เพิ่มสำหรับ footer 
                    writer.PageEvent = new FooterEvent(remarkText, accountInfo, preparedBy, FontLinkBold, FontLinkNormal);
                    document.Open();


                    // Header with Image and Text
                    PdfPTable headerTable = new PdfPTable(2);
                    headerTable.WidthPercentage = 100;
                    float[] columnWidths = { 1f, 3f };
                    headerTable.SetWidths(columnWidths);

                // Adding Image
                //string imagePath = LogoLink; // Adjust the path as needed
                //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);


                string logoLinkw = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/logo-nds.png");

                //// เพิ่มรูปใน PDF
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoLinkw);
                logo.ScaleToFit(70f, 100f);
                logo.Alignment = Element.ALIGN_RIGHT;

                PdfPCell imageCell = new PdfPCell(logo);
                imageCell.Border = Rectangle.NO_BORDER;
                imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                imageCell.PaddingTop = 25f; // รูปกับด้านบน
                headerTable.AddCell(imageCell);



                // Adding Company Information Text without Table
                // Company Name with larger font
                Paragraph companyName = new Paragraph(
                        "บริษัท โนเบิล ดิสทริบิวชั่นเซอรวิ์สเซส(ไทย) จํากัด\n",
                        new Font(BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 22, Font.NORMAL));

                    companyName.Alignment = Element.ALIGN_LEFT;

                    document.Add(new Paragraph("\n")); // เว้นบรรทัด 

                    // Company Address and Contact Information with smaller font
                    Paragraph companyDetails = new Paragraph(
                        "47/403 ถนนป๊อปปูล่า ตําบลบ้านใหม่ อําเภอปากเกร็ด จ.นนทบุรี 11120\n" +
                        "โทร : 024291302    อีเมล์ : Phitsukan@yehpattana.com\n" +
                        "เลขที่ประจําตัวผู้เสียภาษีอากร : 0125561013962",
                        new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 16, Font.NORMAL));
                    companyDetails.Alignment = Element.ALIGN_LEFT;
                    companyDetails.Leading = 15f; // ระยะห่างระหว่างบรรทัด


                    PdfPCell companyInfoCell = new PdfPCell();
                    companyInfoCell.AddElement(companyName);
                    companyInfoCell.AddElement(companyDetails);
                    companyInfoCell.Border = Rectangle.NO_BORDER;
                    companyInfoCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                    headerTable.AddCell(companyInfoCell);
                    headerTable.SpacingBefore = 0f;

                    document.Add(headerTable);

                    document.Add(new Paragraph("\n")); // เว้นบรรทัด 

                    // Text : ใบเสนอราคา (Quotation)
                    // สร้างฟอนต์สำหรับข้อความ
                    BaseFont baseFont = BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font fonthead = new Font(baseFont, 20, Font.NORMAL);
                    BaseFont baseFontDe = BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font fontDe = new Font(baseFontDe, 16, Font.NORMAL);

                    //สร้างตารางที่มี 3 Column
                    PdfPTable tableHeadTextQuotation = new PdfPTable(3);
                    tableHeadTextQuotation.WidthPercentage = 100; // กำหนดความกว้างของตารางเป็น 100%
                    float[] TextQuotationWidths = { 1f, 3f, 1f };
                    tableHeadTextQuotation.SetWidths(TextQuotationWidths);


                    //เพิ่มข้อมูล
                    PdfPCell cell1 = new PdfPCell(new Phrase(""));
                    cell1.Border = PdfPCell.NO_BORDER;

                    tableHeadTextQuotation.AddCell(cell1);


                    //// สร้างเซลล์และเพิ่มข้อความลงในเซลล์
                    //PdfPCell cell2 = new PdfPCell(new Phrase("ใบเสนอราคา (Quotation)\n", font))
                    //{
                    //    HorizontalAlignment = Element.ALIGN_CENTER, // จัดข้อความกึ่งกลาง
                    //    Border = PdfPCell.BOX, // กำหนดเส้นขอบรอบเซลล์
                    //    BorderWidth = 1f, //กำหนดความหนาของเส้นขอบ
                    //    Padding = 5f // กำหนดระยะห่างระหว่างข้อความกับเส้นขอบ
                    //};

                    Phrase phrase = new Phrase();
                    phrase.Add(new Chunk("ใบเสนอราคา (Quotation)", fonthead));
                    phrase.Add(Chunk.NEWLINE);
                    phrase.Add(Chunk.NEWLINE);


                    // สร้าง PdfPCell และเพิ่ม Phrase ลงในเซลล์
                    PdfPCell cell2 = new PdfPCell(phrase)
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Border = PdfPCell.BOX,
                        BorderWidth = 1f,
                        Padding = 5f
                    };


                    //// เพิ่มเซลล์ลงในตาราง
                    tableHeadTextQuotation.AddCell(cell2);

                    PdfPCell cell3 = new PdfPCell(new Phrase(""));
                    cell3.Border = PdfPCell.NO_BORDER;

                    tableHeadTextQuotation.AddCell(cell3);
                    tableHeadTextQuotation.SpacingBefore = 0f; //กำหนดระยะห่างจากด้านบน
                    // เพิ่มตารางลงในเอกสาร
                    document.Add(tableHeadTextQuotation);

                    // สร้างตาราง 2 คอลัมน์
                    PdfPTable tableHeadDetail = new PdfPTable(2);
                    tableHeadDetail.WidthPercentage = 100; // ความกว้างตาราง 100%
                    float[] DetailWidths = { 1.5f, 1f }; // ค่าความกว้างของคอลัมน์
                    tableHeadDetail.SetWidths(DetailWidths);

                    // กำหนดค่าตัวแปร
                    var Customer = GetOrderTable.CustomerName + " " + GetOrderTable.QuoLastname;
                    var CompanyCust = GetOrderTable.QuoCompanyName;

                    if (string.IsNullOrEmpty(GetOrderTable.CustomerName) && string.IsNullOrEmpty(GetOrderTable.QuoLastname))
                    {
                        Customer = "-";
                    }

                    if (string.IsNullOrEmpty(CompanyCust))
                    {
                        CompanyCust = "-";
                    }

                    //----ชื่อลูกค้าและชื่อบริษัท (รวมใน Cell ด้านซ้าย)
                    Paragraph LableCustCompany = new Paragraph();
                    LableCustCompany.Add(new Chunk("ชื่อลูกค้า :   ", new Font(BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    LableCustCompany.Add(new Chunk(Customer, new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    LableCustCompany.Add(Chunk.NEWLINE);
                    LableCustCompany.Add(new Chunk("ชื่อบริษัท :  ", new Font(BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    LableCustCompany.Add(new Chunk(CompanyCust, new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    LableCustCompany.Leading = 15f;
                    LableCustCompany.Alignment = Element.ALIGN_LEFT;


                    PdfPCell cellLeft1 = new PdfPCell();
                    cellLeft1.Border = PdfPCell.NO_BORDER;
                    cellLeft1.AddElement(LableCustCompany);

                    //----เลขที่ใบเสนอราคา (ด้านขวา)
                    Paragraph LabelQuotationNumber = new Paragraph();
                    LabelQuotationNumber.Add(new Chunk("เลขที่ใบเสนอราคา : ", new Font(BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    LabelQuotationNumber.Add(new Chunk(GetOrderTable.QuotationNumber, new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    LabelQuotationNumber.Alignment = Element.ALIGN_RIGHT;


                    PdfPCell cellRight1 = new PdfPCell();
                    cellRight1.Border = PdfPCell.NO_BORDER;
                    cellRight1.AddElement(LabelQuotationNumber);

                    // เพิ่มแถวแรกในตาราง
                    tableHeadDetail.AddCell(cellLeft1);
                    tableHeadDetail.AddCell(cellRight1);

                    //----ที่อยู่ลูกค้า (เต็มความกว้างของตาราง)
                    //Paragraph LableAddress = new Paragraph();
                    //LableAddress.Add(new Chunk("ที่อยู่ : ", new Font(BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    //LableAddress.Add(new Chunk(GetOrderTable.CustomerAddress + "\n", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    //LableAddress.Add(new Chunk("ตำบล " + GetOrderTable.QuoSubDistricts + "  อำเภอ " + GetOrderTable.QuoDistricts + "\n", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    //LableAddress.Add(new Chunk("จังหวัด " + GetOrderTable.QuoProvince + " " + GetOrderTable.QuoZipCode + "\n", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    //LableAddress.Leading = 15f;
                    //LableAddress.Alignment = Element.ALIGN_LEFT;

                    Paragraph LableAddress = new Paragraph();
                    LableAddress.Add(new Chunk("ที่อยู่ :       ", new Font(BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    LableAddress.Add(new Chunk(GetOrderTable.CustomerAddress + " ตำบล " + GetOrderTable.QuoSubDistricts + "\n", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    LableAddress.Add(new Chunk("               อำเภอ " + GetOrderTable.QuoDistricts + " จังหวัด " + GetOrderTable.QuoProvince + " " + GetOrderTable.QuoZipCode + "\n", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    LableAddress.Leading = 15f;
                    LableAddress.Alignment = Element.ALIGN_LEFT;


                    PdfPCell cellFullWidth = new PdfPCell();
                    cellFullWidth.Colspan = 2; // กำหนดให้ Cell นี้ครอบคลุมทั้ง 2 คอลัมน์
                    cellFullWidth.Border = PdfPCell.NO_BORDER;
                    cellFullWidth.AddElement(LableAddress);

                    // เพิ่มแถวที่สอง (ที่อยู่)
                    tableHeadDetail.AddCell(cellFullWidth);

                    // เพิ่มตารางใน Document
                    document.Add(tableHeadDetail);


                    //document.Add(new Paragraph("\n"));

                    Paragraph TaxID = new Paragraph();
                    TaxID.Add(new Chunk("เลขที่ประจําตัวผู้เสียภาษีอากร : ", new Font(BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    TaxID.Add(new Chunk(GetOrderTable.QuoTaxID, new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 15)));
                    TaxID.Alignment = Element.ALIGN_LEFT;
                    document.Add(TaxID);

                    document.Add(new Paragraph("\n"));

                    //document.Add(tableHeadDetail);

                    // Table of items (Full Width Alignment) -- ตารางที่มีรายการสินค้า
                    PdfPTable table = new PdfPTable(5);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 1f, 5f, 1.5f, 1.5f, 1.5f });

                    // Table headers
                    PdfPCell[] headers = new PdfPCell[]
                    {
                        new PdfPCell(new Phrase("ลําดับ \n\n", fontDe)),
                        new PdfPCell(new Phrase("รายการสินค้า \n\n", fontDe)),
                        new PdfPCell(new Phrase("จำนวน (PCS.)\n\n", fontDe)),
                        new PdfPCell(new Phrase("ราคา/หน่วย (บาท)\n\n", fontDe)),
                        new PdfPCell(new Phrase("จำนวนเงิน (บาท)\n\n", fontDe))
                    };
                    foreach (var headerCell in headers)
                    {
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(headerCell);
                    }


                    // Add rows for each product
                    int index = 1; // Start the index for "ลำดับ"
                    foreach (var product in GetProductTable)
                    {
                        // ดึงข้อมูลจาก MasterStyle
                        var masterStyle = _uow.MasterStyles.GetAll()
                            .Where(ms => ms.StyleCode == product.ProductName)
                            .FirstOrDefault();

                        string productDescription = product.ProductName;

                        if (masterStyle != null)
                        {
                            if (product.Size == "NO SIZE" && product.Color != "NO COLOR")
                            {
                                productDescription = productDescription + ", Fabric " + masterStyle.FabricDesc + ", Pattern " + masterStyle.PatternDetail +
                                ", Color " + product.Color;
                            }
                            else if (product.Size != "NO SIZE" && product.Color == "NO COLOR")
                            {
                                productDescription = productDescription + ", Fabric " + masterStyle.FabricDesc + ", Pattern " + masterStyle.PatternDetail +
                                    ", Size " + product.Size;
                            }
                            else if (product.Size == "NO SIZE" && product.Color == "NO COLOR")
                            {
                                productDescription = productDescription + ", Fabric " + masterStyle.FabricDesc + ", Pattern " + masterStyle.PatternDetail;
                            }
                            else
                            {

                                productDescription = productDescription + ", Fabric " + masterStyle.FabricDesc + ", Pattern " + masterStyle.PatternDetail +
                                    ", Size " + product.Size + ", Color " + product.Color;
                            }

                        }


                        // ลำดับ
                        table.AddCell(new PdfPCell(new Phrase(index.ToString(), new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                            //PaddingBottom = 10f 
                        });

                        // รายการสินค้า
                        table.AddCell(new PdfPCell(new Phrase(productDescription, new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            PaddingBottom = 10f
                        });

                        // จำนวน
                        table.AddCell(new PdfPCell(new Phrase($"{product.Qty}", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                            //PaddingBottom = 10f
                        });

                        // ราคา/หน่วย
                        decimal pricePerUnit = product.Qty > 0 ? product.Price / product.Qty : 0;
                        table.AddCell(new PdfPCell(new Phrase($"{pricePerUnit.ToString("N2")}", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT
                            //PaddingBottom = 10f
                        });

                        // จำนวนเงิน
                        table.AddCell(new PdfPCell(new Phrase($"{product.Price.ToString("N2")}", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT
                            //PaddingBottom = 10f
                        });

                        index++; // Increment the index
                    }

                    // Add shipping fee row if QuoShippingPrice > 0
                    if (GetOrderTable.QuoShippingPrice > 0)
                    {
                        // ลำดับ (ใช้ลำดับที่มากที่สุด)
                        table.AddCell(new PdfPCell(new Phrase(index.ToString(), new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                        // รายการสินค้า (ค่าขนส่ง)
                        table.AddCell(new PdfPCell(new Phrase("ค่าขนส่ง (Shipping Fee)", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            PaddingBottom = 10f
                        });

                        // จำนวน (เว้นว่าง)
                        table.AddCell(new PdfPCell(new Phrase("-", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                        // ราคา/หน่วย (เว้นว่าง)
                        table.AddCell(new PdfPCell(new Phrase("-", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT
                        });

                        // จำนวนเงิน (แสดงค่าขนส่ง)
                        table.AddCell(new PdfPCell(new Phrase($"{GetOrderTable.QuoShippingPrice.ToString("N2")}", new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 14)))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT
                        });



                    }

                    document.Add(table);

                    document.Add(new Paragraph("\n"));

                    //คำนวนราคารวม-----------------------------------

                    decimal totalAmountValue = 0;

                    // คำนวณยอดรวมจากข้อมูลใน GetProductTable
                    foreach (var product in GetProductTable)
                    {
                        totalAmountValue += product.Price;
                    }

                    // รวมค่าขนส่งเข้าใน totalAmountValue
                    totalAmountValue = totalAmountValue + GetOrderTable.QuoShippingPrice;
                    // คำนวณ VAT 7%
                    //decimal vat = totalAmountValue * 0.07m;
                    // ราคาก่อน VAT 
                    decimal BeforeVat = (totalAmountValue * 100) / 107;

                    decimal vat = (totalAmountValue * 7) / 107;

                    // คำนวณรวมราคาทั้งสิ้น
                    //decimal grandTotal = totalAmountValue + vat;



                    // Total amount (Right Alignment)
                    //Paragraph totalAmount = new Paragraph("Vat 7% : " + vat.ToString("N2") + "\n" +
                    //                                     "ราคาก่อน Vat : " + BeforeVat.ToString("N2") + "\n" +
                    //                                     "รวมราคาทั้งสิ้น : " + totalAmountValue.ToString("N2") + "",
                    //                                      new Font(BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 16));
                    //totalAmount.Alignment = Element.ALIGN_RIGHT;
                    //document.Add(totalAmount);

                    // สร้างตารางหลักสำหรับรูปภาพและ totalAmountTable
                    PdfPTable mainTable = new PdfPTable(1); // สร้างตาราง 2 คอลัมน์
                    mainTable.WidthPercentage = 25;
                    mainTable.HorizontalAlignment = Element.ALIGN_RIGHT;// กำหนดความกว้างตารางเป็น 100% ของหน้ากระดาษ
                    mainTable.SetWidths(new float[] { 1f }); // สัดส่วนคอลัมน์: รูปภาพ 2 ส่วน, ตารางตัวเลข 1 ส่วน





                    // สร้างตาราง 2 คอลัมน์เพื่อแสดงข้อความและตัวเลข
                    PdfPTable totalAmountTable = new PdfPTable(2);
                    totalAmountTable.WidthPercentage = 25; // กำหนดความกว้างของตาราง (50% ของหน้ากระดาษ)
                    totalAmountTable.HorizontalAlignment = Element.ALIGN_RIGHT; // จัดตารางชิดขวา
                    totalAmountTable.SetWidths(new float[] { 2f, 1f }); // กำหนดสัดส่วนคอลัมน์ (ข้อความ:ตัวเลข)



                    // ฟอนต์สำหรับข้อความ
                    Font boldFont = new Font(BaseFont.CreateFont(FontLinkBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 16);
                    Font normalFont = new Font(BaseFont.CreateFont(FontLinkNormal, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 16);

                    // เพิ่มข้อความและตัวเลขในแต่ละแถว

                    // Row 3: รวมราคาทั้งสิ้น
                    totalAmountTable.AddCell(CreateAlignedCell("รวมราคาทั้งสิ้น : ", boldFont, Element.ALIGN_LEFT));
                    totalAmountTable.AddCell(CreateAlignedCell(totalAmountValue.ToString("N2"), normalFont, Element.ALIGN_RIGHT));


                    // Row 2: ราคาก่อน Vat
                    totalAmountTable.AddCell(CreateAlignedCell("ราคาก่อน Vat : ", boldFont, Element.ALIGN_LEFT));
                    totalAmountTable.AddCell(CreateAlignedCell(BeforeVat.ToString("N2"), normalFont, Element.ALIGN_RIGHT));


                    // Row 1: Vat 7%
                    totalAmountTable.AddCell(CreateAlignedCell("Vat 7% : ", boldFont, Element.ALIGN_LEFT));
                    totalAmountTable.AddCell(CreateAlignedCell(vat.ToString("N2"), normalFont, Element.ALIGN_RIGHT));

                    //TableMain
                    // เซลล์ที่สอง: ใส่ totalAmountTable
                    PdfPCell totalAmountCell = new PdfPCell(totalAmountTable); // ใส่ตารางตัวเลขที่สร้างไว้
                    totalAmountCell.Border = Rectangle.NO_BORDER; // ไม่แสดงเส้นขอบ
                    totalAmountCell.HorizontalAlignment = Element.ALIGN_RIGHT; // จัดแนวตารางในเซลล์
                    mainTable.AddCell(totalAmountCell);

                // เพิ่มตารางลงในเอกสาร
                //document.Add(totalAmountTable);



                // เซลล์แรก: ใส่รูปภาพ
                // ใช้ Path.Combine สร้างพาธในโครงสร้างโฟลเดอร์ของ wwwroot
                //string pmLink = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "payment.png");


                //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(pmLink); // ใส่ path ของรูปภาพ
                //    image.ScaleToFit(100f, 100f); // ปรับขนาดรูปภาพ
                //    image.Alignment = Element.ALIGN_CENTER; // จัดรูปภาพให้อยู่ตรงกลาง


                string pmLink = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/payment.png");

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(pmLink); // ใส่ path ของรูปภาพ
                    image.ScaleToFit(100f, 100f); // ปรับขนาดรูปภาพ
                    image.Alignment = Element.ALIGN_CENTER; // จัดรูปภาพให้อยู่ตรงกลาง
    

                PdfPCell imageCellpayment = new PdfPCell(image);
                    imageCellpayment.Border = Rectangle.NO_BORDER; // ไม่แสดงเส้นขอบ
                    imageCellpayment.HorizontalAlignment = Element.ALIGN_RIGHT; // จัดแนวรูปภาพในเซลล์
                    imageCellpayment.PaddingTop = 10f;
                    mainTable.AddCell(imageCellpayment);



                    // เพิ่ม mainTable ลงในเอกสาร PDF
                    document.Add(mainTable);






                    // ฟังก์ชันช่วยสร้างเซลล์ที่จัดตำแหน่ง
                    PdfPCell CreateAlignedCell(string text, Font font, int alignment)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(text, font));
                        cell.Border = Rectangle.NO_BORDER; // ไม่มีเส้นขอบ
                        cell.HorizontalAlignment = alignment; // จัดตำแหน่งข้อความในเซลล์
                        return cell;
                    }


                    document.Add(new Paragraph("\n"));


                    document.Close();

                    byte[] bytes = stream.ToArray();
                    return File(bytes, "application/pdf", GetOrderTable.QuotationNumber + ".pdf");
                }
     
      
        }






































































        public ActionResult GetMasterCustomerName()
        {
            var query = _uow.YMTGOrderDetails.GetAll().GroupBy(p => p.CustomerCode).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }

        public ActionResult GetMasterBandName(string CodeName)
        {
            var query = _uow.YMTGOrderDetails.GetAll().Where(z => z.CustomerCode == CodeName).FirstOrDefault();
            return new JsonNetResult(query);
        }
        public ActionResult GetMasterSeasons(string CodeName)
        {
            var query = _uow.YMTGOrderDetails.GetAll().Where(z=> z.CustomerCode == CodeName).GroupBy(p => p.Season).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }

        public ActionResult GetMasterStyle(string CodeName , string SeasonName)
        {
            var query = _uow.YMTGOrderDetails.GetAll().Where(z => z.CustomerCode == CodeName && z.Season == SeasonName).GroupBy(p => p.Style).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }

        public ActionResult GetMasterOrder(string CodeName, string SeasonName , string StyleName)
        {
            var query = _uow.YMTGOrderDetails.GetAll().Where(z => z.CustomerCode == CodeName && z.Season == SeasonName && z.Style == StyleName).GroupBy(p => p.OrderNo).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }

        
        public ActionResult GetListAllStyle(string CodeName,  string ListStyle , string SeasonName)
        {
            if(ListStyle == null)
            {
                var getlist = _uow.YPTGUploadfileDatas.GetAll().Where(x => x.Season == SeasonName && x.BrandCode  == CodeName).ToList();
                return new JsonNetResult(getlist);
            }
            else
            {
                var getlist = _uow.YPTGUploadfileDatas.GetAll().Where(x => x.Season == SeasonName && x.StyleName == ListStyle).ToList();
                return new JsonNetResult(getlist);
            }
        }


        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files, string brandCode, string brandName, string season, string styleName , string OrderNo , string TypeName , int Id)
        {
            if (files == null || !files.Any())
            {
                return new JsonNetResult("No files uploaded.");
            }
            var UploadDataDetail = new YPTGUploadDetail();
            var uploadedFileDB = new YPTGUploadfileData();

            var uploadFolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/");

            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            var folderPathToBrandCode = Path.Combine(uploadFolderPath + brandCode);

            // Check if the folder exists
            if (!Directory.Exists(folderPathToBrandCode))
            {
                Directory.CreateDirectory(folderPathToBrandCode);
            }

            var BrandToseason = Path.Combine(folderPathToBrandCode + @"\" + season);

            if (!Directory.Exists(BrandToseason))
            {
                Directory.CreateDirectory(BrandToseason);
            }

            var SeasonTostyleName = Path.Combine(BrandToseason + @"\" +  styleName);

            if (!Directory.Exists(SeasonTostyleName) && styleName != null && styleName != "undefined")
            {
                Directory.CreateDirectory(SeasonTostyleName);
            }

            var StyleNameToTypeName = Path.Combine(SeasonTostyleName + @"\" + TypeName);

            if (!Directory.Exists(StyleNameToTypeName))
            {
                Directory.CreateDirectory(StyleNameToTypeName);
            }


            if(OrderNo == "undefined" || OrderNo == "null")
            {

       
                var getdataorders = _uow.YMTGOrderDetails.GetAll().Where(x => x.Style == styleName && x.CustomerCode == brandCode && x.Season == season).ToList();


                foreach(var listorder in getdataorders)
                {


                    var checkdata = _uow.YPTGUploadfileDatas.GetAll().Where(zz => zz.OrderNo == listorder.OrderNo && zz.TypeName == TypeName).FirstOrDefault();

                    if(checkdata == null)
                    {
                        foreach (var file in files)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var originalFileName = Path.GetFileName(file.FileName);

                                var filePath = "";
                                var renamedFileName = "";

                                string typePrefix = string.Empty;

                                switch (TypeName)
                                {
                                    case "MRP":
                                        typePrefix = "MRP";
                                        break;
                                    case "PACKING":
                                        typePrefix = "PK";
                                        break;
                                    case "SIZE BREAK DOWN":
                                        typePrefix = "SB";
                                        break;
                                    case "SPEC SHEET":
                                        typePrefix = "SP";
                                        break;
                                    case "SWATCH CARD":
                                        typePrefix = "SC";
                                        break;
                                    case "P-NOTED":
                                        typePrefix = string.Empty;
                                        break;
                                }

                                if (!string.IsNullOrEmpty(typePrefix) || TypeName == "P-NOTED")
                                {
                                    if (TypeName == "P-NOTED")
                                    {
                                        renamedFileName = $"{styleName}-{listorder.OrderNo}{Path.GetExtension(originalFileName)}";
                                    }
                                    else
                                    {
                                        renamedFileName = $"{typePrefix}-{styleName}-{listorder.OrderNo}{Path.GetExtension(originalFileName)}";
                                    }
                                    filePath = Path.Combine(StyleNameToTypeName, renamedFileName);
                                    file.SaveAs(filePath);
                                }

                                var getuserpass = _uow.YMTGUsers.GetAll().Where(z => z.Id == Id).FirstOrDefault();


                                uploadedFileDB.TypeName = TypeName;
                                uploadedFileDB.BrandCode = brandCode;
                                uploadedFileDB.BrandName = brandName;
                                uploadedFileDB.StyleName = styleName;
                                uploadedFileDB.Season = season;
                                uploadedFileDB.OrderNo = listorder.OrderNo;
                                uploadedFileDB.NewFileName = renamedFileName;
                                uploadedFileDB.FileUploadCode = Guid.NewGuid().ToString();
                                uploadedFileDB.OriginalFileName = originalFileName;
                                uploadedFileDB.FileExtension = Path.GetExtension(originalFileName);
                                uploadedFileDB.FilePathUrl = StyleNameToTypeName;
                                uploadedFileDB.FilePathName = filePath;
                                uploadedFileDB.Revision = "";
                                uploadedFileDB.RevisionDate = "";
                                uploadedFileDB.Status = 1;
                                uploadedFileDB.Remark = TypeName + brandName + styleName + season + listorder.OrderNo;
                                uploadedFileDB.CreateDate = DateTime.Now;
                                uploadedFileDB.CreateBy = getuserpass.YPTName;
                                uploadedFileDB.FactoryCode = getuserpass.Factory;
                                uploadedFileDB.FileId = 1;

                                _uow.YPTGUploadfileDatas.Add(uploadedFileDB);
                                _uow.Commit();

                            }
                        }
                    }else
                    {
                        foreach (var file in files)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var originalFileName = Path.GetFileName(file.FileName);

                                var filePath = "";
                                var renamedFileName = "";

                                string typePrefix = string.Empty;

                                switch (TypeName)
                                {
                                    case "MRP":
                                        typePrefix = "MRP";
                                        break;
                                    case "PACKING":
                                        typePrefix = "PK";
                                        break;
                                    case "SIZE BREAK DOWN":
                                        typePrefix = "SB";
                                        break;
                                    case "SPEC SHEET":
                                        typePrefix = "SP";
                                        break;
                                    case "SWATCH CARD":
                                        typePrefix = "SC";
                                        break;
                                    case "P-NOTED":
                                        typePrefix = string.Empty;
                                        break;
                                }

                                if (!string.IsNullOrEmpty(typePrefix) || TypeName == "P-NOTED")
                                {
                                    if (TypeName == "P-NOTED")
                                    {
                                        renamedFileName = $"{styleName}-{OrderNo}{Path.GetExtension(originalFileName)}";
                                    }
                                    else
                                    {
                                        renamedFileName = $"{typePrefix}-{styleName}-{OrderNo}{Path.GetExtension(originalFileName)}";
                                    }
                                    filePath = Path.Combine(StyleNameToTypeName, renamedFileName);
                                    file.SaveAs(filePath);
                                }

                                var getuserpass = _uow.YMTGUsers.GetAll().Where(z => z.Id == Id).FirstOrDefault();


                                uploadedFileDB.TypeName = TypeName;
                                uploadedFileDB.BrandCode = brandCode;
                                uploadedFileDB.BrandName = brandName;
                                uploadedFileDB.StyleName = styleName;
                                uploadedFileDB.Season = season;
                                uploadedFileDB.OrderNo = OrderNo;
                                uploadedFileDB.NewFileName = renamedFileName;
                                uploadedFileDB.FileUploadCode = Guid.NewGuid().ToString();
                                uploadedFileDB.OriginalFileName = originalFileName;
                                uploadedFileDB.FileExtension = Path.GetExtension(originalFileName);
                                uploadedFileDB.FilePathUrl = StyleNameToTypeName;
                                uploadedFileDB.FilePathName = filePath;
                                uploadedFileDB.Revision = "";
                                uploadedFileDB.RevisionDate = "";
                                uploadedFileDB.Status = 1;
                                uploadedFileDB.Remark = TypeName + brandName + styleName + season + OrderNo;
                                uploadedFileDB.CreateDate = DateTime.Now;
                                uploadedFileDB.CreateBy = getuserpass.YPTName;
                                uploadedFileDB.FactoryCode = getuserpass.Factory;
                                uploadedFileDB.FileId = 1;

                                _uow.YPTGUploadfileDatas.Add(uploadedFileDB);
                                _uow.Commit();

                            }
                        }
                    }

                   
                }



            }
            else
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalFileName = Path.GetFileName(file.FileName);

                        var filePath = "";
                        var renamedFileName = "";

                        string typePrefix = string.Empty;

                        switch (TypeName)
                        {
                            case "MRP":
                                typePrefix = "MRP";
                                break;
                            case "PACKING":
                                typePrefix = "PK";
                                break;
                            case "SIZE BREAK DOWN":
                                typePrefix = "SB";
                                break;
                            case "SPEC SHEET":
                                typePrefix = "SP";
                                break;
                            case "SWATCH CARD":
                                typePrefix = "SC";
                                break;
                            case "P-NOTED":
                                typePrefix = string.Empty;
                                break;
                        }

                        if (!string.IsNullOrEmpty(typePrefix) || TypeName == "P-NOTED")
                        {
                            if (TypeName == "P-NOTED")
                            {
                                renamedFileName = $"{styleName}-{OrderNo}{Path.GetExtension(originalFileName)}";
                            }
                            else
                            {
                                renamedFileName = $"{typePrefix}-{styleName}-{OrderNo}{Path.GetExtension(originalFileName)}";
                            }
                            filePath = Path.Combine(StyleNameToTypeName, renamedFileName);
                            file.SaveAs(filePath);
                        }

                        var getuserpass = _uow.YMTGUsers.GetAll().Where(z => z.Id == Id).FirstOrDefault();


                        uploadedFileDB.TypeName = TypeName;
                        uploadedFileDB.BrandCode = brandCode;
                        uploadedFileDB.BrandName = brandName;
                        uploadedFileDB.StyleName = styleName;
                        uploadedFileDB.Season = season;
                        uploadedFileDB.OrderNo = OrderNo;
                        uploadedFileDB.NewFileName = renamedFileName;
                        uploadedFileDB.FileUploadCode = Guid.NewGuid().ToString();
                        uploadedFileDB.OriginalFileName = originalFileName;
                        uploadedFileDB.FileExtension = Path.GetExtension(originalFileName);
                        uploadedFileDB.FilePathUrl = StyleNameToTypeName;
                        uploadedFileDB.FilePathName = filePath;
                        uploadedFileDB.Revision = "";
                        uploadedFileDB.RevisionDate = "";
                        uploadedFileDB.Status = 1;
                        uploadedFileDB.Remark = TypeName + brandName + styleName + season + OrderNo;
                        uploadedFileDB.CreateDate = DateTime.Now;
                        uploadedFileDB.CreateBy = getuserpass.YPTName;
                        uploadedFileDB.FactoryCode = getuserpass.Factory;
                        uploadedFileDB.FileId = 1;

                        _uow.YPTGUploadfileDatas.Add(uploadedFileDB);
                        _uow.Commit();

                    }
                }
            }
    


   



            //string recipient = "jaruwan.s@yehpattana.com";
            //string subject = "TON IT TEST";
            //string body = "TEST MAIL >> ทดสอบการส่งเมล์";

            //SendEmail(recipient, subject, body);

            var GetFinishdata = _uow.YPTGUploadfileDatas.GetAll().Where(t => t.StyleName == styleName).OrderByDescending(t => t.Id).ToList();





            // กำหนดโปรโตคอล TLS ที่ต้องการ (.NET Framework 4.5 ขึ้นไปจะรองรับ TLS 1.2)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // ปิดการตรวจสอบใบรับรอง SSL (ถ้ามีปัญหาเรื่อง SSL Certificate)
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            // URL ของ Line Notify API
            string url = "https://notify-api.line.me/api/notify";

            // สร้างคำขอแบบ HTTP POST
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";

            // กำหนด Authorization header สำหรับ Token
            request.Headers.Add("Authorization", "Bearer " + "4qBCGsP9XuVZZNvp5f8dH06A9UJtAqXSMUpUP42PaKP");


            string networkUrl = uploadedFileDB.FilePathName.Replace(@"\\192.168.6.250\PNote", "http://192.168.6.250/PNote").Replace(@"\", "/");

            // เตรียมข้อมูลที่จะส่งไปยัง API
            var postData = string.Format("message={0}",

                "\r\n" + "_________" + "\r\n" +
                "\r\n" + "PNOTE-UploadfileToSystem" + "\r\n"
                + "_________" + "\r\n"
                + "BrandName : " + uploadedFileDB.BrandName + "\r\n"
                + "Season : " + uploadedFileDB.Season + "\r\n"
                + "StyleName : " + uploadedFileDB.StyleName + "\r\n"
                + "OrderNo : " + uploadedFileDB.OrderNo + "\r\n"
                + "TypeName : " + uploadedFileDB.TypeName + "\r\n"
                + "FileName : " + uploadedFileDB.NewFileName + "\r\n"
            //+ "UrlDownload : " + uploadedFileDB.FilePathName + "\r\n"
            //+ "ไฟล์ Packing:" + "[คลิกที่นี่](http://192.168.6.250/PNote/PNOTE-DEV/MCJ/25SS/12JABC30/PACKING/PK-12JABC30-YPSO-24-1771.xlsx) " + "\r\n"
                + "Status : " + "complete" + "\r\n"
                + "CreateBy : " + uploadedFileDB.CreateBy + "\r\n"
                + "CreateDate : " + uploadedFileDB.CreateDate + "\r\n"
                );
 


            byte[] data = Encoding.UTF8.GetBytes(postData);

            // ใส่ข้อมูลลงใน request body
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                // รับการตอบกลับจาก API
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine("Response from LINE Notify: " + result);
                    }
                }
            }
            catch (WebException ex)
            {
                // ดักจับข้อผิดพลาดและแสดงรายละเอียด
                Console.WriteLine("Error: " + ex.Message);
                if (ex.Response != null)
                {
                    using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string errorResponse = reader.ReadToEnd();
                        Console.WriteLine("Error Response: " + errorResponse);
                    }
                }
            }


            return Json(new { message = "uploaded successfully.", GetFinishdata });
        }

        public ActionResult Download(int Id)
        {
            var fileDetail = _uow.YPTGUploadfileDatas.GetAll().Where(x => x.Id == Id).FirstOrDefault();
            if (fileDetail == null)
            {
                return HttpNotFound();
            }
            var filePath = Path.Combine(fileDetail.FilePathUrl, fileDetail.NewFileName);
            return File(filePath, "application/octet-stream", fileDetail.NewFileName);
        }


        public ActionResult OpenfileId(int fileIds)
        {
            var fileDetail = _uow.YPTGUploadfileDatas.GetAll().Where(x => x.Id == fileIds).FirstOrDefault();
            return new JsonNetResult(fileDetail);
        }



        public ActionResult checkfilealready(YPTGUploadfileData DataAlready)
        {
            if(DataAlready.OrderNo == null)
            {

                var getlist = _uow.YPTGUploadfileDatas.GetAll().Where(x => x.TypeName == DataAlready.TypeName && x.BrandCode == DataAlready.BrandCode &&
                x.Season == DataAlready.Season && x.StyleName == DataAlready.StyleName  && x.Status == 1).FirstOrDefault();
                return new JsonNetResult(getlist);
            }else
            {

                var getlist = _uow.YPTGUploadfileDatas.GetAll().Where(x => x.TypeName == DataAlready.TypeName && x.BrandCode == DataAlready.BrandCode &&
                x.Season == DataAlready.Season && x.StyleName == DataAlready.StyleName && x.OrderNo == DataAlready.OrderNo && x.Status == 1).FirstOrDefault();
                return new JsonNetResult(getlist);
            }


        }


        //MASTER DATA UPDATE & UPLOAD
        public ActionResult GetMasterCustomers()
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().GroupBy(p => p.BrandCode).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }

        public ActionResult GetMasterNameCustomers(string getBandname)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == getBandname).FirstOrDefault();
            return new JsonNetResult(query);
        }

        public ActionResult GetUpdateSeason(string CodeName)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CodeName).GroupBy(p => p.Season).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }
        public ActionResult GetUpateMasterStyle(string CodeName, string SeasonName)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CodeName && z.Season == SeasonName).GroupBy(p => p.StyleName).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }
         public ActionResult GetUpdateMasterOrder(string UpdateCodeName, string UpdateSeasonName, string UpdateStyleName)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == UpdateCodeName && z.Season == UpdateSeasonName && z.StyleName == UpdateStyleName).GroupBy(p => p.OrderNo).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }

        public ActionResult GetUpdateMasterTypeName(string UpdateCodeName, string UpdateSeasonName, string UpdateStyleName , string UpdateOrderNo)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == UpdateCodeName && z.Season == UpdateSeasonName && z.StyleName == UpdateStyleName && z.OrderNo == UpdateOrderNo).GroupBy(p => p.TypeName).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }

        public ActionResult Getdataupdate(string UpdateCodeName, string UpdateSeasonName, string UpdateStyleName, string UpdateOrderNo , string UpdateTypename)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == UpdateCodeName && z.Season == UpdateSeasonName && z.StyleName == UpdateStyleName && z.OrderNo == UpdateOrderNo && z.TypeName == UpdateTypename && z.Status == 1).ToList();
            return new JsonNetResult(query);
        }



        public ActionResult DeldataupdateToLog(YPTGUploadfileDataLog deldatas)
        {
            var getdatamain = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.Id == deldatas.Id).FirstOrDefault();
            var sourceFilePath = deldatas.FilePathName;
            var uploadFolderPath = deldatas.FilePathUrl + "\\" + "MoveRevision"; 

            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            string destinationFilePath = Path.Combine(uploadFolderPath, deldatas.NewFileName);

            // ตรวจสอบว่าไฟล์ต้นทางมีอยู่หรือไม่
            if (System.IO.File.Exists(sourceFilePath))
            {
                System.IO.File.Move(sourceFilePath, destinationFilePath);
            }
            else
            {
                return HttpNotFound("ไม่พบไฟล์ต้นทาง");
            }

            //MoveDatatolog
            deldatas.LogId = deldatas.Id;
            _uow.YPTGUploadfileDataLogs.Add(deldatas);
            _uow.Commit();

            //Updatedatafromtable
            getdatamain.Status = 2;
            _uow.YPTGUploadfileDatas.Update(getdatamain);
            _uow.Commit();

            var getlistdata = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.Id == getdatamain.Id && z.Status == 1).FirstOrDefault();
            return new JsonNetResult(getlistdata);
        }


        [HttpPost]
        public ActionResult UploadUpdatesFiles(IEnumerable<HttpPostedFileBase> files, string brandCode, string brandName, string season, string styleName, string OrderNo, string TypeName, int Id)
        {
            var getuser = _uow.YMTGUsers.GetAll().Where(z => z.Id == Id).FirstOrDefault();
            var getdataupload = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == brandCode && z.Season == season && z.StyleName == styleName && z.OrderNo == OrderNo && z.TypeName == TypeName
            && z.Status == 2).FirstOrDefault();

            var filePath = "";

            var DateRevision = DateTime.Now.ToString("dd-MM-yy");

            var RevisionName = $"REV{(string.IsNullOrEmpty(getdataupload.Revision) ? 1 : int.Parse(getdataupload.Revision.Substring(3)) + 1):D2}";

            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var originalFileName = Path.GetFileName(file.FileName);
                    var renamedFileName = "";
                    string typePrefix = string.Empty;

                    switch (TypeName)
                    {
                        case "MRP":
                            typePrefix = "MRP";
                            break;
                        case "PACKING":
                            typePrefix = "PK";
                            break;
                        case "SIZE BREAK DOWN":
                            typePrefix = "SB";
                            break;
                        case "SPEC SHEET":
                            typePrefix = "SP";
                            break;
                        case "SWATCH CARD":
                            typePrefix = "SC";
                            break;
                        case "P-NOTED":
                            typePrefix = string.Empty; 
                            break;
                    }

                    if (!string.IsNullOrEmpty(typePrefix) || TypeName == "P-NOTED")
                    {
                        if (TypeName == "P-NOTED")
                        {
                            renamedFileName = $"{styleName}-{OrderNo}-{RevisionName}-{DateRevision}{Path.GetExtension(originalFileName)}";
                        }else
                        {
                            renamedFileName = $"{typePrefix}-{styleName}-{OrderNo}-{RevisionName}-{DateRevision}{Path.GetExtension(originalFileName)}";
                        }

                        filePath = Path.Combine(getdataupload.FilePathUrl, renamedFileName);
                        file.SaveAs(filePath);
                    }

                    getdataupload.Status = 1;
                    getdataupload.Revision = RevisionName;
                    getdataupload.RevisionDate = DateRevision;
                    getdataupload.CreateDate = DateTime.Now;
                    getdataupload.CreateBy = getuser.YPTName;
                    getdataupload.NewFileName = renamedFileName;
                    getdataupload.FilePathName = getdataupload.FilePathUrl + "\\" + renamedFileName;

                    _uow.YPTGUploadfileDatas.Update(getdataupload);
                    _uow.Commit();
                }
            }
            var Getdatatolist = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.Id == getdataupload.Id && z.Status == 1).ToList();
            return new JsonNetResult(Getdatatolist);
        }
        public ActionResult GetListCustomerSearchs(string CustomerSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs).ToList();
            return new JsonNetResult(query);
        }  
        public ActionResult GetSearchSeason(string CustomerSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs).GroupBy(p => p.Season).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }
        public ActionResult GetSearchMasterStyle(string CustomerSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs).GroupBy(p => p.StyleName).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }
        public ActionResult ListSearchStylesName(string CustomerSearchs, string SeasonSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs && z.Season == SeasonSearchs).ToList();
            return new JsonNetResult(query);
        }
        public ActionResult MenuSearchMasterStyle(string CustomerSearchs, string SeasonSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs && z.Season == SeasonSearchs).GroupBy(p => p.StyleName).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }
        public ActionResult ListSearchOrderNo(string CustomerSearchs, string SeasonSearchs , string StyleNameSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs && z.Season == SeasonSearchs && z.StyleName == StyleNameSearchs).ToList();
            return new JsonNetResult(query);
        }
        public ActionResult MenuSearchOrderNo(string CustomerSearchs, string SeasonSearchs, string StyleNameSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs && z.Season == SeasonSearchs && z.StyleName == StyleNameSearchs).GroupBy(p => p.OrderNo).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }
        public ActionResult ListSearchTypename(string CustomerSearchs, string SeasonSearchs, string StyleNameSearchs , string OrderNoSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs 
            && z.Season == SeasonSearchs && z.StyleName == StyleNameSearchs && z.OrderNo == OrderNoSearchs).ToList();
            return new JsonNetResult(query);
        }
       public ActionResult MenuSearchTypeName(string CustomerSearchs, string SeasonSearchs, string StyleNameSearchs, string OrderNoSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs && z.Season == SeasonSearchs
            && z.StyleName == StyleNameSearchs && z.OrderNo == OrderNoSearchs).GroupBy(p => p.TypeName).ToList();
            var querys = query.Select(g => g.Key).ToList();
            return new JsonNetResult(querys);
        }
        public ActionResult ListSearchdataupdate(string CustomerSearchs, string SeasonSearchs, string StyleNameSearchs, string OrderNoSearchs , string TypeNameSearchs)
        {
            var query = _uow.YPTGUploadfileDatas.GetAll().Where(z => z.BrandCode == CustomerSearchs
            && z.Season == SeasonSearchs && z.StyleName == StyleNameSearchs && z.OrderNo == OrderNoSearchs && z.TypeName == TypeNameSearchs).ToList();
            return new JsonNetResult(query);
        }
        [HttpPost]
        public ActionResult SendEmail(string recipient, string subject, string body)
        {
            var smtpServer = "mail.yehpattana.com";
            var smtpPort = 587; // หรือ 465 สำหรับ SSL
            var username = "automail@yehpattana.com";
            var password = "@1234a";
            var from = "automail@yehpattana.com";

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(from);
                message.To.Add(recipient);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.Credentials = new NetworkCredential(username, password);
                    client.EnableSsl = false; // ปิดการใช้ SSL หากเซิร์ฟเวอร์ไม่รองรับ

                    try
                    {
                        client.Send(message);
                        return Json(new { success = true, message = "Email sent successfully" });
                    }
                    catch (SmtpException smtpEx)
                    {
                        return Json(new { success = false, message = $"SMTP Error: {smtpEx.StatusCode} - {smtpEx.Message}" });
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, message = $"Error sending email: {ex.Message}" });
                    }
                }
            }
        }






        //Get Quotation file by order num
        [HttpPost]
        public ActionResult GetDataQuoFileTables(string OrderNumber)
        {
            if (string.IsNullOrEmpty(OrderNumber))
            {
                var FileBad = "Order number is required.";

                return new JsonNetResult(FileBad);
            }

            //ËÒ file Quotation

            var QuoNum = _uow.YmtgOrderNdss.GetAll().Where(z => z.OrderNumber == OrderNumber).FirstOrDefault();
            string QuoNumStr;
            if (QuoNum == null)
            {
                QuoNumStr = "";

            }
            else
            {
                QuoNumStr = QuoNum.QuotationNumber.ToString();
            }

            var fileQuos = new List<QuotationFile>();

            if (!string.IsNullOrEmpty(QuoNumStr))
            {

                fileQuos = _uow.QuotationFiles.GetAll()
                        .Where(f => f.QuotationNumber == QuoNumStr).ToList();
                //.Select(f => new
                //{
                //    f.QuotationNumber,
                //    f.Id,
                //    f.FileName,
                //    f.FilePath,
                //    f.FileDescription,
                //    CreatedAt = f.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")
                //})
                // .ToList<object>();

            }

            if (!fileQuos.Any())
            {
                return new JsonNetResult("");
            }

            return new JsonNetResult(fileQuos);

        }





        public ActionResult NdsSystemViewPage(string quotationNumber , int EmpNo)
        {
            // ViewPage read only
            var model = new UserQuo
            {
                QuotationNumber = quotationNumber,
                EmpNo = EmpNo
            };

            return View(model);
        }




        //Order Information

 

        [HttpGet]
        public JsonResult GetOrderInfos()
        {

            var orderData = _uow.YmtgOrderModels.GetAll()
                   .Select(o => new
                   {
                       o.OrderNumber,
                       o.OrderDate,
                       o.ShipDate,
                       o.TotalQty,
                       o.OrderStatus,
                       o.CustomerName
                   })
                .OrderByDescending(o => o.OrderNumber)
                .ToList();
            return Json(new { data = orderData }, JsonRequestBehavior.AllowGet);

        }


        
        public ActionResult NdsSystemOrderInformation(int EmpNo)
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
                    return View(EmpNo);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }

        [HttpGet]
        public ActionResult GetOrderDetails(string orderNumber)
        {
            var order = _uow.YmtgOrderModels.GetAll()
                .Where(o => o.OrderNumber == orderNumber).FirstOrDefault();
            return new JsonNetResult(order);
        }

        [HttpGet]
        public ActionResult GetProductOrders(string orderNumber)
        {
            var productOrder = _uow.ProductModels.GetAll()
                .Where(o => o.OrderNumber == orderNumber).ToList();

            return new JsonNetResult(productOrder);
        }




        public ActionResult NdsSystemViewAttachments(string orderNumber, int EmpNo)
        {
            // ViewPage read only
            var model = new UserQuo
            {
                QuotationNumber = orderNumber,
                EmpNo = EmpNo
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult GetDataOtherFileTable(string orderNumber)

        {
            //if (string.IsNullOrEmpty(orderNumber))
            //{
            //    //return BadRequest("Order number is required.");
            //}

            //ËÒ file Quotation



            var fileOther = _uow.AttachmentsModels.GetAll()
                .Where(f => f.OrderNumber == orderNumber).ToList();
            //.Select(f => new
            //{

            //    f.Id,
            //    f.FileName,
            //    f.FilePath,
            //    f.FileDescription,
            //    CreatedAt = f.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")
            //})


            //return Ok(fileOther);
            return new JsonNetResult(fileOther);
        }


        //[HttpPost]
        //public ActionResult UploadFileAboutOrders(HttpPostedFileBase file , string fileDescription, string orderNumber)
        //{
        //    if (file == null || file.ContentLength == 0)
        //    {
        //        var errorfile = "Invalid file.";
        //        return new JsonNetResult(errorfile);
        //    }

        //    if (string.IsNullOrEmpty(orderNumber))
        //    {
        //        var errorfiles = "Order number is required.";
        //        return new JsonNetResult(errorfiles);
        //    }

        //    if (string.IsNullOrEmpty(fileDescription))
        //    {
        //        fileDescription = "";
        //    }

        //    var otherfileFolderPath = Path.Combine(_uploadPath, "Otherfile", orderNumber);
        //    if (!Directory.Exists(otherfileFolderPath))
        //    {
        //        Directory.CreateDirectory(otherfileFolderPath);
        //    }

        //    var fileName = Path.GetFileName(file.FileName);
        //    var filePath = Path.Combine(otherfileFolderPath, fileName);

        //    int counter = 1;
        //    while (System.IO.File.Exists(filePath))
        //    {
        //        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        //        var fileExtension = Path.GetExtension(fileName);
        //        fileName = $"{fileNameWithoutExtension}_{counter++}{fileExtension}";
        //        filePath = Path.Combine(otherfileFolderPath, fileName);
        //    }

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //    }

        //    var newFile = new AttachmentsModel
        //    {
        //        OrderNumber = orderNumber,
        //        FileName = fileName,
        //        FilePath = Path.Combine("Uploads", "Otherfile", orderNumber, fileName), // à¡çº Path áºº Relative
        //        FileDescription = fileDescription,
        //        CreatedAt = DateTime.Now,
        //        AddFileBy = "ADMIN"
        //    };

        //    _uow.AttachmentsModels.Add(newFile);
        //    _uow.Commit();

        //    return new JsonNetResult(newFile);

        //}


        [HttpPost]
        public ActionResult UploadFileAboutOrders(HttpPostedFileBase file, string fileDescription, string orderNumber)
        {
            if (file == null || file.ContentLength == 0)
            {
                return Json(new { success = false, message = "Invalid file." }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(orderNumber))
            {
                return Json(new { success = false, message = "Order number is required." }, JsonRequestBehavior.AllowGet);
            }

            // Assign empty string if fileDescription is null or empty
            fileDescription = fileDescription ?? string.Empty;

            // Construct the folder path to save the file
            var otherfileFolderPath = Path.Combine(Server.MapPath("~/Uploads/Otherfile"), orderNumber);
            if (!Directory.Exists(otherfileFolderPath))
            {
                Directory.CreateDirectory(otherfileFolderPath);
            }

            // Get file name and ensure no duplicate file names
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(otherfileFolderPath, fileName);

            int counter = 1;
            while (System.IO.File.Exists(filePath))
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                var fileExtension = Path.GetExtension(fileName);
                fileName = $"{fileNameWithoutExtension}_{counter++}{fileExtension}";
                filePath = Path.Combine(otherfileFolderPath, fileName);
            }

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.InputStream.CopyTo(stream);
            }

            // Save file information to the database
            var newFile = new AttachmentsModel
            {
                OrderNumber = orderNumber,
                FileName = fileName,
                FilePath = $"/Uploads/Otherfile/{orderNumber}/{fileName}", // Relative path for database
                FileDescription = fileDescription,
                CreatedAt = DateTime.Now,
                AddFileBy = "ADMIN"
            };

            // Save to database (ensure _uow is properly configured and used)
            _uow.AttachmentsModels.Add(newFile);
            _uow.Commit();

            // Return success response
            return Json(new { success = true, data = newFile }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult RFIDIndex(int EmpNo)
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
                    return View(EmpNo);
                }
                else
                {
                    return RedirectToAction("Home", "Home", new { EmpNo = EmpNo });
                }
            }
        }




        public ActionResult GetTags()
        {
            // ประกาศ List เพื่อเก็บผลลัพธ์ทั้งหมด
            List<Product> allData = new List<Product>();



            // วนลูปผ่าน TagsRFID
            var TagsRFID = _uow.RFIDTags.GetAll().ToList();
            foreach (var d in TagsRFID)
            {
                // ดึงข้อมูลจาก Products ตามเงื่อนไข และเพิ่มลงใน allData
                var data = _uow.Products.GetAll().Where(z => z.RFIDData == d.EPC).ToList();
                allData.AddRange(data);
            }

            return Json(allData, JsonRequestBehavior.AllowGet);

        }




        public void StartReading()
        {
            if (RFIDReader.CreateTcpConn(IPConfig, this))
            {
                RFIDReader._Tag6C.GetEPC(IPConfig, eAntennaNo._1 | eAntennaNo._2 | eAntennaNo._3 | eAntennaNo._4, eReadType.Inventory);
            }
        }


        public void ReStartReading()
        {

            var delRFID = _uow.RFIDTags.GetAll().ToList(); // ดึงข้อมูลทั้งหมดจากตาราง RFIDTags

            foreach (var d in delRFID)
            {
                _uow.RFIDTags.Delete(d.Id);
                _uow.Commit();
            }

            if (RFIDReader.CreateTcpConn(IPConfig, this))
            {
                RFIDReader._Tag6C.GetEPC(IPConfig, eAntennaNo._1 | eAntennaNo._2 | eAntennaNo._3 | eAntennaNo._4, eReadType.Inventory);
            }


        }

        public void StopReading()
        {
            RFIDReader._RFIDConfig.Stop(IPConfig);
            RFIDReader.CloseConn(IPConfig);

            var delRFID = _uow.RFIDTags.GetAll().ToList(); // ดึงข้อมูลทั้งหมดจากตาราง RFIDTags

            foreach (var d in delRFID)
            {
                _uow.RFIDTags.Delete(d.Id);
                _uow.Commit();
            }

        }

        public void OutPutTags(Tag_Model tag)
        {

            var newTag = new RFIDTag
            {
                EPC = tag.EPC,
                ReadTime = DateTime.Now,
                IsActive = 1
            };



            var checkdata = _uow.RFIDTags.GetAll().Where(t => t.EPC == tag.EPC).FirstOrDefault();
            if (checkdata == null)
            {
                _uow.RFIDTags.Add(newTag);
                _uow.Commit();
                displayedEpcs.Add(tag.EPC);
            }
            else
            {
                displayedEpcs.Add(tag.EPC);
            }

        }



        public void EventUpload(CallBackEnum type, object param) { }
        public void GPIControlMsg(GPI_Model gpi_model) { }
        public void OutPutTagsOver() { }
        public void PortClosing(string connID) { }
        public void PortConnecting(string connID) { }
        public void WriteDebugMsg(string msg) { }
        public void WriteLog(string msg) { }


        public ActionResult GenerateQRCode(int paymentId)
        {
            // ดึงข้อมูลการชำระเงินจากฐานข้อมูล
            var payment = _uow.Payments.GetAll().FirstOrDefault(p => p.Id == paymentId);

            if (payment == null)
            {
                return new HttpStatusCodeResult(404, "Payment not found");
            }

            // สร้างข้อมูลที่ต้องการใส่ใน QR Code
            var qrCodeContent = $"{payment.PaymentReference}|{payment.Amount}|{payment.Id}";  // เปลี่ยนชื่อเป็น qrCodeContent

            // ใช้ QRCoder สร้าง QR Code
            using (var qrGenerator = new QRCodeGenerator())
            {
                // สร้าง QR Code จากข้อมูล
                var qrCodeData = qrGenerator.CreateQrCode(qrCodeContent, QRCodeGenerator.ECCLevel.Q);  // ใช้ qrCodeContent แทน qrCodeData
                var qrCode = new QRCode(qrCodeData);

                // สร้าง MemoryStream เพื่อแปลง QR Code เป็นภาพ PNG
                using (var ms = new MemoryStream())
                {
                    // สร้างภาพ QR Code และบันทึกใน MemoryStream
                    qrCode.GetGraphic(20).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteArray = ms.ToArray();

                    // ส่งภาพกลับไปในรูปแบบ "image/png"
                    return File(byteArray, "image/png");
                }
            }
        }

    }
}