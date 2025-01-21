
using System;

namespace RfqEbr.Models.Table
{
    public class YMTGNDSShopStockLog
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int Total { get; set; }
        public int AddQty { get; set; }
        public int TotalQty { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
