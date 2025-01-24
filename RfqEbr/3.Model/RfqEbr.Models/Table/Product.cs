
using System;
using System.ComponentModel.DataAnnotations;
namespace RfqEbr.Models.Table
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string RFIDData { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }

        public string SKUCode { get; set; }
        public string Color { get; set; }

        public string Size { get; set; }

        public int QuantityInStock { get; set; }

        public bool IsActive { get; set; }

        public int Status { get; set; }

        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
