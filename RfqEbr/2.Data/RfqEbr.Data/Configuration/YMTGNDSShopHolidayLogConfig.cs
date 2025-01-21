using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;

namespace RfqEbr.Data.Configuration
{

    public class YMTGNDSShopHolidayLogConfig : EntityTypeConfiguration<YMTGNDSShopHolidayLog>
    {

        public YMTGNDSShopHolidayLogConfig()
        {
            ToTable("YMTG_NDSShop_HolidayLog");

            HasKey(e => e.Id);

            Property(e => e.Id)
                 .HasColumnName("Id");

            Property(e => e.HolidayId)
                 .HasColumnName("HolidayId");

            Property(e => e.Total)
                 .HasColumnName("Total");

            Property(e => e.AddQty)
                 .HasColumnName("AddQty");

            Property(e => e.TotalQty)
                .HasColumnName("TotalQty");

            Property(e => e.Remark)
                 .HasColumnName("Remark");

            Property(e => e.CreateBy)
                 .HasColumnName("CreateBy");

            Property(e => e.CreateDate)
                 .HasColumnName("CreateDate");
        }
    }
}















