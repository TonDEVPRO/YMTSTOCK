
using System;
using System.ComponentModel.DataAnnotations;
namespace RfqEbr.Models.Table
{
    public class BarcodeTags
    {
        public int Id { get; set; }
        public string BarcodeName { get; set; }
        public string ProductCode { get; set; }
        public int IsActive { get; set; }
    }
}
