using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RfqEbr.Models.IT.StockDeviceITInLocation
{
    public class StockDeviceITInQty
    {
        public string Code { get; set; }
        public string LocationCode { get; set; }
        public int Qty { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string Editby { get; set; }
        public DateTime EditDate { get; set; } = DateTime.UtcNow;
    }
}
