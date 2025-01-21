
using System;

namespace RfqEbr.Models.Table
{
    public class YPTGUploadDetail
    {
        public int Id { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string Season { get; set; }
        public string StyleName { get; set; }
        public string OrderNo { get; set; }
        public string TypeName { get; set; }
        public string FactoryCode { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
