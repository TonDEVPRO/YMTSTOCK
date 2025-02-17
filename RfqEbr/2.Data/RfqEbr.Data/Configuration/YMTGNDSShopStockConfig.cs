using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;

namespace RfqEbr.Data.Configuration
{

    public class YMTGNDSShopStockConfig : EntityTypeConfiguration<YMTGNDSShopStock>
    {

        public YMTGNDSShopStockConfig()
        {
            ToTable("YMTG_NDSShop_Stocks");

            HasKey(e => e.Id);
            Property(e => e.Id)
                 .HasColumnName("Id")
             .IsRequired();

            Property(e => e.Style)
                 .HasColumnName("Style")
                 .HasMaxLength(250)
                 .IsRequired();

            Property(e => e.Description)
                 .HasColumnName("Description")
                 .HasMaxLength(250);

            Property(e => e.Size)
             .HasColumnName("Size")
              .HasMaxLength(255);


            Property(e => e.Color)
                 .HasColumnName("Color")
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

            Property(e => e.WarehouseName)
                     .HasColumnName("WarehouseName");


            Property(e => e.Location)
                    .HasColumnName("Location");
            Property(e => e.CreateBy)
                 .HasColumnName("CreateBy")
                 .HasMaxLength(150);

            Property(e => e.CreateDate)
                 .HasColumnName("CreateDate");

        }
    }
}















