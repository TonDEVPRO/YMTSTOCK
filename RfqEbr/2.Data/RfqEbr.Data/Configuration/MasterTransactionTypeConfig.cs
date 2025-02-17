using System;
using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.IT;
using RfqEbr.Models.IT.StockDeviceIT;
using RfqEbr.Models.IT.StockDeviceITInLocation;

public class MasterTransactionTypeConfig : EntityTypeConfiguration<MasterTransactionType>
{
    public MasterTransactionTypeConfig()
    {
        ToTable("YMTG_Master_TransactionType");

        HasKey(s => s.ID);
        Property(s => s.TrxType)
            .HasMaxLength(20);        
        Property(s => s.TrxName)
            .HasMaxLength(50);
    }
}