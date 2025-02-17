using System;
using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.IT.StockDeviceIT;
using RfqEbr.Models.IT.StockDeviceITInLocation;

public class StockDeviceTransactionITConfig : EntityTypeConfiguration<StockTransactionIT>
{
    public StockDeviceTransactionITConfig()
    {
        ToTable("YMTG_ITStockTransaction");

        // กำหนด Composite Primary Key
        HasKey(s => new { s.DocNo, s.Code });

        // กำหนดค่าของ Property ต่างๆ
        Property(s => s.DocNo)
            .HasMaxLength(15);

        Property(s => s.Code)
            .HasMaxLength(10);

        Property(s => s.LocationCode)
            .HasMaxLength(10);

        Property(s => s.TrxDate).IsOptional();

        Property(s => s.TrxType).IsOptional()
            .HasMaxLength(20);

        Property(s => s.TrxName).IsOptional()
            .HasMaxLength(50);

        Property(s => s.Supplier).IsOptional()
            .HasMaxLength(50);

        Property(s => s.TrxDoc).IsOptional()
            .HasMaxLength(50);

        Property(s => s.PoNo).IsOptional()
            .HasMaxLength(50);

        Property(s => s.DeptOut).IsOptional()
            .HasMaxLength(50);

        Property(s => s.DeviceId).IsOptional();

        Property(s => s.DeviceName).IsOptional()
            .HasMaxLength(50);

        Property(s => s.UserName).IsOptional()
            .HasMaxLength(50);

        Property(s => s.Qty).IsOptional();

        Property(s => s.CreateBy)
            .HasMaxLength(50);

        Property(s => s.CreateDate);

        Property(s => s.EditBy)
            .HasMaxLength(50);

        Property(s => s.EditDate);
    }
}