
using System;

namespace RfqEbr.Models.Table
{
    public class YMTGOrderDetail
    {
        public string Division { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Season { get; set; }
        public string Style { get; set; }
        public string OrderCode { get; set; }
        
        public string OrderNo { get; set; }
        public string Cur { get; set; }
        public string Status { get; set; }
        public int OrderQty { get; set; }  // Assuming OrderQty is an integer
        public DateTime IssDate { get; set; }  // Assuming IssDate is a date
        //public DateTime ShipDate { get; set; }  // Assuming ShipDate is a date
        //public DateTime StatusDate { get; set; }  // Assuming StatusDate is a date
        public string CustPORef { get; set; }
    }
}
