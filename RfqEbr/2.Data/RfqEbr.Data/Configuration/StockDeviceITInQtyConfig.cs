using System;
using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.IT.StockDeviceITInLocation;

public class StockDeviceITInQtyConfig : EntityTypeConfiguration<StockDeviceITInQty>
{
    public StockDeviceITInQtyConfig()
    {
        ToTable("YMTG_ITStockQtyInLocation");

        HasKey(s => new { s.Code, s.LocationCode });
        Property(s => s.Code);
        Property(s => s.LocationCode);
        Property(s => s.Qty);
        Property(s => s.CreateBy).HasMaxLength(50);
        Property(s => s.CreateDate);
        Property(s => s.Editby).HasMaxLength(50);
        Property(s => s.EditDate);
        //Property(s => s.SpareType)
        //      .HasMaxLength(255)
        //      .IsRequired(); // กำหนดว่า SpareType ต้องไม่เป็น null

        //Property(s => s.ItemName)
        //      .HasMaxLength(255)
        //      .IsRequired(); // กำหนดว่า ItemName ต้องไม่เป็น null

        //Property(s => s.Maximum)
        //      .IsRequired(); // กำหนดว่า Maximum ต้องไม่เป็น null

        //Property(s => s.Minimum)
        //      .IsRequired(); // กำหนดว่า Minimum ต้องไม่เป็น null

        //Property(s => s.Unit)
        //      .HasMaxLength(50)
        //      .IsRequired(); // กำหนดว่า Unit ต้องไม่เป็น null

        //Property(s => s.QtyByStock)
        //      .IsRequired(); // กำหนดว่า QtyByStock ต้องไม่เป็น null

        //Property(s => s.QtyBalance)
        //      .IsRequired(); // กำหนดว่า QtyBalance ต้องไม่เป็น null

        //Property(s => s.Remark)
        //      .HasMaxLength(500); // กำหนดว่า Remark ต้องไม่เป็น null แต่สามารถมีค่าได้

        //Property(s => s.RemarkCancel)
        //      .HasMaxLength(500); // กำหนดว่า RemarkCancel ต้องไม่เป็น null แต่สามารถมีค่าได้

        //Property(s => s.Status)
        //      .IsRequired(); // กำหนดว่า Status ต้องไม่เป็น null

        //Property(s => s.CreateBy)
        //      .HasMaxLength(255)
        //      .IsRequired();

        //Property(s => s.CreateDate)
        //      .HasColumnType("DATETIME")
        //      .IsRequired();

        //Property(s => s.EditBy)
        //      .HasMaxLength(255)
        //      .IsRequired();

        //Property(s => s.EditDate)
        //      .HasColumnType("DATETIME")
        //      .IsRequired();
    }
}