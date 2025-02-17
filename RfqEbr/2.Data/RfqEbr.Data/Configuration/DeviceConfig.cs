using System;
using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.IT;
using RfqEbr.Models.IT.StockDeviceIT;
using RfqEbr.Models.IT.StockDeviceITInLocation;

public class DeviceConfig : EntityTypeConfiguration<Device>
{
    public DeviceConfig()
    {
        ToTable("YMTG_DeviceName");

        // กำหนด Primary Key
        HasKey(d => d.DeviceId);  // Deviceld เป็น Primary Key

        // กำหนดค่า Property ต่างๆ
        Property(d => d.DeviceName)
            .HasMaxLength(50)
            .IsRequired(); // DeviceName ไม่สามารถเป็นค่าว่างได้

        Property(d => d.MacAddress)
            .HasMaxLength(20);

        Property(d => d.IPAddressL)
            .HasMaxLength(20);

        Property(d => d.IPAddressW)
            .HasMaxLength(20)
            .IsOptional();

        Property(d => d.AssetNumber)
            .HasMaxLength(20);

        Property(d => d.Dept)
            .HasMaxLength(30)
            .IsOptional();

        Property(d => d.Com_UserName)
            .HasMaxLength(20)
            .IsOptional();

        Property(d => d.SewLine)
            .HasMaxLength(20)
            .IsOptional();

        Property(d => d.Remark)
            .HasMaxLength(100)
            .IsOptional();

        Property(d => d.UserCreate)
            .HasMaxLength(20)
            .IsOptional();

        Property(d => d.UserEdit)
            .HasMaxLength(20)
            .IsOptional();

        Property(d => d.RemarkNotUse)
            .HasMaxLength(150)
            .IsOptional();

        Property(d => d.Location)
            .HasMaxLength(50)
            .IsOptional();

        Property(d => d.DeviceType)
            .HasMaxLength(20)
            .IsOptional();

        Property(d => d.Factory)
            .HasMaxLength(5)
            .IsOptional();

        Property(d => d.OSType)
            .HasMaxLength(50)
            .IsOptional();

        Property(d => d.OSVersion)
            .HasMaxLength(50)
            .IsOptional();

        Property(d => d.OSProductKey)
            .HasMaxLength(50)
            .IsOptional();

        Property(d => d.Brand)
            .HasMaxLength(50)
            .IsOptional();

        Property(d => d.Model)
            .HasMaxLength(50)
            .IsOptional();

        Property(d => d.Processor)
            .HasMaxLength(80)
            .IsOptional();

        Property(d => d.RAM).IsOptional();

        Property(d => d.StorageDetail)
            .HasMaxLength(100)
            .IsOptional();

        Property(d => d.SerialNumber)
            .HasMaxLength(50)
            .IsOptional();

        Property(d => d.DeviceStatus)
            .HasMaxLength(20)
            .IsOptional();

        Property(d => d.LoginUser)
            .HasMaxLength(50)
            .IsOptional();

        Property(d => d.LoginPassword)
            .HasMaxLength(20)
            .IsOptional();
        Property(d => d.USBWireless).IsOptional();
        Property(d => d.WirelessInclude).IsOptional();


        // ... กำหนด Property อื่นๆ ในทำนองเดียวกัน ...

        // กำหนดค่าสำหรับ Column ที่ Allow Nulls เป็น true (Optional)
        Property(d => d.DateCreate)
            .IsOptional(); // อนุญาตให้เป็นค่าว่างได้
        Property(d => d.DateEdit)
            .IsOptional(); // อนุญาตให้เป็นค่าว่างได้
        Property(d => d.WarrantyStart)
            .IsOptional(); // อนุญาตให้เป็นค่าว่างได้
        Property(d => d.WarrantyExpire)
            .IsOptional(); // อนุญาตให้เป็นค่าว่างได้
        Property(d => d.StatusNotUse)
            .IsOptional();

        // ... กำหนด Property DateTime อื่นๆ ที่ Allow Nulls ...
    }
}