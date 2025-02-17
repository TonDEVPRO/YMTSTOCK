using System;
using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.IT;
using RfqEbr.Models.IT.StockDeviceIT;
using RfqEbr.Models.IT.StockDeviceITInLocation;

public class MasterLocationConfig : EntityTypeConfiguration<MasterLocation>
{
    public MasterLocationConfig()
    {
        ToTable("YMTG_Master_Location");

        HasKey(s => s.LocationCode);
        Property(s => s.Location)
            .HasMaxLength(20);
    }
}