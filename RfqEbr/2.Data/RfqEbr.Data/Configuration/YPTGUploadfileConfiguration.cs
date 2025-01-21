﻿using System.Data.Entity.ModelConfiguration;
 
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace RfqEbr.Data.Configuration
{

    public class YPTGUploadfileConfiguration : EntityTypeConfiguration<YPTGUploadfile>
    {

        public YPTGUploadfileConfiguration()
        {
            ToTable("YPTG_Uploadfile");
            // กำหนดชื่อของตาราง


           HasKey(e => e.Id);

           Property(e => e.Id)
                .HasColumnName("Id")
                .IsRequired();

            Property(e => e.FileId)
                 .HasColumnName("FileId");

           Property(e => e.FileUploadCode)
                .HasColumnName("FileUploadCode")
                .HasMaxLength(100); // Adjust as per your DB schema

           Property(e => e.OriginalFileName)
                .HasColumnName("OriginalFileName")
                .HasMaxLength(255); // Adjust as per your DB schema

           Property(e => e.NewFileName)
                .HasColumnName("NewFileName")
                .HasMaxLength(255); // Adjust as per your DB schema

           Property(e => e.FileExtension)
                .HasColumnName("FileExtension")
                .HasMaxLength(10); // Adjust as per your DB schema

           Property(e => e.TypeName)
                .HasColumnName("TypeName")
                .HasMaxLength(50); // Adjust as per your DB schema

           Property(e => e.FilePathUrl)
                .HasColumnName("FilePathUrl")
                .HasMaxLength(500); // Adjust as per your DB schema

           Property(e => e.FilePathName)
                .HasColumnName("FilePathName")
                .HasMaxLength(500); // Adjust as per your DB schema

            //Property(e => e.ContentType)
            //     .HasColumnName("ContentType")
            //     .HasMaxLength(100); // Adjust as per your DB schema

            //Property(e => e.FileData)
            //     .HasColumnName("FileData");


            
                            Property(e => e.StyleName)
                .HasColumnName("StyleName")
               .HasMaxLength(100);
            Property(e => e.Revision)
                .HasColumnName("Revision")
               .HasMaxLength(100);

            Property(e => e.RevisionDate)
                .HasColumnName("RevisionDate");


            Property(e => e.Status)
                .HasColumnName("Status");


           Property(e => e.Remark)
                .HasColumnName("Remark")
                .HasMaxLength(500); // Adjust as per your DB schema

           Property(e => e.CreateBy)
                .HasColumnName("CreateBy")
                .HasMaxLength(50); // Adjust as per your DB schema

           Property(e => e.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("datetime");

        }
    }
}














