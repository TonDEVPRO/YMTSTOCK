
using System;

namespace RfqEbr.Models.Table
{
    public class YMTGNDSShopStock
    {
        public int Id { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public int Total { get; set; }
        public int Status { get; set; }
        public string WarehouseName { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
