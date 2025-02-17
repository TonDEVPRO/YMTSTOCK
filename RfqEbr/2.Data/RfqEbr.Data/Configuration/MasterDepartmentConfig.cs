using System;
using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.IT;
using RfqEbr.Models.IT.StockDeviceIT;
using RfqEbr.Models.IT.StockDeviceITInLocation;

public class MasterDepartmentConfig : EntityTypeConfiguration<MasterDepartment>
{
    public MasterDepartmentConfig()
    {
        ToTable("YMTG_Master_Department");

        HasKey(s => s.Department);
        Property(s => s.Department)
            .HasMaxLength(50);        
    }
}