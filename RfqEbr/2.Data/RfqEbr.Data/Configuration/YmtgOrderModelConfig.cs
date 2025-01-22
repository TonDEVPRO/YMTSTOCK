
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class YmtgOrderModelConfig : EntityTypeConfiguration<YmtgOrderModel>
{
    public YmtgOrderModelConfig()
    {

        // Mapping Properties
        // Table Name
        ToTable("YMTG_NDS_ViewOrder");

        // Primary Key
        HasKey(t => t.Id);


        Property(t => t.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        Property(t => t.OrderDate)
            .IsRequired();

        Property(t => t.OrderStatus)
            .IsRequired()
            .HasMaxLength(50);

        Property(t => t.ShipDate)
            .IsOptional();

        Property(t => t.TotalQty)
            .IsRequired();

        Property(t => t.TotalPrice)
            .IsRequired()
            .HasPrecision(18, 2); // Adjust precision and scale as needed

        Property(t => t.CustomerName)
            .IsRequired()
            .HasMaxLength(100);

        Property(t => t.CustomerEmail)
            .IsOptional()
            .HasMaxLength(100);

        Property(t => t.CustomerAddress)
            .IsOptional()
            .HasMaxLength(200);

        Property(t => t.CustomerAddressTax)
            .IsOptional()
            .HasMaxLength(200);

        Property(t => t.CustomerPhone)
            .IsOptional()
            .HasMaxLength(20);

        Property(t => t.Remark)
            .IsOptional()
            .HasMaxLength(500);

        Property(t => t.CreateBy)
            .IsRequired()
            .HasMaxLength(50);

        Property(t => t.CreateDate)
            .IsRequired();

    
    }
}














