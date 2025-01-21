
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class YmtgOrderModelConfig : EntityTypeConfiguration<YmtgOrderModel>
{
    public YmtgOrderModelConfig()
    {
        // Table Name
        // กำหนดชื่อ Table
        ToTable("YMTG_NDS_QuotationFiles");

        // Mapping Properties
        


    }
}














