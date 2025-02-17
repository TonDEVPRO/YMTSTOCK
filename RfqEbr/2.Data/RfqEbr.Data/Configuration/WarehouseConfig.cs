
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class WarehouseConfig : EntityTypeConfiguration<Warehouse>
{
    public WarehouseConfig()
    {
        // Table Name
        ToTable("YMTG_NDS_Warehouse");

        HasKey(t => t.WarehouseID);
        Property(t => t.WarehouseID)
            .IsRequired();
        Property(w => w.WarehouseName)
                       .IsRequired()
                       .HasMaxLength(255);
        Property(w => w.Location)
           .HasMaxLength(255);
        Property(w => w.Remark)
               .HasMaxLength(255);
        Property(w => w.Status)
               .IsRequired();
        Property(w => w.CreatedAt);
        Property(w => w.UpdatedAt);
        Property(w => w.CreatedBy)
               .HasMaxLength(255);
        Property(w => w.UpdatedBy)
               .HasMaxLength(255);
    }
}














