using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;

namespace RfqEbr.Data.Configuration
{

    public class YMTGOrderGroupConfiguration : EntityTypeConfiguration<YMTGOrderGroup>
    {

        public YMTGOrderGroupConfiguration()
        {
                // กำหนดชื่อของตาราง
                ToTable("YMTG_GroupData");
 
                 HasKey(e => e.Style); // ใช้คอลัมน์ที่เหมาะสมเป็นคีย์หลัก


                Property(e => e.CustomerCode)
                    .HasColumnName("CustomerCode")
                    .HasMaxLength(50);

                Property(e => e.CustomerName)
                    .HasColumnName("CustomerName")
                    .HasMaxLength(100);

                Property(e => e.Season)
                    .HasColumnName("Season")
                    .HasMaxLength(50);

                Property(e => e.Style)
                    .HasColumnName("Style")
                    .HasMaxLength(50);

        }
    }
}















