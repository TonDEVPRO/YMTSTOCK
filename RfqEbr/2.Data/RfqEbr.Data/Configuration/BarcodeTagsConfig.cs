
using RfqEbr.Models;
using RfqEbr.Models.Table;
using System;
using System.Data.Entity.ModelConfiguration;

public class BarcodeTagsConfig : EntityTypeConfiguration<BarcodeTags>
{
    public BarcodeTagsConfig()
    {

        ToTable("BarcodeTags");

        HasKey(e => e.Id);

        Property(e => e.Id)
             .HasColumnName("Id");

        Property(e => e.BarcodeName)
             .HasColumnName("BarcodeName");

        Property(e => e.ProductCode)
             .HasColumnName("ProductCode");

        Property(e => e.IsActive)
             .HasColumnName("IsActive");
 
    }
}














