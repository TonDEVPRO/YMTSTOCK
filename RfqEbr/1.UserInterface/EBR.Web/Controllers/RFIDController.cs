using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFIDReaderAPI;
using RFIDReaderAPI.Models;
using RfqEbr.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Net;
using Microsoft.Ajax.Utilities;
using RfqEbr.Data.Contracts;
using RfqEbr.Models.Table;


namespace EBR.Web.Controllers
{
    public class RFIDController : Controller, RFIDReaderAPI.Interface.IAsynchronousMessage
    {
        private readonly IUnitOfWork _uow;


        string IPConfig = "192.168.1.116:9090";

        private List<string> displayedEpcs = new List<string>();

        public RFIDController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View();
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
          
            foreach(var d in delRFID)
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
    }
}