using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;

namespace RfqEbr.Data.Configuration
{

    public class YMTGNDSShopHolidayConfig : EntityTypeConfiguration<YMTGNDSShopHoliday>
    {

        public YMTGNDSShopHolidayConfig()
        {
            ToTable("YMTG_NDSStockHoliday");


            // กำหนดคีย์หลัก
            HasKey(e => e.Id);

            // กำหนดประเภทของคอลัมน์ 'Id' ให้เป็น 'bigint' ในฐานข้อมูล (รองรับ Int64)
            Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint");  // ใช้ 'bigint' เพื่อรองรับ Int64


            Property(e => e.OrderNo)
                 .HasColumnName("OrderNo")
                 .HasMaxLength(250);

            Property(e => e.Style)
                 .HasColumnName("Style")
                 .HasMaxLength(250);

            Property(e => e.Description)
                 .HasColumnName("Description")
                 .HasMaxLength(250);

            Property(e => e.Color)
                 .HasColumnName("Color")
                  .HasMaxLength(255);


            Property(e => e.Size)
           .HasColumnName("Size")
            .HasMaxLength(255);


            Property(e => e.Cost)
                .HasColumnName("Cost");


            Property(e => e.Price)
                 .HasColumnName("Price");


            Property(e => e.Total)
                 .HasColumnName("Total")
 ;

            Property(e => e.Status)
                 .HasColumnName("Status");

            Property(e => e.Remark)
                 .HasColumnName("Remark");

            Property(e => e.CreateBy)
                 .HasColumnName("CreateBy")
                 .HasMaxLength(150);

            Property(e => e.CreateDate)
                 .HasColumnName("CreateDate");

        }
    }
}















