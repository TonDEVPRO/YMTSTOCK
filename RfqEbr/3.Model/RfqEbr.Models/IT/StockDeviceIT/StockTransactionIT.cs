using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RfqEbr.Models.IT.StockDeviceIT
{
    public class StockTransactionIT
    {
        public string DocNo { get; set; }
        public string Code { get; set; }
        public string LocationCode { get; set; } = string.Empty;
        public DateTime TrxDate { get; set; } = DateTime.Now.Date;
        public string TrxType { get; set; } = string.Empty;
        public string TrxName { get; set; } = string.Empty;
        public string Supplier { get; set; } = string.Empty;
        public string TrxDoc { get; set; } = string.Empty;
        public string PoNo { get; set; } = string.Empty;
        public string DeptOut { get; set; } = string.Empty;
        public int? DeviceId { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int Qty { get; set; } = 0;
        public string CreateBy { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string EditBy { get; set; }
        public DateTime EditDate { get; set; } = DateTime.UtcNow;
    }
}
