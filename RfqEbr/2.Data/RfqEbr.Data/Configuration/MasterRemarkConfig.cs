
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class MasterRemarkConfig : EntityTypeConfiguration<MasterRemark>
{
    public MasterRemarkConfig()
    {
        // Table Name
        ToTable("YMTG_NDS_QuoMasterRemark");

        // Primary Key
        HasKey(t => t.Id);

        // Properties
        Property(t => t.Id)
            .IsRequired();

        Property(t => t.RemarkQuo)
            .IsRequired()
            .HasMaxLength(250);

  
    }
}














