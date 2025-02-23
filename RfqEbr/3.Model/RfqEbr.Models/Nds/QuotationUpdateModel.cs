﻿using System;
using System.Collections.Generic;

namespace RfqEbr.Models
{
    public class QuotationUpdateModel
    {
        public string QuotationNumber { get; set; }
        public string CustomerName { get; set; }

        public string QuoLastname { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalPrice { get; set; }
        public string Remark { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string QuoProvince { get; set; }
        public string QuoDistricts { get; set; }
        public string QuoSubDistricts { get; set; }
        public string QuoZipCode { get; set; }

        public string QuoCompanyName { get; set; }

        public string QuoTaxID { get; set; }

        public string CustomerEmail { get; set; }

        public string QuoType { get; set; }

        public int QuoShippingPrice { get; set; } = 0;

        public int QuoStatus { get; set; } = 0;





        //QuoLastUpdate
        public DateTime? QuoLastUpdate { get; set; } = DateTime.Now;


        public List<ProductUpdateModel> Entries { get; set; }
    }
}
