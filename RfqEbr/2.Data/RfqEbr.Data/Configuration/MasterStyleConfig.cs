
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class MasterStyleConfig : EntityTypeConfiguration<MasterStyle>
{
    public MasterStyleConfig()
    {
        // Table Name
        // กำหนดชื่อ Table
        ToTable("YMTG_MasterStyle");

        // Mapping Properties
        HasKey(e => e.Id);

        Property(e => e.Style)
            .IsRequired()
            .HasMaxLength(255);

        Property(e => e.StyleCode)
            .HasMaxLength(50);

        Property(e => e.SKUCode)
            .HasMaxLength(50);

        Property(e => e.Brand)
            .HasMaxLength(100);

        Property(e => e.StyleType)
            .HasMaxLength(100);

        Property(e => e.TypeName)
            .HasMaxLength(100);

        Property(e => e.FabricType)
            .HasMaxLength(100);

        Property(e => e.FabricDesc)
            .HasMaxLength(255);

        Property(e => e.PatternCode)
            .HasMaxLength(50);

        Property(e => e.PatternDetail)
            .HasMaxLength(255);

        Property(e => e.UpdateBy)
            .HasMaxLength(50);

        Property(e => e.LastUpdate)
            .IsRequired()
            .HasColumnType("datetime");

        Property(e => e.Description)
            .HasMaxLength(500);

        Property(e => e.FullDesc)
            .HasMaxLength(1000);

        Property(e => e.SysOwner)
            .HasMaxLength(50);

        Property(e => e.SysCreateDate)
            .IsRequired()
            .HasColumnType("datetime");

        Property(e => e.Gender)
            .HasMaxLength(50);

        Property(e => e.FabricSpec)
            .HasMaxLength(255);

        Property(e => e.SpecialTechnical)
            .HasMaxLength(255);
    }
}














