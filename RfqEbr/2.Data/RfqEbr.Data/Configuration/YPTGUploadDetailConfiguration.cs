using System.Data.Entity.ModelConfiguration;
 
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;

namespace RfqEbr.Data.Configuration
{

    public class YPTGUploadDetailConfiguration : EntityTypeConfiguration<YPTGUploadDetail>
    {

        public YPTGUploadDetailConfiguration()
        {
            ToTable("YPTG_UploadDetail");
            // กำหนดชื่อของตาราง

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("Id")
                .IsRequired();

            Property(e => e.TypeName)
                .HasColumnName("TypeName")
                .HasMaxLength(100); // Adjust as per your schema

            Property(e => e.BrandCode)
                .HasColumnName("BrandCode")
                .HasMaxLength(50); // Adjust as per your schema

            Property(e => e.BrandName)
                .HasColumnName("BrandName")
                .HasMaxLength(255); // Adjust as per your schema

            Property(e => e.Season)
                .HasColumnName("Season")
                .HasMaxLength(50); // Adjust as per your schema

            Property(e => e.StyleName)
                .HasColumnName("StyleName")
                .HasMaxLength(255); // Adjust as per your schema

            Property(e => e.OrderNo)
                .HasColumnName("OrderNo")
                .HasMaxLength(100); // Adjust as per your schema

            Property(e => e.FactoryCode)
                .HasColumnName("FactoryCode")
                .HasMaxLength(100); // Adjust as per your schema

            Property(e => e.Status)
                .HasColumnName("Status");

            Property(e => e.Remark)
                .HasColumnName("Remark")
                .HasMaxLength(500); // Adjust as per your schema

            Property(e => e.CreateBy)
                .HasColumnName("CreateBy")
                .HasMaxLength(50); // Adjust as per your schema

            Property(e => e.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("datetime");
        }
    }
}














