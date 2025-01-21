
using System;

namespace RfqEbr.Models.Table
{
    public class YPTGUploadfileDataLog
    {
        public int Id { get; set; }
        public int LogId { get; set; }

        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string Season { get; set; }
        public string StyleName { get; set; }
        public string OrderNo { get; set; }
        public string TypeName { get; set; }
        public string FactoryCode { get; set; }
        public int FileId { get; set; }
        public string FileUploadCode { get; set; }
        public string OriginalFileName { get; set; }
        public string NewFileName { get; set; }
        public string FileExtension { get; set; }
        public string FilePathUrl { get; set; }
        public string FilePathName { get; set; }
        public string Revision { get; set; }
        public string RevisionDate { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
