using System;

namespace RfqEbr.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentReference { get; set; }  // ใช้เพื่อระบุการชำระเงิน
        public decimal Amount { get; set; }           // จำนวนเงิน
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
    }
}
