using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;

namespace RfqEbr.Data.Configuration
{

    public class YMTGOrderDetailConfiguration : EntityTypeConfiguration<YMTGOrderDetail>
    {

        public YMTGOrderDetailConfiguration()
        {
                // กำหนดชื่อของตาราง
                ToTable("YMTG_OrderDetail");
 
                 HasKey(e => e.OrderNo); // ใช้คอลัมน์ที่เหมาะสมเป็นคีย์หลัก

            // กำหนดการแมปของคอลัมน์กับ Properties
             Property(e => e.Division)
                    .HasColumnName("Division")
                    .HasMaxLength(50); // กำหนดความยาวสูงสุด

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
            Property(e => e.OrderCode)
          .HasColumnName("OrderCode")
          .HasMaxLength(50);
            

                Property(e => e.OrderNo)
                    .HasColumnName("OrderNo")
                    .HasMaxLength(50);

                Property(e => e.Cur)
                    .HasColumnName("Cur")
                    .HasMaxLength(10);

                Property(e => e.Status)
                    .HasColumnName("Status")
                    .HasMaxLength(20);

                Property(e => e.OrderQty)
                    .HasColumnName("OrderQty")
                    .IsRequired(); // Assuming OrderQty is a required field

                Property(e => e.IssDate)
                    .HasColumnName("IssDate")
                    .IsRequired();

                //Property(e => e.ShipDate)
                //    .HasColumnName("ShipDate")
                //    .IsRequired();

                //Property(e => e.StatusDate)
                //    .HasColumnName("StatusDate")
                //    .IsRequired();

                Property(e => e.CustPORef)
                    .HasColumnName("CustPORef")
                    .HasMaxLength(100);

        }
    }
}















