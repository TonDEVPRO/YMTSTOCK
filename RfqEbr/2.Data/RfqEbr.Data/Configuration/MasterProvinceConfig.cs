
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class MasterProvinceConfig : EntityTypeConfiguration<MasterProvince>
{
    public MasterProvinceConfig()
    {
        // Table Name
        ToTable("YMTG_MasterProvinces");

        // Primary Key
        HasKey(t => t.Id);

        // Properties
        Property(t => t.Id)
            .IsRequired();

        Property(t => t.Provinces)
            .IsRequired()
            .HasMaxLength(200);

        Property(t => t.Districts)
          .IsRequired()
          .HasMaxLength(200);

        Property(t => t.SubDistricts)
            .IsRequired()
            .HasMaxLength(200);

        Property(t => t.ZipCode)
            .IsRequired()
            .HasMaxLength(200);


    }
}














