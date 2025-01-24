
using RfqEbr.Models;
using RfqEbr.Models.Table;
using System;
using System.Data.Entity.ModelConfiguration;

public class ProductConfig : EntityTypeConfiguration<Product>
{
    public ProductConfig()
    {

        ToTable("Productdata");
            // Primary Key
            HasKey(p => p.ProductId);

        // ProductCode - Required, StringLength(50)
        Property(p => p.ProductCode)
            .IsRequired()
            .HasMaxLength(50);

        //  - Required, StringLength(200)
        Property(p => p.ProductName)
            .IsRequired()
            .HasMaxLength(200);

        // RFIDData - ไม่มีการกำหนดให้ต้องการ (Nullable)
        Property(p => p.RFIDData)
            .HasMaxLength(200); // (หากต้องการกำหนดความยาว)

        // CategoryId - Required
        Property(p => p.CategoryId)
            .IsRequired();

        // UnitPrice - Required, Currency (decimal)
        Property(p => p.UnitPrice)
              .IsRequired();

        // SKUCode - ไม่มีการกำหนดให้ต้องการ
      Property(p => p.SKUCode)
            .HasMaxLength(50); // ตัวอย่างการกำหนดความยาว

        // Color - ไม่มีการกำหนดให้ต้องการ
      Property(p => p.Color)
            .HasMaxLength(50);

        // Size - ไม่มีการกำหนดให้ต้องการ
      Property(p => p.Size)
            .HasMaxLength(50);

        // QuantityInStock - Required
      Property(p => p.QuantityInStock)
            .IsRequired();

        // IsActive - Required
      Property(p => p.IsActive)
            .IsRequired();

        // Status - Required
      Property(p => p.Status)
            .IsRequired();

        // Remark - ไม่มีการกำหนดให้ต้องการ
      Property(p => p.Remark)
            .HasMaxLength(500);

        // CreateDate - DateTime (กำหนดให้ใช้ DataType DateTime)
      Property(p => p.CreateDate)
            .HasColumnType("datetime");
        // UpdateDate - DateTime (กำหนดให้ใช้ DataType DateTime)
      Property(p => p.UpdateDate)
            .HasColumnType("datetime");
 

    }
}














