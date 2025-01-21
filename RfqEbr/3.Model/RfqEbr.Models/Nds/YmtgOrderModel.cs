using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RfqEbr.Models
{
    public class YmtgOrderModel
    {
        public int Id { get; set; } = 0;
        public string OrderNumber { get; set; } = "";
        public DateTime OrderDate { get; set; } = DateTime.Now.Date;
        public string OrderStatus { get; set; } = "";
        public DateTime? ShipDate { get; set; } = DateTime.Now.Date;
        public int TotalQty { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;
        public string CustomerName { get; set; } = "";
        public string CustomerEmail { get; set; } = "";
        public string CustomerAddress { get; set; } = "";
        public string CustomerAddressTax { get; set; } = "";
        public string CustomerPhone { get; set; } = "";
        public string Remark { get; set; } = "";
        public string CreateBy { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now.Date;
    }

}
