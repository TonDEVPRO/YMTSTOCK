﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RfqEbr.Models
{
    public class YmtgProductNds
    {
        public int Id { get; set; } 

        public string QuotationNumber { get; set; } 
        public string OrderNumber { get; set; } 

        public int ProductId { get; set; } 

        public string ProductName { get; set; } 

        public int Qty { get; set; } 

        public string SKUCode { get; set; } 
        public string SKUCodeFull { get; set; } 

        public string Size { get; set; } 

        public string Color { get; set; } 

        public decimal Price { get; set; }

        public string ScreenShots1 { get; set; } = "";
        public string Url1 { get; set; } = "";
        public string SizeScreen1_W { get; set; } = "";
        public string SizeScreen1_H { get; set; } = "";
        public string Topdistance1_Y { get; set; } = "";
        public string Topdistance1_X { get; set; } = "";
        public string ScreenShots2 { get; set; } = "";
        public string Url2 { get; set; } = "";
        public string SizeScreen2_W { get; set; } = "";
        public string SizeScreen2_H { get; set; } = "";
        public string Topdistance2_Y { get; set; } = "";
        public string Topdistance2_X { get; set; } = "";
        public string ScreenShots3 { get; set; } = "";
        public string Url3 { get; set; } = "";
        public string SizeScreen3_W { get; set; } = "";
        public string SizeScreen3_H { get; set; } = "";
        public string Topdistance3_Y { get; set; } = "";
        public string Topdistance3_X { get; set; } = "";
        public string ScreenShots4 { get; set; } = "";
        public string Url4 { get; set; } = "";
        public string SizeScreen4_W { get; set; } = "";
        public string SizeScreen4_H { get; set; } = "";
        public string Topdistance4_Y { get; set; } = "";
        public string Topdistance4_X { get; set; } = "";

        public int PrintingType { get; set; }

        public string Remark { get; set; } = "";

        public string CreateBy { get; set; } 

        public DateTime CreateDate { get; set; }
    }
}
