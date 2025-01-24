
using RfqEbr.Models;
using RfqEbr.Models.Table;
using System;
using System.Data.Entity.ModelConfiguration;

public class RFIDTagsConfig : EntityTypeConfiguration<RFIDTag>
{
    public RFIDTagsConfig()
    {

        ToTable("RFIDTags");

        HasKey(e => e.Id);

        Property(e => e.Id)
             .HasColumnName("Id");

        Property(e => e.EPC)
             .HasColumnName("EPC");

        Property(e => e.ReadTime)
             .HasColumnName("ReadTime");

        Property(e => e.IsActive)
             .HasColumnName("IsActive");
 
    }
}














