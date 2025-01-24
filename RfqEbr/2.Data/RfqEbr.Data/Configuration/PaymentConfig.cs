
using RfqEbr.Models;
using RfqEbr.Models.Table;
using System;
using System.Data.Entity.ModelConfiguration;
using static iTextSharp.text.pdf.events.IndexEvents;

public class PaymentConfig : EntityTypeConfiguration<Payment>
{
    public PaymentConfig()
    {

        ToTable("Payments");
        // Primary Key

        HasKey(p => p.Id);

        // กำหนดการตั้งค่าของแต่ละ property
        Property(p => p.PaymentReference)
              .HasMaxLength(255)
              .IsRequired(); // ถ้าต้องการให้ไม่เป็น null

        Property(p => p.Amount);  // กำหนดว่า Amount ต้องไม่เป็น null

        Property(p => p.Date)
              .HasColumnType("DATETIME")
              .IsRequired();

        Property(p => p.IsPaid)
              .HasColumnType("BIT")
              .IsRequired();

    }
}














