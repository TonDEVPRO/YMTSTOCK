
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class AttachmentsModelConfig : EntityTypeConfiguration<AttachmentsModel>
{
    public AttachmentsModelConfig()
    {

        // Mapping Properties
        // Table Name
        ToTable("YMTG_NDS_OtherFiles");


        HasKey(e => e.Id);

        Property(e => e.Id).HasColumnName("Id").IsRequired();
        Property(e => e.OrderNumber).HasColumnName("OrderNumber").HasMaxLength(255);
        Property(e => e.FileName).HasColumnName("FileName").HasMaxLength(255);
        Property(e => e.FilePath).HasColumnName("FilePath").HasMaxLength(255);

        Property(e => e.FileDescription).HasColumnName("FileDescription").HasMaxLength(255);

        Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime");
        Property(e => e.AddFileBy).HasColumnName("AddFileBy").HasMaxLength(100);
 

    }
}














