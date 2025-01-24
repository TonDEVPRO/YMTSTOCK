
using System;
using System.ComponentModel.DataAnnotations;
namespace RfqEbr.Models.Table
{
    public class RFIDTag
    {
        public int Id { get; set; }
        public string EPC { get; set; }
        public DateTime ReadTime { get; set; }
        public int IsActive { get; set; }
    }
}
