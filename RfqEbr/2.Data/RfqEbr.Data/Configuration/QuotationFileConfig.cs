
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class QuotationFileConfig : EntityTypeConfiguration<QuotationFile>
{
    public QuotationFileConfig()
    {
        // Table Name
        // กำหนดชื่อ Table
        ToTable("YMTG_NDS_QuotationFiles");

        // Mapping Properties
        HasKey(e => e.Id);

        Property(e => e.QuotationNumber)
            .IsRequired()
            .HasMaxLength(255);

        Property(e => e.FileName)
               .IsRequired()
            .HasMaxLength(255);

        Property(e => e.FilePath)
             .IsRequired()
            .HasMaxLength(255);

        Property(e => e.FileDescription)
                   .IsRequired()
            .HasMaxLength(255);

 
        Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime");

   
    }
}














