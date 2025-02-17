using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;

namespace RfqEbr.Data.Configuration
{

    public class YMTGNDSShopStockLogConfig : EntityTypeConfiguration<YMTGNDSShopStockLog>
    {
        public YMTGNDSShopStockLogConfig()
        {
            ToTable("YMTG_NDSShop_StocksLogs");
            HasKey(e => e.Id);

            Property(e => e.Id)
                 .HasColumnName("Id");
            Property(e => e.StockId)
                 .HasColumnName("StockId");
            Property(e => e.Total)
                 .HasColumnName("Total");
            Property(e => e.AddQty)
                 .HasColumnName("AddQty");
            Property(e => e.TotalQty)
                .HasColumnName("TotalQty");
            Property(e => e.Remark)
                 .HasColumnName("Remark");
            Property(e => e.WareHouseName)
            .HasColumnName("WareHouseName");
            Property(e => e.Location)
            .HasColumnName("Location");
            Property(e => e.CreateBy)
                 .HasColumnName("CreateBy");
            Property(e => e.CreateDate)
                 .HasColumnName("CreateDate");
        }
    }
}















