using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RfqEbr.Models.IT.StockDeviceIT
{
    public class StockDeviceIT
    {
        public string Code { get; set; }
        public string SpareType { get; set; }
        public string ItemName { get; set; }
        public int Maximum { get; set; }
        public int Minimum { get; set; }
        public string Unit { get; set; }
        public int QtyByStock { get; set; }
        public int QtyBalance { get; set; }
        public string Remark { get; set; }
        public string RemarkCancel { get; set; }
        public int Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string EditBy { get; set; }
        public DateTime EditDate { get; set; } = DateTime.UtcNow;
    }
}
