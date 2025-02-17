using System;
using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.IT;
using RfqEbr.Models.IT.StockDeviceIT;
using RfqEbr.Models.IT.StockDeviceITInLocation;

public class MasterSupplierConfig : EntityTypeConfiguration<MasterSupplier>
{
    public MasterSupplierConfig()
    {
        ToTable("YMTG_Master_Supplier");

        HasKey(s => s.SupID);
        Property(s => s.SupName)
            .HasMaxLength(50);
    }
}