using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RfqEbr.Models
{

    [Table("YMTG_GenQuotationNumber")]
  
    public class QuotationRun
    {
        [Key] // กำหนดให้ QuotationNumber เป็น Primary Key
        public string QuotationNumber { get; set; }
    }
}
