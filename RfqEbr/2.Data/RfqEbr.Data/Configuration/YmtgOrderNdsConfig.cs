using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;
public class YmtgOrderNdsConfig : EntityTypeConfiguration<YmtgOrderNds>
{
    public YmtgOrderNdsConfig()
    {
        ToTable("YMTG_NDS_Order");
        HasKey(t => t.Id);
        Property(t => t.QuotationNumber)
            .IsRequired()
            .HasMaxLength(50);
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
            .HasPrecision(18, 2); 
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
        Property(t => t.QuoStatus)
            .IsRequired();
        Property(t => t.QuoType)
            .IsOptional()
            .HasMaxLength(50);
        Property(t => t.QuoLastname)
            .IsOptional()
            .HasMaxLength(50);
        Property(t => t.QuoCompanyName)
            .IsOptional()
            .HasMaxLength(100);
        Property(t => t.QuoProvince)
            .IsOptional()
            .HasMaxLength(50);
        Property(t => t.QuoDistricts)
            .IsOptional()
            .HasMaxLength(50);

        Property(t => t.QuoSubDistricts)
            .IsOptional()
            .HasMaxLength(50);

        Property(t => t.QuoZipCode)
            .IsOptional()
            .HasMaxLength(10);

        Property(t => t.QuoRemark)
            .IsOptional()
            .HasMaxLength(500);

        Property(t => t.QuoTaxID)
            .IsOptional()
            .HasMaxLength(20);

        Property(t => t.QuoLastUpdate)
            .IsOptional();

        Property(t => t.QuoShippingPrice)
            .IsRequired();

        Property(t => t.QuoCancel)
            .IsRequired();
    }
}














